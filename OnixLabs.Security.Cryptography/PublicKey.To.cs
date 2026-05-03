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
using OnixLabs.Core.Text;

namespace OnixLabs.Security.Cryptography;

public abstract partial class PublicKey
{
    /// <summary>
    /// Creates a new <see cref="NamedPublicKey"/> from the current <see cref="PublicKey"/> instance.
    /// </summary>
    /// <returns>Returns a new <see cref="NamedPublicKey"/> from the current <see cref="PublicKey"/> instance.</returns>
    public abstract NamedPublicKey ToNamedPublicKey();

    /// <inheritdoc/>
    public string ToString(IFormatProvider provider) => IBaseCodec.GetString(AsReadOnlySpan(), provider);

    /// <inheritdoc/>
    public override string ToString() => ToString(Base16FormatProvider.Invariant);
}
