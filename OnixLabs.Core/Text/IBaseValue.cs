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
using System.Buffers;
using System.Text;

namespace OnixLabs.Core.Text;

/// <summary>
/// Defines a Base-N value.
/// </summary>
public interface IBaseValue : IBinaryConvertible, ISpanFormattable
{
    /// <summary>
    /// Formats the value of the current instance using the specified format.
    /// </summary>
    /// <param name="formatProvider">The provider to use to format the value.</param>
    /// <returns>The value of the current instance in the specified format.</returns>
    string ToString(IFormatProvider? formatProvider);

    /// <summary>
    /// Formats the value of the current instance using the specified format.
    /// </summary>
    /// <param name="format">The format to use.</param>
    /// <param name="formatProvider">The provider to use to format the value.</param>
    /// <returns>The value of the current instance in the specified format.</returns>
    string ToString(ReadOnlySpan<char> format, IFormatProvider? formatProvider);
}

/// <summary>
/// Defines a generic base encoding representation.
/// </summary>
public interface IBaseValue<T> : IValueEquatable<T>, ISpanParsable<T>, IBaseValue where T : struct, IBaseValue<T>
{
    /// <summary>
    /// Implicitly converts the specified <see cref="ReadOnlySpan{T}"/> into a new <typeparamref name="T"/> instance.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns a new <typeparamref name="T"/> instance, converted from the specified <see cref="ReadOnlySpan{T}"/>.</returns>
    public static abstract implicit operator T(ReadOnlySpan<byte> value);

    /// <summary>
    /// Implicitly converts the specified <see cref="ReadOnlyMemory{T}"/> into a new <typeparamref name="T"/> instance.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns a new <typeparamref name="T"/> instance, converted from the specified <see cref="ReadOnlyMemory{T}"/>.</returns>
    public static abstract implicit operator T(ReadOnlyMemory<byte> value);

    /// <summary>
    /// Implicitly converts the specified <see cref="ReadOnlySequence{T}"/> into a new <typeparamref name="T"/> instance.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns a new <typeparamref name="T"/> instance, converted from the specified <see cref="ReadOnlySequence{T}"/>.</returns>
    public static abstract implicit operator T(ReadOnlySequence<byte> value);

    /// <summary>
    /// Implicitly converts the specified <see cref="ReadOnlySpan{T}"/> into a new <typeparamref name="T"/> instance using the default <see cref="Encoding"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns a new <typeparamref name="T"/> instance, converted from the specified <see cref="ReadOnlySpan{T}"/>.</returns>
    public static abstract implicit operator T(ReadOnlySpan<char> value);

    /// <summary>
    /// Implicitly converts the specified <see cref="ReadOnlyMemory{T}"/> into a new <typeparamref name="T"/> instance using the default <see cref="Encoding"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns a new <typeparamref name="T"/> instance, converted from the specified <see cref="ReadOnlyMemory{T}"/>.</returns>
    public static abstract implicit operator T(ReadOnlyMemory<char> value);

    /// <summary>
    /// Implicitly converts the specified <see cref="ReadOnlySequence{T}"/> into a new <typeparamref name="T"/> instance using the default <see cref="Encoding"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns a new <typeparamref name="T"/> instance, converted from the specified <see cref="ReadOnlySequence{T}"/>.</returns>
    public static abstract implicit operator T(ReadOnlySequence<char> value);
}
