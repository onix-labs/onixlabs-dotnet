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

using OnixLabs.Core.Linq;

namespace OnixLabs.Core.Text;

public readonly partial struct Base58
{
    /// <inheritdoc/>
    public static bool Equals(Base58 left, Base58 right) => left.value.SequenceEqualOrNull(right.value);

    /// <inheritdoc/>
    public bool Equals(Base58 other) => Equals(this, other);

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is Base58 other && Equals(other);

    /// <inheritdoc/>
    public override int GetHashCode() => value.GetContentHashCode();
}
