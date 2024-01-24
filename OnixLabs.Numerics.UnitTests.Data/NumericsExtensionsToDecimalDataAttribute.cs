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

using System.Reflection;
using Xunit.Sdk;

namespace OnixLabs.Numerics.UnitTests.Data;

public sealed class NumericsExtensionsToDecimalDataAttribute : DataAttribute
{
    private static readonly (Int128 Value, int Scale, ScaleMode Mode, string Expected)[] Data =
    [
        (0, 0, ScaleMode.Integral, "0"),
        (0, 1, ScaleMode.Integral, "0.0"),
        (0, 2, ScaleMode.Integral, "0.00"),
        (0, 3, ScaleMode.Integral, "0.000"),
        (0, 4, ScaleMode.Integral, "0.0000"),
        (0, 5, ScaleMode.Integral, "0.00000"),
        (0, 6, ScaleMode.Integral, "0.000000"),
        (0, 7, ScaleMode.Integral, "0.0000000"),
        (0, 8, ScaleMode.Integral, "0.00000000"),
        (0, 9, ScaleMode.Integral, "0.000000000"),
        (0, 10, ScaleMode.Integral, "0.0000000000"),
        (0, 28, ScaleMode.Integral, "0.0000000000000000000000000000"),
        (1, 0, ScaleMode.Integral, "1"),
        (1, 1, ScaleMode.Integral, "1.0"),
        (1, 2, ScaleMode.Integral, "1.00"),
        (1, 3, ScaleMode.Integral, "1.000"),
        (1, 4, ScaleMode.Integral, "1.0000"),
        (1, 5, ScaleMode.Integral, "1.00000"),
        (1, 6, ScaleMode.Integral, "1.000000"),
        (1, 7, ScaleMode.Integral, "1.0000000"),
        (1, 8, ScaleMode.Integral, "1.00000000"),
        (1, 9, ScaleMode.Integral, "1.000000000"),
        (1, 10, ScaleMode.Integral, "1.0000000000"),
        (1, 28, ScaleMode.Integral, "1.0000000000000000000000000000"),
        (-1, 0, ScaleMode.Integral, "-1"),
        (-1, 1, ScaleMode.Integral, "-1.0"),
        (-1, 2, ScaleMode.Integral, "-1.00"),
        (-1, 3, ScaleMode.Integral, "-1.000"),
        (-1, 4, ScaleMode.Integral, "-1.0000"),
        (-1, 5, ScaleMode.Integral, "-1.00000"),
        (-1, 6, ScaleMode.Integral, "-1.000000"),
        (-1, 7, ScaleMode.Integral, "-1.0000000"),
        (-1, 8, ScaleMode.Integral, "-1.00000000"),
        (-1, 9, ScaleMode.Integral, "-1.000000000"),
        (-1, 10, ScaleMode.Integral, "-1.0000000000"),
        (-1, 28, ScaleMode.Integral, "-1.0000000000000000000000000000"),
        (0, 0, ScaleMode.Fractional, "0"),
        (0, 1, ScaleMode.Fractional, "0.0"),
        (0, 2, ScaleMode.Fractional, "0.00"),
        (0, 3, ScaleMode.Fractional, "0.000"),
        (0, 4, ScaleMode.Fractional, "0.0000"),
        (0, 5, ScaleMode.Fractional, "0.00000"),
        (0, 6, ScaleMode.Fractional, "0.000000"),
        (0, 7, ScaleMode.Fractional, "0.0000000"),
        (0, 8, ScaleMode.Fractional, "0.00000000"),
        (0, 9, ScaleMode.Fractional, "0.000000000"),
        (0, 10, ScaleMode.Fractional, "0.0000000000"),
        (0, 28, ScaleMode.Fractional, "0.0000000000000000000000000000"),
        (1, 0, ScaleMode.Fractional, "1"),
        (1, 1, ScaleMode.Fractional, "0.1"),
        (1, 2, ScaleMode.Fractional, "0.01"),
        (1, 3, ScaleMode.Fractional, "0.001"),
        (1, 4, ScaleMode.Fractional, "0.0001"),
        (1, 5, ScaleMode.Fractional, "0.00001"),
        (1, 6, ScaleMode.Fractional, "0.000001"),
        (1, 7, ScaleMode.Fractional, "0.0000001"),
        (1, 8, ScaleMode.Fractional, "0.00000001"),
        (1, 9, ScaleMode.Fractional, "0.000000001"),
        (1, 10, ScaleMode.Fractional, "0.0000000001"),
        (1, 28, ScaleMode.Fractional, "0.0000000000000000000000000001"),
        (-1, 0, ScaleMode.Fractional, "-1"),
        (-1, 1, ScaleMode.Fractional, "-0.1"),
        (-1, 2, ScaleMode.Fractional, "-0.01"),
        (-1, 3, ScaleMode.Fractional, "-0.001"),
        (-1, 4, ScaleMode.Fractional, "-0.0001"),
        (-1, 5, ScaleMode.Fractional, "-0.00001"),
        (-1, 6, ScaleMode.Fractional, "-0.000001"),
        (-1, 7, ScaleMode.Fractional, "-0.0000001"),
        (-1, 8, ScaleMode.Fractional, "-0.00000001"),
        (-1, 9, ScaleMode.Fractional, "-0.000000001"),
        (-1, 28, ScaleMode.Fractional, "-0.0000000000000000000000000001"),
        (sbyte.MinValue, 0, ScaleMode.Integral, "-128"),
        (sbyte.MinValue, 1, ScaleMode.Integral, "-128.0"),
        (sbyte.MinValue, 2, ScaleMode.Integral, "-128.00"),
        (sbyte.MinValue, 3, ScaleMode.Integral, "-128.000"),
        (sbyte.MinValue, 4, ScaleMode.Integral, "-128.0000"),
        (sbyte.MinValue, 5, ScaleMode.Integral, "-128.00000"),
        (sbyte.MinValue, 6, ScaleMode.Integral, "-128.000000"),
        (sbyte.MinValue, 7, ScaleMode.Integral, "-128.0000000"),
        (sbyte.MinValue, 8, ScaleMode.Integral, "-128.00000000"),
        (sbyte.MinValue, 9, ScaleMode.Integral, "-128.000000000"),
        (sbyte.MinValue, 10, ScaleMode.Integral, "-128.0000000000"),
        (sbyte.MaxValue, 0, ScaleMode.Integral, "127"),
        (sbyte.MaxValue, 1, ScaleMode.Integral, "127.0"),
        (sbyte.MaxValue, 2, ScaleMode.Integral, "127.00"),
        (sbyte.MaxValue, 3, ScaleMode.Integral, "127.000"),
        (sbyte.MaxValue, 4, ScaleMode.Integral, "127.0000"),
        (sbyte.MaxValue, 5, ScaleMode.Integral, "127.00000"),
        (sbyte.MaxValue, 6, ScaleMode.Integral, "127.000000"),
        (sbyte.MaxValue, 7, ScaleMode.Integral, "127.0000000"),
        (sbyte.MaxValue, 8, ScaleMode.Integral, "127.00000000"),
        (sbyte.MaxValue, 9, ScaleMode.Integral, "127.000000000"),
        (sbyte.MaxValue, 10, ScaleMode.Integral, "127.0000000000"),
        (sbyte.MinValue, 0, ScaleMode.Fractional, "-128"),
        (sbyte.MinValue, 1, ScaleMode.Fractional, "-12.8"),
        (sbyte.MinValue, 2, ScaleMode.Fractional, "-1.28"),
        (sbyte.MinValue, 3, ScaleMode.Fractional, "-0.128"),
        (sbyte.MinValue, 4, ScaleMode.Fractional, "-0.0128"),
        (sbyte.MinValue, 5, ScaleMode.Fractional, "-0.00128"),
        (sbyte.MinValue, 6, ScaleMode.Fractional, "-0.000128"),
        (sbyte.MinValue, 7, ScaleMode.Fractional, "-0.0000128"),
        (sbyte.MinValue, 8, ScaleMode.Fractional, "-0.00000128"),
        (sbyte.MinValue, 9, ScaleMode.Fractional, "-0.000000128"),
        (sbyte.MinValue, 10, ScaleMode.Fractional, "-0.0000000128"),
        (sbyte.MinValue, 28, ScaleMode.Fractional, "-0.0000000000000000000000000128"),
        (sbyte.MaxValue, 0, ScaleMode.Fractional, "127"),
        (sbyte.MaxValue, 1, ScaleMode.Fractional, "12.7"),
        (sbyte.MaxValue, 2, ScaleMode.Fractional, "1.27"),
        (sbyte.MaxValue, 3, ScaleMode.Fractional, "0.127"),
        (sbyte.MaxValue, 4, ScaleMode.Fractional, "0.0127"),
        (sbyte.MaxValue, 5, ScaleMode.Fractional, "0.00127"),
        (sbyte.MaxValue, 6, ScaleMode.Fractional, "0.000127"),
        (sbyte.MaxValue, 7, ScaleMode.Fractional, "0.0000127"),
        (sbyte.MaxValue, 8, ScaleMode.Fractional, "0.00000127"),
        (sbyte.MaxValue, 9, ScaleMode.Fractional, "0.000000127"),
        (sbyte.MaxValue, 10, ScaleMode.Fractional, "0.0000000127"),
        (sbyte.MaxValue, 28, ScaleMode.Fractional, "0.0000000000000000000000000127"),
        (byte.MinValue, 0, ScaleMode.Integral, "0"),
        (byte.MinValue, 1, ScaleMode.Integral, "0.0"),
        (byte.MinValue, 2, ScaleMode.Integral, "0.00"),
        (byte.MinValue, 3, ScaleMode.Integral, "0.000"),
        (byte.MinValue, 4, ScaleMode.Integral, "0.0000"),
        (byte.MinValue, 5, ScaleMode.Integral, "0.00000"),
        (byte.MinValue, 6, ScaleMode.Integral, "0.000000"),
        (byte.MinValue, 7, ScaleMode.Integral, "0.0000000"),
        (byte.MinValue, 8, ScaleMode.Integral, "0.00000000"),
        (byte.MinValue, 9, ScaleMode.Integral, "0.000000000"),
        (byte.MinValue, 10, ScaleMode.Integral, "0.0000000000"),
        (byte.MinValue, 28, ScaleMode.Integral, "0.0000000000000000000000000000"),
        (byte.MaxValue, 0, ScaleMode.Integral, "255"),
        (byte.MaxValue, 1, ScaleMode.Integral, "255.0"),
        (byte.MaxValue, 2, ScaleMode.Integral, "255.00"),
        (byte.MaxValue, 3, ScaleMode.Integral, "255.000"),
        (byte.MaxValue, 4, ScaleMode.Integral, "255.0000"),
        (byte.MaxValue, 5, ScaleMode.Integral, "255.00000"),
        (byte.MaxValue, 6, ScaleMode.Integral, "255.000000"),
        (byte.MaxValue, 7, ScaleMode.Integral, "255.0000000"),
        (byte.MaxValue, 8, ScaleMode.Integral, "255.00000000"),
        (byte.MaxValue, 9, ScaleMode.Integral, "255.000000000"),
        (byte.MaxValue, 10, ScaleMode.Integral, "255.0000000000"),
        (byte.MinValue, 0, ScaleMode.Fractional, "0"),
        (byte.MinValue, 1, ScaleMode.Fractional, "0.0"),
        (byte.MinValue, 2, ScaleMode.Fractional, "0.00"),
        (byte.MinValue, 3, ScaleMode.Fractional, "0.000"),
        (byte.MinValue, 4, ScaleMode.Fractional, "0.0000"),
        (byte.MinValue, 5, ScaleMode.Fractional, "0.00000"),
        (byte.MinValue, 6, ScaleMode.Fractional, "0.000000"),
        (byte.MinValue, 7, ScaleMode.Fractional, "0.0000000"),
        (byte.MinValue, 8, ScaleMode.Fractional, "0.00000000"),
        (byte.MinValue, 9, ScaleMode.Fractional, "0.000000000"),
        (byte.MinValue, 10, ScaleMode.Fractional, "0.0000000000"),
        (byte.MinValue, 28, ScaleMode.Fractional, "0.0000000000000000000000000000"),
        (byte.MaxValue, 0, ScaleMode.Fractional, "255"),
        (byte.MaxValue, 1, ScaleMode.Fractional, "25.5"),
        (byte.MaxValue, 2, ScaleMode.Fractional, "2.55"),
        (byte.MaxValue, 3, ScaleMode.Fractional, "0.255"),
        (byte.MaxValue, 4, ScaleMode.Fractional, "0.0255"),
        (byte.MaxValue, 5, ScaleMode.Fractional, "0.00255"),
        (byte.MaxValue, 6, ScaleMode.Fractional, "0.000255"),
        (byte.MaxValue, 7, ScaleMode.Fractional, "0.0000255"),
        (byte.MaxValue, 8, ScaleMode.Fractional, "0.00000255"),
        (byte.MaxValue, 9, ScaleMode.Fractional, "0.000000255"),
        (byte.MaxValue, 10, ScaleMode.Fractional, "0.0000000255"),
        (byte.MaxValue, 28, ScaleMode.Fractional, "0.0000000000000000000000000255"),
        (short.MinValue, 0, ScaleMode.Integral, "-32768"),
        (short.MinValue, 1, ScaleMode.Integral, "-32768.0"),
        (short.MinValue, 2, ScaleMode.Integral, "-32768.00"),
        (short.MinValue, 3, ScaleMode.Integral, "-32768.000"),
        (short.MinValue, 4, ScaleMode.Integral, "-32768.0000"),
        (short.MinValue, 5, ScaleMode.Integral, "-32768.00000"),
        (short.MinValue, 6, ScaleMode.Integral, "-32768.000000"),
        (short.MinValue, 7, ScaleMode.Integral, "-32768.0000000"),
        (short.MinValue, 8, ScaleMode.Integral, "-32768.00000000"),
        (short.MinValue, 9, ScaleMode.Integral, "-32768.000000000"),
        (short.MinValue, 10, ScaleMode.Integral, "-32768.0000000000"),
        (short.MaxValue, 0, ScaleMode.Integral, "32767"),
        (short.MaxValue, 1, ScaleMode.Integral, "32767.0"),
        (short.MaxValue, 2, ScaleMode.Integral, "32767.00"),
        (short.MaxValue, 3, ScaleMode.Integral, "32767.000"),
        (short.MaxValue, 4, ScaleMode.Integral, "32767.0000"),
        (short.MaxValue, 5, ScaleMode.Integral, "32767.00000"),
        (short.MaxValue, 6, ScaleMode.Integral, "32767.000000"),
        (short.MaxValue, 7, ScaleMode.Integral, "32767.0000000"),
        (short.MaxValue, 8, ScaleMode.Integral, "32767.00000000"),
        (short.MaxValue, 9, ScaleMode.Integral, "32767.000000000"),
        (short.MaxValue, 10, ScaleMode.Integral, "32767.0000000000"),
        (short.MinValue, 0, ScaleMode.Fractional, "-32768"),
        (short.MinValue, 1, ScaleMode.Fractional, "-3276.8"),
        (short.MinValue, 2, ScaleMode.Fractional, "-327.68"),
        (short.MinValue, 3, ScaleMode.Fractional, "-32.768"),
        (short.MinValue, 4, ScaleMode.Fractional, "-3.2768"),
        (short.MinValue, 5, ScaleMode.Fractional, "-0.32768"),
        (short.MinValue, 6, ScaleMode.Fractional, "-0.032768"),
        (short.MinValue, 7, ScaleMode.Fractional, "-0.0032768"),
        (short.MinValue, 8, ScaleMode.Fractional, "-0.00032768"),
        (short.MinValue, 9, ScaleMode.Fractional, "-0.000032768"),
        (short.MinValue, 10, ScaleMode.Fractional, "-0.0000032768"),
        (short.MinValue, 28, ScaleMode.Fractional, "-0.0000000000000000000000032768"),
        (short.MaxValue, 0, ScaleMode.Fractional, "32767"),
        (short.MaxValue, 1, ScaleMode.Fractional, "3276.7"),
        (short.MaxValue, 2, ScaleMode.Fractional, "327.67"),
        (short.MaxValue, 3, ScaleMode.Fractional, "32.767"),
        (short.MaxValue, 4, ScaleMode.Fractional, "3.2767"),
        (short.MaxValue, 5, ScaleMode.Fractional, "0.32767"),
        (short.MaxValue, 6, ScaleMode.Fractional, "0.032767"),
        (short.MaxValue, 7, ScaleMode.Fractional, "0.0032767"),
        (short.MaxValue, 8, ScaleMode.Fractional, "0.00032767"),
        (short.MaxValue, 9, ScaleMode.Fractional, "0.000032767"),
        (short.MaxValue, 10, ScaleMode.Fractional, "0.0000032767"),
        (short.MaxValue, 28, ScaleMode.Fractional, "0.0000000000000000000000032767"),
        (ushort.MinValue, 0, ScaleMode.Integral, "0"),
        (ushort.MinValue, 1, ScaleMode.Integral, "0.0"),
        (ushort.MinValue, 2, ScaleMode.Integral, "0.00"),
        (ushort.MinValue, 3, ScaleMode.Integral, "0.000"),
        (ushort.MinValue, 4, ScaleMode.Integral, "0.0000"),
        (ushort.MinValue, 5, ScaleMode.Integral, "0.00000"),
        (ushort.MinValue, 6, ScaleMode.Integral, "0.000000"),
        (ushort.MinValue, 7, ScaleMode.Integral, "0.0000000"),
        (ushort.MinValue, 8, ScaleMode.Integral, "0.00000000"),
        (ushort.MinValue, 9, ScaleMode.Integral, "0.000000000"),
        (ushort.MinValue, 10, ScaleMode.Integral, "0.0000000000"),
        (ushort.MaxValue, 0, ScaleMode.Integral, "65535"),
        (ushort.MaxValue, 1, ScaleMode.Integral, "65535.0"),
        (ushort.MaxValue, 2, ScaleMode.Integral, "65535.00"),
        (ushort.MaxValue, 3, ScaleMode.Integral, "65535.000"),
        (ushort.MaxValue, 4, ScaleMode.Integral, "65535.0000"),
        (ushort.MaxValue, 5, ScaleMode.Integral, "65535.00000"),
        (ushort.MaxValue, 6, ScaleMode.Integral, "65535.000000"),
        (ushort.MaxValue, 7, ScaleMode.Integral, "65535.0000000"),
        (ushort.MaxValue, 8, ScaleMode.Integral, "65535.00000000"),
        (ushort.MaxValue, 9, ScaleMode.Integral, "65535.000000000"),
        (ushort.MaxValue, 10, ScaleMode.Integral, "65535.0000000000"),
        (ushort.MinValue, 0, ScaleMode.Fractional, "0"),
        (ushort.MinValue, 1, ScaleMode.Fractional, "0.0"),
        (ushort.MinValue, 2, ScaleMode.Fractional, "0.00"),
        (ushort.MinValue, 3, ScaleMode.Fractional, "0.000"),
        (ushort.MinValue, 4, ScaleMode.Fractional, "0.0000"),
        (ushort.MinValue, 5, ScaleMode.Fractional, "0.00000"),
        (ushort.MinValue, 6, ScaleMode.Fractional, "0.000000"),
        (ushort.MinValue, 7, ScaleMode.Fractional, "0.0000000"),
        (ushort.MinValue, 8, ScaleMode.Fractional, "0.00000000"),
        (ushort.MinValue, 9, ScaleMode.Fractional, "0.000000000"),
        (ushort.MinValue, 10, ScaleMode.Fractional, "0.0000000000"),
        (ushort.MinValue, 28, ScaleMode.Fractional, "0.0000000000000000000000000000"),
        (ushort.MaxValue, 0, ScaleMode.Fractional, "65535"),
        (ushort.MaxValue, 1, ScaleMode.Fractional, "6553.5"),
        (ushort.MaxValue, 2, ScaleMode.Fractional, "655.35"),
        (ushort.MaxValue, 3, ScaleMode.Fractional, "65.535"),
        (ushort.MaxValue, 4, ScaleMode.Fractional, "6.5535"),
        (ushort.MaxValue, 5, ScaleMode.Fractional, "0.65535"),
        (ushort.MaxValue, 6, ScaleMode.Fractional, "0.065535"),
        (ushort.MaxValue, 7, ScaleMode.Fractional, "0.0065535"),
        (ushort.MaxValue, 8, ScaleMode.Fractional, "0.00065535"),
        (ushort.MaxValue, 9, ScaleMode.Fractional, "0.000065535"),
        (ushort.MaxValue, 10, ScaleMode.Fractional, "0.0000065535"),
        (ushort.MaxValue, 28, ScaleMode.Fractional, "0.0000000000000000000000065535"),
        (int.MinValue, 0, ScaleMode.Integral, "-2147483648"),
        (int.MinValue, 1, ScaleMode.Integral, "-2147483648.0"),
        (int.MinValue, 2, ScaleMode.Integral, "-2147483648.00"),
        (int.MinValue, 3, ScaleMode.Integral, "-2147483648.000"),
        (int.MinValue, 4, ScaleMode.Integral, "-2147483648.0000"),
        (int.MinValue, 5, ScaleMode.Integral, "-2147483648.00000"),
        (int.MinValue, 6, ScaleMode.Integral, "-2147483648.000000"),
        (int.MinValue, 7, ScaleMode.Integral, "-2147483648.0000000"),
        (int.MinValue, 8, ScaleMode.Integral, "-2147483648.00000000"),
        (int.MinValue, 9, ScaleMode.Integral, "-2147483648.000000000"),
        (int.MinValue, 10, ScaleMode.Integral, "-2147483648.0000000000"),
        (int.MaxValue, 0, ScaleMode.Integral, "2147483647"),
        (int.MaxValue, 1, ScaleMode.Integral, "2147483647.0"),
        (int.MaxValue, 2, ScaleMode.Integral, "2147483647.00"),
        (int.MaxValue, 3, ScaleMode.Integral, "2147483647.000"),
        (int.MaxValue, 4, ScaleMode.Integral, "2147483647.0000"),
        (int.MaxValue, 5, ScaleMode.Integral, "2147483647.00000"),
        (int.MaxValue, 6, ScaleMode.Integral, "2147483647.000000"),
        (int.MaxValue, 7, ScaleMode.Integral, "2147483647.0000000"),
        (int.MaxValue, 8, ScaleMode.Integral, "2147483647.00000000"),
        (int.MaxValue, 9, ScaleMode.Integral, "2147483647.000000000"),
        (int.MaxValue, 10, ScaleMode.Integral, "2147483647.0000000000"),
        (int.MinValue, 0, ScaleMode.Fractional, "-2147483648"),
        (int.MinValue, 1, ScaleMode.Fractional, "-214748364.8"),
        (int.MinValue, 2, ScaleMode.Fractional, "-21474836.48"),
        (int.MinValue, 3, ScaleMode.Fractional, "-2147483.648"),
        (int.MinValue, 4, ScaleMode.Fractional, "-214748.3648"),
        (int.MinValue, 5, ScaleMode.Fractional, "-21474.83648"),
        (int.MinValue, 6, ScaleMode.Fractional, "-2147.483648"),
        (int.MinValue, 7, ScaleMode.Fractional, "-214.7483648"),
        (int.MinValue, 8, ScaleMode.Fractional, "-21.47483648"),
        (int.MinValue, 9, ScaleMode.Fractional, "-2.147483648"),
        (int.MinValue, 10, ScaleMode.Fractional, "-0.2147483648"),
        (int.MinValue, 28, ScaleMode.Fractional, "-0.0000000000000000002147483648"),
        (int.MaxValue, 0, ScaleMode.Fractional, "2147483647"),
        (int.MaxValue, 1, ScaleMode.Fractional, "214748364.7"),
        (int.MaxValue, 2, ScaleMode.Fractional, "21474836.47"),
        (int.MaxValue, 3, ScaleMode.Fractional, "2147483.647"),
        (int.MaxValue, 4, ScaleMode.Fractional, "214748.3647"),
        (int.MaxValue, 5, ScaleMode.Fractional, "21474.83647"),
        (int.MaxValue, 6, ScaleMode.Fractional, "2147.483647"),
        (int.MaxValue, 7, ScaleMode.Fractional, "214.7483647"),
        (int.MaxValue, 8, ScaleMode.Fractional, "21.47483647"),
        (int.MaxValue, 9, ScaleMode.Fractional, "2.147483647"),
        (int.MaxValue, 10, ScaleMode.Fractional, "0.2147483647"),
        (int.MaxValue, 28, ScaleMode.Fractional, "0.0000000000000000002147483647"),
        (uint.MinValue, 0, ScaleMode.Integral, "0"),
        (uint.MinValue, 1, ScaleMode.Integral, "0.0"),
        (uint.MinValue, 2, ScaleMode.Integral, "0.00"),
        (uint.MinValue, 3, ScaleMode.Integral, "0.000"),
        (uint.MinValue, 4, ScaleMode.Integral, "0.0000"),
        (uint.MinValue, 5, ScaleMode.Integral, "0.00000"),
        (uint.MinValue, 6, ScaleMode.Integral, "0.000000"),
        (uint.MinValue, 7, ScaleMode.Integral, "0.0000000"),
        (uint.MinValue, 8, ScaleMode.Integral, "0.00000000"),
        (uint.MinValue, 9, ScaleMode.Integral, "0.000000000"),
        (uint.MinValue, 10, ScaleMode.Integral, "0.0000000000"),
        (uint.MaxValue, 0, ScaleMode.Integral, "4294967295"),
        (uint.MaxValue, 1, ScaleMode.Integral, "4294967295.0"),
        (uint.MaxValue, 2, ScaleMode.Integral, "4294967295.00"),
        (uint.MaxValue, 3, ScaleMode.Integral, "4294967295.000"),
        (uint.MaxValue, 4, ScaleMode.Integral, "4294967295.0000"),
        (uint.MaxValue, 5, ScaleMode.Integral, "4294967295.00000"),
        (uint.MaxValue, 6, ScaleMode.Integral, "4294967295.000000"),
        (uint.MaxValue, 7, ScaleMode.Integral, "4294967295.0000000"),
        (uint.MaxValue, 8, ScaleMode.Integral, "4294967295.00000000"),
        (uint.MaxValue, 9, ScaleMode.Integral, "4294967295.000000000"),
        (uint.MaxValue, 10, ScaleMode.Integral, "4294967295.0000000000"),
        (uint.MinValue, 0, ScaleMode.Fractional, "0"),
        (uint.MinValue, 1, ScaleMode.Fractional, "0.0"),
        (uint.MinValue, 2, ScaleMode.Fractional, "0.00"),
        (uint.MinValue, 3, ScaleMode.Fractional, "0.000"),
        (uint.MinValue, 4, ScaleMode.Fractional, "0.0000"),
        (uint.MinValue, 5, ScaleMode.Fractional, "0.00000"),
        (uint.MinValue, 6, ScaleMode.Fractional, "0.000000"),
        (uint.MinValue, 7, ScaleMode.Fractional, "0.0000000"),
        (uint.MinValue, 8, ScaleMode.Fractional, "0.00000000"),
        (uint.MinValue, 9, ScaleMode.Fractional, "0.000000000"),
        (uint.MinValue, 10, ScaleMode.Fractional, "0.0000000000"),
        (uint.MinValue, 28, ScaleMode.Fractional, "0.0000000000000000000000000000"),
        (uint.MaxValue, 0, ScaleMode.Fractional, "4294967295"),
        (uint.MaxValue, 1, ScaleMode.Fractional, "429496729.5"),
        (uint.MaxValue, 2, ScaleMode.Fractional, "42949672.95"),
        (uint.MaxValue, 3, ScaleMode.Fractional, "4294967.295"),
        (uint.MaxValue, 4, ScaleMode.Fractional, "429496.7295"),
        (uint.MaxValue, 5, ScaleMode.Fractional, "42949.67295"),
        (uint.MaxValue, 6, ScaleMode.Fractional, "4294.967295"),
        (uint.MaxValue, 7, ScaleMode.Fractional, "429.4967295"),
        (uint.MaxValue, 8, ScaleMode.Fractional, "42.94967295"),
        (uint.MaxValue, 9, ScaleMode.Fractional, "4.294967295"),
        (uint.MaxValue, 10, ScaleMode.Fractional, "0.4294967295"),
        (uint.MaxValue, 28, ScaleMode.Fractional, "0.0000000000000000004294967295"),
        (long.MinValue, 0, ScaleMode.Integral, "-9223372036854775808"),
        (long.MinValue, 1, ScaleMode.Integral, "-9223372036854775808.0"),
        (long.MinValue, 2, ScaleMode.Integral, "-9223372036854775808.00"),
        (long.MinValue, 3, ScaleMode.Integral, "-9223372036854775808.000"),
        (long.MinValue, 4, ScaleMode.Integral, "-9223372036854775808.0000"),
        (long.MinValue, 5, ScaleMode.Integral, "-9223372036854775808.00000"),
        (long.MinValue, 6, ScaleMode.Integral, "-9223372036854775808.000000"),
        (long.MinValue, 7, ScaleMode.Integral, "-9223372036854775808.0000000"),
        (long.MinValue, 8, ScaleMode.Integral, "-9223372036854775808.00000000"),
        (long.MinValue, 9, ScaleMode.Integral, "-9223372036854775808.000000000"),
        (long.MaxValue, 0, ScaleMode.Integral, "9223372036854775807"),
        (long.MaxValue, 1, ScaleMode.Integral, "9223372036854775807.0"),
        (long.MaxValue, 2, ScaleMode.Integral, "9223372036854775807.00"),
        (long.MaxValue, 3, ScaleMode.Integral, "9223372036854775807.000"),
        (long.MaxValue, 4, ScaleMode.Integral, "9223372036854775807.0000"),
        (long.MaxValue, 5, ScaleMode.Integral, "9223372036854775807.00000"),
        (long.MaxValue, 6, ScaleMode.Integral, "9223372036854775807.000000"),
        (long.MaxValue, 7, ScaleMode.Integral, "9223372036854775807.0000000"),
        (long.MaxValue, 8, ScaleMode.Integral, "9223372036854775807.00000000"),
        (long.MaxValue, 9, ScaleMode.Integral, "9223372036854775807.000000000"),
        (long.MinValue, 0, ScaleMode.Fractional, "-9223372036854775808"),
        (long.MinValue, 1, ScaleMode.Fractional, "-922337203685477580.8"),
        (long.MinValue, 2, ScaleMode.Fractional, "-92233720368547758.08"),
        (long.MinValue, 3, ScaleMode.Fractional, "-9223372036854775.808"),
        (long.MinValue, 4, ScaleMode.Fractional, "-922337203685477.5808"),
        (long.MinValue, 5, ScaleMode.Fractional, "-92233720368547.75808"),
        (long.MinValue, 6, ScaleMode.Fractional, "-9223372036854.775808"),
        (long.MinValue, 7, ScaleMode.Fractional, "-922337203685.4775808"),
        (long.MinValue, 8, ScaleMode.Fractional, "-92233720368.54775808"),
        (long.MinValue, 9, ScaleMode.Fractional, "-9223372036.854775808"),
        (long.MinValue, 10, ScaleMode.Fractional, "-922337203.6854775808"),
        (long.MinValue, 28, ScaleMode.Fractional, "-0.0000000009223372036854775808"),
        (long.MaxValue, 0, ScaleMode.Fractional, "9223372036854775807"),
        (long.MaxValue, 1, ScaleMode.Fractional, "922337203685477580.7"),
        (long.MaxValue, 2, ScaleMode.Fractional, "92233720368547758.07"),
        (long.MaxValue, 3, ScaleMode.Fractional, "9223372036854775.807"),
        (long.MaxValue, 4, ScaleMode.Fractional, "922337203685477.5807"),
        (long.MaxValue, 5, ScaleMode.Fractional, "92233720368547.75807"),
        (long.MaxValue, 6, ScaleMode.Fractional, "9223372036854.775807"),
        (long.MaxValue, 7, ScaleMode.Fractional, "922337203685.4775807"),
        (long.MaxValue, 8, ScaleMode.Fractional, "92233720368.54775807"),
        (long.MaxValue, 9, ScaleMode.Fractional, "9223372036.854775807"),
        (long.MaxValue, 10, ScaleMode.Fractional, "922337203.6854775807"),
        (long.MaxValue, 28, ScaleMode.Fractional, "0.0000000009223372036854775807"),
        (ulong.MinValue, 0, ScaleMode.Integral, "0"),
        (ulong.MinValue, 1, ScaleMode.Integral, "0.0"),
        (ulong.MinValue, 2, ScaleMode.Integral, "0.00"),
        (ulong.MinValue, 3, ScaleMode.Integral, "0.000"),
        (ulong.MinValue, 4, ScaleMode.Integral, "0.0000"),
        (ulong.MinValue, 5, ScaleMode.Integral, "0.00000"),
        (ulong.MinValue, 6, ScaleMode.Integral, "0.000000"),
        (ulong.MinValue, 7, ScaleMode.Integral, "0.0000000"),
        (ulong.MinValue, 8, ScaleMode.Integral, "0.00000000"),
        (ulong.MinValue, 9, ScaleMode.Integral, "0.000000000"),
        (ulong.MaxValue, 0, ScaleMode.Integral, "18446744073709551615"),
        (ulong.MaxValue, 1, ScaleMode.Integral, "18446744073709551615.0"),
        (ulong.MaxValue, 2, ScaleMode.Integral, "18446744073709551615.00"),
        (ulong.MaxValue, 3, ScaleMode.Integral, "18446744073709551615.000"),
        (ulong.MaxValue, 4, ScaleMode.Integral, "18446744073709551615.0000"),
        (ulong.MaxValue, 5, ScaleMode.Integral, "18446744073709551615.00000"),
        (ulong.MaxValue, 6, ScaleMode.Integral, "18446744073709551615.000000"),
        (ulong.MaxValue, 7, ScaleMode.Integral, "18446744073709551615.0000000"),
        (ulong.MaxValue, 8, ScaleMode.Integral, "18446744073709551615.00000000"),
        (ulong.MaxValue, 9, ScaleMode.Integral, "18446744073709551615.000000000"),
        (ulong.MinValue, 0, ScaleMode.Fractional, "0"),
        (ulong.MinValue, 1, ScaleMode.Fractional, "0.0"),
        (ulong.MinValue, 2, ScaleMode.Fractional, "0.00"),
        (ulong.MinValue, 3, ScaleMode.Fractional, "0.000"),
        (ulong.MinValue, 4, ScaleMode.Fractional, "0.0000"),
        (ulong.MinValue, 5, ScaleMode.Fractional, "0.00000"),
        (ulong.MinValue, 6, ScaleMode.Fractional, "0.000000"),
        (ulong.MinValue, 7, ScaleMode.Fractional, "0.0000000"),
        (ulong.MinValue, 8, ScaleMode.Fractional, "0.00000000"),
        (ulong.MinValue, 9, ScaleMode.Fractional, "0.000000000"),
        (ulong.MinValue, 10, ScaleMode.Fractional, "0.0000000000"),
        (ulong.MinValue, 28, ScaleMode.Fractional, "0.0000000000000000000000000000"),
        (ulong.MaxValue, 0, ScaleMode.Fractional, "18446744073709551615"),
        (ulong.MaxValue, 1, ScaleMode.Fractional, "1844674407370955161.5"),
        (ulong.MaxValue, 2, ScaleMode.Fractional, "184467440737095516.15"),
        (ulong.MaxValue, 3, ScaleMode.Fractional, "18446744073709551.615"),
        (ulong.MaxValue, 4, ScaleMode.Fractional, "1844674407370955.1615"),
        (ulong.MaxValue, 5, ScaleMode.Fractional, "184467440737095.51615"),
        (ulong.MaxValue, 6, ScaleMode.Fractional, "18446744073709.551615"),
        (ulong.MaxValue, 7, ScaleMode.Fractional, "1844674407370.9551615"),
        (ulong.MaxValue, 8, ScaleMode.Fractional, "184467440737.09551615"),
        (ulong.MaxValue, 9, ScaleMode.Fractional, "18446744073.709551615"),
        (ulong.MaxValue, 10, ScaleMode.Fractional, "1844674407.3709551615"),
        (ulong.MaxValue, 28, ScaleMode.Fractional, "0.0000000018446744073709551615")
    ];

    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        foreach ((Int128 value, int scale, ScaleMode mode, string expected) in Data) yield return [value, scale, mode, expected];
    }
}
