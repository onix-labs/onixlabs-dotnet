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
/// Represents a result value that does not allocate, which signifies success or an exception.
/// </summary>
/// <remarks>
/// <para>
/// <see cref="ValueResult"/> is the value-type counterpart of <see cref="Result"/>. It holds only a reference to the exception of a failed
/// result, so creating, copying and matching a <see cref="ValueResult"/> never allocates. The default value is a successful result.
/// </para>
/// <para>
/// With C# 15, <see cref="ValueResult"/> is a union whose only case type is <see cref="Exception"/>: a type pattern matches the exception
/// of a failed result, and <see langword="null"/> matches a successful result.
/// <code>
/// string text = result switch
/// {
///     Exception exception => exception.Message,
///     null => "Success"
/// };
/// </code>
/// </para>
/// </remarks>
[Union]
public readonly struct ValueResult : IValueEquatable<ValueResult>, ValueResult.IUnionMembers
{
    private readonly Exception? exception;

    /// <summary>
    /// Initializes a new instance of the <see cref="ValueResult"/> struct.
    /// </summary>
    /// <param name="exception">The underlying exception of a failed result, or <see langword="null"/> for a successful result.</param>
    private ValueResult(Exception? exception) => this.exception = exception;

    /// <summary>
    /// Gets a value indicating whether the current <see cref="ValueResult"/> is in a successful state.
    /// </summary>
    /// <value><see langword="true"/> if the current <see cref="ValueResult"/> is in a successful state; otherwise, <see langword="false"/>.</value>
    public bool IsSuccess => exception is null;

    /// <summary>
    /// Gets a value indicating whether the current <see cref="ValueResult"/> is in a failed state.
    /// </summary>
    /// <value><see langword="true"/> if the current <see cref="ValueResult"/> is in a failed state; otherwise, <see langword="false"/>.</value>
    public bool IsFailure => exception is not null;

    /// <summary>
    /// Creates a new <see cref="ValueResult"/>, which is successful if the specified action completes;
    /// otherwise, a failed result containing the exception thrown by the specified action.
    /// </summary>
    /// <param name="action">The action to invoke to obtain a successful or failed result.</param>
    /// <returns>
    /// Returns a successful <see cref="ValueResult"/> if the specified action completes;
    /// otherwise, a failed <see cref="ValueResult"/> containing the exception thrown by the specified action.
    /// </returns>
    public static ValueResult Of(Action action)
    {
        try
        {
            action();
            return Success();
        }
        catch (Exception exception)
        {
            return Failure(exception);
        }
    }

    /// <summary>
    /// Creates a new <see cref="ValueResult"/> that represents a successful result.
    /// </summary>
    /// <returns>Returns a new <see cref="ValueResult"/> that represents a successful result.</returns>
    public static ValueResult Success() => default;

    /// <summary>
    /// Creates a new <see cref="ValueResult"/> that represents a failed result.
    /// </summary>
    /// <param name="exception">The underlying exception of the failed result.</param>
    /// <returns>Returns a new <see cref="ValueResult"/> that represents a failed result.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="exception"/> is <see langword="null"/>.</exception>
    public static ValueResult Failure(Exception exception) => new(RequireNotNull(exception, "Exception must not be null."));

    /// <summary>
    /// Creates a new <see cref="ValueResult"/> from the specified <see cref="Result"/>.
    /// </summary>
    /// <param name="result">The <see cref="Result"/> from which to create a new <see cref="ValueResult"/>.</param>
    /// <returns>Returns a new <see cref="ValueResult"/> in the same state as the specified <see cref="Result"/>.</returns>
    public static ValueResult From(Result result) => new(RequireNotNull(result, "Result must not be null.").GetExceptionOrDefault());

    /// <summary>
    /// Creates a new <see cref="ValueResult"/> that represents a failed result.
    /// </summary>
    /// <param name="exception">The underlying exception of the failed result.</param>
    /// <returns>Returns a new <see cref="ValueResult"/> that represents a failed result.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="exception"/> is <see langword="null"/>.</exception>
    // Why: C# 15 would convert an exception through the union's Create member anyway, but earlier compilers have no union conversions.
    public static implicit operator ValueResult(Exception exception) => Failure(exception);

    /// <inheritdoc/>
    public static bool operator ==(ValueResult left, ValueResult right) => left.Equals(right);

    /// <inheritdoc/>
    public static bool operator !=(ValueResult left, ValueResult right) => !left.Equals(right);

    /// <inheritdoc/>
    public bool Equals(ValueResult other) => exception == other.exception;

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is ValueResult other && Equals(other);

    /// <inheritdoc/>
    public override int GetHashCode() => exception?.GetHashCode() ?? 0;

    /// <summary>
    /// Gets the underlying exception if the current <see cref="ValueResult"/> is in a failed state,
    /// or <see langword="null"/> if the current <see cref="ValueResult"/> is in a successful state.
    /// </summary>
    /// <returns>
    /// Returns the underlying exception if the current <see cref="ValueResult"/> is in a failed state,
    /// or <see langword="null"/> if the current <see cref="ValueResult"/> is in a successful state.
    /// </returns>
    public Exception? GetExceptionOrDefault() => exception;

    /// <summary>
    /// Gets the underlying exception if the current <see cref="ValueResult"/> is in a failed state,
    /// or the specified default exception if the current <see cref="ValueResult"/> is in a successful state.
    /// </summary>
    /// <param name="defaultException">The default exception to return in the event that the current <see cref="ValueResult"/> is in a successful state.</param>
    /// <returns>
    /// Returns the underlying exception if the current <see cref="ValueResult"/> is in a failed state,
    /// or the specified default exception if the current <see cref="ValueResult"/> is in a successful state.
    /// </returns>
    public Exception GetExceptionOrDefault(Exception defaultException) => exception ?? defaultException;

    /// <summary>
    /// Gets the underlying exception if the current <see cref="ValueResult"/> is in a failed state,
    /// or <see cref="ValueOptional{T}.None"/> if the current <see cref="ValueResult"/> is in a successful state.
    /// </summary>
    /// <returns>
    /// Returns the underlying exception if the current <see cref="ValueResult"/> is in a failed state,
    /// or <see cref="ValueOptional{T}.None"/> if the current <see cref="ValueResult"/> is in a successful state.
    /// </returns>
    public ValueOptional<Exception> GetExceptionOrNone() => exception;

    /// <summary>
    /// Gets the underlying exception if the current <see cref="ValueResult"/> is in a failed state,
    /// or throws <see cref="InvalidOperationException"/> if the current <see cref="ValueResult"/> is in a successful state.
    /// </summary>
    /// <returns>Returns the underlying exception if the current <see cref="ValueResult"/> is in a failed state.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the current <see cref="ValueResult"/> is in a successful state.</exception>
    public Exception GetExceptionOrThrow() => CheckNotNull(exception, "The current result is in a success state; no exception to retrieve.");

    /// <summary>
    /// Executes the action that matches the state of the current <see cref="ValueResult"/>.
    /// </summary>
    /// <param name="success">The delegate to execute when the current <see cref="ValueResult"/> is in a successful state.</param>
    /// <param name="failure">The delegate to execute when the current <see cref="ValueResult"/> is in a failed state.</param>
    public void Match(Action? success = null, Action<Exception>? failure = null)
    {
        if (exception is null)
            success?.Invoke();
        else
            failure?.Invoke(exception);
    }

    /// <summary>
    /// Executes the delegate that matches the state of the current <see cref="ValueResult"/> and returns its result.
    /// </summary>
    /// <param name="success">The delegate to execute when the current <see cref="ValueResult"/> is in a successful state.</param>
    /// <param name="failure">The delegate to execute when the current <see cref="ValueResult"/> is in a failed state.</param>
    /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
    /// <returns>
    /// Returns the result of the <paramref name="success"/> delegate if the current <see cref="ValueResult"/> is in a successful state;
    /// otherwise, returns the result of the <paramref name="failure"/> delegate.
    /// </returns>
    public TResult Match<TResult>(Func<TResult> success, Func<Exception, TResult> failure) => exception is null ? success() : failure(exception);

    /// <summary>
    /// Applies the provided selector delegate if the current <see cref="ValueResult"/> is in a successful state.
    /// </summary>
    /// <param name="selector">The delegate to apply to the current <see cref="ValueResult"/>.</param>
    /// <returns>
    /// Returns a successful <see cref="ValueResult"/> if the current <see cref="ValueResult"/> is in a successful state, and the delegate invocation
    /// is also successful; otherwise, a failed <see cref="ValueResult"/>.
    /// </returns>
    public ValueResult Select(Action selector) => IsFailure ? this : Of(selector);

    /// <summary>
    /// Applies the provided selector delegate if the current <see cref="ValueResult"/> is in a successful state, and returns the result it produces.
    /// </summary>
    /// <param name="selector">The delegate to apply to the current <see cref="ValueResult"/>.</param>
    /// <returns>
    /// Returns the result produced by the delegate if the current <see cref="ValueResult"/> is in a successful state, and the delegate invocation
    /// is also successful; otherwise, a failed <see cref="ValueResult"/>.
    /// </returns>
    public ValueResult SelectMany(Func<ValueResult> selector)
    {
        if (IsFailure) return this;

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
    /// Applies the provided selector delegate if the current <see cref="ValueResult"/> is in a successful state.
    /// </summary>
    /// <param name="selector">The delegate to apply to the current <see cref="ValueResult"/>.</param>
    /// <typeparam name="TResult">The underlying type of the value produced by the selector delegate.</typeparam>
    /// <returns>
    /// Returns a successful <see cref="ValueResult{T}"/> holding the value produced by the delegate if the current <see cref="ValueResult"/> is in a
    /// successful state, and the delegate invocation is also successful; otherwise, a failed <see cref="ValueResult{T}"/>.
    /// </returns>
    public ValueResult<TResult> Select<TResult>(Func<TResult> selector) =>
        exception is null ? ValueResult<TResult>.Of(selector) : ValueResult<TResult>.Failure(exception);

    /// <summary>
    /// Applies the provided selector delegate if the current <see cref="ValueResult"/> is in a successful state, and returns the result it produces.
    /// </summary>
    /// <param name="selector">The delegate to apply to the current <see cref="ValueResult"/>.</param>
    /// <typeparam name="TResult">The underlying type of the value of the result produced by the selector delegate.</typeparam>
    /// <returns>
    /// Returns the result produced by the delegate if the current <see cref="ValueResult"/> is in a successful state, and the delegate invocation
    /// is also successful; otherwise, a failed <see cref="ValueResult{T}"/>.
    /// </returns>
    public ValueResult<TResult> SelectMany<TResult>(Func<ValueResult<TResult>> selector)
    {
        if (exception is not null) return ValueResult<TResult>.Failure(exception);

        try
        {
            return selector();
        }
        catch (Exception exception)
        {
            return ValueResult<TResult>.Failure(exception);
        }
    }

    /// <summary>
    /// Throws the underlying exception if the current <see cref="ValueResult"/> is in a failed state.
    /// </summary>
    /// <remarks>
    /// The original stack trace of the underlying exception is preserved via
    /// <see cref="ExceptionDispatchInfo"/>; the rethrow point is recorded as a continuation.
    /// </remarks>
    public void Throw()
    {
        if (exception is not null)
            ExceptionDispatchInfo.Capture(exception).Throw();
    }

    /// <summary>
    /// Creates a new <see cref="Result"/> in the same state as the current <see cref="ValueResult"/>.
    /// </summary>
    /// <returns>Returns a new <see cref="Result"/> in the same state as the current <see cref="ValueResult"/>.</returns>
    public Result ToResult() => exception is null ? Result.Success() : Result.Failure(exception);

    /// <inheritdoc/>
    public override string ToString() => exception?.Message ?? string.Empty;

    /// <inheritdoc/>
    object? IUnionMembers.Value => exception;

    /// <inheritdoc/>
    bool IUnionMembers.HasValue => exception is not null;

    /// <inheritdoc/>
    bool IUnionMembers.TryGetValue([MaybeNullWhen(false)] out Exception value)
    {
        value = exception;
        return value is not null;
    }

    /// <summary>
    /// Defines the members through which the C# 15 compiler treats <see cref="ValueResult"/> as a union.
    /// </summary>
    /// <remarks>
    /// The compiler looks for union members only on this interface, so they are implemented explicitly and stay off the public surface of
    /// <see cref="ValueResult"/>. It is not intended to be used directly.
    /// </remarks>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IUnionMembers
    {
        /// <summary>
        /// Gets the underlying exception of a failed result, or <see langword="null"/> for a successful result.
        /// </summary>
        /// <value>The underlying exception of a failed result, or <see langword="null"/> for a successful result.</value>
        object? Value { get; }

        /// <summary>
        /// Gets a value indicating whether the union holds an exception, which is the case for a failed result.
        /// </summary>
        /// <value><see langword="true"/> if the union holds an exception; otherwise, <see langword="false"/>.</value>
        bool HasValue { get; }

        /// <summary>
        /// Creates a new <see cref="ValueResult"/> for the C# 15 union conversion from an exception.
        /// </summary>
        /// <param name="value">The underlying exception of a failed result, or <see langword="null"/> for a successful result.</param>
        /// <returns>Returns a new <see cref="ValueResult"/>, which is failed if <paramref name="value"/> is not <see langword="null"/>.</returns>
        /// <remarks>
        /// The case type is <see cref="Exception"/>?, not <see cref="Exception"/>, so that the compiler reports CS8655 for a <see langword="switch"/>
        /// that has no <see langword="null"/> arm; with a non-nullable case type, such a switch would compile silently and throw on every success.
        /// </remarks>
        static ValueResult Create(Exception? value) => new(value);

        /// <summary>
        /// Gets the underlying exception if the union holds one, which is the case for a failed result.
        /// </summary>
        /// <param name="value">When this method returns, contains the underlying exception, or <see langword="null"/> for a successful result.</param>
        /// <returns>Returns <see langword="true"/> if the union holds an exception; otherwise, <see langword="false"/>.</returns>
        bool TryGetValue([MaybeNullWhen(false)] out Exception value);
    }
}
