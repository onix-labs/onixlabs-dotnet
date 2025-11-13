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

namespace OnixLabs.Core;

/// <summary>
/// Provides extension methods for <see cref="DateTime"/> instances.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public static class DateTimeExtensions
{
    /// <summary>
    /// Provides extension methods for <see cref="DateTime"/> instances.
    /// </summary>
    /// <param name="receiver">The current <see cref="DateTime"/> instance.</param>
    extension(DateTime receiver)
    {
        /// <summary>
        /// Obtains a <see cref="DateOnly"/> instance from the current <see cref="DateTime"/> instance.
        /// </summary>
        /// <returns>Returns the <see cref="DateOnly"/> instance obtained from the current <see cref="DateTime"/> instance.</returns>
        public DateOnly ToDateOnly() => DateOnly.FromDateTime(receiver);

        /// <summary>
        /// Obtains a <see cref="TimeOnly"/> instance from the current <see cref="TimeOnly"/> instance.
        /// </summary>
        /// <returns>Returns the <see cref="TimeOnly"/> instance obtained from the current <see cref="DateTime"/> instance.</returns>
        public TimeOnly ToTimeOnly() => TimeOnly.FromDateTime(receiver);
    }
}
