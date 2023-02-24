// Copyright 2020-2023 ONIXLabs
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

namespace OnixLabs.Core.Numerics;

public readonly partial struct BigDecimal
{
    /// <summary>
    /// Returns a <see cref="byte"/> array that represents the current object.
    /// </summary>
    /// <returns>Returns a <see cref="byte"/> array that represents the current object.</returns>
    public byte[] ToByteArray()
    {
        byte[] unscaledValue = UnscaledValue.ToByteArray();
        byte[] scale = BitConverter.GetBytes(Scale);

        return unscaledValue.ConcatenateWith(scale);
    }

    /// <summary>
    /// Returns a <see cref="string"/> that represents the current object.
    /// </summary>
    /// <returns>A <see cref="string"/> that represents the current object.</returns>
    public override string ToString()
    {
        string sign = Sign < 0 ? "-" : string.Empty;
        string separator = CurrentCultureNumberFormat.NumberDecimalSeparator;
        string integralValue = AbsoluteIntegralValue.ToString();
        string fractionalValue = FractionalValue.ToString().PadLeft(Scale, '0');

        return Scale == 0
            ? $"{sign}{integralValue}"
            : $"{sign}{integralValue}{separator}{fractionalValue}";
    }

    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        // http://www.independent-software.com/net-string-formatting-in-csharp-cheat-sheet.html
        throw new NotImplementedException();
    }
}
