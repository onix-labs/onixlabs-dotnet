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
using System.Threading.Tasks;

namespace OnixLabs.Core;

/// <summary>
/// Represents a failed result.
/// </summary>
public sealed class Failure : Result, IValueEquatable<Failure>
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
    /// Creates a new instance of the <see cref="Result"/> class, where the underlying value represents a failed result.
    /// </summary>
    /// <param name="exception">The underlying failed result exception.</param>
    /// <returns>
    /// Returns a new instance of the <see cref="Result"/> class, where the underlying value represents a failed result.
    /// </returns>
    public static implicit operator Failure(Exception exception) => Failure(exception);

    /// <summary>
    /// Performs an equality comparison between two object instances.
    /// </summary>
    /// <param name="left">The left-hand instance to compare.</param>
    /// <param name="right">The right-hand instance to compare.</param>
    /// <returns>Returns <see langword="true"/> if the left-hand instance is equal to the right-hand instance; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(Failure left, Failure right) => Equals(left, right);

    /// <summary>
    /// Performs an inequality comparison between two object instances.
    /// </summary>
    /// <param name="left">The left-hand instance to compare.</param>
    /// <param name="right">The right-hand instance to compare.</param>
    /// <returns>Returns <see langword="true"/> if the left-hand instance is not equal to the right-hand instance; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(Failure left, Failure right) => !Equals(left, right);

    /// <summary>
    /// Checks whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with the current object.</param>
    /// <returns>Returns <see langword="true"/> if the current object is equal to the other parameter; otherwise, <see langword="false"/>.</returns>
    public bool Equals(Failure? other) => ReferenceEquals(this, other) || other is not null && Equals(other.Exception, Exception);

    /// <summary>
    /// Checks for equality between the current instance and another object.
    /// </summary>
    /// <param name="obj">The object to check for equality.</param>
    /// <returns>Returns <see langword="true"/> if the object is equal to the current instance; otherwise, <see langword="false"/>.</returns>
    public override bool Equals(object? obj) => Equals(obj as Failure);

    /// <summary>
    /// Serves as a hash code function for the current instance.
    /// </summary>
    /// <returns>Returns a hash code for the current instance.</returns>
    public override int GetHashCode() => Exception.GetHashCode();

    /// <summary>
    /// Gets the underlying exception if the current <see cref="Result"/> is in a <see cref="Failure"/> state,
    /// or <see langword="null"/> if the current <see cref="Result"/> is in a <see cref="Success"/> state.
    /// </summary>
    /// <returns>
    /// Returns the underlying exception if the current <see cref="Result"/> is in a <see cref="Failure"/> state,
    /// or <see langword="null"/> if the current <see cref="Result"/> is in a <see cref="Success"/> state.
    /// </returns>
    public override Exception GetExceptionOrDefault() => Exception;

    /// <summary>
    /// Gets the underlying exception if the current <see cref="Result"/> is in a <see cref="Failure"/> state,
    /// or the specified default exception if the current <see cref="Result"/> is in a <see cref="Success"/> state.
    /// </summary>
    /// <param name="defaultException">The default exception to return in the event that the current <see cref="Result"/> is in a <see cref="Success"/> state.</param>
    /// <returns>
    /// Returns the underlying exception if the current <see cref="Result"/> is in a <see cref="Failure"/> state,
    /// or the specified default exception if the current <see cref="Result"/> is in a <see cref="Success"/> state.
    /// </returns>
    public override Exception GetExceptionOrDefault(Exception defaultException) => Exception;

    /// <summary>
    /// Gets the underlying exception if the current <see cref="Result"/> is in a <see cref="Failure"/> state,
    /// or throws <see cref="InvalidOperationException"/> if the current <see cref="Result"/> is in a <see cref="Success"/> state.
    /// </summary>
    /// <returns>
    /// Returns the underlying exception if the current <see cref="Result"/> is in a <see cref="Failure"/> state,
    /// or throws <see cref="InvalidOperationException"/> if the current <see cref="Result"/> is in a <see cref="Success"/> state.
    /// </returns>
    /// <exception cref="InvalidOperationException">If the current <see cref="Result"/> is in a <see cref="Success"/> state.</exception>
    public override Exception GetExceptionOrThrow() => Exception;

    /// <summary>
    /// Executes the action that matches the value of the current <see cref="Result"/> instance.
    /// <remarks>In the case of a failure, the failure branch is invoked.</remarks>
    /// </summary>
    /// <param name="success">The action to execute when the current <see cref="Result"/> instance is in a successful state.</param>
    /// <param name="failure">The action to execute when the current <see cref="Result"/> instance is in a failed state.</param>
    public override void Match(Action? success = null, Action<Exception>? failure = null) => failure?.Invoke(Exception);

    /// <summary>
    /// Executes the function that matches the value of the current <see cref="Result"/> instance and returns its result.
    /// <remarks>In the case of a failure, the failure branch is invoked.</remarks>
    /// </summary>
    /// <param name="success">The function to execute when the current <see cref="Result"/> instance is in a successful state.</param>
    /// <param name="failure">The function to execute when the current <see cref="Result"/> instance is in a failed state.</param>
    /// <typeparam name="TResult">The underlying type of the result produced by the matching function.</typeparam>
    /// <returns>
    /// Returns the result of the <paramref name="success"/> function if the current <see cref="Result"/> instance is in a successful state;
    /// otherwise, returns the result of the <paramref name="failure"/> function if the current <see cref="Result"/> instance is in a failed state.
    /// </returns>
    public override TResult Match<TResult>(Func<TResult> success, Func<Exception, TResult> failure) => failure(Exception);

    /// <summary>
    /// Applies the provided selector action to the value of the current <see cref="Result"/> instance.
    /// <remarks>In the case of a failure, the current <see cref="Failure"/> instance is returned.</remarks>
    /// </summary>
    /// <param name="selector">The action to apply to current <see cref="Result"/> instance.</param>
    /// <returns>
    /// Returns <see cref="Success"/> if the current <see cref="Result"/> is in a successful state, and the action invocation is also successful; otherwise; <see cref="Failure"/>.
    /// </returns>
    public override Result Select(Action selector) => this;

    /// <summary>
    /// Applies the provided selector function to the value of the current <see cref="Result"/> instance.
    /// <remarks>In the case of a failure, a new <see cref="Failure{T}"/> instance is returned with the current exception.</remarks>
    /// </summary>
    /// <param name="selector">The function to apply to the current <see cref="Result"/> instance.</param>
    /// <typeparam name="TResult">The underlying type of the result produced by the selector function.</typeparam>
    /// <returns>
    /// Returns <see cref="Success{T}"/> if the current <see cref="Result"/> is in a successful state, and the function invocation is also successful; otherwise; <see cref="Failure{T}"/>.
    /// </returns>
    public override Result<TResult> Select<TResult>(Func<TResult> selector) => Result<TResult>.Failure(Exception);

    /// <summary>
    /// Applies the provided selector function to the value of the current <see cref="Result"/> instance.
    /// <remarks>In the case of a failure, the current <see cref="Failure"/> instance is returned.</remarks>
    /// </summary>
    /// <param name="selector">The action to function to the current <see cref="Result"/> instance.</param>
    /// <returns>
    /// Returns <see cref="Success"/> if the current <see cref="Result"/> is in a successful state, and the function invocation is also successful; otherwise; <see cref="Failure"/>.
    /// </returns>
    public override Result SelectMany(Func<Result> selector) => this;

    /// <summary>
    /// Applies the provided selector function to the value of the current <see cref="Result"/> instance.
    /// <remarks>In the case of a failure, a new <see cref="Failure{T}"/> instance is returned with the current exception.</remarks>
    /// </summary>
    /// <param name="selector">The function to apply to the current <see cref="Result"/> instance.</param>
    /// <typeparam name="TResult">The underlying type of the result produced by the selector function.</typeparam>
    /// <returns>
    /// Returns <see cref="Success{T}"/> if the current <see cref="Result"/> is in a successful state, and the function invocation is also successful; otherwise; <see cref="Failure{T}"/>.
    /// </returns>
    public override Result<TResult> SelectMany<TResult>(Func<Result<TResult>> selector) => Result<TResult>.Failure(Exception);

    /// <summary>
    /// Throws the underlying exception if the current <see cref="Result"/> is in a failure state.
    /// </summary>
    public override void Throw() => throw Exception;

    /// <summary>
    /// Returns a <see cref="String"/> that represents the current object.
    /// </summary>
    /// <returns>Returns a <see cref="String"/> that represents the current object.</returns>
    public override string ToString() => Exception.ToString();

    /// <summary>
    /// Obtains a new <see cref="Failure{T}"/> instance containing the current exception.
    /// </summary>
    /// <typeparam name="TResult">The underlying type of the <see cref="Failure{T}"/> to return.</typeparam>
    /// <returns>Returns a new <see cref="Failure{T}"/> instance containing the current exception.</returns>
    public Failure<TResult> ToTypedResult<TResult>() => Result<TResult>.Failure(Exception);
}

/// <summary>
/// Represents a failed result.
/// </summary>
/// <typeparam name="T">The type of the underlying result value.</typeparam>
public sealed class Failure<T> : Result<T>, IValueEquatable<Failure<T>>
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
    /// Performs an equality comparison between two object instances.
    /// </summary>
    /// <param name="left">The left-hand instance to compare.</param>
    /// <param name="right">The right-hand instance to compare.</param>
    /// <returns>Returns <see langword="true"/> if the left-hand instance is equal to the right-hand instance; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(Failure<T> left, Failure<T> right) => Equals(left, right);

    /// <summary>
    /// Performs an inequality comparison between two object instances.
    /// </summary>
    /// <param name="left">The left-hand instance to compare.</param>
    /// <param name="right">The right-hand instance to compare.</param>
    /// <returns>Returns <see langword="true"/> if the left-hand instance is not equal to the right-hand instance; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(Failure<T> left, Failure<T> right) => !Equals(left, right);

    /// <summary>
    /// Checks whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with the current object.</param>
    /// <returns>Returns <see langword="true"/> if the current object is equal to the other parameter; otherwise, <see langword="false"/>.</returns>
    public bool Equals(Failure<T>? other) => ReferenceEquals(this, other) || other is not null && Equals(other.Exception, Exception);

    /// <summary>
    /// Checks for equality between the current instance and another object.
    /// </summary>
    /// <param name="obj">The object to check for equality.</param>
    /// <returns>Returns <see langword="true"/> if the object is equal to the current instance; otherwise, <see langword="false"/>.</returns>
    public override bool Equals(object? obj) => Equals(obj as Failure<T>);

    /// <summary>
    /// Serves as a hash code function for the current instance.
    /// </summary>
    /// <returns>Returns a hash code for the current instance.</returns>
    public override int GetHashCode() => Exception.GetHashCode();

    /// <summary>
    /// Gets the underlying exception if the current <see cref="Result{T}"/> is in a <see cref="Failure{T}"/> state,
    /// or <see langword="null"/> if the current <see cref="Result{T}"/> is in a <see cref="Success{T}"/> state.
    /// </summary>
    /// <returns>
    /// Returns the underlying exception if the current <see cref="Result{T}"/> is in a <see cref="Failure{T}"/> state,
    /// or <see langword="null"/> if the current <see cref="Result{T}"/> is in a <see cref="Success{T}"/> state.
    /// </returns>
    public override Exception GetExceptionOrDefault() => Exception;

    /// <summary>
    /// Gets the underlying exception if the current <see cref="Result{T}"/> is in a <see cref="Failure{T}"/> state,
    /// or the specified default exception if the current <see cref="Result{T}"/> is in a <see cref="Success{T}"/> state.
    /// </summary>
    /// <param name="defaultException">The default exception to return in the event that the current <see cref="Result{T}"/> is in a <see cref="Success{T}"/> state.</param>
    /// <returns>
    /// Returns the underlying exception if the current <see cref="Result{T}"/> is in a <see cref="Failure"/> state,
    /// or the specified default exception if the current <see cref="Result{T}"/> is in a <see cref="Success{T}"/> state.
    /// </returns>
    public override Exception GetExceptionOrDefault(Exception defaultException) => Exception;

    /// <summary>
    /// Gets the underlying exception if the current <see cref="Result{T}"/> is in a <see cref="Failure{T}"/> state,
    /// or throws <see cref="InvalidOperationException"/> if the current <see cref="Result{T}"/> is in a <see cref="Success{T}"/> state.
    /// </summary>
    /// <returns>
    /// Returns the underlying exception if the current <see cref="Result{T}"/> is in a <see cref="Failure{T}"/> state,
    /// or throws <see cref="InvalidOperationException"/> if the current <see cref="Result{T}"/> is in a <see cref="Success{T}"/> state.
    /// </returns>
    /// <exception cref="InvalidOperationException">If the current <see cref="Result{T}"/> is in a <see cref="Success{T}"/> state.</exception>
    public override Exception GetExceptionOrThrow() => Exception;

    /// <summary>
    /// Gets the underlying value of the current <see cref="Result{T}"/> instance, if the underlying value is present;
    /// otherwise returns the default <typeparamref name="T"/> value.
    /// </summary>
    /// <returns>
    /// Returns the underlying value of the current <see cref="Result{T}"/> instance, if the underlying value is present;
    /// otherwise returns the default <typeparamref name="T"/> value.
    /// </returns>
    public override T? GetValueOrDefault() => default;

    /// <summary>
    /// Gets the underlying value of the current <see cref="Result{T}"/> instance, if the underlying value is present;
    /// otherwise returns the specified default value.
    /// </summary>
    /// <param name="defaultValue">The default value to return in the event that the underlying value is absent.</param>
    /// <returns>
    /// Returns the underlying value of the current <see cref="Result{T}"/> instance, if the underlying value is present;
    /// otherwise returns the specified default value.
    /// </returns>
    public override T GetValueOrDefault(T defaultValue) => defaultValue;

    /// <summary>
    /// Gets the underlying value of the current <see cref="Result{T}"/> instance;
    /// otherwise throws the underlying exception if the current <see cref="Result{T}"/> is in a failed stated.
    /// </summary>
    /// <returns>
    /// Returns the underlying value of the current <see cref="Result{T}"/> instance;
    /// otherwise throws the underlying exception if the current <see cref="Result{T}"/> is in a failed stated.
    /// </returns>
    public override T GetValueOrThrow() => throw Exception;

    /// <summary>
    /// Executes the action that matches the value of the current <see cref="Result{T}"/> instance.
    /// <remarks>In the case of a failure, the failure branch is invoked.</remarks>
    /// </summary>
    /// <param name="success">The action to execute when the current <see cref="Result{T}"/> instance is in a successful state.</param>
    /// <param name="failure">The action to execute when the current <see cref="Result{T}"/> instance is in a failed state.</param>
    public override void Match(Action<T>? success = null, Action<Exception>? failure = null) => failure?.Invoke(Exception);

    /// <summary>
    /// Executes the function that matches the value of the current <see cref="Result{T}"/> instance and returns its result.
    /// <remarks>In the case of a failure, the failure branch is invoked.</remarks>
    /// </summary>
    /// <param name="success">The function to execute when the current <see cref="Result{T}"/> instance is in a successful state.</param>
    /// <param name="failure">The function to execute when the current <see cref="Result{T}"/> instance is in a failed state.</param>
    /// <typeparam name="TResult">The underlying type of the result produced by the matching function.</typeparam>
    /// <returns>
    /// Returns the result of the <paramref name="success"/> function if the current <see cref="Result{T}"/> instance is in a successful state;
    /// otherwise, returns the result of the <paramref name="failure"/> function if the current <see cref="Result{T}"/> instance is in a failed state.
    /// </returns>
    public override TResult Match<TResult>(Func<T, TResult> success, Func<Exception, TResult> failure) => failure(Exception);

    /// <summary>
    /// Applies the provided selector action to the value of the current <see cref="Result{T}"/> instance.
    /// <remarks>In the case of a failure, a new <see cref="Failure"/> instance is returned with the current exception.</remarks>
    /// </summary>
    /// <param name="selector">The action to apply to the value of the current <see cref="Result{T}"/> instance.</param>
    /// <returns>
    /// Returns <see cref="Success"/> if the current <see cref="Result{T}"/> is in a successful state, and the action invocation is also successful; otherwise; <see cref="Failure"/>.
    /// </returns>
    public override Result Select(Action<T> selector) => Result.Failure(Exception);

    /// <summary>
    /// Applies the provided selector function to the value of the current <see cref="Result{T}"/> instance.
    /// <remarks>In the case of a failure, a new <see cref="Failure{T}"/> instance is returned with the current exception.</remarks>
    /// </summary>
    /// <param name="selector">The function to apply to the value of the current <see cref="Result{T}"/> instance.</param>
    /// <typeparam name="TResult">The underlying type of the result produced by the selector function.</typeparam>
    /// <returns>
    /// Returns <see cref="Success{T}"/> if the current <see cref="Result{T}"/> is in a successful state, and the function invocation is also successful; otherwise; <see cref="Failure{T}"/>.
    /// </returns>
    public override Result<TResult> Select<TResult>(Func<T, TResult> selector) => Result<TResult>.Failure(Exception);

    /// <summary>
    /// Applies the provided selector function to the value of the current <see cref="Result{T}"/> instance.
    /// <remarks>In the case of a failure, a new <see cref="Failure"/> instance is returned with the current exception.</remarks>
    /// </summary>
    /// <param name="selector">The function to apply to the value of the current <see cref="Result{T}"/> instance.</param>
    /// <returns>
    /// Returns <see cref="Success"/> if the current <see cref="Result{T}"/> is in a successful state, and the function invocation is also successful; otherwise; <see cref="Failure"/>.
    /// </returns>
    public override Result SelectMany(Func<T, Result> selector) => Result.Failure(Exception);

    /// <summary>
    /// Applies the provided selector function to the value of the current <see cref="Result{T}"/> instance.
    /// <remarks>In the case of a failure, a new <see cref="Failure{T}"/> instance is returned with the current exception.</remarks>
    /// </summary>
    /// <param name="selector">The function to apply to the value of the current <see cref="Result{T}"/> instance.</param>
    /// <typeparam name="TResult">The underlying type of the result produced by the selector function.</typeparam>
    /// <returns>
    /// Returns <see cref="Success{T}"/> if the current <see cref="Result{T}"/> is in a successful state, and the function invocation is also successful; otherwise; <see cref="Failure{T}"/>.
    /// </returns>
    public override Result<TResult> SelectMany<TResult>(Func<T, Result<TResult>> selector) => Result<TResult>.Failure(Exception);

    /// <summary>
    /// Throws the underlying exception if the current <see cref="Result{T}"/> is in a failure state.
    /// </summary>
    public override void Throw() => throw Exception;

    /// <summary>
    /// Returns a <see cref="String"/> that represents the current object.
    /// </summary>
    /// <returns>Returns a <see cref="String"/> that represents the current object.</returns>
    public override string ToString() => Exception.ToString();

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

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public override void Dispose()
    {
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources asynchronously.
    /// </summary>
    /// <returns>
    /// Returns a task that represents the asynchronous dispose operation.
    /// </returns>
    public override async ValueTask DisposeAsync()
    {
        await ValueTask.CompletedTask;
    }
}
