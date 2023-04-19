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

namespace OnixLabs.Core.Numerics;

/// <summary>
/// Represents the details of a decimal expansion.
/// </summary>
public readonly partial struct DecimalExpansion
{
    /// <summary>
    /// Creates a new instance of the <see cref="DecimalExpansion"/> struct.
    /// </summary>
    /// <param name="type">The type of the decimal expansion.</param>
    /// <param name="integerLength">The integer length of the decimal expansion.</param>
    /// <param name="fractionLength">The fraction length of the decimal expansion.</param>
    private DecimalExpansion(DecimalExpansionType type, int integerLength, int fractionLength)
    {
        Type = type;
        IntegerLength = integerLength;
        FractionLength = fractionLength;
    }

    /// <summary>
    /// The type of the current decimal expansion.
    /// </summary>
    public DecimalExpansionType Type { get; }

    /// <summary>
    /// The integer length of the current decimal expansion.
    /// </summary>
    public int IntegerLength { get; }

    /// <summary>
    /// The fraction length of the current decimal expansion.
    /// </summary>
    public int FractionLength { get; }
}
