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

namespace OnixLabs.Numerics.UnitTests.Data;

public sealed class NumericsExtensionsToNumberInfoIntegerDataAttribute : TestDataAttribute
{
    private static readonly (Int128 Value, BigInteger UnscaledValue, int Scale, BigInteger Significand, int Exponent, int Sign, int Precision)[] Data =
    [
        (0, 0, 0, 0, 0, 0, 1),
        (1, 1, 0, 1, 0, 1, 1),
        (2, 2, 0, 2, 0, 1, 1),
        (3, 3, 0, 3, 0, 1, 1),
        (4, 4, 0, 4, 0, 1, 1),
        (5, 5, 0, 5, 0, 1, 1),
        (6, 6, 0, 6, 0, 1, 1),
        (7, 7, 0, 7, 0, 1, 1),
        (8, 8, 0, 8, 0, 1, 1),
        (9, 9, 0, 9, 0, 1, 1),
        (10, 10, 0, 1, 1, 1, 2),
        (100, 100, 0, 1, 2, 1, 3),
        (1000, 1000, 0, 1, 3, 1, 4),
        (10000, 10000, 0, 1, 4, 1, 5),
        (100000, 100000, 0, 1, 5, 1, 6),
        (1000000, 1000000, 0, 1, 6, 1, 7),
        (10000000, 10000000, 0, 1, 7, 1, 8),
        (100000000, 100000000, 0, 1, 8, 1, 9),
        (1000000000, 1000000000, 0, 1, 9, 1, 10),
        (10000000000, 10000000000, 0, 1, 10, 1, 11),
        (-1, -1, 0, -1, 0, -1, 1),
        (-2, -2, 0, -2, 0, -1, 1),
        (-3, -3, 0, -3, 0, -1, 1),
        (-4, -4, 0, -4, 0, -1, 1),
        (-5, -5, 0, -5, 0, -1, 1),
        (-6, -6, 0, -6, 0, -1, 1),
        (-7, -7, 0, -7, 0, -1, 1),
        (-8, -8, 0, -8, 0, -1, 1),
        (-9, -9, 0, -9, 0, -1, 1),
        (-10, -10, 0, -1, 1, -1, 2),
        (-100, -100, 0, -1, 2, -1, 3),
        (-1000, -1000, 0, -1, 3, -1, 4),
        (-10000, -10000, 0, -1, 4, -1, 5),
        (-100000, -100000, 0, -1, 5, -1, 6),
        (-1000000, -1000000, 0, -1, 6, -1, 7),
        (-10000000, -10000000, 0, -1, 7, -1, 8),
        (-100000000, -100000000, 0, -1, 8, -1, 9),
        (-1000000000, -1000000000, 0, -1, 9, -1, 10),
        (-10000000000, -10000000000, 0, -1, 10, -1, 11),
        (123456, 123456, 0, 123456, 0, 1, 6),
        (123456000, 123456000, 0, 123456, 3, 1, 9),
        (123456000000, 123456000000, 0, 123456, 6, 1, 12),
        (-123456, -123456, 0, -123456, 0, -1, 6),
        (-123456000, -123456000, 0, -123456, 3, -1, 9),
        (-123456000000, -123456000000, 0, -123456, 6, -1, 12),
        (sbyte.MaxValue, 127, 0, 127, 0, 1, 3),
        (sbyte.MinValue, -128, 0, -128, 0, -1, 3),
        (byte.MaxValue, 255, 0, 255, 0, 1, 3),
        (byte.MinValue, 0, 0, 0, 0, 0, 1),
        (short.MaxValue, 32767, 0, 32767, 0, 1, 5),
        (short.MinValue, -32768, 0, -32768, 0, -1, 5),
        (ushort.MaxValue, 65535, 0, 65535, 0, 1, 5),
        (ushort.MinValue, 0, 0, 0, 0, 0, 1),
        (int.MaxValue, 2147483647, 0, 2147483647, 0, 1, 10),
        (int.MinValue, -2147483648, 0, -2147483648, 0, -1, 10),
        (uint.MaxValue, 4294967295, 0, 4294967295, 0, 1, 10),
        (uint.MinValue, 0, 0, 0, 0, 0, 1),
        (long.MaxValue, 9223372036854775807, 0, 9223372036854775807, 0, 1, 19),
        (long.MinValue, -9223372036854775808, 0, -9223372036854775808, 0, -1, 19),
        (ulong.MaxValue, 18446744073709551615, 0, 18446744073709551615, 0, 1, 20),
        (ulong.MinValue, 0, 0, 0, 0, 0, 1)
    ];

    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        foreach ((Int128 value, BigInteger unscaledValue, int scale, BigInteger significand, int exponent, int sign, int precision) in Data)
        {
            yield return [value, unscaledValue, scale, significand, exponent, sign, precision];
        }
    }
}
