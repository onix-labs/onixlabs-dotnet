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
using System.Reflection;
using Xunit.Sdk;

namespace OnixLabs.Numerics.UnitTests.Data;

public sealed class BigDecimalArithmeticAdditionDataAttribute : DataAttribute
{
    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        foreach (decimal left in TestDataGenerator.GenerateScaledValues())
        foreach (decimal right in TestDataGenerator.GenerateScaledValues())
            yield return [left, right, Guid.NewGuid()];

        foreach (decimal left in TestDataGenerator.GenerateRandomValues(count: 10, seed: int.MinValue))
        foreach (decimal right in TestDataGenerator.GenerateRandomValues(count: 10, seed: int.MaxValue))
            yield return [left, right, Guid.NewGuid()];
    }
}
