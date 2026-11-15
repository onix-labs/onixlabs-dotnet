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
/// Provides extension methods for <see cref="ValueResult"/> and <see cref="ValueResult{T}"/> instances.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public static class ValueResultExtensions
{
    /// <summary>
    /// Provides extension methods for <see cref="ValueResult{T}"/> instances whose values are not null.
    /// </summary>
    /// <param name="receiver">The current <see cref="ValueResult{T}"/> instance.</param>
    extension<T>(ValueResult<T> receiver) where T : notnull
    {
        /// <summary>
        /// Gets the <see cref="ValueOptional{T}"/> value of the current <see cref="ValueResult{T}"/> instance,
        /// or <see cref="ValueOptional{T}.None"/> if the result is in a failed state.
        /// </summary>
        /// <returns>
        /// Returns the <see cref="ValueOptional{T}"/> value of the current <see cref="ValueResult{T}"/> instance,
        /// or <see cref="ValueOptional{T}.None"/> if the result is in a failed state.
        /// </returns>
        public ValueOptional<T> GetValueOrNone() =>
            receiver.IsSuccess ? receiver.GetValueOrDefault() : ValueOptional<T>.None;
    }

    /// <summary>
    /// Provides extension methods for <see cref="ValueResult{T}"/> of <see cref="ValueOptional{T}"/> instances.
    /// </summary>
    /// <param name="receiver">The current <see cref="ValueResult{T}"/> of <see cref="ValueOptional{T}"/> instance.</param>
    extension<T>(ValueResult<ValueOptional<T>> receiver) where T : notnull
    {
        /// <summary>
        /// Gets the <see cref="ValueOptional{T}"/> value of the current <see cref="ValueResult{T}"/> instance,
        /// or <see cref="ValueOptional{T}.None"/> if the result is in a failed state.
        /// </summary>
        /// <returns>
        /// Returns the <see cref="ValueOptional{T}"/> value of the current <see cref="ValueResult{T}"/> instance,
        /// or <see cref="ValueOptional{T}.None"/> if the result is in a failed state.
        /// </returns>
        public ValueOptional<T> GetValueOrNone() =>
            receiver.GetValueOrDefault();

        /// <summary>
        /// Gets the underlying <typeparamref name="T"/> value from the current <see cref="ValueResult{T}"/> of <see cref="ValueOptional{T}"/>,
        /// or throws an exception if the result is in a failed state, or the <see cref="ValueOptional{T}"/> is <see cref="ValueOptional{T}.None"/>.
        /// </summary>
        /// <returns>
        /// Returns the underlying <typeparamref name="T"/> value from the current <see cref="ValueResult{T}"/> of <see cref="ValueOptional{T}"/>,
        /// or throws an exception if the result is in a failed state, or the <see cref="ValueOptional{T}"/> is <see cref="ValueOptional{T}.None"/>.
        /// </returns>
        public T GetOptionalValueOrThrow() =>
            receiver.GetValueOrThrow().GetValueOrThrow();

        /// <summary>
        /// Gets the underlying <typeparamref name="T"/> value from the current <see cref="ValueResult{T}"/> of <see cref="ValueOptional{T}"/>,
        /// or returns the default value if the result is in a failed state, or the <see cref="ValueOptional{T}"/> is <see cref="ValueOptional{T}.None"/>.
        /// </summary>
        /// <param name="defaultValue">The default value to return if the result is in a failed state, or the <see cref="ValueOptional{T}"/> is
        /// <see cref="ValueOptional{T}.None"/>.</param>
        /// <returns>
        /// Returns the underlying <typeparamref name="T"/> value from the current <see cref="ValueResult{T}"/> of <see cref="ValueOptional{T}"/>,
        /// or returns the default value if the result is in a failed state, or the <see cref="ValueOptional{T}"/> is <see cref="ValueOptional{T}.None"/>.
        /// </returns>
        public T GetOptionalValueOrDefault(T defaultValue) =>
            receiver.GetValueOrDefault().GetValueOrDefault(defaultValue);
    }

    /// <summary>
    /// Provides extension methods for <see cref="Task{TResult}"/> of <see cref="ValueResult"/> instances.
    /// </summary>
    /// <param name="receiver">The current <see cref="Task{TResult}"/> of <see cref="ValueResult"/> instance.</param>
    extension(Task<ValueResult> receiver)
    {
        /// <summary>
        /// Asynchronously gets the underlying exception if the current <see cref="ValueResult"/> is in a failed state,
        /// or <see langword="null"/> if the current <see cref="ValueResult"/> is in a successful state.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns the underlying exception if the current <see cref="ValueResult"/> is in a failed state,
        /// or <see langword="null"/> if the current <see cref="ValueResult"/> is in a successful state.
        /// </returns>
        public async Task<Exception?> GetExceptionOrDefaultAsync(CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).GetExceptionOrDefault();

        /// <summary>
        /// Asynchronously gets the underlying exception if the current <see cref="ValueResult"/> is in a failed state,
        /// or the specified default exception if the current <see cref="ValueResult"/> is in a successful state.
        /// </summary>
        /// <param name="defaultException">The default exception to return in the event that the current <see cref="ValueResult"/> is in a successful
        /// state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns the underlying exception if the current <see cref="ValueResult"/> is in a failed state,
        /// or the specified default exception if the current <see cref="ValueResult"/> is in a successful state.
        /// </returns>
        public async Task<Exception> GetExceptionOrDefaultAsync(Exception defaultException, CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).GetExceptionOrDefault(defaultException);

        /// <summary>
        /// Asynchronously gets the underlying exception if the current <see cref="ValueResult"/> is in a failed state,
        /// or throws <see cref="InvalidOperationException"/> if the current <see cref="ValueResult"/> is in a successful state.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns the underlying exception if the current <see cref="ValueResult"/> is in a failed state,
        /// or throws <see cref="InvalidOperationException"/> if the current <see cref="ValueResult"/> is in a successful state.
        /// </returns>
        public async Task<Exception> GetExceptionOrThrowAsync(CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).GetExceptionOrThrow();

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async Task MatchAsync(Action? success = null, Action<Exception>? failure = null, CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).Match(success, failure);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async Task MatchAsync(Func<Task>? success = null, Action<Exception>? failure = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async Task MatchAsync(Func<ValueTask>? success = null, Action<Exception>? failure = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async Task MatchAsync(
            Func<CancellationToken, Task>? success = null,
            Action<Exception>? failure = null,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async Task MatchAsync(
            Func<CancellationToken, ValueTask>? success = null,
            Action<Exception>? failure = null,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async Task MatchAsync(Action? success = null, Func<Exception, Task>? failure = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async Task MatchAsync(Action? success = null, Func<Exception, ValueTask>? failure = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async Task MatchAsync(
            Action? success = null,
            Func<Exception, CancellationToken, Task>? failure = null,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async Task MatchAsync(
            Action? success = null,
            Func<Exception, CancellationToken, ValueTask>? failure = null,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async Task MatchAsync(Func<Task>? success = null, Func<Exception, Task>? failure = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async Task MatchAsync(
            Func<ValueTask>? success = null,
            Func<Exception, ValueTask>? failure = null,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async Task MatchAsync(
            Func<CancellationToken, Task>? success = null,
            Func<Exception, CancellationToken, Task>? failure = null,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async Task MatchAsync(
            Func<CancellationToken, ValueTask>? success = null,
            Func<Exception, CancellationToken, ValueTask>? failure = null,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="ValueResult"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="ValueResult"/> instance is in a failed state.
        /// </returns>
        public async Task<TResult> MatchAsync<TResult>(
            Func<TResult> success,
            Func<Exception, TResult> failure,
            CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).Match(success, failure);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="ValueResult"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="ValueResult"/> instance is in a failed state.
        /// </returns>
        public async Task<TResult> MatchAsync<TResult>(
            Func<Task<TResult>> success,
            Func<Exception, TResult> failure,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="ValueResult"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="ValueResult"/> instance is in a failed state.
        /// </returns>
        public async Task<TResult> MatchAsync<TResult>(
            Func<ValueTask<TResult>> success,
            Func<Exception, TResult> failure,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="ValueResult"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="ValueResult"/> instance is in a failed state.
        /// </returns>
        public async Task<TResult> MatchAsync<TResult>(
            Func<CancellationToken, Task<TResult>> success,
            Func<Exception, TResult> failure,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="ValueResult"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="ValueResult"/> instance is in a failed state.
        /// </returns>
        public async Task<TResult> MatchAsync<TResult>(
            Func<CancellationToken, ValueTask<TResult>> success,
            Func<Exception, TResult> failure,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="ValueResult"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="ValueResult"/> instance is in a failed state.
        /// </returns>
        public async Task<TResult> MatchAsync<TResult>(
            Func<TResult> success,
            Func<Exception, Task<TResult>> failure,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="ValueResult"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="ValueResult"/> instance is in a failed state.
        /// </returns>
        public async Task<TResult> MatchAsync<TResult>(
            Func<TResult> success,
            Func<Exception, ValueTask<TResult>> failure,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="ValueResult"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="ValueResult"/> instance is in a failed state.
        /// </returns>
        public async Task<TResult> MatchAsync<TResult>(
            Func<TResult> success,
            Func<Exception, CancellationToken, Task<TResult>> failure,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="ValueResult"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="ValueResult"/> instance is in a failed state.
        /// </returns>
        public async Task<TResult> MatchAsync<TResult>(
            Func<TResult> success,
            Func<Exception, CancellationToken, ValueTask<TResult>> failure,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="ValueResult"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="ValueResult"/> instance is in a failed state.
        /// </returns>
        public async Task<TResult> MatchAsync<TResult>(
            Func<Task<TResult>> success,
            Func<Exception, Task<TResult>> failure,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="ValueResult"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="ValueResult"/> instance is in a failed state.
        /// </returns>
        public async Task<TResult> MatchAsync<TResult>(
            Func<ValueTask<TResult>> success,
            Func<Exception, ValueTask<TResult>> failure,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="ValueResult"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="ValueResult"/> instance is in a failed state.
        /// </returns>
        public async Task<TResult> MatchAsync<TResult>(
            Func<CancellationToken, Task<TResult>> success,
            Func<Exception, CancellationToken, Task<TResult>> failure,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="ValueResult"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="ValueResult"/> instance is in a failed state.
        /// </returns>
        public async Task<TResult> MatchAsync<TResult>(
            Func<CancellationToken, ValueTask<TResult>> success,
            Func<Exception, CancellationToken, ValueTask<TResult>> failure,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to current <see cref="ValueResult"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns a successful <see cref="ValueResult"/> if the current <see cref="ValueResult"/> is in a successful state, and the delegate invocation is
        /// also successful; otherwise, a failed <see cref="ValueResult"/>.
        /// </returns>
        public async Task<ValueResult> SelectAsync(Action selector, CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).Select(selector);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to current <see cref="ValueResult"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns a successful <see cref="ValueResult"/> if the current <see cref="ValueResult"/> is in a successful state, and the delegate invocation is
        /// also successful; otherwise, a failed <see cref="ValueResult"/>.
        /// </returns>
        public async Task<ValueResult> SelectAsync(Func<Task> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .SelectAsync(selector, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to current <see cref="ValueResult"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns a successful <see cref="ValueResult"/> if the current <see cref="ValueResult"/> is in a successful state, and the delegate invocation is
        /// also successful; otherwise, a failed <see cref="ValueResult"/>.
        /// </returns>
        public async Task<ValueResult> SelectAsync(Func<ValueTask> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .SelectAsync(selector, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to current <see cref="ValueResult"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns a successful <see cref="ValueResult"/> if the current <see cref="ValueResult"/> is in a successful state, and the delegate invocation is
        /// also successful; otherwise, a failed <see cref="ValueResult"/>.
        /// </returns>
        public async Task<ValueResult> SelectAsync(Func<CancellationToken, Task> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .SelectAsync(selector, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to current <see cref="ValueResult"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns a successful <see cref="ValueResult"/> if the current <see cref="ValueResult"/> is in a successful state, and the delegate invocation is
        /// also successful; otherwise, a failed <see cref="ValueResult"/>.
        /// </returns>
        public async Task<ValueResult> SelectAsync(Func<CancellationToken, ValueTask> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .SelectAsync(selector, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the current <see cref="ValueResult"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns a successful <see cref="ValueResult{T}"/> if the current <see cref="ValueResult"/> is in a successful state, and the delegate invocation
        /// is also successful; otherwise, a failed <see cref="ValueResult{T}"/>.
        /// </returns>
        public async Task<ValueResult<TResult>> SelectAsync<TResult>(Func<TResult> selector, CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).Select(selector);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the current <see cref="ValueResult"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns a successful <see cref="ValueResult{T}"/> if the current <see cref="ValueResult"/> is in a successful state, and the delegate invocation
        /// is also successful; otherwise, a failed <see cref="ValueResult{T}"/>.
        /// </returns>
        public async Task<ValueResult<TResult>> SelectAsync<TResult>(Func<Task<TResult>> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .SelectAsync(selector, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the current <see cref="ValueResult"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns a successful <see cref="ValueResult{T}"/> if the current <see cref="ValueResult"/> is in a successful state, and the delegate invocation
        /// is also successful; otherwise, a failed <see cref="ValueResult{T}"/>.
        /// </returns>
        public async Task<ValueResult<TResult>> SelectAsync<TResult>(Func<ValueTask<TResult>> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .SelectAsync(selector, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the current <see cref="ValueResult"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns a successful <see cref="ValueResult{T}"/> if the current <see cref="ValueResult"/> is in a successful state, and the delegate invocation
        /// is also successful; otherwise, a failed <see cref="ValueResult{T}"/>.
        /// </returns>
        public async Task<ValueResult<TResult>> SelectAsync<TResult>(
            Func<CancellationToken, Task<TResult>> selector,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .SelectAsync(selector, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the current <see cref="ValueResult"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns a successful <see cref="ValueResult{T}"/> if the current <see cref="ValueResult"/> is in a successful state, and the delegate invocation
        /// is also successful; otherwise, a failed <see cref="ValueResult{T}"/>.
        /// </returns>
        public async Task<ValueResult<TResult>> SelectAsync<TResult>(
            Func<CancellationToken, ValueTask<TResult>> selector,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .SelectAsync(selector, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the current <see cref="ValueResult"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns a successful <see cref="ValueResult"/> if the current <see cref="ValueResult"/> is in a successful state, and the delegate invocation is
        /// also successful; otherwise, a failed <see cref="ValueResult"/>.
        /// </returns>
        public async Task<ValueResult> SelectManyAsync(Func<ValueResult> selector, CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectMany(selector);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the current <see cref="ValueResult"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns a successful <see cref="ValueResult"/> if the current <see cref="ValueResult"/> is in a successful state, and the delegate invocation is
        /// also successful; otherwise, a failed <see cref="ValueResult"/>.
        /// </returns>
        public async Task<ValueResult> SelectManyAsync(Func<Task<ValueResult>> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .SelectManyAsync(selector, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the current <see cref="ValueResult"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns a successful <see cref="ValueResult"/> if the current <see cref="ValueResult"/> is in a successful state, and the delegate invocation is
        /// also successful; otherwise, a failed <see cref="ValueResult"/>.
        /// </returns>
        public async Task<ValueResult> SelectManyAsync(Func<ValueTask<ValueResult>> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .SelectManyAsync(selector, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the current <see cref="ValueResult"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns a successful <see cref="ValueResult"/> if the current <see cref="ValueResult"/> is in a successful state, and the delegate invocation is
        /// also successful; otherwise, a failed <see cref="ValueResult"/>.
        /// </returns>
        public async Task<ValueResult> SelectManyAsync(Func<CancellationToken, Task<ValueResult>> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .SelectManyAsync(selector, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the current <see cref="ValueResult"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns a successful <see cref="ValueResult"/> if the current <see cref="ValueResult"/> is in a successful state, and the delegate invocation is
        /// also successful; otherwise, a failed <see cref="ValueResult"/>.
        /// </returns>
        public async Task<ValueResult> SelectManyAsync(
            Func<CancellationToken, ValueTask<ValueResult>> selector,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .SelectManyAsync(selector, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the current <see cref="ValueResult"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns a successful <see cref="ValueResult{T}"/> if the current <see cref="ValueResult"/> is in a successful state, and the delegate invocation
        /// is also successful; otherwise, a failed <see cref="ValueResult{T}"/>.
        /// </returns>
        public async Task<ValueResult<TResult>> SelectManyAsync<TResult>(Func<ValueResult<TResult>> selector, CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectMany(selector);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the current <see cref="ValueResult"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns a successful <see cref="ValueResult{T}"/> if the current <see cref="ValueResult"/> is in a successful state, and the delegate invocation
        /// is also successful; otherwise, a failed <see cref="ValueResult{T}"/>.
        /// </returns>
        public async Task<ValueResult<TResult>> SelectManyAsync<TResult>(
            Func<Task<ValueResult<TResult>>> selector,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .SelectManyAsync(selector, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the current <see cref="ValueResult"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns a successful <see cref="ValueResult{T}"/> if the current <see cref="ValueResult"/> is in a successful state, and the delegate invocation
        /// is also successful; otherwise, a failed <see cref="ValueResult{T}"/>.
        /// </returns>
        public async Task<ValueResult<TResult>> SelectManyAsync<TResult>(
            Func<ValueTask<ValueResult<TResult>>> selector,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .SelectManyAsync(selector, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the current <see cref="ValueResult"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns a successful <see cref="ValueResult{T}"/> if the current <see cref="ValueResult"/> is in a successful state, and the delegate invocation
        /// is also successful; otherwise, a failed <see cref="ValueResult{T}"/>.
        /// </returns>
        public async Task<ValueResult<TResult>> SelectManyAsync<TResult>(
            Func<CancellationToken, Task<ValueResult<TResult>>> selector,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .SelectManyAsync(selector, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the current <see cref="ValueResult"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns a successful <see cref="ValueResult{T}"/> if the current <see cref="ValueResult"/> is in a successful state, and the delegate invocation
        /// is also successful; otherwise, a failed <see cref="ValueResult{T}"/>.
        /// </returns>
        public async Task<ValueResult<TResult>> SelectManyAsync<TResult>(
            Func<CancellationToken, ValueTask<ValueResult<TResult>>> selector,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .SelectManyAsync(selector, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously throws the underlying exception if the current <see cref="ValueResult"/> is in a failure state.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async Task ThrowAsync(CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).Throw();
    }

    /// <summary>
    /// Provides extension methods for <see cref="Task{TResult}"/> of <see cref="ValueResult{T}"/> instances.
    /// </summary>
    /// <param name="receiver">The current <see cref="Task{TResult}"/> of <see cref="ValueResult{T}"/> instance.</param>
    extension<T>(Task<ValueResult<T>> receiver)
    {
        /// <summary>
        /// Asynchronously gets the underlying exception if the current <see cref="ValueResult{T}"/> is in a failed state,
        /// or <see langword="null"/> if the current <see cref="ValueResult{T}"/> is in a successful state.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns the underlying exception if the current <see cref="ValueResult{T}"/> is in a failed state,
        /// or <see langword="null"/> if the current <see cref="ValueResult{T}"/> is in a successful state.
        /// </returns>
        public async Task<Exception?> GetExceptionOrDefaultAsync(CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).GetExceptionOrDefault();

        /// <summary>
        /// Asynchronously gets the underlying exception if the current <see cref="ValueResult{T}"/> is in a failed state,
        /// or the specified default exception if the current <see cref="ValueResult{T}"/> is in a successful state.
        /// </summary>
        /// <param name="defaultException">The default exception to return in the event that the current <see cref="ValueResult{T}"/> is in a successful
        /// state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns the underlying exception if the current <see cref="ValueResult{T}"/> is in a failed state,
        /// or the specified default exception if the current <see cref="ValueResult{T}"/> is in a successful state.
        /// </returns>
        public async Task<Exception> GetExceptionOrDefaultAsync(Exception defaultException, CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).GetExceptionOrDefault(defaultException);

        /// <summary>
        /// Asynchronously gets the underlying exception if the current <see cref="ValueResult{T}"/> is in a failed state,
        /// or throws <see cref="InvalidOperationException"/> if the current <see cref="ValueResult{T}"/> is in a successful state.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns the underlying exception if the current <see cref="ValueResult{T}"/> is in a failed state,
        /// or throws <see cref="InvalidOperationException"/> if the current <see cref="ValueResult{T}"/> is in a successful state.
        /// </returns>
        public async Task<Exception> GetExceptionOrThrowAsync(CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).GetExceptionOrThrow();

        /// <summary>
        /// Asynchronously gets the underlying value of the current <see cref="ValueResult{T}"/> instance, if the underlying value is present;
        /// otherwise returns the default <typeparamref name="T"/> value.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns the underlying value of the current <see cref="ValueResult{T}"/> instance, if the underlying value is present;
        /// otherwise returns the default <typeparamref name="T"/> value.
        /// </returns>
        public async Task<T?> GetValueOrDefaultAsync(CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).GetValueOrDefault();

        /// <summary>
        /// Asynchronously gets the underlying value of the current <see cref="ValueResult{T}"/> instance, if the underlying value is present;
        /// otherwise returns the specified default value.
        /// </summary>
        /// <param name="defaultValue">The default value to return in the event that the underlying value is absent.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns the underlying value of the current <see cref="ValueResult{T}"/> instance, if the underlying value is present;
        /// otherwise returns the specified default value.
        /// </returns>
        public async Task<T> GetValueOrDefaultAsync(T defaultValue, CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).GetValueOrDefault(defaultValue);

        /// <summary>
        /// Asynchronously gets the underlying value of the current <see cref="ValueResult{T}"/> instance;
        /// otherwise throws the underlying exception if the current <see cref="ValueResult{T}"/> is in a failed stated.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns the underlying value of the current <see cref="ValueResult{T}"/> instance;
        /// otherwise throws the underlying exception if the current <see cref="ValueResult{T}"/> is in a failed stated.
        /// </returns>
        public async Task<T> GetValueOrThrowAsync(CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).GetValueOrThrow();

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult{T}"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async Task MatchAsync(Action<T>? success = null, Action<Exception>? failure = null, CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).Match(success, failure);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult{T}"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async Task MatchAsync(Func<T, Task>? success = null, Action<Exception>? failure = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult{T}"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async Task MatchAsync(Func<T, ValueTask>? success = null, Action<Exception>? failure = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult{T}"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async Task MatchAsync(
            Func<T, CancellationToken, Task>? success = null,
            Action<Exception>? failure = null,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult{T}"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async Task MatchAsync(
            Func<T, CancellationToken, ValueTask>? success = null,
            Action<Exception>? failure = null,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult{T}"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async Task MatchAsync(Action<T>? success = null, Func<Exception, Task>? failure = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult{T}"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async Task MatchAsync(Action<T>? success = null, Func<Exception, ValueTask>? failure = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult{T}"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async Task MatchAsync(
            Action<T>? success = null,
            Func<Exception, CancellationToken, Task>? failure = null,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult{T}"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async Task MatchAsync(
            Action<T>? success = null,
            Func<Exception, CancellationToken, ValueTask>? failure = null,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult{T}"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async Task MatchAsync(Func<T, Task>? success = null, Func<Exception, Task>? failure = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult{T}"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async Task MatchAsync(
            Func<T, ValueTask>? success = null,
            Func<Exception, ValueTask>? failure = null,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult{T}"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async Task MatchAsync(
            Func<T, CancellationToken, Task>? success = null,
            Func<Exception, CancellationToken, Task>? failure = null,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult{T}"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async Task MatchAsync(
            Func<T, CancellationToken, ValueTask>? success = null,
            Func<Exception, CancellationToken, ValueTask>? failure = null,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="ValueResult{T}"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="ValueResult{T}"/> instance is in a failed state.
        /// </returns>
        public async Task<TResult> MatchAsync<TResult>(
            Func<T, TResult> success,
            Func<Exception, TResult> failure,
            CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).Match(success, failure);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="ValueResult{T}"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="ValueResult{T}"/> instance is in a failed state.
        /// </returns>
        public async Task<TResult> MatchAsync<TResult>(
            Func<T, Task<TResult>> success,
            Func<Exception, TResult> failure,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="ValueResult{T}"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="ValueResult{T}"/> instance is in a failed state.
        /// </returns>
        public async Task<TResult> MatchAsync<TResult>(
            Func<T, ValueTask<TResult>> success,
            Func<Exception, TResult> failure,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="ValueResult{T}"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="ValueResult{T}"/> instance is in a failed state.
        /// </returns>
        public async Task<TResult> MatchAsync<TResult>(
            Func<T, CancellationToken, Task<TResult>> success,
            Func<Exception, TResult> failure,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="ValueResult{T}"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="ValueResult{T}"/> instance is in a failed state.
        /// </returns>
        public async Task<TResult> MatchAsync<TResult>(
            Func<T, CancellationToken, ValueTask<TResult>> success,
            Func<Exception, TResult> failure,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="ValueResult{T}"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="ValueResult{T}"/> instance is in a failed state.
        /// </returns>
        public async Task<TResult> MatchAsync<TResult>(
            Func<T, TResult> success,
            Func<Exception, Task<TResult>> failure,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="ValueResult{T}"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="ValueResult{T}"/> instance is in a failed state.
        /// </returns>
        public async Task<TResult> MatchAsync<TResult>(
            Func<T, TResult> success,
            Func<Exception, ValueTask<TResult>> failure,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="ValueResult{T}"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="ValueResult{T}"/> instance is in a failed state.
        /// </returns>
        public async Task<TResult> MatchAsync<TResult>(
            Func<T, TResult> success,
            Func<Exception, CancellationToken, Task<TResult>> failure,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="ValueResult{T}"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="ValueResult{T}"/> instance is in a failed state.
        /// </returns>
        public async Task<TResult> MatchAsync<TResult>(
            Func<T, TResult> success,
            Func<Exception, CancellationToken, ValueTask<TResult>> failure,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="ValueResult{T}"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="ValueResult{T}"/> instance is in a failed state.
        /// </returns>
        public async Task<TResult> MatchAsync<TResult>(
            Func<T, Task<TResult>> success,
            Func<Exception, Task<TResult>> failure,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="ValueResult{T}"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="ValueResult{T}"/> instance is in a failed state.
        /// </returns>
        public async Task<TResult> MatchAsync<TResult>(
            Func<T, ValueTask<TResult>> success,
            Func<Exception, ValueTask<TResult>> failure,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="ValueResult{T}"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="ValueResult{T}"/> instance is in a failed state.
        /// </returns>
        public async Task<TResult> MatchAsync<TResult>(
            Func<T, CancellationToken, Task<TResult>> success,
            Func<Exception, CancellationToken, Task<TResult>> failure,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="ValueResult{T}"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="ValueResult{T}"/> instance is in a failed state.
        /// </returns>
        public async Task<TResult> MatchAsync<TResult>(
            Func<T, CancellationToken, ValueTask<TResult>> success,
            Func<Exception, CancellationToken, ValueTask<TResult>> failure,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="ValueResult{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns a successful <see cref="ValueResult"/> if the current <see cref="ValueResult{T}"/> is in a successful state, and the delegate invocation
        /// is also successful; otherwise, a failed <see cref="ValueResult"/>.
        /// </returns>
        public async Task<ValueResult> SelectAsync(Action<T> selector, CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).Select(selector);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="ValueResult{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns a successful <see cref="ValueResult"/> if the current <see cref="ValueResult{T}"/> is in a successful state, and the delegate invocation
        /// is also successful; otherwise, a failed <see cref="ValueResult"/>.
        /// </returns>
        public async Task<ValueResult> SelectAsync(Func<T, Task> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .SelectAsync(selector, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="ValueResult{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns a successful <see cref="ValueResult"/> if the current <see cref="ValueResult{T}"/> is in a successful state, and the delegate invocation
        /// is also successful; otherwise, a failed <see cref="ValueResult"/>.
        /// </returns>
        public async Task<ValueResult> SelectAsync(Func<T, ValueTask> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .SelectAsync(selector, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="ValueResult{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns a successful <see cref="ValueResult"/> if the current <see cref="ValueResult{T}"/> is in a successful state, and the delegate invocation
        /// is also successful; otherwise, a failed <see cref="ValueResult"/>.
        /// </returns>
        public async Task<ValueResult> SelectAsync(Func<T, CancellationToken, Task> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .SelectAsync(selector, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="ValueResult{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns a successful <see cref="ValueResult"/> if the current <see cref="ValueResult{T}"/> is in a successful state, and the delegate invocation
        /// is also successful; otherwise, a failed <see cref="ValueResult"/>.
        /// </returns>
        public async Task<ValueResult> SelectAsync(Func<T, CancellationToken, ValueTask> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .SelectAsync(selector, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="ValueResult{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns a successful <see cref="ValueResult{T}"/> if the current <see cref="ValueResult{T}"/> is in a successful state, and the delegate
        /// invocation is also successful; otherwise, a failed <see cref="ValueResult{T}"/>.
        /// </returns>
        public async Task<ValueResult<TResult>> SelectAsync<TResult>(Func<T, TResult> selector, CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).Select(selector);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="ValueResult{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns a successful <see cref="ValueResult{T}"/> if the current <see cref="ValueResult{T}"/> is in a successful state, and the delegate
        /// invocation is also successful; otherwise, a failed <see cref="ValueResult{T}"/>.
        /// </returns>
        public async Task<ValueResult<TResult>> SelectAsync<TResult>(Func<T, Task<TResult>> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .SelectAsync(selector, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="ValueResult{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns a successful <see cref="ValueResult{T}"/> if the current <see cref="ValueResult{T}"/> is in a successful state, and the delegate
        /// invocation is also successful; otherwise, a failed <see cref="ValueResult{T}"/>.
        /// </returns>
        public async Task<ValueResult<TResult>> SelectAsync<TResult>(Func<T, ValueTask<TResult>> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .SelectAsync(selector, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="ValueResult{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns a successful <see cref="ValueResult{T}"/> if the current <see cref="ValueResult{T}"/> is in a successful state, and the delegate
        /// invocation is also successful; otherwise, a failed <see cref="ValueResult{T}"/>.
        /// </returns>
        public async Task<ValueResult<TResult>> SelectAsync<TResult>(
            Func<T, CancellationToken, Task<TResult>> selector,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .SelectAsync(selector, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="ValueResult{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns a successful <see cref="ValueResult{T}"/> if the current <see cref="ValueResult{T}"/> is in a successful state, and the delegate
        /// invocation is also successful; otherwise, a failed <see cref="ValueResult{T}"/>.
        /// </returns>
        public async Task<ValueResult<TResult>> SelectAsync<TResult>(
            Func<T, CancellationToken, ValueTask<TResult>> selector,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .SelectAsync(selector, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="ValueResult{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns a successful <see cref="ValueResult"/> if the current <see cref="ValueResult{T}"/> is in a successful state, and the delegate invocation
        /// is also successful; otherwise, a failed <see cref="ValueResult"/>.
        /// </returns>
        public async Task<ValueResult> SelectManyAsync(Func<T, ValueResult> selector, CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectMany(selector);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="ValueResult{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns a successful <see cref="ValueResult"/> if the current <see cref="ValueResult{T}"/> is in a successful state, and the delegate invocation
        /// is also successful; otherwise, a failed <see cref="ValueResult"/>.
        /// </returns>
        public async Task<ValueResult> SelectManyAsync(Func<T, Task<ValueResult>> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .SelectManyAsync(selector, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="ValueResult{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns a successful <see cref="ValueResult"/> if the current <see cref="ValueResult{T}"/> is in a successful state, and the delegate invocation
        /// is also successful; otherwise, a failed <see cref="ValueResult"/>.
        /// </returns>
        public async Task<ValueResult> SelectManyAsync(Func<T, ValueTask<ValueResult>> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .SelectManyAsync(selector, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="ValueResult{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns a successful <see cref="ValueResult"/> if the current <see cref="ValueResult{T}"/> is in a successful state, and the delegate invocation
        /// is also successful; otherwise, a failed <see cref="ValueResult"/>.
        /// </returns>
        public async Task<ValueResult> SelectManyAsync(Func<T, CancellationToken, Task<ValueResult>> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .SelectManyAsync(selector, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="ValueResult{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns a successful <see cref="ValueResult"/> if the current <see cref="ValueResult{T}"/> is in a successful state, and the delegate invocation
        /// is also successful; otherwise, a failed <see cref="ValueResult"/>.
        /// </returns>
        public async Task<ValueResult> SelectManyAsync(
            Func<T, CancellationToken, ValueTask<ValueResult>> selector,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .SelectManyAsync(selector, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="ValueResult{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns a successful <see cref="ValueResult{T}"/> if the current <see cref="ValueResult{T}"/> is in a successful state, and the delegate
        /// invocation is also successful; otherwise, a failed <see cref="ValueResult{T}"/>.
        /// </returns>
        public async Task<ValueResult<TResult>> SelectManyAsync<TResult>(
            Func<T, ValueResult<TResult>> selector,
            CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectMany(selector);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="ValueResult{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns a successful <see cref="ValueResult{T}"/> if the current <see cref="ValueResult{T}"/> is in a successful state, and the delegate
        /// invocation is also successful; otherwise, a failed <see cref="ValueResult{T}"/>.
        /// </returns>
        public async Task<ValueResult<TResult>> SelectManyAsync<TResult>(
            Func<T, Task<ValueResult<TResult>>> selector,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .SelectManyAsync(selector, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="ValueResult{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns a successful <see cref="ValueResult{T}"/> if the current <see cref="ValueResult{T}"/> is in a successful state, and the delegate
        /// invocation is also successful; otherwise, a failed <see cref="ValueResult{T}"/>.
        /// </returns>
        public async Task<ValueResult<TResult>> SelectManyAsync<TResult>(
            Func<T, ValueTask<ValueResult<TResult>>> selector,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .SelectManyAsync(selector, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="ValueResult{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns a successful <see cref="ValueResult{T}"/> if the current <see cref="ValueResult{T}"/> is in a successful state, and the delegate
        /// invocation is also successful; otherwise, a failed <see cref="ValueResult{T}"/>.
        /// </returns>
        public async Task<ValueResult<TResult>> SelectManyAsync<TResult>(
            Func<T, CancellationToken, Task<ValueResult<TResult>>> selector,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .SelectManyAsync(selector, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="ValueResult{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns a successful <see cref="ValueResult{T}"/> if the current <see cref="ValueResult{T}"/> is in a successful state, and the delegate
        /// invocation is also successful; otherwise, a failed <see cref="ValueResult{T}"/>.
        /// </returns>
        public async Task<ValueResult<TResult>> SelectManyAsync<TResult>(
            Func<T, CancellationToken, ValueTask<ValueResult<TResult>>> selector,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .SelectManyAsync(selector, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously throws the underlying exception if the current <see cref="ValueResult{T}"/> is in a failure state.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async Task ThrowAsync(CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).Throw();
    }

    /// <summary>
    /// Provides extension methods for <see cref="Task{TResult}"/> of <see cref="ValueResult{T}"/> instances whose values are not null.
    /// </summary>
    /// <param name="receiver">The current <see cref="Task{TResult}"/> of <see cref="ValueResult{T}"/> instance.</param>
    extension<T>(Task<ValueResult<T>> receiver) where T : notnull
    {
        /// <summary>
        /// Asynchronously gets the <see cref="ValueOptional{T}"/> value of the current <see cref="ValueResult{T}"/> instance,
        /// or <see cref="ValueOptional{T}.None"/> if the result is in a failed state.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns the <see cref="ValueOptional{T}"/> value of the current <see cref="ValueResult{T}"/> instance,
        /// or <see cref="ValueOptional{T}.None"/> if the result is in a failed state.
        /// </returns>
        public async Task<ValueOptional<T>> GetValueOrNoneAsync(CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).GetValueOrNone();
    }

    /// <summary>
    /// Provides extension methods for <see cref="Task{TResult}"/> of <see cref="ValueResult{T}"/> of <see cref="ValueOptional{T}"/> instances.
    /// </summary>
    /// <param name="receiver">The current <see cref="Task{TResult}"/> of <see cref="ValueResult{T}"/> of <see cref="ValueOptional{T}"/> instance.</param>
    extension<T>(Task<ValueResult<ValueOptional<T>>> receiver) where T : notnull
    {
        /// <summary>
        /// Asynchronously gets the <see cref="ValueOptional{T}"/> value of the current <see cref="ValueResult{T}"/> instance,
        /// or <see cref="ValueOptional{T}.None"/> if the result is in a failed state.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns the <see cref="ValueOptional{T}"/> value of the current <see cref="ValueResult{T}"/> instance,
        /// or <see cref="ValueOptional{T}.None"/> if the result is in a failed state.
        /// </returns>
        public async Task<ValueOptional<T>> GetValueOrNoneAsync(CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).GetValueOrNone();

        /// <summary>
        /// Asynchronously gets the underlying <typeparamref name="T"/> value from the current <see cref="ValueResult{T}"/> of <see cref="ValueOptional{T}"/>,
        /// or throws an exception if the result is in a failed state, or the <see cref="ValueOptional{T}"/> is <see cref="ValueOptional{T}.None"/>.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns the underlying <typeparamref name="T"/> value from the current <see cref="ValueResult{T}"/> of <see cref="ValueOptional{T}"/>,
        /// or throws an exception if the result is in a failed state, or the <see cref="ValueOptional{T}"/> is <see cref="ValueOptional{T}.None"/>.
        /// </returns>
        public async Task<T> GetOptionalValueOrThrowAsync(CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).GetOptionalValueOrThrow();

        /// <summary>
        /// Asynchronously gets the underlying <typeparamref name="T"/> value from the current <see cref="ValueResult{T}"/> of <see cref="ValueOptional{T}"/>,
        /// or returns the default value if the result is in a failed state, or the <see cref="ValueOptional{T}"/> is <see cref="ValueOptional{T}.None"/>.
        /// </summary>
        /// <param name="defaultValue">The default value to return if the result is in a failed state, or the <see cref="ValueOptional{T}"/> is
        /// <see cref="ValueOptional{T}.None"/>.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns the underlying <typeparamref name="T"/> value from the current <see cref="ValueResult{T}"/> of <see cref="ValueOptional{T}"/>,
        /// or returns the default value if the result is in a failed state, or the <see cref="ValueOptional{T}"/> is <see cref="ValueOptional{T}.None"/>.
        /// </returns>
        public async Task<T> GetOptionalValueOrDefaultAsync(T defaultValue, CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).GetOptionalValueOrDefault(defaultValue);
    }

    /// <summary>
    /// Provides extension methods for <see cref="ValueTask{TResult}"/> of <see cref="ValueResult"/> instances.
    /// </summary>
    /// <param name="receiver">The current <see cref="ValueTask{TResult}"/> of <see cref="ValueResult"/> instance.</param>
    extension(ValueTask<ValueResult> receiver)
    {
        /// <summary>
        /// Asynchronously gets the underlying exception if the current <see cref="ValueResult"/> is in a failed state,
        /// or <see langword="null"/> if the current <see cref="ValueResult"/> is in a successful state.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns the underlying exception if the current <see cref="ValueResult"/> is in a failed state,
        /// or <see langword="null"/> if the current <see cref="ValueResult"/> is in a successful state.
        /// </returns>
        public async ValueTask<Exception?> GetExceptionOrDefaultAsync(CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).GetExceptionOrDefault();

        /// <summary>
        /// Asynchronously gets the underlying exception if the current <see cref="ValueResult"/> is in a failed state,
        /// or the specified default exception if the current <see cref="ValueResult"/> is in a successful state.
        /// </summary>
        /// <param name="defaultException">The default exception to return in the event that the current <see cref="ValueResult"/> is in a successful
        /// state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns the underlying exception if the current <see cref="ValueResult"/> is in a failed state,
        /// or the specified default exception if the current <see cref="ValueResult"/> is in a successful state.
        /// </returns>
        public async ValueTask<Exception> GetExceptionOrDefaultAsync(Exception defaultException, CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).GetExceptionOrDefault(defaultException);

        /// <summary>
        /// Asynchronously gets the underlying exception if the current <see cref="ValueResult"/> is in a failed state,
        /// or throws <see cref="InvalidOperationException"/> if the current <see cref="ValueResult"/> is in a successful state.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns the underlying exception if the current <see cref="ValueResult"/> is in a failed state,
        /// or throws <see cref="InvalidOperationException"/> if the current <see cref="ValueResult"/> is in a successful state.
        /// </returns>
        public async ValueTask<Exception> GetExceptionOrThrowAsync(CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).GetExceptionOrThrow();

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async ValueTask MatchAsync(Action? success = null, Action<Exception>? failure = null, CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).Match(success, failure);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async ValueTask MatchAsync(Func<Task>? success = null, Action<Exception>? failure = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async ValueTask MatchAsync(Func<ValueTask>? success = null, Action<Exception>? failure = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async ValueTask MatchAsync(
            Func<CancellationToken, Task>? success = null,
            Action<Exception>? failure = null,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async ValueTask MatchAsync(
            Func<CancellationToken, ValueTask>? success = null,
            Action<Exception>? failure = null,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async ValueTask MatchAsync(Action? success = null, Func<Exception, Task>? failure = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async ValueTask MatchAsync(Action? success = null, Func<Exception, ValueTask>? failure = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async ValueTask MatchAsync(
            Action? success = null,
            Func<Exception, CancellationToken, Task>? failure = null,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async ValueTask MatchAsync(
            Action? success = null,
            Func<Exception, CancellationToken, ValueTask>? failure = null,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async ValueTask MatchAsync(Func<Task>? success = null, Func<Exception, Task>? failure = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async ValueTask MatchAsync(
            Func<ValueTask>? success = null,
            Func<Exception, ValueTask>? failure = null,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async ValueTask MatchAsync(
            Func<CancellationToken, Task>? success = null,
            Func<Exception, CancellationToken, Task>? failure = null,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async ValueTask MatchAsync(
            Func<CancellationToken, ValueTask>? success = null,
            Func<Exception, CancellationToken, ValueTask>? failure = null,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="ValueResult"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="ValueResult"/> instance is in a failed state.
        /// </returns>
        public async ValueTask<TResult> MatchAsync<TResult>(
            Func<TResult> success,
            Func<Exception, TResult> failure,
            CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).Match(success, failure);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="ValueResult"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="ValueResult"/> instance is in a failed state.
        /// </returns>
        public async ValueTask<TResult> MatchAsync<TResult>(
            Func<Task<TResult>> success,
            Func<Exception, TResult> failure,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="ValueResult"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="ValueResult"/> instance is in a failed state.
        /// </returns>
        public async ValueTask<TResult> MatchAsync<TResult>(
            Func<ValueTask<TResult>> success,
            Func<Exception, TResult> failure,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="ValueResult"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="ValueResult"/> instance is in a failed state.
        /// </returns>
        public async ValueTask<TResult> MatchAsync<TResult>(
            Func<CancellationToken, Task<TResult>> success,
            Func<Exception, TResult> failure,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="ValueResult"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="ValueResult"/> instance is in a failed state.
        /// </returns>
        public async ValueTask<TResult> MatchAsync<TResult>(
            Func<CancellationToken, ValueTask<TResult>> success,
            Func<Exception, TResult> failure,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="ValueResult"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="ValueResult"/> instance is in a failed state.
        /// </returns>
        public async ValueTask<TResult> MatchAsync<TResult>(
            Func<TResult> success,
            Func<Exception, Task<TResult>> failure,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="ValueResult"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="ValueResult"/> instance is in a failed state.
        /// </returns>
        public async ValueTask<TResult> MatchAsync<TResult>(
            Func<TResult> success,
            Func<Exception, ValueTask<TResult>> failure,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="ValueResult"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="ValueResult"/> instance is in a failed state.
        /// </returns>
        public async ValueTask<TResult> MatchAsync<TResult>(
            Func<TResult> success,
            Func<Exception, CancellationToken, Task<TResult>> failure,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="ValueResult"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="ValueResult"/> instance is in a failed state.
        /// </returns>
        public async ValueTask<TResult> MatchAsync<TResult>(
            Func<TResult> success,
            Func<Exception, CancellationToken, ValueTask<TResult>> failure,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="ValueResult"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="ValueResult"/> instance is in a failed state.
        /// </returns>
        public async ValueTask<TResult> MatchAsync<TResult>(
            Func<Task<TResult>> success,
            Func<Exception, Task<TResult>> failure,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="ValueResult"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="ValueResult"/> instance is in a failed state.
        /// </returns>
        public async ValueTask<TResult> MatchAsync<TResult>(
            Func<ValueTask<TResult>> success,
            Func<Exception, ValueTask<TResult>> failure,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="ValueResult"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="ValueResult"/> instance is in a failed state.
        /// </returns>
        public async ValueTask<TResult> MatchAsync<TResult>(
            Func<CancellationToken, Task<TResult>> success,
            Func<Exception, CancellationToken, Task<TResult>> failure,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="ValueResult"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="ValueResult"/> instance is in a failed state.
        /// </returns>
        public async ValueTask<TResult> MatchAsync<TResult>(
            Func<CancellationToken, ValueTask<TResult>> success,
            Func<Exception, CancellationToken, ValueTask<TResult>> failure,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to current <see cref="ValueResult"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns a successful <see cref="ValueResult"/> if the current <see cref="ValueResult"/> is in a successful state, and the delegate invocation is
        /// also successful; otherwise, a failed <see cref="ValueResult"/>.
        /// </returns>
        public async ValueTask<ValueResult> SelectAsync(Action selector, CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).Select(selector);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to current <see cref="ValueResult"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns a successful <see cref="ValueResult"/> if the current <see cref="ValueResult"/> is in a successful state, and the delegate invocation is
        /// also successful; otherwise, a failed <see cref="ValueResult"/>.
        /// </returns>
        public async ValueTask<ValueResult> SelectAsync(Func<Task> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .SelectAsync(selector, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to current <see cref="ValueResult"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns a successful <see cref="ValueResult"/> if the current <see cref="ValueResult"/> is in a successful state, and the delegate invocation is
        /// also successful; otherwise, a failed <see cref="ValueResult"/>.
        /// </returns>
        public async ValueTask<ValueResult> SelectAsync(Func<ValueTask> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .SelectAsync(selector, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to current <see cref="ValueResult"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns a successful <see cref="ValueResult"/> if the current <see cref="ValueResult"/> is in a successful state, and the delegate invocation is
        /// also successful; otherwise, a failed <see cref="ValueResult"/>.
        /// </returns>
        public async ValueTask<ValueResult> SelectAsync(Func<CancellationToken, Task> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .SelectAsync(selector, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to current <see cref="ValueResult"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns a successful <see cref="ValueResult"/> if the current <see cref="ValueResult"/> is in a successful state, and the delegate invocation is
        /// also successful; otherwise, a failed <see cref="ValueResult"/>.
        /// </returns>
        public async ValueTask<ValueResult> SelectAsync(Func<CancellationToken, ValueTask> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .SelectAsync(selector, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the current <see cref="ValueResult"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns a successful <see cref="ValueResult{T}"/> if the current <see cref="ValueResult"/> is in a successful state, and the delegate invocation
        /// is also successful; otherwise, a failed <see cref="ValueResult{T}"/>.
        /// </returns>
        public async ValueTask<ValueResult<TResult>> SelectAsync<TResult>(Func<TResult> selector, CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).Select(selector);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the current <see cref="ValueResult"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns a successful <see cref="ValueResult{T}"/> if the current <see cref="ValueResult"/> is in a successful state, and the delegate invocation
        /// is also successful; otherwise, a failed <see cref="ValueResult{T}"/>.
        /// </returns>
        public async ValueTask<ValueResult<TResult>> SelectAsync<TResult>(Func<Task<TResult>> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .SelectAsync(selector, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the current <see cref="ValueResult"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns a successful <see cref="ValueResult{T}"/> if the current <see cref="ValueResult"/> is in a successful state, and the delegate invocation
        /// is also successful; otherwise, a failed <see cref="ValueResult{T}"/>.
        /// </returns>
        public async ValueTask<ValueResult<TResult>> SelectAsync<TResult>(Func<ValueTask<TResult>> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .SelectAsync(selector, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the current <see cref="ValueResult"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns a successful <see cref="ValueResult{T}"/> if the current <see cref="ValueResult"/> is in a successful state, and the delegate invocation
        /// is also successful; otherwise, a failed <see cref="ValueResult{T}"/>.
        /// </returns>
        public async ValueTask<ValueResult<TResult>> SelectAsync<TResult>(
            Func<CancellationToken, Task<TResult>> selector,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .SelectAsync(selector, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the current <see cref="ValueResult"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns a successful <see cref="ValueResult{T}"/> if the current <see cref="ValueResult"/> is in a successful state, and the delegate invocation
        /// is also successful; otherwise, a failed <see cref="ValueResult{T}"/>.
        /// </returns>
        public async ValueTask<ValueResult<TResult>> SelectAsync<TResult>(
            Func<CancellationToken, ValueTask<TResult>> selector,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .SelectAsync(selector, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the current <see cref="ValueResult"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns a successful <see cref="ValueResult"/> if the current <see cref="ValueResult"/> is in a successful state, and the delegate invocation is
        /// also successful; otherwise, a failed <see cref="ValueResult"/>.
        /// </returns>
        public async ValueTask<ValueResult> SelectManyAsync(Func<ValueResult> selector, CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectMany(selector);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the current <see cref="ValueResult"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns a successful <see cref="ValueResult"/> if the current <see cref="ValueResult"/> is in a successful state, and the delegate invocation is
        /// also successful; otherwise, a failed <see cref="ValueResult"/>.
        /// </returns>
        public async ValueTask<ValueResult> SelectManyAsync(Func<Task<ValueResult>> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .SelectManyAsync(selector, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the current <see cref="ValueResult"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns a successful <see cref="ValueResult"/> if the current <see cref="ValueResult"/> is in a successful state, and the delegate invocation is
        /// also successful; otherwise, a failed <see cref="ValueResult"/>.
        /// </returns>
        public async ValueTask<ValueResult> SelectManyAsync(Func<ValueTask<ValueResult>> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .SelectManyAsync(selector, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the current <see cref="ValueResult"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns a successful <see cref="ValueResult"/> if the current <see cref="ValueResult"/> is in a successful state, and the delegate invocation is
        /// also successful; otherwise, a failed <see cref="ValueResult"/>.
        /// </returns>
        public async ValueTask<ValueResult> SelectManyAsync(
            Func<CancellationToken, Task<ValueResult>> selector,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .SelectManyAsync(selector, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the current <see cref="ValueResult"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns a successful <see cref="ValueResult"/> if the current <see cref="ValueResult"/> is in a successful state, and the delegate invocation is
        /// also successful; otherwise, a failed <see cref="ValueResult"/>.
        /// </returns>
        public async ValueTask<ValueResult> SelectManyAsync(
            Func<CancellationToken, ValueTask<ValueResult>> selector,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .SelectManyAsync(selector, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the current <see cref="ValueResult"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns a successful <see cref="ValueResult{T}"/> if the current <see cref="ValueResult"/> is in a successful state, and the delegate invocation
        /// is also successful; otherwise, a failed <see cref="ValueResult{T}"/>.
        /// </returns>
        public async ValueTask<ValueResult<TResult>> SelectManyAsync<TResult>(
            Func<ValueResult<TResult>> selector,
            CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectMany(selector);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the current <see cref="ValueResult"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns a successful <see cref="ValueResult{T}"/> if the current <see cref="ValueResult"/> is in a successful state, and the delegate invocation
        /// is also successful; otherwise, a failed <see cref="ValueResult{T}"/>.
        /// </returns>
        public async ValueTask<ValueResult<TResult>> SelectManyAsync<TResult>(
            Func<Task<ValueResult<TResult>>> selector,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .SelectManyAsync(selector, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the current <see cref="ValueResult"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns a successful <see cref="ValueResult{T}"/> if the current <see cref="ValueResult"/> is in a successful state, and the delegate invocation
        /// is also successful; otherwise, a failed <see cref="ValueResult{T}"/>.
        /// </returns>
        public async ValueTask<ValueResult<TResult>> SelectManyAsync<TResult>(
            Func<ValueTask<ValueResult<TResult>>> selector,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .SelectManyAsync(selector, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the current <see cref="ValueResult"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns a successful <see cref="ValueResult{T}"/> if the current <see cref="ValueResult"/> is in a successful state, and the delegate invocation
        /// is also successful; otherwise, a failed <see cref="ValueResult{T}"/>.
        /// </returns>
        public async ValueTask<ValueResult<TResult>> SelectManyAsync<TResult>(
            Func<CancellationToken, Task<ValueResult<TResult>>> selector,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .SelectManyAsync(selector, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the current <see cref="ValueResult"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns a successful <see cref="ValueResult{T}"/> if the current <see cref="ValueResult"/> is in a successful state, and the delegate invocation
        /// is also successful; otherwise, a failed <see cref="ValueResult{T}"/>.
        /// </returns>
        public async ValueTask<ValueResult<TResult>> SelectManyAsync<TResult>(
            Func<CancellationToken, ValueTask<ValueResult<TResult>>> selector,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .SelectManyAsync(selector, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously throws the underlying exception if the current <see cref="ValueResult"/> is in a failure state.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async ValueTask ThrowAsync(CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).Throw();
    }

    /// <summary>
    /// Provides extension methods for <see cref="ValueTask{TResult}"/> of <see cref="ValueResult{T}"/> instances.
    /// </summary>
    /// <param name="receiver">The current <see cref="ValueTask{TResult}"/> of <see cref="ValueResult{T}"/> instance.</param>
    extension<T>(ValueTask<ValueResult<T>> receiver)
    {
        /// <summary>
        /// Asynchronously gets the underlying exception if the current <see cref="ValueResult{T}"/> is in a failed state,
        /// or <see langword="null"/> if the current <see cref="ValueResult{T}"/> is in a successful state.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns the underlying exception if the current <see cref="ValueResult{T}"/> is in a failed state,
        /// or <see langword="null"/> if the current <see cref="ValueResult{T}"/> is in a successful state.
        /// </returns>
        public async ValueTask<Exception?> GetExceptionOrDefaultAsync(CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).GetExceptionOrDefault();

        /// <summary>
        /// Asynchronously gets the underlying exception if the current <see cref="ValueResult{T}"/> is in a failed state,
        /// or the specified default exception if the current <see cref="ValueResult{T}"/> is in a successful state.
        /// </summary>
        /// <param name="defaultException">The default exception to return in the event that the current <see cref="ValueResult{T}"/> is in a successful
        /// state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns the underlying exception if the current <see cref="ValueResult{T}"/> is in a failed state,
        /// or the specified default exception if the current <see cref="ValueResult{T}"/> is in a successful state.
        /// </returns>
        public async ValueTask<Exception> GetExceptionOrDefaultAsync(Exception defaultException, CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).GetExceptionOrDefault(defaultException);

        /// <summary>
        /// Asynchronously gets the underlying exception if the current <see cref="ValueResult{T}"/> is in a failed state,
        /// or throws <see cref="InvalidOperationException"/> if the current <see cref="ValueResult{T}"/> is in a successful state.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns the underlying exception if the current <see cref="ValueResult{T}"/> is in a failed state,
        /// or throws <see cref="InvalidOperationException"/> if the current <see cref="ValueResult{T}"/> is in a successful state.
        /// </returns>
        public async ValueTask<Exception> GetExceptionOrThrowAsync(CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).GetExceptionOrThrow();

        /// <summary>
        /// Asynchronously gets the underlying value of the current <see cref="ValueResult{T}"/> instance, if the underlying value is present;
        /// otherwise returns the default <typeparamref name="T"/> value.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns the underlying value of the current <see cref="ValueResult{T}"/> instance, if the underlying value is present;
        /// otherwise returns the default <typeparamref name="T"/> value.
        /// </returns>
        public async ValueTask<T?> GetValueOrDefaultAsync(CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).GetValueOrDefault();

        /// <summary>
        /// Asynchronously gets the underlying value of the current <see cref="ValueResult{T}"/> instance, if the underlying value is present;
        /// otherwise returns the specified default value.
        /// </summary>
        /// <param name="defaultValue">The default value to return in the event that the underlying value is absent.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns the underlying value of the current <see cref="ValueResult{T}"/> instance, if the underlying value is present;
        /// otherwise returns the specified default value.
        /// </returns>
        public async ValueTask<T> GetValueOrDefaultAsync(T defaultValue, CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).GetValueOrDefault(defaultValue);

        /// <summary>
        /// Asynchronously gets the underlying value of the current <see cref="ValueResult{T}"/> instance;
        /// otherwise throws the underlying exception if the current <see cref="ValueResult{T}"/> is in a failed stated.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns the underlying value of the current <see cref="ValueResult{T}"/> instance;
        /// otherwise throws the underlying exception if the current <see cref="ValueResult{T}"/> is in a failed stated.
        /// </returns>
        public async ValueTask<T> GetValueOrThrowAsync(CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).GetValueOrThrow();

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult{T}"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async ValueTask MatchAsync(Action<T>? success = null, Action<Exception>? failure = null, CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).Match(success, failure);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult{T}"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async ValueTask MatchAsync(Func<T, Task>? success = null, Action<Exception>? failure = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult{T}"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async ValueTask MatchAsync(
            Func<T, ValueTask>? success = null,
            Action<Exception>? failure = null,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult{T}"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async ValueTask MatchAsync(
            Func<T, CancellationToken, Task>? success = null,
            Action<Exception>? failure = null,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult{T}"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async ValueTask MatchAsync(
            Func<T, CancellationToken, ValueTask>? success = null,
            Action<Exception>? failure = null,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult{T}"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async ValueTask MatchAsync(Action<T>? success = null, Func<Exception, Task>? failure = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult{T}"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async ValueTask MatchAsync(
            Action<T>? success = null,
            Func<Exception, ValueTask>? failure = null,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult{T}"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async ValueTask MatchAsync(
            Action<T>? success = null,
            Func<Exception, CancellationToken, Task>? failure = null,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult{T}"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async ValueTask MatchAsync(
            Action<T>? success = null,
            Func<Exception, CancellationToken, ValueTask>? failure = null,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult{T}"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async ValueTask MatchAsync(
            Func<T, Task>? success = null,
            Func<Exception, Task>? failure = null,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult{T}"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async ValueTask MatchAsync(
            Func<T, ValueTask>? success = null,
            Func<Exception, ValueTask>? failure = null,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult{T}"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async ValueTask MatchAsync(
            Func<T, CancellationToken, Task>? success = null,
            Func<Exception, CancellationToken, Task>? failure = null,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult{T}"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async ValueTask MatchAsync(
            Func<T, CancellationToken, ValueTask>? success = null,
            Func<Exception, CancellationToken, ValueTask>? failure = null,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="ValueResult{T}"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="ValueResult{T}"/> instance is in a failed state.
        /// </returns>
        public async ValueTask<TResult> MatchAsync<TResult>(
            Func<T, TResult> success,
            Func<Exception, TResult> failure,
            CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).Match(success, failure);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="ValueResult{T}"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="ValueResult{T}"/> instance is in a failed state.
        /// </returns>
        public async ValueTask<TResult> MatchAsync<TResult>(
            Func<T, Task<TResult>> success,
            Func<Exception, TResult> failure,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="ValueResult{T}"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="ValueResult{T}"/> instance is in a failed state.
        /// </returns>
        public async ValueTask<TResult> MatchAsync<TResult>(
            Func<T, ValueTask<TResult>> success,
            Func<Exception, TResult> failure,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="ValueResult{T}"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="ValueResult{T}"/> instance is in a failed state.
        /// </returns>
        public async ValueTask<TResult> MatchAsync<TResult>(
            Func<T, CancellationToken, Task<TResult>> success,
            Func<Exception, TResult> failure,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="ValueResult{T}"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="ValueResult{T}"/> instance is in a failed state.
        /// </returns>
        public async ValueTask<TResult> MatchAsync<TResult>(
            Func<T, CancellationToken, ValueTask<TResult>> success,
            Func<Exception, TResult> failure,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="ValueResult{T}"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="ValueResult{T}"/> instance is in a failed state.
        /// </returns>
        public async ValueTask<TResult> MatchAsync<TResult>(
            Func<T, TResult> success,
            Func<Exception, Task<TResult>> failure,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="ValueResult{T}"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="ValueResult{T}"/> instance is in a failed state.
        /// </returns>
        public async ValueTask<TResult> MatchAsync<TResult>(
            Func<T, TResult> success,
            Func<Exception, ValueTask<TResult>> failure,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="ValueResult{T}"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="ValueResult{T}"/> instance is in a failed state.
        /// </returns>
        public async ValueTask<TResult> MatchAsync<TResult>(
            Func<T, TResult> success,
            Func<Exception, CancellationToken, Task<TResult>> failure,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="ValueResult{T}"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="ValueResult{T}"/> instance is in a failed state.
        /// </returns>
        public async ValueTask<TResult> MatchAsync<TResult>(
            Func<T, TResult> success,
            Func<Exception, CancellationToken, ValueTask<TResult>> failure,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="ValueResult{T}"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="ValueResult{T}"/> instance is in a failed state.
        /// </returns>
        public async ValueTask<TResult> MatchAsync<TResult>(
            Func<T, Task<TResult>> success,
            Func<Exception, Task<TResult>> failure,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="ValueResult{T}"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="ValueResult{T}"/> instance is in a failed state.
        /// </returns>
        public async ValueTask<TResult> MatchAsync<TResult>(
            Func<T, ValueTask<TResult>> success,
            Func<Exception, ValueTask<TResult>> failure,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="ValueResult{T}"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="ValueResult{T}"/> instance is in a failed state.
        /// </returns>
        public async ValueTask<TResult> MatchAsync<TResult>(
            Func<T, CancellationToken, Task<TResult>> success,
            Func<Exception, CancellationToken, Task<TResult>> failure,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="ValueResult{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="ValueResult{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="ValueResult{T}"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="ValueResult{T}"/> instance is in a failed state.
        /// </returns>
        public async ValueTask<TResult> MatchAsync<TResult>(
            Func<T, CancellationToken, ValueTask<TResult>> success,
            Func<Exception, CancellationToken, ValueTask<TResult>> failure,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .MatchAsync(success, failure, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="ValueResult{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns a successful <see cref="ValueResult"/> if the current <see cref="ValueResult{T}"/> is in a successful state, and the delegate invocation
        /// is also successful; otherwise, a failed <see cref="ValueResult"/>.
        /// </returns>
        public async ValueTask<ValueResult> SelectAsync(Action<T> selector, CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).Select(selector);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="ValueResult{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns a successful <see cref="ValueResult"/> if the current <see cref="ValueResult{T}"/> is in a successful state, and the delegate invocation
        /// is also successful; otherwise, a failed <see cref="ValueResult"/>.
        /// </returns>
        public async ValueTask<ValueResult> SelectAsync(Func<T, Task> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .SelectAsync(selector, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="ValueResult{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns a successful <see cref="ValueResult"/> if the current <see cref="ValueResult{T}"/> is in a successful state, and the delegate invocation
        /// is also successful; otherwise, a failed <see cref="ValueResult"/>.
        /// </returns>
        public async ValueTask<ValueResult> SelectAsync(Func<T, ValueTask> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .SelectAsync(selector, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="ValueResult{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns a successful <see cref="ValueResult"/> if the current <see cref="ValueResult{T}"/> is in a successful state, and the delegate invocation
        /// is also successful; otherwise, a failed <see cref="ValueResult"/>.
        /// </returns>
        public async ValueTask<ValueResult> SelectAsync(Func<T, CancellationToken, Task> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .SelectAsync(selector, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="ValueResult{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns a successful <see cref="ValueResult"/> if the current <see cref="ValueResult{T}"/> is in a successful state, and the delegate invocation
        /// is also successful; otherwise, a failed <see cref="ValueResult"/>.
        /// </returns>
        public async ValueTask<ValueResult> SelectAsync(Func<T, CancellationToken, ValueTask> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .SelectAsync(selector, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="ValueResult{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns a successful <see cref="ValueResult{T}"/> if the current <see cref="ValueResult{T}"/> is in a successful state, and the delegate
        /// invocation is also successful; otherwise, a failed <see cref="ValueResult{T}"/>.
        /// </returns>
        public async ValueTask<ValueResult<TResult>> SelectAsync<TResult>(Func<T, TResult> selector, CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).Select(selector);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="ValueResult{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns a successful <see cref="ValueResult{T}"/> if the current <see cref="ValueResult{T}"/> is in a successful state, and the delegate
        /// invocation is also successful; otherwise, a failed <see cref="ValueResult{T}"/>.
        /// </returns>
        public async ValueTask<ValueResult<TResult>> SelectAsync<TResult>(Func<T, Task<TResult>> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .SelectAsync(selector, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="ValueResult{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns a successful <see cref="ValueResult{T}"/> if the current <see cref="ValueResult{T}"/> is in a successful state, and the delegate
        /// invocation is also successful; otherwise, a failed <see cref="ValueResult{T}"/>.
        /// </returns>
        public async ValueTask<ValueResult<TResult>> SelectAsync<TResult>(
            Func<T, ValueTask<TResult>> selector,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .SelectAsync(selector, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="ValueResult{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns a successful <see cref="ValueResult{T}"/> if the current <see cref="ValueResult{T}"/> is in a successful state, and the delegate
        /// invocation is also successful; otherwise, a failed <see cref="ValueResult{T}"/>.
        /// </returns>
        public async ValueTask<ValueResult<TResult>> SelectAsync<TResult>(
            Func<T, CancellationToken, Task<TResult>> selector,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .SelectAsync(selector, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="ValueResult{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns a successful <see cref="ValueResult{T}"/> if the current <see cref="ValueResult{T}"/> is in a successful state, and the delegate
        /// invocation is also successful; otherwise, a failed <see cref="ValueResult{T}"/>.
        /// </returns>
        public async ValueTask<ValueResult<TResult>> SelectAsync<TResult>(
            Func<T, CancellationToken, ValueTask<TResult>> selector,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .SelectAsync(selector, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="ValueResult{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns a successful <see cref="ValueResult"/> if the current <see cref="ValueResult{T}"/> is in a successful state, and the delegate invocation
        /// is also successful; otherwise, a failed <see cref="ValueResult"/>.
        /// </returns>
        public async ValueTask<ValueResult> SelectManyAsync(Func<T, ValueResult> selector, CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectMany(selector);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="ValueResult{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns a successful <see cref="ValueResult"/> if the current <see cref="ValueResult{T}"/> is in a successful state, and the delegate invocation
        /// is also successful; otherwise, a failed <see cref="ValueResult"/>.
        /// </returns>
        public async ValueTask<ValueResult> SelectManyAsync(Func<T, Task<ValueResult>> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .SelectManyAsync(selector, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="ValueResult{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns a successful <see cref="ValueResult"/> if the current <see cref="ValueResult{T}"/> is in a successful state, and the delegate invocation
        /// is also successful; otherwise, a failed <see cref="ValueResult"/>.
        /// </returns>
        public async ValueTask<ValueResult> SelectManyAsync(Func<T, ValueTask<ValueResult>> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .SelectManyAsync(selector, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="ValueResult{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns a successful <see cref="ValueResult"/> if the current <see cref="ValueResult{T}"/> is in a successful state, and the delegate invocation
        /// is also successful; otherwise, a failed <see cref="ValueResult"/>.
        /// </returns>
        public async ValueTask<ValueResult> SelectManyAsync(
            Func<T, CancellationToken, Task<ValueResult>> selector,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .SelectManyAsync(selector, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="ValueResult{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns a successful <see cref="ValueResult"/> if the current <see cref="ValueResult{T}"/> is in a successful state, and the delegate invocation
        /// is also successful; otherwise, a failed <see cref="ValueResult"/>.
        /// </returns>
        public async ValueTask<ValueResult> SelectManyAsync(
            Func<T, CancellationToken, ValueTask<ValueResult>> selector,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .SelectManyAsync(selector, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="ValueResult{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns a successful <see cref="ValueResult{T}"/> if the current <see cref="ValueResult{T}"/> is in a successful state, and the delegate
        /// invocation is also successful; otherwise, a failed <see cref="ValueResult{T}"/>.
        /// </returns>
        public async ValueTask<ValueResult<TResult>> SelectManyAsync<TResult>(
            Func<T, ValueResult<TResult>> selector,
            CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectMany(selector);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="ValueResult{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns a successful <see cref="ValueResult{T}"/> if the current <see cref="ValueResult{T}"/> is in a successful state, and the delegate
        /// invocation is also successful; otherwise, a failed <see cref="ValueResult{T}"/>.
        /// </returns>
        public async ValueTask<ValueResult<TResult>> SelectManyAsync<TResult>(
            Func<T, Task<ValueResult<TResult>>> selector,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .SelectManyAsync(selector, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="ValueResult{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns a successful <see cref="ValueResult{T}"/> if the current <see cref="ValueResult{T}"/> is in a successful state, and the delegate
        /// invocation is also successful; otherwise, a failed <see cref="ValueResult{T}"/>.
        /// </returns>
        public async ValueTask<ValueResult<TResult>> SelectManyAsync<TResult>(
            Func<T, ValueTask<ValueResult<TResult>>> selector,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .SelectManyAsync(selector, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="ValueResult{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns a successful <see cref="ValueResult{T}"/> if the current <see cref="ValueResult{T}"/> is in a successful state, and the delegate
        /// invocation is also successful; otherwise, a failed <see cref="ValueResult{T}"/>.
        /// </returns>
        public async ValueTask<ValueResult<TResult>> SelectManyAsync<TResult>(
            Func<T, CancellationToken, Task<ValueResult<TResult>>> selector,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .SelectManyAsync(selector, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="ValueResult{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="ValueResult{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns a successful <see cref="ValueResult{T}"/> if the current <see cref="ValueResult{T}"/> is in a successful state, and the delegate
        /// invocation is also successful; otherwise, a failed <see cref="ValueResult{T}"/>.
        /// </returns>
        public async ValueTask<ValueResult<TResult>> SelectManyAsync<TResult>(
            Func<T, CancellationToken, ValueTask<ValueResult<TResult>>> selector,
            CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false))
                .SelectManyAsync(selector, cancellationToken)
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Asynchronously throws the underlying exception if the current <see cref="ValueResult{T}"/> is in a failure state.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async ValueTask ThrowAsync(CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).Throw();
    }

    /// <summary>
    /// Provides extension methods for <see cref="ValueTask{TResult}"/> of <see cref="ValueResult{T}"/> instances whose values are not null.
    /// </summary>
    /// <param name="receiver">The current <see cref="ValueTask{TResult}"/> of <see cref="ValueResult{T}"/> instance.</param>
    extension<T>(ValueTask<ValueResult<T>> receiver) where T : notnull
    {
        /// <summary>
        /// Asynchronously gets the <see cref="ValueOptional{T}"/> value of the current <see cref="ValueResult{T}"/> instance,
        /// or <see cref="ValueOptional{T}.None"/> if the result is in a failed state.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns the <see cref="ValueOptional{T}"/> value of the current <see cref="ValueResult{T}"/> instance,
        /// or <see cref="ValueOptional{T}.None"/> if the result is in a failed state.
        /// </returns>
        public async ValueTask<ValueOptional<T>> GetValueOrNoneAsync(CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).GetValueOrNone();
    }

    /// <summary>
    /// Provides extension methods for <see cref="ValueTask{TResult}"/> of <see cref="ValueResult{T}"/> of <see cref="ValueOptional{T}"/> instances.
    /// </summary>
    /// <param name="receiver">The current <see cref="ValueTask{TResult}"/> of <see cref="ValueResult{T}"/> of <see cref="ValueOptional{T}"/> instance.</param>
    extension<T>(ValueTask<ValueResult<ValueOptional<T>>> receiver) where T : notnull
    {
        /// <summary>
        /// Asynchronously gets the <see cref="ValueOptional{T}"/> value of the current <see cref="ValueResult{T}"/> instance,
        /// or <see cref="ValueOptional{T}.None"/> if the result is in a failed state.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns the <see cref="ValueOptional{T}"/> value of the current <see cref="ValueResult{T}"/> instance,
        /// or <see cref="ValueOptional{T}.None"/> if the result is in a failed state.
        /// </returns>
        public async ValueTask<ValueOptional<T>> GetValueOrNoneAsync(CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).GetValueOrNone();

        /// <summary>
        /// Asynchronously gets the underlying <typeparamref name="T"/> value from the current <see cref="ValueResult{T}"/> of <see cref="ValueOptional{T}"/>,
        /// or throws an exception if the result is in a failed state, or the <see cref="ValueOptional{T}"/> is <see cref="ValueOptional{T}.None"/>.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns the underlying <typeparamref name="T"/> value from the current <see cref="ValueResult{T}"/> of <see cref="ValueOptional{T}"/>,
        /// or throws an exception if the result is in a failed state, or the <see cref="ValueOptional{T}"/> is <see cref="ValueOptional{T}.None"/>.
        /// </returns>
        public async ValueTask<T> GetOptionalValueOrThrowAsync(CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).GetOptionalValueOrThrow();

        /// <summary>
        /// Asynchronously gets the underlying <typeparamref name="T"/> value from the current <see cref="ValueResult{T}"/> of <see cref="ValueOptional{T}"/>,
        /// or returns the default value if the result is in a failed state, or the <see cref="ValueOptional{T}"/> is <see cref="ValueOptional{T}.None"/>.
        /// </summary>
        /// <param name="defaultValue">The default value to return if the result is in a failed state, or the <see cref="ValueOptional{T}"/> is
        /// <see cref="ValueOptional{T}.None"/>.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns the underlying <typeparamref name="T"/> value from the current <see cref="ValueResult{T}"/> of <see cref="ValueOptional{T}"/>,
        /// or returns the default value if the result is in a failed state, or the <see cref="ValueOptional{T}"/> is <see cref="ValueOptional{T}.None"/>.
        /// </returns>
        public async ValueTask<T> GetOptionalValueOrDefaultAsync(T defaultValue, CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).GetOptionalValueOrDefault(defaultValue);
    }
}
