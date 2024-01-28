// Copyright © 2020 ONIXLabs
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
using System.Runtime.CompilerServices;
using OnixLabs.Core.Linq;

namespace OnixLabs.Core;

/// <summary>
/// Provides methods for performing pre-conditions and guard clauses.
/// </summary>
public static class Preconditions
{
    /// <summary>
    /// Performs a general pre-condition check, which fails if the condition returns false.
    /// </summary>
    /// <param name="condition">The condition to check.</param>
    /// <param name="message">The exception message to throw in the event that the condition fails.</param>
    /// <exception cref="InvalidOperationException">If the condition fails.</exception>
    public static void Check(bool condition, string message = "Check requirement failed.")
    {
        if (!condition) throw new InvalidOperationException(message);
    }

    /// <summary>
    /// Performs a general pre-condition requirement that the specified value is not null, which fails if the value is null.
    /// </summary>
    /// <param name="value">The nullable value to check.</param>
    /// <param name="message">The exception message to throw in the event that the condition fails.</param>
    /// <typeparam name="T">The underlying type of the value.</typeparam>
    /// <returns>Returns the specified value as non-nullable in the event that the value is not null.</returns>
    /// <exception cref="InvalidOperationException">If the condition fails because the value is null.</exception>
    public static T CheckNotNull<T>(T? value, string message = "Argument must not be null.") where T : notnull
    {
        return value ?? throw new InvalidOperationException(message);
    }

    /// <summary>
    /// Performs a general pre-condition requirement, which fails if the condition returns false.
    /// </summary>
    /// <param name="condition">The condition of the requirement.</param>
    /// <param name="message">The exception message to throw in the event that the condition fails.</param>
    /// <param name="parameterName">The name of the parameter which is invalid.</param>
    /// <exception cref="ArgumentException">If the condition fails.</exception>
    public static void Require(bool condition, string message = "Argument requirement failed.", string? parameterName = null)
    {
        if (!condition) throw new ArgumentException(message, parameterName);
    }

    /// <summary>
    /// Performs a general pre-condition requirement that the specified value is not null, which fails if the value is null.
    /// </summary>
    /// <param name="value">The nullable value to check.</param>
    /// <param name="message">The exception message to throw in the event that the condition fails.</param>
    /// <param name="parameterName">The name of the parameter which is invalid.</param>
    /// <typeparam name="T">The underlying type of the value.</typeparam>
    /// <returns>Returns the specified value as non-nullable in the event that the value is not null.</returns>
    /// <exception cref="ArgumentNullException">If the condition fails because the value is null.</exception>
    public static T RequireNotNull<T>(T? value, string message = "Argument must not be null.", string? parameterName = null)
        where T : notnull
    {
        return value ?? throw new ArgumentNullException(parameterName, message);
    }

    /// <summary>
    /// Performs a pre-condition requirement that the specified enum value is defined in the enum.
    /// </summary>
    /// <param name="value">The enum value to check.</param>
    /// <param name="parameterName">The name of the parameter being checked.</param>
    /// <typeparam name="T">The underlying type of the enum value.</typeparam>
    /// <returns>Returns the specified enum value if it is defined in the enum.</returns>
    /// <exception cref="ArgumentOutOfRangeException">If the specified value does not exist in the enum.</exception>
    public static void RequireIsDefined<T>(T value, string? parameterName = null) where T : struct, Enum
    {
        if (Enum.IsDefined(value)) return;

        string message = $"Invalid {typeof(T).Name} enum value: {value}. Valid values include: {Enum.GetNames<T>().JoinToString()}.";
        throw new ArgumentOutOfRangeException(parameterName, message);
    }
}
