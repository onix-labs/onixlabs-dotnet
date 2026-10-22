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

using System.Threading.Tasks;

namespace OnixLabs.Core.UnitTests;

/// <summary>
/// Provides asynchronous counterparts of <see cref="Task.FromResult{TResult}"/>, <see cref="Task.CompletedTask"/>,
/// <see cref="ValueTask.FromResult{TResult}"/> and <see cref="ValueTask.CompletedTask"/> that yield before completing,
/// so that awaiting them always suspends rather than completing synchronously.
/// </summary>
internal static class Yielding
{
    public static async Task CompletedTask() => await Task.Yield();

    public static async Task<T> TaskOf<T>(T value)
    {
        await Task.Yield();
        return value;
    }

    public static async ValueTask CompletedValueTask() => await Task.Yield();

    public static async ValueTask<T> ValueTaskOf<T>(T value)
    {
        await Task.Yield();
        return value;
    }
}
