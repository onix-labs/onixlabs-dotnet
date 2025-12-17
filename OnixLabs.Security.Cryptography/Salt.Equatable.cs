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

using System.Collections.Generic;
using OnixLabs.Core.Linq;

namespace OnixLabs.Security.Cryptography;

public readonly partial struct Salt
{
    /// <inheritdoc/>
    public bool Equals(Salt other) => value.SequenceEqualOrNull(other.value);

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is Salt other && Equals(other);

    /// <inheritdoc/>
    public override int GetHashCode() => value.GetContentHashCode();

    /// <inheritdoc/>
    public static bool operator ==(Salt left, Salt right) => EqualityComparer<Salt>.Default.Equals(left, right);

    /// <inheritdoc/>
    public static bool operator !=(Salt left, Salt right) => !EqualityComparer<Salt>.Default.Equals(left, right);
}
