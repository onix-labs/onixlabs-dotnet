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
using System.Reflection;
using OnixLabs.Numerics;

namespace OnixLabs.Playground;

internal static class Program
{
    private static void Main()
    {
        int[] values = [0, 123456000, -123456000];
        int[] scales = [0, 1, 2, 3, 10];

        foreach (decimal left in DecimalTestDataGenerator.GenerateScaledValues(values, scales))
        foreach (decimal right in DecimalTestDataGenerator.GenerateScaledValues(values, scales))
        {
            Console.WriteLine($"({left}m, {right}m, {(left == right).ToString().ToLower()}),");
        }
    }
}
