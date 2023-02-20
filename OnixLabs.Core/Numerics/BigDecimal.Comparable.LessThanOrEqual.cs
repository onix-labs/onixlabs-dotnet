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

public readonly partial struct BigDecimal
{
    /// <summary>
    /// Performs a less than or equal to comparison check between two instances of <see cref="BigDecimal"/>.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>Returns true if the left-hand operand is less than or equal to the right-hand operand; otherwise, false.</returns>
    public static bool IsLessThanOrEqual(BigDecimal left, BigDecimal right)
    {
        return Compare(left, right) is -1 or 0;
    }

    /// <summary>
    /// Performs a less than or equal to comparison check between two instances of <see cref="BigDecimal"/>.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>Returns true if the left-hand operand is less than or equal to the right-hand operand; otherwise, false.</returns>
    public static bool operator <=(BigDecimal left, BigDecimal right)
    {
        return IsLessThanOrEqual(left, right);
    }
}
