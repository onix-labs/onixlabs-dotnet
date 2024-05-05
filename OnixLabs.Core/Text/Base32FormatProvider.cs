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
public sealed class Base32FormatProvider : Enumeration<Base32FormatProvider>, IFormatProvider
{
    /// <summary>
    /// Gets the RFC 4648 Base-32 format provider.
    /// </summary>
    public static readonly Base32FormatProvider Rfc4648 = new(0, nameof(Rfc4648), "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567", false);

    /// <summary>
    /// Gets the Z-Base-32 format provider.
    /// </summary>
    public static readonly Base32FormatProvider ZBase32 = new(1, nameof(ZBase32), "ybndrfg8ejkmcpqxot1uwisza345h769", false);

    /// <summary>
    /// Gets the GeoHash Base-32 format provider.
    /// </summary>
    public static readonly Base32FormatProvider GeoHash = new(2, nameof(GeoHash), "0123456789bcdefghjkmnpqrstuvwxyz", false);

    /// <summary>
    /// Gets the Crockford Base-32 format provider.
    /// </summary>
    public static readonly Base32FormatProvider Crockford = new(3, nameof(Crockford), "0123456789ABCDEFGHJKMNPQRSTVWXYZ", false);

    /// <summary>
    /// Gets the Base-32 Hex format provider.
    /// </summary>
    public static readonly Base32FormatProvider Base32Hex = new(4, nameof(Base32Hex), "0123456789ABCDEFGHIJKLMNOPQRSTUV", false);

    /// <summary>
    /// Gets the RFC 4648 Base-32 format provider.
    /// </summary>
    public static readonly Base32FormatProvider PaddedRfc4648 = new(5, nameof(PaddedRfc4648), "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567", true);

    /// <summary>
    /// Gets the Z-Base-32 format provider.
    /// </summary>
    public static readonly Base32FormatProvider PaddedZBase32 = new(6, nameof(PaddedZBase32), "ybndrfg8ejkmcpqxot1uwisza345h769", true);

    /// <summary>
    /// Gets the GeoHash Base-32 format provider.
    /// </summary>
    public static readonly Base32FormatProvider PaddedGeoHash = new(7, nameof(PaddedGeoHash), "0123456789bcdefghjkmnpqrstuvwxyz", true);

    /// <summary>
    /// Gets the Crockford Base-32 format provider.
    /// </summary>
    public static readonly Base32FormatProvider PaddedCrockford = new(8, nameof(PaddedCrockford), "0123456789ABCDEFGHJKMNPQRSTVWXYZ", true);

    /// <summary>
    /// Gets the Base-32 Hex format provider.
    /// </summary>
    public static readonly Base32FormatProvider PaddedBase32Hex = new(9, nameof(PaddedBase32Hex), "0123456789ABCDEFGHIJKLMNOPQRSTUV", true);

    /// <summary>
    /// Initializes a new instance of the <see cref="Base32FormatProvider"/> class.
    /// </summary>
    /// <param name="value">The value of the enumeration entry.</param>
    /// <param name="name">The name of the enumeration entry.</param>
    /// <param name="alphabet">The alphabet of the format provider.</param>
    /// <param name="isPadded">A value indicating whether the format provider uses padding.</param>
    private Base32FormatProvider(int value, string name, string alphabet, bool isPadded) : base(value, name)
    {
        Alphabet = alphabet;
        IsPadded = isPadded;
    }

    /// <summary>
    /// Gets the alphabet of the current <see cref="Base32FormatProvider"/> instance.
    /// </summary>
    public string Alphabet { get; }

    /// <summary>
    /// Gets a value indicating whether the current <see cref="Base32FormatProvider"/> instance uses padding.
    /// </summary>
    public bool IsPadded { get; }

    /// <summary>Gets an object that provides formatting services for the specified type.</summary>
    /// <param name="formatType">An object that specifies the type of format object to return.</param>
    /// <returns>
    /// Returns an instance of the object specified by <paramref name="formatType"/>,
    /// if the <see cref="T:IFormatProvider"/> implementation can supply that type of object; otherwise, <see langword="null"/>.
    /// </returns>
    public object? GetFormat(Type? formatType)
    {
        return formatType == typeof(Base32FormatProvider) ? this : null;
    }
}
