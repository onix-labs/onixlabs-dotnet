// Copyright 2020-2022 ONIXLabs
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
using OnixLabs.Core.Numerics;

namespace OnixLabs.Playground.Examples.Numerics;

[Title("BigDecimal.Ceiling vs Math.Ceiling")]
public sealed class BigDecimalCeiling : Example
{
    public override void Execute()
    {
        int count = 0;
        int passed = 0;
        decimal start = ReadDecimal("Enter decimal start value: ");
        decimal end = ReadDecimal("Enter decimal end value: ");
        decimal interval = ReadDecimal("Enter decimal interval value: ");

        for (decimal value = start; value <= end; value += interval)
        {
            decimal expected = Math.Ceiling(value);
            BigDecimal actual = BigDecimal.Ceiling(new BigDecimal(value));

            bool pass = expected == actual;
            passed += pass ? 1 : 0;
            count++;

            ConsoleColor color = pass ? ConsoleColor.Green : ConsoleColor.Red;

            string[] table =
            {
                $"BigDecimal.Ceiling({value})".PadRight(32),
                $"expected: {expected}".PadRight(32),
                $"actual: {actual}".PadRight(32)
            };

            WriteLine(string.Join("| ", table), color);
        }

        WriteLine($"Count:  {count}", ConsoleColor.Blue);
        WriteLine($"Passed: {passed}", ConsoleColor.Green);
        WriteLine($"Failed: {count - passed}", ConsoleColor.Red);
    }
}
