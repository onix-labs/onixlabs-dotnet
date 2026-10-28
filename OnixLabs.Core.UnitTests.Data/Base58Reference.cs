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

using System.Linq;
using System.Numerics;
using System.Text;

namespace OnixLabs.Core.UnitTests.Data;

/// <summary>
/// Provides a deliberately simple <see cref="BigInteger"/> Base58 encoder and decoder, used as an oracle for the library's codec.
/// </summary>
/// <remarks>
/// This is the library's original Base58 algorithm, kept here unchanged so that the faster replacement can be checked against it:
/// the input is treated as one big-endian integer and repeatedly divided by 58, and each leading zero byte maps to the alphabet's first
/// character. It shares no code with the library.
/// </remarks>
public static class Base58Reference
{
    /// <summary>
    /// Encodes the specified bytes as Base58 using the specified alphabet.
    /// </summary>
    /// <param name="value">The bytes to encode.</param>
    /// <param name="alphabet">The 58-character alphabet.</param>
    /// <returns>Returns the Base58 encoding of the specified bytes.</returns>
    public static string Encode(byte[] value, string alphabet)
    {
        BigInteger data = BigInteger.Zero;

        foreach (byte b in value)
            data = data * 256 + b;

        StringBuilder builder = new();

        while (data > 0)
        {
            BigInteger remainder = data % 58;
            data /= 58;
            builder.Insert(0, alphabet[(int)remainder]);
        }

        for (int index = 0; index < value.Length && value[index] == 0; index++)
            builder.Insert(0, alphabet[0]);

        return builder.ToString();
    }

    /// <summary>
    /// Decodes the specified Base58 text using the specified alphabet.
    /// </summary>
    /// <param name="value">The text to decode.</param>
    /// <param name="alphabet">The 58-character alphabet.</param>
    /// <returns>Returns the decoded bytes, or <see langword="null"/> if the text contains a character outside the alphabet.</returns>
    public static byte[]? Decode(string value, string alphabet)
    {
        BigInteger data = BigInteger.Zero;

        foreach (char character in value)
        {
            int index = alphabet.IndexOf(character);

            if (index < 0) return null;

            data = data * 58 + index;
        }

        int leadingZeroCount = value.TakeWhile(character => character == alphabet[0]).Count();
        byte[] magnitude = data.ToByteArray().AsEnumerable().Reverse().SkipWhile(b => b == 0).ToArray();

        return [..Enumerable.Repeat((byte)0, leadingZeroCount), ..magnitude];
    }
}
