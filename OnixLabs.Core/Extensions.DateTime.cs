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
using System.ComponentModel;

namespace OnixLabs.Core;

/// <summary>
/// Provides extension methods for <see cref="DateTime"/> instances.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public static class DateTimeExtensions
{
    /// <summary>
    /// Obtains a <see cref="DateOnly"/> instance from the current <see cref="DateTime"/> instance.
    /// </summary>
    /// <param name="value">The <see cref="DateTime"/> instance from which to obtain a <see cref="DateOnly"/> instance.</param>
    /// <returns>Returns the <see cref="DateOnly"/> instance obtained from the current <see cref="DateTime"/> instance.</returns>
    public static DateOnly ToDateOnly(this DateTime value) => DateOnly.FromDateTime(value);

    /// <summary>
    /// Obtains a <see cref="TimeOnly"/> instance from the current <see cref="TimeOnly"/> instance.
    /// </summary>
    /// <param name="value">The <see cref="DateTime"/> instance from which to obtain a <see cref="TimeOnly"/> instance.</param>
    /// <returns>Returns the <see cref="TimeOnly"/> instance obtained from the current <see cref="DateTime"/> instance.</returns>
    public static TimeOnly ToTimeOnly(this DateTime value) => TimeOnly.FromDateTime(value);
}
