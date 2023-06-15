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

using System.Reflection;
using OnixLabs.Core.Numerics;
using Xunit.Sdk;

namespace OnixLabs.Core.UnitTests.Data.Generators;

public sealed class GenericMathIntegerLengthDataGeneratorAttribute : DataAttribute
{
    private readonly int count;

    public GenericMathIntegerLengthDataGeneratorAttribute(int count)
    {
        this.count = count;
    }

    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        Random random = new();

        for (int index = 0; index <= count; index++)
        {
            ulong value = (ulong)long.Abs(random.NextInt64(0, long.MaxValue));
            string trimmed = value.ToString().TrimStart('-');
            ulong expectedLength = ulong.Parse(trimmed) == 0 ? 0 : (ulong)trimmed.Length;

            yield return new object[] { value, expectedLength };
        }
    }
}
