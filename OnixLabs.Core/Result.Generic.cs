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
/// <typeparam name="T">The type of the underlying result value.</typeparam>
public abstract class Result<T> : IValueEquatable<Result<T>>, IDisposable, IAsyncDisposable
{
    // ReSharper disable once StaticMemberInGenericType, HeapView.ObjectAllocation.Evident
    private static readonly InvalidOperationException UnrecognisedResultType = new("The type of the current result is unrecognised.");

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
    /// <param name="token">The cancellation token that can be used to cancel long-running tasks.</param>
    /// <returns>
    /// Returns a new instance of the <see cref="Result{T}"/> class, where the underlying value is the result of a successful invocation
    /// of the specified function; otherwise, the underlying value is the exception thrown by a failed invocation of the specified function.
    /// </returns>
    public static async Task<Result<T>> OfAsync(Func<Task<T>> func, CancellationToken token = default)
    {
        try
        {
            return await func().WaitAsync(token).ConfigureAwait(false);
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
            return await func(token).ConfigureAwait(false);
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
    // ReSharper disable once HeapView.ObjectAllocation.Evident
    public static Success<T> Success(T value) => new(value);

    /// <summary>
    /// Creates a new instance of the <see cref="Result{T}"/> class, where the underlying exception represents a failed result.
    /// </summary>
    /// <param name="exception">The underlying failed exception value.</param>
    /// <returns>
    /// Returns a new instance of the <see cref="Result{T}"/> class, where the underlying exception represents a failed result.
    /// </returns>
    // ReSharper disable once HeapView.ObjectAllocation.Evident
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
    public static bool operator ==(Result<T>? left, Result<T>? right) => Equals(left, right);

    /// <summary>
    /// Performs an inequality comparison between two object instances.
    /// </summary>
    /// <param name="left">The left-hand instance to compare.</param>
    /// <param name="right">The right-hand instance to compare.</param>
    /// <returns>Returns <see langword="true"/> if the left-hand instance is not equal to the right-hand instance; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(Result<T>? left, Result<T>? right) => !Equals(left, right);

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
        // ReSharper disable once HeapView.PossibleBoxingAllocation
        if (this is Success<T> { Value: IDisposable disposable })
            disposable.Dispose();

        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources asynchronously.
    /// </summary>
    /// <returns>
    /// Returns a task that represents the asynchronous dispose operation.
    /// </returns>
    public async ValueTask DisposeAsync()
    {
        // ReSharper disable once HeapView.PossibleBoxingAllocation
        if (this is Success<T> { Value: IAsyncDisposable disposable })
            await disposable.DisposeAsync();

        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Checks whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with the current object.</param>
    /// <returns>Returns <see langword="true"/> if the current object is equal to the other parameter; otherwise, <see langword="false"/>.</returns>
    public bool Equals(Result<T>? other) => ResultEqualityComparer<T>.Default.Equals(this, other);

    /// <summary>
    /// Checks for equality between the current instance and another object.
    /// </summary>
    /// <param name="obj">The object to check for equality.</param>
    /// <returns>Returns <see langword="true"/> if the object is equal to the current instance; otherwise, <see langword="false"/>.</returns>
    public sealed override bool Equals(object? obj) => Equals(obj as Result<T>);

    /// <summary>
    /// Serves as a hash code function for the current instance.
    /// </summary>
    /// <returns>Returns a hash code for the current instance.</returns>
    public sealed override int GetHashCode() => this switch
    {
        // ReSharper disable once HeapView.PossibleBoxingAllocation
        Success<T> success => success.Value?.GetHashCode() ?? default,
        Failure<T> failure => failure.Exception.GetHashCode(),
        _ => throw UnrecognisedResultType
    };

    /// <summary>
    /// Gets the underlying exception if the current <see cref="Result{T}"/> is in a <see cref="Failure{T}"/> state,
    /// or <see langword="null"/> if the current <see cref="Result{T}"/> is in a <see cref="Success{T}"/> state.
    /// </summary>
    /// <returns>
    /// Returns the underlying exception if the current <see cref="Result{T}"/> is in a <see cref="Failure{T}"/> state,
    /// or <see langword="null"/> if the current <see cref="Result{T}"/> is in a <see cref="Success{T}"/> state.
    /// </returns>
    public Exception? GetExceptionOrDefault() => this is Failure<T> failure ? failure.Exception : null;

    /// <summary>
    /// Gets the underlying exception if the current <see cref="Result{T}"/> is in a <see cref="Failure{T}"/> state,
    /// or the specified default exception if the current <see cref="Result{T}"/> is in a <see cref="Success{T}"/> state.
    /// </summary>
    /// <param name="defaultException">The default exception to return in the event that the current <see cref="Result{T}"/> is in a <see cref="Success{T}"/> state.</param>
    /// <returns>
    /// Returns the underlying exception if the current <see cref="Result{T}"/> is in a <see cref="Failure"/> state,
    /// or the specified default exception if the current <see cref="Result{T}"/> is in a <see cref="Success{T}"/> state.
    /// </returns>
    public Exception GetExceptionOrDefault(Exception defaultException) => this is Failure<T> failure ? failure.Exception : defaultException;

    /// <summary>
    /// Gets the underlying exception if the current <see cref="Result{T}"/> is in a <see cref="Failure{T}"/> state,
    /// or throws <see cref="InvalidOperationException"/> if the current <see cref="Result{T}"/> is in a <see cref="Success{T}"/> state.
    /// </summary>
    /// <returns>
    /// Returns the underlying exception if the current <see cref="Result{T}"/> is in a <see cref="Failure{T}"/> state,
    /// or throws <see cref="InvalidOperationException"/> if the current <see cref="Result{T}"/> is in a <see cref="Success{T}"/> state.
    /// </returns>
    /// <exception cref="InvalidOperationException">If the current <see cref="Result{T}"/> is in a <see cref="Success{T}"/> state.</exception>
    public Exception GetExceptionOrThrow() => this is Failure<T> failure ? failure.Exception : throw new InvalidOperationException("The current result is not in a failure state.");

    /// <summary>
    /// Gets the underlying value of the current <see cref="Result{T}"/> instance, if the underlying value is present;
    /// otherwise returns the default <typeparamref name="T"/> value.
    /// </summary>
    /// <returns>
    /// Returns the underlying value of the current <see cref="Result{T}"/> instance, if the underlying value is present;
    /// otherwise returns the default <typeparamref name="T"/> value.
    /// </returns>
    public T? GetValueOrDefault() => this is Success<T> success ? success.Value : default;

    /// <summary>
    /// Gets the underlying value of the current <see cref="Result{T}"/> instance, if the underlying value is present;
    /// otherwise returns the specified default value.
    /// </summary>
    /// <param name="defaultValue">The default value to return in the event that the underlying value is absent.</param>
    /// <returns>
    /// Returns the underlying value of the current <see cref="Result{T}"/> instance, if the underlying value is present;
    /// otherwise returns the specified default value.
    /// </returns>
    public T GetValueOrDefault(T defaultValue) => this is Success<T> success ? success.Value : defaultValue;

    /// <summary>
    /// Gets the underlying value of the current <see cref="Result{T}"/> instance;
    /// otherwise throws the underlying exception if the current <see cref="Result{T}"/> is in a failed stated.
    /// </summary>
    /// <returns>
    /// Returns the underlying value of the current <see cref="Result{T}"/> instance;
    /// otherwise throws the underlying exception if the current <see cref="Result{T}"/> is in a failed stated.
    /// </returns>
    public T GetValueOrThrow() => this is Success<T> success ? success.Value : throw GetExceptionOrThrow();

    /// <summary>
    /// Executes the action that matches the value of the current <see cref="Result{T}"/> instance.
    /// </summary>
    /// <param name="success">The action to execute when the current <see cref="Result{T}"/> instance is in a successful state.</param>
    /// <param name="failure">The action to execute when the current <see cref="Result{T}"/> instance is in a failed state.</param>
    public void Match(Action<T>? success = null, Action<Exception>? failure = null)
    {
        switch (this)
        {
            case Success<T> successResult:
                success?.Invoke(successResult.Value);
                break;
            case Failure<T> failureResult:
                failure?.Invoke(failureResult.Exception);
                break;
        }
    }

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
    public TResult Match<TResult>(Func<T, TResult> success, Func<Exception, TResult> failure) => this switch
    {
        Success<T> successResult => success(successResult.Value),
        Failure<T> failureResult => failure(failureResult.Exception),
        _ => throw UnrecognisedResultType
    };

    /// <summary>
    /// Applies the provided selector action to the value of the current <see cref="Result{T}"/> instance.
    /// </summary>
    /// <param name="selector">The action to apply to the value of the current <see cref="Result{T}"/> instance.</param>
    /// <returns>
    /// Returns <see cref="Success"/> if the current <see cref="Result{T}"/> is in a successful state, and the action invocation is also successful; otherwise; <see cref="Failure"/>.
    /// </returns>
    public Result Select(Action<T> selector)
    {
        if (this is not Success<T> success) return GetExceptionOrThrow();

        try
        {
            selector(success.Value);
            return Result.Success();
        }
        catch (Exception exception)
        {
            return exception;
        }
    }

    /// <summary>
    /// Applies the provided selector function to the value of the current <see cref="Result{T}"/> instance.
    /// </summary>
    /// <param name="selector">The function to apply to the value of the current <see cref="Result{T}"/> instance.</param>
    /// <typeparam name="TResult">The underlying type of the result produced by the selector function.</typeparam>
    /// <returns>
    /// Returns <see cref="Success{T}"/> if the current <see cref="Result{T}"/> is in a successful state, and the function invocation is also successful; otherwise; <see cref="Failure{T}"/>.
    /// </returns>
    public Result<TResult> Select<TResult>(Func<T, TResult> selector)
    {
        if (this is not Success<T> success) return GetExceptionOrThrow();

        try
        {
            return selector(success.Value);
        }
        catch (Exception exception)
        {
            return exception;
        }
    }

    /// <summary>
    /// Applies the provided selector function to the value of the current <see cref="Result{T}"/> instance.
    /// </summary>
    /// <param name="selector">The function to apply to the value of the current <see cref="Result{T}"/> instance.</param>
    /// <returns>
    /// Returns <see cref="Success"/> if the current <see cref="Result{T}"/> is in a successful state, and the function invocation is also successful; otherwise; <see cref="Failure"/>.
    /// </returns>
    public Result SelectMany(Func<T, Result> selector)
    {
        if (this is not Success<T> success) return GetExceptionOrThrow();

        try
        {
            return selector(success.Value);
        }
        catch (Exception exception)
        {
            return exception;
        }
    }

    /// <summary>
    /// Applies the provided selector function to the value of the current <see cref="Result{T}"/> instance.
    /// </summary>
    /// <param name="selector">The function to apply to the value of the current <see cref="Result{T}"/> instance.</param>
    /// <typeparam name="TResult">The underlying type of the result produced by the selector function.</typeparam>
    /// <returns>
    /// Returns <see cref="Success{T}"/> if the current <see cref="Result{T}"/> is in a successful state, and the function invocation is also successful; otherwise; <see cref="Failure{T}"/>.
    /// </returns>
    public Result<TResult> SelectMany<TResult>(Func<T, Result<TResult>> selector)
    {
        if (this is not Success<T> success) return GetExceptionOrThrow();

        try
        {
            return selector(success.Value);
        }
        catch (Exception exception)
        {
            return exception;
        }
    }

    /// <summary>
    /// Throws the underlying exception if the current <see cref="Result{T}"/> is in a failure state.
    /// <remarks>Throwing the underlying exception from a location where it was not generated will yield an incorrect stack trace.</remarks>
    /// </summary>
    public void Throw()
    {
        if (this is Failure<T> failure)
            throw failure.Exception;
    }

    /// <summary>
    /// Returns a <see cref="String"/> that represents the current object.
    /// </summary>
    /// <returns>Returns a <see cref="String"/> that represents the current object.</returns>
    public sealed override string ToString() => this switch
    {
        // ReSharper disable once HeapView.PossibleBoxingAllocation
        Success<T> success => success.Value?.ToString() ?? string.Empty,
        Failure<T> failure => failure.Exception.Message,
        _ => throw UnrecognisedResultType
    };
}

/// <summary>
/// Represents a successful result.
/// </summary>
/// <typeparam name="T">The type of the underlying result value.</typeparam>
public sealed class Success<T> : Result<T>
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
}

/// <summary>
/// Represents a failed result.
/// </summary>
/// <typeparam name="T">The type of the underlying result value.</typeparam>
public sealed class Failure<T> : Result<T>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Failure{T}"/> class.
    /// </summary>
    /// <param name="exception">The underlying exception representing the failed result.</param>
    internal Failure(Exception exception) => Exception = exception;

    /// <summary>
    /// Gets the underlying result exception.
    /// </summary>
    public Exception Exception { get; }

    /// <summary>
    /// Obtains a new <see cref="Failure{T}"/> instance containing the current exception.
    /// </summary>
    /// <typeparam name="TResult">The underlying type of the <see cref="Failure{T}"/> to return.</typeparam>
    /// <returns>Returns a new <see cref="Failure{T}"/> instance containing the current exception.</returns>
    public Failure<TResult> ToTypedResult<TResult>() => Result<TResult>.Failure(Exception);

    /// <summary>
    /// Obtains a new <see cref="Failure"/> instance containing the current exception.
    /// </summary>
    /// <returns>Returns a new <see cref="Failure"/> instance containing the current exception.</returns>
    public Failure ToUntypedResult() => Result.Failure(Exception);
}
