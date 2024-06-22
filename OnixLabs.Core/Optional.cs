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

namespace OnixLabs.Core;

/// <summary>
/// Represents an optional value, which signifies the presence or absence of a value.
/// </summary>
/// <typeparam name="T">The type of the underlying optional value.</typeparam>
public abstract class Optional<T> : IValueEquatable<Optional<T>> where T : notnull
{
    /// <summary>
    /// Gets a value indicating that the optional value is absent.
    /// </summary>
    public static readonly None<T> None = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="Optional{T}"/> class.
    /// </summary>
    internal Optional()
    {
    }

    /// <summary>
    /// Gets a value indicating whether the underlying value of the current <see cref="Optional{T}"/> instance is present.
    /// </summary>
    public bool HasValue => this is Some<T>;

    /// <summary>
    /// Gets a value indicating whether the specified <see cref="Optional{T}"/> instance is <see cref="None{T}"/>.
    /// </summary>
    /// <param name="value">The <see cref="Optional{T}"/> value to check.</param>
    /// <returns>Returns <see langword="true"/> if the specified <see cref="Optional{T}"/> instance is <see cref="None{T}"/>; otherwise, <see langword="false"/>.</returns>
    public static bool IsNone(Optional<T> value) => value is None<T>;

    /// <summary>
    /// Gets a value indicating whether the specified <see cref="Optional{T}"/> instance is <see cref="Some{T}"/>.
    /// </summary>
    /// <param name="value">The <see cref="Optional{T}"/> value to check.</param>
    /// <returns>Returns <see langword="true"/> if the specified <see cref="Optional{T}"/> instance is <see cref="Some{T}"/>; otherwise, <see langword="false"/>.</returns>
    public static bool IsSome(Optional<T> value) => value is Some<T>;

    /// <summary>
    /// Creates a new instance of the <see cref="Optional{T}"/> class, where the underlying value is present if
    /// the specified value is not <see langword="default"/>; otherwise, the underlying value is <see cref="None"/>.
    /// <remarks>
    /// This method is similar to the <see cref="Some"/> method, however <see cref="Some"/> will treat <see langword="struct"/>
    /// <see langword="default"/> values as present, whereas <see cref="Of"/> will treat them as absent.
    /// </remarks>
    /// </summary>
    /// <param name="value">The underlying optional value.</param>
    /// <returns>
    /// Returns a new instance of the <see cref="Optional{T}"/> class, where the underlying value is present if
    /// the specified value is not <see langword="default"/>; otherwise, the underlying value is <see cref="None"/>.
    /// </returns>
    public static Optional<T> Of(T? value) => value is not null && !EqualityComparer<T>.Default.Equals(value, default)
        ? Some(value)
        : None;

    /// <summary>
    /// Creates a new instance of the <see cref="Optional{T}"/> class, where the underlying value is present if
    /// the specified value is not <see langword="null"/>; otherwise, the underlying value is <see cref="None"/>.
    /// </summary>
    /// <param name="value">The underlying optional value.</param>
    /// <returns>
    /// Returns a new instance of the <see cref="Optional{T}"/> class, where the underlying value is present if
    /// the specified value is not <see langword="null"/>; otherwise, the underlying value is <see cref="None"/>.
    /// </returns>
    public static Optional<TStruct> Of<TStruct>(TStruct? value) where TStruct : struct => value.HasValue
        ? Optional<TStruct>.Some(value.Value)
        : Optional<TStruct>.None;

    /// <summary>
    /// Creates a new instance of the <see cref="Optional{T}"/> class, where the underlying value is present if
    /// the specified value is not <see langword="null"/>; otherwise, the underlying value is <see cref="None"/>.
    /// </summary>
    /// <param name="value">The underlying optional value.</param>
    /// <returns>
    /// Returns a new instance of the <see cref="Optional{T}"/> class, where the underlying value is present if
    /// the specified value is not <see langword="null"/>; otherwise, the underlying value is <see cref="None"/>.
    /// </returns>
    public static Some<T> Some(T value) => new(RequireNotNull(value, "Value must not be null.", nameof(value)));

    /// <summary>
    /// Creates a new instance of the <see cref="Optional{T}"/> class, where the underlying value is present if
    /// the specified value is not <see langword="default"/>; otherwise, the underlying value is <see cref="None"/>.
    /// </summary>
    /// <param name="value">The underlying optional value.</param>
    /// <returns>
    /// Returns a new instance of the <see cref="Optional{T}"/> class, where the underlying value is present if
    /// the specified value is not <see langword="default"/>; otherwise, the underlying value is <see cref="None"/>.
    /// </returns>
    public static implicit operator Optional<T>(T? value) => value is not null ? Some(value) : None;

    /// <summary>
    /// Gets the underlying value of the specified <see cref="Optional{T}"/> instance;
    /// otherwise throws an <see cref="InvalidOperationException"/> if the value is absent.
    /// </summary>
    /// <param name="value">The <see cref="Optional{T}"/> instance from which to obtain the underlying value.</param>
    /// <returns>
    /// Returns the underlying value of the specified <see cref="Optional{T}"/> instance;
    /// otherwise throws an <see cref="InvalidOperationException"/> if the value is absent.
    /// </returns>
    /// <exception cref="InvalidOperationException">If the underlying value is absent.</exception>
    public static explicit operator T(Optional<T> value) => value.GetValueOrThrow();

    /// <summary>
    /// Performs an equality comparison between two object instances.
    /// </summary>
    /// <param name="left">The left-hand instance to compare.</param>
    /// <param name="right">The right-hand instance to compare.</param>
    /// <returns>Returns <see langword="true"/> if the left-hand instance is equal to the right-hand instance; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(Optional<T> left, Optional<T> right) => Equals(left, right);

    /// <summary>
    /// Performs an inequality comparison between two object instances.
    /// </summary>
    /// <param name="left">The left-hand instance to compare.</param>
    /// <param name="right">The right-hand instance to compare.</param>
    /// <returns>Returns <see langword="true"/> if the left-hand instance is not equal to the right-hand instance; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(Optional<T> left, Optional<T> right) => !Equals(left, right);

    /// <summary>
    /// Checks whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with the current object.</param>
    /// <returns>Returns <see langword="true"/> if the current object is equal to the other parameter; otherwise, <see langword="false"/>.</returns>
    public bool Equals(Optional<T>? other) => this switch
    {
        Some<T> some => other is Some<T> someOther && some.Equals(someOther),
        None<T> none => other is None<T> noneOther && none.Equals(noneOther),
        _ => ReferenceEquals(this, other)
    };

    /// <summary>
    /// Checks for equality between the current instance and another object.
    /// </summary>
    /// <param name="obj">The object to check for equality.</param>
    /// <returns>Returns <see langword="true"/> if the object is equal to the current instance; otherwise, <see langword="false"/>.</returns>
    public override bool Equals(object? obj) => Equals(obj as Optional<T>);

    /// <summary>
    /// Serves as a hash code function for the current instance.
    /// </summary>
    /// <returns>Returns a hash code for the current instance.</returns>
    public override int GetHashCode() => default;

    /// <summary>
    /// Gets the underlying value of the current <see cref="Optional{T}"/> instance, if the underlying value is present;
    /// otherwise, returns the default <typeparamref name="T"/> value.
    /// </summary>
    /// <returns>
    /// Returns the underlying value of the current <see cref="Optional{T}"/> instance, if the underlying value is present;
    /// otherwise, returns the default <typeparamref name="T"/> value.
    /// </returns>
    public abstract T? GetValueOrDefault();

    /// <summary>
    /// Gets the underlying value of the current <see cref="Optional{T}"/> instance, if the underlying value is present;
    /// otherwise, returns the specified default value.
    /// </summary>
    /// <param name="defaultValue">The default value to return in the event that the underlying value is absent.</param>
    /// <returns>
    /// Returns the underlying value of the current <see cref="Optional{T}"/> instance, if the underlying value is present;
    /// otherwise, returns the specified default value.
    /// </returns>
    public abstract T GetValueOrDefault(T defaultValue);

    /// <summary>
    /// Gets the underlying value of the current <see cref="Optional{T}"/> instance, if the underlying value is present;
    /// otherwise, throws an <see cref="InvalidOperationException"/> exception.
    /// </summary>
    /// <returns>
    /// Returns the underlying value of the current <see cref="Optional{T}"/> instance, if the underlying value is present;
    /// otherwise, throws an <see cref="InvalidOperationException"/> exception.
    /// </returns>
    public abstract T GetValueOrThrow();

    /// <summary>
    /// Executes the action that matches the value of the current <see cref="Optional{T}"/> instance.
    /// </summary>
    /// <param name="some">The action to execute when the underlying value of the current <see cref="Optional{T}"/> instance is present.</param>
    /// <param name="none">The action to execute when the underlying value of the current <see cref="Optional{T}"/> instance is absent.</param>
    public abstract void Match(Action<T>? some = null, Action? none = null);

    /// <summary>
    /// Executes the function that matches the value of the current <see cref="Optional{T}"/> instance and returns its result.
    /// </summary>
    /// <param name="some">The function to execute when the underlying value of the current <see cref="Optional{T}"/> instance is present.</param>
    /// <param name="none">The function to execute when the underlying value of the current <see cref="Optional{T}"/> instance is absent.</param>
    /// <typeparam name="TResult">The underlying type of the result produced by the matching function.</typeparam>
    /// <returns>
    /// Returns the result of the <paramref name="some"/> function if the underlying value of the current <see cref="Optional{T}"/> value is present;
    /// otherwise, returns the result of the <paramref name="some"/> function if the underlying value of the current <see cref="Optional{T}"/> value is absent.
    /// </returns>
    public abstract TResult Match<TResult>(Func<T, TResult> some, Func<TResult> none);

    /// <summary>
    /// Applies the provided selector function to the value of the current <see cref="Optional{T}"/> instance.
    /// </summary>
    /// <param name="selector">The function to apply to the value of the current <see cref="Optional{T}"/> instance.</param>
    /// <typeparam name="TResult">The underlying type of the result produced by the selector function.</typeparam>
    /// <returns>
    /// Returns a new <see cref="Optional{TResult}"/> instance containing the result of the function if the current
    /// <see cref="Optional{T}"/> instance has an underlying value; otherwise, <see cref="Optional{TResult}.None"/>.
    /// </returns>
    public abstract Optional<TResult> Select<TResult>(Func<T, TResult> selector) where TResult : notnull;

    /// <summary>
    /// Applies the provided selector function to the value of the current <see cref="Optional{T}"/> instance.
    /// </summary>
    /// <param name="selector">The function to apply to the value of the current <see cref="Optional{T}"/> instance.</param>
    /// <typeparam name="TResult">The underlying type of the result produced by the selector function.</typeparam>
    /// <returns>
    /// Returns a new <see cref="Optional{TResult}"/> instance containing the result of the function if the current
    /// <see cref="Optional{T}"/> instance has an underlying value; otherwise, <see cref="Optional{TResult}.None"/>.
    /// </returns>
    public abstract Optional<TResult> SelectMany<TResult>(Func<T, Optional<TResult>> selector) where TResult : notnull;

    /// <summary>
    /// Wraps the current <see cref="Optional{T}"/> instance into new, successful <see cref="Result{T}"/> instance.
    /// </summary>
    /// <returns>Returns a new, successful <see cref="Result{T}"/> instance containing the current <see cref="Optional{T}"/> instance.</returns>
    public Result<Optional<T>> ToResult() => Result<Optional<T>>.Success(this);

    /// <summary>
    /// Returns a <see cref="String"/> that represents the current object.
    /// </summary>
    /// <returns>Returns a <see cref="String"/> that represents the current object.</returns>
    public override string ToString() => this switch
    {
        Some<T> some => some.ToString(),
        None<T> none => none.ToString(),
        _ => base.ToString() ?? GetType().FullName ?? nameof(Optional<T>)
    };
}
