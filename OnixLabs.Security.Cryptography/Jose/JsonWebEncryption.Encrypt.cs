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
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;

namespace OnixLabs.Security.Cryptography.Jose;

public sealed partial class JsonWebEncryption
{
    /// <summary>
    /// Encrypts the specified plaintext to the specified recipients, with ECDH-ES (anonymous) or ECDH-1PU (authenticated) key agreement.
    /// </summary>
    /// <param name="plaintext">The plaintext to encrypt.</param>
    /// <param name="recipients">
    /// The recipients' public keys, at least one, all on the same curve: X25519, P-256, P-384 or P-521. Each key's <c>kid</c>, if any, is
    /// written to its per-recipient header.
    /// </param>
    /// <param name="options">The algorithms, sender key and headers to use.</param>
    /// <param name="cancellationToken">A token used to cancel the operation.</param>
    /// <returns>
    /// Returns a new <see cref="JsonWebEncryption"/>, whose protected header carries <c>typ</c> (if given), <c>alg</c>, <c>enc</c>,
    /// <c>skid</c> (for ECDH-1PU, if the sender's key has a <c>kid</c>), <c>apu</c> and <c>apv</c> (if given), and the shared ephemeral key
    /// <c>epk</c>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown when there are no recipients, the recipients are not all on the same curve, or the sender is missing for ECDH-1PU or present
    /// for ECDH-ES.
    /// </exception>
    /// <exception cref="CryptographicException">
    /// Thrown when an algorithm is not supported, or ECDH-1PU is combined with a content encryption that is not AES-CBC-HMAC-SHA2, which
    /// draft-madden-jose-ecdh-1pu-04 §2.1 requires in key wrapping mode.
    /// </exception>
    /// <remarks>
    /// For ECDH-1PU the content is encrypted first, and its authentication tag is then bound into every recipient's key derivation, as
    /// draft-madden-jose-ecdh-1pu-04 §2.1 requires, so that one recipient cannot forge a message to another in the sender's name.
    /// </remarks>
    public static async ValueTask<JsonWebEncryption> EncryptAsync(
        ReadOnlyMemory<byte> plaintext,
        IReadOnlyList<JsonWebKey> recipients,
        JweEncryptionOptions options,
        CancellationToken cancellationToken = default)
    {
        RequireNotNull(recipients);
        RequireNotNull(options);
        Require(recipients.Count > 0, "At least one recipient is required.", nameof(recipients));

        (bool isOnePassUnified, int keyWrapLength) = JweAlgorithm.GetKeyManagement(options.Algorithm);
        (bool isCbcHmac, int keyLength, int ivLength, _) = JweAlgorithm.GetContentEncryption(options.EncryptionAlgorithm);
        JsonWebKey curveKey = recipients[0];

        foreach (JsonWebKey recipient in recipients) RequireRecipientCurve(curveKey, recipient);

        if (isOnePassUnified)
        {
            Require(options.Sender is not null, "ECDH-1PU requires the sender's key.", nameof(options));
            Require(options.Sender!.PublicKey.KeyType == curveKey.KeyType && options.Sender.PublicKey.Curve == curveKey.Curve,
                "The sender's key must be on the same curve as the recipients' keys.", nameof(options));

            if (!isCbcHmac)
                throw new CryptographicException("ECDH-1PU key wrapping requires an AES-CBC-HMAC-SHA2 content encryption algorithm.");
        }
        else
        {
            Require(options.Sender is null, "ECDH-ES is anonymous, so it must not have a sender's key.", nameof(options));
        }

        IJweKeyAgreement ephemeral = options.EphemeralKeyFactory?.Invoke(curveKey) ?? CreateEphemeralKey(curveKey);
        byte[] cek = options.ContentEncryptionKey?.ToArray() ?? RandomNumberGenerator.GetBytes(keyLength);
        byte[] iv = options.InitializationVector?.ToArray() ?? RandomNumberGenerator.GetBytes(ivLength);

        try
        {
            string headerJson = JoseJson.Write(CreateProtectedHeader(options, ephemeral.PublicKey.ToPublicKey().WithKeyId(null)));
            string encodedHeader = JoseJson.EncodeBytes(Encoding.UTF8.GetBytes(headerJson));
            byte[] associatedData = Encoding.ASCII.GetBytes(encodedHeader);
            (byte[] ciphertext, byte[] tag) =
                JweContentEncryption.Encrypt(options.EncryptionAlgorithm, cek, iv, plaintext.Span, associatedData);
            byte[] otherInfo = JweOtherInfo.Create(
                options.Algorithm,
                options.AgreementPartyUInfo,
                options.AgreementPartyVInfo,
                keyWrapLength * 8,
                isOnePassUnified ? tag : default);
            List<JweRecipient> entries = [];

            foreach (JsonWebKey recipient in recipients)
            {
                using JsonWebKey recipientPublicKey = recipient.ToPublicKey();
                byte[] kek = await DeriveKeyEncryptionKeyAsync(
                        ephemeral,
                        options.Sender,
                        recipientPublicKey,
                        recipientPublicKey,
                        otherInfo,
                        keyWrapLength,
                        cancellationToken)
                    .ConfigureAwait(false);

                try
                {
                    JsonElement? header = recipient.KeyId is null
                        ? null
                        : JoseJson.ParseObject(JoseJson.Write(new JsonObject { ["kid"] = recipient.KeyId }), "JWE recipient header");

                    using Aes keyWrap = CreateKeyWrap(kek);
                    entries.Add(new JweRecipient(header, keyWrap.EncryptKeyWrap(cek)));
                }
                finally
                {
                    CryptographicOperations.ZeroMemory(kek);
                }
            }

            JsonElement? unprotectedHeader = CreateUnprotectedHeader(options.SharedUnprotectedHeader);

            JsonElement protectedHeader = JoseJson.ParseObject(headerJson, "JWE protected header");
            JoseJson.RequireDisjoint(protectedHeader, unprotectedHeader);

            return new JsonWebEncryption(encodedHeader, protectedHeader, unprotectedHeader, entries, iv, ciphertext, tag, null);
        }
        finally
        {
            CryptographicOperations.ZeroMemory(cek);
        }
    }

    /// <summary>
    /// Derives the key encryption key for one recipient: Concat KDF over Ze for ECDH-ES, or over Ze ‖ Zs for ECDH-1PU.
    /// </summary>
    /// <param name="local">The party computing Ze: the ephemeral key when sending, or the recipient's key when receiving.</param>
    /// <param name="localStatic">The party computing Zs for ECDH-1PU: the sender's key when sending, or the recipient's key when
    /// receiving.</param>
    /// <param name="ephemeralPeer">The peer of <paramref name="local"/>: the recipient's public key when sending, or <c>epk</c> when
    /// receiving.</param>
    /// <param name="staticPeer">The peer of <paramref name="localStatic"/>: the recipient's public key when sending, or the sender's when
    /// receiving.</param>
    /// <param name="otherInfo">The Concat KDF <c>OtherInfo</c>.</param>
    /// <param name="keyWrapLength">The length of the key encryption key, in bytes.</param>
    /// <param name="cancellationToken">A token used to cancel the operation.</param>
    /// <returns>Returns the key encryption key, which the caller must zero.</returns>
    private static async ValueTask<byte[]> DeriveKeyEncryptionKeyAsync(
        IJweKeyAgreement local,
        IJweKeyAgreement? localStatic,
        JsonWebKey ephemeralPeer,
        JsonWebKey staticPeer,
        byte[] otherInfo,
        int keyWrapLength,
        CancellationToken cancellationToken)
    {
        byte[] kek = new byte[keyWrapLength];
        using Secret ephemeralSecret = await local.DeriveSharedSecretAsync(ephemeralPeer, cancellationToken).ConfigureAwait(false);
        byte[] ze = ephemeralSecret.AsReadOnlySpan().ToArray();

        try
        {
            if (localStatic is null)
            {
                ConcatKdf.DeriveKey(HashAlgorithmName.SHA256, ze, kek, otherInfo);
                return kek;
            }

            using Secret staticSecret = await localStatic.DeriveSharedSecretAsync(staticPeer, cancellationToken).ConfigureAwait(false);
            byte[] zs = staticSecret.AsReadOnlySpan().ToArray();

            try
            {
                Ecdh1Pu.DeriveKey(ze, zs, kek, otherInfo);
                return kek;
            }
            finally
            {
                CryptographicOperations.ZeroMemory(zs);
            }
        }
        finally
        {
            CryptographicOperations.ZeroMemory(ze);
        }
    }

    /// <summary>
    /// Creates the protected header, with members in the order <c>typ</c>, <c>alg</c>, <c>enc</c>, <c>skid</c>, <c>apu</c>, <c>apv</c>,
    /// <c>epk</c>.
    /// </summary>
    /// <param name="options">The encryption options.</param>
    /// <param name="ephemeralPublicKey">The ephemeral public key, without <c>kid</c>.</param>
    /// <returns>Returns a new <see cref="JsonObject"/>.</returns>
    private static JsonObject CreateProtectedHeader(JweEncryptionOptions options, JsonWebKey ephemeralPublicKey)
    {
        JsonObject header = [];

        if (options.Type is not null) header["typ"] = options.Type;
        header["alg"] = options.Algorithm;
        header["enc"] = options.EncryptionAlgorithm;
        if (options.Sender?.PublicKey.KeyId is { } senderKeyId) header["skid"] = senderKeyId;
        if (options.AgreementPartyUInfo is { } apu) header["apu"] = JoseJson.EncodeBytes(apu);
        if (options.AgreementPartyVInfo is { } apv) header["apv"] = JoseJson.EncodeBytes(apv);
        header["epk"] = ephemeralPublicKey.ToJsonObject();

        return header;
    }

    /// <summary>
    /// Creates the shared unprotected header from the specified string members.
    /// </summary>
    /// <param name="members">The members, or <see langword="null"/>.</param>
    /// <returns>Returns the header as a JSON object, or <see langword="null"/> if there are no members.</returns>
    private static JsonElement? CreateUnprotectedHeader(IReadOnlyDictionary<string, string>? members)
    {
        if (members is not { Count: > 0 }) return null;

        JsonObject header = [];
        foreach ((string name, string value) in members) header[name] = value;

        return JoseJson.ParseObject(JoseJson.Write(header), "JWE unprotected header");
    }

    /// <summary>
    /// Creates a fresh ephemeral key on the curve of the specified key.
    /// </summary>
    /// <param name="curveKey">A recipient's public key.</param>
    /// <returns>Returns a key agreement for a new, random ephemeral key.</returns>
    private static IJweKeyAgreement CreateEphemeralKey(JsonWebKey curveKey) => curveKey.Curve switch
    {
        "X25519" => JweKeyAgreement.Create(XdhPrivateKey.Create()),
        "P-256" => JweKeyAgreement.Create(EcdhPrivateKey.Create(ECCurve.NamedCurves.nistP256)),
        "P-384" => JweKeyAgreement.Create(EcdhPrivateKey.Create(ECCurve.NamedCurves.nistP384)),
        "P-521" => JweKeyAgreement.Create(EcdhPrivateKey.Create(ECCurve.NamedCurves.nistP521)),
        _ => throw new CryptographicException($"Key agreement on '{curveKey.Curve}' is not supported.")
    };

    /// <summary>
    /// Checks that a recipient key is a key agreement key on the same curve as the first recipient.
    /// </summary>
    /// <param name="curveKey">The first recipient's key.</param>
    /// <param name="recipient">The recipient key to check.</param>
    /// <exception cref="ArgumentException">Thrown when the recipient is on another curve, or its curve is not a key agreement
    /// curve.</exception>
    private static void RequireRecipientCurve(JsonWebKey curveKey, JsonWebKey recipient)
    {
        RequireNotNull(recipient);
        Require(
            recipient.Curve is "X25519" or "P-256" or "P-384" or "P-521",
            $"Key agreement on '{recipient.Curve}' is not supported.",
            nameof(recipient));
        Require(recipient.KeyType == curveKey.KeyType && recipient.Curve == curveKey.Curve,
            "Every recipient key must be on the same curve, because the recipients share one ephemeral key.", nameof(recipient));
    }
}
