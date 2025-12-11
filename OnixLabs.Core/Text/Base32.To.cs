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
    public override string ToString() => ToString(Base32FormatProvider.Rfc4648);

    /// <inheritdoc/>
    public string ToString(IFormatProvider? formatProvider = null) => ToString(null, formatProvider);

    /// <inheritdoc/>
    public string ToString(string? format, IFormatProvider? formatProvider = null) => ToString(format.AsSpan(), formatProvider);

    /// <inheritdoc/>
    public string ToString(ReadOnlySpan<char> format, IFormatProvider? formatProvider = null) => IBaseCodec.Base32.Encode(value, formatProvider);
}
