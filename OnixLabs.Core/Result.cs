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

namespace OnixLabs.Core;

/// <summary>
/// Represents a result type, which can either hold an underlying value or signify that an error occurred.
/// </summary>
/// <typeparam name="T">The underlying result type.</typeparam>
public readonly record struct Result<T> : IValueEquatable<Result<T>>
{
    /// <summary>
    /// The underlying result value.
    /// </summary>
    private readonly T? value = default;

    /// <summary>
    /// The underlying result exception.
    /// </summary>
    private readonly Exception? exception = default;

    /// <summary>
    /// Initializes a new instance of the <see cref="Result{T}"/> struct.
    /// </summary>
    /// <param name="value">The underlying result value.</param>
    public Result(T value)
    {
        this.value = value;
        HasValue = true;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Result{T}"/> struct.
    /// </summary>
    /// <param name="exception">The underlying result exception.</param>
    public Result(Exception exception)
    {
        this.exception = exception;
        HasValue = false;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Result{T}"/> struct.
    /// This represents a failed result with an unknown error.
    /// </summary>
    public Result()
    {
        exception = new Exception("Unknown error.");
        HasValue = false;
    }

    /// <summary>
    /// Gets the underlying result value.
    /// </summary>
    /// <exception cref="Exception">Throws the underlying result exception if the result value is not present.</exception>
    public T Value => GetValueOrThrow();

    /// <summary>
    /// Gets the underlying result exception.
    /// </summary>
    public Exception Error => exception!;

    /// <summary>
    /// Gets a value indicating whether the underlying result value is present.
    /// </summary>
    public bool HasValue { get; }

    /// <summary>
    /// Gets a value indicating whether the underlying result exception is present.
    /// </summary>
    public bool HasError => !HasValue;

    /// <summary>
    /// Gets a new instance of the <see cref="Result{T}"/> struct where the underlying value is present.
    /// </summary>
    /// <param name="value">The underlying result value.</param>
    /// <returns>Returns a new instance of the <see cref="Result{T}"/> struct where the underlying value is present.</returns>
    public static Result<T> FromValue(T value)
    {
        return new Result<T>(value);
    }

    /// <summary>
    /// Gets a new instance of the <see cref="Result{T}"/> struct where the underlying exception is present.
    /// </summary>
    /// <param name="exception">The underlying result exception.</param>
    /// <returns>Returns a new instance of the <see cref="Result{T}"/> struct where the underlying exception is present.</returns>
    public static Result<T> FromError(Exception exception)
    {
        return new Result<T>(exception);
    }

    /// <summary>
    /// Gets the underlying value of the current <see cref="Result{T}"/> instance.
    /// </summary>
    /// <returns>Returns the underlying value of the current <see cref="Result{T}"/> instance.</returns>
    /// <exception cref="Exception">Throws the underlying result exception if the result value is not present.</exception>
    public static explicit operator T(Result<T> value)
    {
        return value.Value;
    }

    /// <summary>
    /// Gets a new instance of the <see cref="Result{T}"/> struct where the underlying value is present.
    /// </summary>
    /// <param name="value">The underlying result value.</param>
    /// <returns>Returns a new instance of the <see cref="Result{T}"/> struct where the underlying value is present.</returns>
    public static implicit operator Result<T>(T value)
    {
        return FromValue(value);
    }

    /// <summary>
    /// Gets the underlying value of the current <see cref="Result{T}"/> instance.
    /// </summary>
    /// <returns>Returns the underlying value of the current <see cref="Result{T}"/> instance.</returns>
    /// <exception cref="Exception">Throws the underlying result exception if the result value is not present.</exception>
    public T GetValueOrThrow()
    {
        if (HasValue) return value!;
        throw Error;
    }

    /// <summary>
    /// Gets the underlying value of the current <see cref="Result{T}"/> instance, or the specified default value, if the underlying value is not present.
    /// </summary>
    /// <returns>>Returns the underlying value of the current <see cref="Result{T}"/> instance, or the specified default value, if the underlying value is not present.</returns>
    public T GetValueOrDefault(T defaultValue)
    {
        return HasValue ? Value : defaultValue;
    }

    /// <summary>
    /// Matches the value of the current <see cref="Result{T}"/> and executes one of the specified functions depending on the presence of an underlying value.
    /// </summary>
    /// <param name="value">The function to execute if the underlying value of the current <see cref="Optional{T}"/> is present.</param>
    /// <param name="error">The function to execute if the underlying error of the current <see cref="Optional{T}"/> is present.</param>
    // ReSharper disable once ParameterHidesMember
    public void Match(Action<T> value, Action<Exception> error)
    {
        if (HasValue) value(Value);
        else error(Error);
    }

    /// <summary>
    /// Matches the value of the current <see cref="Result{T}"/> and executes one of the specified functions depending on the presence of an underlying value.
    /// </summary>
    /// <param name="value">The function to execute if the underlying value of the current <see cref="Optional{T}"/> is present.</param>
    /// <param name="error">The function to execute if the underlying error of the current <see cref="Optional{T}"/> is present.</param>
    /// <typeparam name="TResult">The underlying type of the result produced by the function.</typeparam>
    /// <returns>
    /// Returns the result of the <paramref name="value"/> function if the underlying value of the current <see cref="Result{T}"/> value is present;
    /// otherwise, returns the result of the <paramref name="error"/> function if the underlying error of the current <see cref="Result{T}"/> value is present.
    /// </returns>
    // ReSharper disable once ParameterHidesMember
    public TResult Match<TResult>(Func<T, TResult> value, Func<Exception, TResult> error)
    {
        return HasValue ? value(Value) : error(Error);
    }

    /// <summary>
    /// Applies the provided function to the value of the current <see cref="Result{T}"/> instance if its value is present.
    /// </summary>
    /// <param name="selector">The function to apply to the value of the current <see cref="Result{T}"/> instance.</param>
    /// <typeparam name="TResult">The underlying type of the result produced by the function.</typeparam>
    /// <returns>
    /// Returns a new <see cref="Result{TResult}"/> instance containing the result of the function if the current
    /// <see cref="Result{T}"/> instance has an underlying value; otherwise, an errored <see cref="Result{TResult}"/> is returned.
    /// </returns>
    public Result<TResult> Select<TResult>(Func<T, TResult> selector)
    {
        return HasValue ? Result<TResult>.FromValue(selector(Value)) : Result<TResult>.FromError(Error);
    }

    /// <summary>
    /// Applies the provided function to the value of the current <see cref="Result{T}"/> instance if its value is present.
    /// </summary>
    /// <param name="selector">The function to apply to the value of the current <see cref="Result{T}"/> instance.</param>
    /// <typeparam name="TResult">The underlying type of the result produced by the function.</typeparam>
    /// <returns>
    /// Returns a new <see cref="Result{TResult}"/> instance containing the result of the function if the current
    /// <see cref="Result{T}"/> instance has an underlying value; otherwise, an errored <see cref="Result{TResult}"/> is returned.
    /// </returns>
    public Result<TResult> SelectMany<TResult>(Func<T, Result<TResult>> selector)
    {
        return HasValue ? selector(Value) : Result<TResult>.FromError(Error);
    }

    /// <summary>
    /// Throws the underlying exception if the current <see cref="Result{T}"/> instance is in an errored state.
    /// </summary>
    /// <exception cref="Exception">If the current <see cref="Result{T}"/> is in an errored state.</exception>
    public void Throw()
    {
        if (HasError) throw Error;
    }

    /// <summary>
    /// Returns a <see cref="String"/> that represents the current object.
    /// </summary>
    /// <returns>Returns a <see cref="String"/> that represents the current object.</returns>
    public override string ToString()
    {
        return HasValue ? Value?.ToString() ?? string.Empty : Error.ToString();
    }
}
