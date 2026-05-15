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

internal readonly partial struct Ed25519FieldElement
{
    /// <summary>
    /// Determines whether this field element represents the same residue class modulo p as <paramref name="other"/>.
    /// </summary>
    /// <param name="other">The field element to compare with the current field element.</param>
    /// <returns>Returns <see langword="true"/> if this element and <paramref name="other"/> represent the same residue class modulo p; otherwise, <see langword="false"/>.</returns>
    public bool ValueEquals(in Ed25519FieldElement other)
    {
        Span<byte> a = stackalloc byte[32];
        Span<byte> b = stackalloc byte[32];
        WriteBytes(a);
        other.WriteBytes(b);
        return a.SequenceEqual(b);
    }

    /// <summary>
    /// Determines whether this field element is equal to the specified <paramref name="other"/> field element.
    /// </summary>
    /// <param name="other">The field element to compare with the current field element.</param>
    /// <returns>Returns <see langword="true"/> if this element and <paramref name="other"/> represent the same residue class modulo p; otherwise, <see langword="false"/>.</returns>
    public bool Equals(Ed25519FieldElement other) => ValueEquals(other);

    /// <summary>
    /// Determines whether this field element is equal to the specified <paramref name="obj"/>.
    /// </summary>
    /// <param name="obj">The object to compare with the current field element.</param>
    /// <returns>Returns <see langword="true"/> if <paramref name="obj"/> is an <see cref="Ed25519FieldElement"/> equal to this element; otherwise, <see langword="false"/>.</returns>
    public override bool Equals(object? obj) => obj is Ed25519FieldElement other && Equals(other);

    /// <summary>
    /// Returns a hash code for the canonical representative of this field element.
    /// </summary>
    /// <returns>Returns a hash code for the canonical representative of this field element.</returns>
    public override int GetHashCode()
    {
        Span<byte> bytes = stackalloc byte[32];
        WriteBytes(bytes);
        HashCode hash = new();
        hash.AddBytes(bytes);
        return hash.ToHashCode();
    }

    /// <summary>
    /// Determines whether two <see cref="Ed25519FieldElement"/> values represent the same residue class modulo p.
    /// </summary>
    /// <param name="left">The <paramref name="left"/> value to compare.</param>
    /// <param name="right">The <paramref name="right"/> value to compare.</param>
    /// <returns>Returns <see langword="true"/> if the two specified values represent the same residue class modulo p; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(in Ed25519FieldElement left, in Ed25519FieldElement right) => left.ValueEquals(right);

    /// <summary>
    /// Determines whether two <see cref="Ed25519FieldElement"/> values represent different residue classes modulo p.
    /// </summary>
    /// <param name="left">The <paramref name="left"/> value to compare.</param>
    /// <param name="right">The <paramref name="right"/> value to compare.</param>
    /// <returns>Returns <see langword="true"/> if the two specified values represent different residue classes modulo p; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(in Ed25519FieldElement left, in Ed25519FieldElement right) => !left.ValueEquals(right);
}
