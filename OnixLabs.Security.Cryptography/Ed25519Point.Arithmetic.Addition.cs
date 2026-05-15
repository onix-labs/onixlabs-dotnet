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

namespace OnixLabs.Security.Cryptography;

internal readonly partial struct Ed25519Point
{
    /// <summary>
    /// Computes the sum of the specified <see cref="Ed25519Point"/> values.
    /// </summary>
    /// <param name="left">The <paramref name="left"/> value to add to.</param>
    /// <param name="right">The <paramref name="right"/> value to add.</param>
    /// <returns>Returns the sum of the specified <see cref="Ed25519Point"/> values.</returns>
    public static Ed25519Point operator +(in Ed25519Point left, in Ed25519Point right) => Add(left, right);

    /// <summary>
    /// Adds two Edwards points using the standard twisted-Edwards "add-2008-hwcd" formulas
    /// specialized for a = -1 (Hisil-Wong-Carter-Dawson).
    /// </summary>
    /// <param name="p1">The first curve point addend.</param>
    /// <param name="p2">The second curve point addend.</param>
    /// <returns>Returns the curve point representing <paramref name="p1"/> + <paramref name="p2"/>.</returns>
    private static Ed25519Point Add(in Ed25519Point p1, in Ed25519Point p2)
    {
        Ed25519FieldElement a = (p1.y - p1.x) * (p2.y - p2.x);
        Ed25519FieldElement b = (p1.y + p1.x) * (p2.y + p2.x);
        Ed25519FieldElement c = p1.t * TwoD * p2.t;

        Ed25519FieldElement zProduct = p1.z * p2.z;
        Ed25519FieldElement d = zProduct + zProduct;

        Ed25519FieldElement e = b - a;
        Ed25519FieldElement f = d - c;
        Ed25519FieldElement g = d + c;
        Ed25519FieldElement h = b + a;

        return new Ed25519Point(e * f, g * h, f * g, e * h);
    }
}
