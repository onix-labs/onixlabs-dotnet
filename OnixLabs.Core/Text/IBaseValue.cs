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

namespace OnixLabs.Core.Text;

/// <summary>
/// Defines a Base-N value.
/// </summary>
public interface IBaseValue : ISpanBinaryConvertible, ISpanFormattable
{
    /// <summary>
    /// Formats the value of the current instance using the specified format.
    /// </summary>
    /// <param name="formatProvider">The provider to use to format the value.</param>
    /// <returns>The value of the current instance in the specified format.</returns>
    string ToString(IFormatProvider? formatProvider);

    /// <summary>
    /// Formats the value of the current instance using the specified format.
    /// </summary>
    /// <param name="format">The format to use.</param>
    /// <param name="formatProvider">The provider to use to format the value.</param>
    /// <returns>The value of the current instance in the specified format.</returns>
    string ToString(ReadOnlySpan<char> format, IFormatProvider? formatProvider);
}

/// <summary>
/// Defines a generic base encoding representation.
/// </summary>
public interface IBaseValue<T> : IValueEquatable<T>, ISpanParsable<T>, IBaseValue where T : struct, IBaseValue<T>;
