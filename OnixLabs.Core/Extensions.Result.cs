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
/// Provides extension methods for <see cref="Result{T}"/> instances.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public static class ResultExtensions
{
    /// <summary>
    /// Asynchronously gets the underlying exception if the current <see cref="Result"/> is in a <see cref="Failure"/> state,
    /// or <see langword="null"/> if the current <see cref="Result"/> is in a <see cref="Success"/> state.
    /// </summary>
    /// <param name="task">The current <see cref="Task{TResult}"/> of <see cref="Result"/>.</param>
    /// <param name="token">The cancellation token that can be used to cancel long-running tasks.</param>
    /// <returns>
    /// Returns the underlying exception if the current <see cref="Result"/> is in a <see cref="Failure"/> state,
    /// or <see langword="null"/> if the current <see cref="Result"/> is in a <see cref="Success"/> state.
    /// </returns>
    public static async Task<Exception?> GetExceptionOrDefaultAsync(this Task<Result> task, CancellationToken token = default) =>
        (await task.WaitAsync(token).ConfigureAwait(false)).GetExceptionOrDefault();

    /// <summary>
    /// Asynchronously gets the underlying exception if the current <see cref="Result"/> is in a <see cref="Failure"/> state,
    /// or the specified default exception if the current <see cref="Result"/> is in a <see cref="Success"/> state.
    /// </summary>
    /// <param name="task">The current <see cref="Task{TResult}"/> of <see cref="Result"/>.</param>
    /// <param name="defaultException">The default exception to return in the event that the current <see cref="Result"/> is in a <see cref="Success"/> state.</param>
    /// <param name="token">The cancellation token that can be used to cancel long-running tasks.</param>
    /// <returns>
    /// Returns the underlying exception if the current <see cref="Result"/> is in a <see cref="Failure"/> state,
    /// or the specified default exception if the current <see cref="Result"/> is in a <see cref="Success"/> state.
    /// </returns>
    public static async Task<Exception> GetExceptionOrDefaultAsync(this Task<Result> task, Exception defaultException, CancellationToken token = default) =>
        (await task.WaitAsync(token).ConfigureAwait(false)).GetExceptionOrDefault(defaultException);

    /// <summary>
    /// Asynchronously gets the underlying exception if the current <see cref="Result"/> is in a <see cref="Failure"/> state,
    /// or throws <see cref="InvalidOperationException"/> if the current <see cref="Result"/> is in a <see cref="Success"/> state.
    /// </summary>
    /// <param name="task">The current <see cref="Task{TResult}"/> of <see cref="Result"/>.</param>
    /// <param name="token">The cancellation token that can be used to cancel long-running tasks.</param>
    /// <returns>
    /// Returns the underlying exception if the current <see cref="Result"/> is in a <see cref="Failure"/> state,
    /// or throws <see cref="InvalidOperationException"/> if the current <see cref="Result"/> is in a <see cref="Success"/> state.
    /// </returns>
    public static async Task<Exception> GetExceptionOrThrowAsync(this Task<Result> task, CancellationToken token = default) =>
        (await task.WaitAsync(token).ConfigureAwait(false)).GetExceptionOrThrow();

    /// <summary>
    /// Asynchronously gets the underlying exception if the current <see cref="Result{T}"/> is in a <see cref="Failure{T}"/> state,
    /// or <see langword="null"/> if the current <see cref="Result{T}"/> is in a <see cref="Success{T}"/> state.
    /// </summary>
    /// <param name="task">The current <see cref="Task{TResult}"/> of <see cref="Result{T}"/>.</param>
    /// <param name="token">The cancellation token that can be used to cancel long-running tasks.</param>
    /// <returns>
    /// Returns the underlying exception if the current <see cref="Result{T}"/> is in a <see cref="Failure{T}"/> state,
    /// or <see langword="null"/> if the current <see cref="Result{T}"/> is in a <see cref="Success{T}"/> state.
    /// </returns>
    public static async Task<Exception?> GetExceptionOrDefaultAsync<T>(this Task<Result<T>> task, CancellationToken token = default) =>
        (await task.WaitAsync(token).ConfigureAwait(false)).GetExceptionOrDefault();

    /// <summary>
    /// Asynchronously gets the underlying exception if the current <see cref="Result{T}"/> is in a <see cref="Failure{T}"/> state,
    /// or the specified default exception if the current <see cref="Result{T}"/> is in a <see cref="Success{T}"/> state.
    /// </summary>
    /// <param name="task">The current <see cref="Task{TResult}"/> of <see cref="Result{T}"/>.</param>
    /// <param name="defaultException">The default exception to return in the event that the current <see cref="Result{T}"/> is in a <see cref="Success{T}"/> state.</param>
    /// <param name="token">The cancellation token that can be used to cancel long-running tasks.</param>
    /// <returns>
    /// Returns the underlying exception if the current <see cref="Result{T}"/> is in a <see cref="Failure{T}"/> state,
    /// or the specified default exception if the current <see cref="Result{T}"/> is in a <see cref="Success{T}"/> state.
    /// </returns>
    public static async Task<Exception> GetExceptionOrDefaultAsync<T>(this Task<Result<T>> task, Exception defaultException, CancellationToken token = default) =>
        (await task.WaitAsync(token).ConfigureAwait(false)).GetExceptionOrDefault(defaultException);

    /// <summary>
    /// Asynchronously gets the underlying exception if the current <see cref="Result{T}"/> is in a <see cref="Failure{T}"/> state,
    /// or throws <see cref="InvalidOperationException"/> if the current <see cref="Result{T}"/> is in a <see cref="Success{T}"/> state.
    /// </summary>
    /// <param name="task">The current <see cref="Task{TResult}"/> of <see cref="Result{T}"/>.</param>
    /// <param name="token">The cancellation token that can be used to cancel long-running tasks.</param>
    /// <returns>
    /// Returns the underlying exception if the current <see cref="Result{T}"/> is in a <see cref="Failure{T}"/> state,
    /// or throws <see cref="InvalidOperationException"/> if the current <see cref="Result{T}"/> is in a <see cref="Success{T}"/> state.
    /// </returns>
    public static async Task<Exception> GetExceptionOrThrowAsync<T>(this Task<Result<T>> task, CancellationToken token = default) =>
        (await task.WaitAsync(token).ConfigureAwait(false)).GetExceptionOrThrow();

    /// <summary>
    /// Asynchronously gets the underlying value of the current <see cref="Result{T}"/> instance, if the underlying value is present;
    /// otherwise returns the default <typeparamref name="T"/> value.
    /// </summary>
    /// <param name="task">The current <see cref="Task{TResult}"/> of <see cref="Result{T}"/>.</param>
    /// <param name="token">The cancellation token that can be used to cancel long-running tasks.</param>
    /// <typeparam name="T">The underlying type of the <see cref="Result{T}"/> value.</typeparam>
    /// <returns>
    /// Returns the underlying value of the current <see cref="Result{T}"/> instance, if the underlying value is present;
    /// otherwise returns the default <typeparamref name="T"/> value.
    /// </returns>
    public static async Task<T?> GetValueOrDefaultAsync<T>(this Task<Result<T>> task, CancellationToken token = default) =>
        (await task.WaitAsync(token).ConfigureAwait(false)).GetValueOrDefault();

    /// <summary>
    /// Asynchronously gets the underlying value of the current <see cref="Result{T}"/> instance, if the underlying value is present;
    /// otherwise returns the specified default value.
    /// </summary>
    /// <param name="task">The current <see cref="Task{TResult}"/> of <see cref="Result{T}"/>.</param>
    /// <param name="defaultValue">The default value to return in the event that the underlying value is absent.</param>
    /// <param name="token">The cancellation token that can be used to cancel long-running tasks.</param>
    /// <typeparam name="T">The underlying type of the <see cref="Result{T}"/> value.</typeparam>
    /// <returns>
    /// Returns the underlying value of the current <see cref="Result{T}"/> instance, if the underlying value is present;
    /// otherwise returns the specified default value.
    /// </returns>
    public static async Task<T> GetValueOrDefaultAsync<T>(this Task<Result<T>> task, T defaultValue, CancellationToken token = default) =>
        (await task.WaitAsync(token).ConfigureAwait(false)).GetValueOrDefault(defaultValue);

    /// <summary>
    /// Asynchronously gets the underlying value of the current <see cref="Result{T}"/> instance;
    /// otherwise throws the underlying exception if the current <see cref="Result{T}"/> is in a failed stated.
    /// </summary>
    /// <param name="task">The current <see cref="Task{TResult}"/> of <see cref="Result{T}"/>.</param>
    /// <param name="token">The cancellation token that can be used to cancel long-running tasks.</param>
    /// <typeparam name="T">The underlying type of the <see cref="Result{T}"/> value.</typeparam>
    /// <returns>
    /// Returns the underlying value of the current <see cref="Result{T}"/> instance;
    /// otherwise throws the underlying exception if the current <see cref="Result{T}"/> is in a failed stated.
    /// </returns>
    public static async Task<T> GetValueOrThrowAsync<T>(this Task<Result<T>> task, CancellationToken token = default) =>
        (await task.WaitAsync(token).ConfigureAwait(false)).GetValueOrThrow();

    /// <summary>
    /// Gets the <see cref="Optional{T}"/> value of the current <see cref="Result{T}"/> instance,
    /// or <see cref="Optional{T}.None"/> if the result is in a <see cref="Failure{T}"/> state.
    /// </summary>
    /// <param name="result">The <see cref="Result{T}"/> instance from which to obtain the <see cref="Optional{T}"/> value.</param>
    /// <typeparam name="T">The underlying type of the <see cref="Optional{T}"/> value.</typeparam>
    /// <returns>
    /// Return the <see cref="Optional{T}"/> value of the current <see cref="Result{T}"/> instance,
    /// or <see cref="Optional{T}.None"/> if the result is in a <see cref="Failure{T}"/> state.
    /// </returns>
    public static Optional<T> GetValueOrNone<T>(this Result<Optional<T>> result) where T : notnull =>
        result is Success<Optional<T>> { Value.HasValue: true } ? result.GetValueOrThrow() : Optional<T>.None;

    /// <summary>
    /// Asynchronously gets the <see cref="Optional{T}"/> value of the current <see cref="Result{T}"/> instance,
    /// or <see cref="Optional{T}.None"/> if the result is in a <see cref="Failure{T}"/> state.
    /// </summary>
    /// <param name="task">The current <see cref="Task{TResult}"/> of <see cref="Result{T}"/> from which to obtain the <see cref="Optional{T}"/> value.</param>
    /// <param name="token">The cancellation token that can be used to cancel long-running tasks.</param>
    /// <typeparam name="T">The underlying type of the <see cref="Optional{T}"/> value.</typeparam>
    /// <returns>
    /// Return the <see cref="Optional{T}"/> value of the current <see cref="Result{T}"/> instance,
    /// or <see cref="Optional{T}.None"/> if the result is in a <see cref="Failure{T}"/> state.
    /// </returns>
    public static async Task<Optional<T>> GetValueOrNoneAsync<T>(this Task<Result<Optional<T>>> task, CancellationToken token = default) where T : notnull =>
        (await task.WaitAsync(token).ConfigureAwait(false)).GetValueOrNone();

    /// <summary>
    /// Gets the underlying <typeparamref name="T"/> value from the current <see cref="Result{T}"/> of <see cref="Optional{T}"/>,
    /// or throws an exception if the result is in a <see cref="Failure{T}"/> state, or the <see cref="Optional{T}"/> is <see cref="Optional{T}.None"/>.
    /// </summary>
    /// <param name="result">The <see cref="Result{T}"/> instance from which to obtain the <see cref="Optional{T}"/> value.</param>
    /// <typeparam name="T">The underlying type of the <see cref="Optional{T}"/> value.</typeparam>
    /// <returns>
    /// Returns the underlying <typeparamref name="T"/> value from the current <see cref="Result{T}"/> of <see cref="Optional{T}"/>,
    /// or throws an exception if the result is in a <see cref="Failure{T}"/> state, or the <see cref="Optional{T}"/> is <see cref="Optional{T}.None"/>.
    /// </returns>
    public static T GetOptionalValueOrThrow<T>(this Result<Optional<T>> result) where T : notnull =>
        result.GetValueOrThrow().GetValueOrThrow();

    /// <summary>
    /// Asynchronously gets the underlying <typeparamref name="T"/> value from the current <see cref="Result{T}"/> of <see cref="Optional{T}"/>,
    /// or throws an exception if the result is in a <see cref="Failure{T}"/> state, or the <see cref="Optional{T}"/> is <see cref="Optional{T}.None"/>.
    /// </summary>
    /// <param name="task">The current <see cref="Task{TResult}"/> of <see cref="Result{T}"/> from which to obtain the <see cref="Optional{T}"/> value.</param>
    /// <param name="token">The cancellation token that can be used to cancel long-running tasks.</param>
    /// <typeparam name="T">The underlying type of the <see cref="Optional{T}"/> value.</typeparam>
    /// <returns>
    /// Returns the underlying <typeparamref name="T"/> value from the current <see cref="Result{T}"/> of <see cref="Optional{T}"/>,
    /// or throws an exception if the result is in a <see cref="Failure{T}"/> state, or the <see cref="Optional{T}"/> is <see cref="Optional{T}.None"/>.
    /// </returns>
    public static async Task<T> GetOptionalValueOrThrowAsync<T>(this Task<Result<Optional<T>>> task, CancellationToken token = default) where T : notnull =>
        (await task.WaitAsync(token).ConfigureAwait(false)).GetOptionalValueOrThrow();

    /// <summary>
    /// Gets the underlying <typeparamref name="T"/> value from the current <see cref="Result{T}"/> of <see cref="Optional{T}"/>,
    /// or returns the default value if the result is in a <see cref="Failure{T}"/> state, or the <see cref="Optional{T}"/> is <see cref="Optional{T}.None"/>.
    /// </summary>
    /// <param name="result">The <see cref="Result{T}"/> instance from which to obtain the <see cref="Optional{T}"/> value.</param>
    /// <param name="defaultValue">The default value to return if the result is in a <see cref="Failure{T}"/> state, or the <see cref="Optional{T}"/> is <see cref="Optional{T}.None"/>.</param>
    /// <typeparam name="T">The underlying type of the <see cref="Optional{T}"/> value.</typeparam>
    /// <returns>
    /// Returns the underlying <typeparamref name="T"/> value from the current <see cref="Result{T}"/> of <see cref="Optional{T}"/>,
    /// or returns the default value if the result is in a <see cref="Failure{T}"/> state, or the <see cref="Optional{T}"/> is <see cref="Optional{T}.None"/>.
    /// </returns>
    public static T GetOptionalValueOrDefault<T>(this Result<Optional<T>> result, T defaultValue) where T : notnull =>
        result is Success<Optional<T>> { Value.HasValue: true } ? result.GetOptionalValueOrThrow() : defaultValue;

    /// <summary>
    /// Asynchronously gets the underlying <typeparamref name="T"/> value from the current <see cref="Result{T}"/> of <see cref="Optional{T}"/>,
    /// or returns the default value if the result is in a <see cref="Failure{T}"/> state, or the <see cref="Optional{T}"/> is <see cref="Optional{T}.None"/>.
    /// </summary>
    /// <param name="task">The current <see cref="Task{TResult}"/> of <see cref="Result{T}"/> from which to obtain the <see cref="Optional{T}"/> value.</param>
    /// <param name="defaultValue">The default value to return if the result is in a <see cref="Failure{T}"/> state, or the <see cref="Optional{T}"/> is <see cref="Optional{T}.None"/>.</param>
    /// <param name="token">The cancellation token that can be used to cancel long-running tasks.</param>
    /// <typeparam name="T">The underlying type of the <see cref="Optional{T}"/> value.</typeparam>
    /// <returns>
    /// Returns the underlying <typeparamref name="T"/> value from the current <see cref="Result{T}"/> of <see cref="Optional{T}"/>,
    /// or returns the default value if the result is in a <see cref="Failure{T}"/> state, or the <see cref="Optional{T}"/> is <see cref="Optional{T}.None"/>.
    /// </returns>
    public static async Task<T> GetOptionalValueOrDefaultAsync<T>(this Task<Result<Optional<T>>> task, T defaultValue, CancellationToken token = default) where T : notnull =>
        (await task.WaitAsync(token).ConfigureAwait(false)).GetOptionalValueOrDefault(defaultValue);

    /// <summary>
    /// Asynchronously executes the action that matches the value of the current <see cref="Result"/> instance.
    /// </summary>
    /// <param name="task">The current <see cref="Task{TResult}"/> of <see cref="Result"/> from which to execute the matching function.</param>
    /// <param name="success">The action to execute when the current <see cref="Result"/> instance is in a successful state.</param>
    /// <param name="failure">The action to execute when the current <see cref="Result"/> instance is in a failed state.</param>
    /// <param name="token">The cancellation token that can be used to cancel long-running tasks.</param>
    public static async Task MatchAsync(this Task<Result> task, Action? success = null, Action<Exception>? failure = null, CancellationToken token = default) =>
        (await task.WaitAsync(token).ConfigureAwait(false)).Match(success, failure);

    /// <summary>
    /// Asynchronously executes the function that matches the value of the current <see cref="Result"/> instance and returns its result.
    /// </summary>
    /// <param name="task">The current <see cref="Task{TResult}"/> of <see cref="Result"/> from which to execute the matching function.</param>
    /// <param name="success">The action to execute when the current <see cref="Result"/> instance is in a successful state.</param>
    /// <param name="failure">The action to execute when the current <see cref="Result"/> instance is in a failed state.</param>
    /// <param name="token">The cancellation token that can be used to cancel long-running tasks.</param>
    /// <typeparam name="TResult">The underlying type of the result produced by the matching function.</typeparam>
    /// <returns>
    /// Returns the result of the <paramref name="success"/> function if the current <see cref="Result"/> instance is in a successful state;
    /// otherwise, returns the result of the <paramref name="failure"/> function if the current <see cref="Result"/> instance is in a failed state.
    /// </returns>
    public static async Task<TResult> MatchAsync<TResult>(this Task<Result> task, Func<TResult> success, Func<Exception, TResult> failure, CancellationToken token = default) =>
        (await task.WaitAsync(token).ConfigureAwait(false)).Match(success, failure);

    /// <summary>
    /// Asynchronously executes the action that matches the value of the current <see cref="Result{T}"/> instance.
    /// </summary>
    /// <param name="task">The current <see cref="Task{TResult}"/> of <see cref="Result{T}"/> from which to execute the matching action.</param>
    /// <param name="success">The action to execute when the current <see cref="Result{T}"/> instance is in a successful state.</param>
    /// <param name="failure">The action to execute when the current <see cref="Result{T}"/> instance is in a failed state.</param>
    /// <param name="token">The cancellation token that can be used to cancel long-running tasks.</param>
    /// <typeparam name="T">The underlying type of the <see cref="Result{T}"/>.</typeparam>
    public static async Task MatchAsync<T>(this Task<Result<T>> task, Action<T>? success = null, Action<Exception>? failure = null, CancellationToken token = default) =>
        (await task.WaitAsync(token).ConfigureAwait(false)).Match(success, failure);

    /// <summary>
    /// Asynchronously executes the function that matches the value of the current <see cref="Result{T}"/> instance and returns its result.
    /// </summary>
    /// <param name="task">The current <see cref="Task{TResult}"/> of <see cref="Result{T}"/> from which to execute the matching function.</param>
    /// <param name="success">The action to execute when the current <see cref="Result{T}"/> instance is in a successful state.</param>
    /// <param name="failure">The action to execute when the current <see cref="Result{T}"/> instance is in a failed state.</param>
    /// <param name="token">The cancellation token that can be used to cancel long-running tasks.</param>
    /// <typeparam name="T">The underlying type of the <see cref="Result{T}"/>.</typeparam>
    /// <typeparam name="TResult">The underlying type of the result produced by the matching function.</typeparam>
    /// <returns>
    /// Returns the result of the <paramref name="success"/> function if the current <see cref="Result{T}"/> instance is in a successful state;
    /// otherwise, returns the result of the <paramref name="failure"/> function if the current <see cref="Result{T}"/> instance is in a failed state.
    /// </returns>
    public static async Task<TResult> MatchAsync<T, TResult>(this Task<Result<T>> task, Func<T, TResult> success, Func<Exception, TResult> failure, CancellationToken token = default) =>
        (await task.WaitAsync(token).ConfigureAwait(false)).Match(success, failure);

    /// <summary>
    /// Asynchronously applies the provided selector action to the value of the current <see cref="Result"/> instance.
    /// </summary>
    /// <param name="task">The current <see cref="Task{TResult}"/> of <see cref="Result"/> upon which to apply the selector action.</param>
    /// <param name="selector">The action to apply to current <see cref="Result"/> instance.</param>
    /// <param name="token">The cancellation token that can be used to cancel long-running tasks.</param>
    /// <returns>
    /// Returns <see cref="Success"/> if the current <see cref="Result"/> is in a successful state, and the action invocation is also successful; otherwise; <see cref="Failure"/>.
    /// </returns>
    public static async Task<Result> SelectAsync(this Task<Result> task, Action selector, CancellationToken token = default) =>
        (await task.WaitAsync(token).ConfigureAwait(false)).Select(selector);

    /// <summary>
    /// Asynchronously applies the provided selector function to the value of the current <see cref="Result"/> instance.
    /// </summary>
    /// <param name="task">The current <see cref="Task{TResult}"/> of <see cref="Result"/> upon which to apply the selector function.</param>
    /// <param name="selector">The function to apply to the current <see cref="Result"/> instance.</param>
    /// <param name="token">The cancellation token that can be used to cancel long-running tasks.</param>
    /// <typeparam name="TResult">The underlying type of the result produced by the selector function.</typeparam>
    /// <returns>
    /// Returns <see cref="Success{T}"/> if the current <see cref="Result"/> is in a successful state, and the function invocation is also successful; otherwise; <see cref="Failure{T}"/>.
    /// </returns>
    public static async Task<Result<TResult>> SelectAsync<TResult>(this Task<Result> task, Func<TResult> selector, CancellationToken token = default) =>
        (await task.WaitAsync(token).ConfigureAwait(false)).Select(selector);

    /// <summary>
    /// Asynchronously applies the provided selector action to the value of the current <see cref="Result{T}"/> instance.
    /// </summary>
    /// <param name="task">The current <see cref="Task{TResult}"/> of <see cref="Result{T}"/> upon which to apply the selector action.</param>
    /// <param name="selector">The action to apply to the value of the current <see cref="Result{T}"/> instance.</param>
    /// <param name="token">The cancellation token that can be used to cancel long-running tasks.</param>
    /// <returns>
    /// Returns <see cref="Success"/> if the current <see cref="Result{T}"/> is in a successful state, and the action invocation is also successful; otherwise; <see cref="Failure"/>.
    /// </returns>
    public static async Task<Result> SelectAsync<T>(this Task<Result<T>> task, Action<T> selector, CancellationToken token = default) =>
        (await task.WaitAsync(token).ConfigureAwait(false)).Select(selector);

    /// <summary>
    /// Asynchronously applies the provided selector function to the value of the current <see cref="Result{T}"/> instance.
    /// </summary>
    /// <param name="task">The current <see cref="Task{TResult}"/> of <see cref="Result{T}"/> upon which to apply the selector function.</param>
    /// <param name="selector">The function to apply to the value of the current <see cref="Result{T}"/> instance.</param>
    /// <param name="token">The cancellation token that can be used to cancel long-running tasks.</param>
    /// <typeparam name="T">The underlying type of the <see cref="Result{T}"/>.</typeparam>
    /// <typeparam name="TResult">The underlying type of the result produced by the selector function.</typeparam>
    /// <returns>
    /// Returns <see cref="Success{T}"/> if the current <see cref="Result{T}"/> is in a successful state, and the function invocation is also successful; otherwise; <see cref="Failure{T}"/>.
    /// </returns>
    public static async Task<Result<TResult>> SelectAsync<T, TResult>(this Task<Result<T>> task, Func<T, TResult> selector, CancellationToken token = default) =>
        (await task.WaitAsync(token).ConfigureAwait(false)).Select(selector);

    /// <summary>
    /// Asynchronously applies the provided selector function to the value of the current <see cref="Result"/> instance.
    /// </summary>
    /// <param name="task">The current <see cref="Task{TResult}"/> of <see cref="Result"/> upon which to apply the selector function.</param>
    /// <param name="selector">The function to apply to the current <see cref="Result"/> instance.</param>
    /// <param name="token">The cancellation token that can be used to cancel long-running tasks.</param>
    /// <returns>
    /// Returns <see cref="Success"/> if the current <see cref="Result"/> is in a successful state, and the function invocation is also successful; otherwise; <see cref="Failure"/>.
    /// </returns>
    public static async Task<Result> SelectManyAsync(this Task<Result> task, Func<Result> selector, CancellationToken token = default) =>
        (await task.WaitAsync(token).ConfigureAwait(false)).SelectMany(selector);

    /// <summary>
    /// Asynchronously applies the provided selector function to the value of the current <see cref="Result"/> instance.
    /// </summary>
    /// <param name="task">The current <see cref="Task{TResult}"/> of <see cref="Result"/> upon which to apply the selector function.</param>
    /// <param name="selector">The function to apply to the current <see cref="Result"/> instance.</param>
    /// <param name="token">The cancellation token that can be used to cancel long-running tasks.</param>
    /// <typeparam name="TResult">The underlying type of the result produced by the selector function.</typeparam>
    /// <returns>
    /// Returns <see cref="Success{T}"/> if the current <see cref="Result"/> is in a successful state, and the function invocation is also successful; otherwise; <see cref="Failure{T}"/>.
    /// </returns>
    public static async Task<Result<TResult>> SelectManyAsync<TResult>(this Task<Result> task, Func<Result<TResult>> selector, CancellationToken token = default) =>
        (await task.WaitAsync(token).ConfigureAwait(false)).SelectMany(selector);

    /// <summary>
    /// Asynchronously applies the provided selector function to the value of the current <see cref="Result{T}"/> instance.
    /// </summary>
    /// <param name="task">The current <see cref="Task{TResult}"/> of <see cref="Result{T}"/> upon which to apply the selector function.</param>
    /// <param name="selector">The function to apply to the value of the current <see cref="Result{T}"/> instance.</param>
    /// <param name="token">The cancellation token that can be used to cancel long-running tasks.</param>
    /// <typeparam name="T">The underlying type of the <see cref="Result{T}"/>.</typeparam>
    /// <returns>
    /// Returns <see cref="Success"/> if the current <see cref="Result{T}"/> is in a successful state, and the function invocation is also successful; otherwise; <see cref="Failure"/>.
    /// </returns>
    public static async Task<Result> SelectManyAsync<T>(this Task<Result<T>> task, Func<T, Result> selector, CancellationToken token = default) =>
        (await task.WaitAsync(token).ConfigureAwait(false)).SelectMany(selector);

    /// <summary>
    /// Asynchronously applies the provided selector function to the value of the current <see cref="Result{T}"/> instance.
    /// </summary>
    /// <param name="task">The current <see cref="Task{TResult}"/> of <see cref="Result{T}"/> upon which to apply the selector function.</param>
    /// <param name="selector">The function to apply to the value of the current <see cref="Result{T}"/> instance.</param>
    /// <param name="token">The cancellation token that can be used to cancel long-running tasks.</param>
    /// <typeparam name="T">The underlying type of the <see cref="Result{T}"/>.</typeparam>
    /// <typeparam name="TResult">The underlying type of the result produced by the selector function.</typeparam>
    /// <returns>
    /// Returns <see cref="Success{T}"/> if the current <see cref="Result{T}"/> is in a successful state, and the function invocation is also successful; otherwise; <see cref="Failure{T}"/>.
    /// </returns>
    public static async Task<Result<TResult>> SelectManyAsync<T, TResult>(this Task<Result<T>> task, Func<T, Result<TResult>> selector, CancellationToken token = default) =>
        (await task.WaitAsync(token).ConfigureAwait(false)).SelectMany(selector);

    /// <summary>
    /// Asynchronously throws the underlying exception if the current <see cref="Result"/> is in a failure state.
    /// </summary>
    /// <param name="task">The current <see cref="Task{TResult}"/> of <see cref="Result"/> from which to throw the underlying exception.</param>
    /// <param name="token">The cancellation token that can be used to cancel long-running tasks.</param>
    public static async Task ThrowAsync(this Task<Result> task, CancellationToken token = default) =>
        (await task.WaitAsync(token).ConfigureAwait(false)).Throw();

    /// <summary>
    /// Asynchronously throws the underlying exception if the current <see cref="Result{T}"/> is in a failure state.
    /// </summary>
    /// <typeparam name="T">The underlying type of the <see cref="Result{T}"/>.</typeparam>
    /// <param name="task">The current <see cref="Task{TResult}"/> of <see cref="Result{T}"/> from which to throw the underlying exception.</param>
    /// <param name="token">The cancellation token that can be used to cancel long-running tasks.</param>
    public static async Task ThrowAsync<T>(this Task<Result<T>> task, CancellationToken token = default) =>
        (await task.WaitAsync(token).ConfigureAwait(false)).Throw();

    /// <summary>
    /// Asynchronously obtains a new <see cref="Failure{T}"/> instance containing the current exception.
    /// </summary>
    /// <param name="task">The current <see cref="Task{TResult}"/> of <see cref="Failure"/> from which to obtain a typed <see cref="Failure{T}"/> instance.</param>
    /// <param name="token">The cancellation token that can be used to cancel long-running tasks.</param>
    /// <typeparam name="TResult">The underlying type of the <see cref="Failure{T}"/> to return.</typeparam>
    /// <returns>Returns a new <see cref="Failure{T}"/> instance containing the current exception.</returns>
    public static async Task<Failure<TResult>> ToTypedResultAsync<TResult>(this Task<Failure> task, CancellationToken token = default) =>
        (await task.WaitAsync(token).ConfigureAwait(false)).ToTypedResult<TResult>();

    /// <summary>
    /// Asynchronously obtains a new <see cref="Failure{T}"/> instance containing the current exception.
    /// </summary>
    /// <param name="task">The current <see cref="Task{TResult}"/> of <see cref="Failure{T}"/> from which to obtain a typed <see cref="Failure{T}"/> instance.</param>
    /// <param name="token">The cancellation token that can be used to cancel long-running tasks.</param>
    /// /// <typeparam name="T">The underlying type of the current <see cref="Failure{T}"/>.</typeparam>
    /// <typeparam name="TResult">The underlying type of the <see cref="Failure{T}"/> to return.</typeparam>
    /// <returns>Returns a new <see cref="Failure{T}"/> instance containing the current exception.</returns>
    public static async Task<Failure<TResult>> ToTypedResultAsync<T, TResult>(this Task<Failure<T>> task, CancellationToken token = default) =>
        (await task.WaitAsync(token).ConfigureAwait(false)).ToTypedResult<TResult>();

    /// <summary>
    /// Asynchronously obtains a new <see cref="Failure"/> instance containing the current exception.
    /// </summary>
    /// <param name="task">The current <see cref="Task{TResult}"/> of <see cref="Failure{T}"/> from which to obtain an untyped <see cref="Failure"/> instance.</param>
    /// <param name="token">The cancellation token that can be used to cancel long-running tasks.</param>
    /// <typeparam name="T">The underlying type of the current <see cref="Failure{T}"/>.</typeparam>
    /// <returns>Returns a new <see cref="Failure"/> instance containing the current exception.</returns>
    public static async Task<Failure> ToUntypedResultAsync<T>(this Task<Failure<T>> task, CancellationToken token = default) =>
        (await task.WaitAsync(token).ConfigureAwait(false)).ToUntypedResult();
}
