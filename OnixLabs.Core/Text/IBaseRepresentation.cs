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
using System.Text;

namespace OnixLabs.Core.Text;

/// <summary>
/// Defines a number base representation.
/// </summary>
public interface IBaseRepresentation : IBinaryConvertible, ISpanFormattable
{
    /// <summary>
    /// Gets the plain-text <see cref="string"/> representation of the current <see typeparam="IBaseRepresentation"/> value, using the default <see cref="Encoding"/>.
    /// </summary>
    /// <returns>Returns the plain-text <see cref="string"/> representation of the current <see typeparam="IBaseRepresentation"/> value, using the default <see cref="Encoding"/>.</returns>
    public string ToPlainTextString();

    /// <summary>
    /// Gets the plain-text <see cref="string"/> representation of the current <see typeparam="IBaseRepresentation"/> value.
    /// </summary>
    /// <param name="encoding">The <see cref="Encoding"/> which will be used to obtain a <see cref="string"/> from the current <see typeparam="TSelf"/> value.</param>
    /// <returns>Returns the plain-text <see cref="string"/> representation of the current <see typeparam="IBaseRepresentation"/> value.</returns>
    public string ToPlainTextString(Encoding encoding);

    /// <summary>
    /// Formats the value of the current instance using the specified format.
    /// </summary>
    /// <param name="format">The format to use.</param>
    /// <param name="formatProvider">The provider to use to format the value.</param>
    /// <returns>The value of the current instance in the specified format.</returns>
    public string ToString(ReadOnlySpan<char> format, IFormatProvider? formatProvider);
}
