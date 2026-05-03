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
using System.Security.Cryptography;

namespace OnixLabs.Security.Cryptography;

public sealed partial class EcdhPrivateKey
{
    /// <inheritdoc/>
    public static EcdhPrivateKey ImportPem(ReadOnlySpan<char> data)
    {
        using ECDiffieHellman algorithm = ECDiffieHellman.Create();
        algorithm.ImportFromPem(data);
        // ReSharper disable once HeapView.ObjectAllocation.Evident
        return new EcdhPrivateKey(algorithm);
    }

    /// <inheritdoc/>
    public static EcdhPrivateKey ImportPem(ReadOnlySpan<char> data, ReadOnlySpan<char> password)
    {
        using ECDiffieHellman algorithm = ECDiffieHellman.Create();
        algorithm.ImportFromEncryptedPem(data, password);
        // ReSharper disable once HeapView.ObjectAllocation.Evident
        return new EcdhPrivateKey(algorithm);
    }

    /// <inheritdoc/>
    public static EcdhPrivateKey ImportPem(ReadOnlySpan<char> data, ReadOnlySpan<byte> password)
    {
        using ECDiffieHellman algorithm = ECDiffieHellman.Create();
        algorithm.ImportFromEncryptedPem(data, password);
        // ReSharper disable once HeapView.ObjectAllocation.Evident
        return new EcdhPrivateKey(algorithm);
    }
}
