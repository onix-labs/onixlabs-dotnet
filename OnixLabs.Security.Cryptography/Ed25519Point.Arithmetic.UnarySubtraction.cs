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
    /// Computes the unary subtraction of the specified <see cref="Ed25519Point"/> value.
    /// </summary>
    /// <param name="value">The value for which to perform unary subtraction.</param>
    /// <returns>Returns the unary subtraction of the specified <see cref="Ed25519Point"/> value.</returns>
    public static Ed25519Point operator -(in Ed25519Point value) => Negate(value);

    /// <summary>
    /// Negates the point. For twisted Edwards with a = -1, -P = (-X : Y : Z : -T).
    /// </summary>
    /// <param name="p">The curve point to negate.</param>
    /// <returns>Returns the curve point representing -<paramref name="p"/>.</returns>
    private static Ed25519Point Negate(in Ed25519Point p) => new(-p.x, p.y, p.z, -p.t);
}
