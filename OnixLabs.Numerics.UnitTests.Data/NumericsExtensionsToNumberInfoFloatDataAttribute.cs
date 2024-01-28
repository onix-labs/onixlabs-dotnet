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

public sealed class NumericsExtensionsToNumberInfoFloatDataAttribute : DataAttribute
{
    private static readonly (float Value, BigInteger UnscaledValue, int Scale, BigInteger Significand, int Exponent, int Sign, int Precision)[] Data =
    [
        (0.1f, 1, 1, 1, -1, 1, 2),
        (0.01f, 1, 2, 1, -2, 1, 3),
        (0.001f, 1, 3, 1, -3, 1, 4),
        (0.0001f, 1, 4, 1, -4, 1, 5),
        (1E-05f, 1, 5, 1, -5, 1, 6),
        (1E-06f, 1, 6, 1, -6, 1, 7),
        (1E-07f, 1, 7, 1, -7, 1, 8),
        (1E-08f, 1, 8, 1, -8, 1, 9),
        (1E-09f, 1, 9, 1, -9, 1, 10),
        (1E-10f, 1, 10, 1, -10, 1, 11),
        (12345679f, 12345679, 0, 12345679, 0, 1, 8),
        (1234567.9f, 12345679, 1, 12345679, -1, 1, 8),
        (123456.79f, 12345679, 2, 12345679, -2, 1, 8),
        (12345.679f, 12345679, 3, 12345679, -3, 1, 8),
        (1234.5679f, 12345679, 4, 12345679, -4, 1, 8),
        (123.45679f, 12345679, 5, 12345679, -5, 1, 8),
        (12.345679f, 12345679, 6, 12345679, -6, 1, 8),
        (1.2345679f, 12345679, 7, 12345679, -7, 1, 8),
        (0.12345679f, 12345679, 8, 12345679, -8, 1, 9),
        (0.012345679f, 12345679, 9, 12345679, -9, 1, 10),
        (0.0012345678f, 12345678, 10, 12345678, -10, 1, 11),
        (0.00012345679f, 12345679, 11, 12345679, -11, 1, 12),
        (1.2345679E-05f, 12345679, 12, 12345679, -12, 1, 13),
        (1.2345679E-06f, 12345679, 13, 12345679, -13, 1, 14),
        (1.2345679E-07f, 12345679, 14, 12345679, -14, 1, 15),
        (1.2345679E-08f, 12345679, 15, 12345679, -15, 1, 16),
        (1.2345679E-09f, 12345679, 16, 12345679, -16, 1, 17),
        (1.2345679E-10f, 12345679, 17, 12345679, -17, 1, 18),
        (-0.1f, -1, 1, -1, -1, -1, 2),
        (-0.01f, -1, 2, -1, -2, -1, 3),
        (-0.001f, -1, 3, -1, -3, -1, 4),
        (-0.0001f, -1, 4, -1, -4, -1, 5),
        (-1E-05f, -1, 5, -1, -5, -1, 6),
        (-1E-06f, -1, 6, -1, -6, -1, 7),
        (-1E-07f, -1, 7, -1, -7, -1, 8),
        (-1E-08f, -1, 8, -1, -8, -1, 9),
        (-1E-09f, -1, 9, -1, -9, -1, 10),
        (-1E-10f, -1, 10, -1, -10, -1, 11),
        (-12345679f, -12345679, 0, -12345679, 0, -1, 8),
        (-1234567.9f, -12345679, 1, -12345679, -1, -1, 8),
        (-123456.79f, -12345679, 2, -12345679, -2, -1, 8),
        (-12345.679f, -12345679, 3, -12345679, -3, -1, 8),
        (-1234.5679f, -12345679, 4, -12345679, -4, -1, 8),
        (-123.45679f, -12345679, 5, -12345679, -5, -1, 8),
        (-12.345679f, -12345679, 6, -12345679, -6, -1, 8),
        (-1.2345679f, -12345679, 7, -12345679, -7, -1, 8),
        (-0.12345679f, -12345679, 8, -12345679, -8, -1, 9),
        (-0.012345679f, -12345679, 9, -12345679, -9, -1, 10),
        (-0.0012345678f, -12345678, 10, -12345678, -10, -1, 11),
        (-0.00012345679f, -12345679, 11, -12345679, -11, -1, 12),
        (-1.2345679E-05f, -12345679, 12, -12345679, -12, -1, 13),
        (-1.2345679E-06f, -12345679, 13, -12345679, -13, -1, 14),
        (-1.2345679E-07f, -12345679, 14, -12345679, -14, -1, 15),
        (-1.2345679E-08f, -12345679, 15, -12345679, -15, -1, 16),
        (-1.2345679E-09f, -12345679, 16, -12345679, -16, -1, 17),
        (-1.2345679E-10f, -12345679, 17, -12345679, -17, -1, 18),
    ];

    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        foreach ((float value, BigInteger unscaledValue, int scale, BigInteger significand, int exponent, int sign, int precision) in Data)
        {
            yield return [value, unscaledValue, scale, significand, exponent, sign, precision];
        }
    }
}
