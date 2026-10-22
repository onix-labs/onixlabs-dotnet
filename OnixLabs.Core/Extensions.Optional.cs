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
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace OnixLabs.Core;

/// <summary>
/// Provides extension methods for <see cref="Optional{T}"/> instances.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public static class OptionalExtensions
{
    /// <summary>
    /// Provides extension methods for <see cref="Task{TResult}"/> of <see cref="Optional{T}"/> instances.
    /// </summary>
    /// <param name="receiver">The current <see cref="Task{TResult}"/> of <see cref="Optional{T}"/> instance.</param>
    extension<T>(Task<Optional<T>> receiver) where T : notnull
    {
        /// <summary>
        /// Asynchronously gets the underlying value of the current <see cref="Optional{T}"/> instance, if the underlying value is present;
        /// otherwise, returns the default <typeparamref name="T"/> value.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns the underlying value of the current <see cref="Optional{T}"/> instance, if the underlying value is present;
        /// otherwise, returns the default <typeparamref name="T"/> value.
        /// </returns>
        public async Task<T?> GetValueOrDefaultAsync(CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).GetValueOrDefault();

        /// <summary>
        /// Asynchronously gets the underlying value of the current <see cref="Optional{T}"/> instance, if the underlying value is present;
        /// otherwise, returns the specified default value.
        /// </summary>
        /// <param name="defaultValue">The default value to return in the event that the underlying value is absent.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns the underlying value of the current <see cref="Optional{T}"/> instance, if the underlying value is present;
        /// otherwise, returns the specified default value.
        /// </returns>
        public async Task<T> GetValueOrDefaultAsync(T defaultValue, CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).GetValueOrDefault(defaultValue);

        /// <summary>
        /// Asynchronously gets the underlying value of the current <see cref="Optional{T}"/> instance, if the underlying value is present;
        /// otherwise, throws an <see cref="InvalidOperationException"/> exception.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns the underlying value of the current <see cref="Optional{T}"/> instance, if the underlying value is present;
        /// otherwise, throws an <see cref="InvalidOperationException"/> exception.
        /// </returns>
        public async Task<T> GetValueOrThrowAsync(CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).GetValueOrThrow();

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Optional{T}"/> instance.
        /// </summary>
        /// <param name="some">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is present.</param>
        /// <param name="none">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is absent.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async Task MatchAsync(Action<T>? some = null, Action? none = null, CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).Match(some, none);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Optional{T}"/> instance.
        /// </summary>
        /// <param name="some">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is present.</param>
        /// <param name="none">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is absent.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async Task MatchAsync(Func<T, Task>? some = null, Action? none = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(some, none, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Optional{T}"/> instance.
        /// </summary>
        /// <param name="some">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is present.</param>
        /// <param name="none">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is absent.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async Task MatchAsync(Func<T, ValueTask>? some = null, Action? none = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(some, none, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Optional{T}"/> instance.
        /// </summary>
        /// <param name="some">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is present.</param>
        /// <param name="none">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is absent.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async Task MatchAsync(Func<T, CancellationToken, Task>? some = null, Action? none = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(some, none, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Optional{T}"/> instance.
        /// </summary>
        /// <param name="some">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is present.</param>
        /// <param name="none">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is absent.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async Task MatchAsync(Func<T, CancellationToken, ValueTask>? some = null, Action? none = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(some, none, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Optional{T}"/> instance.
        /// </summary>
        /// <param name="some">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is present.</param>
        /// <param name="none">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is absent.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async Task MatchAsync(Action<T>? some = null, Func<Task>? none = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(some, none, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Optional{T}"/> instance.
        /// </summary>
        /// <param name="some">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is present.</param>
        /// <param name="none">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is absent.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async Task MatchAsync(Action<T>? some = null, Func<ValueTask>? none = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(some, none, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Optional{T}"/> instance.
        /// </summary>
        /// <param name="some">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is present.</param>
        /// <param name="none">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is absent.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async Task MatchAsync(Action<T>? some = null, Func<CancellationToken, Task>? none = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(some, none, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Optional{T}"/> instance.
        /// </summary>
        /// <param name="some">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is present.</param>
        /// <param name="none">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is absent.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async Task MatchAsync(Action<T>? some = null, Func<CancellationToken, ValueTask>? none = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(some, none, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Optional{T}"/> instance.
        /// </summary>
        /// <param name="some">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is present.</param>
        /// <param name="none">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is absent.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async Task MatchAsync(Func<T, Task>? some = null, Func<Task>? none = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(some, none, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Optional{T}"/> instance.
        /// </summary>
        /// <param name="some">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is present.</param>
        /// <param name="none">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is absent.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async Task MatchAsync(Func<T, ValueTask>? some = null, Func<ValueTask>? none = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(some, none, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Optional{T}"/> instance.
        /// </summary>
        /// <param name="some">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is present.</param>
        /// <param name="none">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is absent.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async Task MatchAsync(Func<T, CancellationToken, Task>? some = null, Func<CancellationToken, Task>? none = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(some, none, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Optional{T}"/> instance.
        /// </summary>
        /// <param name="some">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is present.</param>
        /// <param name="none">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is absent.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async Task MatchAsync(Func<T, CancellationToken, ValueTask>? some = null, Func<CancellationToken, ValueTask>? none = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(some, none, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Optional{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="some">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is present.</param>
        /// <param name="none">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is absent.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="some"/> delegate if the underlying value of the current <see cref="Optional{T}"/> instance is present;
        /// otherwise, returns the result of the <paramref name="none"/> delegate if the underlying value of the current <see cref="Optional{T}"/> instance is absent.
        /// </returns>
        public async Task<TResult> MatchAsync<TResult>(Func<T, TResult> some, Func<TResult> none, CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).Match(some, none);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Optional{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="some">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is present.</param>
        /// <param name="none">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is absent.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="some"/> delegate if the underlying value of the current <see cref="Optional{T}"/> instance is present;
        /// otherwise, returns the result of the <paramref name="none"/> delegate if the underlying value of the current <see cref="Optional{T}"/> instance is absent.
        /// </returns>
        public async Task<TResult> MatchAsync<TResult>(Func<T, Task<TResult>> some, Func<TResult> none, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(some, none, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Optional{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="some">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is present.</param>
        /// <param name="none">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is absent.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="some"/> delegate if the underlying value of the current <see cref="Optional{T}"/> instance is present;
        /// otherwise, returns the result of the <paramref name="none"/> delegate if the underlying value of the current <see cref="Optional{T}"/> instance is absent.
        /// </returns>
        public async Task<TResult> MatchAsync<TResult>(Func<T, ValueTask<TResult>> some, Func<TResult> none, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(some, none, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Optional{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="some">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is present.</param>
        /// <param name="none">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is absent.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="some"/> delegate if the underlying value of the current <see cref="Optional{T}"/> instance is present;
        /// otherwise, returns the result of the <paramref name="none"/> delegate if the underlying value of the current <see cref="Optional{T}"/> instance is absent.
        /// </returns>
        public async Task<TResult> MatchAsync<TResult>(Func<T, CancellationToken, Task<TResult>> some, Func<TResult> none, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(some, none, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Optional{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="some">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is present.</param>
        /// <param name="none">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is absent.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="some"/> delegate if the underlying value of the current <see cref="Optional{T}"/> instance is present;
        /// otherwise, returns the result of the <paramref name="none"/> delegate if the underlying value of the current <see cref="Optional{T}"/> instance is absent.
        /// </returns>
        public async Task<TResult> MatchAsync<TResult>(Func<T, CancellationToken, ValueTask<TResult>> some, Func<TResult> none, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(some, none, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Optional{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="some">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is present.</param>
        /// <param name="none">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is absent.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="some"/> delegate if the underlying value of the current <see cref="Optional{T}"/> instance is present;
        /// otherwise, returns the result of the <paramref name="none"/> delegate if the underlying value of the current <see cref="Optional{T}"/> instance is absent.
        /// </returns>
        public async Task<TResult> MatchAsync<TResult>(Func<T, TResult> some, Func<Task<TResult>> none, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(some, none, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Optional{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="some">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is present.</param>
        /// <param name="none">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is absent.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="some"/> delegate if the underlying value of the current <see cref="Optional{T}"/> instance is present;
        /// otherwise, returns the result of the <paramref name="none"/> delegate if the underlying value of the current <see cref="Optional{T}"/> instance is absent.
        /// </returns>
        public async Task<TResult> MatchAsync<TResult>(Func<T, TResult> some, Func<ValueTask<TResult>> none, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(some, none, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Optional{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="some">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is present.</param>
        /// <param name="none">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is absent.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="some"/> delegate if the underlying value of the current <see cref="Optional{T}"/> instance is present;
        /// otherwise, returns the result of the <paramref name="none"/> delegate if the underlying value of the current <see cref="Optional{T}"/> instance is absent.
        /// </returns>
        public async Task<TResult> MatchAsync<TResult>(Func<T, TResult> some, Func<CancellationToken, Task<TResult>> none, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(some, none, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Optional{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="some">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is present.</param>
        /// <param name="none">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is absent.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="some"/> delegate if the underlying value of the current <see cref="Optional{T}"/> instance is present;
        /// otherwise, returns the result of the <paramref name="none"/> delegate if the underlying value of the current <see cref="Optional{T}"/> instance is absent.
        /// </returns>
        public async Task<TResult> MatchAsync<TResult>(Func<T, TResult> some, Func<CancellationToken, ValueTask<TResult>> none, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(some, none, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Optional{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="some">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is present.</param>
        /// <param name="none">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is absent.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="some"/> delegate if the underlying value of the current <see cref="Optional{T}"/> instance is present;
        /// otherwise, returns the result of the <paramref name="none"/> delegate if the underlying value of the current <see cref="Optional{T}"/> instance is absent.
        /// </returns>
        public async Task<TResult> MatchAsync<TResult>(Func<T, Task<TResult>> some, Func<Task<TResult>> none, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(some, none, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Optional{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="some">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is present.</param>
        /// <param name="none">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is absent.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="some"/> delegate if the underlying value of the current <see cref="Optional{T}"/> instance is present;
        /// otherwise, returns the result of the <paramref name="none"/> delegate if the underlying value of the current <see cref="Optional{T}"/> instance is absent.
        /// </returns>
        public async Task<TResult> MatchAsync<TResult>(Func<T, ValueTask<TResult>> some, Func<ValueTask<TResult>> none, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(some, none, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Optional{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="some">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is present.</param>
        /// <param name="none">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is absent.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="some"/> delegate if the underlying value of the current <see cref="Optional{T}"/> instance is present;
        /// otherwise, returns the result of the <paramref name="none"/> delegate if the underlying value of the current <see cref="Optional{T}"/> instance is absent.
        /// </returns>
        public async Task<TResult> MatchAsync<TResult>(Func<T, CancellationToken, Task<TResult>> some, Func<CancellationToken, Task<TResult>> none, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(some, none, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Optional{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="some">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is present.</param>
        /// <param name="none">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is absent.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="some"/> delegate if the underlying value of the current <see cref="Optional{T}"/> instance is present;
        /// otherwise, returns the result of the <paramref name="none"/> delegate if the underlying value of the current <see cref="Optional{T}"/> instance is absent.
        /// </returns>
        public async Task<TResult> MatchAsync<TResult>(Func<T, CancellationToken, ValueTask<TResult>> some, Func<CancellationToken, ValueTask<TResult>> none, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(some, none, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Optional{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="Optional{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns a new <see cref="Optional{TResult}"/> instance containing the result of the delegate if the current
        /// <see cref="Optional{T}"/> instance has an underlying value; otherwise, <see cref="Optional{TResult}.None"/>.
        /// </returns>
        public async Task<Optional<TResult>> SelectAsync<TResult>(Func<T, TResult> selector, CancellationToken cancellationToken = default) where TResult : notnull =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).Select(selector);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Optional{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="Optional{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns a new <see cref="Optional{TResult}"/> instance containing the result of the delegate if the current
        /// <see cref="Optional{T}"/> instance has an underlying value; otherwise, <see cref="Optional{TResult}.None"/>.
        /// </returns>
        public async Task<Optional<TResult>> SelectAsync<TResult>(Func<T, Task<TResult>> selector, CancellationToken cancellationToken = default) where TResult : notnull =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Optional{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="Optional{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns a new <see cref="Optional{TResult}"/> instance containing the result of the delegate if the current
        /// <see cref="Optional{T}"/> instance has an underlying value; otherwise, <see cref="Optional{TResult}.None"/>.
        /// </returns>
        public async Task<Optional<TResult>> SelectAsync<TResult>(Func<T, ValueTask<TResult>> selector, CancellationToken cancellationToken = default) where TResult : notnull =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Optional{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="Optional{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns a new <see cref="Optional{TResult}"/> instance containing the result of the delegate if the current
        /// <see cref="Optional{T}"/> instance has an underlying value; otherwise, <see cref="Optional{TResult}.None"/>.
        /// </returns>
        public async Task<Optional<TResult>> SelectAsync<TResult>(Func<T, CancellationToken, Task<TResult>> selector, CancellationToken cancellationToken = default) where TResult : notnull =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Optional{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="Optional{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns a new <see cref="Optional{TResult}"/> instance containing the result of the delegate if the current
        /// <see cref="Optional{T}"/> instance has an underlying value; otherwise, <see cref="Optional{TResult}.None"/>.
        /// </returns>
        public async Task<Optional<TResult>> SelectAsync<TResult>(Func<T, CancellationToken, ValueTask<TResult>> selector, CancellationToken cancellationToken = default) where TResult : notnull =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Optional{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="Optional{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns a new <see cref="Optional{TResult}"/> instance containing the result of the delegate if the current
        /// <see cref="Optional{T}"/> instance has an underlying value; otherwise, <see cref="Optional{TResult}.None"/>.
        /// </returns>
        public async Task<Optional<TResult>> SelectManyAsync<TResult>(Func<T, Optional<TResult>> selector, CancellationToken cancellationToken = default) where TResult : notnull =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectMany(selector);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Optional{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="Optional{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns a new <see cref="Optional{TResult}"/> instance containing the result of the delegate if the current
        /// <see cref="Optional{T}"/> instance has an underlying value; otherwise, <see cref="Optional{TResult}.None"/>.
        /// </returns>
        public async Task<Optional<TResult>> SelectManyAsync<TResult>(Func<T, Task<Optional<TResult>>> selector, CancellationToken cancellationToken = default) where TResult : notnull =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectManyAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Optional{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="Optional{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns a new <see cref="Optional{TResult}"/> instance containing the result of the delegate if the current
        /// <see cref="Optional{T}"/> instance has an underlying value; otherwise, <see cref="Optional{TResult}.None"/>.
        /// </returns>
        public async Task<Optional<TResult>> SelectManyAsync<TResult>(Func<T, ValueTask<Optional<TResult>>> selector, CancellationToken cancellationToken = default) where TResult : notnull =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectManyAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Optional{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="Optional{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns a new <see cref="Optional{TResult}"/> instance containing the result of the delegate if the current
        /// <see cref="Optional{T}"/> instance has an underlying value; otherwise, <see cref="Optional{TResult}.None"/>.
        /// </returns>
        public async Task<Optional<TResult>> SelectManyAsync<TResult>(Func<T, CancellationToken, Task<Optional<TResult>>> selector, CancellationToken cancellationToken = default) where TResult : notnull =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectManyAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Optional{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="Optional{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns a new <see cref="Optional{TResult}"/> instance containing the result of the delegate if the current
        /// <see cref="Optional{T}"/> instance has an underlying value; otherwise, <see cref="Optional{TResult}.None"/>.
        /// </returns>
        public async Task<Optional<TResult>> SelectManyAsync<TResult>(Func<T, CancellationToken, ValueTask<Optional<TResult>>> selector, CancellationToken cancellationToken = default) where TResult : notnull =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectManyAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously wraps the current <see cref="Optional{T}"/> instance into new, successful <see cref="Result{T}"/> instance.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>Returns a new, successful <see cref="Result{T}"/> instance containing the current <see cref="Optional{T}"/> instance.</returns>
        public async Task<Result<Optional<T>>> ToResultAsync(CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).ToResult();
    }

    /// <summary>
    /// Provides extension methods for <see cref="ValueTask{TResult}"/> of <see cref="Optional{T}"/> instances.
    /// </summary>
    /// <param name="receiver">The current <see cref="ValueTask{TResult}"/> of <see cref="Optional{T}"/> instance.</param>
    extension<T>(ValueTask<Optional<T>> receiver) where T : notnull
    {
        /// <summary>
        /// Asynchronously gets the underlying value of the current <see cref="Optional{T}"/> instance, if the underlying value is present;
        /// otherwise, returns the default <typeparamref name="T"/> value.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns the underlying value of the current <see cref="Optional{T}"/> instance, if the underlying value is present;
        /// otherwise, returns the default <typeparamref name="T"/> value.
        /// </returns>
        public async ValueTask<T?> GetValueOrDefaultAsync(CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).GetValueOrDefault();

        /// <summary>
        /// Asynchronously gets the underlying value of the current <see cref="Optional{T}"/> instance, if the underlying value is present;
        /// otherwise, returns the specified default value.
        /// </summary>
        /// <param name="defaultValue">The default value to return in the event that the underlying value is absent.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns the underlying value of the current <see cref="Optional{T}"/> instance, if the underlying value is present;
        /// otherwise, returns the specified default value.
        /// </returns>
        public async ValueTask<T> GetValueOrDefaultAsync(T defaultValue, CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).GetValueOrDefault(defaultValue);

        /// <summary>
        /// Asynchronously gets the underlying value of the current <see cref="Optional{T}"/> instance, if the underlying value is present;
        /// otherwise, throws an <see cref="InvalidOperationException"/> exception.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns the underlying value of the current <see cref="Optional{T}"/> instance, if the underlying value is present;
        /// otherwise, throws an <see cref="InvalidOperationException"/> exception.
        /// </returns>
        public async ValueTask<T> GetValueOrThrowAsync(CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).GetValueOrThrow();

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Optional{T}"/> instance.
        /// </summary>
        /// <param name="some">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is present.</param>
        /// <param name="none">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is absent.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async ValueTask MatchAsync(Action<T>? some = null, Action? none = null, CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).Match(some, none);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Optional{T}"/> instance.
        /// </summary>
        /// <param name="some">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is present.</param>
        /// <param name="none">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is absent.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async ValueTask MatchAsync(Func<T, Task>? some = null, Action? none = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(some, none, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Optional{T}"/> instance.
        /// </summary>
        /// <param name="some">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is present.</param>
        /// <param name="none">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is absent.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async ValueTask MatchAsync(Func<T, ValueTask>? some = null, Action? none = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(some, none, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Optional{T}"/> instance.
        /// </summary>
        /// <param name="some">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is present.</param>
        /// <param name="none">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is absent.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async ValueTask MatchAsync(Func<T, CancellationToken, Task>? some = null, Action? none = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(some, none, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Optional{T}"/> instance.
        /// </summary>
        /// <param name="some">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is present.</param>
        /// <param name="none">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is absent.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async ValueTask MatchAsync(Func<T, CancellationToken, ValueTask>? some = null, Action? none = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(some, none, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Optional{T}"/> instance.
        /// </summary>
        /// <param name="some">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is present.</param>
        /// <param name="none">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is absent.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async ValueTask MatchAsync(Action<T>? some = null, Func<Task>? none = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(some, none, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Optional{T}"/> instance.
        /// </summary>
        /// <param name="some">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is present.</param>
        /// <param name="none">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is absent.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async ValueTask MatchAsync(Action<T>? some = null, Func<ValueTask>? none = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(some, none, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Optional{T}"/> instance.
        /// </summary>
        /// <param name="some">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is present.</param>
        /// <param name="none">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is absent.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async ValueTask MatchAsync(Action<T>? some = null, Func<CancellationToken, Task>? none = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(some, none, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Optional{T}"/> instance.
        /// </summary>
        /// <param name="some">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is present.</param>
        /// <param name="none">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is absent.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async ValueTask MatchAsync(Action<T>? some = null, Func<CancellationToken, ValueTask>? none = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(some, none, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Optional{T}"/> instance.
        /// </summary>
        /// <param name="some">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is present.</param>
        /// <param name="none">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is absent.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async ValueTask MatchAsync(Func<T, Task>? some = null, Func<Task>? none = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(some, none, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Optional{T}"/> instance.
        /// </summary>
        /// <param name="some">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is present.</param>
        /// <param name="none">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is absent.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async ValueTask MatchAsync(Func<T, ValueTask>? some = null, Func<ValueTask>? none = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(some, none, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Optional{T}"/> instance.
        /// </summary>
        /// <param name="some">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is present.</param>
        /// <param name="none">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is absent.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async ValueTask MatchAsync(Func<T, CancellationToken, Task>? some = null, Func<CancellationToken, Task>? none = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(some, none, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Optional{T}"/> instance.
        /// </summary>
        /// <param name="some">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is present.</param>
        /// <param name="none">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is absent.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async ValueTask MatchAsync(Func<T, CancellationToken, ValueTask>? some = null, Func<CancellationToken, ValueTask>? none = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(some, none, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Optional{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="some">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is present.</param>
        /// <param name="none">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is absent.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="some"/> delegate if the underlying value of the current <see cref="Optional{T}"/> instance is present;
        /// otherwise, returns the result of the <paramref name="none"/> delegate if the underlying value of the current <see cref="Optional{T}"/> instance is absent.
        /// </returns>
        public async ValueTask<TResult> MatchAsync<TResult>(Func<T, TResult> some, Func<TResult> none, CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).Match(some, none);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Optional{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="some">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is present.</param>
        /// <param name="none">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is absent.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="some"/> delegate if the underlying value of the current <see cref="Optional{T}"/> instance is present;
        /// otherwise, returns the result of the <paramref name="none"/> delegate if the underlying value of the current <see cref="Optional{T}"/> instance is absent.
        /// </returns>
        public async ValueTask<TResult> MatchAsync<TResult>(Func<T, Task<TResult>> some, Func<TResult> none, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(some, none, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Optional{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="some">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is present.</param>
        /// <param name="none">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is absent.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="some"/> delegate if the underlying value of the current <see cref="Optional{T}"/> instance is present;
        /// otherwise, returns the result of the <paramref name="none"/> delegate if the underlying value of the current <see cref="Optional{T}"/> instance is absent.
        /// </returns>
        public async ValueTask<TResult> MatchAsync<TResult>(Func<T, ValueTask<TResult>> some, Func<TResult> none, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(some, none, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Optional{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="some">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is present.</param>
        /// <param name="none">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is absent.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="some"/> delegate if the underlying value of the current <see cref="Optional{T}"/> instance is present;
        /// otherwise, returns the result of the <paramref name="none"/> delegate if the underlying value of the current <see cref="Optional{T}"/> instance is absent.
        /// </returns>
        public async ValueTask<TResult> MatchAsync<TResult>(Func<T, CancellationToken, Task<TResult>> some, Func<TResult> none, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(some, none, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Optional{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="some">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is present.</param>
        /// <param name="none">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is absent.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="some"/> delegate if the underlying value of the current <see cref="Optional{T}"/> instance is present;
        /// otherwise, returns the result of the <paramref name="none"/> delegate if the underlying value of the current <see cref="Optional{T}"/> instance is absent.
        /// </returns>
        public async ValueTask<TResult> MatchAsync<TResult>(Func<T, CancellationToken, ValueTask<TResult>> some, Func<TResult> none, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(some, none, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Optional{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="some">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is present.</param>
        /// <param name="none">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is absent.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="some"/> delegate if the underlying value of the current <see cref="Optional{T}"/> instance is present;
        /// otherwise, returns the result of the <paramref name="none"/> delegate if the underlying value of the current <see cref="Optional{T}"/> instance is absent.
        /// </returns>
        public async ValueTask<TResult> MatchAsync<TResult>(Func<T, TResult> some, Func<Task<TResult>> none, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(some, none, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Optional{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="some">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is present.</param>
        /// <param name="none">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is absent.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="some"/> delegate if the underlying value of the current <see cref="Optional{T}"/> instance is present;
        /// otherwise, returns the result of the <paramref name="none"/> delegate if the underlying value of the current <see cref="Optional{T}"/> instance is absent.
        /// </returns>
        public async ValueTask<TResult> MatchAsync<TResult>(Func<T, TResult> some, Func<ValueTask<TResult>> none, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(some, none, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Optional{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="some">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is present.</param>
        /// <param name="none">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is absent.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="some"/> delegate if the underlying value of the current <see cref="Optional{T}"/> instance is present;
        /// otherwise, returns the result of the <paramref name="none"/> delegate if the underlying value of the current <see cref="Optional{T}"/> instance is absent.
        /// </returns>
        public async ValueTask<TResult> MatchAsync<TResult>(Func<T, TResult> some, Func<CancellationToken, Task<TResult>> none, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(some, none, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Optional{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="some">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is present.</param>
        /// <param name="none">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is absent.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="some"/> delegate if the underlying value of the current <see cref="Optional{T}"/> instance is present;
        /// otherwise, returns the result of the <paramref name="none"/> delegate if the underlying value of the current <see cref="Optional{T}"/> instance is absent.
        /// </returns>
        public async ValueTask<TResult> MatchAsync<TResult>(Func<T, TResult> some, Func<CancellationToken, ValueTask<TResult>> none, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(some, none, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Optional{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="some">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is present.</param>
        /// <param name="none">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is absent.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="some"/> delegate if the underlying value of the current <see cref="Optional{T}"/> instance is present;
        /// otherwise, returns the result of the <paramref name="none"/> delegate if the underlying value of the current <see cref="Optional{T}"/> instance is absent.
        /// </returns>
        public async ValueTask<TResult> MatchAsync<TResult>(Func<T, Task<TResult>> some, Func<Task<TResult>> none, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(some, none, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Optional{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="some">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is present.</param>
        /// <param name="none">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is absent.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="some"/> delegate if the underlying value of the current <see cref="Optional{T}"/> instance is present;
        /// otherwise, returns the result of the <paramref name="none"/> delegate if the underlying value of the current <see cref="Optional{T}"/> instance is absent.
        /// </returns>
        public async ValueTask<TResult> MatchAsync<TResult>(Func<T, ValueTask<TResult>> some, Func<ValueTask<TResult>> none, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(some, none, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Optional{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="some">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is present.</param>
        /// <param name="none">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is absent.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="some"/> delegate if the underlying value of the current <see cref="Optional{T}"/> instance is present;
        /// otherwise, returns the result of the <paramref name="none"/> delegate if the underlying value of the current <see cref="Optional{T}"/> instance is absent.
        /// </returns>
        public async ValueTask<TResult> MatchAsync<TResult>(Func<T, CancellationToken, Task<TResult>> some, Func<CancellationToken, Task<TResult>> none, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(some, none, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Optional{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="some">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is present.</param>
        /// <param name="none">The delegate to execute when the underlying value of the current <see cref="Optional{T}"/> instance is absent.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="some"/> delegate if the underlying value of the current <see cref="Optional{T}"/> instance is present;
        /// otherwise, returns the result of the <paramref name="none"/> delegate if the underlying value of the current <see cref="Optional{T}"/> instance is absent.
        /// </returns>
        public async ValueTask<TResult> MatchAsync<TResult>(Func<T, CancellationToken, ValueTask<TResult>> some, Func<CancellationToken, ValueTask<TResult>> none, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(some, none, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Optional{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="Optional{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns a new <see cref="Optional{TResult}"/> instance containing the result of the delegate if the current
        /// <see cref="Optional{T}"/> instance has an underlying value; otherwise, <see cref="Optional{TResult}.None"/>.
        /// </returns>
        public async ValueTask<Optional<TResult>> SelectAsync<TResult>(Func<T, TResult> selector, CancellationToken cancellationToken = default) where TResult : notnull =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).Select(selector);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Optional{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="Optional{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns a new <see cref="Optional{TResult}"/> instance containing the result of the delegate if the current
        /// <see cref="Optional{T}"/> instance has an underlying value; otherwise, <see cref="Optional{TResult}.None"/>.
        /// </returns>
        public async ValueTask<Optional<TResult>> SelectAsync<TResult>(Func<T, Task<TResult>> selector, CancellationToken cancellationToken = default) where TResult : notnull =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Optional{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="Optional{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns a new <see cref="Optional{TResult}"/> instance containing the result of the delegate if the current
        /// <see cref="Optional{T}"/> instance has an underlying value; otherwise, <see cref="Optional{TResult}.None"/>.
        /// </returns>
        public async ValueTask<Optional<TResult>> SelectAsync<TResult>(Func<T, ValueTask<TResult>> selector, CancellationToken cancellationToken = default) where TResult : notnull =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Optional{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="Optional{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns a new <see cref="Optional{TResult}"/> instance containing the result of the delegate if the current
        /// <see cref="Optional{T}"/> instance has an underlying value; otherwise, <see cref="Optional{TResult}.None"/>.
        /// </returns>
        public async ValueTask<Optional<TResult>> SelectAsync<TResult>(Func<T, CancellationToken, Task<TResult>> selector, CancellationToken cancellationToken = default) where TResult : notnull =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Optional{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="Optional{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns a new <see cref="Optional{TResult}"/> instance containing the result of the delegate if the current
        /// <see cref="Optional{T}"/> instance has an underlying value; otherwise, <see cref="Optional{TResult}.None"/>.
        /// </returns>
        public async ValueTask<Optional<TResult>> SelectAsync<TResult>(Func<T, CancellationToken, ValueTask<TResult>> selector, CancellationToken cancellationToken = default) where TResult : notnull =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Optional{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="Optional{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns a new <see cref="Optional{TResult}"/> instance containing the result of the delegate if the current
        /// <see cref="Optional{T}"/> instance has an underlying value; otherwise, <see cref="Optional{TResult}.None"/>.
        /// </returns>
        public async ValueTask<Optional<TResult>> SelectManyAsync<TResult>(Func<T, Optional<TResult>> selector, CancellationToken cancellationToken = default) where TResult : notnull =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectMany(selector);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Optional{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="Optional{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns a new <see cref="Optional{TResult}"/> instance containing the result of the delegate if the current
        /// <see cref="Optional{T}"/> instance has an underlying value; otherwise, <see cref="Optional{TResult}.None"/>.
        /// </returns>
        public async ValueTask<Optional<TResult>> SelectManyAsync<TResult>(Func<T, Task<Optional<TResult>>> selector, CancellationToken cancellationToken = default) where TResult : notnull =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectManyAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Optional{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="Optional{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns a new <see cref="Optional{TResult}"/> instance containing the result of the delegate if the current
        /// <see cref="Optional{T}"/> instance has an underlying value; otherwise, <see cref="Optional{TResult}.None"/>.
        /// </returns>
        public async ValueTask<Optional<TResult>> SelectManyAsync<TResult>(Func<T, ValueTask<Optional<TResult>>> selector, CancellationToken cancellationToken = default) where TResult : notnull =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectManyAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Optional{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="Optional{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns a new <see cref="Optional{TResult}"/> instance containing the result of the delegate if the current
        /// <see cref="Optional{T}"/> instance has an underlying value; otherwise, <see cref="Optional{TResult}.None"/>.
        /// </returns>
        public async ValueTask<Optional<TResult>> SelectManyAsync<TResult>(Func<T, CancellationToken, Task<Optional<TResult>>> selector, CancellationToken cancellationToken = default) where TResult : notnull =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectManyAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Optional{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="Optional{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns a new <see cref="Optional{TResult}"/> instance containing the result of the delegate if the current
        /// <see cref="Optional{T}"/> instance has an underlying value; otherwise, <see cref="Optional{TResult}.None"/>.
        /// </returns>
        public async ValueTask<Optional<TResult>> SelectManyAsync<TResult>(Func<T, CancellationToken, ValueTask<Optional<TResult>>> selector, CancellationToken cancellationToken = default) where TResult : notnull =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectManyAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously wraps the current <see cref="Optional{T}"/> instance into new, successful <see cref="Result{T}"/> instance.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>Returns a new, successful <see cref="Result{T}"/> instance containing the current <see cref="Optional{T}"/> instance.</returns>
        public async ValueTask<Result<Optional<T>>> ToResultAsync(CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).ToResult();
    }
}
