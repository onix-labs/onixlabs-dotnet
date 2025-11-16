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
/// Represents a Base-64 format provider.
/// </summary>
// ReSharper disable HeapView.ObjectAllocation.Evident StringLiteralTypo
public sealed class Base64FormatProvider : Enumeration<Base64FormatProvider>, IFormatProvider
{
    /// <summary>
    /// Gets the RFC 4648 Base-64 format provider.
    /// </summary>
    public static readonly Base64FormatProvider Rfc4648 = new(0, nameof(Rfc4648), "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=");

    /// <summary>
    /// Initializes a new instance of the <see cref="Base64FormatProvider"/> class.
    /// </summary>
    /// <param name="value">The value of the enumeration entry.</param>
    /// <param name="name">The name of the enumeration entry.</param>
    /// <param name="alphabet">The alphabet of the format provider.</param>
    private Base64FormatProvider(int value, string name, string alphabet) : base(value, name) => Alphabet = alphabet;

    /// <summary>
    /// Gets the alphabet of the current <see cref="Base64FormatProvider"/> instance.
    /// </summary>
    public string Alphabet { get; }

    /// <summary>Gets an object that provides formatting services for the specified type.</summary>
    /// <param name="formatType">An object that specifies the type of format object to return.</param>
    /// <returns>
    /// Returns an instance of the object specified by <paramref name="formatType"/>,
    /// if the <see cref="T:IFormatProvider"/> implementation can supply that type of object; otherwise, <see langword="null"/>.
    /// </returns>
    public object? GetFormat(Type? formatType) => formatType == typeof(Base64FormatProvider) ? this : null;
}
