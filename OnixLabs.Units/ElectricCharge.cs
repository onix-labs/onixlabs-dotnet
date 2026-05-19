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
/// Represents a unit of electric charge — the product of electric current and time (<c>Q = I · t</c>). The SI unit
/// of electric charge is the coulomb (1 C = 1 A·s).
/// </summary>
/// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> value type.</typeparam>
// ReSharper disable MemberCanBePrivate.Global
#pragma warning disable CA2231
public readonly partial struct ElectricCharge<T>(
    in Current<T> left,
    in Time<T> right
) : IMagnitudinalUnit<T>,
    IAdditiveUnit<ElectricCharge<T>>,
    ICompositeUnit<Current<T>, Time<T>>
    where T : IFloatingPoint<T>
#pragma warning restore CA2231
{
    /// <summary>
    /// Gets the <see cref="Current{T}"/> component of the electric charge.
    /// </summary>
    public Current<T> Left { get; } = left;

    /// <summary>
    /// Gets the <see cref="Time{T}"/> component of the electric charge — the interval over which the current flows.
    /// </summary>
    public Time<T> Right { get; } = right;

    /// <summary>
    /// Gets the magnitude of the current <see cref="ElectricCharge{T}"/> as the product of the current (in amperes)
    /// and the duration (in seconds), yielding the charge in coulombs (A·s).
    /// </summary>
    /// <remarks>
    /// The magnitude is an opaque scalar used by <see cref="Equals(ElectricCharge{T})"/>,
    /// <see cref="CompareTo(ElectricCharge{T})"/>, <see cref="GetHashCode"/>, and the arithmetic operators. It is not
    /// intended for display — use <see cref="ToString(string, System.IFormatProvider)"/> for that.
    ///
    /// Both components are read at their SI base scale (Current at amperes via <see cref="Current{T}.Amperes"/>, Time
    /// at seconds via <see cref="Time{T}.Seconds"/>) so the resulting magnitude lands at the human-readable coulomb
    /// scale and round-trips cleanly through arithmetic.
    /// </remarks>
    public T Magnitude => Left.Amperes * Right.Seconds;
}
