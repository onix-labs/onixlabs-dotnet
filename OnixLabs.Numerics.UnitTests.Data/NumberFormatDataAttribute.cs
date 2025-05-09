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
using System.Reflection;
using Xunit.Sdk;

namespace OnixLabs.Numerics.UnitTests.Data;

public sealed class NumberFormatDataAttribute : DataAttribute
{
    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        foreach (CultureInfo culture in TestDataGenerator.GenerateCultures())
        {
            foreach (decimal value in TestDataGenerator.GenerateStaticValues())
                yield return [value, culture, Guid.NewGuid()];

            foreach (decimal value in TestDataGenerator.GenerateScaledValues())
                yield return [value, culture, Guid.NewGuid()];

            foreach (decimal value in TestDataGenerator.GenerateRandomValues())
                yield return [value, culture, Guid.NewGuid()];
        }
    }
}
