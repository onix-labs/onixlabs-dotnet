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
/// Represents a unit of luminous flux — the product of luminous intensity and solid angle
/// (<c>Φᵥ = Iᵥ · Ω</c>). The SI unit of luminous flux is the lumen (1 lm = 1 cd·sr).
/// </summary>
/// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> value type.</typeparam>
// ReSharper disable MemberCanBePrivate.Global
#pragma warning disable CA2231
public readonly partial struct LuminousFlux<T>(
    in LuminousIntensity<T> left,
    in SolidAngle<T> right
) : IMagnitudinalUnit<T>,
    IAdditiveUnit<LuminousFlux<T>>,
    ICompositeUnit<LuminousIntensity<T>, SolidAngle<T>>
    where T : IFloatingPoint<T>
#pragma warning restore CA2231
{
    /// <summary>
    /// Gets the <see cref="LuminousIntensity{T}"/> component of the luminous flux.
    /// </summary>
    public LuminousIntensity<T> Left { get; } = left;

    /// <summary>
    /// Gets the <see cref="SolidAngle{T}"/> component of the luminous flux — the solid angle (steradian) over which
    /// the luminous intensity is emitted.
    /// </summary>
    public SolidAngle<T> Right { get; } = right;

    // Opaque scalar for equality, ordering, hashing, and arithmetic round-trips. Internal so the assembly (and
    // InternalsVisibleTo'd tests) can read it directly; exposed publicly only via the explicit interface impl.
    internal T Magnitude => Left.Candelas * Right.Steradians;

    /// <inheritdoc/>
    T IMagnitudinalUnit<T>.Magnitude => Magnitude;

    // Display-ready SI base value (lumens, cd·sr).
    internal T SIBaseValue => Magnitude;

    internal static LuminousFlux<T> One =>
        new(LuminousIntensity<T>.FromCandelas(T.One), SolidAngle<T>.FromSteradians(T.One));

    internal static LuminousFlux<T> WithMagnitude(T magnitude) =>
        new(LuminousIntensity<T>.FromCandelas(magnitude), SolidAngle<T>.FromSteradians(T.One));
}
