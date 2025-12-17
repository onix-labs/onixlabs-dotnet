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
using OnixLabs.Core;
using OnixLabs.Core.Text;

namespace OnixLabs.Security.Cryptography;

public sealed partial class RsaPublicKey
{
    /// <inheritdoc/>
    public static RsaPublicKey Parse(string value, IFormatProvider? provider = null) =>
        Parse(value.AsSpan(), provider);

    /// <inheritdoc/>
    public static RsaPublicKey Parse(ReadOnlySpan<char> value, IFormatProvider? provider = null) =>
        RsaPublicKey.ParseOrThrow(value, provider);

    /// <inheritdoc/>
    public static bool TryParse(string? value, IFormatProvider? provider, out RsaPublicKey result) =>
        TryParse(value.AsSpan(), provider, out result);

    /// <inheritdoc/>
    public static bool TryParse(ReadOnlySpan<char> value, IFormatProvider? provider, out RsaPublicKey result)
    {
        bool isDecoded = IBaseCodec.TryGetBytes(value, provider ?? Base16FormatProvider.Invariant, out byte[] bytes);
        // ReSharper disable once HeapView.ObjectAllocation.Evident
        result = new RsaPublicKey(bytes);
        return isDecoded;
    }
}
