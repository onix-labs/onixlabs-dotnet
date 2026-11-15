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
/// Represents an optional value that does not allocate, which signifies the presence or absence of a value.
/// </summary>
/// <typeparam name="T">The underlying type of the optional value.</typeparam>
/// <remarks>
/// <see cref="ValueOptional{T}"/> is the value-type counterpart of <see cref="Optional{T}"/>. It holds its value inline, so creating, copying and
/// matching a <see cref="ValueOptional{T}"/> never allocates. The default value is <see cref="None"/>.
/// <see cref="ValueOptional{T}"/> is deliberately not a C# 15 union: for a value type such as <see cref="int"/>, a <see langword="switch"/>
/// without a <see langword="null"/> arm would compile without a warning and throw on every <see cref="None"/>.
/// Use <see cref="HasValue"/>, <see cref="Match{TResult}"/> or <see cref="GetValueOrDefault()"/> instead.
/// </remarks>
public readonly struct ValueOptional<T> : IValueEquatable<ValueOptional<T>> where T : notnull
{
    private readonly T value;
    private readonly bool hasValue;

    /// <summary>
    /// Initializes a new instance of the <see cref="ValueOptional{T}"/> struct that holds the specified value.
    /// </summary>
    /// <param name="value">The underlying optional value.</param>
    private ValueOptional(T value)
    {
        this.value = value;
        hasValue = true;
    }

    /// <summary>
    /// Gets a <see cref="ValueOptional{T}"/> that has no value.
    /// </summary>
    /// <value>A <see cref="ValueOptional{T}"/> that has no value, which is also the default value of <see cref="ValueOptional{T}"/>.</value>
    public static ValueOptional<T> None => default;

    /// <summary>
    /// Gets a value indicating whether the current <see cref="ValueOptional{T}"/> has a value.
    /// </summary>
    /// <value><see langword="true"/> if the current <see cref="ValueOptional{T}"/> has a value; otherwise, <see langword="false"/>.</value>
    public bool HasValue => hasValue;

    /// <summary>
    /// Determines whether the specified <see cref="ValueOptional{T}"/> has no value.
    /// </summary>
    /// <param name="value">The <see cref="ValueOptional{T}"/> to check.</param>
    /// <returns>Returns <see langword="true"/> if the specified <see cref="ValueOptional{T}"/> has no value; otherwise, <see langword="false"/>.</returns>
    public static bool IsNone(ValueOptional<T> value) => !value.hasValue;

    /// <summary>
    /// Determines whether the specified <see cref="ValueOptional{T}"/> has a value.
    /// </summary>
    /// <param name="value">The <see cref="ValueOptional{T}"/> to check.</param>
    /// <returns>Returns <see langword="true"/> if the specified <see cref="ValueOptional{T}"/> has a value; otherwise, <see langword="false"/>.</returns>
    public static bool IsSome(ValueOptional<T> value) => value.hasValue;

    /// <summary>
    /// Creates a new <see cref="ValueOptional{T}"/> from the specified value, which has no value if the specified value is <see langword="null"/>
    /// or the default value of <typeparamref name="T"/>.
    /// </summary>
    /// <param name="value">The underlying optional value.</param>
    /// <returns>
    /// Returns <see cref="None"/> if the specified value is <see langword="null"/> or the default value of <typeparamref name="T"/>;
    /// otherwise, a <see cref="ValueOptional{T}"/> that holds the specified value.
    /// </returns>
    // ReSharper disable once HeapView.PossibleBoxingAllocation
    public static ValueOptional<T> Of(T? value) => value is null || EqualityComparer<T>.Default.Equals(value, default) ? None : Some(value);

    /// <summary>
    /// Creates a new <see cref="ValueOptional{T}"/> from the specified nullable value, which has no value if the specified value is <see langword="null"/>.
    /// </summary>
    /// <param name="value">The underlying optional value.</param>
    /// <typeparam name="TStruct">The underlying type of the value.</typeparam>
    /// <returns>
    /// Returns <see cref="None"/> if the specified value is <see langword="null"/>; otherwise, a <see cref="ValueOptional{T}"/> that holds the specified value.
    /// </returns>
    public static ValueOptional<TStruct> Of<TStruct>(TStruct? value) where TStruct : struct => value.HasValue
        ? ValueOptional<TStruct>.Some(value.Value)
        : ValueOptional<TStruct>.None;

    /// <summary>
    /// Creates a new <see cref="ValueOptional{T}"/> that holds the specified value.
    /// </summary>
    /// <param name="value">The underlying optional value.</param>
    /// <returns>Returns a new <see cref="ValueOptional{T}"/> that holds the specified value.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
    public static ValueOptional<T> Some(T value) => new(RequireNotNull(value, "Value must not be null."));

    /// <summary>
    /// Creates a new <see cref="ValueOptional{T}"/> from the specified <see cref="Optional{T}"/>.
    /// </summary>
    /// <param name="optional">The <see cref="Optional{T}"/> from which to create a new <see cref="ValueOptional{T}"/>.</param>
    /// <returns>Returns a new <see cref="ValueOptional{T}"/> in the same state as the specified <see cref="Optional{T}"/>.</returns>
    public static ValueOptional<T> From(Optional<T> optional) => RequireNotNull(optional, "Optional must not be null.") switch
    {
        Some<T> some => Some(some.Value),
        None<T> => None
    };

    /// <summary>
    /// Creates a new <see cref="ValueOptional{T}"/> from the specified value, which has no value if the specified value is <see langword="null"/>.
    /// </summary>
    /// <param name="value">The underlying optional value.</param>
    /// <returns>Returns <see cref="None"/> if the specified value is <see langword="null"/>; otherwise, a <see cref="ValueOptional{T}"/> that holds it.</returns>
    // ReSharper disable once HeapView.PossibleBoxingAllocation
    public static implicit operator ValueOptional<T>(T? value) => value is null ? None : Some(value);

    /// <summary>
    /// Gets the underlying value of the specified <see cref="ValueOptional{T}"/>, or throws if it has no value.
    /// </summary>
    /// <param name="value">The <see cref="ValueOptional{T}"/> from which to obtain the underlying value.</param>
    /// <returns>Returns the underlying value of the specified <see cref="ValueOptional{T}"/>.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the specified <see cref="ValueOptional{T}"/> has no value.</exception>
    public static explicit operator T(ValueOptional<T> value) => value.GetValueOrThrow();

    /// <inheritdoc/>
    public static bool operator ==(ValueOptional<T> left, ValueOptional<T> right) => left.Equals(right);

    /// <inheritdoc/>
    public static bool operator !=(ValueOptional<T> left, ValueOptional<T> right) => !left.Equals(right);

    /// <inheritdoc/>
    public bool Equals(ValueOptional<T> other) => ValueOptionalEqualityComparer<T>.Default.Equals(this, other);

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is ValueOptional<T> other && Equals(other);

    /// <inheritdoc/>
    // ReSharper disable once HeapView.PossibleBoxingAllocation
    public override int GetHashCode() => hasValue ? value.GetHashCode() : 0;

    /// <summary>
    /// Gets the underlying value of the current <see cref="ValueOptional{T}"/>,
    /// or the default value of <typeparamref name="T"/> if the current <see cref="ValueOptional{T}"/> has no value.
    /// </summary>
    /// <returns>
    /// Returns the underlying value of the current <see cref="ValueOptional{T}"/>,
    /// or the default value of <typeparamref name="T"/> if the current <see cref="ValueOptional{T}"/> has no value.
    /// </returns>
    public T? GetValueOrDefault() => hasValue ? value : default;

    /// <summary>
    /// Gets the underlying value of the current <see cref="ValueOptional{T}"/>,
    /// or the specified default value if the current <see cref="ValueOptional{T}"/> has no value.
    /// </summary>
    /// <param name="defaultValue">The default value to return in the event that the current <see cref="ValueOptional{T}"/> has no value.</param>
    /// <returns>
    /// Returns the underlying value of the current <see cref="ValueOptional{T}"/>,
    /// or the specified default value if the current <see cref="ValueOptional{T}"/> has no value.
    /// </returns>
    public T GetValueOrDefault(T defaultValue) => hasValue ? value : defaultValue;

    /// <summary>
    /// Gets the underlying value of the current <see cref="ValueOptional{T}"/>,
    /// or throws <see cref="InvalidOperationException"/> if the current <see cref="ValueOptional{T}"/> has no value.
    /// </summary>
    /// <returns>Returns the underlying value of the current <see cref="ValueOptional{T}"/>.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the current <see cref="ValueOptional{T}"/> has no value.</exception>
    public T GetValueOrThrow() => hasValue ? value : throw new InvalidOperationException($"Optional value of type {typeof(T).FullName} is not present.");

    /// <summary>
    /// Executes the action that matches the state of the current <see cref="ValueOptional{T}"/>.
    /// </summary>
    /// <param name="some">The delegate to execute when the current <see cref="ValueOptional{T}"/> has a value.</param>
    /// <param name="none">The delegate to execute when the current <see cref="ValueOptional{T}"/> has no value.</param>
    public void Match(Action<T>? some = null, Action? none = null)
    {
        if (hasValue)
            some?.Invoke(value);
        else
            none?.Invoke();
    }

    /// <summary>
    /// Executes the delegate that matches the state of the current <see cref="ValueOptional{T}"/> and returns its result.
    /// </summary>
    /// <param name="some">The delegate to execute when the current <see cref="ValueOptional{T}"/> has a value.</param>
    /// <param name="none">The delegate to execute when the current <see cref="ValueOptional{T}"/> has no value.</param>
    /// <typeparam name="TResult">The underlying type of the result produced by the matching delegate.</typeparam>
    /// <returns>
    /// Returns the result of the <paramref name="some"/> delegate if the current <see cref="ValueOptional{T}"/> has a value;
    /// otherwise, returns the result of the <paramref name="none"/> delegate.
    /// </returns>
    public TResult Match<TResult>(Func<T, TResult> some, Func<TResult> none) => hasValue ? some(value) : none();

    /// <summary>
    /// Applies the provided selector delegate to the value of the current <see cref="ValueOptional{T}"/>.
    /// </summary>
    /// <param name="selector">The delegate to apply to the value of the current <see cref="ValueOptional{T}"/>.</param>
    /// <typeparam name="TResult">The underlying type of the value produced by the selector delegate.</typeparam>
    /// <returns>
    /// Returns a <see cref="ValueOptional{T}"/> holding the value produced by the delegate if the current <see cref="ValueOptional{T}"/> has a value,
    /// and the delegate produces a non-null value; otherwise, <see cref="ValueOptional{T}.None"/>.
    /// </returns>
    public ValueOptional<TResult> Select<TResult>(Func<T, TResult> selector) where TResult : notnull =>
        hasValue ? selector(value) : ValueOptional<TResult>.None;

    /// <summary>
    /// Applies the provided selector delegate to the value of the current <see cref="ValueOptional{T}"/>, and returns the optional it produces.
    /// </summary>
    /// <param name="selector">The delegate to apply to the value of the current <see cref="ValueOptional{T}"/>.</param>
    /// <typeparam name="TResult">The underlying type of the value of the optional produced by the selector delegate.</typeparam>
    /// <returns>
    /// Returns the optional produced by the delegate if the current <see cref="ValueOptional{T}"/> has a value;
    /// otherwise, <see cref="ValueOptional{T}.None"/>.
    /// </returns>
    public ValueOptional<TResult> SelectMany<TResult>(Func<T, ValueOptional<TResult>> selector) where TResult : notnull =>
        hasValue ? selector(value) : ValueOptional<TResult>.None;

    /// <summary>
    /// Creates a new <see cref="ValueResult{T}"/> that represents a successful result, holding the current <see cref="ValueOptional{T}"/>.
    /// </summary>
    /// <returns>Returns a new <see cref="ValueResult{T}"/> that represents a successful result, holding the current <see cref="ValueOptional{T}"/>.</returns>
    public ValueResult<ValueOptional<T>> ToResult() => ValueResult<ValueOptional<T>>.Success(this);

    /// <summary>
    /// Creates a new <see cref="Optional{T}"/> in the same state as the current <see cref="ValueOptional{T}"/>.
    /// </summary>
    /// <returns>Returns a new <see cref="Optional{T}"/> in the same state as the current <see cref="ValueOptional{T}"/>.</returns>
    public Optional<T> ToOptional() => hasValue ? Optional<T>.Some(value) : Optional<T>.None;

    /// <inheritdoc/>
    // ReSharper disable once HeapView.PossibleBoxingAllocation
    public override string ToString() => hasValue ? value.ToString() ?? string.Empty : nameof(None);
}
