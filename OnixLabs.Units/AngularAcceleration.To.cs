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
using System.Globalization;

namespace OnixLabs.Units;

public readonly partial struct AngularAcceleration<T>
{
    /// <inheritdoc/>
    public override string ToString() => ToString(DefaultFormat);

    /// <inheritdoc/>
    public string ToString(string? format, IFormatProvider? formatProvider) =>
        ToString(format.AsSpan(), formatProvider);

    /// <summary>
    /// Formats the value using <c>&lt;angular-velocity&gt;/&lt;time&gt;[:scale]</c> (e.g. <c>rad/s/s:3</c>).
    /// </summary>
    public string ToString(ReadOnlySpan<char> format, IFormatProvider? formatProvider = null)
    {
        if (format.IsEmpty) format = DefaultFormat.AsSpan();

        int colonIndex = format.IndexOf(':');
        ReadOnlySpan<char> unitPart;
        int scale;

        if (colonIndex < 0)
        {
            unitPart = format;
            scale = (formatProvider as CultureInfo ?? CultureInfo.CurrentCulture).NumberFormat.NumberDecimalDigits;
        }
        else
        {
            unitPart = format[..colonIndex];
            if (!int.TryParse(format[(colonIndex + 1)..], NumberStyles.Integer, CultureInfo.InvariantCulture, out scale))
                throw new FormatException("AngularAcceleration format scale must contain only decimal digits.");
        }

        // AngularVelocity-spec contains '/', time-spec does not. Split on last '/'.
        int lastSlash = unitPart.LastIndexOf('/');
        if (lastSlash < 0)
            throw new FormatException($"AngularAcceleration format must contain a '/' separator between angular-velocity and time (e.g. '{DefaultFormat}').");

        ReadOnlySpan<char> angularVelocitySpecifier = unitPart[..lastSlash];
        ReadOnlySpan<char> timeSpecifier = unitPart[(lastSlash + 1)..];

        if (angularVelocitySpecifier.IsEmpty)
            throw new FormatException("AngularAcceleration format angular-velocity specifier must not be empty.");

        if (timeSpecifier.IsEmpty)
            throw new FormatException("AngularAcceleration format time specifier must not be empty.");

        T value = Left.ValueOf(angularVelocitySpecifier) / Right.ValueOf(timeSpecifier);
        T rounded = scale > 0 ? T.Round(value, scale) : value;

        return $"{rounded.ToString($"N{scale}", formatProvider ?? CultureInfo.CurrentCulture)} "
               + $"{new string(angularVelocitySpecifier)}/{new string(timeSpecifier)}";
    }
}
