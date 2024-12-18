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
using System.IO;
using System.Security.Cryptography;
using OnixLabs.Core;

namespace OnixLabs.Security.Cryptography;

/// <summary>
/// Defines an RSA cryptographic public key.
/// </summary>
public interface IRsaPublicKey : IPublicKeyImportable<RsaPublicKey>, IPublicKeyExportable, IBinaryConvertible
{
    /// <summary>
    /// Determines whether the specified data was signed by the RSA cryptographic private key that corresponds to the current
    /// RSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current RSA cryptographic public key.</param>
    /// <param name="data">The unsigned data to validate against the current RSA cryptographic public key.</param>
    /// <param name="algorithm">The hash algorithm that was used to hash and sign the input data.</param>
    /// <param name="padding">The RSA signature padding mode that was used to generate the cryptographic digital signature.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the specified data was signed by the RSA cryptographic private key that corresponds to the
    /// current RSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature; otherwise, <see langword="false"/>.
    /// </returns>
    bool IsDataValid(ReadOnlySpan<byte> signature, ReadOnlySpan<byte> data, HashAlgorithmName algorithm, RSASignaturePadding padding);

    /// <summary>
    /// Determines whether the specified data was signed by the RSA cryptographic private key that corresponds to the current
    /// RSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current RSA cryptographic public key.</param>
    /// <param name="data">The unsigned data to validate against the current RSA cryptographic public key.</param>
    /// <param name="offset">The offset into the byte array from which to begin using data.</param>
    /// <param name="count">The number of bytes in the array to use as data.</param>
    /// <param name="algorithm">The hash algorithm that was used to hash and sign the input data.</param>
    /// <param name="padding">The RSA signature padding mode that was used to generate the cryptographic digital signature.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the specified data was signed by the RSA cryptographic private key that corresponds to the
    /// current RSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature; otherwise, <see langword="false"/>.
    /// </returns>
    bool IsDataValid(ReadOnlySpan<byte> signature, ReadOnlySpan<byte> data, int offset, int count, HashAlgorithmName algorithm, RSASignaturePadding padding);

    /// <summary>
    /// Determines whether the specified data was signed by the RSA cryptographic private key that corresponds to the current
    /// RSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current RSA cryptographic public key.</param>
    /// <param name="data">The unsigned data to validate against the current RSA cryptographic public key.</param>
    /// <param name="algorithm">The hash algorithm that was used to hash and sign the input data.</param>
    /// <param name="padding">The RSA signature padding mode that was used to generate the cryptographic digital signature.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the specified data was signed by the RSA cryptographic private key that corresponds to the
    /// current RSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature; otherwise, <see langword="false"/>.
    /// </returns>
    bool IsDataValid(ReadOnlySpan<byte> signature, Stream data, HashAlgorithmName algorithm, RSASignaturePadding padding);

    /// <summary>
    /// Determines whether the specified data was signed by the RSA cryptographic private key that corresponds to the current
    /// RSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current RSA cryptographic public key.</param>
    /// <param name="data">The unsigned data to validate against the current RSA cryptographic public key.</param>
    /// <param name="algorithm">The hash algorithm that was used to hash and sign the input data.</param>
    /// <param name="padding">The RSA signature padding mode that was used to generate the cryptographic digital signature.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the specified data was signed by the RSA cryptographic private key that corresponds to the
    /// current RSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature; otherwise, <see langword="false"/>.
    /// </returns>
    bool IsDataValid(ReadOnlySpan<byte> signature, IBinaryConvertible data, HashAlgorithmName algorithm, RSASignaturePadding padding);

    /// <summary>
    /// Determines whether the specified data was signed by the RSA cryptographic private key that corresponds to the current
    /// RSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current RSA cryptographic public key.</param>
    /// <param name="data">The unsigned data to validate against the current RSA cryptographic public key.</param>
    /// <param name="algorithm">The hash algorithm that was used to hash and sign the input data.</param>
    /// <param name="padding">The RSA signature padding mode that was used to generate the cryptographic digital signature.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the specified data was signed by the RSA cryptographic private key that corresponds to the
    /// current RSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature; otherwise, <see langword="false"/>.
    /// </returns>
    bool IsDataValid(DigitalSignature signature, ReadOnlySpan<byte> data, HashAlgorithmName algorithm, RSASignaturePadding padding);

    /// <summary>
    /// Determines whether the specified data was signed by the RSA cryptographic private key that corresponds to the current
    /// RSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current RSA cryptographic public key.</param>
    /// <param name="data">The unsigned data to validate against the current RSA cryptographic public key.</param>
    /// <param name="offset">The offset into the byte array from which to begin using data.</param>
    /// <param name="count">The number of bytes in the array to use as data.</param>
    /// <param name="algorithm">The hash algorithm that was used to hash and sign the input data.</param>
    /// <param name="padding">The RSA signature padding mode that was used to generate the cryptographic digital signature.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the specified data was signed by the RSA cryptographic private key that corresponds to the
    /// current RSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature; otherwise, <see langword="false"/>.
    /// </returns>
    bool IsDataValid(DigitalSignature signature, ReadOnlySpan<byte> data, int offset, int count, HashAlgorithmName algorithm, RSASignaturePadding padding);

    /// <summary>
    /// Determines whether the specified data was signed by the RSA cryptographic private key that corresponds to the current
    /// RSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current RSA cryptographic public key.</param>
    /// <param name="data">The unsigned data to validate against the current RSA cryptographic public key.</param>
    /// <param name="algorithm">The hash algorithm that was used to hash and sign the input data.</param>
    /// <param name="padding">The RSA signature padding mode that was used to generate the cryptographic digital signature.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the specified data was signed by the RSA cryptographic private key that corresponds to the
    /// current RSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature; otherwise, <see langword="false"/>.
    /// </returns>
    bool IsDataValid(DigitalSignature signature, Stream data, HashAlgorithmName algorithm, RSASignaturePadding padding);

    /// <summary>
    /// Determines whether the specified data was signed by the RSA cryptographic private key that corresponds to the current
    /// RSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current RSA cryptographic public key.</param>
    /// <param name="data">The unsigned data to validate against the current RSA cryptographic public key.</param>
    /// <param name="algorithm">The hash algorithm that was used to hash and sign the input data.</param>
    /// <param name="padding">The RSA signature padding mode that was used to generate the cryptographic digital signature.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the specified data was signed by the RSA cryptographic private key that corresponds to the
    /// current RSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature; otherwise, <see langword="false"/>.
    /// </returns>
    bool IsDataValid(DigitalSignature signature, IBinaryConvertible data, HashAlgorithmName algorithm, RSASignaturePadding padding);

    /// <summary>
    /// Determines whether the specified hash was signed by the RSA cryptographic private key that corresponds to the current
    /// RSA cryptographic public key, and that the signed hash matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current RSA cryptographic public key.</param>
    /// <param name="hash">The unsigned hash to validate against the current RSA cryptographic public key.</param>
    /// <param name="algorithm">The hash algorithm that was used to hash and sign the input hash.</param>
    /// <param name="padding">The RSA signature padding mode that was used to generate the cryptographic digital signature.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the specified hash was signed by the RSA cryptographic private key that corresponds to the
    /// current RSA cryptographic public key, and that the signed hash matches the specified cryptographic digital signature; otherwise, <see langword="false"/>.
    /// </returns>
    bool IsHashValid(ReadOnlySpan<byte> signature, ReadOnlySpan<byte> hash, HashAlgorithmName algorithm, RSASignaturePadding padding);

    /// <summary>
    /// Determines whether the specified hash was signed by the RSA cryptographic private key that corresponds to the current
    /// RSA cryptographic public key, and that the signed hash matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current RSA cryptographic public key.</param>
    /// <param name="hash">The unsigned hash to validate against the current RSA cryptographic public key.</param>
    /// <param name="algorithm">The hash algorithm that was used to hash and sign the input hash.</param>
    /// <param name="padding">The RSA signature padding mode that was used to generate the cryptographic digital signature.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the specified hash was signed by the RSA cryptographic private key that corresponds to the
    /// current RSA cryptographic public key, and that the signed hash matches the specified cryptographic digital signature; otherwise, <see langword="false"/>.
    /// </returns>
    bool IsHashValid(DigitalSignature signature, ReadOnlySpan<byte> hash, HashAlgorithmName algorithm, RSASignaturePadding padding);

    /// <summary>
    /// Determines whether the specified hash was signed by the RSA cryptographic private key that corresponds to the current
    /// RSA cryptographic public key, and that the signed hash matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current RSA cryptographic public key.</param>
    /// <param name="hash">The unsigned hash to validate against the current RSA cryptographic public key.</param>
    /// <param name="algorithm">The hash algorithm that was used to hash and sign the input hash.</param>
    /// <param name="padding">The RSA signature padding mode that was used to generate the cryptographic digital signature.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the specified hash was signed by the RSA cryptographic private key that corresponds to the
    /// current RSA cryptographic public key, and that the signed hash matches the specified cryptographic digital signature; otherwise, <see langword="false"/>.
    /// </returns>
    bool IsHashValid(ReadOnlySpan<byte> signature, Hash hash, HashAlgorithmName algorithm, RSASignaturePadding padding);

    /// <summary>
    /// Determines whether the specified hash was signed by the RSA cryptographic private key that corresponds to the current
    /// RSA cryptographic public key, and that the signed hash matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current RSA cryptographic public key.</param>
    /// <param name="hash">The unsigned hash to validate against the current RSA cryptographic public key.</param>
    /// <param name="algorithm">The hash algorithm that was used to hash and sign the input hash.</param>
    /// <param name="padding">The RSA signature padding mode that was used to generate the cryptographic digital signature.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the specified hash was signed by the RSA cryptographic private key that corresponds to the
    /// current RSA cryptographic public key, and that the signed hash matches the specified cryptographic digital signature; otherwise, <see langword="false"/>.
    /// </returns>
    bool IsHashValid(DigitalSignature signature, Hash hash, HashAlgorithmName algorithm, RSASignaturePadding padding);

    /// <summary>
    /// Verifies that the specified data was signed by the RSA cryptographic private key that corresponds to the current
    /// RSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current RSA cryptographic public key.</param>
    /// <param name="data">The unsigned data to validate against the current RSA cryptographic public key.</param>
    /// <param name="algorithm">The hash algorithm that was used to hash and sign the input data.</param>
    /// <param name="padding">The RSA signature padding mode that was used to generate the cryptographic digital signature.</param>
    void VerifyData(ReadOnlySpan<byte> signature, ReadOnlySpan<byte> data, HashAlgorithmName algorithm, RSASignaturePadding padding);

    /// <summary>
    /// Verifies that the specified data was signed by the RSA cryptographic private key that corresponds to the current
    /// RSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current RSA cryptographic public key.</param>
    /// <param name="data">The unsigned data to validate against the current RSA cryptographic public key.</param>
    /// <param name="offset">The offset into the byte array from which to begin using data.</param>
    /// <param name="count">The number of bytes in the array to use as data.</param>
    /// <param name="algorithm">The hash algorithm that was used to hash and sign the input data.</param>
    /// <param name="padding">The RSA signature padding mode that was used to generate the cryptographic digital signature.</param>
    void VerifyData(ReadOnlySpan<byte> signature, ReadOnlySpan<byte> data, int offset, int count, HashAlgorithmName algorithm, RSASignaturePadding padding);

    /// <summary>
    /// Verifies that the specified data was signed by the RSA cryptographic private key that corresponds to the current
    /// RSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current RSA cryptographic public key.</param>
    /// <param name="data">The unsigned data to validate against the current RSA cryptographic public key.</param>
    /// <param name="algorithm">The hash algorithm that was used to hash and sign the input data.</param>
    /// <param name="padding">The RSA signature padding mode that was used to generate the cryptographic digital signature.</param>
    void VerifyData(ReadOnlySpan<byte> signature, Stream data, HashAlgorithmName algorithm, RSASignaturePadding padding);

    /// <summary>
    /// Verifies that the specified data was signed by the RSA cryptographic private key that corresponds to the current
    /// RSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current RSA cryptographic public key.</param>
    /// <param name="data">The unsigned data to validate against the current RSA cryptographic public key.</param>
    /// <param name="algorithm">The hash algorithm that was used to hash and sign the input data.</param>
    /// <param name="padding">The RSA signature padding mode that was used to generate the cryptographic digital signature.</param>
    void VerifyData(ReadOnlySpan<byte> signature, IBinaryConvertible data, HashAlgorithmName algorithm, RSASignaturePadding padding);

    /// <summary>
    /// Verifies that the specified data was signed by the RSA cryptographic private key that corresponds to the current
    /// RSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current RSA cryptographic public key.</param>
    /// <param name="data">The unsigned data to validate against the current RSA cryptographic public key.</param>
    /// <param name="algorithm">The hash algorithm that was used to hash and sign the input data.</param>
    /// <param name="padding">The RSA signature padding mode that was used to generate the cryptographic digital signature.</param>
    void VerifyData(DigitalSignature signature, ReadOnlySpan<byte> data, HashAlgorithmName algorithm, RSASignaturePadding padding);

    /// <summary>
    /// Verifies that the specified data was signed by the RSA cryptographic private key that corresponds to the current
    /// RSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current RSA cryptographic public key.</param>
    /// <param name="data">The unsigned data to validate against the current RSA cryptographic public key.</param>
    /// <param name="offset">The offset into the byte array from which to begin using data.</param>
    /// <param name="count">The number of bytes in the array to use as data.</param>
    /// <param name="algorithm">The hash algorithm that was used to hash and sign the input data.</param>
    /// <param name="padding">The RSA signature padding mode that was used to generate the cryptographic digital signature.</param>
    void VerifyData(DigitalSignature signature, ReadOnlySpan<byte> data, int offset, int count, HashAlgorithmName algorithm, RSASignaturePadding padding);

    /// <summary>
    /// Verifies that the specified data was signed by the RSA cryptographic private key that corresponds to the current
    /// RSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current RSA cryptographic public key.</param>
    /// <param name="data">The unsigned data to validate against the current RSA cryptographic public key.</param>
    /// <param name="algorithm">The hash algorithm that was used to hash and sign the input data.</param>
    /// <param name="padding">The RSA signature padding mode that was used to generate the cryptographic digital signature.</param>
    void VerifyData(DigitalSignature signature, Stream data, HashAlgorithmName algorithm, RSASignaturePadding padding);

    /// <summary>
    /// Verifies that the specified data was signed by the RSA cryptographic private key that corresponds to the current
    /// RSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current RSA cryptographic public key.</param>
    /// <param name="data">The unsigned data to validate against the current RSA cryptographic public key.</param>
    /// <param name="algorithm">The hash algorithm that was used to hash and sign the input data.</param>
    /// <param name="padding">The RSA signature padding mode that was used to generate the cryptographic digital signature.</param>
    void VerifyData(DigitalSignature signature, IBinaryConvertible data, HashAlgorithmName algorithm, RSASignaturePadding padding);

    /// <summary>
    /// Verifies that the specified hash was signed by the RSA cryptographic private key that corresponds to the current
    /// RSA cryptographic public key, and that the signed hash matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current RSA cryptographic public key.</param>
    /// <param name="hash">The unsigned hash to validate against the current RSA cryptographic public key.</param>
    /// <param name="algorithm">The hash algorithm that was used to hash and sign the input hash.</param>
    /// <param name="padding">The RSA signature padding mode that was used to generate the cryptographic digital signature.</param>
    void VerifyHash(ReadOnlySpan<byte> signature, ReadOnlySpan<byte> hash, HashAlgorithmName algorithm, RSASignaturePadding padding);

    /// <summary>
    /// Verifies that the specified hash was signed by the RSA cryptographic private key that corresponds to the current
    /// RSA cryptographic public key, and that the signed hash matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current RSA cryptographic public key.</param>
    /// <param name="hash">The unsigned hash to validate against the current RSA cryptographic public key.</param>
    /// <param name="algorithm">The hash algorithm that was used to hash and sign the input hash.</param>
    /// <param name="padding">The RSA signature padding mode that was used to generate the cryptographic digital signature.</param>
    void VerifyHash(DigitalSignature signature, ReadOnlySpan<byte> hash, HashAlgorithmName algorithm, RSASignaturePadding padding);

    /// <summary>
    /// Verifies that the specified hash was signed by the RSA cryptographic private key that corresponds to the current
    /// RSA cryptographic public key, and that the signed hash matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current RSA cryptographic public key.</param>
    /// <param name="hash">The unsigned hash to validate against the current RSA cryptographic public key.</param>
    /// <param name="algorithm">The hash algorithm that was used to hash and sign the input hash.</param>
    /// <param name="padding">The RSA signature padding mode that was used to generate the cryptographic digital signature.</param>
    void VerifyHash(ReadOnlySpan<byte> signature, Hash hash, HashAlgorithmName algorithm, RSASignaturePadding padding);

    /// <summary>
    /// Verifies that the specified hash was signed by the RSA cryptographic private key that corresponds to the current
    /// RSA cryptographic public key, and that the signed hash matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current RSA cryptographic public key.</param>
    /// <param name="hash">The unsigned hash to validate against the current RSA cryptographic public key.</param>
    /// <param name="algorithm">The hash algorithm that was used to hash and sign the input hash.</param>
    /// <param name="padding">The RSA signature padding mode that was used to generate the cryptographic digital signature.</param>
    void VerifyHash(DigitalSignature signature, Hash hash, HashAlgorithmName algorithm, RSASignaturePadding padding);
}
