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
/// Represents a unit of acceleration — the rate of change of speed with respect to time, expressed as a
/// <see cref="Speed{T}"/> divided by a <see cref="Time{T}"/>.
/// </summary>
/// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> value type.</typeparam>
// ReSharper disable MemberCanBePrivate.Global
#pragma warning disable CA2231
public readonly partial struct Acceleration<T>(
    in Speed<T> left,
    in Time<T> right
) : IMagnitudinalUnit<T>,
    IAdditiveUnit<Acceleration<T>>,
    ICompositeUnit<Speed<T>, Time<T>>
    where T : IFloatingPoint<T>
#pragma warning restore CA2231
{
    /// <summary>
    /// Gets the <see cref="Speed{T}"/> component of the acceleration.
    /// </summary>
    public Speed<T> Left { get; } = left;

    /// <summary>
    /// Gets the <see cref="Time{T}"/> component of the acceleration — the interval over which the speed changes.
    /// </summary>
    public Time<T> Right { get; } = right;

    /// <summary>
    /// Gets the magnitude of the current <see cref="Acceleration{T}"/> as the ratio of the speed magnitude (in m/s)
    /// and the time interval (in seconds), yielding the acceleration in m/s².
    /// </summary>
    /// <remarks>
    /// The magnitude is an opaque scalar used by <see cref="Equals(Acceleration{T})"/>, <see cref="CompareTo(Acceleration{T})"/>,
    /// <see cref="GetHashCode"/>, and the arithmetic operators. It is not intended for display — use
    /// <see cref="ToString(string, System.IFormatProvider)"/> for that.
    ///
    /// Unlike the strict <c>Left.Canonical / Right.Canonical</c> rule for composites of two single-dimension units,
    /// nested composites read the inner composite at its natural scale (<see cref="Speed{T}.Magnitude"/>, in m/s) and
    /// the outer single-dimension unit at its SI base scale (<see cref="Time{T}.Seconds"/>). The ratio lands at the
    /// human-readable m/s² scale, so the magnitude is meaningful and round-trips cleanly through arithmetic.
    /// </remarks>
    public T Magnitude => Left.Magnitude / Right.Seconds;
}
