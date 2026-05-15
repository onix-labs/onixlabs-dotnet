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
    /// Doubles an Edwards point using "dbl-2008-hwcd" for a = -1. Cheaper than <see cref="Add"/>(p, p)
    /// because the four input multiplications collapse to squarings and the curve constant d is not used.
    /// </summary>
    /// <param name="p">The curve point to double.</param>
    /// <returns>Returns the curve point representing 2 * <paramref name="p"/>.</returns>
    public static Ed25519Point Double(in Ed25519Point p)
    {
        Ed25519FieldElement a = Ed25519FieldElement.Square(p.x); // A = X^2
        Ed25519FieldElement b = Ed25519FieldElement.Square(p.y); // B = Y^2
        Ed25519FieldElement zSquared = Ed25519FieldElement.Square(p.z);
        Ed25519FieldElement c = zSquared + zSquared; // C = 2*Z^2

        Ed25519FieldElement xPlusYSquared = Ed25519FieldElement.Square(p.x + p.y);

        Ed25519FieldElement e = xPlusYSquared - (a + b); // E = (X+Y)^2 - A - B
        Ed25519FieldElement g = b - a; // G = B - A   (a = -1)
        Ed25519FieldElement f = g - c; // F = G - C
        Ed25519FieldElement h = -(a + b); // H = -(A + B)

        return new Ed25519Point(e * f, g * h, f * g, e * h);
    }
}
