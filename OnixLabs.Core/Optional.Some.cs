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
/// Represents a present optional value.
/// </summary>
/// <typeparam name="T">The type of the underlying optional value.</typeparam>
public sealed class Some<T> : Optional<T>, IValueEquatable<Some<T>> where T : notnull
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Some{T}"/> class.
    /// </summary>
    /// <param name="value">The underlying optional value.</param>
    internal Some(T value) => Value = value;

    /// <summary>
    /// Gets the underlying optional value.
    /// </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public T Value { get; }

    /// <summary>
    /// Performs an equality comparison between two object instances.
    /// </summary>
    /// <param name="left">The left-hand instance to compare.</param>
    /// <param name="right">The right-hand instance to compare.</param>
    /// <returns>Returns <see langword="true"/> if the left-hand instance is equal to the right-hand instance; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(Some<T> left, Some<T> right) => Equals(left, right);

    /// <summary>
    /// Performs an inequality comparison between two object instances.
    /// </summary>
    /// <param name="left">The left-hand instance to compare.</param>
    /// <param name="right">The right-hand instance to compare.</param>
    /// <returns>Returns <see langword="true"/> if the left-hand instance is not equal to the right-hand instance; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(Some<T> left, Some<T> right) => !Equals(left, right);

    /// <summary>
    /// Checks whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with the current object.</param>
    /// <returns>Returns <see langword="true"/> if the current object is equal to the other parameter; otherwise, <see langword="false"/>.</returns>
    public bool Equals(Some<T>? other) => ReferenceEquals(this, other) || other is not null && EqualityComparer<T>.Default.Equals(other.Value, Value);

    /// <summary>
    /// Checks for equality between the current instance and another object.
    /// </summary>
    /// <param name="obj">The object to check for equality.</param>
    /// <returns>Returns <see langword="true"/> if the object is equal to the current instance; otherwise, <see langword="false"/>.</returns>
    public override bool Equals(object? obj) => Equals(obj as Some<T>);

    /// <summary>
    /// Serves as a hash code function for the current instance.
    /// </summary>
    /// <returns>Returns a hash code for the current instance.</returns>
    // ReSharper disable once HeapView.PossibleBoxingAllocation
    public override int GetHashCode() => Value.GetHashCode();

    /// <summary>
    /// Gets the underlying value of the current <see cref="Optional{T}"/> instance, if the underlying value is present;
    /// otherwise, returns the default <typeparamref name="T"/> value.
    /// </summary>
    /// <returns>
    /// Returns the underlying value of the current <see cref="Optional{T}"/> instance, if the underlying value is present;
    /// otherwise, returns the default <typeparamref name="T"/> value.
    /// </returns>
    public override T GetValueOrDefault() => Value;

    /// <summary>
    /// Gets the underlying value of the current <see cref="Optional{T}"/> instance, if the underlying value is present;
    /// otherwise, returns the specified default value.
    /// </summary>
    /// <param name="defaultValue">The default value to return in the event that the underlying value is absent.</param>
    /// <returns>
    /// Returns the underlying value of the current <see cref="Optional{T}"/> instance, if the underlying value is present;
    /// otherwise, returns the specified default value.
    /// </returns>
    public override T GetValueOrDefault(T defaultValue) => Value;

    /// <summary>
    /// Gets the underlying value of the current <see cref="Optional{T}"/> instance, if the underlying value is present;
    /// otherwise, throws an <see cref="InvalidOperationException"/> exception.
    /// </summary>
    /// <returns>
    /// Returns the underlying value of the current <see cref="Optional{T}"/> instance, if the underlying value is present;
    /// otherwise, throws an <see cref="InvalidOperationException"/> exception.
    /// </returns>
    public override T GetValueOrThrow() => Value;

    /// <summary>
    /// Executes the action that matches the value of the current <see cref="Optional{T}"/> instance.
    /// </summary>
    /// <param name="some">The action to execute when the underlying value of the current <see cref="Optional{T}"/> instance is present.</param>
    /// <param name="none">The action to execute when the underlying value of the current <see cref="Optional{T}"/> instance is absent.</param>
    public override void Match(Action<T>? some = null, Action? none = null) => some?.Invoke(Value);

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
    public override TResult Match<TResult>(Func<T, TResult> some, Func<TResult> none) => some(Value);

    /// <summary>
    /// Applies the provided selector function to the value of the current <see cref="Optional{T}"/> instance.
    /// </summary>
    /// <param name="selector">The function to apply to the value of the current <see cref="Optional{T}"/> instance.</param>
    /// <typeparam name="TResult">The underlying type of the result produced by the selector function.</typeparam>
    /// <returns>
    /// Returns a new <see cref="Optional{TResult}"/> instance containing the result of the function if the current
    /// <see cref="Optional{T}"/> instance has an underlying value; otherwise, <see cref="Optional{TResult}.None"/>.
    /// </returns>
    public override Optional<TResult> Select<TResult>(Func<T, TResult> selector) => selector(Value);

    /// <summary>
    /// Applies the provided selector function to the value of the current <see cref="Optional{T}"/> instance.
    /// </summary>
    /// <param name="selector">The function to apply to the value of the current <see cref="Optional{T}"/> instance.</param>
    /// <typeparam name="TResult">The underlying type of the result produced by the selector function.</typeparam>
    /// <returns>
    /// Returns a new <see cref="Optional{TResult}"/> instance containing the result of the function if the current
    /// <see cref="Optional{T}"/> instance has an underlying value; otherwise, <see cref="Optional{TResult}.None"/>.
    /// </returns>
    public override Optional<TResult> SelectMany<TResult>(Func<T, Optional<TResult>> selector) => selector(Value);

    /// <summary>
    /// Returns a <see cref="String"/> that represents the current object.
    /// </summary>
    /// <returns>Returns a <see cref="String"/> that represents the current object.</returns>
    // ReSharper disable once HeapView.PossibleBoxingAllocation
    public override string ToString() => Value.ToString() ?? string.Empty;
}
