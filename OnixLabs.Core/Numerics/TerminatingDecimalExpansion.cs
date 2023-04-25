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
/// Represents terminating decimal expansion information.
/// </summary>
public sealed class TerminatingDecimalExpansion : DecimalExpansion
{
    /// <summary>
    /// Creates a new instance of the <see cref="TerminatingDecimalExpansion"/> class.
    /// </summary>
    /// <param name="integerLength">The length of the integer part of the decimal expansion.</param>
    /// <param name="fractionLength">The length of the fractional part of the decimal expansion.</param>
    internal TerminatingDecimalExpansion(int integerLength, int fractionLength)
    {
        IntegerLength = integerLength;
        FractionLength = fractionLength;
    }

    /// <summary>
    /// Gets the length of the integer part of the decimal expansion.
    /// </summary>
    public override int IntegerLength { get; }

    /// <summary>
    /// Gets the length of the fractional part of the decimal expansion.
    /// </summary>
    public int FractionLength { get; }

    /// <summary>
    /// Gets the truncating fraction length of the decimal expansion.
    /// For a <see cref="TerminatingDecimalExpansion"/> the truncating fraction length is calculated as the full, terminating fraction length.
    /// </summary>
    internal override int TruncatingFractionLength => FractionLength;
}
