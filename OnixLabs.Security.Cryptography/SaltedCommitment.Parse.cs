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
using System.Security.Cryptography;
using OnixLabs.Core;

namespace OnixLabs.Security.Cryptography;

public readonly partial record struct SaltedCommitment
{
    /// <inheritdoc/>
    public static SaltedCommitment Parse(string value, IFormatProvider? provider = null) =>
        Parse(value.AsSpan(), provider);

    /// <inheritdoc/>
    public static SaltedCommitment Parse(ReadOnlySpan<char> value, IFormatProvider? provider = null) =>
        SaltedCommitment.ParseOrThrow(value, provider);

    /// <inheritdoc/>
    public static bool TryParse([NotNullWhen(true)] string? value, IFormatProvider? provider, out SaltedCommitment result) =>
        TryParse(value.AsSpan(), provider, out result);

    /// <inheritdoc/>
    /// <remarks>
    /// Parsing is strict: the algorithm must be supported, and the length of the commitment must match its output.
    /// </remarks>
    public static bool TryParse(ReadOnlySpan<char> value, IFormatProvider? provider, out SaltedCommitment result)
    {
        result = default;

        int index = value.IndexOf(Separator);

        if (index < 0) return false;

        ReadOnlySpan<char> name = value[..index];
        ReadOnlySpan<char> data = value[(index + 1)..];

        if (!TryGetHashLength(name, out int length)) return false;
        if (!Hash.TryParse(data, provider, out Hash hash) || hash.Length != length) return false;

        result = new SaltedCommitment(hash, new HashAlgorithmName(name.ToString()));

        return true;
    }
}
