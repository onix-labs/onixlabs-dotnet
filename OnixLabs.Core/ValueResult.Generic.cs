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
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;

namespace OnixLabs.Core;

/// <summary>
/// Represents a result value that does not allocate, which signifies the presence of a value or an exception.
/// </summary>
/// <typeparam name="T">The type of the underlying result value.</typeparam>
/// <remarks>
/// <para>
/// <see cref="ValueResult{T}"/> is the value-type counterpart of <see cref="Result{T}"/>. It holds the value of a successful result, or a reference
/// to the exception of a failed one, so creating, copying and matching a <see cref="ValueResult{T}"/> never allocates.
/// </para>
/// <para>
/// The default value is a failed result, whose exception reports that the result was never initialised, so that an uninitialised field or array
/// element surfaces as an error rather than as a successful <see langword="default"/> value.
/// </para>
/// <para>
/// With C# 15, <see cref="ValueResult{T}"/> is a union whose case types are <typeparamref name="T"/> and <see cref="Exception"/>:
/// <code>
/// string text = result switch
/// {
///     int value => $"Value: {value}",
///     Exception exception => exception.Message
/// };
/// </code>
/// When <typeparamref name="T"/> is nullable, a successful result can hold <see langword="null"/>, which matches a <see langword="null"/> pattern,
/// and the compiler requires a <see langword="null"/> arm.
/// </para>
/// </remarks>
[Union]
public readonly struct ValueResult<T> : IValueEquatable<ValueResult<T>>, ValueResult<T>.IUnionMembers
{
    // ReSharper disable once HeapView.ObjectAllocation.Evident
    private static readonly InvalidOperationException UninitialisedException = new("The result was never initialised; it is the default value of its type.");

    private readonly T value;
    private readonly Exception? exception;
    private readonly bool isSuccess;

    /// <summary>
    /// Initializes a new instance of the <see cref="ValueResult{T}"/> struct.
    /// </summary>
    /// <param name="value">The underlying value of a successful result.</param>
    /// <param name="exception">The underlying exception of a failed result, or <see langword="null"/> for a successful result.</param>
    /// <param name="isSuccess">A value indicating whether the result is successful.</param>
    private ValueResult(T value, Exception? exception, bool isSuccess)
    {
        this.value = value;
        this.exception = exception;
        this.isSuccess = isSuccess;
    }

    /// <summary>
    /// Gets a value indicating whether the current <see cref="ValueResult{T}"/> is in a successful state.
    /// </summary>
    /// <value><see langword="true"/> if the current <see cref="ValueResult{T}"/> is in a successful state; otherwise, <see langword="false"/>.</value>
    public bool IsSuccess => isSuccess;

    /// <summary>
    /// Gets a value indicating whether the current <see cref="ValueResult{T}"/> is in a failed state.
    /// </summary>
    /// <value><see langword="true"/> if the current <see cref="ValueResult{T}"/> is in a failed state; otherwise, <see langword="false"/>.</value>
    public bool IsFailure => !isSuccess;

    /// <summary>
    /// Gets the exception of a failed result, including the uninitialised exception of the default value.
    /// </summary>
    /// <remarks>Only meaningful when the current <see cref="ValueResult{T}"/> is in a failed state.</remarks>
    private Exception FailureException => exception ?? UninitialisedException;

    /// <summary>
    /// Gets the exception of a failed result, or <see langword="null"/> for a successful result.
    /// </summary>
    private Exception? ExceptionOrDefault => isSuccess ? null : FailureException;

    /// <summary>
    /// Creates a new <see cref="ValueResult{T}"/>, which holds the value returned by the specified function;
    /// otherwise, a failed result containing the exception thrown by the specified function.
    /// </summary>
    /// <param name="func">The function to invoke to obtain a successful or failed result.</param>
    /// <returns>
    /// Returns a successful <see cref="ValueResult{T}"/> holding the value returned by the specified function;
    /// otherwise, a failed <see cref="ValueResult{T}"/> containing the exception thrown by the specified function.
    /// </returns>
    public static ValueResult<T> Of(Func<T> func)
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
    /// Creates a new <see cref="ValueResult{T}"/> that represents a successful result.
    /// </summary>
    /// <param name="value">The underlying value of the successful result.</param>
    /// <returns>Returns a new <see cref="ValueResult{T}"/> that represents a successful result.</returns>
    public static ValueResult<T> Success(T value) => new(value, null, true);

    /// <summary>
    /// Creates a new <see cref="ValueResult{T}"/> that represents a failed result.
    /// </summary>
    /// <param name="exception">The underlying exception of the failed result.</param>
    /// <returns>Returns a new <see cref="ValueResult{T}"/> that represents a failed result.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="exception"/> is <see langword="null"/>.</exception>
    public static ValueResult<T> Failure(Exception exception) => new(default!, RequireNotNull(exception, "Exception must not be null."), false);

    /// <summary>
    /// Creates a new <see cref="ValueResult{T}"/> from the specified <see cref="Result{T}"/>.
    /// </summary>
    /// <param name="result">The <see cref="Result{T}"/> from which to create a new <see cref="ValueResult{T}"/>.</param>
    /// <returns>Returns a new <see cref="ValueResult{T}"/> in the same state as the specified <see cref="Result{T}"/>.</returns>
    public static ValueResult<T> From(Result<T> result) => RequireNotNull(result, "Result must not be null.") switch
    {
        Success<T> success => Success(success.Value),
        Failure<T> failure => Failure(failure.Exception)
    };

    /// <summary>
    /// Creates a new <see cref="ValueResult{T}"/> that represents a successful result.
    /// </summary>
    /// <param name="value">The underlying value of the successful result.</param>
    /// <returns>Returns a new <see cref="ValueResult{T}"/> that represents a successful result.</returns>
    // Why: C# 15 would convert a value through the union's Create members anyway, but earlier compilers have no union conversions.
    public static implicit operator ValueResult<T>(T value) => Success(value);

    /// <summary>
    /// Creates a new <see cref="ValueResult{T}"/> that represents a failed result.
    /// </summary>
    /// <param name="exception">The underlying exception of the failed result.</param>
    /// <returns>Returns a new <see cref="ValueResult{T}"/> that represents a failed result.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="exception"/> is <see langword="null"/>.</exception>
    public static implicit operator ValueResult<T>(Exception exception) => Failure(exception);

    /// <summary>
    /// Gets the underlying value of the specified <see cref="ValueResult{T}"/>, or throws its exception if it is in a failed state.
    /// </summary>
    /// <param name="value">The <see cref="ValueResult{T}"/> from which to obtain the underlying value.</param>
    /// <returns>Returns the underlying value of the specified <see cref="ValueResult{T}"/>.</returns>
    public static explicit operator T(ValueResult<T> value) => value.GetValueOrThrow();

    /// <inheritdoc/>
    public static bool operator ==(ValueResult<T> left, ValueResult<T> right) => left.Equals(right);

    /// <inheritdoc/>
    public static bool operator !=(ValueResult<T> left, ValueResult<T> right) => !left.Equals(right);

    /// <inheritdoc/>
    public bool Equals(ValueResult<T> other) => ValueResultEqualityComparer<T>.Default.Equals(this, other);

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is ValueResult<T> other && Equals(other);

    /// <inheritdoc/>
    // ReSharper disable once HeapView.PossibleBoxingAllocation
    public override int GetHashCode() => isSuccess ? value?.GetHashCode() ?? 0 : FailureException.GetHashCode();

    /// <summary>
    /// Gets the underlying exception if the current <see cref="ValueResult{T}"/> is in a failed state,
    /// or <see langword="null"/> if the current <see cref="ValueResult{T}"/> is in a successful state.
    /// </summary>
    /// <returns>
    /// Returns the underlying exception if the current <see cref="ValueResult{T}"/> is in a failed state,
    /// or <see langword="null"/> if the current <see cref="ValueResult{T}"/> is in a successful state.
    /// </returns>
    public Exception? GetExceptionOrDefault() => ExceptionOrDefault;

    /// <summary>
    /// Gets the underlying exception if the current <see cref="ValueResult{T}"/> is in a failed state,
    /// or the specified default exception if the current <see cref="ValueResult{T}"/> is in a successful state.
    /// </summary>
    /// <param name="defaultException">The default exception to return in the event that the current <see cref="ValueResult{T}"/> is in a successful state.</param>
    /// <returns>
    /// Returns the underlying exception if the current <see cref="ValueResult{T}"/> is in a failed state,
    /// or the specified default exception if the current <see cref="ValueResult{T}"/> is in a successful state.
    /// </returns>
    public Exception GetExceptionOrDefault(Exception defaultException) => ExceptionOrDefault ?? defaultException;

    /// <summary>
    /// Gets the underlying exception if the current <see cref="ValueResult{T}"/> is in a failed state,
    /// or <see cref="ValueOptional{T}.None"/> if the current <see cref="ValueResult{T}"/> is in a successful state.
    /// </summary>
    /// <returns>
    /// Returns the underlying exception if the current <see cref="ValueResult{T}"/> is in a failed state,
    /// or <see cref="ValueOptional{T}.None"/> if the current <see cref="ValueResult{T}"/> is in a successful state.
    /// </returns>
    public ValueOptional<Exception> GetExceptionOrNone() => ExceptionOrDefault;

    /// <summary>
    /// Gets the underlying exception if the current <see cref="ValueResult{T}"/> is in a failed state,
    /// or throws <see cref="InvalidOperationException"/> if the current <see cref="ValueResult{T}"/> is in a successful state.
    /// </summary>
    /// <returns>Returns the underlying exception if the current <see cref="ValueResult{T}"/> is in a failed state.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the current <see cref="ValueResult{T}"/> is in a successful state.</exception>
    public Exception GetExceptionOrThrow() => CheckNotNull(ExceptionOrDefault, "The current result is in a success state; no exception to retrieve.");

    /// <summary>
    /// Gets the underlying value if the current <see cref="ValueResult{T}"/> is in a successful state,
    /// or the default value of <typeparamref name="T"/> if the current <see cref="ValueResult{T}"/> is in a failed state.
    /// </summary>
    /// <returns>
    /// Returns the underlying value if the current <see cref="ValueResult{T}"/> is in a successful state,
    /// or the default value of <typeparamref name="T"/> if the current <see cref="ValueResult{T}"/> is in a failed state.
    /// </returns>
    public T? GetValueOrDefault() => isSuccess ? value : default;

    /// <summary>
    /// Gets the underlying value if the current <see cref="ValueResult{T}"/> is in a successful state,
    /// or the specified default value if the current <see cref="ValueResult{T}"/> is in a failed state.
    /// </summary>
    /// <param name="defaultValue">The default value to return in the event that the current <see cref="ValueResult{T}"/> is in a failed state.</param>
    /// <returns>
    /// Returns the underlying value if the current <see cref="ValueResult{T}"/> is in a successful state,
    /// or the specified default value if the current <see cref="ValueResult{T}"/> is in a failed state.
    /// </returns>
    public T GetValueOrDefault(T defaultValue) => isSuccess ? value : defaultValue;

    /// <summary>
    /// Gets the underlying value if the current <see cref="ValueResult{T}"/> is in a successful state,
    /// or throws the underlying exception if the current <see cref="ValueResult{T}"/> is in a failed state.
    /// </summary>
    /// <returns>Returns the underlying value if the current <see cref="ValueResult{T}"/> is in a successful state.</returns>
    public T GetValueOrThrow()
    {
        Throw();
        return value;
    }

    /// <summary>
    /// Gets the underlying value of the current <see cref="ValueResult{T}"/> as an <see langword="out"/> parameter.
    /// </summary>
    /// <param name="value">
    /// When this method returns, contains the value of the current <see cref="ValueResult{T}"/> if it is in a successful state;
    /// otherwise, the default value of <typeparamref name="T"/>.
    /// </param>
    /// <returns>Returns the current <see cref="ValueResult{T}"/>.</returns>
    public ValueResult<T> GetValue(out T value)
    {
        // Why: matches Result<T>.GetValue, which also reports default(T) for a failure through a non-nullable out parameter.
        value = isSuccess ? this.value : default!;
        return this;
    }

    /// <summary>
    /// Executes the action that matches the state of the current <see cref="ValueResult{T}"/>.
    /// </summary>
    /// <param name="success">The delegate to execute when the current <see cref="ValueResult{T}"/> is in a successful state.</param>
    /// <param name="failure">The delegate to execute when the current <see cref="ValueResult{T}"/> is in a failed state.</param>
    public void Match(Action<T>? success = null, Action<Exception>? failure = null)
    {
        if (isSuccess)
            success?.Invoke(value);
        else
            failure?.Invoke(FailureException);
    }

    /// <summary>
    /// Executes the delegate that matches the state of the current <see cref="ValueResult{T}"/> and returns its result.
    /// </summary>
    /// <param name="success">The delegate to execute when the current <see cref="ValueResult{T}"/> is in a successful state.</param>
    /// <param name="failure">The delegate to execute when the current <see cref="ValueResult{T}"/> is in a failed state.</param>
    /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
    /// <returns>
    /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="ValueResult{T}"/> is in a successful state;
    /// otherwise, returns the result of the <paramref name="failure"/> delegate.
    /// </returns>
    public TResult Match<TResult>(Func<T, TResult> success, Func<Exception, TResult> failure) => isSuccess ? success(value) : failure(FailureException);

    /// <summary>
    /// Applies the provided selector delegate to the value of the current <see cref="ValueResult{T}"/> if it is in a successful state.
    /// </summary>
    /// <param name="selector">The delegate to apply to the value of the current <see cref="ValueResult{T}"/>.</param>
    /// <returns>
    /// Returns a successful <see cref="ValueResult"/> if the current <see cref="ValueResult{T}"/> is in a successful state, and the delegate invocation
    /// is also successful; otherwise, a failed <see cref="ValueResult"/>.
    /// </returns>
    public ValueResult Select(Action<T> selector)
    {
        if (!isSuccess) return ValueResult.Failure(FailureException);

        try
        {
            selector(value);
            return ValueResult.Success();
        }
        catch (Exception exception)
        {
            return ValueResult.Failure(exception);
        }
    }

    /// <summary>
    /// Applies the provided selector delegate to the value of the current <see cref="ValueResult{T}"/> if it is in a successful state.
    /// </summary>
    /// <param name="selector">The delegate to apply to the value of the current <see cref="ValueResult{T}"/>.</param>
    /// <typeparam name="TResult">The underlying type of the value produced by the selector delegate.</typeparam>
    /// <returns>
    /// Returns a successful <see cref="ValueResult{T}"/> holding the value produced by the delegate if the current <see cref="ValueResult{T}"/> is in a
    /// successful state, and the delegate invocation is also successful; otherwise, a failed <see cref="ValueResult{T}"/>.
    /// </returns>
    public ValueResult<TResult> Select<TResult>(Func<T, TResult> selector)
    {
        if (!isSuccess) return ValueResult<TResult>.Failure(FailureException);

        try
        {
            return ValueResult<TResult>.Success(selector(value));
        }
        catch (Exception exception)
        {
            return ValueResult<TResult>.Failure(exception);
        }
    }

    /// <summary>
    /// Applies the provided selector delegate to the value of the current <see cref="ValueResult{T}"/> if it is in a successful state,
    /// and returns the result it produces.
    /// </summary>
    /// <param name="selector">The delegate to apply to the value of the current <see cref="ValueResult{T}"/>.</param>
    /// <returns>
    /// Returns the result produced by the delegate if the current <see cref="ValueResult{T}"/> is in a successful state, and the delegate invocation
    /// is also successful; otherwise, a failed <see cref="ValueResult"/>.
    /// </returns>
    public ValueResult SelectMany(Func<T, ValueResult> selector)
    {
        if (!isSuccess) return ValueResult.Failure(FailureException);

        try
        {
            return selector(value);
        }
        catch (Exception exception)
        {
            return ValueResult.Failure(exception);
        }
    }

    /// <summary>
    /// Applies the provided selector delegate to the value of the current <see cref="ValueResult{T}"/> if it is in a successful state,
    /// and returns the result it produces.
    /// </summary>
    /// <param name="selector">The delegate to apply to the value of the current <see cref="ValueResult{T}"/>.</param>
    /// <typeparam name="TResult">The underlying type of the value of the result produced by the selector delegate.</typeparam>
    /// <returns>
    /// Returns the result produced by the delegate if the current <see cref="ValueResult{T}"/> is in a successful state, and the delegate invocation
    /// is also successful; otherwise, a failed <see cref="ValueResult{T}"/>.
    /// </returns>
    public ValueResult<TResult> SelectMany<TResult>(Func<T, ValueResult<TResult>> selector)
    {
        if (!isSuccess) return ValueResult<TResult>.Failure(FailureException);

        try
        {
            return selector(value);
        }
        catch (Exception exception)
        {
            return ValueResult<TResult>.Failure(exception);
        }
    }

    /// <summary>
    /// Throws the underlying exception if the current <see cref="ValueResult{T}"/> is in a failed state.
    /// </summary>
    /// <remarks>
    /// The original stack trace of the underlying exception is preserved via
    /// <see cref="ExceptionDispatchInfo"/>; the rethrow point is recorded as a continuation.
    /// </remarks>
    public void Throw()
    {
        if (!isSuccess)
            ExceptionDispatchInfo.Capture(FailureException).Throw();
    }

    /// <summary>
    /// Creates a new <see cref="Result{T}"/> in the same state as the current <see cref="ValueResult{T}"/>.
    /// </summary>
    /// <returns>Returns a new <see cref="Result{T}"/> in the same state as the current <see cref="ValueResult{T}"/>.</returns>
    public Result<T> ToResult() => isSuccess ? Result<T>.Success(value) : Result<T>.Failure(FailureException);

    /// <inheritdoc/>
    // ReSharper disable once HeapView.PossibleBoxingAllocation
    public override string ToString() => isSuccess ? value?.ToString() ?? string.Empty : FailureException.Message;

    /// <inheritdoc/>
    // ReSharper disable once HeapView.PossibleBoxingAllocation
    object? IUnionMembers.Value => isSuccess ? value : FailureException;

    /// <inheritdoc/>
    bool IUnionMembers.HasValue => !isSuccess || value is not null;

    /// <inheritdoc/>
    bool IUnionMembers.TryGetValue([MaybeNullWhen(false)] out T value)
    {
        value = this.value;
        return isSuccess && value is not null;
    }

    /// <inheritdoc/>
    bool IUnionMembers.TryGetValue([MaybeNullWhen(false)] out Exception value)
    {
        value = ExceptionOrDefault;
        return value is not null;
    }

    /// <summary>
    /// Defines the members through which the C# 15 compiler treats <see cref="ValueResult{T}"/> as a union.
    /// </summary>
    /// <remarks>
    /// The compiler looks for union members only on this interface, so they are implemented explicitly and stay off the public surface of
    /// <see cref="ValueResult{T}"/>, where <c>Value</c> would otherwise clash with the meaning it has across the result types. It is not intended
    /// to be used directly.
    /// </remarks>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IUnionMembers
    {
        /// <summary>
        /// Gets the underlying value of a successful result, or the underlying exception of a failed result.
        /// </summary>
        /// <value>The underlying value of a successful result, or the underlying exception of a failed result.</value>
        object? Value { get; }

        /// <summary>
        /// Gets a value indicating whether the union holds a non-null value or an exception.
        /// </summary>
        /// <value><see langword="true"/> if the union holds a non-null value or an exception; otherwise, <see langword="false"/>.</value>
        bool HasValue { get; }

        /// <summary>
        /// Creates a new <see cref="ValueResult{T}"/> for the C# 15 union conversion from a value.
        /// </summary>
        /// <param name="value">The underlying value of the successful result.</param>
        /// <returns>Returns a new <see cref="ValueResult{T}"/> that represents a successful result.</returns>
        static ValueResult<T> Create(T value) => Success(value);

        /// <summary>
        /// Creates a new <see cref="ValueResult{T}"/> for the C# 15 union conversion from an exception.
        /// </summary>
        /// <param name="value">The underlying exception of the failed result.</param>
        /// <returns>Returns a new <see cref="ValueResult{T}"/> that represents a failed result.</returns>
        static ValueResult<T> Create(Exception value) => Failure(value);

        /// <summary>
        /// Gets the underlying value if the union holds a non-null value, which is the case for a successful result.
        /// </summary>
        /// <param name="value">When this method returns, contains the underlying value, or the default value of <typeparamref name="T"/>.</param>
        /// <returns>Returns <see langword="true"/> if the union holds a non-null value; otherwise, <see langword="false"/>.</returns>
        bool TryGetValue([MaybeNullWhen(false)] out T value);

        /// <summary>
        /// Gets the underlying exception if the union holds one, which is the case for a failed result.
        /// </summary>
        /// <param name="value">When this method returns, contains the underlying exception, or <see langword="null"/> for a successful result.</param>
        /// <returns>Returns <see langword="true"/> if the union holds an exception; otherwise, <see langword="false"/>.</returns>
        bool TryGetValue([MaybeNullWhen(false)] out Exception value);
    }
}
