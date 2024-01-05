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

using System.Reflection;
using Xunit.Sdk;

namespace OnixLabs.Numerics.UnitTests.Data;

public sealed class GenericMathPowDataAttribute(int exponent, bool zero = true, double tolerance = 1e-15) : DataAttribute
{
    private static readonly double[] PositiveValues = [+1, +2, +3, +4, +5, +6, +7, +8, +9, +10, +100, +123];

    private static readonly double[] NegativeValues = [-1, -2, -3, -4, -5, -6, -7, -8, -9, -10, -100, -123];

    private static readonly double[] Fractions =
    [
        0.0, 0.12, 0.123, 0.1234, 0.12345, 0.123456, 0.1234567, 0.12345678, 0.123456789, 0.1234567899,
        0.1, 0.01, 0.001, 0.0001, 0.00001, 0.000001, 0.0000001, 0.00000001, 0.000000001, 0.0000000001,
        0.2, 0.02, 0.002, 0.0002, 0.00002, 0.000002, 0.0000002, 0.00000002, 0.000000002, 0.0000000002,
        0.3, 0.03, 0.003, 0.0003, 0.00003, 0.000003, 0.0000003, 0.00000003, 0.000000003, 0.0000000003,
        0.5, 0.05, 0.005, 0.0005, 0.00005, 0.000005, 0.0000005, 0.00000005, 0.000000005, 0.0000000005,
        0.6, 0.06, 0.006, 0.0006, 0.00006, 0.000006, 0.0000006, 0.00000006, 0.000000006, 0.0000000006,
        0.7, 0.07, 0.007, 0.0007, 0.00007, 0.000007, 0.0000007, 0.00000007, 0.000000007, 0.0000000007,
        0.8, 0.08, 0.008, 0.0008, 0.00008, 0.000008, 0.0000008, 0.00000008, 0.000000008, 0.0000000008,
        0.9, 0.09, 0.009, 0.0009, 0.00009, 0.000009, 0.0000009, 0.00000009, 0.000000009, 0.0000000009
    ];

    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        if (zero) yield return [0, exponent, Math.Pow(0, exponent), tolerance];

        foreach (double value in PositiveValues)
        {
            foreach (double fraction in Fractions)
            {
                double sum = value + fraction;
                yield return [sum, exponent, Math.Pow(sum, exponent), tolerance];
            }
        }

        foreach (double value in NegativeValues)
        {
            foreach (double fraction in Fractions)
            {
                double sum = value + fraction;
                yield return [sum, exponent, Math.Pow(sum, exponent), tolerance];
            }
        }
    }
}
