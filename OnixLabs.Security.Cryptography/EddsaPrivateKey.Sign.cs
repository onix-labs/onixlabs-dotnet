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

public sealed partial class EddsaPrivateKey
{
    /// <inheritdoc/>
    public byte[] SignData(ReadOnlySpan<byte> data)
    {
        byte[] seed = KeyData;
        try
        {
            byte[] signature = new byte[Ed25519.SignatureLength];
            Ed25519.Sign(seed, data, signature);
            return signature;
        }
        finally
        {
            CryptographicOperations.ZeroMemory(seed);
        }
    }

    /// <inheritdoc/>
    public byte[] SignData(ReadOnlySpan<byte> data, int offset, int count) =>
        SignData(data.Slice(offset, count));

    /// <summary>
    /// Signs the specified <see cref="Stream"/> data.
    /// </summary>
    /// <remarks>
    /// PureEdDSA requires the entire message in memory because it is hashed twice during signing
    /// (once to derive the per-signature nonce and once to derive the challenge scalar). This
    /// overload therefore reads the stream to completion into an in-memory buffer before signing.
    /// </remarks>
    /// <param name="data">The input data to sign.</param>
    /// <returns>Returns a new <see cref="byte"/> array instance containing the cryptographic digital signature.</returns>
    public byte[] SignData(Stream data)
    {
        using MemoryStream buffer = new();
        data.CopyTo(buffer);
        return SignData(buffer.GetBuffer().AsSpan(0, (int)buffer.Length));
    }

    /// <inheritdoc/>
    public byte[] SignData(IBinaryConvertible data) => SignData(data.AsReadOnlySpan());
}
