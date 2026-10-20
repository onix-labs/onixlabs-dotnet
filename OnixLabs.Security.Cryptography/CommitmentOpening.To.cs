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

public readonly partial struct CommitmentOpening
{
    private const string Redacted = "[redacted]";

    /// <summary>
    /// Returns the value of the current <see cref="CommitmentOpening"/>, encoded with the specified format provider.
    /// </summary>
    /// <remarks>
    /// The returned string exposes the secret opening; only use it to disclose the opening deliberately.
    /// </remarks>
    /// <param name="provider">The format provider that will be used to encode the opening.</param>
    /// <returns>Returns the value of the current <see cref="CommitmentOpening"/>, encoded with the specified format provider.</returns>
    public string ToString(IFormatProvider provider) => IBaseCodec.GetString(AsReadOnlySpan(), provider);

    /// <summary>
    /// Returns a placeholder that does not reveal the value of the current <see cref="CommitmentOpening"/>.
    /// </summary>
    /// <remarks>
    /// The opening is secret, so it is never written to logs, debuggers or string interpolation by accident.
    /// To export it deliberately, use <see cref="ToString(IFormatProvider)"/>.
    /// </remarks>
    /// <returns>Returns a placeholder that does not reveal the value of the current <see cref="CommitmentOpening"/>.</returns>
    public override string ToString() => Redacted;
}
