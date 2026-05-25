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

using System;

namespace OnixLabs.Numerics;

public readonly partial struct Float256
{
    /// <summary>Computes an approximation of the reciprocal of the specified <see cref="Float256"/> value.</summary>
    /// <param name="value">The value whose reciprocal is to be approximated.</param>
    /// <returns>Returns an approximation of <c>1 / value</c>; this implementation returns the correctly-rounded quotient.</returns>
    public static Float256 ReciprocalEstimate(Float256 value) => One / value;

    /// <summary>Computes an approximation of the reciprocal of the square root of the specified <see cref="Float256"/> value.</summary>
    /// <param name="value">The value whose reciprocal square root is to be approximated.</param>
    /// <returns>Returns an approximation of <c>1 / √value</c>, faithfully rounded to within one unit in the last place (ULP) since it inherits the accuracy of <see cref="Sqrt"/>.</returns>
    public static Float256 ReciprocalSqrtEstimate(Float256 value) => One / Sqrt(value);
}
