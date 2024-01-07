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
        float[] values =
        [
            0.1f,
            0.01f,
            0.001f,
            0.0001f,
            0.00001f,
            0.000001f,
            0.0000001f,
            0.00000001f,
            0.000000001f,
            0.0000000001f,
            12345678.9f,
            1234567.89f,
            123456.789f,
            12345.6789f,
            12345.6789f,
            1234.56789f,
            123.456789f,
            12.3456789f,
            1.23456789f,
            0.123456789f,
            0.0123456789f,
            0.00123456789f,
            0.000123456789f,
            0.0000123456789f,
            0.00000123456789f,
            0.000000123456789f,
            0.0000000123456789f,
            0.00000000123456789f,
            0.000000000123456789f,
            -0.1f,
            -0.01f,
            -0.001f,
            -0.0001f,
            -0.00001f,
            -0.000001f,
            -0.0000001f,
            -0.00000001f,
            -0.000000001f,
            -0.0000000001f,
            -12345678.9f,
            -1234567.89f,
            -123456.789f,
            -12345.6789f,
            -12345.6789f,
            -1234.56789f,
            -123.456789f,
            -12.3456789f,
            -1.23456789f,
            -0.123456789f,
            -0.0123456789f,
            -0.00123456789f,
            -0.000123456789f,
            -0.0000123456789f,
            -0.00000123456789f,
            -0.000000123456789f,
            -0.0000000123456789f,
            -0.00000000123456789f,
            -0.000000000123456789f,
        ];

        foreach (float value in values)
        {
            NumberInfo i = NumberInfo.Create(value);
            Console.WriteLine($"({value}f, {i.Significand}, {i.Exponent}, {i.Precision}, {i.Sign}, {i.UnscaledValue}, {i.Scale}),");
        }
    }

    private static void Print(NumberInfo info)
    {
        foreach (PropertyInfo property in info.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            Console.WriteLine($"{property.Name} = {property.GetValue(info)}");
        }
    }
}
