// Copyright 2020 ONIXLabs
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
using OnixLabs.Core;
using OnixLabs.Security;

namespace OnixLabs.Playground;

internal static class Program
{
    private static void Main()
    {
        int[] lengths = [1, 2, 4, 8, 16, 32, 64, 128];
        int[] seeds = [0, 4, 7, 9, 123, 256, 721, 999];

        foreach (int length in lengths)
        {
            foreach (int seed in seeds)
            {
                string token = SecurityTokenBuilder
                    .CreatePseudoRandom(length, seed)
                    .UseAlphaNumericCharacters()
                    .UseExtendedSpecialCharacters()
                    .ToSecurityToken()
                    .ToString()
                    .ToEscapedString();

                Console.WriteLine($"[InlineData({length}, {seed}, \"{token}\")]");
            }
        }
    }
}
