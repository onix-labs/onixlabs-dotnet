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
using System.Text;

namespace OnixLabs.Core.Numerics;

internal sealed partial class BigDecimalParser
{
    private string Sanitize(ReadOnlySpan<char> value)
    {
        ReadOnlySpan<char> slice = value.Trim();
        StringBuilder builder = new();

        while (slice.Length > 0)
        {
            if (CheckSliceStartsWith(ref slice, negativeSign, builder)) continue;
            if (CheckSliceStartsWith(ref slice, positiveSign, builder)) continue;
            if (CheckSliceStartsWith(ref slice, decimalPoint, builder)) continue;

            CheckSliceContainsHexDigit(ref slice, builder);
        }

        return builder.ToString();
    }

    private static bool CheckSliceStartsWith(ref ReadOnlySpan<char> slice, string value, StringBuilder builder)
    {
        bool result = slice.StartsWith(value);

        if (result)
        {
            builder.Append(value);
            slice = slice[value.Length..];
        }

        return result;
    }

    private static void CheckSliceContainsHexDigit(ref ReadOnlySpan<char> slice, StringBuilder builder)
    {
        if (char.IsAsciiHexDigit(slice[0]))
        {
            builder.Append(slice[0]);
        }

        slice = slice[1..];
    }
}
