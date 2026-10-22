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

using System.Threading;
using System.Threading.Tasks;

namespace OnixLabs.Core;

/// <summary>
/// Provides internal extension methods for <see cref="ValueTask{TResult}"/> instances.
/// </summary>
internal static class ValueTaskExtensions
{
    /// <summary>
    /// Provides internal extension methods for <see cref="ValueTask{TResult}"/> instances.
    /// </summary>
    /// <param name="receiver">The current <see cref="ValueTask{TResult}"/> instance.</param>
    extension<TResult>(ValueTask<TResult> receiver)
    {
        /// <summary>
        /// Gets a <see cref="ValueTask{TResult}"/> that will complete when the current <see cref="ValueTask{TResult}"/> completes,
        /// or when the specified <see cref="CancellationToken"/> has cancellation requested.
        /// </summary>
        /// <param name="token">The cancellation token that can be used to cancel long-running tasks.</param>
        /// <returns>
        /// Returns the current <see cref="ValueTask{TResult}"/> if it has already completed, without allocating;
        /// otherwise, returns a <see cref="ValueTask{TResult}"/> that represents the asynchronous wait.
        /// </returns>
        public ValueTask<TResult> WaitAsync(CancellationToken token) =>
            receiver.IsCompleted ? receiver : new ValueTask<TResult>(receiver.AsTask().WaitAsync(token));
    }
}
