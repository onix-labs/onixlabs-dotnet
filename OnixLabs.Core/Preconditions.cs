// Copyright 2020-2022 ONIXLabs
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

namespace OnixLabs.Core;

/// <summary>
/// Provides methods for performing pre-conditions and guard clauses.
/// </summary>
public static class Preconditions
{
    /// <summary>
    /// Performs a general pre-condition check, which fails if the condition returns false.
    /// </summary>
    /// <param name="value">The condition to check.</param>
    /// <param name="message">The exception message to throw in the event that the condition fails.</param>
    /// <param name="paramName">The name of the parameter which is invalid.</param>
    /// <exception cref="ArgumentException">If the condition fails.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static void Check(bool value, string message = "Argument check failed.", string? paramName = null)
    {
        if (!value)
        {
            throw new ArgumentException(message, paramName);
        }
    }

    /// <summary>
    /// Performs a general pre-condition check that the specified value is not null, which fails if the value is null.
    /// </summary>
    /// <param name="value">The nullable value to check.</param>
    /// <param name="message">The exception message to throw in the event that the condition fails.</param>
    /// <typeparam name="T">The underlying type of the value to check.</typeparam>
    /// <returns>Returns the specified value as non-nullable in the event that the value is not null.</returns>
    /// <exception cref="ArgumentNullException">If the condition fails and the value is null.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T CheckNotNull<T>(T? value, string message = "Argument null checked failed.") where T : notnull
    {
        if (value is null)
        {
            throw new ArgumentNullException(message);
        }

        return value;
    }
}
