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
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using OnixLabs.Core.Linq;

namespace OnixLabs.Core;

/// <summary>
/// Provides general methods for performing pre-conditions and guard clauses.
/// </summary>
public static class Preconditions
{
    private const string ArgumentFailed = "Argument must satisfy the specified condition.";
    private const string ArgumentIsNotFailure = "Argument must be a Failure state.";
    private const string ArgumentIsNotSuccess = "Argument must be a Success state.";
    private const string ArgumentIsNotNone = "Argument must be a None<T> value.";
    private const string ArgumentIsNotSome = "Argument must be a Some<T> value.";
    private const string ArgumentNull = "Argument must not be null.";
    private const string ArgumentNullOrEmpty = "Argument must not be null or empty.";
    private const string ArgumentNullOrWhiteSpace = "Argument must not be null or whitespace.";
    private const string ArgumentOutOfRange = "Argument must be within range.";

    /// <summary>
    /// Provides <see cref="InvalidOperationException"/> with static extension methods for performing pre-conditions and guard clauses.
    /// </summary>
    extension(InvalidOperationException)
    {
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
        /// Performs a general pre-condition check that fails when the specified <see cref="Result"/> is not of type <see cref="Failure"/>.
        /// </summary>
        /// <param name="result">The current <see cref="Result"/> to check.</param>
        /// <param name="message">The exception message to throw in the event that the specified <see cref="Result"/> is not of type <see cref="Failure"/>.</param>
        /// <returns>Returns the current <see cref="Result"/> as a <see cref="Failure"/> instance.</returns>
        /// <exception cref="InvalidOperationException">If the specified <see cref="Result"/> is not of type <see cref="Failure"/>.</exception>
        public static Failure CheckIsFailure(Result result, string message = ArgumentIsNotFailure) =>
            result as Failure ?? throw new InvalidOperationException(message);

        /// <summary>
        /// Performs a general pre-condition check that fails when the specified <see cref="Result"/> is not of type <see cref="Success"/>.
        /// </summary>
        /// <param name="result">The current <see cref="Result"/> to check.</param>
        /// <param name="message">The exception message to throw in the event that the specified <see cref="Result"/> is not of type <see cref="Success"/>.</param>
        /// <returns>Returns the current <see cref="Result"/> as a <see cref="Success"/> instance.</returns>
        /// <exception cref="InvalidOperationException">If the specified <see cref="Result"/> is not of type <see cref="Success"/>.</exception>
        public static Success CheckIsSuccess(Result result, string message = ArgumentIsNotSuccess) =>
            result as Success ?? throw new InvalidOperationException(message);

        /// <summary>
        /// Performs a general pre-condition check that fails when the specified <see cref="Result{T}"/> is not of type <see cref="Failure{T}"/>.
        /// </summary>
        /// <param name="result">The current <see cref="Result{T}"/> to check.</param>
        /// <param name="message">The exception message to throw in the event that the specified <see cref="Result{T}"/> is not of type <see cref="Failure{T}"/>.</param>
        /// <typeparam name="T">The underlying type of the current <see cref="Result{T}"/> instance.</typeparam>
        /// <returns>Returns the current <see cref="Result{T}"/> as a <see cref="Failure{T}"/> instance.</returns>
        /// <exception cref="InvalidOperationException">If the specified <see cref="Result{T}"/> is not of type <see cref="Failure{T}"/>.</exception>
        public static Failure<T> CheckIsFailure<T>(Result<T> result, string message = ArgumentIsNotFailure) =>
            result as Failure<T> ?? throw new InvalidOperationException(message);

        /// <summary>
        /// Performs a general pre-condition check that fails when the specified <see cref="Result{T}"/> is not of type <see cref="Success{T}"/>.
        /// </summary>
        /// <param name="result">The current <see cref="Result{T}"/> to check.</param>
        /// <param name="message">The exception message to throw in the event that the specified <see cref="Result{T}"/> is not of type <see cref="Success{T}"/>.</param>
        /// <typeparam name="T">The underlying type of the current <see cref="Result{T}"/> instance.</typeparam>
        /// <returns>Returns the current <see cref="Result{T}"/> as a <see cref="Success{T}"/> instance.</returns>
        /// <exception cref="InvalidOperationException">If the specified <see cref="Result{T}"/> is not of type <see cref="Success{T}"/>.</exception>
        public static Success<T> CheckIsSuccess<T>(Result<T> result, string message = ArgumentIsNotSuccess) =>
            result as Success<T> ?? throw new InvalidOperationException(message);

        /// <summary>
        /// Performs a general pre-condition check that fails when the specified <see cref="Optional{T}"/> is not of type <see cref="None{T}"/>.
        /// </summary>
        /// <param name="optional">The current <see cref="Optional{T}"/> to check.</param>
        /// <param name="message">The exception message to throw in the event that the specified <see cref="Optional{T}"/> is not of type <see cref="None{T}"/>.</param>
        /// <typeparam name="T">The underlying type of the current <see cref="Optional{T}"/> instance.</typeparam>
        /// <returns>Returns the current <see cref="Optional{T}"/> as a <see cref="None{T}"/> instance.</returns>
        /// <exception cref="InvalidOperationException">If the specified <see cref="Optional{T}"/> is not of type <see cref="None{T}"/>.</exception>
        public static None<T> CheckIsNone<T>(Optional<T> optional, string message = ArgumentIsNotNone) where T : notnull =>
            optional as None<T> ?? throw new InvalidOperationException(message);

        /// <summary>
        /// Performs a general pre-condition check that fails when the specified <see cref="Optional{T}"/> is not of type <see cref="Some{T}"/>.
        /// </summary>
        /// <param name="optional">The current <see cref="Optional{T}"/> to check.</param>
        /// <param name="message">The exception message to throw in the event that the specified <see cref="Optional{T}"/> is not of type <see cref="Some{T}"/>.</param>
        /// <typeparam name="T">The underlying type of the current <see cref="Optional{T}"/> instance.</typeparam>
        /// <returns>Returns the current <see cref="Optional{T}"/> as a <see cref="Some{T}"/> instance.</returns>
        /// <exception cref="InvalidOperationException">If the specified <see cref="Optional{T}"/> is not of type <see cref="Some{T}"/>.</exception>
        public static Some<T> CheckIsSome<T>(Optional<T> optional, string message = ArgumentIsNotSome) where T : notnull =>
            optional as Some<T> ?? throw new InvalidOperationException(message);

        /// <summary>
        /// Performs a general pre-condition check that fails if the specified value is <see langword="null"/>.
        /// </summary>
        /// <param name="value">The nullable value to check.</param>
        /// <param name="message">The exception message to throw in the event that the specified value is <see langword="null"/>.</param>
        /// <typeparam name="T">The underlying type of the value.</typeparam>
        /// <returns>Returns a non-null value of the specified type.</returns>
        /// <exception cref="InvalidOperationException">If the specified value is <see langword="null"/>.</exception>
        public static T CheckNotNull<T>(T? value, string message = ArgumentNull) =>
            value ?? throw new InvalidOperationException(message);

        /// <summary>
        /// Performs a general pre-condition check that fails if the specified value is <see langword="null"/>.
        /// </summary>
        /// <param name="value">The nullable value to check.</param>
        /// <param name="message">The exception message to throw in the event that the specified value is <see langword="null"/>.</param>
        /// <typeparam name="T">The underlying type of the value.</typeparam>
        /// <returns>Returns a non-null value of the specified type.</returns>
        /// <exception cref="InvalidOperationException">If the specified value is <see langword="null"/>.</exception>
        public static T CheckNotNull<T>(T? value, string message = ArgumentNull) where T : struct =>
            value ?? throw new InvalidOperationException(message);

        /// <summary>
        /// Performs a general pre-condition check that fails if the specified value is <see langword="null"/> or an empty string.
        /// </summary>
        /// <param name="value">The <see cref="String"/> value to check.</param>
        /// <param name="message">The exception message to throw in the event that the specified value is <see langword="null"/> or an empty string.</param>
        /// <returns>Returns a non-null, non-empty <see cref="String"/> value.</returns>
        /// <exception cref="InvalidOperationException">If the specified value is <see langword="null"/> or an empty string.</exception>
        public static string CheckNotNullOrEmpty(string? value, string message = ArgumentNullOrEmpty) =>
            !string.IsNullOrEmpty(value) ? value : throw new InvalidOperationException(message);

        /// <summary>
        /// Performs a general pre-condition check that fails if the specified value is <see langword="null"/> or a whitespace string.
        /// </summary>
        /// <param name="value">The <see cref="String"/> value to check.</param>
        /// <param name="message">The exception message to throw in the event that the specified value is <see langword="null"/> or a whitespace string.</param>
        /// <returns>Returns a non-null, non-empty <see cref="String"/> value.</returns>
        /// <exception cref="InvalidOperationException">If the specified value is <see langword="null"/> or a whitespace string.</exception>
        public static string CheckNotNullOrWhiteSpace(string? value, string message = ArgumentNullOrWhiteSpace) =>
            !string.IsNullOrWhiteSpace(value) ? value : throw new InvalidOperationException(message);
    }

    /// <summary>
    /// Provides <see cref="ArgumentException"/> with static extension methods for performing pre-conditions and guard clauses.
    /// </summary>
    extension(ArgumentException)
    {
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
        /// Performs a general pre-condition check that fails when the specified <see cref="Result"/> is not of type <see cref="Failure"/>.
        /// </summary>
        /// <param name="result">The current <see cref="Result"/> to check.</param>
        /// <param name="message">The exception message to throw in the event that the specified <see cref="Result"/> is not of type <see cref="Failure"/>.</param>
        /// <param name="parameterName">The name of the invalid parameter.</param>
        /// <returns>Returns the current <see cref="Result"/> as a <see cref="Failure"/> instance.</returns>
        /// <exception cref="InvalidOperationException">If the specified <see cref="Result"/> is not of type <see cref="Failure"/>.</exception>
        public static Failure RequireIsFailure(
            Result result,
            string message = ArgumentIsNotFailure,
            [CallerArgumentExpression(nameof(result))]
            string? parameterName = null
        ) => result as Failure ?? throw new ArgumentException(message, parameterName);

        /// <summary>
        /// Performs a general pre-condition check that fails when the specified <see cref="Result"/> is not of type <see cref="Success"/>.
        /// </summary>
        /// <param name="result">The current <see cref="Result"/> to check.</param>
        /// <param name="message">The exception message to throw in the event that the specified <see cref="Result"/> is not of type <see cref="Success"/>.</param>
        /// <param name="parameterName">The name of the invalid parameter.</param>
        /// <returns>Returns the current <see cref="Result"/> as a <see cref="Success"/> instance.</returns>
        /// <exception cref="InvalidOperationException">If the specified <see cref="Result"/> is not of type <see cref="Success"/>.</exception>
        public static Success RequireIsSuccess(
            Result result,
            string message = ArgumentIsNotSuccess,
            [CallerArgumentExpression(nameof(result))]
            string? parameterName = null
        ) => result as Success ?? throw new ArgumentException(message, parameterName);

        /// <summary>
        /// Performs a general pre-condition check that fails when the specified <see cref="Result{T}"/> is not of type <see cref="Failure{T}"/>.
        /// </summary>
        /// <param name="result">The current <see cref="Result{T}"/> to check.</param>
        /// <param name="message">The exception message to throw in the event that the specified <see cref="Result{T}"/> is not of type <see cref="Failure{T}"/>.</param>
        /// <param name="parameterName">The name of the invalid parameter.</param>
        /// <typeparam name="T">The underlying type of the current <see cref="Result{T}"/> instance.</typeparam>
        /// <returns>Returns the current <see cref="Result{T}"/> as a <see cref="Failure{T}"/> instance.</returns>
        /// <exception cref="InvalidOperationException">If the specified <see cref="Result{T}"/> is not of type <see cref="Failure{T}"/>.</exception>
        public static Failure<T> RequireIsFailure<T>(
            Result<T> result,
            string message = ArgumentIsNotFailure,
            [CallerArgumentExpression(nameof(result))]
            string? parameterName = null
        ) => result as Failure<T> ?? throw new ArgumentException(message, parameterName);

        /// <summary>
        /// Performs a general pre-condition check that fails when the specified <see cref="Result{T}"/> is not of type <see cref="Success{T}"/>.
        /// </summary>
        /// <param name="result">The current <see cref="Result{T}"/> to check.</param>
        /// <param name="message">The exception message to throw in the event that the specified <see cref="Result{T}"/> is not of type <see cref="Success{T}"/>.</param>
        /// <param name="parameterName">The name of the invalid parameter.</param>
        /// <typeparam name="T">The underlying type of the current <see cref="Result{T}"/> instance.</typeparam>
        /// <returns>Returns the current <see cref="Result{T}"/> as a <see cref="Success{T}"/> instance.</returns>
        /// <exception cref="InvalidOperationException">If the specified <see cref="Result{T}"/> is not of type <see cref="Success{T}"/>.</exception>
        public static Success<T> RequireIsSuccess<T>(
            Result<T> result,
            string message = ArgumentIsNotSuccess,
            [CallerArgumentExpression(nameof(result))]
            string? parameterName = null
        ) => result as Success<T> ?? throw new ArgumentException(message, parameterName);

        /// <summary>
        /// Performs a general pre-condition check that fails when the specified <see cref="Optional{T}"/> is not of type <see cref="None{T}"/>.
        /// </summary>
        /// <param name="optional">The current <see cref="Optional{T}"/> to check.</param>
        /// <param name="message">The exception message to throw in the event that the specified <see cref="Optional{T}"/> is not of type <see cref="None{T}"/>.</param>
        /// <param name="parameterName">The name of the invalid parameter.</param>
        /// <typeparam name="T">The underlying type of the current <see cref="Optional{T}"/> instance.</typeparam>
        /// <returns>Returns the current <see cref="Optional{T}"/> as a <see cref="None{T}"/> instance.</returns>
        /// <exception cref="InvalidOperationException">If the specified <see cref="Optional{T}"/> is not of type <see cref="None{T}"/>.</exception>
        public static None<T> RequireIsNone<T>(
            Optional<T> optional,
            string message = ArgumentIsNotNone,
            [CallerArgumentExpression(nameof(optional))]
            string? parameterName = null
        ) where T : notnull => optional as None<T> ?? throw new ArgumentException(message, parameterName);

        /// <summary>
        /// Performs a general pre-condition check that fails when the specified <see cref="Optional{T}"/> is not of type <see cref="Some{T}"/>.
        /// </summary>
        /// <param name="optional">The current <see cref="Optional{T}"/> to check.</param>
        /// <param name="message">The exception message to throw in the event that the specified <see cref="Optional{T}"/> is not of type <see cref="Some{T}"/>.</param>
        /// <param name="parameterName">The name of the invalid parameter.</param>
        /// <typeparam name="T">The underlying type of the current <see cref="Optional{T}"/> instance.</typeparam>
        /// <returns>Returns the current <see cref="Optional{T}"/> as a <see cref="Some{T}"/> instance.</returns>
        /// <exception cref="InvalidOperationException">If the specified <see cref="Optional{T}"/> is not of type <see cref="Some{T}"/>.</exception>
        public static Some<T> RequireIsSome<T>(
            Optional<T> optional,
            string message = ArgumentIsNotSome,
            [CallerArgumentExpression(nameof(optional))]
            string? parameterName = null
        ) where T : notnull => optional as Some<T> ?? throw new ArgumentException(message, parameterName);

        /// <summary>
        /// Performs a general pre-condition requirement that fails if the specified value is <see langword="null"/> or an empty string.
        /// </summary>
        /// <param name="value">The <see cref="String"/> value to check.</param>
        /// <param name="message">The exception message to throw in the event that the specified value is <see langword="null"/> or an empty string.</param>
        /// <param name="parameterName">The name of the invalid parameter.</param>
        /// <returns>Returns a non-null, non-empty <see cref="String"/> value.</returns>
        /// <exception cref="ArgumentException">If the specified value is <see langword="null"/> or an empty string.</exception>
        public static string RequireNotNullOrEmpty(
            [NotNull] string? value,
            string message = ArgumentNullOrEmpty,
            [CallerArgumentExpression(nameof(value))]
            string? parameterName = null
        ) => !string.IsNullOrEmpty(value) ? value : throw new ArgumentException(message, parameterName);

        /// <summary>
        /// Performs a general pre-condition requirement that fails if the specified value is <see langword="null"/> or a whitespace string.
        /// </summary>
        /// <param name="value">The <see cref="String"/> value to check.</param>
        /// <param name="message">The exception message to throw in the event that the specified value is <see langword="null"/> or a whitespace string.</param>
        /// <param name="parameterName">The name of the invalid parameter.</param>
        /// <returns>Returns a non-null, non-empty <see cref="String"/> value.</returns>
        /// <exception cref="ArgumentException">If the specified value is <see langword="null"/> or a whitespace string.</exception>
        public static string RequireNotNullOrWhiteSpace(
            [NotNull] string? value,
            string message = ArgumentNullOrWhiteSpace,
            [CallerArgumentExpression(nameof(value))]
            string? parameterName = null
        ) => !string.IsNullOrWhiteSpace(value) ? value : throw new ArgumentException(message, parameterName);
    }

    /// <summary>
    /// Provides <see cref="ArgumentNullException"/> with static extension methods for performing pre-conditions and guard clauses.
    /// </summary>
    extension(ArgumentNullException)
    {
        /// <summary>
        /// Performs a general pre-condition requirement that fails if the specified value is <see langword="null"/>.
        /// </summary>
        /// <param name="value">The nullable value to check.</param>
        /// <param name="message">The exception message to throw in the event that the specified value is <see langword="null"/>.</param>
        /// <param name="parameterName">The name of the invalid parameter.</param>
        /// <typeparam name="T">The underlying type of the value.</typeparam>
        /// <returns>Returns a non-null value of the specified type.</returns>
        /// <exception cref="ArgumentNullException">If the specified value is <see langword="null"/>.</exception>
        public static T RequireNotNull<T>(
            [NotNull] T? value,
            string message = ArgumentNull,
            [CallerArgumentExpression(nameof(value))]
            string? parameterName = null
        ) => value ?? throw new ArgumentNullException(parameterName, message);

        /// <summary>
        /// Performs a general pre-condition requirement that fails if the specified value is <see langword="null"/>.
        /// </summary>
        /// <param name="value">The nullable value to check.</param>
        /// <param name="message">The exception message to throw in the event that the specified value is <see langword="null"/>.</param>
        /// <param name="parameterName">The name of the invalid parameter.</param>
        /// <typeparam name="T">The underlying type of the value.</typeparam>
        /// <returns>Returns a non-null value of the specified type.</returns>
        /// <exception cref="ArgumentNullException">If the specified value is <see langword="null"/>.</exception>
        public static T RequireNotNull<T>(
            [NotNull] T? value,
            string message = ArgumentNull,
            [CallerArgumentExpression(nameof(value))]
            string? parameterName = null
        ) where T : struct => value ?? throw new ArgumentNullException(parameterName, message);
    }

    /// <summary>
    /// Provides <see cref="ArgumentOutOfRangeException"/> with static extension methods for performing pre-conditions and guard clauses.
    /// </summary>
    extension(ArgumentOutOfRangeException)
    {
        /// <summary>
        /// Performs a general pre-condition requirement that fails when the specified value falls inclusively outside the specified minimum and maximum values.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="minimum">The inclusive minimum value to test.</param>
        /// <param name="maximum">The inclusive maximum value to test.</param>
        /// <param name="message">The exception message to throw in the event that the specified value falls inclusively outside the specified minimum and maximum values.</param>
        /// <param name="parameterName">The name of the invalid parameter.</param>
        /// <typeparam name="T">The underlying type of the value to check.</typeparam>
        /// <exception cref="ArgumentOutOfRangeException">If the specified value falls inclusively outside the specified minimum and maximum values.</exception>
        public static T RequireWithinRangeInclusive<T>(
            T value,
            T minimum,
            T maximum,
            string message = ArgumentOutOfRange,
            [CallerArgumentExpression(nameof(value))]
            string? parameterName = null
        ) where T : IComparable<T> => value.IsWithinRangeInclusive(minimum, maximum)
            ? value
            : throw new ArgumentOutOfRangeException(parameterName, message);

        /// <summary>
        /// Performs a general pre-condition requirement that fails when the specified value falls exclusively outside the specified minimum and maximum values.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="minimum">The exclusive minimum value to test.</param>
        /// <param name="maximum">The exclusive maximum value to test.</param>
        /// <param name="message">The exception message to throw in the event that the specified value falls exclusively outside the specified minimum and maximum values.</param>
        /// <param name="parameterName">The name of the invalid parameter.</param>
        /// <typeparam name="T">The underlying type of the value to check.</typeparam>
        /// <exception cref="ArgumentOutOfRangeException">If the specified value falls exclusively outside the specified minimum and maximum values.</exception>
        public static T RequireWithinRangeExclusive<T>(
            T value,
            T minimum,
            T maximum,
            string message = ArgumentOutOfRange,
            [CallerArgumentExpression(nameof(value))]
            string? parameterName = null
        ) where T : IComparable<T> => value.IsWithinRangeExclusive(minimum, maximum)
            ? value
            : throw new ArgumentOutOfRangeException(parameterName, message);

        /// <summary>
        /// Performs a general pre-condition requirement that the specified value is defined by the specified <see cref="Enum"/> type.
        /// </summary>
        /// <param name="value">The enum value to check.</param>
        /// <param name="parameterName">The name of the invalid parameter.</param>
        /// <typeparam name="T">The underlying type of the <see cref="Enum"/>.</typeparam>
        /// <exception cref="ArgumentOutOfRangeException">If the specified value is not defined by the specified <see cref="Enum"/> type.</exception>
        public static T RequireIsDefined<T>(
            T value,
            [CallerArgumentExpression(nameof(value))]
            string? parameterName = null
        ) where T : struct, Enum => Enum.IsDefined(value)
            ? value
            : throw new ArgumentOutOfRangeException(parameterName, $"Invalid {typeof(T).Name} enum value: {value}. Valid values include: {Enum.GetNames<T>().JoinToString()}.");
    }
}
