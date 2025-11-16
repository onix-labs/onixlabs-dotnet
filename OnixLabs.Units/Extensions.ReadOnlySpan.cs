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

internal static class ReadOnlySpanExtensions
{
    extension(ReadOnlySpan<char> format)
    {
        public (string specifier, int scale) GetSpecifierAndScale(string defaultSpecifier)
        {
            const char plus = '+';
            const char minus = '-';

            int defaultScale = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalDigits;

            if (format.IsEmpty)
                return (defaultSpecifier, defaultScale);

            int index = 0;

            while (index < format.Length && char.IsLetter(format[index]))
                index++;

            if (index == 0)
                throw new FormatException("Format must start with a letter specifier.");

            string specifier = new(format[..index]);
            ReadOnlySpan<char> scaleCharacters = format[index..];

            if (scaleCharacters.IsEmpty)
                return (specifier, defaultScale);

            if (scaleCharacters[0] is plus or minus)
                throw new FormatException($"Scale must not begin with a leading '{plus}' or '{minus}' sign.");

            return int.TryParse(scaleCharacters, NumberStyles.None, CultureInfo.InvariantCulture, out int scale)
                ? (specifier, scale)
                : throw new FormatException("Scale must contain only decimal digits.");
        }
    }
}
