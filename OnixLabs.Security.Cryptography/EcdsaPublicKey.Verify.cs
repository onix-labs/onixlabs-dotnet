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
    /// <inheritdoc/>
    public bool IsDataValid(ReadOnlySpan<byte> signature, ReadOnlySpan<byte> data, HashAlgorithm algorithm, DSASignatureFormat format = default)
    {
        byte[] hash = algorithm.ComputeHash(data);
        return IsHashValid(signature, hash, format);
    }

    /// <inheritdoc/>
    public bool IsDataValid(ReadOnlySpan<byte> signature, ReadOnlySpan<byte> data, int offset, int count, HashAlgorithm algorithm, DSASignatureFormat format = default)
    {
        byte[] hash = algorithm.ComputeHash(data, offset, count);
        return IsHashValid(signature, hash, format);
    }

    /// <inheritdoc/>
    public bool IsDataValid(ReadOnlySpan<byte> signature, Stream data, HashAlgorithm algorithm, DSASignatureFormat format = default)
    {
        byte[] hash = algorithm.ComputeHash(data);
        return IsHashValid(signature, hash, format);
    }

    /// <inheritdoc/>
    public bool IsDataValid(ReadOnlySpan<byte> signature, IBinaryConvertible data, HashAlgorithm algorithm, DSASignatureFormat format = default)
    {
        byte[] hash = algorithm.ComputeHash(data.AsReadOnlySpan());
        return IsHashValid(signature, hash, format);
    }

    /// <inheritdoc/>
    public bool IsDataValid(ReadOnlySpan<byte> signature, ReadOnlySpan<byte> data, HashAlgorithmName algorithm, DSASignatureFormat format = default)
    {
        return IsDataValidCore(data, signature, algorithm, format);
    }

    /// <inheritdoc/>
    public bool IsDataValid(ReadOnlySpan<byte> signature, ReadOnlySpan<byte> data, int offset, int count, HashAlgorithmName algorithm, DSASignatureFormat format = default)
    {
        return IsDataValidCore(data.Slice(offset, count), signature, algorithm, format);
    }

    /// <inheritdoc/>
    public bool IsDataValid(ReadOnlySpan<byte> signature, Stream data, HashAlgorithmName algorithm, DSASignatureFormat format = default)
    {
        return IsDataValidCore(data, signature, algorithm, format);
    }

    /// <inheritdoc/>
    public bool IsDataValid(ReadOnlySpan<byte> signature, IBinaryConvertible data, HashAlgorithmName algorithm, DSASignatureFormat format = default)
    {
        return IsDataValidCore(data.AsReadOnlySpan(), signature, algorithm, format);
    }

    /// <inheritdoc/>
    public bool IsDataValid(DigitalSignature signature, ReadOnlySpan<byte> data, HashAlgorithm algorithm, DSASignatureFormat format = default)
    {
        byte[] hash = algorithm.ComputeHash(data);
        return IsHashValid(signature, hash, format);
    }

    /// <inheritdoc/>
    public bool IsDataValid(DigitalSignature signature, ReadOnlySpan<byte> data, int offset, int count, HashAlgorithm algorithm, DSASignatureFormat format = default)
    {
        byte[] hash = algorithm.ComputeHash(data, offset, count);
        return IsHashValid(signature, hash, format);
    }

    /// <inheritdoc/>
    public bool IsDataValid(DigitalSignature signature, Stream data, HashAlgorithm algorithm, DSASignatureFormat format = default)
    {
        byte[] hash = algorithm.ComputeHash(data);
        return IsHashValid(signature, hash, format);
    }

    /// <inheritdoc/>
    public bool IsDataValid(DigitalSignature signature, IBinaryConvertible data, HashAlgorithm algorithm, DSASignatureFormat format = default)
    {
        byte[] hash = algorithm.ComputeHash(data.AsReadOnlySpan());
        return IsHashValid(signature, hash, format);
    }

    /// <inheritdoc/>
    public bool IsDataValid(DigitalSignature signature, ReadOnlySpan<byte> data, HashAlgorithmName algorithm, DSASignatureFormat format = default)
    {
        return IsDataValidCore(data, signature.AsReadOnlySpan(), algorithm, format);
    }

    /// <inheritdoc/>
    public bool IsDataValid(DigitalSignature signature, ReadOnlySpan<byte> data, int offset, int count, HashAlgorithmName algorithm, DSASignatureFormat format = default)
    {
        return IsDataValidCore(data.Slice(offset, count), signature.AsReadOnlySpan(), algorithm, format);
    }

    /// <inheritdoc/>
    public bool IsDataValid(DigitalSignature signature, Stream data, HashAlgorithmName algorithm, DSASignatureFormat format = default)
    {
        return IsDataValidCore(data, signature.AsReadOnlySpan(), algorithm, format);
    }

    /// <inheritdoc/>
    public bool IsDataValid(DigitalSignature signature, IBinaryConvertible data, HashAlgorithmName algorithm, DSASignatureFormat format = default)
    {
        return IsDataValidCore(data.AsReadOnlySpan(), signature.AsReadOnlySpan(), algorithm, format);
    }

    /// <inheritdoc/>
    public bool IsHashValid(ReadOnlySpan<byte> signature, Hash hash, DSASignatureFormat format = default)
    {
        return IsHashValidCore(hash.AsReadOnlySpan(), signature, format);
    }

    /// <inheritdoc/>
    public bool IsHashValid(ReadOnlySpan<byte> signature, ReadOnlySpan<byte> hash, DSASignatureFormat format = default)
    {
        return IsHashValidCore(hash, signature, format);
    }

    /// <inheritdoc/>
    public bool IsHashValid(DigitalSignature signature, Hash hash, DSASignatureFormat format = default)
    {
        return IsHashValidCore(hash.AsReadOnlySpan(), signature.AsReadOnlySpan(), format);
    }

    /// <inheritdoc/>
    public bool IsHashValid(DigitalSignature signature, ReadOnlySpan<byte> hash, DSASignatureFormat format = default)
    {
        return IsHashValidCore(hash, signature.AsReadOnlySpan(), format);
    }

    /// <inheritdoc/>
    public void VerifyData(ReadOnlySpan<byte> signature, ReadOnlySpan<byte> data, HashAlgorithm algorithm, DSASignatureFormat format = default)
    {
        CheckSignature(IsDataValid(signature, data, algorithm, format));
    }

    /// <inheritdoc/>
    public void VerifyData(ReadOnlySpan<byte> signature, ReadOnlySpan<byte> data, int offset, int count, HashAlgorithm algorithm, DSASignatureFormat format = default)
    {
        CheckSignature(IsDataValid(signature, data, offset, count, algorithm, format));
    }

    /// <inheritdoc/>
    public void VerifyData(ReadOnlySpan<byte> signature, Stream data, HashAlgorithm algorithm, DSASignatureFormat format = default)
    {
        CheckSignature(IsDataValid(signature, data, algorithm, format));
    }

    /// <inheritdoc/>
    public void VerifyData(ReadOnlySpan<byte> signature, IBinaryConvertible data, HashAlgorithm algorithm, DSASignatureFormat format = default)
    {
        CheckSignature(IsDataValid(signature, data, algorithm, format));
    }

    /// <inheritdoc/>
    public void VerifyData(ReadOnlySpan<byte> signature, ReadOnlySpan<byte> data, HashAlgorithmName algorithm, DSASignatureFormat format = default)
    {
        CheckSignature(IsDataValid(signature, data, algorithm, format));
    }

    /// <inheritdoc/>
    public void VerifyData(ReadOnlySpan<byte> signature, ReadOnlySpan<byte> data, int offset, int count, HashAlgorithmName algorithm, DSASignatureFormat format = default)
    {
        CheckSignature(IsDataValid(signature, data, offset, count, algorithm, format));
    }

    /// <inheritdoc/>
    public void VerifyData(ReadOnlySpan<byte> signature, Stream data, HashAlgorithmName algorithm, DSASignatureFormat format = default)
    {
        CheckSignature(IsDataValid(signature, data, algorithm, format));
    }

    /// <inheritdoc/>
    public void VerifyData(ReadOnlySpan<byte> signature, IBinaryConvertible data, HashAlgorithmName algorithm, DSASignatureFormat format = default)
    {
        CheckSignature(IsDataValid(signature, data, algorithm, format));
    }

    /// <inheritdoc/>
    public void VerifyData(DigitalSignature signature, ReadOnlySpan<byte> data, HashAlgorithm algorithm, DSASignatureFormat format = default)
    {
        CheckSignature(IsDataValid(signature, data, algorithm, format));
    }

    /// <inheritdoc/>
    public void VerifyData(DigitalSignature signature, ReadOnlySpan<byte> data, int offset, int count, HashAlgorithm algorithm, DSASignatureFormat format = default)
    {
        CheckSignature(IsDataValid(signature, data, offset, count, algorithm, format));
    }

    /// <inheritdoc/>
    public void VerifyData(DigitalSignature signature, Stream data, HashAlgorithm algorithm, DSASignatureFormat format = default)
    {
        CheckSignature(IsDataValid(signature, data, algorithm, format));
    }

    /// <inheritdoc/>
    public void VerifyData(DigitalSignature signature, IBinaryConvertible data, HashAlgorithm algorithm, DSASignatureFormat format = default)
    {
        CheckSignature(IsDataValid(signature, data, algorithm, format));
    }

    /// <inheritdoc/>
    public void VerifyData(DigitalSignature signature, ReadOnlySpan<byte> data, HashAlgorithmName algorithm, DSASignatureFormat format = default)
    {
        CheckSignature(IsDataValid(signature, data, algorithm, format));
    }

    /// <inheritdoc/>
    public void VerifyData(DigitalSignature signature, ReadOnlySpan<byte> data, int offset, int count, HashAlgorithmName algorithm, DSASignatureFormat format = default)
    {
        CheckSignature(IsDataValid(signature, data, offset, count, algorithm, format));
    }

    /// <inheritdoc/>
    public void VerifyData(DigitalSignature signature, Stream data, HashAlgorithmName algorithm, DSASignatureFormat format = default)
    {
        CheckSignature(IsDataValid(signature, data, algorithm, format));
    }

    /// <inheritdoc/>
    public void VerifyData(DigitalSignature signature, IBinaryConvertible data, HashAlgorithmName algorithm, DSASignatureFormat format = default)
    {
        CheckSignature(IsDataValid(signature, data, algorithm, format));
    }

    /// <inheritdoc/>
    public void VerifyHash(ReadOnlySpan<byte> signature, Hash hash, DSASignatureFormat format = default)
    {
        CheckSignature(IsHashValid(signature, hash, format));
    }

    /// <inheritdoc/>
    public void VerifyHash(ReadOnlySpan<byte> signature, ReadOnlySpan<byte> hash, DSASignatureFormat format = default)
    {
        CheckSignature(IsHashValid(signature, hash, format));
    }

    /// <inheritdoc/>
    public void VerifyHash(DigitalSignature signature, Hash hash, DSASignatureFormat format = default)
    {
        CheckSignature(IsHashValid(signature, hash, format));
    }

    /// <inheritdoc/>
    public void VerifyHash(DigitalSignature signature, ReadOnlySpan<byte> hash, DSASignatureFormat format = default)
    {
        CheckSignature(IsHashValid(signature, hash, format));
    }

    /// <summary>
    /// Determines whether the specified signature of the specified data is valid, hashing the data with the specified algorithm.
    /// </summary>
    /// <param name="data">The signed data.</param>
    /// <param name="signature">The signature to verify.</param>
    /// <param name="algorithm">The hash algorithm with which the data was hashed before signing.</param>
    /// <param name="format">The signature format.</param>
    /// <returns>Returns <see langword="true"/> if the signature is valid; otherwise, <see langword="false"/>.</returns>
    private bool IsDataValidCore(
        ReadOnlySpan<byte> data,
        ReadOnlySpan<byte> signature,
        HashAlgorithmName algorithm,
        DSASignatureFormat format)
    {
        if (TryGetSecp256k1PublicKey(out Secp256k1Point point))
        {
            using IncrementalHash hash = IncrementalHash.CreateHash(algorithm);
            hash.AppendData(data);
            return Secp256k1.Verify(point, hash.GetHashAndReset(), signature, format);
        }

        using ECDsa key = ImportKeyData();
        return key.VerifyData(data, signature, algorithm, format);
    }

    /// <summary>
    /// Determines whether the specified signature of the specified stream data is valid, hashing the data with the specified algorithm.
    /// </summary>
    /// <param name="data">The signed data, which is read to the end.</param>
    /// <param name="signature">The signature to verify.</param>
    /// <param name="algorithm">The hash algorithm with which the data was hashed before signing.</param>
    /// <param name="format">The signature format.</param>
    /// <returns>Returns <see langword="true"/> if the signature is valid; otherwise, <see langword="false"/>.</returns>
    private bool IsDataValidCore(Stream data, ReadOnlySpan<byte> signature, HashAlgorithmName algorithm, DSASignatureFormat format)
    {
        if (TryGetSecp256k1PublicKey(out Secp256k1Point point))
        {
            using IncrementalHash hash = IncrementalHash.CreateHash(algorithm);
            byte[] buffer = new byte[4096];

            for (int read = data.Read(buffer); read > 0; read = data.Read(buffer))
                hash.AppendData(buffer, 0, read);

            return Secp256k1.Verify(point, hash.GetHashAndReset(), signature, format);
        }

        using ECDsa key = ImportKeyData();
        return key.VerifyData(data, signature.ToArray(), algorithm, format);
    }

    /// <summary>
    /// Determines whether the specified signature of the specified hash is valid.
    /// </summary>
    /// <param name="hash">The signed hash.</param>
    /// <param name="signature">The signature to verify.</param>
    /// <param name="format">The signature format.</param>
    /// <returns>Returns <see langword="true"/> if the signature is valid; otherwise, <see langword="false"/>.</returns>
    private bool IsHashValidCore(ReadOnlySpan<byte> hash, ReadOnlySpan<byte> signature, DSASignatureFormat format)
    {
        if (TryGetSecp256k1PublicKey(out Secp256k1Point point))
            return Secp256k1.Verify(point, hash, signature, format);

        using ECDsa key = ImportKeyData();
        return key.VerifyHash(hash, signature, format);
    }

    /// <summary>
    /// Performs a signature check pre-condition that throws a <see cref="CryptographicException"/> in the event that the specified
    /// condition is <see langword="false"/>.
    /// </summary>
    /// <param name="condition">The signature condition to check.</param>
    /// <exception cref="CryptographicException">Thrown when the specified condition is <see langword="false"/>.</exception>
    private static void CheckSignature(bool condition)
    {
        if (condition) return;

        const string message = "The specified digital signature could not be verified. Either the specified data is incorrect, " +
                               "or the data was not signed with a private key corresponding to the current public key.";

        throw new CryptographicException(message);
    }
}
