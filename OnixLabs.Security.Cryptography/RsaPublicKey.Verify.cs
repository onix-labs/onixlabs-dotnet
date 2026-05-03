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

public sealed partial class RsaPublicKey
{
    /// <inheritdoc/>
    public bool IsDataValid(ReadOnlySpan<byte> signature, ReadOnlySpan<byte> data, HashAlgorithmName algorithm, RSASignaturePadding padding)
    {
        using RSA key = ImportKeyData();
        return key.VerifyData(data, signature, algorithm, padding);
    }

    /// <inheritdoc/>
    public bool IsDataValid(ReadOnlySpan<byte> signature, ReadOnlySpan<byte> data, int offset, int count, HashAlgorithmName algorithm, RSASignaturePadding padding)
    {
        using RSA key = ImportKeyData();
        return key.VerifyData(data.Slice(offset, count), signature, algorithm, padding);
    }

    /// <inheritdoc/>
    public bool IsDataValid(ReadOnlySpan<byte> signature, Stream data, HashAlgorithmName algorithm, RSASignaturePadding padding)
    {
        using RSA key = ImportKeyData();
        return key.VerifyData(data, signature.ToArray(), algorithm, padding);
    }

    /// <inheritdoc/>
    public bool IsDataValid(ReadOnlySpan<byte> signature, IBinaryConvertible data, HashAlgorithmName algorithm, RSASignaturePadding padding)
    {
        using RSA key = ImportKeyData();
        return key.VerifyData(data.AsReadOnlySpan(), signature, algorithm, padding);
    }

    /// <inheritdoc/>
    public bool IsDataValid(DigitalSignature signature, ReadOnlySpan<byte> data, HashAlgorithmName algorithm, RSASignaturePadding padding)
    {
        using RSA key = ImportKeyData();
        return key.VerifyData(data, signature.AsReadOnlySpan(), algorithm, padding);
    }

    /// <inheritdoc/>
    public bool IsDataValid(DigitalSignature signature, ReadOnlySpan<byte> data, int offset, int count, HashAlgorithmName algorithm, RSASignaturePadding padding)
    {
        using RSA key = ImportKeyData();
        return key.VerifyData(data.Slice(offset, count), signature.AsReadOnlySpan(), algorithm, padding);
    }

    /// <inheritdoc/>
    public bool IsDataValid(DigitalSignature signature, Stream data, HashAlgorithmName algorithm, RSASignaturePadding padding)
    {
        using RSA key = ImportKeyData();
        // TODO : Span for signature is inefficiently converted back to an array.
        return key.VerifyData(data, signature.AsReadOnlySpan().ToArray(), algorithm, padding);
    }

    /// <inheritdoc/>
    public bool IsDataValid(DigitalSignature signature, IBinaryConvertible data, HashAlgorithmName algorithm, RSASignaturePadding padding)
    {
        using RSA key = ImportKeyData();
        return key.VerifyData(data.AsReadOnlySpan(), signature.AsReadOnlySpan(), algorithm, padding);
    }

    /// <inheritdoc/>
    public bool IsHashValid(ReadOnlySpan<byte> signature, ReadOnlySpan<byte> hash, HashAlgorithmName algorithm, RSASignaturePadding padding)
    {
        using RSA key = ImportKeyData();
        return key.VerifyHash(hash, signature, algorithm, padding);
    }

    /// <inheritdoc/>
    public bool IsHashValid(DigitalSignature signature, ReadOnlySpan<byte> hash, HashAlgorithmName algorithm, RSASignaturePadding padding)
    {
        using RSA key = ImportKeyData();
        return key.VerifyHash(hash, signature.AsReadOnlySpan(), algorithm, padding);
    }

    /// <inheritdoc/>
    public bool IsHashValid(ReadOnlySpan<byte> signature, Hash hash, HashAlgorithmName algorithm, RSASignaturePadding padding)
    {
        using RSA key = ImportKeyData();
        return key.VerifyHash(hash.AsReadOnlySpan(), signature, algorithm, padding);
    }

    /// <inheritdoc/>
    public bool IsHashValid(DigitalSignature signature, Hash hash, HashAlgorithmName algorithm, RSASignaturePadding padding)
    {
        using RSA key = ImportKeyData();
        return key.VerifyHash(hash.AsReadOnlySpan(), signature.AsReadOnlySpan(), algorithm, padding);
    }

    /// <inheritdoc/>
    public void VerifyData(ReadOnlySpan<byte> signature, ReadOnlySpan<byte> data, HashAlgorithmName algorithm, RSASignaturePadding padding)
    {
        CheckSignature(IsDataValid(signature, data, algorithm, padding));
    }

    /// <inheritdoc/>
    public void VerifyData(ReadOnlySpan<byte> signature, ReadOnlySpan<byte> data, int offset, int count, HashAlgorithmName algorithm, RSASignaturePadding padding)
    {
        CheckSignature(IsDataValid(signature, data, offset, count, algorithm, padding));
    }

    /// <inheritdoc/>
    public void VerifyData(ReadOnlySpan<byte> signature, Stream data, HashAlgorithmName algorithm, RSASignaturePadding padding)
    {
        CheckSignature(IsDataValid(signature, data, algorithm, padding));
    }

    /// <inheritdoc/>
    public void VerifyData(ReadOnlySpan<byte> signature, IBinaryConvertible data, HashAlgorithmName algorithm, RSASignaturePadding padding)
    {
        CheckSignature(IsDataValid(signature, data, algorithm, padding));
    }

    /// <inheritdoc/>
    public void VerifyData(DigitalSignature signature, ReadOnlySpan<byte> data, HashAlgorithmName algorithm, RSASignaturePadding padding)
    {
        CheckSignature(IsDataValid(signature, data, algorithm, padding));
    }

    /// <inheritdoc/>
    public void VerifyData(DigitalSignature signature, ReadOnlySpan<byte> data, int offset, int count, HashAlgorithmName algorithm, RSASignaturePadding padding)
    {
        CheckSignature(IsDataValid(signature, data, offset, count, algorithm, padding));
    }

    /// <inheritdoc/>
    public void VerifyData(DigitalSignature signature, Stream data, HashAlgorithmName algorithm, RSASignaturePadding padding)
    {
        CheckSignature(IsDataValid(signature, data, algorithm, padding));
    }

    /// <inheritdoc/>
    public void VerifyData(DigitalSignature signature, IBinaryConvertible data, HashAlgorithmName algorithm, RSASignaturePadding padding)
    {
        CheckSignature(IsDataValid(signature, data, algorithm, padding));
    }

    /// <inheritdoc/>
    public void VerifyHash(ReadOnlySpan<byte> signature, ReadOnlySpan<byte> hash, HashAlgorithmName algorithm, RSASignaturePadding padding)
    {
        CheckSignature(IsHashValid(signature, hash, algorithm, padding));
    }

    /// <inheritdoc/>
    public void VerifyHash(DigitalSignature signature, ReadOnlySpan<byte> hash, HashAlgorithmName algorithm, RSASignaturePadding padding)
    {
        CheckSignature(IsHashValid(signature, hash, algorithm, padding));
    }

    /// <inheritdoc/>
    public void VerifyHash(ReadOnlySpan<byte> signature, Hash hash, HashAlgorithmName algorithm, RSASignaturePadding padding)
    {
        CheckSignature(IsHashValid(signature, hash, algorithm, padding));
    }

    /// <inheritdoc/>
    public void VerifyHash(DigitalSignature signature, Hash hash, HashAlgorithmName algorithm, RSASignaturePadding padding)
    {
        CheckSignature(IsHashValid(signature, hash, algorithm, padding));
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
