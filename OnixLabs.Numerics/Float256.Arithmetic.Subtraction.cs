// Copyright © 2020 ONIXLabs
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

namespace OnixLabs.Numerics;

public readonly partial struct Float256
{
    /// <summary>Computes the difference of two <see cref="Float256"/> values.</summary>
    /// <param name="left">The minuend.</param>
    /// <param name="right">The subtrahend.</param>
    /// <returns>Returns <c>left - right</c> with IEEE 754 special-value handling.</returns>
    public static Float256 Subtract(Float256 left, Float256 right) => Add(left, -right);

    /// <inheritdoc cref="Subtract(Float256, Float256)"/>
    public static Float256 operator -(Float256 left, Float256 right) => Subtract(left, right);
}
