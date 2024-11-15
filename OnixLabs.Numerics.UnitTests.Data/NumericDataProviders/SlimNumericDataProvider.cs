// Copyright 2020 ONIXLabs
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
using System.Globalization;

namespace OnixLabs.Numerics.UnitTests.Data.NumericDataProviders;

internal sealed class SlimNumericDataProvider : INumericDataProvider
{
    public int MinScale => 0;
    public int MaxScale => 3;
    public int RandomCount => 5;

    public int[] IntegerValues => [0, +1, +3, +1_234_567_890, -1, -3, -1_234_567_890];
    public int[] IntegerScales => [0, 3];

    public decimal[] DecimalValues =>
    [
        0m,
        1m,
        0.000000000000000000000000001m,
        127m,
        0.0000000000000000000000127m,
        255m,
        0.0000000000000000000000255m,
        32767m,
        0.00000000000000000032767m,
        65535m,
        0.00000000000000000065535m,
        2147483647m,
        0.000000002147483647m,
        4294967295m,
        0.000000004294967295m,
        9223372036854775807m,
        18446744073709551615m,
        18446744073709551.615m,

        -1m,
        -0.000000000000000000000000001m,
        -127m,
        -0.0000000000000000000000127m,
        -255m,
        -0.0000000000000000000000255m,
        -32767m,
        -0.00000000000000000032767m,
        -65535m,
        -0.00000000000000000065535m,
        -2147483647m,
        -0.000000002147483647m,
        -4294967295m,
        -0.000000004294967295m,
        -9223372036854775807m,
        -18446744073709551615m,
        -18446744073709551.615m,
    ];

    public ScaleMode[] ScaleModes => Enum.GetValues<ScaleMode>();
    public MidpointRounding[] RoundingModes => Enum.GetValues<MidpointRounding>();

    public CultureInfo[] Cultures =>
    [
        CultureInfo.InvariantCulture,
        CultureInfo.GetCultureInfo("ar-001"), // Arabic (world)
        CultureInfo.GetCultureInfo("en-001"), // English (world)
        CultureInfo.GetCultureInfo("eu"), // Basque
        CultureInfo.GetCultureInfo("zh-Hant-CN"), // Chinese, Traditional (China mainland)
    ];
}
