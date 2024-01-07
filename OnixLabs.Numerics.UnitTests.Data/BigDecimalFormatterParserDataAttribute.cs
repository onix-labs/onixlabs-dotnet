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

using System.Globalization;
using System.Reflection;
using Xunit.Sdk;

namespace OnixLabs.Numerics.UnitTests.Data;

public sealed class BigDecimalFormatterParserDataAttribute : DataAttribute
{
    private static readonly decimal[] Data =
    [
        0m,
        0.0m,
        0.00m,
        0.000m,
        0.0000m,
        0.00000m,
        0.000000m,
        0.0000000m,
        0.00000000m,
        0.000000000m,
        0.0000000000m,
        0.00000000000m,
        0.000000000000m,
        0.0000000000000m,
        0.00000000000000m,
        0.000000000000000m,
        0.0000000000000000m,
        0.00000000000000000m,
        0.000000000000000000m,
        0.0000000000000000000m,
        0.00000000000000000000m,
        0.000000000000000000000m,
        0.0000000000000000000000m,
        0.00000000000000000000000m,
        0.000000000000000000000000m,
        0.0000000000000000000000000m,
        0.00000000000000000000000000m,
        0.000000000000000000000000000m,
        1m,
        0.1m,
        0.01m,
        0.001m,
        0.0001m,
        0.00001m,
        0.000001m,
        0.0000001m,
        0.00000001m,
        0.000000001m,
        0.0000000001m,
        0.00000000001m,
        0.000000000001m,
        0.0000000000001m,
        0.00000000000001m,
        0.000000000000001m,
        0.0000000000000001m,
        0.00000000000000001m,
        0.000000000000000001m,
        0.0000000000000000001m,
        0.00000000000000000001m,
        0.000000000000000000001m,
        0.0000000000000000000001m,
        0.00000000000000000000001m,
        0.000000000000000000000001m,
        0.0000000000000000000000001m,
        0.00000000000000000000000001m,
        0.000000000000000000000000001m,
        1.0m,
        1.00m,
        1.000m,
        1.0000m,
        1.00000m,
        1.000000m,
        1.0000000m,
        1.00000000m,
        1.000000000m,
        1.0000000000m,
        1.00000000000m,
        1.000000000000m,
        1.0000000000000m,
        1.00000000000000m,
        1.000000000000000m,
        1.0000000000000000m,
        1.00000000000000000m,
        1.000000000000000000m,
        1.0000000000000000000m,
        1.00000000000000000000m,
        1.000000000000000000000m,
        1.0000000000000000000000m,
        1.00000000000000000000000m,
        1.000000000000000000000000m,
        1.0000000000000000000000000m,
        1.00000000000000000000000000m,
        1.000000000000000000000000000m,
        10m,
        0.10m,
        0.010m,
        0.0010m,
        0.00010m,
        0.000010m,
        0.0000010m,
        0.00000010m,
        0.000000010m,
        0.0000000010m,
        0.00000000010m,
        0.000000000010m,
        0.0000000000010m,
        0.00000000000010m,
        0.000000000000010m,
        0.0000000000000010m,
        0.00000000000000010m,
        0.000000000000000010m,
        0.0000000000000000010m,
        0.00000000000000000010m,
        0.000000000000000000010m,
        0.0000000000000000000010m,
        0.00000000000000000000010m,
        0.000000000000000000000010m,
        0.0000000000000000000000010m,
        0.00000000000000000000000010m,
        10.0m,
        10.00m,
        10.000m,
        10.0000m,
        10.00000m,
        10.000000m,
        10.0000000m,
        10.00000000m,
        10.000000000m,
        10.0000000000m,
        10.00000000000m,
        10.000000000000m,
        10.0000000000000m,
        10.00000000000000m,
        10.000000000000000m,
        10.0000000000000000m,
        10.00000000000000000m,
        10.000000000000000000m,
        10.0000000000000000000m,
        10.00000000000000000000m,
        10.000000000000000000000m,
        10.0000000000000000000000m,
        10.00000000000000000000000m,
        10.000000000000000000000000m,
        10.0000000000000000000000000m,
        10.00000000000000000000000000m,
        100m,
        0.100m,
        0.0100m,
        0.00100m,
        0.000100m,
        0.0000100m,
        0.00000100m,
        0.000000100m,
        0.0000000100m,
        0.00000000100m,
        0.000000000100m,
        0.0000000000100m,
        0.00000000000100m,
        0.000000000000100m,
        0.0000000000000100m,
        0.00000000000000100m,
        0.000000000000000100m,
        0.0000000000000000100m,
        0.00000000000000000100m,
        0.000000000000000000100m,
        0.0000000000000000000100m,
        0.00000000000000000000100m,
        0.000000000000000000000100m,
        0.0000000000000000000000100m,
        100.0m,
        100.00m,
        100.000m,
        100.0000m,
        100.00000m,
        100.000000m,
        100.0000000m,
        100.00000000m,
        100.000000000m,
        100.0000000000m,
        100.00000000000m,
        100.000000000000m,
        100.0000000000000m,
        100.00000000000000m,
        100.000000000000000m,
        100.0000000000000000m,
        100.00000000000000000m,
        100.000000000000000000m,
        100.0000000000000000000m,
        100.00000000000000000000m,
        100.000000000000000000000m,
        100.0000000000000000000000m,
        100.00000000000000000000000m,
        100.000000000000000000000000m,
        100.0000000000000000000000000m,
        127m,
        12.7m,
        1.27m,
        0.127m,
        0.0127m,
        0.00127m,
        0.000127m,
        0.0000127m,
        0.00000127m,
        0.000000127m,
        0.0000000127m,
        0.00000000127m,
        0.000000000127m,
        0.0000000000127m,
        0.00000000000127m,
        0.000000000000127m,
        0.0000000000000127m,
        0.00000000000000127m,
        0.000000000000000127m,
        0.0000000000000000127m,
        0.00000000000000000127m,
        0.000000000000000000127m,
        0.0000000000000000000127m,
        0.00000000000000000000127m,
        0.000000000000000000000127m,
        0.0000000000000000000000127m,
        127.0m,
        127.00m,
        127.000m,
        127.0000m,
        127.00000m,
        127.000000m,
        127.0000000m,
        127.00000000m,
        127.000000000m,
        127.0000000000m,
        127.00000000000m,
        127.000000000000m,
        127.0000000000000m,
        127.00000000000000m,
        127.000000000000000m,
        127.0000000000000000m,
        127.00000000000000000m,
        127.000000000000000000m,
        127.0000000000000000000m,
        127.00000000000000000000m,
        127.000000000000000000000m,
        127.0000000000000000000000m,
        127.00000000000000000000000m,
        127.000000000000000000000000m,
        127.0000000000000000000000000m,
        255m,
        25.5m,
        2.55m,
        0.255m,
        0.0255m,
        0.00255m,
        0.000255m,
        0.0000255m,
        0.00000255m,
        0.000000255m,
        0.0000000255m,
        0.00000000255m,
        0.000000000255m,
        0.0000000000255m,
        0.00000000000255m,
        0.000000000000255m,
        0.0000000000000255m,
        0.00000000000000255m,
        0.000000000000000255m,
        0.0000000000000000255m,
        0.00000000000000000255m,
        0.000000000000000000255m,
        0.0000000000000000000255m,
        0.00000000000000000000255m,
        0.000000000000000000000255m,
        0.0000000000000000000000255m,
        255.0m,
        255.00m,
        255.000m,
        255.0000m,
        255.00000m,
        255.000000m,
        255.0000000m,
        255.00000000m,
        255.000000000m,
        255.0000000000m,
        255.00000000000m,
        255.000000000000m,
        255.0000000000000m,
        255.00000000000000m,
        255.000000000000000m,
        255.0000000000000000m,
        255.00000000000000000m,
        255.000000000000000000m,
        255.0000000000000000000m,
        255.00000000000000000000m,
        255.000000000000000000000m,
        255.0000000000000000000000m,
        255.00000000000000000000000m,
        255.000000000000000000000000m,
        255.0000000000000000000000000m,
        32767m,
        3276.7m,
        327.67m,
        32.767m,
        3.2767m,
        0.32767m,
        0.032767m,
        0.0032767m,
        0.00032767m,
        0.000032767m,
        0.0000032767m,
        0.00000032767m,
        0.000000032767m,
        0.0000000032767m,
        0.00000000032767m,
        0.000000000032767m,
        0.0000000000032767m,
        0.00000000000032767m,
        0.000000000000032767m,
        0.0000000000000032767m,
        0.00000000000000032767m,
        0.000000000000000032767m,
        0.0000000000000000032767m,
        0.00000000000000000032767m,
        32767.0m,
        32767.00m,
        32767.000m,
        32767.0000m,
        32767.00000m,
        32767.000000m,
        32767.0000000m,
        32767.00000000m,
        32767.000000000m,
        32767.0000000000m,
        32767.00000000000m,
        32767.000000000000m,
        32767.0000000000000m,
        32767.00000000000000m,
        32767.000000000000000m,
        32767.0000000000000000m,
        32767.00000000000000000m,
        32767.000000000000000000m,
        32767.0000000000000000000m,
        32767.00000000000000000000m,
        32767.000000000000000000000m,
        32767.0000000000000000000000m,
        32767.00000000000000000000000m,
        65535m,
        6553.5m,
        655.35m,
        65.535m,
        6.5535m,
        0.65535m,
        0.065535m,
        0.0065535m,
        0.00065535m,
        0.000065535m,
        0.0000065535m,
        0.00000065535m,
        0.000000065535m,
        0.0000000065535m,
        0.00000000065535m,
        0.000000000065535m,
        0.0000000000065535m,
        0.00000000000065535m,
        0.000000000000065535m,
        0.0000000000000065535m,
        0.00000000000000065535m,
        0.000000000000000065535m,
        0.0000000000000000065535m,
        0.00000000000000000065535m,
        65535.0m,
        65535.00m,
        65535.000m,
        65535.0000m,
        65535.00000m,
        65535.000000m,
        65535.0000000m,
        65535.00000000m,
        65535.000000000m,
        65535.0000000000m,
        65535.00000000000m,
        65535.000000000000m,
        65535.0000000000000m,
        65535.00000000000000m,
        65535.000000000000000m,
        65535.0000000000000000m,
        65535.00000000000000000m,
        65535.000000000000000000m,
        65535.0000000000000000000m,
        65535.00000000000000000000m,
        65535.000000000000000000000m,
        65535.0000000000000000000000m,
        65535.00000000000000000000000m,
        2147483647m,
        214748364.7m,
        21474836.47m,
        2147483.647m,
        214748.3647m,
        21474.83647m,
        2147.483647m,
        214.7483647m,
        21.47483647m,
        2.147483647m,
        0.2147483647m,
        0.02147483647m,
        0.002147483647m,
        0.0002147483647m,
        0.00002147483647m,
        0.000002147483647m,
        0.0000002147483647m,
        0.00000002147483647m,
        0.000000002147483647m,
        2147483647.0m,
        2147483647.00m,
        2147483647.000m,
        2147483647.0000m,
        2147483647.00000m,
        2147483647.000000m,
        2147483647.0000000m,
        2147483647.00000000m,
        2147483647.000000000m,
        2147483647.0000000000m,
        2147483647.00000000000m,
        2147483647.000000000000m,
        2147483647.0000000000000m,
        2147483647.00000000000000m,
        2147483647.000000000000000m,
        2147483647.0000000000000000m,
        2147483647.00000000000000000m,
        2147483647.000000000000000000m,
        4294967295m,
        429496729.5m,
        42949672.95m,
        4294967.295m,
        429496.7295m,
        42949.67295m,
        4294.967295m,
        429.4967295m,
        42.94967295m,
        4.294967295m,
        0.4294967295m,
        0.04294967295m,
        0.004294967295m,
        0.0004294967295m,
        0.00004294967295m,
        0.000004294967295m,
        0.0000004294967295m,
        0.00000004294967295m,
        0.000000004294967295m,
        4294967295.0m,
        4294967295.00m,
        4294967295.000m,
        4294967295.0000m,
        4294967295.00000m,
        4294967295.000000m,
        4294967295.0000000m,
        4294967295.00000000m,
        4294967295.000000000m,
        4294967295.0000000000m,
        4294967295.00000000000m,
        4294967295.000000000000m,
        4294967295.0000000000000m,
        4294967295.00000000000000m,
        4294967295.000000000000000m,
        4294967295.0000000000000000m,
        4294967295.00000000000000000m,
        4294967295.000000000000000000m,
        9223372036854775807m,
        922337203685477580.7m,
        92233720368547758.07m,
        9223372036854775.807m,
        922337203685477.5807m,
        92233720368547.75807m,
        9223372036854.775807m,
        922337203685.4775807m,
        92233720368.54775807m,
        9223372036.854775807m,
        9223372036854775807.0m,
        9223372036854775807.00m,
        9223372036854775807.000m,
        9223372036854775807.0000m,
        9223372036854775807.00000m,
        9223372036854775807.000000m,
        9223372036854775807.0000000m,
        9223372036854775807.00000000m,
        9223372036854775807.000000000m,
        18446744073709551615m,
        1844674407370955161.5m,
        184467440737095516.15m,
        18446744073709551.615m,
        1844674407370955.1615m,
        184467440737095.51615m,
        18446744073709.551615m,
        1844674407370.9551615m,
        184467440737.09551615m,
        18446744073709551615.0m,
        18446744073709551615.00m,
        18446744073709551615.000m,
        18446744073709551615.0000m,
        18446744073709551615.00000m,
        18446744073709551615.000000m,
        18446744073709551615.0000000m,
        18446744073709551615.00000000m,
        -1m,
        -0.1m,
        -0.01m,
        -0.001m,
        -0.0001m,
        -0.00001m,
        -0.000001m,
        -0.0000001m,
        -0.00000001m,
        -0.000000001m,
        -0.0000000001m,
        -0.00000000001m,
        -0.000000000001m,
        -0.0000000000001m,
        -0.00000000000001m,
        -0.000000000000001m,
        -0.0000000000000001m,
        -0.00000000000000001m,
        -0.000000000000000001m,
        -0.0000000000000000001m,
        -0.00000000000000000001m,
        -0.000000000000000000001m,
        -0.0000000000000000000001m,
        -0.00000000000000000000001m,
        -0.000000000000000000000001m,
        -0.0000000000000000000000001m,
        -0.00000000000000000000000001m,
        -0.000000000000000000000000001m,
        -1.0m,
        -1.00m,
        -1.000m,
        -1.0000m,
        -1.00000m,
        -1.000000m,
        -1.0000000m,
        -1.00000000m,
        -1.000000000m,
        -1.0000000000m,
        -1.00000000000m,
        -1.000000000000m,
        -1.0000000000000m,
        -1.00000000000000m,
        -1.000000000000000m,
        -1.0000000000000000m,
        -1.00000000000000000m,
        -1.000000000000000000m,
        -1.0000000000000000000m,
        -1.00000000000000000000m,
        -1.000000000000000000000m,
        -1.0000000000000000000000m,
        -1.00000000000000000000000m,
        -1.000000000000000000000000m,
        -1.0000000000000000000000000m,
        -1.00000000000000000000000000m,
        -1.000000000000000000000000000m,
        -10m,
        -0.10m,
        -0.010m,
        -0.0010m,
        -0.00010m,
        -0.000010m,
        -0.0000010m,
        -0.00000010m,
        -0.000000010m,
        -0.0000000010m,
        -0.00000000010m,
        -0.000000000010m,
        -0.0000000000010m,
        -0.00000000000010m,
        -0.000000000000010m,
        -0.0000000000000010m,
        -0.00000000000000010m,
        -0.000000000000000010m,
        -0.0000000000000000010m,
        -0.00000000000000000010m,
        -0.000000000000000000010m,
        -0.0000000000000000000010m,
        -0.00000000000000000000010m,
        -0.000000000000000000000010m,
        -0.0000000000000000000000010m,
        -0.00000000000000000000000010m,
        -10.0m,
        -10.00m,
        -10.000m,
        -10.0000m,
        -10.00000m,
        -10.000000m,
        -10.0000000m,
        -10.00000000m,
        -10.000000000m,
        -10.0000000000m,
        -10.00000000000m,
        -10.000000000000m,
        -10.0000000000000m,
        -10.00000000000000m,
        -10.000000000000000m,
        -10.0000000000000000m,
        -10.00000000000000000m,
        -10.000000000000000000m,
        -10.0000000000000000000m,
        -10.00000000000000000000m,
        -10.000000000000000000000m,
        -10.0000000000000000000000m,
        -10.00000000000000000000000m,
        -10.000000000000000000000000m,
        -10.0000000000000000000000000m,
        -10.00000000000000000000000000m,
        -100m,
        -0.100m,
        -0.0100m,
        -0.00100m,
        -0.000100m,
        -0.0000100m,
        -0.00000100m,
        -0.000000100m,
        -0.0000000100m,
        -0.00000000100m,
        -0.000000000100m,
        -0.0000000000100m,
        -0.00000000000100m,
        -0.000000000000100m,
        -0.0000000000000100m,
        -0.00000000000000100m,
        -0.000000000000000100m,
        -0.0000000000000000100m,
        -0.00000000000000000100m,
        -0.000000000000000000100m,
        -0.0000000000000000000100m,
        -0.00000000000000000000100m,
        -0.000000000000000000000100m,
        -0.0000000000000000000000100m,
        -100.0m,
        -100.00m,
        -100.000m,
        -100.0000m,
        -100.00000m,
        -100.000000m,
        -100.0000000m,
        -100.00000000m,
        -100.000000000m,
        -100.0000000000m,
        -100.00000000000m,
        -100.000000000000m,
        -100.0000000000000m,
        -100.00000000000000m,
        -100.000000000000000m,
        -100.0000000000000000m,
        -100.00000000000000000m,
        -100.000000000000000000m,
        -100.0000000000000000000m,
        -100.00000000000000000000m,
        -100.000000000000000000000m,
        -100.0000000000000000000000m,
        -100.00000000000000000000000m,
        -100.000000000000000000000000m,
        -100.0000000000000000000000000m,
        -128m,
        -12.8m,
        -1.28m,
        -0.128m,
        -0.0128m,
        -0.00128m,
        -0.000128m,
        -0.0000128m,
        -0.00000128m,
        -0.000000128m,
        -0.0000000128m,
        -0.00000000128m,
        -0.000000000128m,
        -0.0000000000128m,
        -0.00000000000128m,
        -0.000000000000128m,
        -0.0000000000000128m,
        -0.00000000000000128m,
        -0.000000000000000128m,
        -0.0000000000000000128m,
        -0.00000000000000000128m,
        -0.000000000000000000128m,
        -0.0000000000000000000128m,
        -0.00000000000000000000128m,
        -0.000000000000000000000128m,
        -0.0000000000000000000000128m,
        -128.0m,
        -128.00m,
        -128.000m,
        -128.0000m,
        -128.00000m,
        -128.000000m,
        -128.0000000m,
        -128.00000000m,
        -128.000000000m,
        -128.0000000000m,
        -128.00000000000m,
        -128.000000000000m,
        -128.0000000000000m,
        -128.00000000000000m,
        -128.000000000000000m,
        -128.0000000000000000m,
        -128.00000000000000000m,
        -128.000000000000000000m,
        -128.0000000000000000000m,
        -128.00000000000000000000m,
        -128.000000000000000000000m,
        -128.0000000000000000000000m,
        -128.00000000000000000000000m,
        -128.000000000000000000000000m,
        -128.0000000000000000000000000m,
        -32768m,
        -3276.8m,
        -327.68m,
        -32.768m,
        -3.2768m,
        -0.32768m,
        -0.032768m,
        -0.0032768m,
        -0.00032768m,
        -0.000032768m,
        -0.0000032768m,
        -0.00000032768m,
        -0.000000032768m,
        -0.0000000032768m,
        -0.00000000032768m,
        -0.000000000032768m,
        -0.0000000000032768m,
        -0.00000000000032768m,
        -0.000000000000032768m,
        -0.0000000000000032768m,
        -0.00000000000000032768m,
        -0.000000000000000032768m,
        -0.0000000000000000032768m,
        -0.00000000000000000032768m,
        -32768.0m,
        -32768.00m,
        -32768.000m,
        -32768.0000m,
        -32768.00000m,
        -32768.000000m,
        -32768.0000000m,
        -32768.00000000m,
        -32768.000000000m,
        -32768.0000000000m,
        -32768.00000000000m,
        -32768.000000000000m,
        -32768.0000000000000m,
        -32768.00000000000000m,
        -32768.000000000000000m,
        -32768.0000000000000000m,
        -32768.00000000000000000m,
        -32768.000000000000000000m,
        -32768.0000000000000000000m,
        -32768.00000000000000000000m,
        -32768.000000000000000000000m,
        -32768.0000000000000000000000m,
        -32768.00000000000000000000000m,
        -2147483648m,
        -214748364.8m,
        -21474836.48m,
        -2147483.648m,
        -214748.3648m,
        -21474.83648m,
        -2147.483648m,
        -214.7483648m,
        -21.47483648m,
        -2.147483648m,
        -0.2147483648m,
        -0.02147483648m,
        -0.002147483648m,
        -0.0002147483648m,
        -0.00002147483648m,
        -0.000002147483648m,
        -0.0000002147483648m,
        -0.00000002147483648m,
        -0.000000002147483648m,
        -2147483648.0m,
        -2147483648.00m,
        -2147483648.000m,
        -2147483648.0000m,
        -2147483648.00000m,
        -2147483648.000000m,
        -2147483648.0000000m,
        -2147483648.00000000m,
        -2147483648.000000000m,
        -2147483648.0000000000m,
        -2147483648.00000000000m,
        -2147483648.000000000000m,
        -2147483648.0000000000000m,
        -2147483648.00000000000000m,
        -2147483648.000000000000000m,
        -2147483648.0000000000000000m,
        -2147483648.00000000000000000m,
        -2147483648.000000000000000000m,
        -9223372036854775808m,
        -922337203685477580.8m,
        -92233720368547758.08m,
        -9223372036854775.808m,
        -922337203685477.5808m,
        -92233720368547.75808m,
        -9223372036854.775808m,
        -922337203685.4775808m,
        -92233720368.54775808m,
        -9223372036.854775808m,
        -9223372036854775808.0m,
        -9223372036854775808.00m,
        -9223372036854775808.000m,
        -9223372036854775808.0000m,
        -9223372036854775808.00000m,
        -9223372036854775808.000000m,
        -9223372036854775808.0000000m,
        -9223372036854775808.00000000m,
        -9223372036854775808.000000000m,
    ];

    private static readonly CultureInfo[] Cultures =
    [
        CultureInfo.InvariantCulture,
        CultureInfo.GetCultureInfo("ar-001"), // Arabic (world)
        CultureInfo.GetCultureInfo("ar-AE"), // Arabic (United Arab Emirates)
        CultureInfo.GetCultureInfo("en-001"), // English (world)
        CultureInfo.GetCultureInfo("en-150"), // English (Europe)
        CultureInfo.GetCultureInfo("en-GB"), // English (United Kingdom)
        CultureInfo.GetCultureInfo("en-US"), // English (United States)
        CultureInfo.GetCultureInfo("en-US-POSIX"), // English (United States, Computer)
        CultureInfo.GetCultureInfo("en-CV"), // English (Cape Verde)
        CultureInfo.GetCultureInfo("kea-CV"), // Kabuverdianu (Cape Verde)
        CultureInfo.GetCultureInfo("pt-CV"), // Portuguese (Cape Verde)
        CultureInfo.GetCultureInfo("eu"), // Basque
        CultureInfo.GetCultureInfo("eu-ES"), // Basque (Spain)
        CultureInfo.GetCultureInfo("bg-BG"), // Bulgarian (Bulgaria)
        CultureInfo.GetCultureInfo("de-DE"), // German (Germany)
        CultureInfo.GetCultureInfo("es-ES"), // Spanish (Spain)
        CultureInfo.GetCultureInfo("fi-FI"), // Finnish (Finland)
        CultureInfo.GetCultureInfo("fo-FO"), // Faroese (Faroe Islands)
        CultureInfo.GetCultureInfo("fr-FR"), // French (France)
        CultureInfo.GetCultureInfo("hr-HR"), // Croatian (Croatia)
        CultureInfo.GetCultureInfo("hu-HU"), // Hungarian (Hungary)
        CultureInfo.GetCultureInfo("id-ID"), // Indonesian (Indonesia)
        CultureInfo.GetCultureInfo("is-IS"), // Icelandic (Iceland)
        CultureInfo.GetCultureInfo("it-IT"), // Italian (Italy)
        CultureInfo.GetCultureInfo("lt-LT"), // Lithuanian (Lithuania)
        CultureInfo.GetCultureInfo("lv-LV"), // Latvian (Latvia)
        CultureInfo.GetCultureInfo("mg-MG"), // Malagasy (Madagascar)
        CultureInfo.GetCultureInfo("mk-MK"), // Macedonian (North Macedonia)
        CultureInfo.GetCultureInfo("mn-MN"), // Mongolian (Mongolia)
        CultureInfo.GetCultureInfo("mt-MT"), // Maltese (Malta)
        CultureInfo.GetCultureInfo("nl-NL"), // Dutch (Netherlands)
        CultureInfo.GetCultureInfo("pl-PL"), // Polish (Poland)
        CultureInfo.GetCultureInfo("pt-PT"), // Portuguese (Portugal)
        CultureInfo.GetCultureInfo("ro-RO"), // Romanian (Romania)
        CultureInfo.GetCultureInfo("ru-RU"), // Russian (Russia)
        CultureInfo.GetCultureInfo("rw-RW"), // Kinyarwanda (Rwanda)
        CultureInfo.GetCultureInfo("se-SE"), // Northern Sami (Sweden)
        CultureInfo.GetCultureInfo("sk-SK"), // Slovak (Slovakia)
        CultureInfo.GetCultureInfo("so-SO"), // Somali (Somalia)
        CultureInfo.GetCultureInfo("th-TH"), // Thai (Thailand)
        CultureInfo.GetCultureInfo("to-TO"), // Tongan (Tonga)
        CultureInfo.GetCultureInfo("tr-TR"), // Turkish (Türkiye)
        CultureInfo.GetCultureInfo("zh-Hans-CN"), // Chinese, Simplified (China mainland)
        CultureInfo.GetCultureInfo("zh-Hant-CN"), // Chinese, Traditional (China mainland)
    ];

    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        foreach (CultureInfo culture in Cultures)
        {
            foreach (decimal value in Data) yield return [value, culture];
        }
    }
}
