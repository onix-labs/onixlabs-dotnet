// Copyright 2020-2021 ONIXLabs
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

namespace OnixLabs.Security.Cryptography
{
    /// <summary>
    /// Represents a cryptographic public/private key pair.
    /// </summary>
    public sealed class KeyPair : IEquatable<KeyPair>
    {
        /// <summary>
        /// Prevents new instances of the <see cref="KeyPair"/> class from being created.
        /// </summary>
        /// <param name="privateKey">The private key component of this key pair.</param>
        /// <param name="publicKey">The public key component of this key pair.</param>
        private KeyPair(PrivateKey privateKey, PublicKey publicKey)
        {
            PrivateKey = privateKey;
            PublicKey = publicKey;
        }

        /// <summary>
        /// Gets the private key component of this key pair.
        /// </summary>
        public PrivateKey PrivateKey { get; }

        /// <summary>
        /// Gets the public key component of this key pair.
        /// </summary>
        public PublicKey PublicKey { get; }

        /// <summary>
        /// Performs an equality check between two object instances.
        /// </summary>
        /// <param name="a">Instance a.</param>
        /// <param name="b">Instance b.</param>
        /// <returns>True if the instances are equal; otherwise, false.</returns>
        public static bool operator ==(KeyPair a, KeyPair b)
        {
            return Equals(a, b);
        }

        /// <summary>
        /// Performs an inequality check between two object instances.
        /// </summary>
        /// <param name="a">Instance a.</param>
        /// <param name="b">Instance b.</param>
        /// <returns>True if the instances are not equal; otherwise, false.</returns>
        public static bool operator !=(KeyPair a, KeyPair b)
        {
            return !Equals(a, b);
        }

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

            return new KeyPair(privateKey, publicKey);
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

            return new KeyPair(privateKey, publicKey);
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

            return new KeyPair(privateKey, publicKey);
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

            return new KeyPair(privateKey, publicKey);
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

            return new KeyPair(privateKey, publicKey);
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

            return new KeyPair(privateKey, publicKey);
        }

        /// <summary>
        /// Checks for equality between this instance and another object.
        /// </summary>
        /// <param name="other">The object to check for equality.</param>
        /// <returns>true if the object is equal to this instance; otherwise, false.</returns>
        public bool Equals(KeyPair? other)
        {
            return ReferenceEquals(this, other)
                   || other is not null
                   && other.PrivateKey == PrivateKey
                   && other.PublicKey == PublicKey;
        }

        /// <summary>
        /// Checks for equality between this instance and another object.
        /// </summary>
        /// <param name="obj">The object to check for equality.</param>
        /// <returns>true if the object is equal to this instance; otherwise, false.</returns>
        public override bool Equals(object? obj)
        {
            return Equals(obj as KeyPair);
        }

        /// <summary>
        /// Serves as a hash code function for this instance.
        /// </summary>
        /// <returns>A hash code for this instance.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(PrivateKey, PublicKey);
        }

        /// <summary>
        /// Returns a <see cref="string"/> that represents the current object.
        /// </summary>
        /// <returns>A <see cref="string"/> that represents the current object.</returns>
        public override string ToString()
        {
            return $"{PrivateKey}:{PublicKey}";
        }
    }
}
