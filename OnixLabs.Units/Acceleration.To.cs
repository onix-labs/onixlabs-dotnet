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

public readonly partial struct Acceleration<T>
{
    /// <inheritdoc/>
    public override string ToString() => ToString(DefaultFormat);

    /// <inheritdoc/>
    public string ToString(string? format, IFormatProvider? formatProvider) =>
        ToString(format.AsSpan(), formatProvider);

    /// <summary>
    /// Formats the value of the current instance using the specified format.
    /// </summary>
    /// <param name="format">
    /// The format to use. Two forms are accepted:
    /// <list type="bullet">
    /// <item><description>
    /// <b>Squared form</b> — <c>&lt;distance&gt;/&lt;time&gt;²[:scale]</c> (for example <c>m/s²</c> or <c>m/s2:3</c>).
    /// The trailing <c>²</c> may be written as either the Unicode superscript-two (<c>²</c>) or the ASCII digit
    /// <c>2</c>. The same time unit is used both inside the implicit speed and as the outer time interval.
    /// </description></item>
    /// <item><description>
    /// <b>Compound form</b> — <c>&lt;speed&gt;/&lt;time&gt;[:scale]</c> where <c>&lt;speed&gt;</c> itself takes the
    /// composite shape <c>&lt;distance&gt;/&lt;time&gt;</c> (for example <c>km/h/s:3</c> or <c>mi/h/min:0</c>). The
    /// inner speed's time unit can differ from the outer time unit.
    /// </description></item>
    /// </list>
    /// The rendered output preserves whichever form the caller used, except the squared form always renders the marker
    /// as <c>²</c>.
    /// </param>
    /// <param name="formatProvider">The provider to use to format the value.</param>
    /// <returns>Returns the value of the current instance in the specified format.</returns>
    public string ToString(ReadOnlySpan<char> format, IFormatProvider? formatProvider = null)
    {
        if (format.IsEmpty) format = DefaultFormat.AsSpan();

        int colonIndex = format.IndexOf(':');
        ReadOnlySpan<char> unitPart;
        int scale;

        if (colonIndex < 0)
        {
            unitPart = format;
            scale = (formatProvider as CultureInfo ?? CultureInfo.CurrentCulture)
                .NumberFormat.NumberDecimalDigits;
        }
        else
        {
            unitPart = format[..colonIndex];
            if (!int.TryParse(format[(colonIndex + 1)..], NumberStyles.Integer, CultureInfo.InvariantCulture, out scale))
                throw new FormatException("Acceleration format scale must contain only decimal digits.");
        }

        if (unitPart.IsEmpty)
            throw new FormatException($"Acceleration format unit part must not be empty (e.g. '{DefaultFormat}').");

        // Detect squared form: trailing '²' (U+00B2) or ASCII '2'. Neither distance nor time specifiers in this library
        // end with '2', so the discriminator is unambiguous. When squared, the unit part is interpreted as a complete
        // "distance/time" pair which doubles as both the speed-spec and the source of the outer time-spec.
        bool isSquaredForm = unitPart[^1] == '²' || unitPart[^1] == '2';
        if (isSquaredForm) unitPart = unitPart[..^1];

        if (unitPart.IsEmpty)
            throw new FormatException("Acceleration format unit part must contain a unit before the squared marker.");

        ReadOnlySpan<char> speedSpecifier;
        ReadOnlySpan<char> timeSpecifier;

        if (isSquaredForm)
        {
            // unitPart looks like "distance/time". The speed-spec is the whole thing; the outer time-spec is the part
            // after the only '/'.
            int slashIndex = unitPart.IndexOf('/');
            if (slashIndex < 0)
                throw new FormatException($"Acceleration squared format must contain '/' between distance and time (e.g. '{DefaultFormat}').");

            speedSpecifier = unitPart;
            timeSpecifier = unitPart[(slashIndex + 1)..];
        }
        else
        {
            // unitPart looks like "<speed-spec>/<time-spec>" where speed-spec itself contains a '/'. Split on the LAST
            // '/' — everything before is the inner speed-spec, everything after is the outer time-spec.
            int lastSlash = unitPart.LastIndexOf('/');
            if (lastSlash < 0)
                throw new FormatException($"Acceleration compound format must contain a '/' separator between speed and time (e.g. 'km/h/s').");

            speedSpecifier = unitPart[..lastSlash];
            timeSpecifier = unitPart[(lastSlash + 1)..];
        }

        if (speedSpecifier.IsEmpty)
            throw new FormatException("Acceleration format speed specifier must not be empty.");

        if (timeSpecifier.IsEmpty)
            throw new FormatException("Acceleration format time specifier must not be empty.");

        T value = Left.ValueOf(speedSpecifier) / Right.ValueOf(timeSpecifier);
        T rounded = scale > 0 ? T.Round(value, scale) : value;

        string formatted = rounded.ToString($"N{scale}", formatProvider ?? CultureInfo.CurrentCulture);

        return isSquaredForm
            ? $"{formatted} {new string(speedSpecifier)}²"
            : $"{formatted} {new string(speedSpecifier)}/{new string(timeSpecifier)}";
    }
}
