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
using System.ComponentModel;
using OnixLabs.Core.Text;

namespace OnixLabs.Security.Cryptography;

public abstract partial class PublicKey
{
    public ReadOnlyMemory<byte> AsReadOnlyMemory() => KeyData;

    /// <summary>
    /// Gets the underlying <see cref="T:Byte[]"/> representation of the current <see cref="PublicKey"/> instance as a new <see cref="ReadOnlySpan{T}"/> instance.
    /// </summary>
    /// <returns>Return the underlying <see cref="T:Byte[]"/> representation of the current <see cref="PublicKey"/> instance as a new <see cref="ReadOnlySpan{T}"/> instance.</returns>
    public ReadOnlySpan<byte> AsReadOnlySpan() => KeyData;

    /// <summary>
    /// Creates a new <see cref="NamedPublicKey"/> from the current <see cref="PublicKey"/> instance.
    /// </summary>
    /// <param name="algorithmName">The name of the algorithm that was used to produce the associated public key.</param>
    /// <returns>Returns a new <see cref="NamedPublicKey"/> from the current <see cref="PublicKey"/> instance.</returns>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("Passing custom algorithm names can lead to named keys. Use the parameterless ToNamedPublicKey method instead.")]
    public NamedPublicKey ToNamedPublicKey(string algorithmName) => new(this, algorithmName);

    /// <summary>
    /// Creates a new <see cref="NamedPublicKey"/> from the current <see cref="PublicKey"/> instance.
    /// </summary>
    /// <returns>Returns a new <see cref="NamedPublicKey"/> from the current <see cref="PublicKey"/> instance.</returns>
    public abstract NamedPublicKey ToNamedPublicKey();

    /// <summary>
    /// Returns a <see cref="string"/> that represents the current object.
    /// </summary>
    /// <param name="provider">The format provider that will be used to determine the format of the string.</param>
    /// <returns>Returns a <see cref="string"/> that represents the current object.</returns>
    public string ToString(IFormatProvider provider) => IBaseCodec.GetString(AsReadOnlySpan(), provider);

    /// <summary>
    /// Returns a <see cref="string"/> that represents the current object.
    /// </summary>
    /// <returns>Returns a <see cref="string"/> that represents the current object.</returns>
    public override string ToString() => ToString(Base16FormatProvider.Invariant);
}
