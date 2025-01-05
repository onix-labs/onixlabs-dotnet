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
using System.Diagnostics.CodeAnalysis;

namespace OnixLabs.Security.Cryptography;

public readonly partial record struct NamedHash
{
    public static NamedHash Parse(string value, IFormatProvider? provider = null) => Parse(value.AsSpan(), provider);

    public static NamedHash Parse(ReadOnlySpan<char> value, IFormatProvider? provider = null)
    {
        if (TryParse(value, provider, out NamedHash result)) return result;
        throw new FormatException($"The input string '{value}' was not in a correct format.");
    }

    public static bool TryParse([NotNullWhen(true)] string? value, IFormatProvider? provider, out NamedHash result) => TryParse(value.AsSpan(), provider, out result);

    public static bool TryParse(ReadOnlySpan<char> value, IFormatProvider? provider, out NamedHash result)
    {
        result = default;

        int index = value.IndexOf(Separator);

        if (index < 0) return false;

        ReadOnlySpan<char> name = value[..index];
        ReadOnlySpan<char> data = value[(index + 1)..];

        if (name.IsEmpty || data.IsEmpty) return false;

        bool isDecoded = Hash.TryParse(data, provider, out Hash hash);

        result = new NamedHash(hash, name.ToString());

        return isDecoded;
    }
}
