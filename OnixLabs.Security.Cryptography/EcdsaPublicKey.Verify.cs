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

public sealed partial class EcdsaPublicKey
{
    /// <summary>
    /// Determines whether the specified data was signed by the ECDSA cryptographic private key that corresponds to the current
    /// ECDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="data">The unsigned data to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="algorithm">The hash algorithm that was used to hash and sign the input data.</param>
    /// <param name="format">The digital signature format that was used to generate the cryptographic digital signature.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the specified data was signed by the ECDSA cryptographic private key that corresponds to the
    /// current ECDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature; otherwise, <see langword="false"/>.
    /// </returns>
    public bool IsDataValid(ReadOnlySpan<byte> signature, ReadOnlySpan<byte> data, HashAlgorithm algorithm, DSASignatureFormat format = default)
    {
        byte[] hash = algorithm.ComputeHash(data.ToArray());
        return IsHashValid(signature, hash, format);
    }

    /// <summary>
    /// Determines whether the specified data was signed by the ECDSA cryptographic private key that corresponds to the current
    /// ECDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="data">The unsigned data to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="offset">The offset into the byte array from which to begin using data.</param>
    /// <param name="count">The number of bytes in the array to use as data.</param>
    /// <param name="algorithm">The hash algorithm that was used to hash and sign the input data.</param>
    /// <param name="format">The digital signature format that was used to generate the cryptographic digital signature.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the specified data was signed by the ECDSA cryptographic private key that corresponds to the
    /// current ECDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature; otherwise, <see langword="false"/>.
    /// </returns>
    public bool IsDataValid(ReadOnlySpan<byte> signature, ReadOnlySpan<byte> data, int offset, int count, HashAlgorithm algorithm, DSASignatureFormat format = default)
    {
        byte[] hash = algorithm.ComputeHash(data.ToArray(), offset, count);
        return IsHashValid(signature, hash, format);
    }

    /// <summary>
    /// Determines whether the specified data was signed by the ECDSA cryptographic private key that corresponds to the current
    /// ECDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="data">The unsigned data to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="algorithm">The hash algorithm that was used to hash and sign the input data.</param>
    /// <param name="format">The digital signature format that was used to generate the cryptographic digital signature.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the specified data was signed by the ECDSA cryptographic private key that corresponds to the
    /// current ECDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature; otherwise, <see langword="false"/>.
    /// </returns>
    public bool IsDataValid(ReadOnlySpan<byte> signature, Stream data, HashAlgorithm algorithm, DSASignatureFormat format = default)
    {
        byte[] hash = algorithm.ComputeHash(data);
        return IsHashValid(signature, hash, format);
    }

    /// <summary>
    /// Determines whether the specified data was signed by the ECDSA cryptographic private key that corresponds to the current
    /// ECDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="data">The unsigned data to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="algorithm">The hash algorithm that was used to hash and sign the input data.</param>
    /// <param name="format">The digital signature format that was used to generate the cryptographic digital signature.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the specified data was signed by the ECDSA cryptographic private key that corresponds to the
    /// current ECDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature; otherwise, <see langword="false"/>.
    /// </returns>
    public bool IsDataValid(ReadOnlySpan<byte> signature, IBinaryConvertible data, HashAlgorithm algorithm, DSASignatureFormat format = default)
    {
        byte[] hash = algorithm.ComputeHash(data.ToByteArray());
        return IsHashValid(signature, hash, format);
    }

    /// <summary>
    /// Determines whether the specified data was signed by the ECDSA cryptographic private key that corresponds to the current
    /// ECDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="data">The unsigned data to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="algorithm">The hash algorithm that was used to hash and sign the input data.</param>
    /// <param name="format">The digital signature format that was used to generate the cryptographic digital signature.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the specified data was signed by the ECDSA cryptographic private key that corresponds to the
    /// current ECDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature; otherwise, <see langword="false"/>.
    /// </returns>
    public bool IsDataValid(ReadOnlySpan<byte> signature, ReadOnlySpan<byte> data, HashAlgorithmName algorithm, DSASignatureFormat format = default)
    {
        using ECDsa key = ImportKeyData();
        return key.VerifyData(data, signature, algorithm, format);
    }

    /// <summary>
    /// Determines whether the specified data was signed by the ECDSA cryptographic private key that corresponds to the current
    /// ECDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="data">The unsigned data to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="offset">The offset into the byte array from which to begin using data.</param>
    /// <param name="count">The number of bytes in the array to use as data.</param>
    /// <param name="algorithm">The hash algorithm that was used to hash and sign the input data.</param>
    /// <param name="format">The digital signature format that was used to generate the cryptographic digital signature.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the specified data was signed by the ECDSA cryptographic private key that corresponds to the
    /// current ECDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature; otherwise, <see langword="false"/>.
    /// </returns>
    public bool IsDataValid(ReadOnlySpan<byte> signature, ReadOnlySpan<byte> data, int offset, int count, HashAlgorithmName algorithm, DSASignatureFormat format = default)
    {
        using ECDsa key = ImportKeyData();
        return key.VerifyData(data.ToArray(), offset, count, signature.ToArray(), algorithm, format);
    }

    /// <summary>
    /// Determines whether the specified data was signed by the ECDSA cryptographic private key that corresponds to the current
    /// ECDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="data">The unsigned data to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="algorithm">The hash algorithm that was used to hash and sign the input data.</param>
    /// <param name="format">The digital signature format that was used to generate the cryptographic digital signature.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the specified data was signed by the ECDSA cryptographic private key that corresponds to the
    /// current ECDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature; otherwise, <see langword="false"/>.
    /// </returns>
    public bool IsDataValid(ReadOnlySpan<byte> signature, Stream data, HashAlgorithmName algorithm, DSASignatureFormat format = default)
    {
        using ECDsa key = ImportKeyData();
        return key.VerifyData(data, signature.ToArray(), algorithm, format);
    }

    /// <summary>
    /// Determines whether the specified data was signed by the ECDSA cryptographic private key that corresponds to the current
    /// ECDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="data">The unsigned data to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="algorithm">The hash algorithm that was used to hash and sign the input data.</param>
    /// <param name="format">The digital signature format that was used to generate the cryptographic digital signature.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the specified data was signed by the ECDSA cryptographic private key that corresponds to the
    /// current ECDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature; otherwise, <see langword="false"/>.
    /// </returns>
    public bool IsDataValid(ReadOnlySpan<byte> signature, IBinaryConvertible data, HashAlgorithmName algorithm, DSASignatureFormat format = default)
    {
        using ECDsa key = ImportKeyData();
        return key.VerifyData(data.ToByteArray(), signature, algorithm, format);
    }

    /// <summary>
    /// Determines whether the specified data was signed by the ECDSA cryptographic private key that corresponds to the current
    /// ECDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="data">The unsigned data to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="algorithm">The hash algorithm that was used to hash and sign the input data.</param>
    /// <param name="format">The digital signature format that was used to generate the cryptographic digital signature.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the specified data was signed by the ECDSA cryptographic private key that corresponds to the
    /// current ECDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature; otherwise, <see langword="false"/>.
    /// </returns>
    public bool IsDataValid(DigitalSignature signature, ReadOnlySpan<byte> data, HashAlgorithm algorithm, DSASignatureFormat format = default)
    {
        byte[] hash = algorithm.ComputeHash(data.ToArray());
        return IsHashValid(signature, hash, format);
    }

    /// <summary>
    /// Determines whether the specified data was signed by the ECDSA cryptographic private key that corresponds to the current
    /// ECDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="data">The unsigned data to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="offset">The offset into the byte array from which to begin using data.</param>
    /// <param name="count">The number of bytes in the array to use as data.</param>
    /// <param name="algorithm">The hash algorithm that was used to hash and sign the input data.</param>
    /// <param name="format">The digital signature format that was used to generate the cryptographic digital signature.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the specified data was signed by the ECDSA cryptographic private key that corresponds to the
    /// current ECDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature; otherwise, <see langword="false"/>.
    /// </returns>
    public bool IsDataValid(DigitalSignature signature, ReadOnlySpan<byte> data, int offset, int count, HashAlgorithm algorithm, DSASignatureFormat format = default)
    {
        byte[] hash = algorithm.ComputeHash(data.ToArray(), offset, count);
        return IsHashValid(signature, hash, format);
    }

    /// <summary>
    /// Determines whether the specified data was signed by the ECDSA cryptographic private key that corresponds to the current
    /// ECDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="data">The unsigned data to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="algorithm">The hash algorithm that was used to hash and sign the input data.</param>
    /// <param name="format">The digital signature format that was used to generate the cryptographic digital signature.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the specified data was signed by the ECDSA cryptographic private key that corresponds to the
    /// current ECDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature; otherwise, <see langword="false"/>.
    /// </returns>
    public bool IsDataValid(DigitalSignature signature, Stream data, HashAlgorithm algorithm, DSASignatureFormat format = default)
    {
        byte[] hash = algorithm.ComputeHash(data);
        return IsHashValid(signature, hash, format);
    }

    /// <summary>
    /// Determines whether the specified data was signed by the ECDSA cryptographic private key that corresponds to the current
    /// ECDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="data">The unsigned data to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="algorithm">The hash algorithm that was used to hash and sign the input data.</param>
    /// <param name="format">The digital signature format that was used to generate the cryptographic digital signature.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the specified data was signed by the ECDSA cryptographic private key that corresponds to the
    /// current ECDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature; otherwise, <see langword="false"/>.
    /// </returns>
    public bool IsDataValid(DigitalSignature signature, IBinaryConvertible data, HashAlgorithm algorithm, DSASignatureFormat format = default)
    {
        byte[] hash = algorithm.ComputeHash(data.ToByteArray());
        return IsHashValid(signature, hash, format);
    }

    /// <summary>
    /// Determines whether the specified data was signed by the ECDSA cryptographic private key that corresponds to the current
    /// ECDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="data">The unsigned data to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="algorithm">The hash algorithm that was used to hash and sign the input data.</param>
    /// <param name="format">The digital signature format that was used to generate the cryptographic digital signature.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the specified data was signed by the ECDSA cryptographic private key that corresponds to the
    /// current ECDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature; otherwise, <see langword="false"/>.
    /// </returns>
    public bool IsDataValid(DigitalSignature signature, ReadOnlySpan<byte> data, HashAlgorithmName algorithm, DSASignatureFormat format = default)
    {
        using ECDsa key = ImportKeyData();
        return key.VerifyData(data, signature.ToByteArray(), algorithm, format);
    }

    /// <summary>
    /// Determines whether the specified data was signed by the ECDSA cryptographic private key that corresponds to the current
    /// ECDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="data">The unsigned data to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="offset">The offset into the byte array from which to begin using data.</param>
    /// <param name="count">The number of bytes in the array to use as data.</param>
    /// <param name="algorithm">The hash algorithm that was used to hash and sign the input data.</param>
    /// <param name="format">The digital signature format that was used to generate the cryptographic digital signature.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the specified data was signed by the ECDSA cryptographic private key that corresponds to the
    /// current ECDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature; otherwise, <see langword="false"/>.
    /// </returns>
    public bool IsDataValid(DigitalSignature signature, ReadOnlySpan<byte> data, int offset, int count, HashAlgorithmName algorithm, DSASignatureFormat format = default)
    {
        using ECDsa key = ImportKeyData();
        return key.VerifyData(data.ToArray(), offset, count, signature.ToByteArray(), algorithm, format);
    }

    /// <summary>
    /// Determines whether the specified data was signed by the ECDSA cryptographic private key that corresponds to the current
    /// ECDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="data">The unsigned data to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="algorithm">The hash algorithm that was used to hash and sign the input data.</param>
    /// <param name="format">The digital signature format that was used to generate the cryptographic digital signature.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the specified data was signed by the ECDSA cryptographic private key that corresponds to the
    /// current ECDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature; otherwise, <see langword="false"/>.
    /// </returns>
    public bool IsDataValid(DigitalSignature signature, Stream data, HashAlgorithmName algorithm, DSASignatureFormat format = default)
    {
        using ECDsa key = ImportKeyData();
        return key.VerifyData(data, signature.ToByteArray(), algorithm, format);
    }

    /// <summary>
    /// Determines whether the specified data was signed by the ECDSA cryptographic private key that corresponds to the current
    /// ECDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="data">The unsigned data to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="algorithm">The hash algorithm that was used to hash and sign the input data.</param>
    /// <param name="format">The digital signature format that was used to generate the cryptographic digital signature.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the specified data was signed by the ECDSA cryptographic private key that corresponds to the
    /// current ECDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature; otherwise, <see langword="false"/>.
    /// </returns>
    public bool IsDataValid(DigitalSignature signature, IBinaryConvertible data, HashAlgorithmName algorithm, DSASignatureFormat format = default)
    {
        using ECDsa key = ImportKeyData();
        return key.VerifyData(data.ToByteArray(), signature.ToByteArray(), algorithm, format);
    }

    /// <summary>
    /// Determines whether the specified hash was signed by the ECDSA cryptographic private key that corresponds to the current
    /// ECDSA cryptographic public key, and that the signed hash matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="hash">The unsigned hash to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="format">The digital signature format that was used to generate the cryptographic digital signature.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the specified hash was signed by the ECDSA cryptographic private key that corresponds to the
    /// current ECDSA cryptographic public key, and that the signed hash matches the specified cryptographic digital signature; otherwise, <see langword="false"/>.
    /// </returns>
    public bool IsHashValid(ReadOnlySpan<byte> signature, Hash hash, DSASignatureFormat format = default)
    {
        using ECDsa key = ImportKeyData();
        return key.VerifyHash(hash.ToByteArray(), signature, format);
    }

    /// <summary>
    /// Determines whether the specified hash was signed by the ECDSA cryptographic private key that corresponds to the current
    /// ECDSA cryptographic public key, and that the signed hash matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="hash">The unsigned hash to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="format">The digital signature format that was used to generate the cryptographic digital signature.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the specified hash was signed by the ECDSA cryptographic private key that corresponds to the
    /// current ECDSA cryptographic public key, and that the signed hash matches the specified cryptographic digital signature; otherwise, <see langword="false"/>.
    /// </returns>
    public bool IsHashValid(ReadOnlySpan<byte> signature, ReadOnlySpan<byte> hash, DSASignatureFormat format = default)
    {
        using ECDsa key = ImportKeyData();
        return key.VerifyHash(hash, signature, format);
    }

    /// <summary>
    /// Determines whether the specified hash was signed by the ECDSA cryptographic private key that corresponds to the current
    /// ECDSA cryptographic public key, and that the signed hash matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="hash">The unsigned hash to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="format">The digital signature format that was used to generate the cryptographic digital signature.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the specified hash was signed by the ECDSA cryptographic private key that corresponds to the
    /// current ECDSA cryptographic public key, and that the signed hash matches the specified cryptographic digital signature; otherwise, <see langword="false"/>.
    /// </returns>
    public bool IsHashValid(DigitalSignature signature, Hash hash, DSASignatureFormat format = default)
    {
        using ECDsa key = ImportKeyData();
        return key.VerifyHash(hash.ToByteArray(), signature.ToByteArray(), format);
    }

    /// <summary>
    /// Determines whether the specified hash was signed by the ECDSA cryptographic private key that corresponds to the current
    /// ECDSA cryptographic public key, and that the signed hash matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="hash">The unsigned hash to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="format">The digital signature format that was used to generate the cryptographic digital signature.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the specified hash was signed by the ECDSA cryptographic private key that corresponds to the
    /// current ECDSA cryptographic public key, and that the signed hash matches the specified cryptographic digital signature; otherwise, <see langword="false"/>.
    /// </returns>
    public bool IsHashValid(DigitalSignature signature, ReadOnlySpan<byte> hash, DSASignatureFormat format = default)
    {
        using ECDsa key = ImportKeyData();
        return key.VerifyHash(hash, signature.ToByteArray(), format);
    }

    /// <summary>
    /// Verifies that the specified data was signed by the ECDSA cryptographic private key that corresponds to the current
    /// ECDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="data">The unsigned data to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="algorithm">The hash algorithm that was used to hash and sign the input data.</param>
    /// <param name="format">The digital signature format that was used to generate the cryptographic digital signature.</param>
    public void VerifyData(ReadOnlySpan<byte> signature, ReadOnlySpan<byte> data, HashAlgorithm algorithm, DSASignatureFormat format = default)
    {
        CheckSignature(IsDataValid(signature, data, algorithm, format));
    }

    /// <summary>
    /// Verifies that the specified data was signed by the ECDSA cryptographic private key that corresponds to the current
    /// ECDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="data">The unsigned data to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="offset">The offset into the byte array from which to begin using data.</param>
    /// <param name="count">The number of bytes in the array to use as data.</param>
    /// <param name="algorithm">The hash algorithm that was used to hash and sign the input data.</param>
    /// <param name="format">The digital signature format that was used to generate the cryptographic digital signature.</param>
    public void VerifyData(ReadOnlySpan<byte> signature, ReadOnlySpan<byte> data, int offset, int count, HashAlgorithm algorithm, DSASignatureFormat format = default)
    {
        CheckSignature(IsDataValid(signature, data, offset, count, algorithm, format));
    }

    /// <summary>
    /// Verifies that the specified data was signed by the ECDSA cryptographic private key that corresponds to the current
    /// ECDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="data">The unsigned data to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="algorithm">The hash algorithm that was used to hash and sign the input data.</param>
    /// <param name="format">The digital signature format that was used to generate the cryptographic digital signature.</param>
    public void VerifyData(ReadOnlySpan<byte> signature, Stream data, HashAlgorithm algorithm, DSASignatureFormat format = default)
    {
        CheckSignature(IsDataValid(signature, data, algorithm, format));
    }

    /// <summary>
    /// Verifies that the specified data was signed by the ECDSA cryptographic private key that corresponds to the current
    /// ECDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="data">The unsigned data to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="algorithm">The hash algorithm that was used to hash and sign the input data.</param>
    /// <param name="format">The digital signature format that was used to generate the cryptographic digital signature.</param>
    public void VerifyData(ReadOnlySpan<byte> signature, IBinaryConvertible data, HashAlgorithm algorithm, DSASignatureFormat format = default)
    {
        CheckSignature(IsDataValid(signature, data, algorithm, format));
    }

    /// <summary>
    /// Verifies that the specified data was signed by the ECDSA cryptographic private key that corresponds to the current
    /// ECDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="data">The unsigned data to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="algorithm">The hash algorithm that was used to hash and sign the input data.</param>
    /// <param name="format">The digital signature format that was used to generate the cryptographic digital signature.</param>
    public void VerifyData(ReadOnlySpan<byte> signature, ReadOnlySpan<byte> data, HashAlgorithmName algorithm, DSASignatureFormat format = default)
    {
        CheckSignature(IsDataValid(signature, data, algorithm, format));
    }

    /// <summary>
    /// Verifies that the specified data was signed by the ECDSA cryptographic private key that corresponds to the current
    /// ECDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="data">The unsigned data to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="offset">The offset into the byte array from which to begin using data.</param>
    /// <param name="count">The number of bytes in the array to use as data.</param>
    /// <param name="algorithm">The hash algorithm that was used to hash and sign the input data.</param>
    /// <param name="format">The digital signature format that was used to generate the cryptographic digital signature.</param>
    public void VerifyData(ReadOnlySpan<byte> signature, ReadOnlySpan<byte> data, int offset, int count, HashAlgorithmName algorithm, DSASignatureFormat format = default)
    {
        CheckSignature(IsDataValid(signature, data, offset, count, algorithm, format));
    }

    /// <summary>
    /// Verifies that the specified data was signed by the ECDSA cryptographic private key that corresponds to the current
    /// ECDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="data">The unsigned data to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="algorithm">The hash algorithm that was used to hash and sign the input data.</param>
    /// <param name="format">The digital signature format that was used to generate the cryptographic digital signature.</param>
    public void VerifyData(ReadOnlySpan<byte> signature, Stream data, HashAlgorithmName algorithm, DSASignatureFormat format = default)
    {
        CheckSignature(IsDataValid(signature, data, algorithm, format));
    }

    /// <summary>
    /// Verifies that the specified data was signed by the ECDSA cryptographic private key that corresponds to the current
    /// ECDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="data">The unsigned data to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="algorithm">The hash algorithm that was used to hash and sign the input data.</param>
    /// <param name="format">The digital signature format that was used to generate the cryptographic digital signature.</param>
    public void VerifyData(ReadOnlySpan<byte> signature, IBinaryConvertible data, HashAlgorithmName algorithm, DSASignatureFormat format = default)
    {
        CheckSignature(IsDataValid(signature, data, algorithm, format));
    }

    /// <summary>
    /// Verifies that the specified data was signed by the ECDSA cryptographic private key that corresponds to the current
    /// ECDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="data">The unsigned data to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="algorithm">The hash algorithm that was used to hash and sign the input data.</param>
    /// <param name="format">The digital signature format that was used to generate the cryptographic digital signature.</param>
    public void VerifyData(DigitalSignature signature, ReadOnlySpan<byte> data, HashAlgorithm algorithm, DSASignatureFormat format = default)
    {
        CheckSignature(IsDataValid(signature, data, algorithm, format));
    }

    /// <summary>
    /// Verifies that the specified data was signed by the ECDSA cryptographic private key that corresponds to the current
    /// ECDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="data">The unsigned data to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="offset">The offset into the byte array from which to begin using data.</param>
    /// <param name="count">The number of bytes in the array to use as data.</param>
    /// <param name="algorithm">The hash algorithm that was used to hash and sign the input data.</param>
    /// <param name="format">The digital signature format that was used to generate the cryptographic digital signature.</param>
    public void VerifyData(DigitalSignature signature, ReadOnlySpan<byte> data, int offset, int count, HashAlgorithm algorithm, DSASignatureFormat format = default)
    {
        CheckSignature(IsDataValid(signature, data, offset, count, algorithm, format));
    }

    /// <summary>
    /// Verifies that the specified data was signed by the ECDSA cryptographic private key that corresponds to the current
    /// ECDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="data">The unsigned data to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="algorithm">The hash algorithm that was used to hash and sign the input data.</param>
    /// <param name="format">The digital signature format that was used to generate the cryptographic digital signature.</param>
    public void VerifyData(DigitalSignature signature, Stream data, HashAlgorithm algorithm, DSASignatureFormat format = default)
    {
        CheckSignature(IsDataValid(signature, data, algorithm, format));
    }

    /// <summary>
    /// Verifies that the specified data was signed by the ECDSA cryptographic private key that corresponds to the current
    /// ECDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="data">The unsigned data to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="algorithm">The hash algorithm that was used to hash and sign the input data.</param>
    /// <param name="format">The digital signature format that was used to generate the cryptographic digital signature.</param>
    public void VerifyData(DigitalSignature signature, IBinaryConvertible data, HashAlgorithm algorithm, DSASignatureFormat format = default)
    {
        CheckSignature(IsDataValid(signature, data, algorithm, format));
    }

    /// <summary>
    /// Verifies that the specified data was signed by the ECDSA cryptographic private key that corresponds to the current
    /// ECDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="data">The unsigned data to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="algorithm">The hash algorithm that was used to hash and sign the input data.</param>
    /// <param name="format">The digital signature format that was used to generate the cryptographic digital signature.</param>
    public void VerifyData(DigitalSignature signature, ReadOnlySpan<byte> data, HashAlgorithmName algorithm, DSASignatureFormat format = default)
    {
        CheckSignature(IsDataValid(signature, data, algorithm, format));
    }

    /// <summary>
    /// Verifies that the specified data was signed by the ECDSA cryptographic private key that corresponds to the current
    /// ECDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="data">The unsigned data to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="offset">The offset into the byte array from which to begin using data.</param>
    /// <param name="count">The number of bytes in the array to use as data.</param>
    /// <param name="algorithm">The hash algorithm that was used to hash and sign the input data.</param>
    /// <param name="format">The digital signature format that was used to generate the cryptographic digital signature.</param>
    public void VerifyData(DigitalSignature signature, ReadOnlySpan<byte> data, int offset, int count, HashAlgorithmName algorithm, DSASignatureFormat format = default)
    {
        CheckSignature(IsDataValid(signature, data, offset, count, algorithm, format));
    }

    /// <summary>
    /// Verifies that the specified data was signed by the ECDSA cryptographic private key that corresponds to the current
    /// ECDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="data">The unsigned data to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="algorithm">The hash algorithm that was used to hash and sign the input data.</param>
    /// <param name="format">The digital signature format that was used to generate the cryptographic digital signature.</param>
    public void VerifyData(DigitalSignature signature, Stream data, HashAlgorithmName algorithm, DSASignatureFormat format = default)
    {
        CheckSignature(IsDataValid(signature, data, algorithm, format));
    }

    /// <summary>
    /// Verifies that the specified data was signed by the ECDSA cryptographic private key that corresponds to the current
    /// ECDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="data">The unsigned data to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="algorithm">The hash algorithm that was used to hash and sign the input data.</param>
    /// <param name="format">The digital signature format that was used to generate the cryptographic digital signature.</param>
    public void VerifyData(DigitalSignature signature, IBinaryConvertible data, HashAlgorithmName algorithm, DSASignatureFormat format = default)
    {
        CheckSignature(IsDataValid(signature, data, algorithm, format));
    }

    /// <summary>
    /// Verifies that the specified hash was signed by the ECDSA cryptographic private key that corresponds to the current
    /// ECDSA cryptographic public key, and that the signed hash matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="hash">The unsigned hash to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="format">The digital signature format that was used to generate the cryptographic digital signature.</param>
    public void VerifyHash(ReadOnlySpan<byte> signature, Hash hash, DSASignatureFormat format = default)
    {
        CheckSignature(IsHashValid(signature, hash, format));
    }

    /// <summary>
    /// Verifies that the specified hash was signed by the ECDSA cryptographic private key that corresponds to the current
    /// ECDSA cryptographic public key, and that the signed hash matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="hash">The unsigned hash to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="format">The digital signature format that was used to generate the cryptographic digital signature.</param>
    public void VerifyHash(ReadOnlySpan<byte> signature, ReadOnlySpan<byte> hash, DSASignatureFormat format = default)
    {
        CheckSignature(IsHashValid(signature, hash, format));
    }

    /// <summary>
    /// Verifies that the specified hash was signed by the ECDSA cryptographic private key that corresponds to the current
    /// ECDSA cryptographic public key, and that the signed hash matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="hash">The unsigned hash to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="format">The digital signature format that was used to generate the cryptographic digital signature.</param>
    public void VerifyHash(DigitalSignature signature, Hash hash, DSASignatureFormat format = default)
    {
        CheckSignature(IsHashValid(signature, hash, format));
    }

    /// <summary>
    /// Verifies that the specified hash was signed by the ECDSA cryptographic private key that corresponds to the current
    /// ECDSA cryptographic public key, and that the signed hash matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="hash">The unsigned hash to validate against the current ECDSA cryptographic public key.</param>
    /// <param name="format">The digital signature format that was used to generate the cryptographic digital signature.</param>
    public void VerifyHash(DigitalSignature signature, ReadOnlySpan<byte> hash, DSASignatureFormat format = default)
    {
        CheckSignature(IsHashValid(signature, hash, format));
    }

    /// <summary>
    /// Performs a signature check pre-condition that throws a <see cref="CryptographicException"/> in the event that the specified condition is <see langword="false"/>.
    /// </summary>
    /// <param name="condition">The signature condition to check.</param>
    /// <exception cref="CryptographicException">If the specified condition is <see langword="false"/>.</exception>
    private static void CheckSignature(bool condition)
    {
        if (condition) return;

        const string message = "The specified digital signature could not be verified. Either the specified data is incorrect, " +
                               "or the data was not signed with a private key corresponding to the current public key.";

        throw new CryptographicException(message);
    }
}
