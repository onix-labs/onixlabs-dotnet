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

using System.ComponentModel;

namespace OnixLabs.Core;

/// <summary>
/// Provides extension methods for <see cref="Result{T}"/> instances.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public static class ResultExtensions
{
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
}
