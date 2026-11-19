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
using System.Security.Cryptography;

namespace OnixLabs.Security.Cryptography;

/// <summary>
/// Represents the key derivation of ECDH-1PU (Elliptic Curve Diffie-Hellman One-Pass Unified Model), as specified in
/// draft-madden-jose-ecdh-1pu-04 §2.3, which DIDComm Messaging v2.1 uses for authenticated encryption ("authcrypt").
/// </summary>
/// <remarks>
/// <para>
/// ECDH-1PU combines two key agreements on the same curve. The sender computes Ze from its ephemeral private key and the recipient's public
/// key, and Zs from its static private key and the recipient's public key. The recipient computes Ze from its private key and the ephemeral
/// public key (<c>epk</c>), and Zs from its private key and the sender's static public key. Both then derive the key from Z = Ze ‖ Zs with
/// the
/// Concat KDF and SHA-256.
/// </para>
/// <para>
/// Why: each half is taken as raw bytes, so that callers whose private keys never leave a key store can compute each agreement there, with
/// <see cref="XdhPrivateKey.DeriveSharedSecret"/> or <see cref="EcdhPrivateKey.DeriveRawSharedSecret"/>, and pass the results in.
/// </para>
/// </remarks>
public static class Ecdh1Pu
{
    /// <summary>
    /// Derives an ECDH-1PU key from the ephemeral and static shared secrets and the specified Concat KDF <c>OtherInfo</c>.
    /// </summary>
    /// <param name="ephemeralSharedSecret">The raw shared secret Ze of the ephemeral key agreement.</param>
    /// <param name="staticSharedSecret">The raw shared secret Zs of the static key agreement, which must be the same length as Ze.</param>
    /// <param name="otherInfo">
    /// The Concat KDF <c>OtherInfo</c> (the draft's FixedInfo): <c>AlgorithmID ‖ PartyUInfo ‖ PartyVInfo ‖ SuppPubInfo ‖ SuppPrivInfo</c>,
    /// where in
    /// key wrapping mode SuppPubInfo carries the JWE authentication tag.
    /// </param>
    /// <param name="destination">The buffer that receives the derived key; its length is the key length.</param>
    /// <exception cref="ArgumentException">
    /// Thrown when either shared secret is empty, the two are of different lengths (and so not from the same curve), or
    /// <paramref name="destination"/> is empty.
    /// </exception>
    public static void DeriveKey(
        ReadOnlySpan<byte> ephemeralSharedSecret,
        ReadOnlySpan<byte> staticSharedSecret,
        ReadOnlySpan<byte> otherInfo,
        Span<byte> destination)
    {
        Require
        (
            condition: ephemeralSharedSecret.Length > 0,
            message: "The ephemeral shared secret must not be empty.",
            parameterName: nameof(ephemeralSharedSecret)
        );

        Require
        (
            condition: staticSharedSecret.Length == ephemeralSharedSecret.Length,
            message: "The static and ephemeral shared secrets must be the same length, from key agreements on the same curve.",
            parameterName: nameof(staticSharedSecret)
        );

        int length = ephemeralSharedSecret.Length + staticSharedSecret.Length;
        Span<byte> z = length <= 256 ? stackalloc byte[256] : new byte[length];
        z = z[..length];

        try
        {
            ephemeralSharedSecret.CopyTo(z);
            staticSharedSecret.CopyTo(z[ephemeralSharedSecret.Length..]);
            ConcatKdf.DeriveKey(HashAlgorithmName.SHA256, z, otherInfo, destination);
        }
        finally
        {
            CryptographicOperations.ZeroMemory(z);
        }
    }
}
