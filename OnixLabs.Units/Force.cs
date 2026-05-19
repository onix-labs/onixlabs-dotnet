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
/// Represents a unit of force — the product of mass and acceleration (Newton's second law, F = ma). The SI unit of
/// force is the Newton (kg·m/s²).
/// </summary>
/// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> value type.</typeparam>
// ReSharper disable MemberCanBePrivate.Global
#pragma warning disable CA2231
public readonly partial struct Force<T>(
    in Mass<T> left,
    in Acceleration<T> right
) : IMagnitudinalUnit<T>,
    IAdditiveUnit<Force<T>>,
    ICompositeUnit<Mass<T>, Acceleration<T>>
    where T : IFloatingPoint<T>
#pragma warning restore CA2231
{
    /// <summary>
    /// Gets the <see cref="Mass{T}"/> component of the force.
    /// </summary>
    public Mass<T> Left { get; } = left;

    /// <summary>
    /// Gets the <see cref="Acceleration{T}"/> component of the force.
    /// </summary>
    public Acceleration<T> Right { get; } = right;

    /// <summary>
    /// Gets the magnitude of the current <see cref="Force{T}"/> as the product of the mass (in kilograms) and
    /// acceleration (in m/s²), yielding the force in Newtons (kg·m/s²).
    /// </summary>
    /// <remarks>
    /// The magnitude is an opaque scalar used by <see cref="Equals(Force{T})"/>, <see cref="CompareTo(Force{T})"/>,
    /// <see cref="GetHashCode"/>, and the arithmetic operators. It is not intended for display — use
    /// <see cref="ToString(string, System.IFormatProvider)"/> for that.
    ///
    /// As with <see cref="Acceleration{T}"/>, the formula reads each component at its SI base scale (Mass at kg,
    /// Acceleration at its m/s² magnitude) so the resulting magnitude lands at the human-readable Newton scale and
    /// round-trips cleanly through arithmetic.
    /// </remarks>
    public T Magnitude => Left.KiloGrams * Right.Magnitude;
}
