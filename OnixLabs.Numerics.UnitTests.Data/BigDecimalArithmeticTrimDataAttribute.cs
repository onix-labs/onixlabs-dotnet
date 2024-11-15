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

using System.Collections.Generic;
using System.Reflection;
using Xunit.Sdk;

namespace OnixLabs.Numerics.UnitTests.Data;

public sealed class BigDecimalArithmeticTrimDataAttribute : DataAttribute
{
    private static readonly (decimal Value, decimal Expected)[] Data =
    [
        (0.0m, 0m),
        (1.0m, 1m),
        (1.0000000000000000000000000000m, 1m),
        (1.10m, 1.1m),
        (1.1000000000000000000000000000m, 1.1m),
        (123.4560000000000000000000000000m, 123.456m),
        (-1.0m, -1m),
        (-1.0000000000000000000000000000m, -1m),
        (-1.10m, -1.1m),
        (-1.1000000000000000000000000000m, -1.1m),
        (-123.4560000000000000000000000000m, -123.456m)
    ];

    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        foreach ((decimal value, decimal expected) in Data)
            yield return [value, expected];
    }
}
