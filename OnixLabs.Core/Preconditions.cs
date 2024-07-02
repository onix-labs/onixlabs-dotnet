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
using OnixLabs.Core.Linq;

namespace OnixLabs.Core;

/// <summary>
/// Provides general methods for performing pre-conditions and guard clauses.
/// </summary>
public static class Preconditions
{
    private const string ArgumentFailed = "Argument must satisfy the specified condition.";
    private const string ArgumentNull = "Argument must not be null.";
    private const string ArgumentNullOrEmpty = "Argument must not be null or empty.";
    private const string ArgumentNullOrWhiteSpace = "Argument must not be null or whitespace.";
    private const string ArgumentOutOfRange = "Argument must be within range.";

    /// <summary>
    /// Performs a general pre-condition check that fails when the specified condition is <see langword="false"/>.
    /// </summary>
    /// <param name="condition">The condition to check.</param>
    /// <param name="message">The exception message to throw in the event that the specified condition is <see langword="false"/>.</param>
    /// <exception cref="InvalidOperationException">If the specified condition is <see langword="false"/>.</exception>
    public static void Check(bool condition, string message = ArgumentFailed)
    {
        if (!condition) throw new InvalidOperationException(message);
    }

    /// <summary>
    /// Performs a general pre-condition check that fails if the specified value is <see langword="null"/>.
    /// </summary>
    /// <param name="value">The nullable value to check.</param>
    /// <param name="message">The exception message to throw in the event that the specified value is <see langword="null"/>.</param>
    /// <typeparam name="T">The underlying type of the value.</typeparam>
    /// <returns>Returns a non-null value of the specified type.</returns>
    /// <exception cref="InvalidOperationException">If the specified value is <see langword="null"/>.</exception>
    public static T CheckNotNull<T>(T? value, string message = ArgumentNull)
    {
        return value ?? throw new InvalidOperationException(message);
    }

    /// <summary>
    /// Performs a general pre-condition check that fails if the specified value is <see langword="null"/>.
    /// </summary>
    /// <param name="value">The nullable value to check.</param>
    /// <param name="message">The exception message to throw in the event that the specified value is <see langword="null"/>.</param>
    /// <typeparam name="T">The underlying type of the value.</typeparam>
    /// <returns>Returns a non-null value of the specified type.</returns>
    /// <exception cref="InvalidOperationException">If the specified value is <see langword="null"/>.</exception>
    public static T CheckNotNull<T>(T? value, string message = ArgumentNull) where T : struct
    {
        return value ?? throw new InvalidOperationException(message);
    }

    /// <summary>
    /// Performs a general pre-condition check that fails if the specified value is <see langword="null"/> or an empty string.
    /// </summary>
    /// <param name="value">The <see cref="String"/> value to check.</param>
    /// <param name="message">The exception message to throw in the event that the specified value is <see langword="null"/> or an empty string.</param>
    /// <returns>Returns a non-null, non-empty <see cref="String"/> value.</returns>
    /// <exception cref="InvalidOperationException">If the specified value is <see langword="null"/> or an empty string.</exception>
    public static string CheckNotNullOrEmpty(string? value, string message = ArgumentNullOrEmpty)
    {
        if (string.IsNullOrEmpty(value)) throw new InvalidOperationException(message);
        return value;
    }

    /// <summary>
    /// Performs a general pre-condition check that fails if the specified value is <see langword="null"/> or a whitespace string.
    /// </summary>
    /// <param name="value">The <see cref="String"/> value to check.</param>
    /// <param name="message">The exception message to throw in the event that the specified value is <see langword="null"/> or a whitespace string.</param>
    /// <returns>Returns a non-null, non-empty <see cref="String"/> value.</returns>
    /// <exception cref="InvalidOperationException">If the specified value is <see langword="null"/> or a whitespace string.</exception>
    public static string CheckNotNullOrWhiteSpace(string? value, string message = ArgumentNullOrWhiteSpace)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new InvalidOperationException(message);
        return value;
    }

    /// <summary>
    /// Performs a general pre-condition requirement that fails when the specified condition is <see langword="false"/>.
    /// </summary>
    /// <param name="condition">The condition to check.</param>
    /// <param name="message">The exception message to throw in the event that the specified condition is <see langword="false"/>.</param>
    /// <param name="parameterName">The name of the invalid parameter.</param>
    /// <exception cref="ArgumentException">If the specified condition is <see langword="false"/>.</exception>
    public static void Require(bool condition, string message = ArgumentFailed, string? parameterName = null)
    {
        if (!condition) throw new ArgumentException(message, parameterName);
    }

    /// <summary>
    /// Performs a general pre-condition requirement that fails when the specified condition is <see langword="false"/>.
    /// </summary>
    /// <param name="condition">The condition to check.</param>
    /// <param name="message">The exception message to throw in the event that the specified condition is <see langword="false"/>.</param>
    /// <param name="parameterName">The name of the invalid parameter.</param>
    /// <exception cref="ArgumentOutOfRangeException">If the specified condition is <see langword="false"/>.</exception>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("This method is obsolete and will be removed in a future version. Use RequireWithinRangeInclusive or RequireWithinRangeExclusive methods instead.")]
    public static void RequireWithinRange(bool condition, string message = ArgumentOutOfRange, string? parameterName = null)
    {
        if (!condition) throw new ArgumentOutOfRangeException(parameterName, message);
    }

    /// <summary>
    /// Performs a general pre-condition requirement that fails when the specified value falls inclusively outside the specified minimum and maximum values.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="min">The inclusive minimum value to test.</param>
    /// <param name="max">The inclusive maximum value to test.</param>
    /// <param name="message">The exception message to throw in the event that the specified value falls inclusively outside the specified minimum and maximum values.</param>
    /// <param name="parameterName">The name of the invalid parameter.</param>
    /// <typeparam name="T">The underlying type of the value to check.</typeparam>
    /// <exception cref="ArgumentOutOfRangeException">If the specified value falls inclusively outside the specified minimum and maximum values.</exception>
    public static void RequireWithinRangeInclusive<T>(T value, T min, T max, string message = ArgumentOutOfRange, string? parameterName = null) where T : IComparable<T>
    {
        if (!value.IsWithinRangeInclusive(min, max)) throw new ArgumentOutOfRangeException(parameterName, message);
    }

    /// <summary>
    /// Performs a general pre-condition requirement that fails when the specified value falls exclusively outside the specified minimum and maximum values.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="min">The exclusive minimum value to test.</param>
    /// <param name="max">The exclusive maximum value to test.</param>
    /// <param name="message">The exception message to throw in the event that the specified value falls exclusively outside the specified minimum and maximum values.</param>
    /// <param name="parameterName">The name of the invalid parameter.</param>
    /// <typeparam name="T">The underlying type of the value to check.</typeparam>
    /// <exception cref="ArgumentOutOfRangeException">If the specified value falls exclusively outside the specified minimum and maximum values.</exception>
    public static void RequireWithinRangeExclusive<T>(T value, T min, T max, string message = ArgumentOutOfRange, string? parameterName = null) where T : IComparable<T>
    {
        if (!value.IsWithinRangeExclusive(min, max)) throw new ArgumentOutOfRangeException(parameterName, message);
    }

    /// <summary>
    /// Performs a general pre-condition requirement that fails if the specified value is <see langword="null"/>.
    /// </summary>
    /// <param name="value">The nullable value to check.</param>
    /// <param name="message">The exception message to throw in the event that the specified value is <see langword="null"/>.</param>
    /// <param name="parameterName">The name of the invalid parameter.</param>
    /// <typeparam name="T">The underlying type of the value.</typeparam>
    /// <returns>Returns a non-null value of the specified type.</returns>
    /// <exception cref="ArgumentNullException">If the specified value is <see langword="null"/>.</exception>
    public static T RequireNotNull<T>(T? value, string message = ArgumentNull, string? parameterName = null)
    {
        return value ?? throw new ArgumentNullException(parameterName, message);
    }

    /// <summary>
    /// Performs a general pre-condition requirement that fails if the specified value is <see langword="null"/>.
    /// </summary>
    /// <param name="value">The nullable value to check.</param>
    /// <param name="message">The exception message to throw in the event that the specified value is <see langword="null"/>.</param>
    /// <param name="parameterName">The name of the invalid parameter.</param>
    /// <typeparam name="T">The underlying type of the value.</typeparam>
    /// <returns>Returns a non-null value of the specified type.</returns>
    /// <exception cref="ArgumentNullException">If the specified value is <see langword="null"/>.</exception>
    public static T RequireNotNull<T>(T? value, string message = ArgumentNull, string? parameterName = null) where T : struct
    {
        return value ?? throw new ArgumentNullException(parameterName, message);
    }

    /// <summary>
    /// Performs a general pre-condition requirement that fails if the specified value is <see langword="null"/> or an empty string.
    /// </summary>
    /// <param name="value">The <see cref="String"/> value to check.</param>
    /// <param name="message">The exception message to throw in the event that the specified value is <see langword="null"/> or an empty string.</param>
    /// <param name="parameterName">The name of the invalid parameter.</param>
    /// <returns>Returns a non-null, non-empty <see cref="String"/> value.</returns>
    /// <exception cref="ArgumentException">If the specified value is <see langword="null"/> or an empty string.</exception>
    public static string RequireNotNullOrEmpty(string? value, string message = ArgumentNullOrEmpty, string? parameterName = null)
    {
        if (string.IsNullOrEmpty(value)) throw new ArgumentException(message, parameterName);
        return value;
    }

    /// <summary>
    /// Performs a general pre-condition requirement that fails if the specified value is <see langword="null"/> or a whitespace string.
    /// </summary>
    /// <param name="value">The <see cref="String"/> value to check.</param>
    /// <param name="message">The exception message to throw in the event that the specified value is <see langword="null"/> or a whitespace string.</param>
    /// <param name="parameterName">The name of the invalid parameter.</param>
    /// <returns>Returns a non-null, non-empty <see cref="String"/> value.</returns>
    /// <exception cref="ArgumentException">If the specified value is <see langword="null"/> or a whitespace string.</exception>
    public static string RequireNotNullOrWhiteSpace(string? value, string message = ArgumentNullOrWhiteSpace, string? parameterName = null)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException(message, parameterName);
        return value;
    }

    /// <summary>
    /// Performs a general pre-condition requirement that the specified value is defined by the specified <see cref="Enum"/> type.
    /// </summary>
    /// <param name="value">The enum value to check.</param>
    /// <param name="parameterName">The name of the invalid parameter.</param>
    /// <typeparam name="T">The underlying type of the <see cref="Enum"/>.</typeparam>
    /// <exception cref="ArgumentOutOfRangeException">If the specified value is not defined by the specified <see cref="Enum"/> type.</exception>
    public static void RequireIsDefined<T>(T value, string? parameterName = null) where T : struct, Enum
    {
        if (Enum.IsDefined(value)) return;

        string message = $"Invalid {typeof(T).Name} enum value: {value}. Valid values include: {Enum.GetNames<T>().JoinToString()}.";
        throw new ArgumentOutOfRangeException(parameterName, message);
    }
}
