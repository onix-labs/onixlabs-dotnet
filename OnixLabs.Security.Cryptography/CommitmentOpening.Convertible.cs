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

namespace OnixLabs.Security.Cryptography;

public readonly partial struct CommitmentOpening
{
    /// <inheritdoc/>
    /// <remarks>
    /// The returned memory exposes the secret opening; only use it to disclose the opening deliberately.
    /// </remarks>
    public ReadOnlyMemory<byte> AsReadOnlyMemory() => value;

    /// <inheritdoc/>
    /// <remarks>
    /// The returned span exposes the secret opening; only use it to disclose the opening deliberately.
    /// </remarks>
    public ReadOnlySpan<byte> AsReadOnlySpan() => value;
}
