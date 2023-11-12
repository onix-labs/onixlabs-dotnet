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

namespace OnixLabs.Core.Units;

public readonly partial struct DataSize<T>
{
    /// <summary>
    /// Represents a zero-bit <see cref="DataSize{T}"/> value.
    /// </summary>
    public static DataSize<T> Zero => default;

    /// <summary>
    /// Gets the number of bits per nibble.
    /// </summary>
    private static T BitsPerNibble => T.CreateChecked(4);

    /// <summary>
    /// Gets the number of bits per byte.
    /// </summary>
    private static T BitsPerByte => T.CreateChecked(8);

    /// <summary>
    /// Gets the number of bits per word.
    /// </summary>
    private static T BitsPerWord => T.CreateChecked(16);

    /// <summary>
    /// Gets the number of bits per double-word.
    /// </summary>
    private static T BitsPerDoubleWord => T.CreateChecked(32);

    /// <summary>
    /// Gets the number of bits per quad-word.
    /// </summary>
    private static T BitsPerQuadWord => T.CreateChecked(64);

    /// <summary>
    /// Gets the value of a binary thousand.
    /// </summary>
    private static T BinaryThousand => T.CreateChecked(1024);

    /// <summary>
    /// Gets the value of a decimal thousand.
    /// </summary>
    private static T DecimalThousand => T.CreateChecked(1000);
    
    /// <summary>
    /// Gets the default format.
    /// </summary>
    private const string DefaultFormat = "b";
}
