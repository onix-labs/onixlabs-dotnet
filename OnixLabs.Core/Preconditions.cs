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
using OnixLabs.Core.Linq;

namespace OnixLabs.Core;

/// <summary>
/// Provides general methods for performing pre-conditions and guard clauses.
/// </summary>
public static class Preconditions
{
    /// <summary>
    /// Performs a general pre-condition check that fails when the specified condition is <see langword="false"/>.
    /// </summary>
    /// <param name="condition">The condition to check.</param>
    /// <param name="message">The exception message to throw in the event that the specified condition is <see langword="false"/>.</param>
    /// <exception cref="InvalidOperationException">If the specified condition is <see langword="false"/>.</exception>
    public static void Check(bool condition, string message = "Check failed.")
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
    public static T CheckNotNull<T>(T? value, string message = "Argument must not be null.") where T : notnull
    {
        return value ?? throw new InvalidOperationException(message);
    }

    /// <summary>
    /// Performs a general pre-condition requirement that fails when the specified condition is <see langword="false"/>.
    /// </summary>
    /// <param name="condition">The condition to check.</param>
    /// <param name="message">The exception message to throw in the event that the specified condition is <see langword="false"/>.</param>
    /// <param name="parameterName">The name of the invalid paramter.</param>
    /// <exception cref="ArgumentException">If the specified condition is <see langword="false"/>.</exception>
    public static void Require(bool condition, string message = "Argument requirement failed.", string? parameterName = null)
    {
        if (!condition) throw new ArgumentException(message, parameterName);
    }

    /// <summary>
    /// Performs a general pre-condition requirement that fails when the specified condition is <see langword="false"/>.
    /// </summary>
    /// <param name="condition">The condition to check.</param>
    /// <param name="message">The exception message to throw in the event that the specified condition is <see langword="false"/>.</param>
    /// <param name="parameterName">The name of the invalid paramter.</param>
    /// <exception cref="ArgumentOutOfRangeException">If the specified condition is <see langword="false"/>.</exception>
    public static void RequireWithinRange(bool condition, string message = "Argument is out of range.", string? parameterName = null)
    {
        if (!condition) throw new ArgumentOutOfRangeException(parameterName, message);
    }

    /// <summary>
    /// Performs a general pre-condition requirement that fails if the specified value is <see langword="null"/>.
    /// </summary>
    /// <param name="value">The nullable value to check.</param>
    /// <param name="message">The exception message to throw in the event that the specified value is <see langword="null"/>.</param>
    /// <param name="parameterName">The name of the invalid parameter.</param>
    /// <typeparam name="T">The underlying type of the value.</typeparam>
    /// <returns>Returns a non-null value of the specified type.</returns>
    /// <exception cref="InvalidOperationException">If the specified value is <see langword="null"/>.</exception>
    public static T RequireNotNull<T>(T? value, string message = "Argument must not be null.", string? parameterName = null) where T : notnull
    {
        return value ?? throw new ArgumentNullException(parameterName, message);
    }

    /// <summary>
    /// Performs a general pre-condition requirement that the specified value is defined by the specified <see cref="Enum"/> type.
    /// </summary>
    /// <param name="value">The enum value to check.</param>
    /// <param name="parameterName">The name of the invalid parameter.</param>
    /// <typeparam name="T">The underlying type of the <see cref="Enum"/>.</typeparam>
    public static void RequireIsDefined<T>(T value, string? parameterName = null) where T : struct, Enum
    {
        if (Enum.IsDefined(value)) return;

        string message = $"Invalid {typeof(T).Name} enum value: {value}. Valid values include: {Enum.GetNames<T>().JoinToString()}.";
        throw new ArgumentOutOfRangeException(parameterName, message);
    }
}
