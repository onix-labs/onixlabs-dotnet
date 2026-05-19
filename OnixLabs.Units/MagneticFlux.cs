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
/// Represents magnetic flux — the integral of magnetic field over an area, equivalent here to electric potential
/// multiplied by time (<c>Φ = V · t</c>). The SI unit of magnetic flux is the weber (1 Wb = 1 V·s).
/// </summary>
/// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> value type.</typeparam>
// ReSharper disable MemberCanBePrivate.Global
#pragma warning disable CA2231
public readonly partial struct MagneticFlux<T>(
    in ElectricPotential<T> left,
    in Time<T> right
) : IMagnitudinalUnit<T>,
    IAdditiveUnit<MagneticFlux<T>>,
    ICompositeUnit<ElectricPotential<T>, Time<T>>
    where T : IFloatingPoint<T>
#pragma warning restore CA2231
{
    /// <summary>Gets the <see cref="ElectricPotential{T}"/> component.</summary>
    public ElectricPotential<T> Left { get; } = left;

    /// <summary>Gets the <see cref="Time{T}"/> component.</summary>
    public Time<T> Right { get; } = right;

    /// <summary>
    /// Gets the magnitude as volts multiplied by seconds, yielding magnetic flux in webers.
    /// </summary>
    /// <remarks>Opaque scalar for <see cref="Equals(MagneticFlux{T})"/>, <see cref="CompareTo(MagneticFlux{T})"/>, and arithmetic.</remarks>
    public T Magnitude => Left.Magnitude * Right.Seconds;
}
