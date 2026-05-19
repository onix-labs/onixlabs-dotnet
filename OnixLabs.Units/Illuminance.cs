// Copyright 2020-2025 ONIXLabs
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

using System.Numerics;

namespace OnixLabs.Units;

/// <summary>
/// Represents illuminance — luminous flux incident per unit area (<c>E = Φᵥ / A</c>). The SI unit is the lux
/// (1 lx = 1 lm/m² = 1 cd·sr/m²).
/// </summary>
/// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> value type.</typeparam>
// ReSharper disable MemberCanBePrivate.Global
#pragma warning disable CA2231
public readonly partial struct Illuminance<T>(
    in LuminousFlux<T> left,
    in Area<T> right
) : IMagnitudinalUnit<T>,
    IAdditiveUnit<Illuminance<T>>,
    ICompositeUnit<LuminousFlux<T>, Area<T>>
    where T : IFloatingPoint<T>
#pragma warning restore CA2231
{
    /// <summary>Gets the <see cref="LuminousFlux{T}"/> component.</summary>
    public LuminousFlux<T> Left { get; } = left;

    /// <summary>Gets the <see cref="Area{T}"/> component.</summary>
    public Area<T> Right { get; } = right;

    /// <summary>
    /// Gets the magnitude as lumens divided by square meters, yielding illuminance in lux.
    /// </summary>
    /// <remarks>Opaque scalar for <see cref="Equals(Illuminance{T})"/>, <see cref="CompareTo(Illuminance{T})"/>, and arithmetic.</remarks>
    public T Magnitude => Left.Magnitude / Right.SquareMeters;
}
