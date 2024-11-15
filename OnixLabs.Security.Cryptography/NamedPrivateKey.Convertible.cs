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
using OnixLabs.Core.Text;

namespace OnixLabs.Security.Cryptography;

public readonly partial record struct NamedPrivateKey
{
    /// <summary>
    /// Gets the underlying <see cref="T:Byte[]"/> representation of the underlying <see cref="NamedPrivateKey"/> instance as a new <see cref="ReadOnlyMemory{T}"/> instance.
    /// <remarks>This method only obtains the bytes representing the private key value. The name of the private key will not be encoded into the resulting span.</remarks>
    /// </summary>
    /// <returns>Return the underlying <see cref="T:Byte[]"/> representation of the underlying <see cref="NamedPrivateKey"/> instance as a new <see cref="ReadOnlyMemory{T}"/> instance.</returns>
    public ReadOnlyMemory<byte> AsReadOnlyMemory() => PrivateKey.AsReadOnlyMemory();

    /// <summary>
    /// Gets the underlying <see cref="T:Byte[]"/> representation of the underlying <see cref="NamedPrivateKey"/> instance as a new <see cref="ReadOnlySpan{T}"/> instance.
    /// <remarks>This method only obtains the bytes representing the private key value. The name of the private key will not be encoded into the resulting span.</remarks>
    /// </summary>
    /// <returns>Return the underlying <see cref="T:Byte[]"/> representation of the underlying <see cref="NamedPrivateKey"/> instance as a new <see cref="ReadOnlySpan{T}"/> instance.</returns>
    public ReadOnlySpan<byte> AsReadOnlySpan() => PrivateKey.AsReadOnlySpan();
}
