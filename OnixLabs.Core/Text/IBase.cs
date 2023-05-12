// Copyright 2020-2023 ONIXLabs
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
using System.Text;
using OnixLabs.Core.Collections;

namespace OnixLabs.Core.Text;

/// <summary>
/// Represents a number base representation.
/// </summary>
/// <typeparam name="TSelf">The underlying type of the number base representation.</typeparam>
public interface IBase<TSelf> : IEquatable<TSelf> where TSelf : struct, IBase<TSelf>
{
    /// <summary>
    /// Gets an empty <see cref="IBase{TSelf}"/> value.
    /// </summary>
    public static virtual TSelf Empty => TSelf.Create(Collection.EmptyArray<byte>());

    /// <summary>
    /// Creates a new <see cref="IBase{TSelf}"/> value from the specified <see cref="T:byte[]"/> value.
    /// </summary>
    /// <param name="value">The value from which to create a new <see cref="IBase{TSelf}"/> value.</param>
    /// <returns>Returns a new <see cref="IBase{TSelf}"/> value from the specified <see cref="T:byte[]"/> value.</returns>
    public static abstract TSelf Create(byte[] value);

    /// <summary>
    /// Creates a new <see cref="IBase{TSelf}"/> value from the specified <see cref="ReadOnlySpan{T}"/> value.
    /// </summary>
    /// <param name="value">The value from which to create a new <see cref="IBase{TSelf}"/> value.</param>
    /// <returns>Returns a new <see cref="IBase{TSelf}"/> value from the specified <see cref="ReadOnlySpan{T}"/> value.</returns>
    public static virtual TSelf Create(ReadOnlySpan<char> value)
    {
        return TSelf.Create(value, Encoding.Default);
    }

    /// <summary>
    /// Creates a new <see cref="IBase{TSelf}"/> value from the specified <see cref="ReadOnlySpan{T}"/> value.
    /// </summary>
    /// <param name="value">The value from which to create a new <see cref="IBase{TSelf}"/> value.</param>
    /// <param name="encoding">The <see cref="Encoding"/> from which to obtain a <see cref="T:byte[]"/> value.</param>
    /// <returns>Returns a new <see cref="IBase{TSelf}"/> value from the specified <see cref="ReadOnlySpan{T}"/> value.</returns>
    public static virtual TSelf Create(ReadOnlySpan<char> value, Encoding encoding)
    {
        // TODO : Check if future versions support GetBytes with ReadOnlySpan<char> overload.
        byte[] bytes = encoding.GetBytes(value.ToArray());
        return TSelf.Create(bytes);
    }

    /// <summary>
    /// Parses a <see cref="IBase{TSelf}"/> value from the specified <see cref="ReadOnlySpan{T}"/> value.
    /// </summary>
    /// <param name="value">The value from which to parse a <see cref="IBase{TSelf}"/> value.</param>
    /// <returns>Returns a new <see cref="IBase{TSelf}"/> value parsed from the specified <see cref="ReadOnlySpan{T}"/> value.</returns>
    public static abstract TSelf Parse(ReadOnlySpan<char> value);

    /// <summary>
    /// Gets the current <see cref="IBase{TSelf}"/> as a <see cref="T:byte[]"/> value.
    /// </summary>
    /// <returns>Returns the current <see cref="IBase{TSelf}"/> as a <see cref="T:byte[]"/> value.</returns>
    byte[] ToByteArray();

    /// <summary>
    /// Gets the plain-text <see cref="string"/> representation of the current <see cref="IBase{TSelf}"/> value.
    /// </summary>
    /// <returns>Returns the plain-text <see cref="string"/> representation of the current <see cref="IBase{TSelf}"/> value.</returns>
    public string ToPlainTextString()
    {
        return ToPlainTextString(Encoding.Default);
    }

    /// <summary>
    /// Gets the plain-text <see cref="string"/> representation of the current <see cref="IBase{TSelf}"/> value.
    /// </summary>
    /// <param name="encoding">The <see cref="Encoding"/> from which to obtain a <see cref="string"/> value.</param>
    /// <returns>Returns the plain-text <see cref="string"/> representation of the current <see cref="IBase{TSelf}"/> value.</returns>
    public string ToPlainTextString(Encoding encoding)
    {
        byte[] bytes = ToByteArray();
        return encoding.GetString(bytes);
    }
}
