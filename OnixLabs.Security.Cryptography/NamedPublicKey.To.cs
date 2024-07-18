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

public readonly partial record struct NamedPublicKey
{
    /// <summary>
    /// Gets the underlying <see cref="T:Byte[]"/> representation of the underlying <see cref="NamedPublicKey"/> instance.
    /// <remarks>This method only obtains the bytes representing the public key value. The name of the public key will not be encoded into the resulting byte array.</remarks>
    /// </summary>
    /// <returns>Return the underlying <see cref="T:Byte[]"/> representation of the underlying <see cref="NamedPublicKey"/> instance.</returns>
    public byte[] ToByteArray() => PublicKey.ToByteArray();

    /// <summary>
    /// Gets the underlying <see cref="T:Byte[]"/> representation of the current <see cref="PublicKey"/> instance as a new <see cref="ReadOnlySpan{T}"/> instance.
    /// <remarks>This method only obtains the bytes representing the public key value. The name of the public key will not be encoded into the resulting span.</remarks>
    /// </summary>
    /// <returns>Return the underlying <see cref="T:Byte[]"/> representation of the current <see cref="PublicKey"/> instance as a new <see cref="ReadOnlySpan{T}"/> instance.</returns>
    public ReadOnlySpan<byte> ToReadOnlySpan() => PublicKey.ToReadOnlySpan();

    /// <summary>
    /// Returns a <see cref="string"/> that represents the current object.
    /// </summary>
    /// <param name="provider">The format provider that will be used to determine the format of the string.</param>
    /// <returns>Returns a <see cref="string"/> that represents the current object.</returns>
    public string ToString(IFormatProvider provider) => string.Concat(AlgorithmName, Separator, IBaseCodec.GetString(ToByteArray(), provider));

    /// <summary>
    /// Returns a <see cref="string"/> that represents the current object.
    /// </summary>
    /// <returns>Returns a <see cref="string"/> that represents the current object.</returns>
    public override string ToString() => ToString(Base16FormatProvider.Invariant);
}
