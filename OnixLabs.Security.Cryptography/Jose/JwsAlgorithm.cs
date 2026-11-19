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

using System.Security.Cryptography;

namespace OnixLabs.Security.Cryptography.Jose;

/// <summary>
/// Represents the JWS <c>alg</c> values supported for signing and verification, and the key each requires.
/// </summary>
public static class JwsAlgorithm
{
    /// <summary>
    /// EdDSA with Ed25519 (RFC 8037 §3.1).
    /// </summary>
    public const string EdDsa = "EdDSA";

    /// <summary>
    /// ECDSA with P-256 and SHA-256 (RFC 7518 §3.4).
    /// </summary>
    public const string Es256 = "ES256";

    /// <summary>
    /// ECDSA with P-384 and SHA-384 (RFC 7518 §3.4).
    /// </summary>
    public const string Es384 = "ES384";

    /// <summary>
    /// ECDSA with P-521 and SHA-512 (RFC 7518 §3.4).
    /// </summary>
    public const string Es512 = "ES512";

    /// <summary>
    /// ECDSA with secp256k1 and SHA-256 (RFC 8812 §3.2). Supported for verification only.
    /// </summary>
    public const string Es256K = "ES256K";

    /// <summary>
    /// Gets the curve and hash algorithm of the specified ECDSA algorithm.
    /// </summary>
    /// <param name="algorithm">The JWS <c>alg</c> value.</param>
    /// <param name="curve">When this method returns, contains the JOSE curve name the algorithm requires.</param>
    /// <param name="hashAlgorithm">When this method returns, contains the hash algorithm the algorithm uses.</param>
    /// <returns>Returns <see langword="true"/> if <paramref name="algorithm"/> is a supported ECDSA algorithm; otherwise,
    /// <see langword="false"/>.</returns>
    internal static bool TryGetEcdsaParameters(string algorithm, out string curve, out HashAlgorithmName hashAlgorithm)
    {
        (curve, hashAlgorithm) = algorithm switch
        {
            Es256 => ("P-256", HashAlgorithmName.SHA256),
            Es384 => ("P-384", HashAlgorithmName.SHA384),
            Es512 => ("P-521", HashAlgorithmName.SHA512),
            Es256K => ("secp256k1", HashAlgorithmName.SHA256),
            _ => (string.Empty, default)
        };

        return curve.Length > 0;
    }

    /// <summary>
    /// Gets the ECDSA algorithm that signs with keys on the specified curve.
    /// </summary>
    /// <param name="curve">The JOSE curve name.</param>
    /// <returns>Returns the JWS <c>alg</c> value.</returns>
    /// <exception cref="CryptographicException">Thrown when the curve has no supported ECDSA algorithm.</exception>
    internal static string GetEcdsaAlgorithm(string curve) => curve switch
    {
        "P-256" => Es256,
        "P-384" => Es384,
        "P-521" => Es512,
        "secp256k1" => Es256K,
        _ => throw new CryptographicException($"The curve '{curve}' has no supported ECDSA algorithm.")
    };
}
