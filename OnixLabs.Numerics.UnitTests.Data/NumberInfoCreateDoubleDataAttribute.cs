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

using System.Numerics;
using System.Reflection;
using Xunit.Sdk;

namespace OnixLabs.Numerics.UnitTests.Data;

public sealed class NumberInfoCreateDoubleDataAttribute : DataAttribute
{
    private static readonly (double, BigInteger, int, int, int, BigInteger, int)[] Data =
    [
        (2.718281828459045, 2718281828459045, -15, 16, 1, 2718281828459045, 15),
        (3.141592653589793, 3141592653589793, -15, 16, 1, 3141592653589793, 15),
        (6.283185307179586, 6283185307179586, -15, 16, 1, 6283185307179586, 15),
        (1E-01, 1, -1, 2, 1, 1, 1),
        (1E-02, 1, -2, 3, 1, 1, 2),
        (1E-03, 1, -3, 4, 1, 1, 3),
        (1E-04, 1, -4, 5, 1, 1, 4),
        (1E-05, 1, -5, 6, 1, 1, 5),
        (1E-06, 1, -6, 7, 1, 1, 6),
        (1E-07, 1, -7, 8, 1, 1, 7),
        (1E-08, 1, -8, 9, 1, 1, 8),
        (1E-09, 1, -9, 10, 1, 1, 9),
        (1E-10, 1, -10, 11, 1, 1, 10),
        (12345678.9, 123456789, -1, 9, 1, 123456789, 1),
        (1234567.89, 123456789, -2, 9, 1, 123456789, 2),
        (123456.789, 123456789, -3, 9, 1, 123456789, 3),
        (12345.6789, 123456789, -4, 9, 1, 123456789, 4),
        (1234.56789, 123456789, -5, 9, 1, 123456789, 5),
        (123.456789, 123456789, -6, 9, 1, 123456789, 6),
        (12.3456789, 123456789, -7, 9, 1, 123456789, 7),
        (1.23456789, 123456789, -8, 9, 1, 123456789, 8),
        (0.123456789, 123456789, -9, 10, 1, 123456789, 9),
        (0.0123456789, 123456789, -10, 11, 1, 123456789, 10),
        (0.00123456789, 123456789, -11, 12, 1, 123456789, 11),
        (0.000123456789, 123456789, -12, 13, 1, 123456789, 12),
        (1.23456789E-05, 123456789, -13, 14, 1, 123456789, 13),
        (1.23456789E-06, 123456789, -14, 15, 1, 123456789, 14),
        (1.23456789E-07, 123456789, -15, 16, 1, 123456789, 15),
        (1.23456789E-08, 123456789, -16, 17, 1, 123456789, 16),
        (1.23456789E-09, 123456789, -17, 18, 1, 123456789, 17),
        (1.23456789E-10, 123456789, -18, 19, 1, 123456789, 18),
        (-1E-01, 1, -1, 2, -1, -1, 1),
        (-1E-02, 1, -2, 3, -1, -1, 2),
        (-1E-03, 1, -3, 4, -1, -1, 3),
        (-1E-04, 1, -4, 5, -1, -1, 4),
        (-1E-05, 1, -5, 6, -1, -1, 5),
        (-1E-06, 1, -6, 7, -1, -1, 6),
        (-1E-07, 1, -7, 8, -1, -1, 7),
        (-1E-08, 1, -8, 9, -1, -1, 8),
        (-1E-09, 1, -9, 10, -1, -1, 9),
        (-1E-10, 1, -10, 11, -1, -1, 10),
        (-12345678.9, 123456789, -1, 9, -1, -123456789, 1),
        (-1234567.89, 123456789, -2, 9, -1, -123456789, 2),
        (-123456.789, 123456789, -3, 9, -1, -123456789, 3),
        (-12345.6789, 123456789, -4, 9, -1, -123456789, 4),
        (-1234.56789, 123456789, -5, 9, -1, -123456789, 5),
        (-123.456789, 123456789, -6, 9, -1, -123456789, 6),
        (-12.3456789, 123456789, -7, 9, -1, -123456789, 7),
        (-1.23456789, 123456789, -8, 9, -1, -123456789, 8),
        (-0.123456789, 123456789, -9, 10, -1, -123456789, 9),
        (-0.0123456789, 123456789, -10, 11, -1, -123456789, 10),
        (-0.00123456789, 123456789, -11, 12, -1, -123456789, 11),
        (-0.000123456789, 123456789, -12, 13, -1, -123456789, 12),
        (-1.23456789E-05, 123456789, -13, 14, -1, -123456789, 13),
        (-1.23456789E-06, 123456789, -14, 15, -1, -123456789, 14),
        (-1.23456789E-07, 123456789, -15, 16, -1, -123456789, 15),
        (-1.23456789E-08, 123456789, -16, 17, -1, -123456789, 16),
        (-1.23456789E-09, 123456789, -17, 18, -1, -123456789, 17),
        (-1.23456789E-10, 123456789, -18, 19, -1, -123456789, 18)
    ];

    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        foreach ((double value, BigInteger significand, int exponent, int precision, int sign, BigInteger unscaledValue, int scale) in Data)
        {
            yield return [value, significand, exponent, precision, sign, unscaledValue, scale];
        }
    }
}
