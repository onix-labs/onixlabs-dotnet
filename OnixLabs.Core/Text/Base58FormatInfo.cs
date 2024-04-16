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
/// Represents Base-58 formatting information.
/// </summary>
public sealed class Base58FormatInfo : IFormatProvider
{
    /// <summary>
    /// The default Base-58 alphabet, which is the same as Bitcoin's Base-58 alphabet.
    /// </summary>
    public static Base58FormatInfo Default => new(nameof(Default), "123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz");

    /// <summary>
    /// The Ripple Base-58 alphabet.
    /// </summary>
    public static Base58FormatInfo Ripple => new(nameof(Ripple), "rpshnaf39wBUDNEGHJKLM4PQRST7VWXYZ2bcdeCg65jkm8oFqi1tuvAxyz");

    /// <summary>
    /// The Flickr Base-58 alphabet.
    /// </summary>
    public static Base58FormatInfo Flickr => new(nameof(Flickr), "123456789abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ");

    /// <summary>
    /// Initializes a new instance of the <see cref="Base58FormatInfo"/> class.
    /// </summary>
    /// <param name="name">The name of the Base-58 alphabet.</param>
    /// <param name="alphabet">The alphabet that will be used for Base-58 encoding and decoding operations.</param>
    private Base58FormatInfo(string name, string alphabet) => (Name, Alphabet) = (name, alphabet);

    /// <summary>
    /// Gets the name of the Base-58 alphabet.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the alphabet that will be used for Base-58 encoding and decoding operations.
    /// </summary>
    public string Alphabet { get; }

    /// <summary>Returns an object that provides formatting services for the specified type.</summary>
    /// <param name="formatType">An object that specifies the type of format object to return.</param>
    /// <returns>An instance of the object specified by <paramref name="formatType" />, if the <see cref="T:System.IFormatProvider" /> implementation can supply that type of object; otherwise, <see langword="null" />.</returns>
    public object? GetFormat(Type? formatType) => formatType == typeof(Base58FormatInfo) ? this : null;
}
