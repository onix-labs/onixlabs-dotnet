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
/// Specifies the supported Base-58 alphabets.
/// </summary>
public sealed class Base58Alphabet : Enumeration<Base32Alphabet>
{
    /// <summary>
    /// The default Base-58 alphabet, which is the same as Bitcoin's Base-58 alphabet.
    /// </summary>
    public static Base58Alphabet Default => new(0, nameof(Default), "123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz");

    /// <summary>
    /// The Ripple Base-58 alphabet.
    /// </summary>
    public static Base58Alphabet Ripple => new(0, nameof(Ripple), "rpshnaf39wBUDNEGHJKLM4PQRST7VWXYZ2bcdeCg65jkm8oFqi1tuvAxyz");

    /// <summary>
    /// The Flickr Base-58 alphabet.
    /// </summary>
    public static Base58Alphabet Flickr => new(0, nameof(Flickr), "123456789abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ");

    /// <summary>
    /// Initializes a new instance of the <see cref="Base58Alphabet"/> class.
    /// </summary>
    /// <param name="value">The value of the enumeration entry.</param>
    /// <param name="name">The name of the enumeration entry.</param>
    /// <param name="alphabet">The alphabet that will be used for Base-58 encoding and decoding operations.</param>
    private Base58Alphabet(int value, string name, string alphabet) : base(value, name)
    {
        Alphabet = alphabet;
    }

    /// <summary>
    /// Gets the alphabet that will be used for Base-58 encoding and decoding operations.
    /// </summary>
    public string Alphabet { get; }
}
