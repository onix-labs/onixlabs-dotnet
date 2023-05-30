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
    /// Computes the power of the specified <see cref="BigDecimal"/> value, raised to the power of the specified exponent.
    /// </summary>
    /// <param name="value">The <see cref="BigDecimal"/> value to raise.</param>
    /// <param name="exponent">The exponent to raise by.</param>
    /// <returns>Returns the power of the specified <see cref="BigDecimal"/> value, raised to the power of the specified exponent.</returns>
    public static BigDecimal Pow(BigDecimal value, int exponent)
    {
        if (exponent == 0) return One;
        if (exponent == 1) return value;

        BigDecimal result = value;
        int absExponent = int.Abs(exponent);

        while (--absExponent > 0)
        {
            result *= value;
        }

        return exponent < 0 ? One / result : result;
    }
}
