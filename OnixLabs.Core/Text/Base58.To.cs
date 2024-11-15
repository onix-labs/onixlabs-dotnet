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

namespace OnixLabs.Core.Text;

public readonly partial struct Base58
{
    public ReadOnlyMemory<byte> AsReadOnlyMemory() => value;

    /// <summary>
    /// Gets the underlying <see cref="T:Byte[]"/> representation of the current <see cref="Base58"/> instance as a new <see cref="ReadOnlySpan{T}"/> instance.
    /// </summary>
    /// <returns>Return the underlying <see cref="T:Byte[]"/> representation of the current <see cref="Base58"/> instance as a new <see cref="ReadOnlySpan{T}"/> instance.</returns>
    public ReadOnlySpan<byte> AsReadOnlySpan() => value;

    /// <summary>
    /// Returns a <see cref="String"/> that represents the current object.
    /// </summary>
    /// <returns>Returns a <see cref="String"/> that represents the current object.</returns>
    public override string ToString() => ToString(Base58FormatProvider.Bitcoin);

    /// <summary>
    /// Formats the value of the current instance using the specified format.
    /// </summary>
    /// <param name="formatProvider">The provider to use to format the value.</param>
    /// <returns>The value of the current instance in the specified format.</returns>
    public string ToString(IFormatProvider? formatProvider) => ToString(null, formatProvider);

    /// <summary>
    /// Formats the value of the current instance using the specified format.
    /// </summary>
    /// <param name="format">The format to use.</param>
    /// <param name="formatProvider">The provider to use to format the value.</param>
    /// <returns>The value of the current instance in the specified format.</returns>
    public string ToString(string? format, IFormatProvider? formatProvider) => ToString(format.AsSpan(), formatProvider);

    /// <summary>
    /// Formats the value of the current instance using the specified format.
    /// </summary>
    /// <param name="format">The format to use.</param>
    /// <param name="formatProvider">The provider to use to format the value.</param>
    /// <returns>The value of the current instance in the specified format.</returns>
    public string ToString(ReadOnlySpan<char> format, IFormatProvider? formatProvider) => IBaseCodec.Base58.Encode(value, formatProvider);
}
