// Copyright 2020-2024 ONIXLabs
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
    /// Determines whether the specified cryptographic digital signature is valid for the specified unsigned data.
    /// </summary>
    /// <param name="data">The unsigned data to validate.</param>
    /// <param name="signature">The cryptographic digital signature to validate.</param>
    /// <param name="algorithm">The <see cref="HashAlgorithm"/> that will be used to hash the specified unsigned data.</param>
    /// <param name="format">The signature format to use when signing the specified unsigned data.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the specified unsigned data was signed with the cryptographic private key component for the
    /// current cryptographic public key, and matches the specified cryptographic digital signature; otherwise, <see langword="false"/>.
    /// </returns>
    public bool IsDataValid(ReadOnlySpan<byte> data, ReadOnlySpan<byte> signature, HashAlgorithm algorithm, DSASignatureFormat format = default) =>
        IsHashValid(algorithm.ComputeHash(data.ToArray()), signature, format);

    /// <summary>
    /// Determines whether the specified cryptographic digital signature is valid for the specified unsigned data.
    /// </summary>
    /// <param name="data">The unsigned data to validate.</param>
    /// <param name="offset">The offset into the <see cref="ReadOnlySpan{T}"/> from which to begin using data.</param>
    /// <param name="count">The number of bytes in the <see cref="ReadOnlySpan{T}"/> to use as data.</param>
    /// <param name="signature">The cryptographic digital signature to validate.</param>
    /// <param name="algorithm">The <see cref="HashAlgorithm"/> that will be used to hash the specified unsigned data.</param>
    /// <param name="format">The signature format to use when signing the specified unsigned data.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the specified unsigned data was signed with the cryptographic private key component for the
    /// current cryptographic public key, and matches the specified cryptographic digital signature; otherwise, <see langword="false"/>.
    /// </returns>
    public bool IsDataValid(ReadOnlySpan<byte> data, int offset, int count, ReadOnlySpan<byte> signature, HashAlgorithm algorithm, DSASignatureFormat format = default) =>
        IsHashValid(algorithm.ComputeHash(data.ToArray(), offset, count), signature, format);

    /// <summary>
    /// Determines whether the specified cryptographic digital signature is valid for the specified unsigned data.
    /// </summary>
    /// <param name="data">The unsigned data to validate.</param>
    /// <param name="signature">The cryptographic digital signature to validate.</param>
    /// <param name="algorithm">The <see cref="HashAlgorithm"/> that will be used to hash the specified unsigned data.</param>
    /// <param name="format">The signature format to use when signing the specified unsigned data.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the specified unsigned data was signed with the cryptographic private key component for the
    /// current cryptographic public key, and matches the specified cryptographic digital signature; otherwise, <see langword="false"/>.
    /// </returns>
    public bool IsDataValid(IBinaryConvertible data, ReadOnlySpan<byte> signature, HashAlgorithm algorithm, DSASignatureFormat format = default) =>
        IsHashValid(algorithm.ComputeHash(data.ToByteArray()), signature, format);

    /// <summary>
    /// Determines whether the specified cryptographic digital signature is valid for the specified unsigned data.
    /// </summary>
    /// <param name="data">The unsigned data to validate.</param>
    /// <param name="signature">The cryptographic digital signature to validate.</param>
    /// <param name="algorithm">The <see cref="HashAlgorithm"/> that will be used to hash the specified unsigned data.</param>
    /// <param name="format">The signature format to use when signing the specified unsigned data.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the specified unsigned data was signed with the cryptographic private key component for the
    /// current cryptographic public key, and matches the specified cryptographic digital signature; otherwise, <see langword="false"/>.
    /// </returns>
    public bool IsDataValid(Stream data, ReadOnlySpan<byte> signature, HashAlgorithm algorithm, DSASignatureFormat format = default) =>
        IsHashValid(algorithm.ComputeHash(data), signature, format);

    /// <summary>
    /// Determines whether the specified cryptographic digital signature is valid for the specified unsigned data.
    /// </summary>
    /// <param name="data">The unsigned data to validate.</param>
    /// <param name="signature">The cryptographic digital signature to validate.</param>
    /// <param name="algorithm">The <see cref="HashAlgorithm"/> that will be used to hash the specified unsigned data.</param>
    /// <param name="format">The signature format to use when signing the specified unsigned data.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the specified unsigned data was signed with the cryptographic private key component for the
    /// current cryptographic public key, and matches the specified cryptographic digital signature; otherwise, <see langword="false"/>.
    /// </returns>
    public bool IsDataValid(ReadOnlySpan<byte> data, DigitalSignature signature, HashAlgorithm algorithm, DSASignatureFormat format = default) =>
        IsHashValid(algorithm.ComputeHash(data.ToArray()), signature, format);

    /// <summary>
    /// Determines whether the specified cryptographic digital signature is valid for the specified unsigned data.
    /// </summary>
    /// <param name="data">The unsigned data to validate.</param>
    /// <param name="offset">The offset into the <see cref="ReadOnlySpan{T}"/> from which to begin using data.</param>
    /// <param name="count">The number of bytes in the <see cref="ReadOnlySpan{T}"/> to use as data.</param>
    /// <param name="signature">The cryptographic digital signature to validate.</param>
    /// <param name="algorithm">The <see cref="HashAlgorithm"/> that will be used to hash the specified unsigned data.</param>
    /// <param name="format">The signature format to use when signing the specified unsigned data.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the specified unsigned data was signed with the cryptographic private key component for the
    /// current cryptographic public key, and matches the specified cryptographic digital signature; otherwise, <see langword="false"/>.
    /// </returns>
    public bool IsDataValid(ReadOnlySpan<byte> data, int offset, int count, DigitalSignature signature, HashAlgorithm algorithm, DSASignatureFormat format = default) =>
        IsHashValid(algorithm.ComputeHash(data.ToArray(), offset, count), signature, format);

    /// <summary>
    /// Determines whether the specified cryptographic digital signature is valid for the specified unsigned data.
    /// </summary>
    /// <param name="data">The unsigned data to validate.</param>
    /// <param name="signature">The cryptographic digital signature to validate.</param>
    /// <param name="algorithm">The <see cref="HashAlgorithm"/> that will be used to hash the specified unsigned data.</param>
    /// <param name="format">The signature format to use when signing the specified unsigned data.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the specified unsigned data was signed with the cryptographic private key component for the
    /// current cryptographic public key, and matches the specified cryptographic digital signature; otherwise, <see langword="false"/>.
    /// </returns>
    public bool IsDataValid(IBinaryConvertible data, DigitalSignature signature, HashAlgorithm algorithm, DSASignatureFormat format = default) =>
        IsHashValid(algorithm.ComputeHash(data.ToByteArray()), signature, format);

    /// <summary>
    /// Determines whether the specified cryptographic digital signature is valid for the specified unsigned data.
    /// </summary>
    /// <param name="data">The unsigned data to validate.</param>
    /// <param name="signature">The cryptographic digital signature to validate.</param>
    /// <param name="algorithm">The <see cref="HashAlgorithm"/> that will be used to hash the specified unsigned data.</param>
    /// <param name="format">The signature format to use when signing the specified unsigned data.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the specified unsigned data was signed with the cryptographic private key component for the
    /// current cryptographic public key, and matches the specified cryptographic digital signature; otherwise, <see langword="false"/>.
    /// </returns>
    public bool IsDataValid(Stream data, DigitalSignature signature, HashAlgorithm algorithm, DSASignatureFormat format = default) =>
        IsHashValid(algorithm.ComputeHash(data), signature, format);

    /// <summary>
    /// Determines whether the specified cryptographic digital signature is valid for the specified hash.
    /// </summary>
    /// <param name="hash">The unsigned hash to validate.</param>
    /// <param name="signature">The cryptographic digital signature to validate.</param>
    /// <param name="format">The signature format to use when signing the specified unsigned data.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the specified unsigned hash was signed with the cryptographic private key component for the
    /// current cryptographic public key, and matches the specified cryptographic digital signature; otherwise, <see langword="false"/>.
    /// </returns>
    public bool IsHashValid(Hash hash, ReadOnlySpan<byte> signature, DSASignatureFormat format = default) =>
        IsHashValid(hash.ToByteArray(), signature, format);

    /// <summary>
    /// Determines whether the specified cryptographic digital signature is valid for the specified hash.
    /// </summary>
    /// <param name="hash">The unsigned hash to validate.</param>
    /// <param name="signature">The cryptographic digital signature to validate.</param>
    /// <param name="format">The signature format to use when signing the specified unsigned data.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the specified unsigned hash was signed with the cryptographic private key component for the
    /// current cryptographic public key, and matches the specified cryptographic digital signature; otherwise, <see langword="false"/>.
    /// </returns>
    public bool IsHashValid(Hash hash, DigitalSignature signature, DSASignatureFormat format = default) =>
        IsHashValid(hash.ToByteArray(), signature.ToByteArray(), format);

    /// <summary>
    /// Determines whether the specified cryptographic digital signature is valid for the specified hash.
    /// </summary>
    /// <param name="hash">The unsigned hash to validate.</param>
    /// <param name="signature">The cryptographic digital signature to validate.</param>
    /// <param name="format">The signature format to use when signing the specified unsigned data.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the specified unsigned hash was signed with the cryptographic private key component for the
    /// current cryptographic public key, and matches the specified cryptographic digital signature; otherwise, <see langword="false"/>.
    /// </returns>
    public bool IsHashValid(ReadOnlySpan<byte> hash, DigitalSignature signature, DSASignatureFormat format = default) =>
        IsHashValid(hash.ToArray(), signature.ToByteArray(), format);

    /// <summary>
    /// Determines whether the specified cryptographic digital signature is valid for the specified hash.
    /// </summary>
    /// <param name="hash">The unsigned hash to validate.</param>
    /// <param name="signature">The cryptographic digital signature to validate.</param>
    /// <param name="format">The signature format to use when signing the specified unsigned data.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the specified unsigned hash was signed with the cryptographic private key component for the
    /// current cryptographic public key, and matches the specified cryptographic digital signature; otherwise, <see langword="false"/>.
    /// </returns>
    public bool IsHashValid(ReadOnlySpan<byte> hash, ReadOnlySpan<byte> signature, DSASignatureFormat format = default)
    {
        using ECDsa publicKey = ImportPublicKey();
        return publicKey.VerifyHash(hash, signature, format);
    }

    /// <summary>
    /// Verifies that the specified cryptographic digital signature is valid for the specified unsigned data.
    /// </summary>
    /// <param name="data">The unsigned data to validate.</param>
    /// <param name="signature">The cryptographic digital signature to validate.</param>
    /// <param name="algorithm">The <see cref="HashAlgorithm"/> that will be used to hash the specified unsigned data.</param>
    /// <param name="format">The signature format to use when signing the specified unsigned data.</param>
    /// <exception cref="CryptographicException">If the cryptographic digital signature cannot be verified.</exception>
    public void VerifyData(ReadOnlySpan<byte> data, ReadOnlySpan<byte> signature, HashAlgorithm algorithm, DSASignatureFormat format = default) =>
        CheckSignature(IsDataValid(data, signature, algorithm, format));

    /// <summary>
    /// Verifies that the specified cryptographic digital signature is valid for the specified unsigned data.
    /// </summary>
    /// <param name="data">The unsigned data to validate.</param>
    /// <param name="offset">The offset into the <see cref="ReadOnlySpan{T}"/> from which to begin using data.</param>
    /// <param name="count">The number of bytes in the <see cref="ReadOnlySpan{T}"/> to use as data.</param>
    /// <param name="signature">The cryptographic digital signature to validate.</param>
    /// <param name="algorithm">The <see cref="HashAlgorithm"/> that will be used to hash the specified unsigned data.</param>
    /// <param name="format">The signature format to use when signing the specified unsigned data.</param>
    /// <exception cref="CryptographicException">If the cryptographic digital signature cannot be verified.</exception>
    public void VerifyData(ReadOnlySpan<byte> data, int offset, int count, ReadOnlySpan<byte> signature, HashAlgorithm algorithm, DSASignatureFormat format = default) =>
        CheckSignature(IsDataValid(data, offset, count, signature, algorithm, format));

    /// <summary>
    /// Verifies that the specified cryptographic digital signature is valid for the specified unsigned data.
    /// </summary>
    /// <param name="data">The unsigned data to validate.</param>
    /// <param name="signature">The cryptographic digital signature to validate.</param>
    /// <param name="algorithm">The <see cref="HashAlgorithm"/> that will be used to hash the specified unsigned data.</param>
    /// <param name="format">The signature format to use when signing the specified unsigned data.</param>
    /// <exception cref="CryptographicException">If the cryptographic digital signature cannot be verified.</exception>
    public void VerifyData(IBinaryConvertible data, ReadOnlySpan<byte> signature, HashAlgorithm algorithm, DSASignatureFormat format = default) =>
        CheckSignature(IsDataValid(data, signature, algorithm, format));

    /// <summary>
    /// Verifies that the specified cryptographic digital signature is valid for the specified unsigned data.
    /// </summary>
    /// <param name="data">The unsigned data to validate.</param>
    /// <param name="signature">The cryptographic digital signature to validate.</param>
    /// <param name="algorithm">The <see cref="HashAlgorithm"/> that will be used to hash the specified unsigned data.</param>
    /// <param name="format">The signature format to use when signing the specified unsigned data.</param>
    /// <exception cref="CryptographicException">If the cryptographic digital signature cannot be verified.</exception>
    public void VerifyData(Stream data, ReadOnlySpan<byte> signature, HashAlgorithm algorithm, DSASignatureFormat format = default) =>
        CheckSignature(IsDataValid(data, signature, algorithm, format));

    /// <summary>
    /// Verifies that the specified cryptographic digital signature is valid for the specified unsigned data.
    /// </summary>
    /// <param name="data">The unsigned data to validate.</param>
    /// <param name="signature">The cryptographic digital signature to validate.</param>
    /// <param name="algorithm">The <see cref="HashAlgorithm"/> that will be used to hash the specified unsigned data.</param>
    /// <param name="format">The signature format to use when signing the specified unsigned data.</param>
    /// <exception cref="CryptographicException">If the cryptographic digital signature cannot be verified.</exception>
    public void VerifyData(ReadOnlySpan<byte> data, DigitalSignature signature, HashAlgorithm algorithm, DSASignatureFormat format = default) =>
        CheckSignature(IsDataValid(data, signature, algorithm, format));

    /// <summary>
    /// Verifies that the specified cryptographic digital signature is valid for the specified unsigned data.
    /// </summary>
    /// <param name="data">The unsigned data to validate.</param>
    /// <param name="offset">The offset into the <see cref="ReadOnlySpan{T}"/> from which to begin using data.</param>
    /// <param name="count">The number of bytes in the <see cref="ReadOnlySpan{T}"/> to use as data.</param>
    /// <param name="signature">The cryptographic digital signature to validate.</param>
    /// <param name="algorithm">The <see cref="HashAlgorithm"/> that will be used to hash the specified unsigned data.</param>
    /// <param name="format">The signature format to use when signing the specified unsigned data.</param>
    /// <exception cref="CryptographicException">If the cryptographic digital signature cannot be verified.</exception>
    public void VerifyData(ReadOnlySpan<byte> data, int offset, int count, DigitalSignature signature, HashAlgorithm algorithm, DSASignatureFormat format = default) =>
        CheckSignature(IsDataValid(data, offset, count, signature, algorithm, format));

    /// <summary>
    /// Verifies that the specified cryptographic digital signature is valid for the specified unsigned data.
    /// </summary>
    /// <param name="data">The unsigned data to validate.</param>
    /// <param name="signature">The cryptographic digital signature to validate.</param>
    /// <param name="algorithm">The <see cref="HashAlgorithm"/> that will be used to hash the specified unsigned data.</param>
    /// <param name="format">The signature format to use when signing the specified unsigned data.</param>
    /// <exception cref="CryptographicException">If the cryptographic digital signature cannot be verified.</exception>
    public void VerifyData(IBinaryConvertible data, DigitalSignature signature, HashAlgorithm algorithm, DSASignatureFormat format = default) =>
        CheckSignature(IsDataValid(data, signature, algorithm, format));

    /// <summary>
    /// Verifies that the specified cryptographic digital signature is valid for the specified unsigned data.
    /// </summary>
    /// <param name="data">The unsigned data to validate.</param>
    /// <param name="signature">The cryptographic digital signature to validate.</param>
    /// <param name="algorithm">The <see cref="HashAlgorithm"/> that will be used to hash the specified unsigned data.</param>
    /// <param name="format">The signature format to use when signing the specified unsigned data.</param>
    /// <exception cref="CryptographicException">If the cryptographic digital signature cannot be verified.</exception>
    public void VerifyData(Stream data, DigitalSignature signature, HashAlgorithm algorithm, DSASignatureFormat format = default) =>
        CheckSignature(IsDataValid(data, signature, algorithm, format));

    /// <summary>
    /// Verifies that the specified cryptographic digital signature is valid for the specified unsigned hash.
    /// </summary>
    /// <param name="hash">The unsigned hash to validate.</param>
    /// <param name="signature">The cryptographic digital signature to validate.</param>
    /// <param name="format">The signature format to use when signing the specified unsigned hash.</param>
    /// <exception cref="CryptographicException">If the cryptographic digital signature cannot be verified.</exception>
    public void VerifyHash(Hash hash, ReadOnlySpan<byte> signature, DSASignatureFormat format = default) =>
        CheckSignature(IsHashValid(hash, signature, format));

    /// <summary>
    /// Verifies that the specified cryptographic digital signature is valid for the specified unsigned hash.
    /// </summary>
    /// <param name="hash">The unsigned hash to validate.</param>
    /// <param name="signature">The cryptographic digital signature to validate.</param>
    /// <param name="format">The signature format to use when signing the specified unsigned hash.</param>
    /// <exception cref="CryptographicException">If the cryptographic digital signature cannot be verified.</exception>
    public void VerifyHash(ReadOnlySpan<byte> hash, ReadOnlySpan<byte> signature, DSASignatureFormat format = default) =>
        CheckSignature(IsHashValid(hash, signature, format));


    /// <summary>
    /// Verifies that the specified cryptographic digital signature is valid for the specified unsigned hash.
    /// </summary>
    /// <param name="hash">The unsigned hash to validate.</param>
    /// <param name="signature">The cryptographic digital signature to validate.</param>
    /// <param name="format">The signature format to use when signing the specified unsigned hash.</param>
    /// <exception cref="CryptographicException">If the cryptographic digital signature cannot be verified.</exception>
    public void VerifyHash(Hash hash, DigitalSignature signature, DSASignatureFormat format = default) =>
        CheckSignature(IsHashValid(hash, signature, format));

    /// <summary>
    /// Verifies that the specified cryptographic digital signature is valid for the specified unsigned hash.
    /// </summary>
    /// <param name="hash">The unsigned hash to validate.</param>
    /// <param name="signature">The cryptographic digital signature to validate.</param>
    /// <param name="format">The signature format to use when signing the specified unsigned hash.</param>
    /// <exception cref="CryptographicException">If the cryptographic digital signature cannot be verified.</exception>
    public void VerifyHash(ReadOnlySpan<byte> hash, DigitalSignature signature, DSASignatureFormat format = default) =>
        CheckSignature(IsHashValid(hash, signature, format));

    /// <summary>
    /// Performs a signature pre-condition check, which fails if the condition returns <see langword="false"/>.
    /// </summary>
    /// <param name="condition">The condition to check.</param>
    /// <exception cref="CryptographicException">If the condition fails.</exception>
    private static void CheckSignature(bool condition)
    {
        if (!condition)
            throw new CryptographicException(
                "The specified digital signature could not be verified. Either the specified data is incorrect, " +
                "or the data was not signed with a private key corresponding to the current public key.");
    }
}
