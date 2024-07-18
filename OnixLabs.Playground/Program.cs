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
using System.Reflection;
using System.Runtime.CompilerServices;

namespace OnixLabs.Playground;

internal static class Program
{
    private static void Main()
    {
        try
        {
            InvalidOperationException ex = new("Hello");

            foreach (PropertyInfo property in ex.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                Console.WriteLine($"{property.Name} = {property.GetValue(ex)}");
            }

            throw ex;
        }
        catch (Exception ex)
        {
            foreach (PropertyInfo property in ex.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                Console.WriteLine($"{property.Name} = {property.GetValue(ex)}");
            }
        }

    }
}
