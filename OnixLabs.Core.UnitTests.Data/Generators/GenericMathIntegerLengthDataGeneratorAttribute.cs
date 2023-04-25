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
using Xunit.Sdk;

namespace OnixLabs.Core.UnitTests.Data.Generators;

public class GenericMathIntegerLengthDataGeneratorAttribute : DataAttribute
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
            int value = random.Next(int.MinValue, int.MaxValue);
            int expectedLength = value.ToString().TrimStart('-').Length;

            yield return new object[] { value, expectedLength };
        }
    }
}
