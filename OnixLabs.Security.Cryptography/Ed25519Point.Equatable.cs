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

internal readonly partial struct Ed25519Point
{
    /// <summary>
    /// Determines whether this curve point is equal to the specified <paramref name="other"/> curve point.
    /// </summary>
    /// <param name="other">The curve point to compare with the current point.</param>
    /// <returns>Returns <see langword="true"/> if the two points encode to identical bytes; otherwise, <see langword="false"/>.</returns>
    public bool Equals(Ed25519Point other) => EncodingEquals(other);

    /// <summary>
    /// Determines whether this curve point is equal to the specified <paramref name="obj"/>.
    /// </summary>
    /// <param name="obj">The object to compare with the current point.</param>
    /// <returns>Returns <see langword="true"/> if <paramref name="obj"/> is an <see cref="Ed25519Point"/> equal to this point; otherwise, <see langword="false"/>.</returns>
    public override bool Equals(object? obj) => obj is Ed25519Point other && Equals(other);

    /// <summary>
    /// Returns a hash code for the canonical encoding of this curve point.
    /// </summary>
    /// <returns>Returns a hash code for the canonical encoding of this curve point.</returns>
    public override int GetHashCode()
    {
        Span<byte> bytes = stackalloc byte[32];
        Encode(bytes);
        HashCode hash = new();
        hash.AddBytes(bytes);
        return hash.ToHashCode();
    }

    /// <summary>
    /// Determines whether two <see cref="Ed25519Point"/> values encode to identical bytes.
    /// </summary>
    /// <param name="left">The <paramref name="left"/> value to compare.</param>
    /// <param name="right">The <paramref name="right"/> value to compare.</param>
    /// <returns>Returns <see langword="true"/> if the two specified values encode to identical bytes; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(in Ed25519Point left, in Ed25519Point right) => left.EncodingEquals(right);

    /// <summary>
    /// Determines whether two <see cref="Ed25519Point"/> values encode to different bytes.
    /// </summary>
    /// <param name="left">The <paramref name="left"/> value to compare.</param>
    /// <param name="right">The <paramref name="right"/> value to compare.</param>
    /// <returns>Returns <see langword="true"/> if the two specified values encode to different bytes; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(in Ed25519Point left, in Ed25519Point right) => !left.EncodingEquals(right);

    /// <summary>
    /// Determines whether this curve point is equal to <paramref name="other"/> on the curve.
    /// The internal projective representations may differ even when the affine points coincide;
    /// this method canonicalizes by encoding before comparison.
    /// </summary>
    /// <param name="other">The curve point to compare with the current point.</param>
    /// <returns>Returns <see langword="true"/> if the two points encode to identical bytes; otherwise, <see langword="false"/>.</returns>
    private bool EncodingEquals(in Ed25519Point other)
    {
        Span<byte> a = stackalloc byte[32];
        Span<byte> b = stackalloc byte[32];
        Encode(a);
        other.Encode(b);
        return a.SequenceEqual(b);
    }
}
