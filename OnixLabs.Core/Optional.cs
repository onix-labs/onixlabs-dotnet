// Copyright 2020-2024 ONIXLabs
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

namespace OnixLabs.Core;

/// <summary>
/// Represents an optional type.
/// </summary>
/// <typeparam name="T">The underlying optional type.</typeparam>
public readonly record struct Optional<T> : IValueEquatable<Optional<T>> where T : notnull
{
    /// <summary>
    /// Gets a value indicating that the optional value is not present.
    /// </summary>
    public static readonly Optional<T> None = default;

    /// <summary>
    /// The underlying optional value.
    /// </summary>
    private readonly T? value = default;

    /// <summary>
    /// Initialises a new instance of the <see cref="Optional{T}"/> struct.
    /// </summary>
    /// <param name="value">The underlying optional value.</param>
    public Optional(T value)
    {
        this.value = value;
        HasValue = true;
    }

    /// <summary>
    /// Gets the underlying optional value.
    /// </summary>
    /// <exception cref="InvalidOperationException">If the underlying value is not present.</exception>
    public T Value => GetValueOrThrow();

    /// <summary>
    /// Gets a value indicating whether the underlying optional value is present.
    /// </summary>
    public bool HasValue { get; }

    /// <summary>
    /// Gets a new instance of the <see cref="Optional{T}"/> struct where the underlying value is present.
    /// </summary>
    /// <param name="value">The underlying optional value.</param>
    /// <returns>Returns a new instance of the <see cref="Optional{T}"/> struct where the underlying value is present.</returns>
    public static Optional<T> Some(T value)
    {
        return new Optional<T>(value);
    }

    /// <summary>
    /// Gets the underlying value of the specified <see cref="Optional{T}"/> instance.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns the underlying value of the specified <see cref="Optional{T}"/> instance.</returns>
    /// <exception cref="InvalidOperationException">If the underlying value is not present.</exception>
    public static explicit operator T(Optional<T> value)
    {
        return value.Value;
    }

    /// <summary>
    /// Gets a new <see cref="Optional{T}"/> instance representing the specified value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>Returns a new <see cref="Optional{T}"/> instance representing the specified value.</returns>
    public static implicit operator Optional<T>(T value)
    {
        return Some(value);
    }

    /// <summary>
    /// Gets the underlying value of the current <see cref="Optional{T}"/> instance.
    /// </summary>
    /// <returns>>Returns the underlying value of the current <see cref="Optional{T}"/> instance.</returns>
    /// <exception cref="InvalidOperationException">If the underlying value is not present.</exception>
    public T GetValueOrThrow()
    {
        Check(HasValue, $"Optional value of type {typeof(T).FullName} is not present.");
        return value!;
    }

    /// <summary>
    /// Gets the underlying value of the current <see cref="Optional{T}"/> instance, or the specified default value, if the underlying value is not present.
    /// </summary>
    /// <returns>>Returns the underlying value of the current <see cref="Optional{T}"/> instance, or the specified default value, if the underlying value is not present.</returns>
    public T GetValueOrDefault(T defaultValue)
    {
        return HasValue ? Value : defaultValue;
    }

    /// <summary>
    /// Applies the provided function to the value of the current <see cref="Optional{T}"/> instance if its value is present.
    /// </summary>
    /// <param name="func">The function to apply to the value of the current <see cref="Optional{T}"/> instance.</param>
    /// <typeparam name="TResult">The underlying type of the result produced by the function.</typeparam>
    /// <returns> Returns an <see cref="Optional{TResult}"/> instance containing the result of the function if the current <see cref="Optional{T}"/> instance has an underlying value; otherwise, <see cref="Optional{TResult}.None"/>.</returns>
    public Optional<TResult> Bind<TResult>(Func<T, Optional<TResult>> func) where TResult : notnull
    {
        return HasValue ? func(Value) : Optional<TResult>.None;
    }

    /// <summary>
    /// Applies the provided function to the value of the current <see cref="Optional{T}"/> instance if its value is present.
    /// </summary>
    /// <param name="func">The function to apply to the value of the current <see cref="Optional{T}"/> instance.</param>
    /// <typeparam name="TResult">The underlying type of the result produced by the function.</typeparam>
    /// <returns> Returns an <see cref="Optional{TResult}"/> instance containing the result of the function if the current <see cref="Optional{T}"/> instance has an underlying value; otherwise, <see cref="Optional{TResult}.None"/>.</returns>
    public Optional<TResult> Select<TResult>(Func<T, TResult> func) where TResult : notnull
    {
        return HasValue ? Optional<TResult>.Some(func(Value)) : Optional<TResult>.None;
    }

    /// <summary>
    /// Returns a <see cref="String"/> that represents the current object.
    /// </summary>
    /// <returns>Returns a <see cref="String"/> that represents the current object.</returns>
    public override string ToString()
    {
        return HasValue ? Value.ToString() ?? string.Empty : "None";
    }
}
