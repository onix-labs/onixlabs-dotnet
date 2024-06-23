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
using System.Threading;
using System.Threading.Tasks;

namespace OnixLabs.Core;

/// <summary>
/// Represents a result value, which signifies the presence of a value or an exception.
/// </summary>
public abstract class Result : IValueEquatable<Result>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Result"/> class.
    /// </summary>
    internal Result()
    {
    }

    /// <summary>
    /// Gets a value indicating whether the current <see cref="Result"/> is in a successful state.
    /// </summary>
    public bool IsSuccess => this is Success;

    /// <summary>
    /// Gets a value indicating whether the current <see cref="Result"/> is in a failed state.
    /// </summary>
    public bool IsFailure => this is Failure;

    /// <summary>
    /// Creates a new instance of the <see cref="Result"/> class, where the underlying value is the result of a successful invocation
    /// of the specified function; otherwise, the underlying value is the exception thrown by a failed invocation of the specified function.
    /// </summary>
    /// <param name="action">The action to invoke to obtain a successful or failed result.</param>
    /// <returns>
    /// Returns a new instance of the <see cref="Result"/> class, where the underlying value is the result of a successful invocation
    /// of the specified function; otherwise, the underlying value is the exception thrown by a failed invocation of the specified function.
    /// </returns>
    public static Result Of(Action action)
    {
        try
        {
            action();
            return Success();
        }
        catch (Exception exception)
        {
            return exception;
        }
    }

    /// <summary>
    /// Creates a new instance of the <see cref="Result"/> class, where the underlying value is the result of a successful invocation
    /// of the specified function; otherwise, the underlying value is the exception thrown by a failed invocation of the specified function.
    /// </summary>
    /// <param name="func">The function to invoke to obtain a successful or failed result.</param>
    /// <returns>
    /// Returns a new instance of the <see cref="Result"/> class, where the underlying value is the result of a successful invocation
    /// of the specified function; otherwise, the underlying value is the exception thrown by a failed invocation of the specified function.
    /// </returns>
    public static async Task<Result> OfAsync(Func<Task> func)
    {
        try
        {
            await func();
            return Success();
        }
        catch (Exception exception)
        {
            return exception;
        }
    }

    /// <summary>
    /// Creates a new instance of the <see cref="Result"/> class, where the underlying value is the result of a successful invocation
    /// of the specified function; otherwise, the underlying value is the exception thrown by a failed invocation of the specified function.
    /// </summary>
    /// <param name="func">The function to invoke to obtain a successful or failed result.</param>
    /// <param name="token">The cancellation token to pass to the invoked function.</param>
    /// <returns>
    /// Returns a new instance of the <see cref="Result"/> class, where the underlying value is the result of a successful invocation
    /// of the specified function; otherwise, the underlying value is the exception thrown by a failed invocation of the specified function.
    /// </returns>
    public static async Task<Result> OfAsync(Func<CancellationToken, Task> func, CancellationToken token = default)
    {
        try
        {
            await func(token);
            return Success();
        }
        catch (Exception exception)
        {
            return exception;
        }
    }

    /// <summary>
    /// Creates a new instance of the <see cref="Result"/> class, where the underlying value represents a successful result.
    /// </summary>
    /// <returns>
    /// Returns a new instance of the <see cref="Result"/> class, where the underlying value represents a successful result.
    /// </returns>
    // ReSharper disable once MemberCanBePrivate.Global
    public static Success Success() => Core.Success.Instance;

    /// <summary>
    /// Creates a new instance of the <see cref="Result"/> class, where the underlying exception represents a failed result.
    /// </summary>
    /// <param name="exception">The underlying failed exception value.</param>
    /// <returns>
    /// Returns a new instance of the <see cref="Result"/> class, where the underlying exception represents a failed result.
    /// </returns>
    // ReSharper disable once MemberCanBePrivate.Global
    public static Failure Failure(Exception exception) => new(exception);

    /// <summary>
    /// Creates a new instance of the <see cref="Result"/> class, where the underlying value represents a failed result.
    /// </summary>
    /// <param name="exception">The underlying failed result exception.</param>
    /// <returns>
    /// Returns a new instance of the <see cref="Result"/> class, where the underlying value represents a failed result.
    /// </returns>
    public static implicit operator Result(Exception exception) => Failure(exception);

    /// <summary>
    /// Performs an equality comparison between two object instances.
    /// </summary>
    /// <param name="left">The left-hand instance to compare.</param>
    /// <param name="right">The right-hand instance to compare.</param>
    /// <returns>Returns <see langword="true"/> if the left-hand instance is equal to the right-hand instance; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(Result left, Result right) => Equals(left, right);

    /// <summary>
    /// Performs an inequality comparison between two object instances.
    /// </summary>
    /// <param name="left">The left-hand instance to compare.</param>
    /// <param name="right">The right-hand instance to compare.</param>
    /// <returns>Returns <see langword="true"/> if the left-hand instance is not equal to the right-hand instance; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(Result left, Result right) => !Equals(left, right);

    /// <summary>
    /// Checks whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with the current object.</param>
    /// <returns>Returns <see langword="true"/> if the current object is equal to the other parameter; otherwise, <see langword="false"/>.</returns>
    public bool Equals(Result? other) => this switch
    {
        Success success => other is Success successOther && success.Equals(successOther),
        Failure failure => other is Failure failureOther && failure.Equals(failureOther),
        _ => ReferenceEquals(this, other)
    };

    /// <summary>
    /// Checks for equality between the current instance and another object.
    /// </summary>
    /// <param name="obj">The object to check for equality.</param>
    /// <returns>Returns <see langword="true"/> if the object is equal to the current instance; otherwise, <see langword="false"/>.</returns>
    public override bool Equals(object? obj) => Equals(obj as Result);

    /// <summary>
    /// Serves as a hash code function for the current instance.
    /// </summary>
    /// <returns>Returns a hash code for the current instance.</returns>
    public override int GetHashCode() => default;

    /// <summary>
    /// Executes the action that matches the value of the current <see cref="Result"/> instance.
    /// </summary>
    /// <param name="success">The action to execute when the current <see cref="Result"/> instance is in a successful state.</param>
    /// <param name="failure">The action to execute when the current <see cref="Result"/> instance is in a failed state.</param>
    public abstract void Match(Action? success = null, Action<Exception>? failure = null);

    /// <summary>
    /// Executes the function that matches the value of the current <see cref="Result"/> instance and returns its result.
    /// </summary>
    /// <param name="success">The action to execute when the current <see cref="Result"/> instance is in a successful state.</param>
    /// <param name="failure">The action to execute when the current <see cref="Result"/> instance is in a failed state.</param>
    /// <typeparam name="TResult">The underlying type of the result produced by the matching function.</typeparam>
    /// <returns>
    /// Returns the result of the <paramref name="success"/> function if the current <see cref="Result"/> instance is in a successful state;
    /// otherwise, returns the result of the <paramref name="failure"/> function if the current <see cref="Result"/> instance is in a failed state.
    /// </returns>
    public abstract TResult Match<TResult>(Func<TResult> success, Func<Exception, TResult> failure);

    /// <summary>
    /// Applies the provided selector action to the value of the current <see cref="Result"/> instance.
    /// </summary>
    /// <param name="selector">The action to apply to current <see cref="Result"/> instance.</param>
    /// <returns>
    /// Returns <see cref="Success"/> if the current <see cref="Result"/> is in a successful state, and the action invocation is also successful; otherwise; <see cref="Failure"/>.
    /// </returns>
    public abstract Result Select(Action selector);

    /// <summary>
    /// Applies the provided selector function to the value of the current <see cref="Result"/> instance.
    /// </summary>
    /// <param name="selector">The function to apply to the current <see cref="Result"/> instance.</param>
    /// <typeparam name="TResult">The underlying type of the result produced by the selector function.</typeparam>
    /// <returns>
    /// Returns <see cref="Success{T}"/> if the current <see cref="Result"/> is in a successful state, and the function invocation is also successful; otherwise; <see cref="Failure{T}"/>.
    /// </returns>
    public abstract Result<TResult> Select<TResult>(Func<TResult> selector);

    /// <summary>
    /// Applies the provided selector function to the value of the current <see cref="Result"/> instance.
    /// </summary>
    /// <param name="selector">The function to apply to the current <see cref="Result"/> instance.</param>
    /// <returns>
    /// Returns <see cref="Success"/> if the current <see cref="Result"/> is in a successful state, and the function invocation is also successful; otherwise; <see cref="Failure"/>.
    /// </returns>
    public abstract Result SelectMany(Func<Result> selector);

    /// <summary>
    /// Applies the provided selector function to the value of the current <see cref="Result"/> instance.
    /// </summary>
    /// <param name="selector">The function to apply to the current <see cref="Result"/> instance.</param>
    /// <typeparam name="TResult">The underlying type of the result produced by the selector function.</typeparam>
    /// <returns>
    /// Returns <see cref="Success{T}"/> if the current <see cref="Result"/> is in a successful state, and the function invocation is also successful; otherwise; <see cref="Failure{T}"/>.
    /// </returns>
    public abstract Result<TResult> SelectMany<TResult>(Func<Result<TResult>> selector);

    /// <summary>
    /// Throws the underlying exception if the current <see cref="Result"/> is in a failure state.
    /// </summary>
    public abstract void Throw();

    /// <summary>
    /// Returns a <see cref="String"/> that represents the current object.
    /// </summary>
    /// <returns>Returns a <see cref="String"/> that represents the current object.</returns>
    public override string ToString() => this switch
    {
        Success success => success.ToString(),
        Failure failure => failure.ToString(),
        _ => base.ToString() ?? GetType().FullName ?? nameof(Result)
    };
}

/// <summary>
/// Represents a result value, which signifies the presence of a value or an exception.
/// </summary>
/// <typeparam name="T">The type of the underlying result value.</typeparam>
public abstract class Result<T> : IValueEquatable<Result<T>>, IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Result{T}"/> class.
    /// </summary>
    internal Result()
    {
    }

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
            return func();
        }
        catch (Exception exception)
        {
            return exception;
        }
    }

    /// <summary>
    /// Creates a new instance of the <see cref="Result{T}"/> class, where the underlying value is the result of a successful invocation
    /// of the specified function; otherwise, the underlying value is the exception thrown by a failed invocation of the specified function.
    /// </summary>
    /// <param name="func">The function to invoke to obtain a successful or failed result.</param>
    /// <returns>
    /// Returns a new instance of the <see cref="Result{T}"/> class, where the underlying value is the result of a successful invocation
    /// of the specified function; otherwise, the underlying value is the exception thrown by a failed invocation of the specified function.
    /// </returns>
    public static async Task<Result<T>> OfAsync(Func<Task<T>> func)
    {
        try
        {
            return await func();
        }
        catch (Exception exception)
        {
            return exception;
        }
    }

    /// <summary>
    /// Creates a new instance of the <see cref="Result{T}"/> class, where the underlying value is the result of a successful invocation
    /// of the specified function; otherwise, the underlying value is the exception thrown by a failed invocation of the specified function.
    /// </summary>
    /// <param name="func">The function to invoke to obtain a successful or failed result.</param>
    /// <param name="token">The cancellation token to pass to the invoked function.</param>
    /// <returns>
    /// Returns a new instance of the <see cref="Result{T}"/> class, where the underlying value is the result of a successful invocation
    /// of the specified function; otherwise, the underlying value is the exception thrown by a failed invocation of the specified function.
    /// </returns>
    public static async Task<Result<T>> OfAsync(Func<CancellationToken, Task<T>> func, CancellationToken token = default)
    {
        try
        {
            return await func(token);
        }
        catch (Exception exception)
        {
            return exception;
        }
    }

    /// <summary>
    /// Creates a new instance of the <see cref="Result{T}"/> class, where the underlying value represents a successful result.
    /// </summary>
    /// <param name="value">The underlying successful result value.</param>
    /// <returns>
    /// Returns a new instance of the <see cref="Result{T}"/> class, where the underlying value represents a successful result.
    /// </returns>
    public static Success<T> Success(T value) => new(value);

    /// <summary>
    /// Creates a new instance of the <see cref="Result{T}"/> class, where the underlying exception represents a failed result.
    /// </summary>
    /// <param name="exception">The underlying failed exception value.</param>
    /// <returns>
    /// Returns a new instance of the <see cref="Result{T}"/> class, where the underlying exception represents a failed result.
    /// </returns>
    public static Failure<T> Failure(Exception exception) => new(exception);

    /// <summary>
    /// Creates a new instance of the <see cref="Result{T}"/> class, where the underlying value represents a successful result.
    /// </summary>
    /// <param name="value">The underlying successful result value.</param>
    /// <returns>
    /// Returns a new instance of the <see cref="Result{T}"/> class, where the underlying value represents a successful result.
    /// </returns>
    public static implicit operator Result<T>(T value) => Success(value);

    /// <summary>
    /// Creates a new instance of the <see cref="Result{T}"/> class, where the underlying value represents a failed result.
    /// </summary>
    /// <param name="exception">The underlying failed result exception.</param>
    /// <returns>
    /// Returns a new instance of the <see cref="Result{T}"/> class, where the underlying value represents a failed result.
    /// </returns>
    public static implicit operator Result<T>(Exception exception) => Failure(exception);

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
    public abstract void Match(Action<T>? success = null, Action<Exception>? failure = null);

    /// <summary>
    /// Executes the function that matches the value of the current <see cref="Result{T}"/> instance and returns its result.
    /// </summary>
    /// <param name="success">The function to execute when the current <see cref="Result{T}"/> instance is in a successful state.</param>
    /// <param name="failure">The function to execute when the current <see cref="Result{T}"/> instance is in a failed state.</param>
    /// <typeparam name="TResult">The underlying type of the result produced by the matching function.</typeparam>
    /// <returns>
    /// Returns the result of the <paramref name="success"/> function if the current <see cref="Result{T}"/> instance is in a successful state;
    /// otherwise, returns the result of the <paramref name="failure"/> function if the current <see cref="Result{T}"/> instance is in a failed state.
    /// </returns>
    public abstract TResult Match<TResult>(Func<T, TResult> success, Func<Exception, TResult> failure);

    /// <summary>
    /// Applies the provided selector action to the value of the current <see cref="Result{T}"/> instance.
    /// </summary>
    /// <param name="selector">The action to apply to the value of the current <see cref="Result{T}"/> instance.</param>
    /// <returns>
    /// Returns <see cref="Success"/> if the current <see cref="Result{T}"/> is in a successful state, and the action invocation is also successful; otherwise; <see cref="Failure"/>.
    /// </returns>
    public abstract Result Select(Action<T> selector);

    /// <summary>
    /// Applies the provided selector function to the value of the current <see cref="Result{T}"/> instance.
    /// </summary>
    /// <param name="selector">The function to apply to the value of the current <see cref="Result{T}"/> instance.</param>
    /// <typeparam name="TResult">The underlying type of the result produced by the selector function.</typeparam>
    /// <returns>
    /// Returns <see cref="Success{T}"/> if the current <see cref="Result{T}"/> is in a successful state, and the function invocation is also successful; otherwise; <see cref="Failure{T}"/>.
    /// </returns>
    public abstract Result<TResult> Select<TResult>(Func<T, TResult> selector);

    /// <summary>
    /// Applies the provided selector function to the value of the current <see cref="Result{T}"/> instance.
    /// </summary>
    /// <param name="selector">The function to apply to the value of the current <see cref="Result{T}"/> instance.</param>
    /// <returns>
    /// Returns <see cref="Success"/> if the current <see cref="Result{T}"/> is in a successful state, and the function invocation is also successful; otherwise; <see cref="Failure"/>.
    /// </returns>
    public abstract Result SelectMany(Func<T, Result> selector);

    /// <summary>
    /// Applies the provided selector function to the value of the current <see cref="Result{T}"/> instance.
    /// </summary>
    /// <param name="selector">The function to apply to the value of the current <see cref="Result{T}"/> instance.</param>
    /// <typeparam name="TResult">The underlying type of the result produced by the selector function.</typeparam>
    /// <returns>
    /// Returns <see cref="Success{T}"/> if the current <see cref="Result{T}"/> is in a successful state, and the function invocation is also successful; otherwise; <see cref="Failure{T}"/>.
    /// </returns>
    public abstract Result<TResult> SelectMany<TResult>(Func<T, Result<TResult>> selector);

    /// <summary>
    /// Throws the underlying exception if the current <see cref="Result{T}"/> is in a failure state.
    /// </summary>
    public abstract void Throw();

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

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public abstract void Dispose();

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources asynchronously.
    /// </summary>
    /// <returns>
    /// Returns a task that represents the asynchronous dispose operation.
    /// </returns>
    public abstract ValueTask DisposeAsync();
}
