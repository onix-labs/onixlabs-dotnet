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

public readonly partial struct Base32
{
    /// <inheritdoc/>
    public static Base32 Parse(string value, IFormatProvider? provider = null) => Parse(value.AsSpan(), provider);

    /// <inheritdoc/>
    public static Base32 Parse(ReadOnlySpan<char> value, IFormatProvider? provider = null) => new(IBaseCodec.Base32.Decode(value, provider));

    /// <inheritdoc/>
    public static bool TryParse(string? value, IFormatProvider? provider, out Base32 result) => TryParse(value.AsSpan(), provider, out result);

    /// <inheritdoc/>
    public static bool TryParse(ReadOnlySpan<char> value, IFormatProvider? provider, out Base32 result)
    {
        if (IBaseCodec.Base32.TryDecode(value, provider, out byte[] bytes))
        {
            result = new Base32(bytes);
            return true;
        }

        result = default;
        return false;
    }
}
