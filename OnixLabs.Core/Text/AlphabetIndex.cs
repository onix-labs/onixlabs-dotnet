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
/// Represents a reverse lookup from an ASCII alphabet's characters to their positions in the alphabet.
/// </summary>
/// <remarks>
/// Why: decoding looks up every input character. A 128-entry table answers in one array read where <see cref="string.IndexOf(char)"/>
/// scans the alphabet. Lookups are ordinal and case-sensitive, and the first occurrence of a repeated character wins, exactly as with
/// <see cref="string.IndexOf(char)"/>.
/// </remarks>
internal sealed class AlphabetIndex
{
    private const int AsciiLength = 128;

    private readonly sbyte[] indexes = new sbyte[AsciiLength];

    /// <summary>
    /// Initializes a new instance of the <see cref="AlphabetIndex"/> class.
    /// </summary>
    /// <param name="alphabet">The alphabet to index, which must contain only ASCII characters and at most 127 of them.</param>
    public AlphabetIndex(string alphabet)
    {
        indexes.AsSpan().Fill(-1);

        for (int index = alphabet.Length - 1; index >= 0; index--)
            indexes[alphabet[index]] = (sbyte)index;
    }

    /// <summary>
    /// Gets the position of the specified character in the alphabet.
    /// </summary>
    /// <param name="character">The character to look up.</param>
    /// <returns>Returns the zero-based position of <paramref name="character"/> in the alphabet, or -1 if it is not in the alphabet.</returns>
    public int IndexOf(char character) => character < AsciiLength ? indexes[character] : -1;
}
