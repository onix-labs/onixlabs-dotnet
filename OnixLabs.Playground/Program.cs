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

namespace OnixLabs.Playground;

internal static class Program
{
    private static void Main()
    {
        Random random = new();
        
        for (int index = 0; index < 200; index++)
        {
            int lo = random.Next();
            int mid = random.Next();
            int hi = random.Next();
            bool isNegative = random.Next(int.MinValue, int.MaxValue) < 0;
            byte scale = (byte)random.Next(0, 28);
            
            Console.WriteLine($"{new decimal(lo, mid, hi, isNegative, scale)}m, ");
        }
        
        Console.WriteLine(decimal.MinValue);
    }
}
