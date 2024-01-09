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
using ScaleMode = OnixLabs.Numerics.ScaleMode;

namespace OnixLabs.Playground;

internal static class Program
{
    private static void Main()
    {
        decimal[] values =
        [
            +0.1234567890987654321000000000m,
            +1.1234567890987654321000000000m,
            -0.1234567890987654321000000000m,
            -1.1234567890987654321000000000m
        ];

        int[] scales = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28];

        foreach (decimal value in values)
        {
            foreach (int scale in scales)
            {
                foreach (MidpointRounding rounding in Enum.GetValues<MidpointRounding>())
                {
                    decimal expected = decimal.Round(value, scale, rounding);
                    Console.WriteLine($"({value}, {scale}, {rounding}, {expected})");
                }
            }
        }
    }
}
