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
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnixLabs.Core;

/// <summary>
/// Represents a successful result.
/// </summary>
public sealed class Success : Result, IValueEquatable<Success>
{
    /// <summary>
    /// Gets the singleton <see cref="Success"/> instance.
    /// </summary>
    // ReSharper disable once HeapView.ObjectAllocation.Evident
    public static readonly Success Instance = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="Success"/> class.
    /// </summary>
    private Success()
    {
    }

    /// <summary>
    /// Performs an equality comparison between two object instances.
    /// </summary>
    /// <param name="left">The left-hand instance to compare.</param>
    /// <param name="right">The right-hand instance to compare.</param>
    /// <returns>Returns <see langword="true"/> if the left-hand instance is equal to the right-hand instance; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(Success left, Success right) => Equals(left, right);

    /// <summary>
    /// Performs an inequality comparison between two object instances.
    /// </summary>
    /// <param name="left">The left-hand instance to compare.</param>
    /// <param name="right">The right-hand instance to compare.</param>
    /// <returns>Returns <see langword="true"/> if the left-hand instance is not equal to the right-hand instance; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(Success left, Success right) => !Equals(left, right);

    /// <summary>
    /// Checks whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with the current object.</param>
    /// <returns>Returns <see langword="true"/> if the current object is equal to the other parameter; otherwise, <see langword="false"/>.</returns>
    public bool Equals(Success? other) => ReferenceEquals(this, Instance) && ReferenceEquals(other, Instance);

    /// <summary>
    /// Checks for equality between the current instance and another object.
    /// </summary>
    /// <param name="obj">The object to check for equality.</param>
    /// <returns>Returns <see langword="true"/> if the object is equal to the current instance; otherwise, <see langword="false"/>.</returns>
    public override bool Equals(object? obj) => Equals(obj as Success);

    /// <summary>
    /// Serves as a hash code function for the current instance.
    /// </summary>
    /// <returns>Returns a hash code for the current instance.</returns>
    public override int GetHashCode() => default;

    /// <summary>
    /// Gets the underlying exception if the current <see cref="Result"/> is in a <see cref="Failure"/> state,
    /// or <see langword="null"/> if the current <see cref="Result"/> is in a <see cref="Success"/> state.
    /// </summary>
    /// <returns>
    /// Returns the underlying exception if the current <see cref="Result"/> is in a <see cref="Failure"/> state,
    /// or <see langword="null"/> if the current <see cref="Result"/> is in a <see cref="Success"/> state.
    /// </returns>
    public override Exception? GetExceptionOrDefault() => null;

    /// <summary>
    /// Gets the underlying exception if the current <see cref="Result"/> is in a <see cref="Failure"/> state,
    /// or the specified default exception if the current <see cref="Result"/> is in a <see cref="Success"/> state.
    /// </summary>
    /// <param name="defaultException">The default exception to return in the event that the current <see cref="Result"/> is in a <see cref="Success"/> state.</param>
    /// <returns>
    /// Returns the underlying exception if the current <see cref="Result"/> is in a <see cref="Failure"/> state,
    /// or the specified default exception if the current <see cref="Result"/> is in a <see cref="Success"/> state.
    /// </returns>
    public override Exception GetExceptionOrDefault(Exception defaultException) => defaultException;

    /// <summary>
    /// Gets the underlying exception if the current <see cref="Result"/> is in a <see cref="Failure"/> state,
    /// or throws <see cref="InvalidOperationException"/> if the current <see cref="Result"/> is in a <see cref="Success"/> state.
    /// </summary>
    /// <returns>
    /// Returns the underlying exception if the current <see cref="Result"/> is in a <see cref="Failure"/> state,
    /// or throws <see cref="InvalidOperationException"/> if the current <see cref="Result"/> is in a <see cref="Success"/> state.
    /// </returns>
    /// <exception cref="InvalidOperationException">If the current <see cref="Result"/> is in a <see cref="Success"/> state.</exception>
    public override Exception GetExceptionOrThrow() => throw new InvalidOperationException("The current result is not in a Failure state.");

    /// <summary>
    /// Executes the action that matches the value of the current <see cref="Result"/> instance.
    /// <remarks>In the case of a success, the success branch is invoked.</remarks>
    /// </summary>
    /// <param name="success">The action to execute when the current <see cref="Result"/> instance is in a successful state.</param>
    /// <param name="failure">The action to execute when the current <see cref="Result"/> instance is in a failed state.</param>
    public override void Match(Action? success = null, Action<Exception>? failure = null) => success?.Invoke();

    /// <summary>
    /// Executes the function that matches the value of the current <see cref="Result"/> instance and returns its result.
    /// <remarks>In the case of a success, the success branch is invoked.</remarks>
    /// </summary>
    /// <param name="success">The function to execute when the current <see cref="Result"/> instance is in a successful state.</param>
    /// <param name="failure">The function to execute when the current <see cref="Result"/> instance is in a failed state.</param>
    /// <typeparam name="TResult">The underlying type of the result produced by the matching function.</typeparam>
    /// <returns>
    /// Returns the result of the <paramref name="success"/> function if the current <see cref="Result"/> instance is in a successful state;
    /// otherwise, returns the result of the <paramref name="failure"/> function if the current <see cref="Result"/> instance is in a failed state.
    /// </returns>
    public override TResult Match<TResult>(Func<TResult> success, Func<Exception, TResult> failure) => success();

    /// <summary>
    /// Applies the provided selector action to the value of the current <see cref="Result"/> instance.
    /// <remarks>In the case of a success, the result of the selector is wrapped into a new <see cref="Result"/> instance.</remarks>
    /// </summary>
    /// <param name="selector">The action to apply to current <see cref="Result"/> instance.</param>
    /// <returns>
    /// Returns <see cref="Success"/> if the current <see cref="Result"/> is in a successful state, and the action invocation is also successful; otherwise; <see cref="Failure"/>.
    /// </returns>
    public override Result Select(Action selector)
    {
        try
        {
            selector();
            return Success();
        }
        catch (Exception exception)
        {
            return Failure(exception);
        }
    }

    /// <summary>
    /// Applies the provided selector function to the value of the current <see cref="Result"/> instance.
    /// <remarks>In the case of a success, the result of the selector is wrapped into a new <see cref="Result"/> instance.</remarks>
    /// </summary>
    /// <param name="selector">The function to apply to the current <see cref="Result"/> instance.</param>
    /// <typeparam name="TResult">The underlying type of the result produced by the selector function.</typeparam>
    /// <returns>
    /// Returns <see cref="Success{T}"/> if the current <see cref="Result"/> is in a successful state, and the function invocation is also successful; otherwise; <see cref="Failure{T}"/>.
    /// </returns>
    public override Result<TResult> Select<TResult>(Func<TResult> selector)
    {
        try
        {
            return Result<TResult>.Success(selector());
        }
        catch (Exception exception)
        {
            return Result<TResult>.Failure(exception);
        }
    }

    /// <summary>
    /// Applies the provided selector function to the value of the current <see cref="Result"/> instance.
    /// <remarks>In the case of a success, the result of the selector is wrapped into a new <see cref="Result"/> instance.</remarks>
    /// </summary>
    /// <param name="selector">The function to apply to the current <see cref="Result"/> instance.</param>
    /// <returns>
    /// Returns <see cref="Success"/> if the current <see cref="Result"/> is in a successful state, and the function invocation is also successful; otherwise; <see cref="Failure"/>.
    /// </returns>
    public override Result SelectMany(Func<Result> selector)
    {
        try
        {
            return selector();
        }
        catch (Exception exception)
        {
            return Failure(exception);
        }
    }

    /// <summary>
    /// Applies the provided selector function to the value of the current <see cref="Result"/> instance.
    /// <remarks>In the case of a success, the result of the selector is wrapped into a new <see cref="Result{T} "/> instance.</remarks>
    /// </summary>
    /// <param name="selector">The function to apply to the current <see cref="Result"/> instance.</param>
    /// <typeparam name="TResult">The underlying type of the result produced by the selector function.</typeparam>
    /// <returns>
    /// Returns <see cref="Success{T}"/> if the current <see cref="Result"/> is in a successful state, and the function invocation is also successful; otherwise; <see cref="Failure{T}"/>.
    /// </returns>
    public override Result<TResult> SelectMany<TResult>(Func<Result<TResult>> selector)
    {
        try
        {
            return selector();
        }
        catch (Exception exception)
        {
            return Result<TResult>.Failure(exception);
        }
    }

    /// <summary>
    /// Throws the underlying exception if the current <see cref="Result"/> is in a failure state.
    /// </summary>
    public override void Throw()
    {
    }

    /// <summary>
    /// Returns a <see cref="String"/> that represents the current object.
    /// </summary>
    /// <returns>Returns a <see cref="String"/> that represents the current object.</returns>
    public override string ToString() => string.Empty;
}

/// <summary>
/// Represents a successful result.
/// </summary>
/// <typeparam name="T">The type of the underlying result value.</typeparam>
public sealed class Success<T> : Result<T>, IValueEquatable<Success<T>>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Success{T}"/> class.
    /// </summary>
    /// <param name="value">The underlying value representing the successful result.</param>
    internal Success(T value) => Value = value;

    /// <summary>
    /// Gets the underlying result value.
    /// </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public T Value { get; }

    /// <summary>
    /// Performs an equality comparison between two object instances.
    /// </summary>
    /// <param name="left">The left-hand instance to compare.</param>
    /// <param name="right">The right-hand instance to compare.</param>
    /// <returns>Returns <see langword="true"/> if the left-hand instance is equal to the right-hand instance; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(Success<T> left, Success<T> right) => Equals(left, right);

    /// <summary>
    /// Performs an inequality comparison between two object instances.
    /// </summary>
    /// <param name="left">The left-hand instance to compare.</param>
    /// <param name="right">The right-hand instance to compare.</param>
    /// <returns>Returns <see langword="true"/> if the left-hand instance is not equal to the right-hand instance; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(Success<T> left, Success<T> right) => !Equals(left, right);

    /// <summary>
    /// Checks whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with the current object.</param>
    /// <returns>Returns <see langword="true"/> if the current object is equal to the other parameter; otherwise, <see langword="false"/>.</returns>
    public bool Equals(Success<T>? other) => ReferenceEquals(this, other) || other is not null && EqualityComparer<T>.Default.Equals(other.Value, Value);

    /// <summary>
    /// Checks for equality between the current instance and another object.
    /// </summary>
    /// <param name="obj">The object to check for equality.</param>
    /// <returns>Returns <see langword="true"/> if the object is equal to the current instance; otherwise, <see langword="false"/>.</returns>
    public override bool Equals(object? obj) => Equals(obj as Success<T>);

    /// <summary>
    /// Serves as a hash code function for the current instance.
    /// </summary>
    /// <returns>Returns a hash code for the current instance.</returns>
    // ReSharper disable once HeapView.PossibleBoxingAllocation
    public override int GetHashCode() => Value?.GetHashCode() ?? default;

    /// <summary>
    /// Gets the underlying exception if the current <see cref="Result{T}"/> is in a <see cref="Failure{T}"/> state,
    /// or <see langword="null"/> if the current <see cref="Result{T}"/> is in a <see cref="Success{T}"/> state.
    /// </summary>
    /// <returns>
    /// Returns the underlying exception if the current <see cref="Result{T}"/> is in a <see cref="Failure{T}"/> state,
    /// or <see langword="null"/> if the current <see cref="Result{T}"/> is in a <see cref="Success{T}"/> state.
    /// </returns>
    public override Exception? GetExceptionOrDefault() => null;

    /// <summary>
    /// Gets the underlying exception if the current <see cref="Result{T}"/> is in a <see cref="Failure{T}"/> state,
    /// or the specified default exception if the current <see cref="Result{T}"/> is in a <see cref="Success{T}"/> state.
    /// </summary>
    /// <param name="defaultException">The default exception to return in the event that the current <see cref="Result{T}"/> is in a <see cref="Success{T}"/> state.</param>
    /// <returns>
    /// Returns the underlying exception if the current <see cref="Result{T}"/> is in a <see cref="Failure"/> state,
    /// or the specified default exception if the current <see cref="Result{T}"/> is in a <see cref="Success{T}"/> state.
    /// </returns>
    public override Exception GetExceptionOrDefault(Exception defaultException) => defaultException;

    /// <summary>
    /// Gets the underlying exception if the current <see cref="Result{T}"/> is in a <see cref="Failure{T}"/> state,
    /// or throws <see cref="InvalidOperationException"/> if the current <see cref="Result{T}"/> is in a <see cref="Success{T}"/> state.
    /// </summary>
    /// <returns>
    /// Returns the underlying exception if the current <see cref="Result{T}"/> is in a <see cref="Failure{T}"/> state,
    /// or throws <see cref="InvalidOperationException"/> if the current <see cref="Result{T}"/> is in a <see cref="Success{T}"/> state.
    /// </returns>
    /// <exception cref="InvalidOperationException">If the current <see cref="Result{T}"/> is in a <see cref="Success{T}"/> state.</exception>
    public override Exception GetExceptionOrThrow() => throw new InvalidOperationException("The current result is not in a Failure<T> state.");

    /// <summary>
    /// Gets the underlying value of the current <see cref="Result{T}"/> instance, if the underlying value is present;
    /// otherwise returns the default <typeparamref name="T"/> value.
    /// </summary>
    /// <returns>
    /// Returns the underlying value of the current <see cref="Result{T}"/> instance, if the underlying value is present;
    /// otherwise returns the default <typeparamref name="T"/> value.
    /// </returns>
    public override T GetValueOrDefault() => Value;

    /// <summary>
    /// Gets the underlying value of the current <see cref="Result{T}"/> instance, if the underlying value is present;
    /// otherwise returns the specified default value.
    /// </summary>
    /// <param name="defaultValue">The default value to return in the event that the underlying value is absent.</param>
    /// <returns>
    /// Returns the underlying value of the current <see cref="Result{T}"/> instance, if the underlying value is present;
    /// otherwise returns the specified default value.
    /// </returns>
    public override T GetValueOrDefault(T defaultValue) => Value ?? defaultValue;

    /// <summary>
    /// Gets the underlying value of the current <see cref="Result{T}"/> instance;
    /// otherwise throws the underlying exception if the current <see cref="Result{T}"/> is in a failed stated.
    /// </summary>
    /// <returns>
    /// Returns the underlying value of the current <see cref="Result{T}"/> instance;
    /// otherwise throws the underlying exception if the current <see cref="Result{T}"/> is in a failed stated.
    /// </returns>
    public override T GetValueOrThrow() => Value;

    /// <summary>
    /// Executes the action that matches the value of the current <see cref="Result{T}"/> instance.
    /// <remarks>In the case of a success, the success branch is invoked.</remarks>
    /// </summary>
    /// <param name="success">The action to execute when the current <see cref="Result{T}"/> instance is in a successful state.</param>
    /// <param name="failure">The action to execute when the current <see cref="Result{T}"/> instance is in a failed state.</param>
    public override void Match(Action<T>? success = null, Action<Exception>? failure = null) => success?.Invoke(Value);

    /// <summary>
    /// Executes the function that matches the value of the current <see cref="Result{T}"/> instance and returns its result.
    /// <remarks>In the case of a success, the success branch is invoked.</remarks>
    /// </summary>
    /// <param name="success">The function to execute when the current <see cref="Result{T}"/> instance is in a successful state.</param>
    /// <param name="failure">The function to execute when the current <see cref="Result{T}"/> instance is in a failed state.</param>
    /// <typeparam name="TResult">The underlying type of the result produced by the matching function.</typeparam>
    /// <returns>
    /// Returns the result of the <paramref name="success"/> function if the current <see cref="Result{T}"/> instance is in a successful state;
    /// otherwise, returns the result of the <paramref name="failure"/> function if the current <see cref="Result{T}"/> instance is in a failed state.
    /// </returns>
    public override TResult Match<TResult>(Func<T, TResult> success, Func<Exception, TResult> failure) => success(Value);

    /// <summary>
    /// Applies the provided selector action to the value of the current <see cref="Result"/> instance.
    /// <remarks>In the case of a success, the result of the selector is wrapped into a new <see cref="Result"/> instance.</remarks>
    /// </summary>
    /// <param name="selector">The action to apply to current <see cref="Result"/> instance.</param>
    /// <returns>
    /// Returns <see cref="Success"/> if the current <see cref="Result"/> is in a successful state, and the action invocation is also successful; otherwise; <see cref="Failure"/>.
    /// </returns>
    public override Result Select(Action<T> selector)
    {
        try
        {
            selector(Value);
            return Result.Success();
        }
        catch (Exception exception)
        {
            return Result.Failure(exception);
        }
    }

    /// <summary>
    /// Applies the provided selector function to the value of the current <see cref="Result"/> instance.
    /// <remarks>In the case of a success, the result of the selector is wrapped into a new <see cref="Result{T}"/> instance.</remarks>
    /// </summary>
    /// <param name="selector">The function to apply to the current <see cref="Result"/> instance.</param>
    /// <typeparam name="TResult">The underlying type of the result produced by the selector function.</typeparam>
    /// <returns>
    /// Returns <see cref="Success{T}"/> if the current <see cref="Result"/> is in a successful state, and the function invocation is also successful; otherwise; <see cref="Failure{T}"/>.
    /// </returns>
    public override Result<TResult> Select<TResult>(Func<T, TResult> selector)
    {
        try
        {
            return selector(Value);
        }
        catch (Exception exception)
        {
            return Result<TResult>.Failure(exception);
        }
    }

    /// <summary>
    /// Applies the provided selector function to the value of the current <see cref="Result"/> instance.
    /// <remarks>In the case of a success, the result of the selector is wrapped into a new <see cref="Result"/> instance.</remarks>
    /// </summary>
    /// <param name="selector">The function to apply to the current <see cref="Result"/> instance.</param>
    /// <returns>
    /// Returns <see cref="Success"/> if the current <see cref="Result"/> is in a successful state, and the function invocation is also successful; otherwise; <see cref="Failure"/>.
    /// </returns>
    public override Result SelectMany(Func<T, Result> selector)
    {
        try
        {
            return selector(Value);
        }
        catch (Exception exception)
        {
            return Result.Failure(exception);
        }
    }

    /// <summary>
    /// Applies the provided selector function to the value of the current <see cref="Result"/> instance.
    /// <remarks>In the case of a success, the result of the selector is wrapped into a new <see cref="Result{T}"/> instance.</remarks>
    /// </summary>
    /// <param name="selector">The function to apply to the current <see cref="Result"/> instance.</param>
    /// <typeparam name="TResult">The underlying type of the result produced by the selector function.</typeparam>
    /// <returns>
    /// Returns <see cref="Success{T}"/> if the current <see cref="Result"/> is in a successful state, and the function invocation is also successful; otherwise; <see cref="Failure{T}"/>.
    /// </returns>
    public override Result<TResult> SelectMany<TResult>(Func<T, Result<TResult>> selector)
    {
        try
        {
            return selector(Value);
        }
        catch (Exception exception)
        {
            return Result<TResult>.Failure(exception);
        }
    }

    /// <summary>
    /// Throws the underlying exception if the current <see cref="Result{T}"/> is in a failure state.
    /// </summary>
    public override void Throw()
    {
    }

    /// <summary>
    /// Returns a <see cref="String"/> that represents the current object.
    /// </summary>
    /// <returns>Returns a <see cref="String"/> that represents the current object.</returns>
    // ReSharper disable once HeapView.PossibleBoxingAllocation
    public override string ToString() => Value?.ToString() ?? string.Empty;

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public override void Dispose()
    {
        // ReSharper disable once HeapView.PossibleBoxingAllocation
        if (Value is IDisposable disposable)
            disposable.Dispose();
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources asynchronously.
    /// </summary>
    /// <returns>
    /// Returns a task that represents the asynchronous dispose operation.
    /// </returns>
    public override async ValueTask DisposeAsync()
    {
        // ReSharper disable once HeapView.PossibleBoxingAllocation
        if (Value is IAsyncDisposable disposable)
            await disposable.DisposeAsync();
    }
}
