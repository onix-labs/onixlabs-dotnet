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
/// Represents repeating decimal expansion information.
/// </summary>
public sealed class RepeatingDecimalExpansion : DecimalExpansion
{
    /// <summary>
    /// Creates a new instance of the <see cref="RepeatingDecimalExpansion"/> class.
    /// </summary>
    /// <param name="integerLength">The length of the integer part of the decimal expansion.</param>
    /// <param name="repetendLength">The length of the repetend part of the decimal expansion.</param>
    /// <param name="repetendOffset">The length of the repetend offset part of the decimal expansion.</param>
    internal RepeatingDecimalExpansion(int integerLength, int repetendLength, int repetendOffset)
    {
        IntegerLength = integerLength;
        RepetendLength = repetendLength;
        RepetendOffset = repetendOffset;
    }

    /// <summary>
    /// Gets the length of the integer part of the decimal expansion.
    /// </summary>
    public override int IntegerLength { get; }

    /// <summary>
    /// Gets the length of the repetend part of the decimal expansion.
    /// </summary>
    public int RepetendLength { get; }

    /// <summary>
    /// Gets the length of the repetend offset part of the decimal expansion.
    /// </summary>
    public int RepetendOffset { get; }

    /// <summary>
    /// Gets the truncating fraction length of the decimal expansion.
    /// For a <see cref="RepeatingDecimalExpansion"/> the truncating fraction length is calculated as the repetend offset, plus the repetend offset.
    /// </summary>
    internal override int TruncatingFractionLength => RepetendOffset + RepetendLength;
}
