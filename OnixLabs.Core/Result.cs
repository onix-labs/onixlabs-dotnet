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
/// Represents a result value, which signifies the presence of a value or an exception.
/// </summary>
/// <typeparam name="T">The type of the underlying result value.</typeparam>
public abstract class Result<T> : IValueEquatable<Result<T>>
{
    /// <summary>
    /// Gets a value indicating whether the current <see cref="Result{T}"/> is in a successful state.
    /// </summary>
    public bool IsSuccess => this is Success<T>;

    /// <summary>
    /// Gets a value indicating whether the current <see cref="Result{T}"/> is in a failed state.
    /// </summary>
    public bool IsFailure => this is Failure<T>;

    /// <summary>
    /// Creates a new instance of the <see cref="Result{T}"/> class, where the underlying value is the result of a successful invocation
    /// of the specified function; otherwise, the underlying value is the exception thrown by a failed invocation of the specified function.
    /// </summary>
    /// <param name="func">The function to invoke to obtain a successful or failed result.</param>
    /// <returns>
    /// Returns a new instance of the <see cref="Result{T}"/> class, where the underlying value is the result of a successful invocation
    /// of the specified function; otherwise, the underlying value is the exception thrown by a failed invocation of the specified function.
    /// </returns>
    public static Result<T> Of(Func<T> func)
    {
        try
        {
            return Success(func());
        }
        catch (Exception exception)
        {
            return Failure(exception);
        }
    }

    /// <summary>
    /// Creates a new instance of the <see cref="Result{T}"/> class, where the underlying value represents a successful result.
    /// </summary>
    /// <param name="value">The underlying successful result value.</param>
    /// <returns>
    /// Returns a new instance of the <see cref="Result{T}"/> class, where the underlying value represents a successful result.
    /// </returns>
    public static Result<T> Success(T value) => new Success<T>(value);

    /// <summary>
    /// Creates a new instance of the <see cref="Result{T}"/> class, where the underlying exception represents a failed result.
    /// </summary>
    /// <param name="exception">The underlying failed exception value.</param>
    /// <returns>
    /// Returns a new instance of the <see cref="Result{T}"/> class, where the underlying exception represents a failed result.
    /// </returns>
    public static Result<T> Failure(Exception exception) => new Failure<T>(exception);

    /// <summary>
    /// Creates a new instance of the <see cref="Result{T}"/> class, where the underlying value represents a successful result.
    /// </summary>
    /// <param name="value">The underlying successful result value.</param>
    /// <returns>
    /// Returns a new instance of the <see cref="Result{T}"/> class, where the underlying value represents a successful result.
    /// </returns>
    public static implicit operator Result<T>(T value) => Success(value);

    /// <summary>
    /// Gets the underlying value of the specified <see cref="Result{T}"/> instance;
    /// otherwise throws the underlying exception if the specified <see cref="Result{T}"/> is in a failed stated.
    /// </summary>
    /// <param name="value">The <see cref="Result{T}"/> value from which to obtain the underlying value.</param>
    /// <returns>
    /// Returns the underlying value of the specified <see cref="Result{T}"/> instance;
    /// otherwise throws the underlying exception if the specified <see cref="Result{T}"/> is in a failed stated.
    /// </returns>
    public static explicit operator T(Result<T> value) => value.GetValueOrThrow();

    /// <summary>
    /// Performs an equality comparison between two object instances.
    /// </summary>
    /// <param name="left">The left-hand instance to compare.</param>
    /// <param name="right">The right-hand instance to compare.</param>
    /// <returns>Returns <see langword="true"/> if the left-hand instance is equal to the right-hand instance; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(Result<T> left, Result<T> right) => Equals(left, right);

    /// <summary>
    /// Performs an inequality comparison between two object instances.
    /// </summary>
    /// <param name="left">The left-hand instance to compare.</param>
    /// <param name="right">The right-hand instance to compare.</param>
    /// <returns>Returns <see langword="true"/> if the left-hand instance is not equal to the right-hand instance; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(Result<T> left, Result<T> right) => !Equals(left, right);

    /// <summary>
    /// Checks whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with the current object.</param>
    /// <returns>Returns <see langword="true"/> if the current object is equal to the other parameter; otherwise, <see langword="false"/>.</returns>
    public bool Equals(Result<T>? other) => this switch
    {
        Success<T> success => other is Success<T> successOther && success.Equals(successOther),
        Failure<T> failure => other is Failure<T> failureOther && failure.Equals(failureOther),
        _ => ReferenceEquals(this, other)
    };

    /// <summary>
    /// Checks for equality between the current instance and another object.
    /// </summary>
    /// <param name="obj">The object to check for equality.</param>
    /// <returns>Returns <see langword="true"/> if the object is equal to the current instance; otherwise, <see langword="false"/>.</returns>
    public override bool Equals(object? obj) => Equals(obj as Result<T>);

    /// <summary>
    /// Serves as a hash code function for the current instance.
    /// </summary>
    /// <returns>Returns a hash code for the current instance.</returns>
    public override int GetHashCode() => default;

    /// <summary>
    /// Gets the underlying value of the current <see cref="Result{T}"/> instance, if the underlying value is present;
    /// otherwise returns the default <typeparamref name="T"/> value.
    /// </summary>
    /// <returns>
    /// Returns the underlying value of the current <see cref="Result{T}"/> instance, if the underlying value is present;
    /// otherwise returns the default <typeparamref name="T"/> value.
    /// </returns>
    public abstract T? GetValueOrDefault();

    /// <summary>
    /// Gets the underlying value of the current <see cref="Result{T}"/> instance, if the underlying value is present;
    /// otherwise returns the specified default value.
    /// </summary>
    /// <param name="defaultValue">The default value to return in the event that the underlying value is absent.</param>
    /// <returns>
    /// Returns the underlying value of the current <see cref="Result{T}"/> instance, if the underlying value is present;
    /// otherwise returns the specified default value.
    /// </returns>
    public abstract T GetValueOrDefault(T defaultValue);

    /// <summary>
    /// Gets the underlying value of the current <see cref="Result{T}"/> instance;
    /// otherwise throws the underlying exception if the current <see cref="Result{T}"/> is in a failed stated.
    /// </summary>
    /// <returns>
    /// Returns the underlying value of the current <see cref="Result{T}"/> instance;
    /// otherwise throws the underlying exception if the current <see cref="Result{T}"/> is in a failed stated.
    /// </returns>
    public abstract T GetValueOrThrow();

    /// <summary>
    /// Executes the action that matches the value of the current <see cref="Result{T}"/> instance.
    /// </summary>
    /// <param name="success">The action to execute when the current <see cref="Result{T}"/> instance is in a successful state.</param>
    /// <param name="failure">The action to execute when the current <see cref="Result{T}"/> instance is in a failed state.</param>
    public abstract void Match(Action<T> success, Action<Exception> failure);

    /// <summary>
    /// Executes the function that matches the value of the current <see cref="Result{T}"/> instance and returns its result.
    /// </summary>
    /// <param name="success">The action to execute when the current <see cref="Result{T}"/> instance is in a successful state.</param>
    /// <param name="failure">The action to execute when the current <see cref="Result{T}"/> instance is in a failed state.</param>
    /// <typeparam name="TResult">The underlying type of the result produced by the matching function.</typeparam>
    /// <returns>
    /// Returns the result of the <paramref name="success"/> function if the current <see cref="Result{T}"/> instance is in a successful state;
    /// otherwise, returns the result of the <paramref name="failure"/> function if the current <see cref="Result{T}"/> instance is in a failed state.
    /// </returns>
    public abstract TResult Match<TResult>(Func<T, TResult> success, Func<Exception, TResult> failure);

    /// <summary>
    /// Applies the provided selector function to the value of the current <see cref="Result{T}"/> instance.
    /// </summary>
    /// <param name="selector">The function to apply to the value of the current <see cref="Result{T}"/> instance.</param>
    /// <typeparam name="TResult">The underlying type of the result produced by the selector function.</typeparam>
    /// <returns>
    /// Returns a new <see cref="Result{TResult}"/> instance containing the result of the function if the current
    /// <see cref="Result{T}"/> instance is in a successful state; otherwise, returns the current failed <see cref="Result{T}"/> instance.
    /// </returns>
    public abstract Result<TResult> Select<TResult>(Func<T, TResult> selector) where TResult : notnull;

    /// <summary>
    /// Applies the provided selector function to the value of the current <see cref="Result{T}"/> instance.
    /// </summary>
    /// <param name="selector">The function to apply to the value of the current <see cref="Result{T}"/> instance.</param>
    /// <typeparam name="TResult">The underlying type of the result produced by the selector function.</typeparam>
    /// <returns>
    /// Returns a new <see cref="Result{TResult}"/> instance containing the result of the function if the current
    /// <see cref="Result{T}"/> instance is in a successful state; otherwise, returns the current failed <see cref="Result{T}"/> instance.
    /// </returns>
    public abstract Result<TResult> SelectMany<TResult>(Func<T, Result<TResult>> selector) where TResult : notnull;

    /// <summary>
    /// Returns a <see cref="String"/> that represents the current object.
    /// </summary>
    /// <returns>Returns a <see cref="String"/> that represents the current object.</returns>
    public override string ToString() => this switch
    {
        Success<T> success => success.ToString(),
        Failure<T> failure => failure.ToString(),
        _ => base.ToString() ?? GetType().FullName ?? nameof(Result<T>)
    };
}
