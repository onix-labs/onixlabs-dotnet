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

public readonly partial struct Base32
{
    /// <summary>
    /// Parses a Base-32 value into a <see cref="Base32"/> instance.
    /// </summary>
    /// <param name="value">The Base-16 (hexadecimal) value to parse.</param>
    /// <returns>Returns a new <see cref="Base32"/> instance.</returns>
    public static Base32 Parse(ReadOnlySpan<char> value)
    {
        return Parse(value, Base32Alphabet.Default);
    }

    /// <summary>
    /// Parses a Base-32 value into a <see cref="Base32"/> instance.
    /// </summary>
    /// <param name="value">The Base-16 (hexadecimal) value to parse.</param>
    /// <param name="alphabet">The alphabet that will be used for Base-32 encoding and decoding operations.</param>
    /// <returns>Returns a new <see cref="Base32"/> instance.</returns>
    public static Base32 Parse(ReadOnlySpan<char> value, Base32Alphabet alphabet)
    {
        bool padding = value.Contains('=');
        byte[] bytes = Decode(value, alphabet.Alphabet, padding);
        return Create(bytes, padding);
    }
}
