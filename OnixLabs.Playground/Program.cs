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

namespace OnixLabs.Playground;

internal static class Program
{
    private static void Main()
    {
        // IEnumerable<Foo<string>> enumerable = Enumerable
        //     .Range(1, 1_000_000)
        //     .Select(_ => new Foo<string>(_.ToString()));
        //
        // int count = 0;
        // TimeSpan op1 = TimeSpan.Zero;
        // TimeSpan op2 = TimeSpan.Zero;
        //
        // foreach (Foo<string> x in enumerable)
        // {
        //     count++;
        //
        //     DateTime s1 = DateTime.UtcNow;
        //     x.GetHashCode();
        //     op1 += DateTime.UtcNow - s1;
        //
        //     DateTime s2 = DateTime.UtcNow;
        //     x.GetHashCode2();
        //     op2 += DateTime.UtcNow - s2;
        // }
        //
        // Console.WriteLine($"Count: {count}");
        // Console.WriteLine($"Op1: {op1}");
        // Console.WriteLine($"Op2: {op2}");
    }
}

// public class Foo<T>(T value)
// {
//     public T Value { get; } = value;
//
//     public override int GetHashCode() => Value?.GetHashCode() ?? default;
//     public int GetHashCode2() => HashCode.Combine(Value);
// }
