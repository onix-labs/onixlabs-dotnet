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

/// <summary>
/// Represents a point on the twisted Edwards curve -x^2 + y^2 = 1 + d*x^2*y^2 in extended coordinates
/// (X : Y : Z : T) where x = X/Z, y = Y/Z, T = XY/Z. The curve is the one used by Ed25519
/// per RFC 8032 §5.1.
/// </summary>
internal readonly partial struct Ed25519Point : IEquatable<Ed25519Point>
{
    /// <summary>
    /// The projective X coordinate.
    /// </summary>
    private readonly Ed25519FieldElement x;

    /// <summary>
    /// The projective Y coordinate.
    /// </summary>
    private readonly Ed25519FieldElement y;

    /// <summary>
    /// The projective Z coordinate (homogeneous denominator).
    /// </summary>
    private readonly Ed25519FieldElement z;

    /// <summary>
    /// The extended T coordinate, equal to XY/Z.
    /// </summary>
    private readonly Ed25519FieldElement t;

    /// <summary>
    /// Initializes a new instance of the <see cref="Ed25519Point"/> struct.
    /// </summary>
    /// <param name="x">The projective X coordinate.</param>
    /// <param name="y">The projective Y coordinate.</param>
    /// <param name="z">The projective Z coordinate (homogeneous denominator).</param>
    /// <param name="t">The extended T coordinate, equal to XY/Z.</param>
    private Ed25519Point(in Ed25519FieldElement x, in Ed25519FieldElement y, in Ed25519FieldElement z, in Ed25519FieldElement t)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.t = t;
    }
}
