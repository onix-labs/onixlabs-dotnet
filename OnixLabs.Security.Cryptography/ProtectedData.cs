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

using System.IO;
using System.Security.Cryptography;
using OnixLabs.Core.Linq;
using Aes = System.Security.Cryptography.Aes;

namespace OnixLabs.Security.Cryptography;

/// <summary>
/// Represents an in-memory data protection mechanism for sensitive, long-lived cryptographic data.
/// </summary>
// ReSharper disable HeapView.ObjectAllocation.Evident
internal sealed class ProtectedData
{
    private readonly byte[] key = Salt.CreateNonZero(32).AsReadOnlySpan().ToArray();
    private readonly byte[] iv = Salt.CreateNonZero(16).AsReadOnlySpan().ToArray();

    /// <summary>
    /// Encrypted the specified data.
    /// </summary>
    /// <param name="data">The data to encrypt.</param>
    /// <returns>Returns the encrypted data.</returns>
    public byte[] Encrypt(byte[] data)
    {
        if (data.IsEmpty()) return data;

        using Aes algorithm = Aes.Create();

        algorithm.Key = key;
        algorithm.IV = iv;
        algorithm.Padding = PaddingMode.PKCS7;

        ICryptoTransform transform = algorithm.CreateEncryptor(algorithm.Key, algorithm.IV);

        using MemoryStream memoryStream = new();
        using CryptoStream cryptoStream = new(memoryStream, transform, CryptoStreamMode.Write);

        cryptoStream.Write(data, 0, data.Length);
        cryptoStream.FlushFinalBlock();

        return memoryStream.ToArray();
    }

    /// <summary>
    /// Decrypts the specified data.
    /// </summary>
    /// <param name="data">The data to decrypt.</param>
    /// <returns>Returns the decrypted data.</returns>
    public byte[] Decrypt(byte[] data)
    {
        if (data.IsEmpty()) return data;

        using Aes algorithm = Aes.Create();

        algorithm.Key = key;
        algorithm.IV = iv;
        algorithm.Padding = PaddingMode.PKCS7;

        ICryptoTransform transform = algorithm.CreateDecryptor(algorithm.Key, algorithm.IV);

        using MemoryStream memoryStream = new(data);
        using CryptoStream cryptoStream = new(memoryStream, transform, CryptoStreamMode.Read);
        using MemoryStream resultStream = new();

        cryptoStream.CopyTo(resultStream);

        return resultStream.ToArray();
    }
}
