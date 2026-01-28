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
using System.Runtime.CompilerServices;
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
    public bool IsSuccess
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => this is Success;
    }

    /// <summary>
    /// Gets a value indicating whether the current <see cref="Result"/> is in a failed state.
    /// </summary>
    public bool IsFailure
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => this is Failure;
    }

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
    /// <param name="token">The cancellation token that can be used to cancel long-running tasks.</param>
    /// <returns>
    /// Returns a new instance of the <see cref="Result"/> class, where the underlying value is the result of a successful invocation
    /// of the specified function; otherwise, the underlying value is the exception thrown by a failed invocation of the specified function.
    /// </returns>
    public static async Task<Result> OfAsync(Func<Task> func, CancellationToken token = default)
    {
        try
        {
            await func().WaitAsync(token).ConfigureAwait(false);
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
            await func(token).ConfigureAwait(false);
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
    // ReSharper disable once MemberCanBePrivate.Global, HeapView.ObjectAllocation.Evident
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
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Result? left, Result? right) => Equals(left, right);

    /// <summary>
    /// Performs an inequality comparison between two object instances.
    /// </summary>
    /// <param name="left">The left-hand instance to compare.</param>
    /// <param name="right">The right-hand instance to compare.</param>
    /// <returns>Returns <see langword="true"/> if the left-hand instance is not equal to the right-hand instance; otherwise, <see langword="false"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Result? left, Result? right) => !Equals(left, right);

    /// <inheritdoc/>
    public bool Equals(Result? other)
    {
        if (ReferenceEquals(this, other)) return true;

        if (this is Failure thisFailure && other is Failure otherFailure)
            return thisFailure.Exception == otherFailure.Exception;

        return this is Success && other is Success;
    }

    /// <inheritdoc/>
    public sealed override bool Equals(object? obj) => Equals(obj as Result);

    /// <inheritdoc/>
    public sealed override int GetHashCode() => this is Failure failure ? failure.Exception.GetHashCode() : 0;

    /// <summary>
    /// Gets the underlying exception if the current <see cref="Result"/> is in a <see cref="Failure"/> state,
    /// or <see langword="null"/> if the current <see cref="Result"/> is in a <see cref="Success"/> state.
    /// </summary>
    /// <returns>
    /// Returns the underlying exception if the current <see cref="Result"/> is in a <see cref="Failure"/> state,
    /// or <see langword="null"/> if the current <see cref="Result"/> is in a <see cref="Success"/> state.
    /// </returns>
    public Exception? GetExceptionOrDefault() => this is Failure failure ? failure.Exception : null;

    /// <summary>
    /// Gets the underlying exception if the current <see cref="Result"/> is in a <see cref="Failure"/> state,
    /// or the specified default exception if the current <see cref="Result"/> is in a <see cref="Success"/> state.
    /// </summary>
    /// <param name="defaultValue">The default exception to return in the event that the current <see cref="Result"/> is in a <see cref="Success"/> state.</param>
    /// <returns>
    /// Returns the underlying exception if the current <see cref="Result"/> is in a <see cref="Failure"/> state,
    /// or the specified default exception if the current <see cref="Result"/> is in a <see cref="Success"/> state.
    /// </returns>
    public Exception GetExceptionOrDefault(Exception defaultValue) => this is Failure failure ? failure.Exception : defaultValue;

    /// <summary>
    /// Gets the underlying exception if the current <see cref="Result"/> is in a <see cref="Failure"/> state,
    /// or <see cref="None{T}"/> if the current <see cref="Result"/> is in a <see cref="Success"/> state.
    /// </summary>
    /// <returns>
    /// Returns the underlying exception if the current <see cref="Result"/> is in a <see cref="Failure"/> state,
    /// or <see cref="None{T}"/> if the current <see cref="Result"/> is in a <see cref="Success"/> state.
    /// </returns>
    public Optional<Exception> GetExceptionOrNone() => GetExceptionOrDefault();

    /// <summary>
    /// Gets the underlying exception if the current <see cref="Result"/> is in a <see cref="Failure"/> state,
    /// or throws <see cref="InvalidOperationException"/> if the current <see cref="Result"/> is in a <see cref="Success"/> state.
    /// </summary>
    /// <returns>
    /// Returns the underlying exception if the current <see cref="Result"/> is in a <see cref="Failure"/> state,
    /// or throws <see cref="InvalidOperationException"/> if the current <see cref="Result"/> is in a <see cref="Success"/> state.
    /// </returns>
    /// <exception cref="InvalidOperationException">If the current <see cref="Result"/> is in a <see cref="Success"/> state.</exception>
    public Exception GetExceptionOrThrow() => this is Failure failure ? failure.Exception : throw new InvalidOperationException("The current result is not in a failure state.");

    /// <summary>
    /// Executes the action that matches the value of the current <see cref="Result"/> instance.
    /// </summary>
    /// <param name="success">The delegate to execute when the current <see cref="Result"/> instance is in a successful state.</param>
    /// <param name="failure">The delegate to execute when the current <see cref="Result"/> instance is in a failed state.</param>
    public void Match(Action? success = null, Action<Exception>? failure = null)
    {
        switch (this)
        {
            case Failure failureResult:
                failure?.Invoke(failureResult.Exception);
                break;
            default:
                success?.Invoke();
                break;
        }
    }

    /// <summary>
    /// Asynchronously executes the action that matches the value of the current <see cref="Result"/> instance.
    /// </summary>
    /// <param name="success">The delegate to execute when the current <see cref="Result"/> instance is in a successful state.</param>
    /// <param name="failure">The delegate to execute when the current <see cref="Result"/> instance is in a failed state.</param>
    /// <param name="token">The cancellation token that can be used to cancel long-running tasks.</param>
    public async Task MatchAsync(Func<Task>? success = null, Action<Exception>? failure = null, CancellationToken token = default)
    {
        switch (this)
        {
            case Failure failureResult:
                failure?.Invoke(failureResult.Exception);
                break;
            default:
                if (success is not null)
                    await success().WaitAsync(token).ConfigureAwait(false);
                break;
        }
    }

    /// <summary>
    /// Asynchronously executes the action that matches the value of the current <see cref="Result"/> instance.
    /// </summary>
    /// <param name="success">The delegate to execute when the current <see cref="Result"/> instance is in a successful state.</param>
    /// <param name="failure">The delegate to execute when the current <see cref="Result"/> instance is in a failed state.</param>
    /// <param name="token">The cancellation token that can be used to cancel long-running tasks.</param>
    public async Task MatchAsync(Func<CancellationToken, Task>? success = null, Action<Exception>? failure = null, CancellationToken token = default)
    {
        switch (this)
        {
            case Failure failureResult:
                failure?.Invoke(failureResult.Exception);
                break;
            default:
                if (success is not null)
                    await success(token).ConfigureAwait(false);
                break;
        }
    }

    /// <summary>
    /// Asynchronously executes the action that matches the value of the current <see cref="Result"/> instance.
    /// </summary>
    /// <param name="success">The delegate to execute when the current <see cref="Result"/> instance is in a successful state.</param>
    /// <param name="failure">The delegate to execute when the current <see cref="Result"/> instance is in a failed state.</param>
    /// <param name="token">The cancellation token that can be used to cancel long-running tasks.</param>
    public async Task MatchAsync(Action? success = null, Func<Exception, Task>? failure = null, CancellationToken token = default)
    {
        switch (this)
        {
            case Failure failureResult:
                if (failure is not null)
                    await failure(failureResult.Exception).WaitAsync(token).ConfigureAwait(false);
                break;
            default:
                success?.Invoke();
                break;
        }
    }

    /// <summary>
    /// Asynchronously executes the action that matches the value of the current <see cref="Result"/> instance.
    /// </summary>
    /// <param name="success">The delegate to execute when the current <see cref="Result"/> instance is in a successful state.</param>
    /// <param name="failure">The delegate to execute when the current <see cref="Result"/> instance is in a failed state.</param>
    /// <param name="token">The cancellation token that can be used to cancel long-running tasks.</param>
    public async Task MatchAsync(Action? success = null, Func<Exception, CancellationToken, Task>? failure = null, CancellationToken token = default)
    {
        switch (this)
        {
            case Failure failureResult:
                if (failure is not null)
                    await failure(failureResult.Exception, token).ConfigureAwait(false);
                break;
            default:
                success?.Invoke();
                break;
        }
    }

    /// <summary>
    /// Asynchronously executes the action that matches the value of the current <see cref="Result"/> instance.
    /// </summary>
    /// <param name="success">The delegate to execute when the current <see cref="Result"/> instance is in a successful state.</param>
    /// <param name="failure">The delegate to execute when the current <see cref="Result"/> instance is in a failed state.</param>
    /// <param name="token">The cancellation token that can be used to cancel long-running tasks.</param>
    public async Task MatchAsync(Func<Task>? success = null, Func<Exception, Task>? failure = null, CancellationToken token = default)
    {
        switch (this)
        {
            case Failure failureResult:
                if (failure is not null)
                    await failure(failureResult.Exception).WaitAsync(token).ConfigureAwait(false);
                break;
            default:
                if (success is not null)
                    await success().WaitAsync(token).ConfigureAwait(false);
                break;
        }
    }

    /// <summary>
    /// Asynchronously executes the action that matches the value of the current <see cref="Result"/> instance.
    /// </summary>
    /// <param name="success">The delegate to execute when the current <see cref="Result"/> instance is in a successful state.</param>
    /// <param name="failure">The delegate to execute when the current <see cref="Result"/> instance is in a failed state.</param>
    /// <param name="token">The cancellation token that can be used to cancel long-running tasks.</param>
    public async Task MatchAsync(Func<CancellationToken, Task>? success = null, Func<Exception, CancellationToken, Task>? failure = null, CancellationToken token = default)
    {
        switch (this)
        {
            case Failure failureResult:
                if (failure is not null)
                    await failure(failureResult.Exception, token).ConfigureAwait(false);
                break;
            default:
                if (success is not null)
                    await success(token).ConfigureAwait(false);
                break;
        }
    }

    /// <summary>
    /// Executes the delegate that matches the value of the current <see cref="Result"/> instance and returns its result.
    /// </summary>
    /// <param name="success">The delegate to execute when the current <see cref="Result"/> instance is in a successful state.</param>
    /// <param name="failure">The delegate to execute when the current <see cref="Result"/> instance is in a failed state.</param>
    /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
    /// <returns>
    /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="Result"/> instance is in a successful state;
    /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="Result"/> instance is in a failed state.
    /// </returns>
    public TResult Match<TResult>(Func<TResult> success, Func<Exception, TResult> failure) =>
        this is Failure failureResult ? failure(failureResult.Exception) : success();

    /// <summary>
    /// Asynchronously executes the delegate that matches the value of the current <see cref="Result"/> instance and returns its result.
    /// </summary>
    /// <param name="success">The delegate to execute when the current <see cref="Result"/> instance is in a successful state.</param>
    /// <param name="failure">The delegate to execute when the current <see cref="Result"/> instance is in a failed state.</param>
    /// <param name="token">The cancellation token that can be used to cancel long-running tasks.</param>
    /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
    /// <returns>
    /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="Result"/> instance is in a successful state;
    /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="Result"/> instance is in a failed state.
    /// </returns>
    public async Task<TResult> MatchAsync<TResult>(Func<Task<TResult>> success, Func<Exception, TResult> failure, CancellationToken token = default) =>
        this is Failure failureResult ? failure(failureResult.Exception) : await success().WaitAsync(token).ConfigureAwait(false);

    /// <summary>
    /// Asynchronously executes the delegate that matches the value of the current <see cref="Result"/> instance and returns its result.
    /// </summary>
    /// <param name="success">The delegate to execute when the current <see cref="Result"/> instance is in a successful state.</param>
    /// <param name="failure">The delegate to execute when the current <see cref="Result"/> instance is in a failed state.</param>
    /// <param name="token">The cancellation token that can be used to cancel long-running tasks.</param>
    /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
    /// <returns>
    /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="Result"/> instance is in a successful state;
    /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="Result"/> instance is in a failed state.
    /// </returns>
    public async Task<TResult> MatchAsync<TResult>(Func<CancellationToken, Task<TResult>> success, Func<Exception, TResult> failure, CancellationToken token = default) =>
        this is Failure failureResult ? failure(failureResult.Exception) : await success(token).ConfigureAwait(false);

    /// <summary>
    /// Asynchronously executes the delegate that matches the value of the current <see cref="Result"/> instance and returns its result.
    /// </summary>
    /// <param name="success">The delegate to execute when the current <see cref="Result"/> instance is in a successful state.</param>
    /// <param name="failure">The delegate to execute when the current <see cref="Result"/> instance is in a failed state.</param>
    /// <param name="token">The cancellation token that can be used to cancel long-running tasks.</param>
    /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
    /// <returns>
    /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="Result"/> instance is in a successful state;
    /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="Result"/> instance is in a failed state.
    /// </returns>
    public async Task<TResult> MatchAsync<TResult>(Func<TResult> success, Func<Exception, Task<TResult>> failure, CancellationToken token = default) =>
        this is Failure failureResult ? await failure(failureResult.Exception).WaitAsync(token).ConfigureAwait(false) : success();

    /// <summary>
    /// Asynchronously executes the delegate that matches the value of the current <see cref="Result"/> instance and returns its result.
    /// </summary>
    /// <param name="success">The delegate to execute when the current <see cref="Result"/> instance is in a successful state.</param>
    /// <param name="failure">The delegate to execute when the current <see cref="Result"/> instance is in a failed state.</param>
    /// <param name="token">The cancellation token that can be used to cancel long-running tasks.</param>
    /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
    /// <returns>
    /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="Result"/> instance is in a successful state;
    /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="Result"/> instance is in a failed state.
    /// </returns>
    public async Task<TResult> MatchAsync<TResult>(Func<TResult> success, Func<Exception, CancellationToken, Task<TResult>> failure, CancellationToken token = default) =>
        this is Failure failureResult ? await failure(failureResult.Exception, token).ConfigureAwait(false) : success();

    /// <summary>
    /// Asynchronously executes the delegate that matches the value of the current <see cref="Result"/> instance and returns its result.
    /// </summary>
    /// <param name="success">The delegate to execute when the current <see cref="Result"/> instance is in a successful state.</param>
    /// <param name="failure">The delegate to execute when the current <see cref="Result"/> instance is in a failed state.</param>
    /// <param name="token">The cancellation token that can be used to cancel long-running tasks.</param>
    /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
    /// <returns>
    /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="Result"/> instance is in a successful state;
    /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="Result"/> instance is in a failed state.
    /// </returns>
    public async Task<TResult> MatchAsync<TResult>(Func<Task<TResult>> success, Func<Exception, Task<TResult>> failure, CancellationToken token = default) =>
        this is Failure failureResult ? await failure(failureResult.Exception).WaitAsync(token).ConfigureAwait(false) : await success().WaitAsync(token).ConfigureAwait(false);

    /// <summary>
    /// Asynchronously executes the delegate that matches the value of the current <see cref="Result"/> instance and returns its result.
    /// </summary>
    /// <param name="success">The delegate to execute when the current <see cref="Result"/> instance is in a successful state.</param>
    /// <param name="failure">The delegate to execute when the current <see cref="Result"/> instance is in a failed state.</param>
    /// <param name="token">The cancellation token that can be used to cancel long-running tasks.</param>
    /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
    /// <returns>
    /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="Result"/> instance is in a successful state;
    /// otherwise, returns the result of the <paramref name="failure"/> delegate if the current <see cref="Result"/> instance is in a failed state.
    /// </returns>
    public async Task<TResult> MatchAsync<TResult>(Func<CancellationToken, Task<TResult>> success, Func<Exception, CancellationToken, Task<TResult>> failure, CancellationToken token = default) =>
        this is Failure failureResult ? await failure(failureResult.Exception, token).ConfigureAwait(false) : await success(token).ConfigureAwait(false);

    /// <summary>
    /// Applies the provided selector delegate to the value of the current <see cref="Result"/> instance.
    /// </summary>
    /// <param name="selector">The delegate to apply to current <see cref="Result"/> instance.</param>
    /// <returns>
    /// Returns <see cref="Success"/> if the current <see cref="Result"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure"/>.
    /// </returns>
    public Result Select(Action selector)
    {
        if (this is Failure) return this;

        try
        {
            selector();
            return Success();
        }
        catch (Exception exception)
        {
            return exception;
        }
    }

    /// <summary>
    /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result"/> instance.
    /// </summary>
    /// <param name="selector">The delegate to apply to current <see cref="Result"/> instance.</param>
    /// <param name="token">The cancellation token that can be used to cancel long-running tasks.</param>
    /// <returns>
    /// Returns <see cref="Success"/> if the current <see cref="Result"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure"/>.
    /// </returns>
    public async Task<Result> SelectAsync(Func<Task> selector, CancellationToken token = default)
    {
        if (this is Failure) return this;

        try
        {
            await selector().WaitAsync(token).ConfigureAwait(false);
            return Success();
        }
        catch (Exception exception)
        {
            return exception;
        }
    }

    /// <summary>
    /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result"/> instance.
    /// </summary>
    /// <param name="selector">The delegate to apply to current <see cref="Result"/> instance.</param>
    /// <param name="token">The cancellation token that can be used to cancel long-running tasks.</param>
    /// <returns>
    /// Returns <see cref="Success"/> if the current <see cref="Result"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure"/>.
    /// </returns>
    public async Task<Result> SelectAsync(Func<CancellationToken, Task> selector, CancellationToken token = default)
    {
        if (this is Failure) return this;

        try
        {
            await selector(token).ConfigureAwait(false);
            return Success();
        }
        catch (Exception exception)
        {
            return exception;
        }
    }

    /// <summary>
    /// Applies the provided selector delegate to the value of the current <see cref="Result"/> instance.
    /// </summary>
    /// <param name="selector">The delegate to apply to the current <see cref="Result"/> instance.</param>
    /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
    /// <returns>
    /// Returns <see cref="Success{T}"/> if the current <see cref="Result"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure{T}"/>.
    /// </returns>
    public Result<TResult> Select<TResult>(Func<TResult> selector)
    {
        if (this is Failure failure) return failure.ToTypedResult<TResult>();

        try
        {
            return selector();
        }
        catch (Exception exception)
        {
            return exception;
        }
    }

    /// <summary>
    /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result"/> instance.
    /// </summary>
    /// <param name="selector">The delegate to apply to the current <see cref="Result"/> instance.</param>
    /// <param name="token">The cancellation token that can be used to cancel long-running tasks.</param>
    /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
    /// <returns>
    /// Returns <see cref="Success{T}"/> if the current <see cref="Result"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure{T}"/>.
    /// </returns>
    public async Task<Result<TResult>> SelectAsync<TResult>(Func<Task<TResult>> selector, CancellationToken token = default)
    {
        if (this is Failure failure) return failure.ToTypedResult<TResult>();

        try
        {
            return await selector().WaitAsync(token).ConfigureAwait(false);
        }
        catch (Exception exception)
        {
            return exception;
        }
    }

    /// <summary>
    /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result"/> instance.
    /// </summary>
    /// <param name="selector">The delegate to apply to the current <see cref="Result"/> instance.</param>
    /// <param name="token">The cancellation token that can be used to cancel long-running tasks.</param>
    /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
    /// <returns>
    /// Returns <see cref="Success{T}"/> if the current <see cref="Result"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure{T}"/>.
    /// </returns>
    public async Task<Result<TResult>> SelectAsync<TResult>(Func<CancellationToken, Task<TResult>> selector, CancellationToken token = default)
    {
        if (this is Failure failure) return failure.ToTypedResult<TResult>();

        try
        {
            return await selector(token).ConfigureAwait(false);
        }
        catch (Exception exception)
        {
            return exception;
        }
    }

    /// <summary>
    /// Applies the provided selector delegate to the value of the current <see cref="Result"/> instance.
    /// </summary>
    /// <param name="selector">The delegate to apply to the current <see cref="Result"/> instance.</param>
    /// <returns>
    /// Returns <see cref="Success"/> if the current <see cref="Result"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure"/>.
    /// </returns>
    public Result SelectMany(Func<Result> selector)
    {
        if (this is Failure) return this;

        try
        {
            return selector();
        }
        catch (Exception exception)
        {
            return exception;
        }
    }

    /// <summary>
    /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result"/> instance.
    /// </summary>
    /// <param name="selector">The delegate to apply to the current <see cref="Result"/> instance.</param>
    /// <param name="token">The cancellation token that can be used to cancel long-running tasks.</param>
    /// <returns>
    /// Returns <see cref="Success"/> if the current <see cref="Result"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure"/>.
    /// </returns>
    public async Task<Result> SelectManyAsync(Func<Task<Result>> selector, CancellationToken token = default)
    {
        if (this is Failure) return this;

        try
        {
            return await selector().WaitAsync(token).ConfigureAwait(false);
        }
        catch (Exception exception)
        {
            return exception;
        }
    }

    /// <summary>
    /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result"/> instance.
    /// </summary>
    /// <param name="selector">The delegate to apply to the current <see cref="Result"/> instance.</param>
    /// <param name="token">The cancellation token that can be used to cancel long-running tasks.</param>
    /// <returns>
    /// Returns <see cref="Success"/> if the current <see cref="Result"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure"/>.
    /// </returns>
    public async Task<Result> SelectManyAsync(Func<CancellationToken, Task<Result>> selector, CancellationToken token = default)
    {
        if (this is Failure) return this;

        try
        {
            return await selector(token).ConfigureAwait(false);
        }
        catch (Exception exception)
        {
            return exception;
        }
    }

    /// <summary>
    /// Applies the provided selector delegate to the value of the current <see cref="Result"/> instance.
    /// </summary>
    /// <param name="selector">The delegate to apply to the current <see cref="Result"/> instance.</param>
    /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
    /// <returns>
    /// Returns <see cref="Success{T}"/> if the current <see cref="Result"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure{T}"/>.
    /// </returns>
    public Result<TResult> SelectMany<TResult>(Func<Result<TResult>> selector)
    {
        if (this is Failure failure) return failure.ToTypedResult<TResult>();

        try
        {
            return selector();
        }
        catch (Exception exception)
        {
            return exception;
        }
    }

    /// <summary>
    /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result"/> instance.
    /// </summary>
    /// <param name="selector">The delegate to apply to the current <see cref="Result"/> instance.</param>
    /// <param name="token">The cancellation token that can be used to cancel long-running tasks.</param>
    /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
    /// <returns>
    /// Returns <see cref="Success{T}"/> if the current <see cref="Result"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure{T}"/>.
    /// </returns>
    public async Task<Result<TResult>> SelectManyAsync<TResult>(Func<Task<Result<TResult>>> selector, CancellationToken token = default)
    {
        if (this is Failure failure) return failure.ToTypedResult<TResult>();

        try
        {
            return await selector().WaitAsync(token).ConfigureAwait(false);
        }
        catch (Exception exception)
        {
            return exception;
        }
    }

    /// <summary>
    /// Asynchronously applies the provided selector delegate to the value of the current <see cref="Result"/> instance.
    /// </summary>
    /// <param name="selector">The delegate to apply to the current <see cref="Result"/> instance.</param>
    /// <param name="token">The cancellation token that can be used to cancel long-running tasks.</param>
    /// <typeparam name="TResult">The underlying type of the result produced by the selector delegate.</typeparam>
    /// <returns>
    /// Returns <see cref="Success{T}"/> if the current <see cref="Result"/> is in a successful state, and the delegate invocation is also successful; otherwise; <see cref="Failure{T}"/>.
    /// </returns>
    public async Task<Result<TResult>> SelectManyAsync<TResult>(Func<CancellationToken, Task<Result<TResult>>> selector, CancellationToken token = default)
    {
        if (this is Failure failure) return failure.ToTypedResult<TResult>();

        try
        {
            return await selector(token).ConfigureAwait(false);
        }
        catch (Exception exception)
        {
            return exception;
        }
    }

    /// <summary>
    /// Throws the underlying exception if the current <see cref="Result"/> is in a failure state.
    /// <remarks>Throwing the underlying exception from a location where it was not generated will yield an incorrect stack trace.</remarks>
    /// </summary>
    public void Throw()
    {
        if (this is Failure failure)
            throw failure.Exception;
    }

    /// <inheritdoc/>
    public sealed override string ToString() => this is Failure failure ? failure.Exception.Message : string.Empty;
}

/// <summary>
/// Represents a successful result.
/// </summary>
public sealed class Success : Result
{
    /// <summary>
    /// Gets the singleton <see cref="Success"/> instance.
    /// </summary>
    // ReSharper disable once HeapView.ObjectAllocation.Evident
    internal static readonly Success Instance = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="Success"/> class.
    /// </summary>
    private Success()
    {
    }

    /// <summary>
    /// Obtains a new <see cref="Success{T}"/> instance containing the current exception.
    /// </summary>
    /// <typeparam name="TResult">The underlying type of the <see cref="Success{T}"/> to return.</typeparam>
    /// <returns>Returns a new <see cref="Success{T}"/> instance containing the current exception.</returns>
    // ReSharper disable once MemberCanBeMadeStatic.Global
#pragma warning disable CA1822
    public Success<TResult> ToTypedResult<TResult>(TResult value) => Result<TResult>.Success(value);
#pragma warning restore CA1822
}

/// <summary>
/// Represents a failed result.
/// </summary>
public sealed class Failure : Result
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Failure"/> class.
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
}
