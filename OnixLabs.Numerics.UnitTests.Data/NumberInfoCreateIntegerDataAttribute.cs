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

public sealed class NumberInfoCreateIntegerDataAttribute : DataAttribute
{
    private static readonly (Int128, BigInteger, int, int, int, BigInteger, int)[] Data =
    [
        (0, 0, 0, 1, 0, 0, 0),
        (1, 1, 0, 1, 1, 1, 0),
        (2, 2, 0, 1, 1, 2, 0),
        (3, 3, 0, 1, 1, 3, 0),
        (4, 4, 0, 1, 1, 4, 0),
        (5, 5, 0, 1, 1, 5, 0),
        (6, 6, 0, 1, 1, 6, 0),
        (7, 7, 0, 1, 1, 7, 0),
        (8, 8, 0, 1, 1, 8, 0),
        (9, 9, 0, 1, 1, 9, 0),
        (10, 1, 1, 2, 1, 10, 0),
        (100, 1, 2, 3, 1, 100, 0),
        (1000, 1, 3, 4, 1, 1000, 0),
        (10000, 1, 4, 5, 1, 10000, 0),
        (100000, 1, 5, 6, 1, 100000, 0),
        (1000000, 1, 6, 7, 1, 1000000, 0),
        (10000000, 1, 7, 8, 1, 10000000, 0),
        (100000000, 1, 8, 9, 1, 100000000, 0),
        (1000000000, 1, 9, 10, 1, 1000000000, 0),
        (10000000000, 1, 10, 11, 1, 10000000000, 0),
        (-1, 1, 0, 1, -1, -1, 0),
        (-2, 2, 0, 1, -1, -2, 0),
        (-3, 3, 0, 1, -1, -3, 0),
        (-4, 4, 0, 1, -1, -4, 0),
        (-5, 5, 0, 1, -1, -5, 0),
        (-6, 6, 0, 1, -1, -6, 0),
        (-7, 7, 0, 1, -1, -7, 0),
        (-8, 8, 0, 1, -1, -8, 0),
        (-9, 9, 0, 1, -1, -9, 0),
        (-10, 1, 1, 2, -1, -10, 0),
        (-100, 1, 2, 3, -1, -100, 0),
        (-1000, 1, 3, 4, -1, -1000, 0),
        (-10000, 1, 4, 5, -1, -10000, 0),
        (-100000, 1, 5, 6, -1, -100000, 0),
        (-1000000, 1, 6, 7, -1, -1000000, 0),
        (-10000000, 1, 7, 8, -1, -10000000, 0),
        (-100000000, 1, 8, 9, -1, -100000000, 0),
        (-1000000000, 1, 9, 10, -1, -1000000000, 0),
        (-10000000000, 1, 10, 11, -1, -10000000000, 0),
        (123456, 123456, 0, 6, 1, 123456, 0),
        (123456000, 123456, 3, 9, 1, 123456000, 0),
        (123456000000, 123456, 6, 12, 1, 123456000000, 0),
        (-123456, 123456, 0, 6, -1, -123456, 0),
        (-123456000, 123456, 3, 9, -1, -123456000, 0),
        (-123456000000, 123456, 6, 12, -1, -123456000000, 0),
        (sbyte.MaxValue, 127, 0, 3, 1, 127, 0),
        (sbyte.MinValue, 128, 0, 3, -1, -128, 0),
        (byte.MaxValue, 255, 0, 3, 1, 255, 0),
        (byte.MinValue, 0, 0, 1, 0, 0, 0),
        (short.MaxValue, 32767, 0, 5, 1, 32767, 0),
        (short.MinValue, 32768, 0, 5, -1, -32768, 0),
        (ushort.MaxValue, 65535, 0, 5, 1, 65535, 0),
        (ushort.MinValue, 0, 0, 1, 0, 0, 0),
        (int.MaxValue, 2147483647, 0, 10, 1, 2147483647, 0),
        (int.MinValue, 2147483648, 0, 10, -1, -2147483648, 0),
        (uint.MaxValue, 4294967295, 0, 10, 1, 4294967295, 0),
        (uint.MinValue, 0, 0, 1, 0, 0, 0),
        (long.MaxValue, 9223372036854775807, 0, 19, 1, 9223372036854775807, 0),
        (long.MinValue, 9223372036854775808, 0, 19, -1, -9223372036854775808, 0),
        (ulong.MaxValue, 18446744073709551615, 0, 20, 1, 18446744073709551615, 0),
        (ulong.MinValue, 0, 0, 1, 0, 0, 0)
    ];

    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        foreach ((Int128 value, BigInteger significand, int exponent, int precision, int sign, BigInteger unscaledValue, int scale) in Data)
        {
            yield return [value, significand, exponent, precision, sign, unscaledValue, scale];
        }
    }
}
