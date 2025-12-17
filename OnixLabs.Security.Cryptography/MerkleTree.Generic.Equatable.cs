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

public abstract partial class MerkleTree<T>
{
    /// <inheritdoc/>
    public bool Equals(MerkleTree<T>? other) => ReferenceEquals(this, other) || other is not null && other.Hash == Hash;

    /// <inheritdoc/>
    public override bool Equals(object? obj) => Equals(obj as MerkleTree<T>);

    /// <inheritdoc/>
    public override int GetHashCode() => HashCode.Combine(GetType(), Hash);

    /// <inheritdoc/>
    public static bool operator ==(MerkleTree<T>? left, MerkleTree<T>? right) => Equals(left, right);

    /// <inheritdoc/>
    public static bool operator !=(MerkleTree<T>? left, MerkleTree<T>? right) => !Equals(left, right);
}
