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

public sealed partial class EcdhPrivateKey
{
    /// <inheritdoc/>
    /// <remarks>
    /// Why: hashing the raw secret here, rather than calling <see cref="ECDiffieHellman.DeriveKeyMaterial"/>, gives the same result, but
    /// validates the other party's public key as <see cref="DeriveRawSharedSecret"/> does, and matches
    /// <see cref="XdhPrivateKey.DeriveSharedSecret"/>.
    /// </remarks>
    public Secret DeriveSharedSecret(IEcdhPublicKey publicKey)
    {
        using Secret rawSecret = DeriveRawSharedSecret(publicKey);
        Span<byte> secretData = stackalloc byte[SHA256.HashSizeInBytes];

        try
        {
            SHA256.HashData(rawSecret.AsReadOnlySpan(), secretData);
            return new Secret(secretData);
        }
        finally
        {
            CryptographicOperations.ZeroMemory(secretData);
        }
    }

    /// <inheritdoc/>
    public Secret DeriveRawSharedSecret(IEcdhPublicKey publicKey)
    {
        // Why: an interface with static abstract members cannot be a type argument, so the check is made on the key as an object.
        RequireNotNull<object>(publicKey);

        using ECDiffieHellman ourKey = ImportKeyData();
        using ECDiffieHellman theirKey = ImportValidatedPublicKey(publicKey, ourKey.ExportParameters(false).Curve);
        using ECDiffieHellmanPublicKey theirPublicKey = theirKey.PublicKey;
        byte[] secretData = ourKey.DeriveRawSecretAgreement(theirPublicKey);

        try
        {
            return new Secret(secretData);
        }
        finally
        {
            CryptographicOperations.ZeroMemory(secretData);
        }
    }

    /// <summary>
    /// Imports the specified public key, and checks that it is a point on the specified named curve.
    /// </summary>
    /// <param name="publicKey">The EC Diffie-Hellman cryptographic public key to import.</param>
    /// <param name="curve">The named curve of the current private key, on which the public key must lie.</param>
    /// <returns>Returns a new <see cref="ECDiffieHellman"/> instance containing the imported public key.</returns>
    /// <exception cref="CryptographicException">
    /// Thrown when the public key cannot be imported, is not on the same named curve as <paramref name="curve"/>, or is not a point on it.
    /// </exception>
    /// <remarks>
    /// Why: DIDComm and NIST SP 800-56A require the other party's public key to be validated before use, or an invalid curve attack can
    /// recover the private key. Platforms differ: macOS rejects off-curve points on import, and other platforms are believed to, but the
    /// check is made here too so that the behaviour does not depend on the platform. Explicitly parameterised curves are refused for the
    /// same reason, since only some platforms import them.
    /// </remarks>
    private static ECDiffieHellman ImportValidatedPublicKey(IEcdhPublicKey publicKey, ECCurve curve)
    {
        ECDiffieHellman algorithm = ECDiffieHellman.Create();

        try
        {
            algorithm.ImportSubjectPublicKeyInfo(publicKey.AsReadOnlySpan(), out int _);
            ECParameters parameters = algorithm.ExportParameters(false);

            if (!parameters.Curve.IsNamed || parameters.Curve.Oid.Value != curve.Oid.Value)
                throw new CryptographicException("The EC Diffie-Hellman public key is not on the same named curve as the private key.");

            if (!NistCurvePoint.IsOnCurve(parameters))
                throw new CryptographicException("The EC Diffie-Hellman public key is not a point on its curve.");

            return algorithm;
        }
        catch (Exception exception) when (exception is not CryptographicException)
        {
            algorithm.Dispose();

            // Why: macOS reports explicit curves as PlatformNotSupportedException and malformed keys as ArgumentException, while the
            // rest of the key surface reports invalid input as CryptographicException.
            throw new CryptographicException("The EC Diffie-Hellman public key could not be imported.", exception);
        }
        catch
        {
            algorithm.Dispose();
            throw;
        }
    }
}
