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
/// Represents angular acceleration — the rate of change of angular velocity with respect to time
/// (<c>α = ω / t</c>). SI base unit is rad/s².
/// </summary>
/// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> value type.</typeparam>
// ReSharper disable MemberCanBePrivate.Global
#pragma warning disable CA2231
public readonly partial struct AngularAcceleration<T>(
    in AngularVelocity<T> left,
    in Time<T> right
) : IMagnitudinalUnit<T>,
    IAdditiveUnit<AngularAcceleration<T>>,
    ICompositeUnit<AngularVelocity<T>, Time<T>>
    where T : IFloatingPoint<T>
#pragma warning restore CA2231
{
    /// <summary>Gets the <see cref="AngularVelocity{T}"/> component.</summary>
    public AngularVelocity<T> Left { get; } = left;

    /// <summary>Gets the <see cref="Time{T}"/> component.</summary>
    public Time<T> Right { get; } = right;

    /// <summary>Gets the magnitude in rad/s².</summary>
    /// <remarks>Opaque scalar for equality/comparison/arithmetic.</remarks>
    public T Magnitude => Left.Magnitude / Right.Seconds;
}
