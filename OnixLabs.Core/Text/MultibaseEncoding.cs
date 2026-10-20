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
/// Represents a Multibase encoding: a single prefix character that names the base encoding of the string that follows it.
/// </summary>
/// <remarks>
/// Only encodings with <i>final</i> status in the Multibase registry are supported; draft and experimental prefixes are rejected.
/// Each member's documentation gives its name in the Multibase specification. Decoding is strict: the prefix fixes the alphabet, the padding and
/// the case of the payload.
/// </remarks>
// ReSharper disable HeapView.ObjectAllocation.Evident
public sealed class MultibaseEncoding : Enumeration<MultibaseEncoding>
{
    /// <summary>
    /// The Bitcoin Base-58 encoding, with prefix <c>z</c> (Multibase name <c>base58btc</c>).
    /// </summary>
    public static readonly MultibaseEncoding Base58Bitcoin =
        new(0, nameof(Base58Bitcoin), 'z', Base58FormatProvider.Bitcoin);

    /// <summary>
    /// The lowercase Base-16 encoding, with prefix <c>f</c> (Multibase name <c>base16</c>).
    /// </summary>
    public static readonly MultibaseEncoding Base16Lowercase =
        new(1, nameof(Base16Lowercase), 'f', Base16FormatProvider.Lowercase);

    /// <summary>
    /// The uppercase Base-16 encoding, with prefix <c>F</c> (Multibase name <c>base16upper</c>).
    /// </summary>
    public static readonly MultibaseEncoding Base16Uppercase =
        new(2, nameof(Base16Uppercase), 'F', Base16FormatProvider.Uppercase);

    /// <summary>
    /// The unpadded, lowercase RFC 4648 Base-32 encoding, with prefix <c>b</c> (Multibase name <c>base32</c>).
    /// </summary>
    public static readonly MultibaseEncoding UnpaddedBase32Lowercase =
        new(3, nameof(UnpaddedBase32Lowercase), 'b', Base32FormatProvider.UnpaddedRfc4648Lowercase);

    /// <summary>
    /// The unpadded, uppercase RFC 4648 Base-32 encoding, with prefix <c>B</c> (Multibase name <c>base32upper</c>).
    /// </summary>
    public static readonly MultibaseEncoding UnpaddedBase32Uppercase =
        new(4, nameof(UnpaddedBase32Uppercase), 'B', Base32FormatProvider.UnpaddedRfc4648Uppercase);

    /// <summary>
    /// The unpadded RFC 4648 Base-64 encoding, with prefix <c>m</c> (Multibase name <c>base64</c>).
    /// </summary>
    public static readonly MultibaseEncoding UnpaddedBase64 =
        new(5, nameof(UnpaddedBase64), 'm', Base64FormatProvider.UnpaddedRfc4648);

    /// <summary>
    /// The unpadded RFC 4648 URL and filename safe Base-64 encoding, with prefix <c>u</c> (Multibase name <c>base64url</c>).
    /// </summary>
    public static readonly MultibaseEncoding UnpaddedBase64Url =
        new(6, nameof(UnpaddedBase64Url), 'u', Base64FormatProvider.UnpaddedRfc4648Url);

    /// <summary>
    /// The padded RFC 4648 URL and filename safe Base-64 encoding, with prefix <c>U</c> (Multibase name <c>base64urlpad</c>).
    /// </summary>
    public static readonly MultibaseEncoding PaddedBase64Url =
        new(7, nameof(PaddedBase64Url), 'U', Base64FormatProvider.PaddedRfc4648Url);

    /// <summary>
    /// Initializes a new instance of the <see cref="MultibaseEncoding"/> class.
    /// </summary>
    /// <param name="value">The value of the enumeration entry.</param>
    /// <param name="name">The name of the enumeration entry.</param>
    /// <param name="prefix">The prefix character that identifies the encoding.</param>
    /// <param name="provider">The format provider that encodes and decodes the payload.</param>
    private MultibaseEncoding(int value, string name, char prefix, IFormatProvider provider) : base(value, name) =>
        (Prefix, Provider) = (prefix, provider);

    /// <summary>
    /// Gets the prefix character that identifies the current <see cref="MultibaseEncoding"/>.
    /// </summary>
    /// <value>The prefix character that identifies the current encoding.</value>
    public char Prefix { get; }

    /// <summary>
    /// Gets the format provider that encodes and decodes the payload of the current <see cref="MultibaseEncoding"/>.
    /// </summary>
    /// <value>The format provider that encodes and decodes the payload of the current encoding.</value>
    internal IFormatProvider Provider { get; }
}
