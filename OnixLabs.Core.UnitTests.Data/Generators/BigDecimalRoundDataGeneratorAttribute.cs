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

namespace OnixLabs.Core.UnitTests.Data.Generators;

public sealed class BigDecimalRoundDataGeneratorAttribute(int start, int end, int skip, int scale) : DataAttribute
{
    private readonly decimal start = GetScaledValue(start, scale);
    private readonly decimal end = GetScaledValue(end, scale);
    private readonly decimal skip = GetScaledValue(skip, scale);

    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        MidpointRounding[] roundings = Enum.GetValues<MidpointRounding>();

        for (decimal value = start; value <= end; value += skip)
        {
            for (int currentScale = 0; currentScale <= scale; currentScale++)
            {
                foreach (MidpointRounding rounding in roundings)
                {
                    decimal expected = Math.Round(value, currentScale, rounding);
                    yield return new object[] { value, currentScale, rounding, expected };
                }
            }
        }
    }

    private static decimal GetScaledValue(int unscaledValue, int scale)
    {
        int magnitude = (int)Math.Pow(10, scale);
        return (decimal)unscaledValue / magnitude;
    }
}
