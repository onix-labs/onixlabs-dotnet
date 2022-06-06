// Copyright 2020-2022 ONIXLabs
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

namespace OnixLabs.Security.Cryptography;

public sealed partial class KeyPair
{
    /// <summary>
    /// Creates an ECDSA public/private key pair.
    /// </summary>
    /// <param name="type">The <see cref="HashAlgorithmType"/> for computing signature data.</param>
    /// <returns>Returns an ECDSA public/private key pair.</returns>
    public static KeyPair CreateEcdsaKeyPair(HashAlgorithmType type)
    {
        using ECDsa asymmetricAlgorithm = ECDsa.Create();

        PrivateKey privateKey = new EcdsaPrivateKey(asymmetricAlgorithm.ExportECPrivateKey(), type);
        PublicKey publicKey = new EcdsaPublicKey(asymmetricAlgorithm.ExportSubjectPublicKeyInfo(), type);

        return new KeyPair(privateKey, publicKey, type);
    }

    /// <summary>
    /// Creates an ECDSA public/private key pair.
    /// </summary>
    /// <param name="type">The <see cref="HashAlgorithmType"/> for computing signature data.</param>
    /// <param name="curve">The curve to use for key generation.</param>
    /// <returns>Returns an ECDSA public/private key pair.</returns>
    public static KeyPair CreateEcdsaKeyPair(HashAlgorithmType type, ECCurve curve)
    {
        using ECDsa asymmetricAlgorithm = ECDsa.Create(curve);

        PrivateKey privateKey = new EcdsaPrivateKey(asymmetricAlgorithm.ExportECPrivateKey(), type);
        PublicKey publicKey = new EcdsaPublicKey(asymmetricAlgorithm.ExportSubjectPublicKeyInfo(), type);

        return new KeyPair(privateKey, publicKey, type);
    }

    /// <summary>
    /// Creates an ECDSA public/private key pair.
    /// </summary>
    /// <param name="type">The <see cref="HashAlgorithmType"/> for computing signature data.</param>
    /// <param name="parameters">The parameters representing the key to use.</param>
    /// <returns>Returns an ECDSA public/private key pair.</returns>
    public static KeyPair CreateEcdsaKeyPair(HashAlgorithmType type, ECParameters parameters)
    {
        using ECDsa asymmetricAlgorithm = ECDsa.Create(parameters);

        PrivateKey privateKey = new EcdsaPrivateKey(asymmetricAlgorithm.ExportECPrivateKey(), type);
        PublicKey publicKey = new EcdsaPublicKey(asymmetricAlgorithm.ExportSubjectPublicKeyInfo(), type);

        return new KeyPair(privateKey, publicKey, type);
    }

    /// <summary>
    /// Creates an RSA public/private key pair.
    /// </summary>
    /// <param name="type">The <see cref="HashAlgorithmType"/> for computing signature data.</param>
    /// <param name="padding">The <see cref="RSASignaturePadding" /> for computing signature data.</param>
    /// <returns>Returns an ECDSA public/private key pair.</returns>
    public static KeyPair CreateRsaKeyPair(HashAlgorithmType type, RSASignaturePadding padding)
    {
        using RSA asymmetricAlgorithm = RSA.Create();

        PrivateKey privateKey = new RsaPrivateKey(asymmetricAlgorithm.ExportRSAPrivateKey(), type, padding);
        PublicKey publicKey = new RsaPublicKey(asymmetricAlgorithm.ExportRSAPublicKey(), type, padding);

        return new KeyPair(privateKey, publicKey, type);
    }

    /// <summary>
    /// Creates an RSA public/private key pair.
    /// </summary>
    /// <param name="type">The <see cref="HashAlgorithmType"/> for computing signature data.</param>
    /// <param name="padding">The <see cref="RSASignaturePadding" /> for computing signature data.</param>
    /// <param name="keySizeInBits">The key size, in bits.</param>
    /// <returns>Returns an ECDSA public/private key pair.</returns>
    public static KeyPair CreateRsaKeyPair(HashAlgorithmType type, RSASignaturePadding padding, int keySizeInBits)
    {
        using RSA asymmetricAlgorithm = RSA.Create(keySizeInBits);

        PrivateKey privateKey = new RsaPrivateKey(asymmetricAlgorithm.ExportRSAPrivateKey(), type, padding);
        PublicKey publicKey = new RsaPublicKey(asymmetricAlgorithm.ExportRSAPublicKey(), type, padding);

        return new KeyPair(privateKey, publicKey, type);
    }

    /// <summary>
    /// Creates an RSA public/private key pair.
    /// </summary>
    /// <param name="type">The <see cref="HashAlgorithmType"/> for computing signature data.</param>
    /// <param name="padding">The <see cref="RSASignaturePadding" /> for computing signature data.</param>
    /// <param name="parameters">The parameters for the RSA algorithm.</param>
    /// <returns>Returns an ECDSA public/private key pair.</returns>
    public static KeyPair CreateRsaKeyPair(
        HashAlgorithmType type,
        RSASignaturePadding padding,
        RSAParameters parameters)
    {
        using RSA asymmetricAlgorithm = RSA.Create(parameters);

        PrivateKey privateKey = new RsaPrivateKey(asymmetricAlgorithm.ExportRSAPrivateKey(), type, padding);
        PublicKey publicKey = new RsaPublicKey(asymmetricAlgorithm.ExportRSAPublicKey(), type, padding);

        return new KeyPair(privateKey, publicKey, type);
    }
}
