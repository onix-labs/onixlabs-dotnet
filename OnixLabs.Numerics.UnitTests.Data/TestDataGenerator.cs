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

using System;
using System.Collections.Generic;
using System.Globalization;
using OnixLabs.Numerics.UnitTests.Data.NumericDataProviders;

namespace OnixLabs.Numerics.UnitTests.Data;

internal static class TestDataGenerator
{
    private static readonly INumericDataProvider Provider = INumericDataProvider.GetProvider();

    public static IEnumerable<decimal> GenerateScaledValues(
        IEnumerable<int>? values = null,
        IEnumerable<int>? scales = null,
        IEnumerable<ScaleMode>? modes = null)
    {
        foreach (ScaleMode mode in modes ?? Provider.ScaleModes)
        foreach (int value in values ?? Provider.IntegerValues)
        foreach (int scale in scales ?? Provider.IntegerScales)
            yield return value.ToDecimal(scale, mode);
    }

    public static IEnumerable<decimal> GenerateRandomValues(int? count = null, int seed = default)
    {
        count ??= Provider.RandomCount;
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

    public static IEnumerable<decimal> GenerateStaticValues()
    {
        return Provider.DecimalValues;
    }

    public static IEnumerable<CultureInfo> GenerateCultures()
    {
        return Provider.Cultures;
    }

    public static IEnumerable<MidpointRounding> GetMidpointRoundingModes()
    {
        return Provider.RoundingModes;
    }

    public static IEnumerable<int> GenerateScaleValues(int? min = null, int? max = null)
    {
        for (int scale = min ?? Provider.MinScale; scale <= (max ?? Provider.MaxScale); scale++)
            yield return scale;
    }
}
