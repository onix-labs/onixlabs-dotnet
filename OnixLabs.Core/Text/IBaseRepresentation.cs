// Copyright © 2020 ONIXLabs
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

namespace OnixLabs.Core.Text;

/// <summary>
/// Defines a number base representation.
/// </summary>
/// <typeparam name="TSelf">The underlying type of the number base representation.</typeparam>
public interface IBaseRepresentation<TSelf> :
    IEquatable<TSelf>,
    ISpanParsable<TSelf>,
    ISpanFormattable
    where TSelf : struct, IBaseRepresentation<TSelf>
{
    /// <summary>
    /// Gets an empty <see typeparam="TSelf"/> value.
    /// </summary>
    public static abstract TSelf Empty { get; }

    /// <summary>
    /// Creates a new <see typeparam="TSelf"/> value from the specified <see cref="T:byte[]"/> value.
    /// </summary>
    /// <param name="value">The <see cref="T:byte[]"/> value from which to create a new <see typeparam="TSelf"/> value.</param>
    /// <returns>Returns a new <see typeparam="TSelf"/> value from the specified <see cref="T:byte[]"/> value.</returns>
    public static abstract TSelf Create(byte[] value);

    /// <summary>
    /// Creates a new <see typeparam="TSelf"/> value from the specified <see cref="ReadOnlySpan{T}"/> value.
    /// </summary>
    /// <param name="value">The <see cref="ReadOnlySpan{T}"/> value from which to create a new <see typeparam="TSelf"/> value.</param>
    /// <returns>Returns a new <see typeparam="TSelf"/> value from the specified <see cref="ReadOnlySpan{T}"/> value.</returns>
    public static abstract TSelf Create(ReadOnlySpan<byte> value);

    /// <summary>
    /// Creates a new <see typeparam="TSelf"/> value from the specified <see cref="string"/> value, using the default <see cref="Encoding"/>.
    /// </summary>
    /// <param name="value">The <see cref="string"/> value from which to create a new <see typeparam="TSelf"/> value.</param>
    /// <returns>Returns a new <see typeparam="TSelf"/> value from the specified <see cref="string"/> value.</returns>
    public static abstract TSelf Create(string value);

    /// <summary>
    /// Creates a new <see typeparam="TSelf"/> value from the specified <see cref="string"/> value.
    /// </summary>
    /// <param name="value">The <see cref="string"/> value from which to create a new <see typeparam="TSelf"/> value.</param>
    /// <param name="encoding">The <see cref="Encoding"/> which will be used to obtain a <see cref="T:byte[]"/> from the specified <see cref="string"/> value.</param>
    /// <returns>Returns a new <see typeparam="TSelf"/> value from the specified <see cref="string"/> value.</returns>
    public static abstract TSelf Create(string value, Encoding encoding);

    /// <summary>
    /// Creates a new <see typeparam="TSelf"/> value from the specified <see cref="T:char[]"/> value, using the default <see cref="Encoding"/>.
    /// </summary>
    /// <param name="value">The <see cref="T:char[]"/> value from which to create a new <see typeparam="TSelf"/> value.</param>
    /// <returns>Returns a new <see typeparam="TSelf"/> value from the specified <see cref="T:char[]"/> value.</returns>
    public static abstract TSelf Create(char[] value);

    /// <summary>
    /// Creates a new <see typeparam="TSelf"/> value from the specified <see cref="T:char[]"/> value.
    /// </summary>
    /// <param name="value">The <see cref="T:char[]"/> value from which to create a new <see typeparam="TSelf"/> value.</param>
    /// <param name="encoding">The <see cref="Encoding"/> which will be used to obtain a <see cref="T:byte[]"/> from the specified <see cref="T:char[]"/> value.</param>
    /// <returns>Returns a new <see typeparam="TSelf"/> value from the specified <see cref="T:char[]"/> value.</returns>
    public static abstract TSelf Create(char[] value, Encoding encoding);

    /// <summary>
    /// Creates a new <see typeparam="TSelf"/> value from the specified <see cref="ReadOnlySpan{T}"/> value, using the default <see cref="Encoding"/>.
    /// </summary>
    /// <param name="value">The <see cref="ReadOnlySpan{T}"/> value from which to create a new <see typeparam="TSelf"/> value.</param>
    /// <returns>Returns a new <see typeparam="TSelf"/> value from the specified <see cref="ReadOnlySpan{T}"/> value.</returns>
    public static abstract TSelf Create(ReadOnlySpan<char> value);

    /// <summary>
    /// Creates a new <see typeparam="TSelf"/> value from the specified <see cref="ReadOnlySpan{T}"/> value.
    /// </summary>
    /// <param name="value">The <see cref="ReadOnlySpan{T}"/> value from which to create a new <see typeparam="TSelf"/> value.</param>
    /// <param name="encoding">The <see cref="Encoding"/> which will be used to obtain a <see cref="T:byte[]"/> from the specified <see cref="ReadOnlySpan{T}"/> value.</param>
    /// <returns>Returns a new <see typeparam="TSelf"/> value from the specified <see cref="ReadOnlySpan{T}"/> value.</returns>
    public static abstract TSelf Create(ReadOnlySpan<char> value, Encoding encoding);

    /// <summary>
    /// Performs an equality check between two <see typeparam="TSelf"/> instances.
    /// </summary>
    /// <param name="left">The left-hand <see typeparam="TSelf"/> instance to compare.</param>
    /// <param name="right">The right-hand <see typeparam="TSelf"/> instance to compare.</param>
    /// <returns>Returns <see langword="true"/> if the instances are equal; otherwise, <see langword="false"/>.</returns>
    public static abstract bool operator ==(TSelf left, TSelf right);

    /// <summary>
    /// Performs an inequality check between two <see typeparam="TSelf"/> instances.
    /// </summary>
    /// <param name="left">The left-hand <see typeparam="TSelf"/> instance to compare.</param>
    /// <param name="right">The right-hand <see typeparam="TSelf"/> instance to compare.</param>
    /// <returns>Returns <see langword="true"/> if the instances are not equal; otherwise, <see langword="false"/>.</returns>
    public static abstract bool operator !=(TSelf left, TSelf right);

    /// <summary>
    /// Gets the underlying <see cref="T:byte[]"/> represented by the current <see typeparam="TSelf"/> value.
    /// </summary>
    /// <returns>Returns the underlying <see cref="T:byte[]"/> represented by the current <see typeparam="TSelf"/> value.</returns>
    public byte[] ToByteArray();

    /// <summary>
    /// Gets the plain-text <see cref="string"/> representation of the current <see typeparam="TSelf"/> value, using the default <see cref="Encoding"/>.
    /// </summary>
    /// <returns>Returns the plain-text <see cref="string"/> representation of the current <see typeparam="TSelf"/> value, using the default <see cref="Encoding"/>.</returns>
    public string ToPlainTextString();

    /// <summary>
    /// Gets the plain-text <see cref="string"/> representation of the current <see typeparam="TSelf"/> value.
    /// </summary>
    /// <param name="encoding">The <see cref="Encoding"/> which will be used to obtain a <see cref="string"/> from the current <see typeparam="TSelf"/> value.</param>
    /// <returns>Returns the plain-text <see cref="string"/> representation of the current <see typeparam="TSelf"/> value.</returns>
    public string ToPlainTextString(Encoding encoding);

    /// <summary>
    /// Formats the value of the current instance using the specified format.
    /// </summary>
    /// <param name="format">The format to use.</param>
    /// <param name="formatProvider">The provider to use to format the value.</param>
    /// <returns>The value of the current instance in the specified format.</returns>
    public string ToString(ReadOnlySpan<char> format, IFormatProvider? formatProvider);
}
