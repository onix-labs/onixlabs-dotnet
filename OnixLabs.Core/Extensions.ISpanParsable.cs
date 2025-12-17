// Copyright 2020-2025 ONIXLabs
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

namespace OnixLabs.Core;

/// <summary>
/// Provides extension methods for <see cref="ISpanParsable{TSelf}"/> instances.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public static class ISpanParsableExtensions
{
    /// <summary>
    /// Provides extension methods for <see cref="ISpanParsable{TSelf}"/> instances.
    /// </summary>
    /// <typeparam name="T">The underlying <see cref="ISpanParsable{TSelf}"/> type.</typeparam>
    extension<T>(ISpanParsable<T>) where T : ISpanParsable<T>
    {
        /// <summary>
        /// Tries to parse the specified <paramref name="value"/>, or throws a <see cref="FormatException"/> if the value cannot be parsed.
        /// </summary>
        /// <param name="value">The value to parse.</param>
        /// <param name="provider">An object that provides culture-specific formatting information about the value.</param>
        /// <returns>Returns a new instance of <typeparamref name="T"/> parsed from the specified value.</returns>
        /// <exception cref="FormatException">If the value cannot be parsed.</exception>
        public static T ParseOrThrow(ReadOnlySpan<char> value, IFormatProvider? provider = null)
        {
            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (T.TryParse(value, provider, out T? result)) return result;
            throw new FormatException($"The input string '{value}' was not in a correct format.");
        }
    }
}
