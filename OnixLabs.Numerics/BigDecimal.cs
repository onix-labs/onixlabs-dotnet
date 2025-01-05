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
using System.Numerics;
using OnixLabs.Core;

namespace OnixLabs.Numerics;

/// <summary>
/// Represents an arbitrarily large signed decimal.
/// </summary>
// ReSharper disable once HeapView.PossibleBoxingAllocation
public readonly partial struct BigDecimal : IFloatingPoint<BigDecimal>, IValueEquatable<BigDecimal>, IValueComparable<BigDecimal>, IConvertible
{
    /// <summary>
    /// The underlying <see cref="NumberInfo"/> that represents the current <see cref="BigDecimal"/> value.
    /// </summary>
    private readonly NumberInfo number;

    /// <summary>
    /// Initializes a new instance of the <see cref="BigDecimal"/> struct.
    /// </summary>
    /// <param name="value">The unscaled integer value from which to construct a <see cref="BigDecimal"/> value.</param>
    /// <param name="scale">The scale of the <see cref="BigDecimal"/> value.</param>
    /// <param name="mode">The scale mode that determines how the specified value should be scaled.</param>
    public BigDecimal(BigInteger value, int scale = 0, ScaleMode mode = default)
    {
        Require(scale >= 0, "Scale must be greater than or equal to zero.", nameof(scale));
        RequireIsDefined(mode, nameof(mode));
        number = value.ToNumberInfo(scale, mode);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BigDecimal"/> struct.
    /// </summary>
    /// <param name="value">The floating-point value from which to construct a <see cref="BigDecimal"/>value.</param>
    /// <param name="mode">The conversion mode that determines whether the floating-point value should be converted from its binary or decimal representation.</param>
    public BigDecimal(float value, ConversionMode mode = default)
    {
        RequireIsDefined(mode, nameof(mode));
        number = value.ToNumberInfo(mode);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BigDecimal"/> struct.
    /// </summary>
    /// <param name="value">The floating-point value from which to construct a <see cref="BigDecimal"/>value.</param>
    /// <param name="mode">The conversion mode that determines whether the floating-point value should be converted from its binary or decimal representation.</param>
    public BigDecimal(double value, ConversionMode mode = default)
    {
        RequireIsDefined(mode, nameof(mode));
        number = value.ToNumberInfo(mode);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BigDecimal"/> struct.
    /// </summary>
    /// <param name="value">The decimal value from which to construct a <see cref="BigDecimal"/>value.</param>
    public BigDecimal(decimal value) => number = value.ToNumberInfo();

    /// <summary>
    /// Gets the unscaled value of the current <see cref="BigDecimal"/> value.
    /// </summary>
    public BigInteger UnscaledValue => number.UnscaledValue;

    /// <summary>
    /// Gets the scale of the current <see cref="BigDecimal"/> value.
    /// </summary>
    public int Scale => number.Scale;
}
