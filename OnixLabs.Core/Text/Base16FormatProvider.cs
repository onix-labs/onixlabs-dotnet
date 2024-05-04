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
/// Represents a Base-16 format provider.
/// </summary>
public sealed class Base16FormatProvider : Enumeration<Base16FormatProvider>, IFormatProvider
{
    /// <summary>
    /// Gets the invariant Base-16 format provider.
    /// The invariant format provider favours lowercase for Base-16 encoding.
    /// The alphabet provided by this format provider is not a strict Base-16 alphabet as it contains all uppercase and lowercase values.
    /// </summary>
    public static readonly Base16FormatProvider Invariant = new(0, nameof(Invariant), "0123456789ABCDEFabcdef");

    /// <summary>
    /// Gets the uppercase Base-16 format provider.
    /// </summary>
    public static readonly Base16FormatProvider Uppercase = new(1, nameof(Uppercase), "0123456789ABCDEF");

    /// <summary>
    /// Gets the lowercase Base-16 format provider.
    /// </summary>
    public static readonly Base16FormatProvider Lowercase = new(2, nameof(Lowercase), "0123456789abcdef");

    /// <summary>
    /// Initializes a new instance of the <see cref="Base16FormatProvider"/> class.
    /// </summary>
    /// <param name="value">The value of the enumeration entry.</param>
    /// <param name="name">The name of the enumeration entry.</param>
    /// <param name="alphabet">The alphabet of the format provider.</param>
    private Base16FormatProvider(int value, string name, string alphabet) : base(value, name)
    {
        Alphabet = alphabet;
    }

    /// <summary>
    /// Gets the alphabet of the current <see cref="Base16FormatProvider"/> instance.
    /// </summary>
    public string Alphabet { get; }

    /// <summary>Gets an object that provides formatting services for the specified type.</summary>
    /// <param name="formatType">An object that specifies the type of format object to return.</param>
    /// <returns>
    /// Returns an instance of the object specified by <paramref name="formatType"/>,
    /// if the <see cref="T:System.IFormatProvider"/> implementation can supply that type of object; otherwise, <see langword="null"/>.
    /// </returns>
    public object? GetFormat(Type? formatType)
    {
        throw new NotImplementedException();
    }
}
