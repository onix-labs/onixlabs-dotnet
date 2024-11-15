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
using System.Collections.Generic;
using System.Reflection;
using Xunit.Sdk;

namespace OnixLabs.Numerics.UnitTests.Data;

public sealed class BigDecimalArithmeticScaleDataAttribute : DataAttribute
{
    private static readonly int[] Integers = [0, 1, 2, 3, 123, 123456789, -1, -2, -3, -123, -123456789];

    private static readonly int[] IntegerScales = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15];

    private static readonly decimal[] Decimals =
    [
        +0.1234567890987654321000000000m,
        +1.1234567890987654321000000000m,
        -0.1234567890987654321000000000m,
        -1.1234567890987654321000000000m
    ];

    private static readonly int[] DecimalScales =
    [
        0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27
    ];

    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        foreach (int value in Integers)
        foreach (int initialScale in IntegerScales)
        foreach (MidpointRounding rounding in Enum.GetValues<MidpointRounding>())
        foreach (int desiredScale in IntegerScales)
        {
            decimal initial = value.ToDecimal(initialScale, ScaleMode.Integral);
            decimal expected = value.ToDecimal(desiredScale, ScaleMode.Integral);
            yield return [initial, desiredScale, rounding, expected];
        }

        foreach (decimal value in Decimals)
        foreach (int scale in DecimalScales)
        foreach (MidpointRounding rounding in Enum.GetValues<MidpointRounding>())
        {
            decimal expected = decimal.Round(value, scale, rounding);
            yield return [value, scale, rounding, expected];
        }
    }
}
