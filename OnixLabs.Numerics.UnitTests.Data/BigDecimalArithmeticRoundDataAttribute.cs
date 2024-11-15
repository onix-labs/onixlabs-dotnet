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
using System.Linq;
using System.Reflection;
using Xunit.Sdk;

namespace OnixLabs.Numerics.UnitTests.Data;

public sealed class BigDecimalArithmeticRoundDataAttribute : DataAttribute
{
    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        IEnumerable<decimal> values = Enumerable.Empty<decimal>()
            .Concat(TestDataGenerator.GenerateConstantValues())
            .Concat(TestDataGenerator.GenerateScaledMaxValues())
            .Concat(TestDataGenerator.GenerateScaledMinValues())
            .Concat(TestDataGenerator.GenerateRandomValues());

        foreach (decimal value in values)
        foreach (int scale in TestDataGenerator.GenerateScaleValues())
        foreach (MidpointRounding mode in TestDataGenerator.GetMidpointRoundingModes())
            yield return [value, scale, mode, Guid.NewGuid()];
    }
}
