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

namespace OnixLabs.Numerics.UnitTests.Data;

internal static class DecimalTestDataGenerator
{
    private const int MinScale = 0;
    private const int MaxScale = 28;

    private static readonly int[] DefaultValues =
    [
        0,

        +1, +2, +3, +4, +5, +6, +7, +8, +9,
        +10, +100, +1_000, +10_000, +1_000_000_000,
        +12, +123, +1_234, +12_345, +1_234_567_890,

        -1, -2, -3, -4, -5, -6, -7, -8, -9,
        -10, -100, -1_000, -10_000, -1_000_000_000,
        -12, -123, -1_234, -12_345, -1_234_567_890
    ];

    private static readonly int[] DefaultScales = [0, 1, 2, 3, 4, 5, 10];

    private static readonly ScaleMode[] DefaultModes = Enum.GetValues<ScaleMode>();

    public static IEnumerable<decimal> GenerateScaledValues(
        IEnumerable<int>? values = null,
        IEnumerable<int>? scales = null,
        IEnumerable<ScaleMode>? modes = null)
    {
        foreach (ScaleMode mode in modes ?? DefaultModes)
        foreach (int value in values ?? DefaultValues)
        foreach (int scale in scales ?? DefaultScales)
            yield return value.ToDecimal(scale, mode);
    }

    public static IEnumerable<decimal> GenerateRandomValues(int count = 100, int seed = default)
    {
        Random random = new(seed);

        for (int index = 0; index < count; index++)
            yield return random.Next(int.MinValue, int.MaxValue).ToDecimal(random.Next(0, 10));
    }

    public static IEnumerable<decimal> GenerateScaledMaxValues()
    {
        foreach (int scale in GenerateScaleValues())
            yield return ((Int128)decimal.MaxValue).ToDecimal(scale);
    }

    public static IEnumerable<decimal> GenerateScaledMinValues()
    {
        foreach (int scale in GenerateScaleValues())
            yield return ((Int128)decimal.MinValue).ToDecimal(scale);
    }

    public static IEnumerable<decimal> GenerateConstantValues()
    {
        yield return decimal.Zero;
        yield return decimal.One;
        yield return decimal.MinusOne;
        yield return decimal.MaxValue;
        yield return decimal.MinValue;
    }

    public static IEnumerable<MidpointRounding> GetMidpointRoundingModes()
    {
        return Enum.GetValues<MidpointRounding>();
    }

    public static IEnumerable<int> GenerateScaleValues(int min = MinScale, int max = MaxScale)
    {
        for (int scale = min; scale <= max; scale++) yield return scale;
    }
}
