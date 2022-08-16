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
using System.Reflection;

namespace OnixLabs.Playground.Examples;

public abstract class Example
{
    public static void Run()
    {
        int option = -1;
        Type example = typeof(Example);
        Type[] examples = example.Assembly.GetTypes().Where(type => type.IsAssignableTo(example) && !type.IsAbstract).ToArray();

        while (option != 0)
        {
            for (int index = 0; index < examples.Length; index++)
            {
                TitleAttribute? attribute = examples[index].GetCustomAttribute<TitleAttribute>();
                string title = attribute?.Title ?? examples[index].Name;
                string number = (index + 1).ToString().PadLeft((int) Math.Log10(examples.Length));

                WriteLine($"{number}: {title}");
            }

            try
            {
                option = ReadInt32($"Enter (1-{examples.Length}), or 0 to exit: ");

                if (option < 0 || option > examples.Length)
                {
                    throw new IndexOutOfRangeException($"{option} is not a valid option");
                }

                if (option > 0)
                {
                    Type selected = examples[option - 1];
                    Example? instance = Activator.CreateInstance(selected) as Example;

                    if (instance is null)
                    {
                        throw new InvalidOperationException($"Failed to execute example: {selected.Name}.");
                    }

                    instance.Execute();
                }
            }
            catch (Exception ex)
            {
                WriteLine(ex.Message, ConsoleColor.Red);
            }
        }
    }

    protected static void Write(object obj, ConsoleColor color = ConsoleColor.Gray)
    {
        Console.ForegroundColor = color;
        Console.Write(obj);
        Console.ResetColor();
    }

    protected static void WriteLine(object obj, ConsoleColor color = ConsoleColor.Gray)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(obj);
        Console.ResetColor();
    }

    public static string ReadString(string prompt = "", ConsoleColor color = ConsoleColor.Gray)
    {
        Write(prompt, color);
        return Console.ReadLine() ?? string.Empty;
    }

    public static int ReadInt32(string prompt = "", ConsoleColor color = ConsoleColor.Gray)
    {
        return int.Parse(ReadString(prompt, color));
    }

    public static decimal ReadDecimal(string prompt = "", ConsoleColor color = ConsoleColor.Gray)
    {
        return decimal.Parse(ReadString(prompt, color));
    }

    public abstract void Execute();
}
