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

/// <summary>
/// Represents a Base-64 format provider.
/// </summary>
/// <remarks>
/// Decoding is strict for every provider: missing or unexpected padding, whitespace, characters outside the alphabet,
/// impossible lengths, and non-zero trailing bits are rejected, so that every byte sequence has exactly one valid encoding.
/// </remarks>
// ReSharper disable HeapView.ObjectAllocation.Evident StringLiteralTypo
public sealed class Base64FormatProvider : Enumeration<Base64FormatProvider>, IFormatProvider
{
    /// <summary>
    /// The padded RFC 4648 §4 Base-64 format provider.
    /// </summary>
    public static readonly Base64FormatProvider PaddedRfc4648 = new(0, nameof(PaddedRfc4648), "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/", true);

    /// <summary>
    /// The unpadded RFC 4648 §4 Base-64 format provider.
    /// </summary>
    public static readonly Base64FormatProvider UnpaddedRfc4648 = new(1, nameof(UnpaddedRfc4648), "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/", false);

    /// <summary>
    /// The padded RFC 4648 §5 URL and filename safe Base-64 format provider.
    /// </summary>
    public static readonly Base64FormatProvider PaddedRfc4648Url = new(2, nameof(PaddedRfc4648Url), "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_", true);

    /// <summary>
    /// The unpadded RFC 4648 §5 URL and filename safe Base-64 format provider.
    /// </summary>
    public static readonly Base64FormatProvider UnpaddedRfc4648Url = new(3, nameof(UnpaddedRfc4648Url), "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_", false);

    /// <summary>
    /// Initializes a new instance of the <see cref="Base64FormatProvider"/> class.
    /// </summary>
    /// <param name="value">The value of the enumeration entry.</param>
    /// <param name="name">The name of the enumeration entry.</param>
    /// <param name="alphabet">The alphabet of the format provider.</param>
    /// <param name="isPadded">A value indicating whether the format provider uses padding.</param>
    private Base64FormatProvider(int value, string name, string alphabet, bool isPadded) : base(value, name) => (Alphabet, IsPadded) = (alphabet, isPadded);

    /// <summary>
    /// Gets the alphabet of the current <see cref="Base64FormatProvider"/> instance.
    /// </summary>
    /// <value>The Base-64 alphabet associated with the current format provider.</value>
    public string Alphabet { get; }

    /// <summary>
    /// Gets a value indicating whether the current <see cref="Base64FormatProvider"/> instance uses padding.
    /// </summary>
    /// <value><see langword="true"/> if the current format provider uses padding; otherwise, <see langword="false"/>.</value>
    public bool IsPadded { get; }

    /// <inheritdoc/>
    public object? GetFormat(Type? formatType) => formatType == typeof(Base64FormatProvider) ? this : null;
}
