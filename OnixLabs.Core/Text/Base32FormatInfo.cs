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

namespace OnixLabs.Core.Text;

/// <summary>
/// Represents Base-32 formatting information.
/// </summary>
public sealed class Base32FormatInfo : IFormatProvider
{
    /// <summary>
    /// The default (RFC-4648) Base-32 alphabet.
    /// </summary>
    public static Base32FormatInfo Default => new(nameof(Default), "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567");

    /// <summary>
    /// The Z-Base-32 alphabet.
    /// </summary>
    public static Base32FormatInfo ZBase32 => new(nameof(ZBase32), "ybndrfg8ejkmcpqxot1uwisza345h769");

    /// <summary>
    /// The Geohash Base-32 alphabet.
    /// </summary>
    public static Base32FormatInfo GeoHash => new(nameof(GeoHash), "0123456789bcdefghjkmnpqrstuvwxyz");

    /// <summary>
    /// The Crockford Base-32 alphabet.
    /// </summary>
    public static Base32FormatInfo Crockford => new(nameof(Crockford), "0123456789ABCDEFGHJKMNPQRSTVWXYZ");

    /// <summary>
    /// The Base-32 Hex alphabet.
    /// </summary>
    public static Base32FormatInfo Base32Hex => new(nameof(Base32Hex), "0123456789ABCDEFGHIJKLMNOPQRSTUV");

    /// <summary>
    /// Initializes a new instance of the <see cref="Base32FormatInfo"/> class.
    /// </summary>
    /// <param name="name">The name of the Base-32 alphabet.</param>
    /// <param name="alphabet">The alphabet that will be used for Base-32 encoding and decoding operations.</param>
    private Base32FormatInfo(string name, string alphabet) => (Name, Alphabet) = (name, alphabet);

    /// <summary>
    /// Gets the name of the Base-32 alphabet.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the alphabet that will be used for Base-32 encoding and decoding operations.
    /// </summary>
    public string Alphabet { get; }

    /// <summary>Returns an object that provides formatting services for the specified type.</summary>
    /// <param name="formatType">An object that specifies the type of format object to return.</param>
    /// <returns>
    /// Returns an instance of the object specified by <paramref name="formatType" />,
    /// if the <see cref="T:System.IFormatProvider" /> implementation can supply that type of object; otherwise, <see langword="null" />.
    /// </returns>
    public object? GetFormat(Type? formatType) => formatType == typeof(Base32FormatInfo) ? this : null;
}
