// Copyright 2020 ONIXLabs
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace OnixLabs.Security.Cryptography.Jose;

public sealed partial class JsonWebEncryption
{
    /// <summary>
    /// Decrypts the current JWE as the specified recipient.
    /// </summary>
    /// <param name="recipient">
    /// The recipient's key agreement. If its public key has a <c>kid</c>, the recipient entry with the same <c>kid</c> is used; otherwise
    /// the JWE must have exactly one recipient.
    /// </param>
    /// <param name="senderPublicKey">
    /// For ECDH-1PU, the sender's static public key, which the recipient must already trust, for example by resolving
    /// <see cref="SenderKeyId"/>; for ECDH-ES, <see langword="null"/>.
    /// </param>
    /// <param name="cancellationToken">A token used to cancel the operation.</param>
    /// <returns>Returns the plaintext.</returns>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="senderPublicKey"/> is missing for ECDH-1PU or present for ECDH-ES, or its <c>kid</c> differs from
    /// <c>skid</c>.
    /// </exception>
    /// <exception cref="CryptographicException">
    /// Thrown when no recipient entry matches, the header is invalid or uses an unsupported key management algorithm, the key agreement
    /// fails, or the encrypted key fails its integrity check.
    /// </exception>
    /// <exception cref="AuthenticationTagMismatchException">Thrown when the content fails authentication.</exception>
    /// <exception cref="NotSupportedException">Thrown when the content encryption algorithm is not supported, such as XC20P.</exception>
    public async ValueTask<byte[]> DecryptAsync(
        IJweKeyAgreement recipient,
        JsonWebKey? senderPublicKey = null,
        CancellationToken cancellationToken = default)
    {
        byte[] cek = await UnwrapContentEncryptionKeyAsync(recipient, senderPublicKey, cancellationToken).ConfigureAwait(false);

        try
        {
            string encryption = GetHeaderValue("enc", FindRecipient(recipient));
            return JweContentEncryption.Decrypt(encryption, cek, iv, ciphertext, tag, GetAuthenticatedData());
        }
        finally
        {
            CryptographicOperations.ZeroMemory(cek);
        }
    }

    /// <summary>
    /// Derives the key encryption key and unwraps the content encryption key, without decrypting the content.
    /// </summary>
    /// <param name="recipient">The recipient's key agreement.</param>
    /// <param name="senderPublicKey">For ECDH-1PU, the sender's static public key; otherwise <see langword="null"/>.</param>
    /// <param name="cancellationToken">A token used to cancel the operation.</param>
    /// <returns>Returns the content encryption key, which the caller must zero.</returns>
    /// <remarks>
    /// Why: this is separate from <see cref="DecryptAsync"/> so that the key agreement and key unwrapping of a JWE whose content encryption
    /// is not supported, such as DIDComm's optional XC20P, can still be checked. The key wrap's integrity check authenticates the result.
    /// </remarks>
    internal async ValueTask<byte[]> UnwrapContentEncryptionKeyAsync(
        IJweKeyAgreement recipient,
        JsonWebKey? senderPublicKey,
        CancellationToken cancellationToken)
    {
        RequireNotNull(recipient);

        JweRecipient entry = FindRecipient(recipient);
        string algorithm = GetHeaderValue("alg", entry);
        (bool isOnePassUnified, int keyWrapLength) = JweAlgorithm.GetKeyManagement(algorithm);
        (bool isCbcHmac, int keyLength) = TryGetContentEncryption(GetHeaderValue("enc", entry));

        if (ProtectedHeader.TryGetProperty("crit", out _) || HasMember(entry, "crit"))
            throw new CryptographicException("The JWE has a 'crit' header member, and no extensions are supported.");

        JsonElement epkElement = FindObject("epk", entry)
            ?? throw new CryptographicException("The JWE has no ephemeral public key, 'epk'.");
        using JsonWebKey ephemeralPublicKey = JsonWebKey.FromJson(epkElement);
        JweKeyAgreement.RequireSameCurve(recipient.PublicKey, ephemeralPublicKey);

        IJweKeyAgreement? localStatic = null;

        if (isOnePassUnified)
        {
            Require(senderPublicKey is not null, "ECDH-1PU requires the sender's public key.", nameof(senderPublicKey));
            JweKeyAgreement.RequireSameCurve(recipient.PublicKey, senderPublicKey!);

            string? senderKeyId = JoseJson.FindString("skid", ProtectedHeader, UnprotectedHeader, entry.Header);
            Require(senderKeyId is null || senderPublicKey!.KeyId is null || senderKeyId == senderPublicKey.KeyId,
                "The sender's public key does not have the key identifier named by 'skid'.", nameof(senderPublicKey));

            if (!isCbcHmac)
                throw new CryptographicException("ECDH-1PU key wrapping requires an AES-CBC-HMAC-SHA2 content encryption algorithm.");

            localStatic = recipient;
        }
        else
        {
            Require(senderPublicKey is null, "ECDH-ES is anonymous, so it has no sender's public key.", nameof(senderPublicKey));
        }

        byte[] otherInfo = JweOtherInfo.Create(
            algorithm,
            FindBytes("apu", entry),
            FindBytes("apv", entry),
            keyWrapLength * 8,
            isOnePassUnified ? tag : default);

        byte[] kek = await DeriveKeyEncryptionKeyAsync(
                recipient,
                localStatic,
                ephemeralPublicKey,
                senderPublicKey ?? ephemeralPublicKey,
                otherInfo,
                keyWrapLength,
                cancellationToken)
            .ConfigureAwait(false);

        try
        {
            if (entry.EncryptedKey.Length < 24 || entry.EncryptedKey.Length % 8 != 0)
                throw new CryptographicException("The encrypted key is not a valid length for AES Key Wrap.");

            using Aes keyWrap = CreateKeyWrap(kek);
            byte[] cek = keyWrap.DecryptKeyWrap(entry.EncryptedKey);

            if (keyLength < 0 || cek.Length == keyLength) return cek;

            CryptographicOperations.ZeroMemory(cek);
            throw new CryptographicException("The content encryption key has the wrong length for the content encryption algorithm.");
        }
        finally
        {
            CryptographicOperations.ZeroMemory(kek);
        }
    }

    /// <summary>
    /// Gets the properties of the specified content encryption algorithm, if it is supported.
    /// </summary>
    /// <param name="encryption">The JWE <c>enc</c> value.</param>
    /// <returns>Returns whether it is an AES-CBC-HMAC algorithm, and its key length in bytes, or -1 if the algorithm is not
    /// supported.</returns>
    private static (bool IsCbcHmac, int KeyLength) TryGetContentEncryption(string encryption)
    {
        try
        {
            (bool isCbcHmac, int keyLength, _, _) = JweAlgorithm.GetContentEncryption(encryption);
            return (isCbcHmac, keyLength);
        }
        catch (NotSupportedException)
        {
            return (false, -1);
        }
    }

    /// <summary>
    /// Finds the recipient entry for the specified key agreement.
    /// </summary>
    /// <param name="recipient">The recipient's key agreement.</param>
    /// <returns>Returns the matching recipient entry.</returns>
    /// <exception cref="CryptographicException">Thrown when no entry, or more than one, matches.</exception>
    private JweRecipient FindRecipient(IJweKeyAgreement recipient)
    {
        string? keyId = recipient.PublicKey.KeyId;

        if (keyId is null)
            return Recipients.Count == 1 ? Recipients[0]
                : throw new CryptographicException("The recipient key has no 'kid', and the JWE has several recipients.");

        JweRecipient[] matches = Recipients.Where(entry => entry.KeyId == keyId).ToArray();

        return matches.Length switch
        {
            1 => matches[0],
            0 => throw new CryptographicException($"The JWE has no recipient with 'kid' '{keyId}'."),
            _ => throw new CryptographicException($"The JWE has more than one recipient with 'kid' '{keyId}'.")
        };
    }

    /// <summary>
    /// Gets the specified required string member from the protected, shared unprotected, or recipient header.
    /// </summary>
    /// <param name="name">The member name.</param>
    /// <param name="entry">The recipient entry.</param>
    /// <returns>Returns the string value.</returns>
    /// <exception cref="CryptographicException">Thrown when no header has the member.</exception>
    private string GetHeaderValue(string name, JweRecipient entry) =>
        JoseJson.FindString(name, ProtectedHeader, UnprotectedHeader, entry.Header)
            ?? throw new CryptographicException($"The JWE header has no '{name}'.");

    /// <summary>
    /// Gets and decodes the specified optional base64url member from the protected, shared unprotected, or recipient header.
    /// </summary>
    /// <param name="name">The member name.</param>
    /// <param name="entry">The recipient entry.</param>
    /// <returns>Returns the decoded bytes, or empty if no header has the member.</returns>
    /// <exception cref="CryptographicException">Thrown when the member is not valid base64url.</exception>
    private byte[] FindBytes(string name, JweRecipient entry)
    {
        string? value = JoseJson.FindString(name, ProtectedHeader, UnprotectedHeader, entry.Header);

        if (value is null) return [];

        return JoseJson.TryDecodeBytes(value, out byte[] bytes) ? bytes
            : throw new CryptographicException($"The JWE header member '{name}' is not valid base64url.");
    }

    /// <summary>
    /// Gets the specified optional object member from the protected, shared unprotected, or recipient header.
    /// </summary>
    /// <param name="name">The member name.</param>
    /// <param name="entry">The recipient entry.</param>
    /// <returns>Returns the object, or <see langword="null"/> if no header has the member.</returns>
    private JsonElement? FindObject(string name, JweRecipient entry)
    {
        foreach (JsonElement? header in new[] { ProtectedHeader, UnprotectedHeader, entry.Header })
            if (header is { } value && JoseJson.GetOptionalObject(value, name) is { } member)
                return member;

        return null;
    }

    /// <summary>
    /// Determines whether the shared unprotected or recipient header has the specified member.
    /// </summary>
    /// <param name="entry">The recipient entry.</param>
    /// <param name="name">The member name.</param>
    /// <returns>Returns <see langword="true"/> if either header has the member; otherwise, <see langword="false"/>.</returns>
    private bool HasMember(JweRecipient entry, string name) =>
        UnprotectedHeader is { } shared && shared.TryGetProperty(name, out _)
        || entry.Header is { } header && header.TryGetProperty(name, out _);
}
