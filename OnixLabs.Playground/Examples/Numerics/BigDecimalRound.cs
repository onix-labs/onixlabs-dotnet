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
using System.Linq;
using OnixLabs.Core.Numerics;

namespace OnixLabs.Playground.Examples.Numerics;

[Title("BigDecimal.Round vs Math.Round")]
public sealed class BigDecimalRounding : Example
{
    public override void Execute()
    {
        int count = 0;
        int passed = 0;
        decimal start = ReadDecimal("Enter decimal start value: ");
        decimal end = ReadDecimal("Enter decimal end value: ");
        decimal interval = ReadDecimal("Enter decimal interval value: ");
        int maxScale = ReadInt32("Enter integral max scale value: ");
        MidpointRounding[] roundings = ReadRoundingModes();

        for (decimal value = start; value <= end; value += interval)
        {
            for (int scale = 0; scale <= maxScale; scale++)
            {
                foreach (MidpointRounding rounding in roundings)
                {
                    decimal expected = Math.Round(value, scale, rounding);
                    BigDecimal actual = BigDecimal.Round(new BigDecimal(value), scale, rounding);

                    bool pass = expected == actual;
                    passed += pass ? 1 : 0;
                    count++;

                    ConsoleColor color = pass ? ConsoleColor.Green : ConsoleColor.Red;

                    string[] table =
                    {
                        $"BigDecimal.Round({value}, {scale}, {rounding})".PadRight(64),
                        $"expected: {expected}".PadRight(32),
                        $"actual: {actual}".PadRight(32)
                    };

                    WriteLine(string.Join("| ", table), color);
                }
            }
        }

        WriteLine($"Count:  {count}", ConsoleColor.Blue);
        WriteLine($"Passed: {passed}", ConsoleColor.Green);
        WriteLine($"Failed: {count - passed}", ConsoleColor.Red);
    }

    private static MidpointRounding[] ReadRoundingModes()
    {
        MidpointRounding[] roundings = Enum.GetValues<MidpointRounding>();

        for (int index = 0; index < roundings.Length; index++)
        {
            WriteLine($"{index + 1}: {roundings[index]}");
        }
        
        int selection = ReadInt32($"Enter rounding mode (1-{roundings.Length}), or 0 for all of them: ");

        return selection == 0 ? roundings : roundings.Skip(selection - 1).Take(1).ToArray();
    }
}
