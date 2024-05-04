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
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using OnixLabs.Core.Text;

namespace OnixLabs.Playground;

internal static class Program
{
    private static void Main()
    {
        Dictionary<IBaseCodec, IEnumerable<IFormatProvider>> dic = [];
        dic[IBaseCodec.Base32] = Base32FormatProvider.GetAll();
        dic[IBaseCodec.Base58] = Base58FormatProvider.GetAll();
        dic[IBaseCodec.Base64] = [Base16FormatProvider.Lowercase];

        string[] values = ["ABCDEFGHIJKLMNOPQRSTUVWXYZ", "abcdefghijklmnopqrstuvwxyz", "0123456789"];

        foreach ((IBaseCodec codec, IEnumerable<IFormatProvider> providers) in dic)
        {
            foreach (IFormatProvider provider in providers)
            {
                PropertyInfo prop = provider.GetType().GetProperty("Name", BindingFlags.Public | BindingFlags.Instance)!;
                string name = prop.GetValue(provider)!.ToString()!;

                Console.WriteLine($"{codec.GetType().Name} : {name}");

                Console.WriteLine("ENCODE");
                foreach (string value in values)
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(value);
                    string encoded = codec.Encode(bytes, provider);

                    Console.WriteLine($"[InlineData(\"{value}\",\"{encoded}\")]");
                }

                Console.WriteLine("DECODE");
                foreach (string value in values)
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(value);
                    string encoded = codec.Encode(bytes, provider);

                    Console.WriteLine($"[InlineData(\"{encoded}\",\"{value}\")]");
                }

                Console.WriteLine();
            }
        }
    }
}
