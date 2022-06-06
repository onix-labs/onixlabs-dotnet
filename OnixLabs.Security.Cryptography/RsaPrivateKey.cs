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

/// <summary>
/// Represents an RSA private key.
/// </summary>
public sealed partial class RsaPrivateKey : PrivateKey
{
    /// <summary>
    /// Creates a new instance of the <see cref="RsaPrivateKey"/> class.
    /// </summary>
    /// <param name="data">The private key data.</param>
    /// <param name="type">The hash algorithm type for computing signature data.</param>
    /// <param name="padding">The <see cref="RSASignaturePadding" /> for computing signature data.</param>
    internal RsaPrivateKey(byte[] data, HashAlgorithmType type, RSASignaturePadding padding) : base(data, type)
    {
        Padding = padding;
    }

    /// <summary>
    /// Gets the <see cref="RSASignaturePadding" /> for computing signature data.
    /// </summary>
    public RSASignaturePadding Padding { get; }
}
