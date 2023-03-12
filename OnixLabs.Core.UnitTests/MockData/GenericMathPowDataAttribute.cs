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

using System.Collections.Generic;
using System.Reflection;
using Xunit.Sdk;

namespace OnixLabs.Core.UnitTests.MockData;

public sealed class GenericMathPowDataAttribute : DataAttribute
{
    private readonly double minValue;
    private readonly double maxValue;
    private readonly int minExponent;
    private readonly int maxExponent;

    public GenericMathPowDataAttribute(double minValue, double maxValue, int minExponent, int maxExponent)
    {
        this.minValue = minValue;
        this.maxValue = maxValue;
        this.minExponent = minExponent;
        this.maxExponent = maxExponent;
    }

    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        for (double value = minValue; value <= maxValue; value++)
        {
            for (int exponent = minExponent; exponent <= maxExponent; exponent++)
            {
                double expected = double.Pow(value, exponent);
                yield return new object[] { value, exponent, expected };
            }
        }
    }
}
