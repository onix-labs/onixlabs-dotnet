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
/// Represents a Base-32 format provider.
/// </summary>
/// <remarks>
/// Decoding is strict for every provider: missing or unexpected padding, characters outside the alphabet,
/// impossible lengths, and non-zero trailing bits are rejected, so that every byte sequence has exactly one valid encoding.
/// The Crockford providers are the exception: as their specification requires, decoding accepts either case, reads I and L as 1 and O as 0, and ignores hyphens.
/// </remarks>
// ReSharper disable HeapView.ObjectAllocation.Evident
public sealed class Base32FormatProvider : Enumeration<Base32FormatProvider>, IFormatProvider
{
    /// <summary>
    /// The unpadded RFC 4648 Base-32 format provider.
    /// </summary>
    public static readonly Base32FormatProvider UnpaddedRfc4648 = new(0, nameof(UnpaddedRfc4648), "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567", false);

    /// <summary>
    /// The unpadded Z-Base-32 format provider.
    /// </summary>
    public static readonly Base32FormatProvider UnpaddedZBase32 = new(1, nameof(UnpaddedZBase32), "ybndrfg8ejkmcpqxot1uwisza345h769", false);

    /// <summary>
    /// The unpadded GeoHash Base-32 format provider.
    /// </summary>
    public static readonly Base32FormatProvider UnpaddedGeoHash = new(2, nameof(UnpaddedGeoHash), "0123456789bcdefghjkmnpqrstuvwxyz", false);

    /// <summary>
    /// The unpadded Crockford Base-32 format provider.
    /// </summary>
    /// <remarks>
    /// Encoding produces uppercase. Decoding accepts either case, reads I and L as 1 and O as 0, and ignores hyphens. Check symbols are not supported.
    /// </remarks>
    public static readonly Base32FormatProvider UnpaddedCrockford = new(3, nameof(UnpaddedCrockford), "0123456789ABCDEFGHJKMNPQRSTVWXYZ", false);

    /// <summary>
    /// The unpadded Base-32 Hex format provider.
    /// </summary>
    public static readonly Base32FormatProvider UnpaddedBase32Hex = new(4, nameof(UnpaddedBase32Hex), "0123456789ABCDEFGHIJKLMNOPQRSTUV", false);

    /// <summary>
    /// The padded RFC 4648 Base-32 format provider.
    /// </summary>
    public static readonly Base32FormatProvider PaddedRfc4648 = new(5, nameof(PaddedRfc4648), "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567", true);

    /// <summary>
    /// The padded Z-Base-32 format provider.
    /// </summary>
    public static readonly Base32FormatProvider PaddedZBase32 = new(6, nameof(PaddedZBase32), "ybndrfg8ejkmcpqxot1uwisza345h769", true);

    /// <summary>
    /// The padded GeoHash Base-32 format provider.
    /// </summary>
    public static readonly Base32FormatProvider PaddedGeoHash = new(7, nameof(PaddedGeoHash), "0123456789bcdefghjkmnpqrstuvwxyz", true);

    /// <summary>
    /// The padded Crockford Base-32 format provider.
    /// </summary>
    /// <remarks>
    /// Encoding produces uppercase. Decoding accepts either case, reads I and L as 1 and O as 0, and ignores hyphens. Check symbols are not supported.
    /// </remarks>
    public static readonly Base32FormatProvider PaddedCrockford = new(8, nameof(PaddedCrockford), "0123456789ABCDEFGHJKMNPQRSTVWXYZ", true);

    /// <summary>
    /// The padded Base-32 Hex format provider.
    /// </summary>
    public static readonly Base32FormatProvider PaddedBase32Hex = new(9, nameof(PaddedBase32Hex), "0123456789ABCDEFGHIJKLMNOPQRSTUV", true);

    /// <summary>
    /// Initializes a new instance of the <see cref="Base32FormatProvider"/> class.
    /// </summary>
    /// <param name="value">The value of the enumeration entry.</param>
    /// <param name="name">The name of the enumeration entry.</param>
    /// <param name="alphabet">The alphabet of the format provider.</param>
    /// <param name="isPadded">A value indicating whether the format provider uses padding.</param>
    private Base32FormatProvider(int value, string name, string alphabet, bool isPadded) : base(value, name) => (Alphabet, IsPadded) = (alphabet, isPadded);

    /// <summary>
    /// Gets the alphabet of the current <see cref="Base32FormatProvider"/> instance.
    /// </summary>
    /// <value>The Base-32 alphabet associated with the current format provider.</value>
    public string Alphabet { get; }

    /// <summary>
    /// Gets a value indicating whether the current <see cref="Base32FormatProvider"/> instance uses padding.
    /// </summary>
    /// <value><see langword="true"/> if the current format provider uses padding; otherwise, <see langword="false"/>.</value>
    public bool IsPadded { get; }

    /// <inheritdoc/>
    public object? GetFormat(Type? formatType) => formatType == typeof(Base32FormatProvider) ? this : null;
}
