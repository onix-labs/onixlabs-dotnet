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

public sealed class NumberInfoCreateFloatDataAttribute : DataAttribute
{
    private static readonly (float, BigInteger, int, int, int, BigInteger, int)[] Data =
    [
        (1E-01f, 1, -1, 2, 1, 1, 1),
        (1E-02f, 1, -2, 3, 1, 1, 2),
        (1E-03f, 1, -3, 4, 1, 1, 3),
        (1E-04f, 1, -4, 5, 1, 1, 4),
        (1E-05f, 1, -5, 6, 1, 1, 5),
        (1E-06f, 1, -6, 7, 1, 1, 6),
        (1E-07f, 1, -7, 8, 1, 1, 7),
        (1E-08f, 1, -8, 9, 1, 1, 8),
        (1E-09f, 1, -9, 10, 1, 1, 9),
        (1E-10f, 1, -10, 11, 1, 1, 10),
        (12345679f, 12345679, 0, 8, 1, 12345679, 0),
        (1234567.9f, 12345679, -1, 8, 1, 12345679, 1),
        (123456.79f, 12345679, -2, 8, 1, 12345679, 2),
        (12345.679f, 12345679, -3, 8, 1, 12345679, 3),
        (12345.679f, 12345679, -3, 8, 1, 12345679, 3),
        (1234.5679f, 12345679, -4, 8, 1, 12345679, 4),
        (123.45679f, 12345679, -5, 8, 1, 12345679, 5),
        (12.345679f, 12345679, -6, 8, 1, 12345679, 6),
        (1.2345679f, 12345679, -7, 8, 1, 12345679, 7),
        (0.12345679f, 12345679, -8, 9, 1, 12345679, 8),
        (0.012345679f, 12345679, -9, 10, 1, 12345679, 9),
        (0.0012345678f, 12345678, -10, 11, 1, 12345678, 10),
        (0.00012345679f, 12345679, -11, 12, 1, 12345679, 11),
        (1.2345679E-05f, 12345679, -12, 13, 1, 12345679, 12),
        (1.2345679E-06f, 12345679, -13, 14, 1, 12345679, 13),
        (1.2345679E-07f, 12345679, -14, 15, 1, 12345679, 14),
        (1.2345679E-08f, 12345679, -15, 16, 1, 12345679, 15),
        (1.2345679E-09f, 12345679, -16, 17, 1, 12345679, 16),
        (1.2345679E-10f, 12345679, -17, 18, 1, 12345679, 17),
        (-1E-01f, 1, -1, 2, -1, -1, 1),
        (-1E-02f, 1, -2, 3, -1, -1, 2),
        (-1E-03f, 1, -3, 4, -1, -1, 3),
        (-1E-04f, 1, -4, 5, -1, -1, 4),
        (-1E-05f, 1, -5, 6, -1, -1, 5),
        (-1E-06f, 1, -6, 7, -1, -1, 6),
        (-1E-07f, 1, -7, 8, -1, -1, 7),
        (-1E-08f, 1, -8, 9, -1, -1, 8),
        (-1E-09f, 1, -9, 10, -1, -1, 9),
        (-1E-10f, 1, -10, 11, -1, -1, 10),
        (-12345679f, 12345679, 0, 8, -1, -12345679, 0),
        (-1234567.9f, 12345679, -1, 8, -1, -12345679, 1),
        (-123456.79f, 12345679, -2, 8, -1, -12345679, 2),
        (-12345.679f, 12345679, -3, 8, -1, -12345679, 3),
        (-12345.679f, 12345679, -3, 8, -1, -12345679, 3),
        (-1234.5679f, 12345679, -4, 8, -1, -12345679, 4),
        (-123.45679f, 12345679, -5, 8, -1, -12345679, 5),
        (-12.345679f, 12345679, -6, 8, -1, -12345679, 6),
        (-1.2345679f, 12345679, -7, 8, -1, -12345679, 7),
        (-0.12345679f, 12345679, -8, 9, -1, -12345679, 8),
        (-0.012345679f, 12345679, -9, 10, -1, -12345679, 9),
        (-0.0012345678f, 12345678, -10, 11, -1, -12345678, 10),
        (-0.00012345679f, 12345679, -11, 12, -1, -12345679, 11),
        (-1.2345679E-05f, 12345679, -12, 13, -1, -12345679, 12),
        (-1.2345679E-06f, 12345679, -13, 14, -1, -12345679, 13),
        (-1.2345679E-07f, 12345679, -14, 15, -1, -12345679, 14),
        (-1.2345679E-08f, 12345679, -15, 16, -1, -12345679, 15),
        (-1.2345679E-09f, 12345679, -16, 17, -1, -12345679, 16),
        (-1.2345679E-10f, 12345679, -17, 18, -1, -12345679, 17),
    ];

    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        foreach ((float value, BigInteger significand, int exponent, int precision, int sign, BigInteger unscaledValue, int scale) in Data)
        {
            yield return [value, significand, exponent, precision, sign, unscaledValue, scale];
        }
    }
}
