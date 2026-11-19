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
using System.Text.Json;
using OnixLabs.Security.Cryptography.Jose;

namespace OnixLabs.Security.Cryptography.UnitTests.Jose;

/// <summary>
/// Represents lookups of the DIDComm Messaging v2.1 test keys, from the DID documents of Appendix B and the secrets of Appendix A.
/// </summary>
internal static class DidCommKeys
{
    /// <summary>
    /// Finds the public key with the specified identifier in the specified DID document.
    /// </summary>
    /// <param name="didDocument">The DID document.</param>
    /// <param name="keyId">The verification method identifier, which becomes the key's <c>kid</c>.</param>
    /// <returns>Returns the public key as a JWK with the specified <c>kid</c>.</returns>
    public static JsonWebKey FindPublicKey(JsonElement didDocument, string keyId)
    {
        foreach (string relationship in (string[])["authentication", "keyAgreement"])
        {
            if (!didDocument.TryGetProperty(relationship, out JsonElement methods)) continue;

            foreach (JsonElement method in methods.EnumerateArray())
                if (method.GetProperty("id").GetString() == keyId)
                    return JsonWebKey.Parse(method.GetProperty("publicKeyJwk").GetRawText()).WithKeyId(keyId);
        }

        throw new InvalidOperationException($"No key '{keyId}' in the DID document.");
    }

    /// <summary>
    /// Finds the secret with the specified identifier in the specified list of Appendix A secrets.
    /// </summary>
    /// <param name="secrets">The secrets.</param>
    /// <param name="keyId">The key identifier.</param>
    /// <returns>Returns the private key as a JWK with the specified <c>kid</c>.</returns>
    /// <remarks>Why: the recipient secrets of Appendix A.2 spell the member <c>"kid "</c>, with a trailing space, so both spellings are
    /// matched.</remarks>
    public static JsonWebKey FindSecret(JsonElement secrets, string keyId)
    {
        JsonElement secret = secrets.EnumerateArray().Single(element =>
            (element.TryGetProperty("kid", out JsonElement kid) || element.TryGetProperty("kid ", out kid)) && kid.GetString() == keyId);

        return JsonWebKey.Parse(secret.GetRawText()).WithKeyId(keyId);
    }

    /// <summary>
    /// Creates the key agreement for the recipient secret with the specified identifier.
    /// </summary>
    /// <param name="secrets">The secrets.</param>
    /// <param name="keyId">The key identifier.</param>
    /// <returns>Returns a key agreement backed by the secret, whose public key has the specified <c>kid</c>.</returns>
    public static IJweKeyAgreement CreateKeyAgreement(JsonElement secrets, string keyId)
    {
        using JsonWebKey secret = FindSecret(secrets, keyId);

        return secret.Curve == "X25519"
            ? JweKeyAgreement.Create(secret.ToXdhPrivateKey(), keyId)
            : JweKeyAgreement.Create(secret.ToEcdhPrivateKey(), keyId);
    }
}
