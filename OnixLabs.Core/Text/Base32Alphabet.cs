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

namespace OnixLabs.Core.Text;

/// <summary>
/// Specifies the supported Base-32 alphabets.
/// </summary>
public sealed class Base32Alphabet : Enumeration<Base32Alphabet>
{
    /// <summary>
    /// The default (RFC-4648) Base-32 alphabet.
    /// </summary>
    public static Base32Alphabet Default => new(0, nameof(Default), "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567");

    /// <summary>
    /// The Z-Base-32 alphabet.
    /// </summary>
    public static Base32Alphabet ZBase32 => new(1, nameof(ZBase32), "ybndrfg8ejkmcpqxot1uwisza345h769");

    /// <summary>
    /// The Geohash Base-32 alphabet.
    /// </summary>
    public static Base32Alphabet GeoHash => new(2, nameof(GeoHash), "0123456789bcdefghjkmnpqrstuvwxyz");

    /// <summary>
    /// The Crockford Base-32 alphabet.
    /// </summary>
    public static Base32Alphabet Crockford => new(3, nameof(Crockford), "0123456789ABCDEFGHJKMNPQRSTVWXYZ");

    /// <summary>
    /// The Base-32 Hex alphabet.
    /// </summary>
    public static Base32Alphabet Base32Hex => new(4, nameof(Base32Hex), "0123456789ABCDEFGHIJKLMNOPQRSTUV");

    /// <summary>
    /// Initializes a new instance of the <see cref="Base32Alphabet"/> class.
    /// </summary>
    /// <param name="value">The value of the enumeration entry.</param>
    /// <param name="name">The name of the enumeration entry.</param>
    /// <param name="alphabet">The alphabet that will be used for Base-32 encoding and decoding operations.</param>
    private Base32Alphabet(int value, string name, string alphabet) : base(value, name)
    {
        Alphabet = alphabet;
    }

    /// <summary>
    /// Gets the alphabet that will be used for Base-32 encoding and decoding operations.
    /// </summary>
    public string Alphabet { get; }
}
