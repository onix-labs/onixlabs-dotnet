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
using System.Threading.Tasks;

namespace OnixLabs.Core;

/// <summary>
/// Provides extension methods for <see cref="Result{T}"/> instances.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public static class ResultExtensions
{
    public static async Task<T?> GetValueOrDefaultAsync<T>(this Task<Result<T>> task) =>
        (await task.ConfigureAwait(false)).GetValueOrDefault();

    public static async Task<T> GetValueOrDefaultAsync<T>(this Task<Result<T>> task, T defaultValue) =>
        (await task.ConfigureAwait(false)).GetValueOrDefault(defaultValue);

    public static async Task<T> GetValueOrThrowAsync<T>(this Task<Result<T>> task) =>
        (await task.ConfigureAwait(false)).GetValueOrThrow();

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

    public static async Task<Optional<T>> GetValueOrNoneAsync<T>(this Task<Result<Optional<T>>> task) where T : notnull =>
        (await task.ConfigureAwait(false)).GetValueOrNone();

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

    public static async Task<T> GetOptionalValueOrThrowAsync<T>(this Task<Result<Optional<T>>> task) where T : notnull =>
        (await task.ConfigureAwait(false)).GetOptionalValueOrThrow();

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

    public static async Task<T> GetOptionalValueOrDefaultAsync<T>(this Task<Result<Optional<T>>> task, T defaultValue) where T : notnull =>
        (await task.ConfigureAwait(false)).GetOptionalValueOrDefault(defaultValue);

    public static async Task MatchAsync(this Task<Result> task, Action? success = null, Action<Exception>? failure = null) =>
        (await task.ConfigureAwait(false)).Match(success, failure);

    public static async Task<TResult> MatchAsync<TResult>(this Task<Result> task, Func<TResult> success, Func<Exception, TResult> failure) =>
        (await task.ConfigureAwait(false)).Match(success, failure);

    public static async Task MatchAsync<T>(this Task<Result<T>> task, Action<T>? success = null, Action<Exception>? failure = null) =>
        (await task.ConfigureAwait(false)).Match(success, failure);

    public static async Task<TResult> MatchAsync<T, TResult>(this Task<Result<T>> task, Func<T, TResult> success, Func<Exception, TResult> failure) =>
        (await task.ConfigureAwait(false)).Match(success, failure);

    public static async Task<Result> SelectAsync(this Task<Result> task, Action selector) =>
        (await task.ConfigureAwait(false)).Select(selector);

    public static async Task<Result<TResult>> SelectAsync<TResult>(this Task<Result> task, Func<TResult> selector) =>
        (await task.ConfigureAwait(false)).Select(selector);

    public static async Task<Result> SelectAsync<T>(this Task<Result<T>> task, Action<T> selector) =>
        (await task.ConfigureAwait(false)).Select(selector);

    public static async Task<Result<TResult>> SelectAsync<T, TResult>(this Task<Result<T>> task, Func<T, TResult> selector) =>
        (await task.ConfigureAwait(false)).Select(selector);

    public static async Task<Result> SelectManyAsync(this Task<Result> task, Func<Result> selector) =>
        (await task.ConfigureAwait(false)).SelectMany(selector);

    public static async Task<Result<TResult>> SelectManyAsync<TResult>(this Task<Result> task, Func<Result<TResult>> selector) =>
        (await task.ConfigureAwait(false)).SelectMany(selector);

    public static async Task<Result> SelectManyAsync<T>(this Task<Result<T>> task, Func<T, Result> selector) =>
        (await task.ConfigureAwait(false)).SelectMany(selector);

    public static async Task<Result<TResult>> SelectManyAsync<T, TResult>(this Task<Result<T>> task, Func<T, Result<TResult>> selector) =>
        (await task.ConfigureAwait(false)).SelectMany(selector);

    public static async Task ThrowAsync(this Task<Result> task) => (await task.ConfigureAwait(false)).Throw();
}
