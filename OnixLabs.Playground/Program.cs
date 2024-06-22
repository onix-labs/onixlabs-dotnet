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
using System.Threading;
using System.Threading.Tasks;
using OnixLabs.Core;
using OnixLabs.Core.Text;

namespace OnixLabs.Playground;

internal static class Program
{
    private static async Task Main()
    {
        Func<Task<int>> x = async () => await Task.FromResult(123);

        CancellationToken token = CancellationToken.None;

        Result<int> result = await Result<int>
            .OfAsync(Generate, token)
            .SelectAsync(Square, token);

        Console.WriteLine(result);

        Console.WriteLine("Hello, World!".ToByteArray().ToBase32().ToString(Base32FormatProvider.PaddedRfc4648));
    }

    public static async Task<int> Generate(CancellationToken token = default) => await Task.FromResult(3);

    public static async Task<int> Square(int value, CancellationToken token = default) => await Task.FromResult(value * value);
}

public static class Extensions
{
    public static async Task<Result<T>> SelectAsync<T>(this Task<Result<T>> result, Func<T, Task<T>> func)
    {
        Result<T> x = await result;
        if (x is not Success<T> success) return x;

        return await func(success.Value);
    }

    public static async Task<Result<T>> SelectAsync<T>(this Task<Result<T>> result, Func<T, CancellationToken, Task<T>> func, CancellationToken token = default)
    {
        Result<T> x = await result;
        if (x is not Success<T> success) return x;

        return await func(success.Value, token);
    }
}

// MatchAsync
