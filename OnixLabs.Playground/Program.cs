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
using System.Numerics;
using OnixLabs.Core.Numerics;

namespace OnixLabs.Playground;

internal static class Program
{
    private static void Main(string[] args)
    {
        Divide(10.00m, 10.00m);
        Divide(10.0m, 100.0m);
        Divide(100.0m, 10.0m);
        Divide(12.34m, 4.56m);
        Divide(4.56m, 12.34m);
        Divide(6349.8847363m, 998.131m);
        Divide(998.131m, 6349.8847363m);
    }

    private static void Divide(decimal left, decimal right)
    {
        BigDecimal a = new(left);
        BigDecimal b = new(right);
        
        Console.WriteLine($"{a} / {b} = {BigDecimal.Divide(a, b)}");
    }
}
