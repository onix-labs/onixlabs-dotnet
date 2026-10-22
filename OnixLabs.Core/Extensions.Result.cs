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
/// Provides extension methods for <see cref="Result"/> and <see cref="Result{T}"/> instances.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public static class ResultExtensions
{
    /// <summary>
    /// Provides extension methods for <see cref="Result{T}"/> instances whose values are not null.
    /// </summary>
    /// <param name="receiver">The current <see cref="Result{T}"/> instance.</param>
    extension<T>(Result<T> receiver) where T : notnull
    {
        /// <summary>
        /// Gets the <see cref="Optional{T}"/> value of the current <see cref="Result{T}"/> instance,
        /// or <see cref="Optional{T}.None"/> if the result is in a <see cref="Failure{T}"/> state.
        /// </summary>
        /// <returns>
        /// Returns the <see cref="Optional{T}"/> value of the current <see cref="Result{T}"/> instance,
        /// or <see cref="Optional{T}.None"/> if the result is in a <see cref="Failure{T}"/> state.
        /// </returns>
        public Optional<T> GetValueOrNone() =>
            receiver is Success<T> success ? success.Value : Optional<T>.None;
    }

    /// <summary>
    /// Provides extension methods for <see cref="Result{T}"/> of <see cref="Optional{T}"/> instances.
    /// </summary>
    /// <param name="receiver">The current <see cref="Result{T}"/> of <see cref="Optional{T}"/> instance.</param>
    extension<T>(Result<Optional<T>> receiver) where T : notnull
    {
        /// <summary>
        /// Gets the <see cref="Optional{T}"/> value of the current <see cref="Result{T}"/> instance,
        /// or <see cref="Optional{T}.None"/> if the result is in a <see cref="Failure{T}"/> state.
        /// </summary>
        /// <returns>
        /// Returns the <see cref="Optional{T}"/> value of the current <see cref="Result{T}"/> instance,
        /// or <see cref="Optional{T}.None"/> if the result is in a <see cref="Failure{T}"/> state.
        /// </returns>
        public Optional<T> GetValueOrNone() =>
            receiver is Success<Optional<T>> success ? success.Value : Optional<T>.None;

        /// <summary>
        /// Gets the underlying <typeparamref name="T"/> value from the current <see cref="Result{T}"/> of <see cref="Optional{T}"/>,
        /// or throws an exception if the result is in a <see cref="Failure{T}"/> state, or the <see cref="Optional{T}"/> is <see cref="Optional{T}.None"/>.
        /// </summary>
        /// <returns>
        /// Returns the underlying <typeparamref name="T"/> value from the current <see cref="Result{T}"/> of <see cref="Optional{T}"/>,
        /// or throws an exception if the result is in a <see cref="Failure{T}"/> state, or the <see cref="Optional{T}"/> is <see cref="Optional{T}.None"/>.
        /// </returns>
        public T GetOptionalValueOrThrow() =>
            receiver.GetValueOrThrow().GetValueOrThrow();

        /// <summary>
        /// Gets the underlying <typeparamref name="T"/> value from the current <see cref="Result{T}"/> of <see cref="Optional{T}"/>,
        /// or returns the default value if the result is in a <see cref="Failure{T}"/> state, or the <see cref="Optional{T}"/> is <see cref="Optional{T}.None"/>.
        /// </summary>
        /// <param name="defaultValue">The default value to return if the result is in a <see cref="Failure{T}"/> state, or the <see cref="Optional{T}"/> is <see cref="Optional{T}.None"/>.</param>
        /// <returns>
        /// Returns the underlying <typeparamref name="T"/> value from the current <see cref="Result{T}"/> of <see cref="Optional{T}"/>,
        /// or returns the default value if the result is in a <see cref="Failure{T}"/> state, or the <see cref="Optional{T}"/> is <see cref="Optional{T}.None"/>.
        /// </returns>
        public T GetOptionalValueOrDefault(T defaultValue) =>
            receiver is Success<Optional<T>> { Value: Some<T> some } ? some.Value : defaultValue;
    }

    /// <summary>
    /// Provides extension methods for <see cref="Task{TResult}"/> of <see cref="Result"/> instances.
    /// </summary>
    /// <param name="receiver">The current <see cref="Task{TResult}"/> of <see cref="Result"/> instance.</param>
    extension(Task<Result> receiver)
    {
        /// <summary>
        /// Asynchronously gets the underlying exception if the current <see cref="Result"/> is in a <see cref="Failure"/> state,
        /// or <see langword="null"/> if the current <see cref="Result"/> is in a <see cref="Success"/> state.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns the underlying exception if the current <see cref="Result"/> is in a <see cref="Failure"/> state,
        /// or <see langword="null"/> if the current <see cref="Result"/> is in a <see cref="Success"/> state.
        /// </returns>
        public async Task<Exception?> GetExceptionOrDefaultAsync(CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).GetExceptionOrDefault();

        /// <summary>
        /// Asynchronously gets the underlying exception if the current <see cref="Result"/> is in a <see cref="Failure"/> state,
        /// or the specified default exception if the current <see cref="Result"/> is in a <see cref="Success"/> state.
        /// </summary>
        /// <param name="defaultException">The default exception to return in the event that the current <see cref="Result"/> is in a <see cref="Success"/> state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns the underlying exception if the current <see cref="Result"/> is in a <see cref="Failure"/> state,
        /// or the specified default exception if the current <see cref="Result"/> is in a <see cref="Success"/> state.
        /// </returns>
        public async Task<Exception> GetExceptionOrDefaultAsync(Exception defaultException, CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).GetExceptionOrDefault(defaultException);

        /// <summary>
        /// Asynchronously gets the underlying exception if the current <see cref="Result"/> is in a <see cref="Failure"/> state,
        /// or throws <see cref="InvalidOperationException"/> if the current <see cref="Result"/> is in a <see cref="Success"/> state.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns the underlying exception if the current <see cref="Result"/> is in a <see cref="Failure"/> state,
        /// or throws <see cref="InvalidOperationException"/> if the current <see cref="Result"/> is in a <see cref="Success"/> state.
        /// </returns>
        public async Task<Exception> GetExceptionOrThrowAsync(CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).GetExceptionOrThrow();

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async Task MatchAsync(Action? success = null, Action<Exception>? failure = null, CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).Match(success, failure);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async Task MatchAsync(Func<Task>? success = null, Action<Exception>? failure = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async Task MatchAsync(Func<ValueTask>? success = null, Action<Exception>? failure = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async Task MatchAsync(Func<CancellationToken, Task>? success = null, Action<Exception>? failure = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async Task MatchAsync(Func<CancellationToken, ValueTask>? success = null, Action<Exception>? failure = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async Task MatchAsync(Action? success = null, Func<Exception, Task>? failure = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async Task MatchAsync(Action? success = null, Func<Exception, ValueTask>? failure = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async Task MatchAsync(Action? success = null, Func<Exception, CancellationToken, Task>? failure = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async Task MatchAsync(Action? success = null, Func<Exception, CancellationToken, ValueTask>? failure = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async Task MatchAsync(Func<Task>? success = null, Func<Exception, Task>? failure = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async Task MatchAsync(Func<ValueTask>? success = null, Func<Exception, ValueTask>? failure = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async Task MatchAsync(Func<CancellationToken, Task>? success = null, Func<Exception, CancellationToken, Task>? failure = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async Task MatchAsync(Func<CancellationToken, ValueTask>? success = null, Func<Exception, CancellationToken, ValueTask>? failure = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="Result"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="Result"/> instance is in a failed state.
        /// </returns>
        public async Task<TResult> MatchAsync<TResult>(Func<TResult> success, Func<Exception, TResult> failure, CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).Match(success, failure);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="Result"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="Result"/> instance is in a failed state.
        /// </returns>
        public async Task<TResult> MatchAsync<TResult>(Func<Task<TResult>> success, Func<Exception, TResult> failure, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="Result"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="Result"/> instance is in a failed state.
        /// </returns>
        public async Task<TResult> MatchAsync<TResult>(Func<ValueTask<TResult>> success, Func<Exception, TResult> failure, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="Result"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="Result"/> instance is in a failed state.
        /// </returns>
        public async Task<TResult> MatchAsync<TResult>(Func<CancellationToken, Task<TResult>> success, Func<Exception, TResult> failure, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="Result"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="Result"/> instance is in a failed state.
        /// </returns>
        public async Task<TResult> MatchAsync<TResult>(Func<CancellationToken, ValueTask<TResult>> success, Func<Exception, TResult> failure, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="Result"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="Result"/> instance is in a failed state.
        /// </returns>
        public async Task<TResult> MatchAsync<TResult>(Func<TResult> success, Func<Exception, Task<TResult>> failure, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="Result"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="Result"/> instance is in a failed state.
        /// </returns>
        public async Task<TResult> MatchAsync<TResult>(Func<TResult> success, Func<Exception, ValueTask<TResult>> failure, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="Result"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="Result"/> instance is in a failed state.
        /// </returns>
        public async Task<TResult> MatchAsync<TResult>(Func<TResult> success, Func<Exception, CancellationToken, Task<TResult>> failure, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="Result"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="Result"/> instance is in a failed state.
        /// </returns>
        public async Task<TResult> MatchAsync<TResult>(Func<TResult> success, Func<Exception, CancellationToken, ValueTask<TResult>> failure, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="Result"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="Result"/> instance is in a failed state.
        /// </returns>
        public async Task<TResult> MatchAsync<TResult>(Func<Task<TResult>> success, Func<Exception, Task<TResult>> failure, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="Result"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="Result"/> instance is in a failed state.
        /// </returns>
        public async Task<TResult> MatchAsync<TResult>(Func<ValueTask<TResult>> success, Func<Exception, ValueTask<TResult>> failure, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="Result"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="Result"/> instance is in a failed state.
        /// </returns>
        public async Task<TResult> MatchAsync<TResult>(Func<CancellationToken, Task<TResult>> success, Func<Exception, CancellationToken, Task<TResult>> failure, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="Result"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="Result"/> instance is in a failed state.
        /// </returns>
        public async Task<TResult> MatchAsync<TResult>(Func<CancellationToken, ValueTask<TResult>> success, Func<Exception, CancellationToken, ValueTask<TResult>> failure, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to current <see cref="Result"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns <see cref="Success"/> if the current <see cref="Result"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure"/>.
        /// </returns>
        public async Task<Result> SelectAsync(Action selector, CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).Select(selector);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to current <see cref="Result"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns <see cref="Success"/> if the current <see cref="Result"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure"/>.
        /// </returns>
        public async Task<Result> SelectAsync(Func<Task> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to current <see cref="Result"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns <see cref="Success"/> if the current <see cref="Result"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure"/>.
        /// </returns>
        public async Task<Result> SelectAsync(Func<ValueTask> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to current <see cref="Result"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns <see cref="Success"/> if the current <see cref="Result"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure"/>.
        /// </returns>
        public async Task<Result> SelectAsync(Func<CancellationToken, Task> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to current <see cref="Result"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns <see cref="Success"/> if the current <see cref="Result"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure"/>.
        /// </returns>
        public async Task<Result> SelectAsync(Func<CancellationToken, ValueTask> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the current <see cref="Result"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns <see cref="Success{T}"/> if the current <see cref="Result"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure{T}"/>.
        /// </returns>
        public async Task<Result<TResult>> SelectAsync<TResult>(Func<TResult> selector, CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).Select(selector);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the current <see cref="Result"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns <see cref="Success{T}"/> if the current <see cref="Result"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure{T}"/>.
        /// </returns>
        public async Task<Result<TResult>> SelectAsync<TResult>(Func<Task<TResult>> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the current <see cref="Result"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns <see cref="Success{T}"/> if the current <see cref="Result"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure{T}"/>.
        /// </returns>
        public async Task<Result<TResult>> SelectAsync<TResult>(Func<ValueTask<TResult>> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the current <see cref="Result"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns <see cref="Success{T}"/> if the current <see cref="Result"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure{T}"/>.
        /// </returns>
        public async Task<Result<TResult>> SelectAsync<TResult>(Func<CancellationToken, Task<TResult>> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the current <see cref="Result"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns <see cref="Success{T}"/> if the current <see cref="Result"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure{T}"/>.
        /// </returns>
        public async Task<Result<TResult>> SelectAsync<TResult>(Func<CancellationToken, ValueTask<TResult>> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the current <see cref="Result"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns <see cref="Success"/> if the current <see cref="Result"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure"/>.
        /// </returns>
        public async Task<Result> SelectManyAsync(Func<Result> selector, CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectMany(selector);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the current <see cref="Result"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns <see cref="Success"/> if the current <see cref="Result"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure"/>.
        /// </returns>
        public async Task<Result> SelectManyAsync(Func<Task<Result>> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectManyAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the current <see cref="Result"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns <see cref="Success"/> if the current <see cref="Result"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure"/>.
        /// </returns>
        public async Task<Result> SelectManyAsync(Func<ValueTask<Result>> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectManyAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the current <see cref="Result"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns <see cref="Success"/> if the current <see cref="Result"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure"/>.
        /// </returns>
        public async Task<Result> SelectManyAsync(Func<CancellationToken, Task<Result>> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectManyAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the current <see cref="Result"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns <see cref="Success"/> if the current <see cref="Result"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure"/>.
        /// </returns>
        public async Task<Result> SelectManyAsync(Func<CancellationToken, ValueTask<Result>> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectManyAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the current <see cref="Result"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns <see cref="Success{T}"/> if the current <see cref="Result"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure{T}"/>.
        /// </returns>
        public async Task<Result<TResult>> SelectManyAsync<TResult>(Func<Result<TResult>> selector, CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectMany(selector);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the current <see cref="Result"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns <see cref="Success{T}"/> if the current <see cref="Result"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure{T}"/>.
        /// </returns>
        public async Task<Result<TResult>> SelectManyAsync<TResult>(Func<Task<Result<TResult>>> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectManyAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the current <see cref="Result"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns <see cref="Success{T}"/> if the current <see cref="Result"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure{T}"/>.
        /// </returns>
        public async Task<Result<TResult>> SelectManyAsync<TResult>(Func<ValueTask<Result<TResult>>> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectManyAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the current <see cref="Result"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns <see cref="Success{T}"/> if the current <see cref="Result"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure{T}"/>.
        /// </returns>
        public async Task<Result<TResult>> SelectManyAsync<TResult>(Func<CancellationToken, Task<Result<TResult>>> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectManyAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the current <see cref="Result"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns <see cref="Success{T}"/> if the current <see cref="Result"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure{T}"/>.
        /// </returns>
        public async Task<Result<TResult>> SelectManyAsync<TResult>(Func<CancellationToken, ValueTask<Result<TResult>>> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectManyAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously throws the underlying exception if the current <see cref="Result"/> is in a failure state.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async Task ThrowAsync(CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).Throw();
    }

    /// <summary>
    /// Provides extension methods for <see cref="Task{TResult}"/> of <see cref="Result{T}"/> instances.
    /// </summary>
    /// <param name="receiver">The current <see cref="Task{TResult}"/> of <see cref="Result{T}"/> instance.</param>
    extension<T>(Task<Result<T>> receiver)
    {
        /// <summary>
        /// Asynchronously gets the underlying exception if the current <see cref="Result{T}"/> is in a <see cref="Failure{T}"/> state,
        /// or <see langword="null"/> if the current <see cref="Result{T}"/> is in a <see cref="Success{T}"/> state.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns the underlying exception if the current <see cref="Result{T}"/> is in a <see cref="Failure{T}"/> state,
        /// or <see langword="null"/> if the current <see cref="Result{T}"/> is in a <see cref="Success{T}"/> state.
        /// </returns>
        public async Task<Exception?> GetExceptionOrDefaultAsync(CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).GetExceptionOrDefault();

        /// <summary>
        /// Asynchronously gets the underlying exception if the current <see cref="Result{T}"/> is in a <see cref="Failure{T}"/> state,
        /// or the specified default exception if the current <see cref="Result{T}"/> is in a <see cref="Success{T}"/> state.
        /// </summary>
        /// <param name="defaultException">The default exception to return in the event that the current <see cref="Result{T}"/> is in a <see cref="Success{T}"/> state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns the underlying exception if the current <see cref="Result{T}"/> is in a <see cref="Failure{T}"/> state,
        /// or the specified default exception if the current <see cref="Result{T}"/> is in a <see cref="Success{T}"/> state.
        /// </returns>
        public async Task<Exception> GetExceptionOrDefaultAsync(Exception defaultException, CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).GetExceptionOrDefault(defaultException);

        /// <summary>
        /// Asynchronously gets the underlying exception if the current <see cref="Result{T}"/> is in a <see cref="Failure{T}"/> state,
        /// or throws <see cref="InvalidOperationException"/> if the current <see cref="Result{T}"/> is in a <see cref="Success{T}"/> state.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns the underlying exception if the current <see cref="Result{T}"/> is in a <see cref="Failure{T}"/> state,
        /// or throws <see cref="InvalidOperationException"/> if the current <see cref="Result{T}"/> is in a <see cref="Success{T}"/> state.
        /// </returns>
        public async Task<Exception> GetExceptionOrThrowAsync(CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).GetExceptionOrThrow();

        /// <summary>
        /// Asynchronously gets the underlying value of the current <see cref="Result{T}"/> instance, if the underlying value is present;
        /// otherwise returns the default <typeparamref name="T"/> value.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns the underlying value of the current <see cref="Result{T}"/> instance, if the underlying value is present;
        /// otherwise returns the default <typeparamref name="T"/> value.
        /// </returns>
        public async Task<T?> GetValueOrDefaultAsync(CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).GetValueOrDefault();

        /// <summary>
        /// Asynchronously gets the underlying value of the current <see cref="Result{T}"/> instance, if the underlying value is present;
        /// otherwise returns the specified default value.
        /// </summary>
        /// <param name="defaultValue">The default value to return in the event that the underlying value is absent.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns the underlying value of the current <see cref="Result{T}"/> instance, if the underlying value is present;
        /// otherwise returns the specified default value.
        /// </returns>
        public async Task<T> GetValueOrDefaultAsync(T defaultValue, CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).GetValueOrDefault(defaultValue);

        /// <summary>
        /// Asynchronously gets the underlying value of the current <see cref="Result{T}"/> instance;
        /// otherwise throws the underlying exception if the current <see cref="Result{T}"/> is in a failed stated.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns the underlying value of the current <see cref="Result{T}"/> instance;
        /// otherwise throws the underlying exception if the current <see cref="Result{T}"/> is in a failed stated.
        /// </returns>
        public async Task<T> GetValueOrThrowAsync(CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).GetValueOrThrow();

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result{T}"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async Task MatchAsync(Action<T>? success = null, Action<Exception>? failure = null, CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).Match(success, failure);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result{T}"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async Task MatchAsync(Func<T, Task>? success = null, Action<Exception>? failure = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result{T}"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async Task MatchAsync(Func<T, ValueTask>? success = null, Action<Exception>? failure = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result{T}"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async Task MatchAsync(Func<T, CancellationToken, Task>? success = null, Action<Exception>? failure = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result{T}"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async Task MatchAsync(Func<T, CancellationToken, ValueTask>? success = null, Action<Exception>? failure = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result{T}"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async Task MatchAsync(Action<T>? success = null, Func<Exception, Task>? failure = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result{T}"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async Task MatchAsync(Action<T>? success = null, Func<Exception, ValueTask>? failure = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result{T}"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async Task MatchAsync(Action<T>? success = null, Func<Exception, CancellationToken, Task>? failure = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result{T}"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async Task MatchAsync(Action<T>? success = null, Func<Exception, CancellationToken, ValueTask>? failure = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result{T}"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async Task MatchAsync(Func<T, Task>? success = null, Func<Exception, Task>? failure = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result{T}"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async Task MatchAsync(Func<T, ValueTask>? success = null, Func<Exception, ValueTask>? failure = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result{T}"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async Task MatchAsync(Func<T, CancellationToken, Task>? success = null, Func<Exception, CancellationToken, Task>? failure = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result{T}"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async Task MatchAsync(Func<T, CancellationToken, ValueTask>? success = null, Func<Exception, CancellationToken, ValueTask>? failure = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="Result{T}"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="Result{T}"/> instance is in a failed state.
        /// </returns>
        public async Task<TResult> MatchAsync<TResult>(Func<T, TResult> success, Func<Exception, TResult> failure, CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).Match(success, failure);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="Result{T}"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="Result{T}"/> instance is in a failed state.
        /// </returns>
        public async Task<TResult> MatchAsync<TResult>(Func<T, Task<TResult>> success, Func<Exception, TResult> failure, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="Result{T}"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="Result{T}"/> instance is in a failed state.
        /// </returns>
        public async Task<TResult> MatchAsync<TResult>(Func<T, ValueTask<TResult>> success, Func<Exception, TResult> failure, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="Result{T}"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="Result{T}"/> instance is in a failed state.
        /// </returns>
        public async Task<TResult> MatchAsync<TResult>(Func<T, CancellationToken, Task<TResult>> success, Func<Exception, TResult> failure, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="Result{T}"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="Result{T}"/> instance is in a failed state.
        /// </returns>
        public async Task<TResult> MatchAsync<TResult>(Func<T, CancellationToken, ValueTask<TResult>> success, Func<Exception, TResult> failure, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="Result{T}"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="Result{T}"/> instance is in a failed state.
        /// </returns>
        public async Task<TResult> MatchAsync<TResult>(Func<T, TResult> success, Func<Exception, Task<TResult>> failure, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="Result{T}"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="Result{T}"/> instance is in a failed state.
        /// </returns>
        public async Task<TResult> MatchAsync<TResult>(Func<T, TResult> success, Func<Exception, ValueTask<TResult>> failure, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="Result{T}"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="Result{T}"/> instance is in a failed state.
        /// </returns>
        public async Task<TResult> MatchAsync<TResult>(Func<T, TResult> success, Func<Exception, CancellationToken, Task<TResult>> failure, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="Result{T}"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="Result{T}"/> instance is in a failed state.
        /// </returns>
        public async Task<TResult> MatchAsync<TResult>(Func<T, TResult> success, Func<Exception, CancellationToken, ValueTask<TResult>> failure, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="Result{T}"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="Result{T}"/> instance is in a failed state.
        /// </returns>
        public async Task<TResult> MatchAsync<TResult>(Func<T, Task<TResult>> success, Func<Exception, Task<TResult>> failure, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="Result{T}"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="Result{T}"/> instance is in a failed state.
        /// </returns>
        public async Task<TResult> MatchAsync<TResult>(Func<T, ValueTask<TResult>> success, Func<Exception, ValueTask<TResult>> failure, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="Result{T}"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="Result{T}"/> instance is in a failed state.
        /// </returns>
        public async Task<TResult> MatchAsync<TResult>(Func<T, CancellationToken, Task<TResult>> success, Func<Exception, CancellationToken, Task<TResult>> failure, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="Result{T}"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="Result{T}"/> instance is in a failed state.
        /// </returns>
        public async Task<TResult> MatchAsync<TResult>(Func<T, CancellationToken, ValueTask<TResult>> success, Func<Exception, CancellationToken, ValueTask<TResult>> failure, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="Result{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns <see cref="Success"/> if the current <see cref="Result{T}"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure"/>.
        /// </returns>
        public async Task<Result> SelectAsync(Action<T> selector, CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).Select(selector);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="Result{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns <see cref="Success"/> if the current <see cref="Result{T}"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure"/>.
        /// </returns>
        public async Task<Result> SelectAsync(Func<T, Task> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="Result{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns <see cref="Success"/> if the current <see cref="Result{T}"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure"/>.
        /// </returns>
        public async Task<Result> SelectAsync(Func<T, ValueTask> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="Result{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns <see cref="Success"/> if the current <see cref="Result{T}"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure"/>.
        /// </returns>
        public async Task<Result> SelectAsync(Func<T, CancellationToken, Task> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="Result{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns <see cref="Success"/> if the current <see cref="Result{T}"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure"/>.
        /// </returns>
        public async Task<Result> SelectAsync(Func<T, CancellationToken, ValueTask> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="Result{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns <see cref="Success{T}"/> if the current <see cref="Result{T}"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure{T}"/>.
        /// </returns>
        public async Task<Result<TResult>> SelectAsync<TResult>(Func<T, TResult> selector, CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).Select(selector);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="Result{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns <see cref="Success{T}"/> if the current <see cref="Result{T}"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure{T}"/>.
        /// </returns>
        public async Task<Result<TResult>> SelectAsync<TResult>(Func<T, Task<TResult>> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="Result{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns <see cref="Success{T}"/> if the current <see cref="Result{T}"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure{T}"/>.
        /// </returns>
        public async Task<Result<TResult>> SelectAsync<TResult>(Func<T, ValueTask<TResult>> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="Result{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns <see cref="Success{T}"/> if the current <see cref="Result{T}"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure{T}"/>.
        /// </returns>
        public async Task<Result<TResult>> SelectAsync<TResult>(Func<T, CancellationToken, Task<TResult>> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="Result{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns <see cref="Success{T}"/> if the current <see cref="Result{T}"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure{T}"/>.
        /// </returns>
        public async Task<Result<TResult>> SelectAsync<TResult>(Func<T, CancellationToken, ValueTask<TResult>> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="Result{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns <see cref="Success"/> if the current <see cref="Result{T}"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure"/>.
        /// </returns>
        public async Task<Result> SelectManyAsync(Func<T, Result> selector, CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectMany(selector);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="Result{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns <see cref="Success"/> if the current <see cref="Result{T}"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure"/>.
        /// </returns>
        public async Task<Result> SelectManyAsync(Func<T, Task<Result>> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectManyAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="Result{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns <see cref="Success"/> if the current <see cref="Result{T}"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure"/>.
        /// </returns>
        public async Task<Result> SelectManyAsync(Func<T, ValueTask<Result>> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectManyAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="Result{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns <see cref="Success"/> if the current <see cref="Result{T}"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure"/>.
        /// </returns>
        public async Task<Result> SelectManyAsync(Func<T, CancellationToken, Task<Result>> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectManyAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="Result{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns <see cref="Success"/> if the current <see cref="Result{T}"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure"/>.
        /// </returns>
        public async Task<Result> SelectManyAsync(Func<T, CancellationToken, ValueTask<Result>> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectManyAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="Result{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns <see cref="Success{T}"/> if the current <see cref="Result{T}"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure{T}"/>.
        /// </returns>
        public async Task<Result<TResult>> SelectManyAsync<TResult>(Func<T, Result<TResult>> selector, CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectMany(selector);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="Result{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns <see cref="Success{T}"/> if the current <see cref="Result{T}"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure{T}"/>.
        /// </returns>
        public async Task<Result<TResult>> SelectManyAsync<TResult>(Func<T, Task<Result<TResult>>> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectManyAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="Result{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns <see cref="Success{T}"/> if the current <see cref="Result{T}"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure{T}"/>.
        /// </returns>
        public async Task<Result<TResult>> SelectManyAsync<TResult>(Func<T, ValueTask<Result<TResult>>> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectManyAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="Result{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns <see cref="Success{T}"/> if the current <see cref="Result{T}"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure{T}"/>.
        /// </returns>
        public async Task<Result<TResult>> SelectManyAsync<TResult>(Func<T, CancellationToken, Task<Result<TResult>>> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectManyAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="Result{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns <see cref="Success{T}"/> if the current <see cref="Result{T}"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure{T}"/>.
        /// </returns>
        public async Task<Result<TResult>> SelectManyAsync<TResult>(Func<T, CancellationToken, ValueTask<Result<TResult>>> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectManyAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously throws the underlying exception if the current <see cref="Result{T}"/> is in a failure state.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async Task ThrowAsync(CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).Throw();
    }

    /// <summary>
    /// Provides extension methods for <see cref="Task{TResult}"/> of <see cref="Result{T}"/> instances whose values are not null.
    /// </summary>
    /// <param name="receiver">The current <see cref="Task{TResult}"/> of <see cref="Result{T}"/> instance.</param>
    extension<T>(Task<Result<T>> receiver) where T : notnull
    {
        /// <summary>
        /// Asynchronously gets the <see cref="Optional{T}"/> value of the current <see cref="Result{T}"/> instance,
        /// or <see cref="Optional{T}.None"/> if the result is in a <see cref="Failure{T}"/> state.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns the <see cref="Optional{T}"/> value of the current <see cref="Result{T}"/> instance,
        /// or <see cref="Optional{T}.None"/> if the result is in a <see cref="Failure{T}"/> state.
        /// </returns>
        public async Task<Optional<T>> GetValueOrNoneAsync(CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).GetValueOrNone();
    }

    /// <summary>
    /// Provides extension methods for <see cref="Task{TResult}"/> of <see cref="Result{T}"/> of <see cref="Optional{T}"/> instances.
    /// </summary>
    /// <param name="receiver">The current <see cref="Task{TResult}"/> of <see cref="Result{T}"/> of <see cref="Optional{T}"/> instance.</param>
    extension<T>(Task<Result<Optional<T>>> receiver) where T : notnull
    {
        /// <summary>
        /// Asynchronously gets the <see cref="Optional{T}"/> value of the current <see cref="Result{T}"/> instance,
        /// or <see cref="Optional{T}.None"/> if the result is in a <see cref="Failure{T}"/> state.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns the <see cref="Optional{T}"/> value of the current <see cref="Result{T}"/> instance,
        /// or <see cref="Optional{T}.None"/> if the result is in a <see cref="Failure{T}"/> state.
        /// </returns>
        public async Task<Optional<T>> GetValueOrNoneAsync(CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).GetValueOrNone();

        /// <summary>
        /// Asynchronously gets the underlying <typeparamref name="T"/> value from the current <see cref="Result{T}"/> of <see cref="Optional{T}"/>,
        /// or throws an exception if the result is in a <see cref="Failure{T}"/> state, or the <see cref="Optional{T}"/> is <see cref="Optional{T}.None"/>.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns the underlying <typeparamref name="T"/> value from the current <see cref="Result{T}"/> of <see cref="Optional{T}"/>,
        /// or throws an exception if the result is in a <see cref="Failure{T}"/> state, or the <see cref="Optional{T}"/> is <see cref="Optional{T}.None"/>.
        /// </returns>
        public async Task<T> GetOptionalValueOrThrowAsync(CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).GetOptionalValueOrThrow();

        /// <summary>
        /// Asynchronously gets the underlying <typeparamref name="T"/> value from the current <see cref="Result{T}"/> of <see cref="Optional{T}"/>,
        /// or returns the default value if the result is in a <see cref="Failure{T}"/> state, or the <see cref="Optional{T}"/> is <see cref="Optional{T}.None"/>.
        /// </summary>
        /// <param name="defaultValue">The default value to return if the result is in a <see cref="Failure{T}"/> state, or the <see cref="Optional{T}"/> is <see cref="Optional{T}.None"/>.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns the underlying <typeparamref name="T"/> value from the current <see cref="Result{T}"/> of <see cref="Optional{T}"/>,
        /// or returns the default value if the result is in a <see cref="Failure{T}"/> state, or the <see cref="Optional{T}"/> is <see cref="Optional{T}.None"/>.
        /// </returns>
        public async Task<T> GetOptionalValueOrDefaultAsync(T defaultValue, CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).GetOptionalValueOrDefault(defaultValue);
    }

    /// <summary>
    /// Provides extension methods for <see cref="Task{TResult}"/> of <see cref="Failure"/> instances.
    /// </summary>
    /// <param name="receiver">The current <see cref="Task{TResult}"/> of <see cref="Failure"/> instance.</param>
    extension(Task<Failure> receiver)
    {
        /// <summary>
        /// Asynchronously obtains a new <see cref="Failure{T}"/> instance containing the current exception.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the <see cref="Failure{T}"/> to return.</typeparam>
        /// <returns>Returns a new <see cref="Failure{T}"/> instance containing the current exception.</returns>
        public async Task<Failure<TResult>> ToTypedResultAsync<TResult>(CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).ToTypedResult<TResult>();
    }

    /// <summary>
    /// Provides extension methods for <see cref="Task{TResult}"/> of <see cref="Failure{T}"/> instances.
    /// </summary>
    /// <param name="receiver">The current <see cref="Task{TResult}"/> of <see cref="Failure{T}"/> instance.</param>
    extension<T>(Task<Failure<T>> receiver)
    {
        /// <summary>
        /// Asynchronously obtains a new <see cref="Failure{T}"/> instance containing the current exception.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the <see cref="Failure{T}"/> to return.</typeparam>
        /// <returns>Returns a new <see cref="Failure{T}"/> instance containing the current exception.</returns>
        public async Task<Failure<TResult>> ToTypedResultAsync<TResult>(CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).ToTypedResult<TResult>();

        /// <summary>
        /// Asynchronously obtains a new <see cref="Failure"/> instance containing the current exception.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>Returns a new <see cref="Failure"/> instance containing the current exception.</returns>
        public async Task<Failure> ToUntypedResultAsync(CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).ToUntypedResult();
    }

    /// <summary>
    /// Provides extension methods for <see cref="ValueTask{TResult}"/> of <see cref="Result"/> instances.
    /// </summary>
    /// <param name="receiver">The current <see cref="ValueTask{TResult}"/> of <see cref="Result"/> instance.</param>
    extension(ValueTask<Result> receiver)
    {
        /// <summary>
        /// Asynchronously gets the underlying exception if the current <see cref="Result"/> is in a <see cref="Failure"/> state,
        /// or <see langword="null"/> if the current <see cref="Result"/> is in a <see cref="Success"/> state.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns the underlying exception if the current <see cref="Result"/> is in a <see cref="Failure"/> state,
        /// or <see langword="null"/> if the current <see cref="Result"/> is in a <see cref="Success"/> state.
        /// </returns>
        public async ValueTask<Exception?> GetExceptionOrDefaultAsync(CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).GetExceptionOrDefault();

        /// <summary>
        /// Asynchronously gets the underlying exception if the current <see cref="Result"/> is in a <see cref="Failure"/> state,
        /// or the specified default exception if the current <see cref="Result"/> is in a <see cref="Success"/> state.
        /// </summary>
        /// <param name="defaultException">The default exception to return in the event that the current <see cref="Result"/> is in a <see cref="Success"/> state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns the underlying exception if the current <see cref="Result"/> is in a <see cref="Failure"/> state,
        /// or the specified default exception if the current <see cref="Result"/> is in a <see cref="Success"/> state.
        /// </returns>
        public async ValueTask<Exception> GetExceptionOrDefaultAsync(Exception defaultException, CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).GetExceptionOrDefault(defaultException);

        /// <summary>
        /// Asynchronously gets the underlying exception if the current <see cref="Result"/> is in a <see cref="Failure"/> state,
        /// or throws <see cref="InvalidOperationException"/> if the current <see cref="Result"/> is in a <see cref="Success"/> state.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns the underlying exception if the current <see cref="Result"/> is in a <see cref="Failure"/> state,
        /// or throws <see cref="InvalidOperationException"/> if the current <see cref="Result"/> is in a <see cref="Success"/> state.
        /// </returns>
        public async ValueTask<Exception> GetExceptionOrThrowAsync(CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).GetExceptionOrThrow();

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async ValueTask MatchAsync(Action? success = null, Action<Exception>? failure = null, CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).Match(success, failure);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async ValueTask MatchAsync(Func<Task>? success = null, Action<Exception>? failure = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async ValueTask MatchAsync(Func<ValueTask>? success = null, Action<Exception>? failure = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async ValueTask MatchAsync(Func<CancellationToken, Task>? success = null, Action<Exception>? failure = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async ValueTask MatchAsync(Func<CancellationToken, ValueTask>? success = null, Action<Exception>? failure = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async ValueTask MatchAsync(Action? success = null, Func<Exception, Task>? failure = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async ValueTask MatchAsync(Action? success = null, Func<Exception, ValueTask>? failure = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async ValueTask MatchAsync(Action? success = null, Func<Exception, CancellationToken, Task>? failure = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async ValueTask MatchAsync(Action? success = null, Func<Exception, CancellationToken, ValueTask>? failure = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async ValueTask MatchAsync(Func<Task>? success = null, Func<Exception, Task>? failure = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async ValueTask MatchAsync(Func<ValueTask>? success = null, Func<Exception, ValueTask>? failure = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async ValueTask MatchAsync(Func<CancellationToken, Task>? success = null, Func<Exception, CancellationToken, Task>? failure = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async ValueTask MatchAsync(Func<CancellationToken, ValueTask>? success = null, Func<Exception, CancellationToken, ValueTask>? failure = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="Result"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="Result"/> instance is in a failed state.
        /// </returns>
        public async ValueTask<TResult> MatchAsync<TResult>(Func<TResult> success, Func<Exception, TResult> failure, CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).Match(success, failure);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="Result"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="Result"/> instance is in a failed state.
        /// </returns>
        public async ValueTask<TResult> MatchAsync<TResult>(Func<Task<TResult>> success, Func<Exception, TResult> failure, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="Result"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="Result"/> instance is in a failed state.
        /// </returns>
        public async ValueTask<TResult> MatchAsync<TResult>(Func<ValueTask<TResult>> success, Func<Exception, TResult> failure, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="Result"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="Result"/> instance is in a failed state.
        /// </returns>
        public async ValueTask<TResult> MatchAsync<TResult>(Func<CancellationToken, Task<TResult>> success, Func<Exception, TResult> failure, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="Result"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="Result"/> instance is in a failed state.
        /// </returns>
        public async ValueTask<TResult> MatchAsync<TResult>(Func<CancellationToken, ValueTask<TResult>> success, Func<Exception, TResult> failure, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="Result"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="Result"/> instance is in a failed state.
        /// </returns>
        public async ValueTask<TResult> MatchAsync<TResult>(Func<TResult> success, Func<Exception, Task<TResult>> failure, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="Result"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="Result"/> instance is in a failed state.
        /// </returns>
        public async ValueTask<TResult> MatchAsync<TResult>(Func<TResult> success, Func<Exception, ValueTask<TResult>> failure, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="Result"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="Result"/> instance is in a failed state.
        /// </returns>
        public async ValueTask<TResult> MatchAsync<TResult>(Func<TResult> success, Func<Exception, CancellationToken, Task<TResult>> failure, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="Result"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="Result"/> instance is in a failed state.
        /// </returns>
        public async ValueTask<TResult> MatchAsync<TResult>(Func<TResult> success, Func<Exception, CancellationToken, ValueTask<TResult>> failure, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="Result"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="Result"/> instance is in a failed state.
        /// </returns>
        public async ValueTask<TResult> MatchAsync<TResult>(Func<Task<TResult>> success, Func<Exception, Task<TResult>> failure, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="Result"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="Result"/> instance is in a failed state.
        /// </returns>
        public async ValueTask<TResult> MatchAsync<TResult>(Func<ValueTask<TResult>> success, Func<Exception, ValueTask<TResult>> failure, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="Result"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="Result"/> instance is in a failed state.
        /// </returns>
        public async ValueTask<TResult> MatchAsync<TResult>(Func<CancellationToken, Task<TResult>> success, Func<Exception, CancellationToken, Task<TResult>> failure, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="Result"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="Result"/> instance is in a failed state.
        /// </returns>
        public async ValueTask<TResult> MatchAsync<TResult>(Func<CancellationToken, ValueTask<TResult>> success, Func<Exception, CancellationToken, ValueTask<TResult>> failure, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to current <see cref="Result"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns <see cref="Success"/> if the current <see cref="Result"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure"/>.
        /// </returns>
        public async ValueTask<Result> SelectAsync(Action selector, CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).Select(selector);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to current <see cref="Result"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns <see cref="Success"/> if the current <see cref="Result"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure"/>.
        /// </returns>
        public async ValueTask<Result> SelectAsync(Func<Task> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to current <see cref="Result"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns <see cref="Success"/> if the current <see cref="Result"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure"/>.
        /// </returns>
        public async ValueTask<Result> SelectAsync(Func<ValueTask> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to current <see cref="Result"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns <see cref="Success"/> if the current <see cref="Result"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure"/>.
        /// </returns>
        public async ValueTask<Result> SelectAsync(Func<CancellationToken, Task> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to current <see cref="Result"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns <see cref="Success"/> if the current <see cref="Result"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure"/>.
        /// </returns>
        public async ValueTask<Result> SelectAsync(Func<CancellationToken, ValueTask> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the current <see cref="Result"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns <see cref="Success{T}"/> if the current <see cref="Result"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure{T}"/>.
        /// </returns>
        public async ValueTask<Result<TResult>> SelectAsync<TResult>(Func<TResult> selector, CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).Select(selector);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the current <see cref="Result"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns <see cref="Success{T}"/> if the current <see cref="Result"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure{T}"/>.
        /// </returns>
        public async ValueTask<Result<TResult>> SelectAsync<TResult>(Func<Task<TResult>> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the current <see cref="Result"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns <see cref="Success{T}"/> if the current <see cref="Result"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure{T}"/>.
        /// </returns>
        public async ValueTask<Result<TResult>> SelectAsync<TResult>(Func<ValueTask<TResult>> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the current <see cref="Result"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns <see cref="Success{T}"/> if the current <see cref="Result"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure{T}"/>.
        /// </returns>
        public async ValueTask<Result<TResult>> SelectAsync<TResult>(Func<CancellationToken, Task<TResult>> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the current <see cref="Result"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns <see cref="Success{T}"/> if the current <see cref="Result"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure{T}"/>.
        /// </returns>
        public async ValueTask<Result<TResult>> SelectAsync<TResult>(Func<CancellationToken, ValueTask<TResult>> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the current <see cref="Result"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns <see cref="Success"/> if the current <see cref="Result"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure"/>.
        /// </returns>
        public async ValueTask<Result> SelectManyAsync(Func<Result> selector, CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectMany(selector);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the current <see cref="Result"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns <see cref="Success"/> if the current <see cref="Result"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure"/>.
        /// </returns>
        public async ValueTask<Result> SelectManyAsync(Func<Task<Result>> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectManyAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the current <see cref="Result"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns <see cref="Success"/> if the current <see cref="Result"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure"/>.
        /// </returns>
        public async ValueTask<Result> SelectManyAsync(Func<ValueTask<Result>> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectManyAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the current <see cref="Result"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns <see cref="Success"/> if the current <see cref="Result"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure"/>.
        /// </returns>
        public async ValueTask<Result> SelectManyAsync(Func<CancellationToken, Task<Result>> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectManyAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the current <see cref="Result"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns <see cref="Success"/> if the current <see cref="Result"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure"/>.
        /// </returns>
        public async ValueTask<Result> SelectManyAsync(Func<CancellationToken, ValueTask<Result>> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectManyAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the current <see cref="Result"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns <see cref="Success{T}"/> if the current <see cref="Result"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure{T}"/>.
        /// </returns>
        public async ValueTask<Result<TResult>> SelectManyAsync<TResult>(Func<Result<TResult>> selector, CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectMany(selector);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the current <see cref="Result"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns <see cref="Success{T}"/> if the current <see cref="Result"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure{T}"/>.
        /// </returns>
        public async ValueTask<Result<TResult>> SelectManyAsync<TResult>(Func<Task<Result<TResult>>> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectManyAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the current <see cref="Result"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns <see cref="Success{T}"/> if the current <see cref="Result"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure{T}"/>.
        /// </returns>
        public async ValueTask<Result<TResult>> SelectManyAsync<TResult>(Func<ValueTask<Result<TResult>>> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectManyAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the current <see cref="Result"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns <see cref="Success{T}"/> if the current <see cref="Result"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure{T}"/>.
        /// </returns>
        public async ValueTask<Result<TResult>> SelectManyAsync<TResult>(Func<CancellationToken, Task<Result<TResult>>> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectManyAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the current <see cref="Result"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns <see cref="Success{T}"/> if the current <see cref="Result"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure{T}"/>.
        /// </returns>
        public async ValueTask<Result<TResult>> SelectManyAsync<TResult>(Func<CancellationToken, ValueTask<Result<TResult>>> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectManyAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously throws the underlying exception if the current <see cref="Result"/> is in a failure state.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async ValueTask ThrowAsync(CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).Throw();
    }

    /// <summary>
    /// Provides extension methods for <see cref="ValueTask{TResult}"/> of <see cref="Result{T}"/> instances.
    /// </summary>
    /// <param name="receiver">The current <see cref="ValueTask{TResult}"/> of <see cref="Result{T}"/> instance.</param>
    extension<T>(ValueTask<Result<T>> receiver)
    {
        /// <summary>
        /// Asynchronously gets the underlying exception if the current <see cref="Result{T}"/> is in a <see cref="Failure{T}"/> state,
        /// or <see langword="null"/> if the current <see cref="Result{T}"/> is in a <see cref="Success{T}"/> state.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns the underlying exception if the current <see cref="Result{T}"/> is in a <see cref="Failure{T}"/> state,
        /// or <see langword="null"/> if the current <see cref="Result{T}"/> is in a <see cref="Success{T}"/> state.
        /// </returns>
        public async ValueTask<Exception?> GetExceptionOrDefaultAsync(CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).GetExceptionOrDefault();

        /// <summary>
        /// Asynchronously gets the underlying exception if the current <see cref="Result{T}"/> is in a <see cref="Failure{T}"/> state,
        /// or the specified default exception if the current <see cref="Result{T}"/> is in a <see cref="Success{T}"/> state.
        /// </summary>
        /// <param name="defaultException">The default exception to return in the event that the current <see cref="Result{T}"/> is in a <see cref="Success{T}"/> state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns the underlying exception if the current <see cref="Result{T}"/> is in a <see cref="Failure{T}"/> state,
        /// or the specified default exception if the current <see cref="Result{T}"/> is in a <see cref="Success{T}"/> state.
        /// </returns>
        public async ValueTask<Exception> GetExceptionOrDefaultAsync(Exception defaultException, CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).GetExceptionOrDefault(defaultException);

        /// <summary>
        /// Asynchronously gets the underlying exception if the current <see cref="Result{T}"/> is in a <see cref="Failure{T}"/> state,
        /// or throws <see cref="InvalidOperationException"/> if the current <see cref="Result{T}"/> is in a <see cref="Success{T}"/> state.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns the underlying exception if the current <see cref="Result{T}"/> is in a <see cref="Failure{T}"/> state,
        /// or throws <see cref="InvalidOperationException"/> if the current <see cref="Result{T}"/> is in a <see cref="Success{T}"/> state.
        /// </returns>
        public async ValueTask<Exception> GetExceptionOrThrowAsync(CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).GetExceptionOrThrow();

        /// <summary>
        /// Asynchronously gets the underlying value of the current <see cref="Result{T}"/> instance, if the underlying value is present;
        /// otherwise returns the default <typeparamref name="T"/> value.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns the underlying value of the current <see cref="Result{T}"/> instance, if the underlying value is present;
        /// otherwise returns the default <typeparamref name="T"/> value.
        /// </returns>
        public async ValueTask<T?> GetValueOrDefaultAsync(CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).GetValueOrDefault();

        /// <summary>
        /// Asynchronously gets the underlying value of the current <see cref="Result{T}"/> instance, if the underlying value is present;
        /// otherwise returns the specified default value.
        /// </summary>
        /// <param name="defaultValue">The default value to return in the event that the underlying value is absent.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns the underlying value of the current <see cref="Result{T}"/> instance, if the underlying value is present;
        /// otherwise returns the specified default value.
        /// </returns>
        public async ValueTask<T> GetValueOrDefaultAsync(T defaultValue, CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).GetValueOrDefault(defaultValue);

        /// <summary>
        /// Asynchronously gets the underlying value of the current <see cref="Result{T}"/> instance;
        /// otherwise throws the underlying exception if the current <see cref="Result{T}"/> is in a failed stated.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns the underlying value of the current <see cref="Result{T}"/> instance;
        /// otherwise throws the underlying exception if the current <see cref="Result{T}"/> is in a failed stated.
        /// </returns>
        public async ValueTask<T> GetValueOrThrowAsync(CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).GetValueOrThrow();

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result{T}"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async ValueTask MatchAsync(Action<T>? success = null, Action<Exception>? failure = null, CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).Match(success, failure);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result{T}"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async ValueTask MatchAsync(Func<T, Task>? success = null, Action<Exception>? failure = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result{T}"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async ValueTask MatchAsync(Func<T, ValueTask>? success = null, Action<Exception>? failure = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result{T}"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async ValueTask MatchAsync(Func<T, CancellationToken, Task>? success = null, Action<Exception>? failure = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result{T}"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async ValueTask MatchAsync(Func<T, CancellationToken, ValueTask>? success = null, Action<Exception>? failure = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result{T}"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async ValueTask MatchAsync(Action<T>? success = null, Func<Exception, Task>? failure = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result{T}"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async ValueTask MatchAsync(Action<T>? success = null, Func<Exception, ValueTask>? failure = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result{T}"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async ValueTask MatchAsync(Action<T>? success = null, Func<Exception, CancellationToken, Task>? failure = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result{T}"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async ValueTask MatchAsync(Action<T>? success = null, Func<Exception, CancellationToken, ValueTask>? failure = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result{T}"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async ValueTask MatchAsync(Func<T, Task>? success = null, Func<Exception, Task>? failure = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result{T}"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async ValueTask MatchAsync(Func<T, ValueTask>? success = null, Func<Exception, ValueTask>? failure = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result{T}"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async ValueTask MatchAsync(Func<T, CancellationToken, Task>? success = null, Func<Exception, CancellationToken, Task>? failure = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result{T}"/> instance.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async ValueTask MatchAsync(Func<T, CancellationToken, ValueTask>? success = null, Func<Exception, CancellationToken, ValueTask>? failure = null, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="Result{T}"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="Result{T}"/> instance is in a failed state.
        /// </returns>
        public async ValueTask<TResult> MatchAsync<TResult>(Func<T, TResult> success, Func<Exception, TResult> failure, CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).Match(success, failure);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="Result{T}"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="Result{T}"/> instance is in a failed state.
        /// </returns>
        public async ValueTask<TResult> MatchAsync<TResult>(Func<T, Task<TResult>> success, Func<Exception, TResult> failure, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="Result{T}"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="Result{T}"/> instance is in a failed state.
        /// </returns>
        public async ValueTask<TResult> MatchAsync<TResult>(Func<T, ValueTask<TResult>> success, Func<Exception, TResult> failure, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="Result{T}"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="Result{T}"/> instance is in a failed state.
        /// </returns>
        public async ValueTask<TResult> MatchAsync<TResult>(Func<T, CancellationToken, Task<TResult>> success, Func<Exception, TResult> failure, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="Result{T}"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="Result{T}"/> instance is in a failed state.
        /// </returns>
        public async ValueTask<TResult> MatchAsync<TResult>(Func<T, CancellationToken, ValueTask<TResult>> success, Func<Exception, TResult> failure, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="Result{T}"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="Result{T}"/> instance is in a failed state.
        /// </returns>
        public async ValueTask<TResult> MatchAsync<TResult>(Func<T, TResult> success, Func<Exception, Task<TResult>> failure, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="Result{T}"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="Result{T}"/> instance is in a failed state.
        /// </returns>
        public async ValueTask<TResult> MatchAsync<TResult>(Func<T, TResult> success, Func<Exception, ValueTask<TResult>> failure, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="Result{T}"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="Result{T}"/> instance is in a failed state.
        /// </returns>
        public async ValueTask<TResult> MatchAsync<TResult>(Func<T, TResult> success, Func<Exception, CancellationToken, Task<TResult>> failure, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="Result{T}"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="Result{T}"/> instance is in a failed state.
        /// </returns>
        public async ValueTask<TResult> MatchAsync<TResult>(Func<T, TResult> success, Func<Exception, CancellationToken, ValueTask<TResult>> failure, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="Result{T}"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="Result{T}"/> instance is in a failed state.
        /// </returns>
        public async ValueTask<TResult> MatchAsync<TResult>(Func<T, Task<TResult>> success, Func<Exception, Task<TResult>> failure, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="Result{T}"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="Result{T}"/> instance is in a failed state.
        /// </returns>
        public async ValueTask<TResult> MatchAsync<TResult>(Func<T, ValueTask<TResult>> success, Func<Exception, ValueTask<TResult>> failure, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="Result{T}"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="Result{T}"/> instance is in a failed state.
        /// </returns>
        public async ValueTask<TResult> MatchAsync<TResult>(Func<T, CancellationToken, Task<TResult>> success, Func<Exception, CancellationToken, Task<TResult>> failure, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously executes the delegate that matches the value of the current <see cref="Result{T}"/> instance and returns its result.
        /// </summary>
        /// <param name="success">The delegate to execute when the current <see cref="Result{T}"/> instance is in a successful state.</param>
        /// <param name="failure">The delegate to execute when the current <see cref="Result{T}"/> instance is in a failed state.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
        /// <returns>
        /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="Result{T}"/> instance is in a successful state;
        /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="Result{T}"/> instance is in a failed state.
        /// </returns>
        public async ValueTask<TResult> MatchAsync<TResult>(Func<T, CancellationToken, ValueTask<TResult>> success, Func<Exception, CancellationToken, ValueTask<TResult>> failure, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).MatchAsync(success, failure, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="Result{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns <see cref="Success"/> if the current <see cref="Result{T}"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure"/>.
        /// </returns>
        public async ValueTask<Result> SelectAsync(Action<T> selector, CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).Select(selector);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="Result{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns <see cref="Success"/> if the current <see cref="Result{T}"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure"/>.
        /// </returns>
        public async ValueTask<Result> SelectAsync(Func<T, Task> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="Result{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns <see cref="Success"/> if the current <see cref="Result{T}"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure"/>.
        /// </returns>
        public async ValueTask<Result> SelectAsync(Func<T, ValueTask> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="Result{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns <see cref="Success"/> if the current <see cref="Result{T}"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure"/>.
        /// </returns>
        public async ValueTask<Result> SelectAsync(Func<T, CancellationToken, Task> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="Result{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns <see cref="Success"/> if the current <see cref="Result{T}"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure"/>.
        /// </returns>
        public async ValueTask<Result> SelectAsync(Func<T, CancellationToken, ValueTask> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="Result{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns <see cref="Success{T}"/> if the current <see cref="Result{T}"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure{T}"/>.
        /// </returns>
        public async ValueTask<Result<TResult>> SelectAsync<TResult>(Func<T, TResult> selector, CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).Select(selector);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="Result{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns <see cref="Success{T}"/> if the current <see cref="Result{T}"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure{T}"/>.
        /// </returns>
        public async ValueTask<Result<TResult>> SelectAsync<TResult>(Func<T, Task<TResult>> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="Result{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns <see cref="Success{T}"/> if the current <see cref="Result{T}"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure{T}"/>.
        /// </returns>
        public async ValueTask<Result<TResult>> SelectAsync<TResult>(Func<T, ValueTask<TResult>> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="Result{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns <see cref="Success{T}"/> if the current <see cref="Result{T}"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure{T}"/>.
        /// </returns>
        public async ValueTask<Result<TResult>> SelectAsync<TResult>(Func<T, CancellationToken, Task<TResult>> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="Result{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns <see cref="Success{T}"/> if the current <see cref="Result{T}"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure{T}"/>.
        /// </returns>
        public async ValueTask<Result<TResult>> SelectAsync<TResult>(Func<T, CancellationToken, ValueTask<TResult>> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="Result{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns <see cref="Success"/> if the current <see cref="Result{T}"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure"/>.
        /// </returns>
        public async ValueTask<Result> SelectManyAsync(Func<T, Result> selector, CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectMany(selector);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="Result{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns <see cref="Success"/> if the current <see cref="Result{T}"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure"/>.
        /// </returns>
        public async ValueTask<Result> SelectManyAsync(Func<T, Task<Result>> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectManyAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="Result{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns <see cref="Success"/> if the current <see cref="Result{T}"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure"/>.
        /// </returns>
        public async ValueTask<Result> SelectManyAsync(Func<T, ValueTask<Result>> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectManyAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="Result{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns <see cref="Success"/> if the current <see cref="Result{T}"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure"/>.
        /// </returns>
        public async ValueTask<Result> SelectManyAsync(Func<T, CancellationToken, Task<Result>> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectManyAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="Result{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns <see cref="Success"/> if the current <see cref="Result{T}"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure"/>.
        /// </returns>
        public async ValueTask<Result> SelectManyAsync(Func<T, CancellationToken, ValueTask<Result>> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectManyAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="Result{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns <see cref="Success{T}"/> if the current <see cref="Result{T}"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure{T}"/>.
        /// </returns>
        public async ValueTask<Result<TResult>> SelectManyAsync<TResult>(Func<T, Result<TResult>> selector, CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectMany(selector);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="Result{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns <see cref="Success{T}"/> if the current <see cref="Result{T}"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure{T}"/>.
        /// </returns>
        public async ValueTask<Result<TResult>> SelectManyAsync<TResult>(Func<T, Task<Result<TResult>>> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectManyAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="Result{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns <see cref="Success{T}"/> if the current <see cref="Result{T}"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure{T}"/>.
        /// </returns>
        public async ValueTask<Result<TResult>> SelectManyAsync<TResult>(Func<T, ValueTask<Result<TResult>>> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectManyAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="Result{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns <see cref="Success{T}"/> if the current <see cref="Result{T}"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure{T}"/>.
        /// </returns>
        public async ValueTask<Result<TResult>> SelectManyAsync<TResult>(Func<T, CancellationToken, Task<Result<TResult>>> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectManyAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result{T}"/> instance.
        /// </summary>
        /// <param name="selector">The delegate to apply to the value of the current <see cref="Result{T}"/> instance.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
        /// <returns>
        /// Returns <see cref="Success{T}"/> if the current <see cref="Result{T}"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure{T}"/>.
        /// </returns>
        public async ValueTask<Result<TResult>> SelectManyAsync<TResult>(Func<T, CancellationToken, ValueTask<Result<TResult>>> selector, CancellationToken cancellationToken = default) =>
            await (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).SelectManyAsync(selector, cancellationToken).WaitAsync(cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously throws the underlying exception if the current <see cref="Result{T}"/> is in a failure state.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async ValueTask ThrowAsync(CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).Throw();
    }

    /// <summary>
    /// Provides extension methods for <see cref="ValueTask{TResult}"/> of <see cref="Result{T}"/> instances whose values are not null.
    /// </summary>
    /// <param name="receiver">The current <see cref="ValueTask{TResult}"/> of <see cref="Result{T}"/> instance.</param>
    extension<T>(ValueTask<Result<T>> receiver) where T : notnull
    {
        /// <summary>
        /// Asynchronously gets the <see cref="Optional{T}"/> value of the current <see cref="Result{T}"/> instance,
        /// or <see cref="Optional{T}.None"/> if the result is in a <see cref="Failure{T}"/> state.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns the <see cref="Optional{T}"/> value of the current <see cref="Result{T}"/> instance,
        /// or <see cref="Optional{T}.None"/> if the result is in a <see cref="Failure{T}"/> state.
        /// </returns>
        public async ValueTask<Optional<T>> GetValueOrNoneAsync(CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).GetValueOrNone();
    }

    /// <summary>
    /// Provides extension methods for <see cref="ValueTask{TResult}"/> of <see cref="Result{T}"/> of <see cref="Optional{T}"/> instances.
    /// </summary>
    /// <param name="receiver">The current <see cref="ValueTask{TResult}"/> of <see cref="Result{T}"/> of <see cref="Optional{T}"/> instance.</param>
    extension<T>(ValueTask<Result<Optional<T>>> receiver) where T : notnull
    {
        /// <summary>
        /// Asynchronously gets the <see cref="Optional{T}"/> value of the current <see cref="Result{T}"/> instance,
        /// or <see cref="Optional{T}.None"/> if the result is in a <see cref="Failure{T}"/> state.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns the <see cref="Optional{T}"/> value of the current <see cref="Result{T}"/> instance,
        /// or <see cref="Optional{T}.None"/> if the result is in a <see cref="Failure{T}"/> state.
        /// </returns>
        public async ValueTask<Optional<T>> GetValueOrNoneAsync(CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).GetValueOrNone();

        /// <summary>
        /// Asynchronously gets the underlying <typeparamref name="T"/> value from the current <see cref="Result{T}"/> of <see cref="Optional{T}"/>,
        /// or throws an exception if the result is in a <see cref="Failure{T}"/> state, or the <see cref="Optional{T}"/> is <see cref="Optional{T}.None"/>.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns the underlying <typeparamref name="T"/> value from the current <see cref="Result{T}"/> of <see cref="Optional{T}"/>,
        /// or throws an exception if the result is in a <see cref="Failure{T}"/> state, or the <see cref="Optional{T}"/> is <see cref="Optional{T}.None"/>.
        /// </returns>
        public async ValueTask<T> GetOptionalValueOrThrowAsync(CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).GetOptionalValueOrThrow();

        /// <summary>
        /// Asynchronously gets the underlying <typeparamref name="T"/> value from the current <see cref="Result{T}"/> of <see cref="Optional{T}"/>,
        /// or returns the default value if the result is in a <see cref="Failure{T}"/> state, or the <see cref="Optional{T}"/> is <see cref="Optional{T}.None"/>.
        /// </summary>
        /// <param name="defaultValue">The default value to return if the result is in a <see cref="Failure{T}"/> state, or the <see cref="Optional{T}"/> is <see cref="Optional{T}.None"/>.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns the underlying <typeparamref name="T"/> value from the current <see cref="Result{T}"/> of <see cref="Optional{T}"/>,
        /// or returns the default value if the result is in a <see cref="Failure{T}"/> state, or the <see cref="Optional{T}"/> is <see cref="Optional{T}.None"/>.
        /// </returns>
        public async ValueTask<T> GetOptionalValueOrDefaultAsync(T defaultValue, CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).GetOptionalValueOrDefault(defaultValue);
    }

    /// <summary>
    /// Provides extension methods for <see cref="ValueTask{TResult}"/> of <see cref="Failure"/> instances.
    /// </summary>
    /// <param name="receiver">The current <see cref="ValueTask{TResult}"/> of <see cref="Failure"/> instance.</param>
    extension(ValueTask<Failure> receiver)
    {
        /// <summary>
        /// Asynchronously obtains a new <see cref="Failure{T}"/> instance containing the current exception.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the <see cref="Failure{T}"/> to return.</typeparam>
        /// <returns>Returns a new <see cref="Failure{T}"/> instance containing the current exception.</returns>
        public async ValueTask<Failure<TResult>> ToTypedResultAsync<TResult>(CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).ToTypedResult<TResult>();
    }

    /// <summary>
    /// Provides extension methods for <see cref="ValueTask{TResult}"/> of <see cref="Failure{T}"/> instances.
    /// </summary>
    /// <param name="receiver">The current <see cref="ValueTask{TResult}"/> of <see cref="Failure{T}"/> instance.</param>
    extension<T>(ValueTask<Failure<T>> receiver)
    {
        /// <summary>
        /// Asynchronously obtains a new <see cref="Failure{T}"/> instance containing the current exception.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <typeparam name="TResult">The underlying type of the <see cref="Failure{T}"/> to return.</typeparam>
        /// <returns>Returns a new <see cref="Failure{T}"/> instance containing the current exception.</returns>
        public async ValueTask<Failure<TResult>> ToTypedResultAsync<TResult>(CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).ToTypedResult<TResult>();

        /// <summary>
        /// Asynchronously obtains a new <see cref="Failure"/> instance containing the current exception.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>Returns a new <see cref="Failure"/> instance containing the current exception.</returns>
        public async ValueTask<Failure> ToUntypedResultAsync(CancellationToken cancellationToken = default) =>
            (await receiver.WaitAsync(cancellationToken).ConfigureAwait(false)).ToUntypedResult();
    }
}
