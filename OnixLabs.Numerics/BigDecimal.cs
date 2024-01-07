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

namespace OnixLabs.Numerics;

/// <summary>
/// Represents an arbitrarily large signed decimal.
/// </summary>
public readonly partial struct BigDecimal : IFloatingPoint<BigDecimal>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BigDecimal"/> struct.
    /// </summary>
    /// <param name="value">The unscaled integer value from which to construct a <see cref="BigDecimal"/> value.</param>
    /// <param name="scale">The scale of the <see cref="BigDecimal"/> value.</param>
    /// <param name="mode">The scale mode that determines how the specified value should be scaled.</param>
    public BigDecimal(BigInteger value, int scale = default, ScaleMode mode = default)
    {
        Require(scale >= 0, "Scale must be greater than or equal to zero.", nameof(scale));
        RequireIsDefined(mode, nameof(mode));
        NumberInfo = NumberInfo.Create(value, scale, mode);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BigDecimal"/> struct.
    /// </summary>
    /// <param name="value">The floating-point value from which to construct a <see cref="BigDecimal"/>value.</param>
    /// <param name="mode">The conversion mode that determines whether the floating-point value should be converted from its binary or decimal representation.</param>
    public BigDecimal(float value, ConversionMode mode = default)
    {
        RequireIsDefined(mode, nameof(mode));
        NumberInfo = NumberInfo.Create(value, mode);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BigDecimal"/> struct.
    /// </summary>
    /// <param name="value">The floating-point value from which to construct a <see cref="BigDecimal"/>value.</param>
    /// <param name="mode">The conversion mode that determines whether the floating-point value should be converted from its binary or decimal representation.</param>
    public BigDecimal(double value, ConversionMode mode = default)
    {
        RequireIsDefined(mode, nameof(mode));
        NumberInfo = NumberInfo.Create(value, mode);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BigDecimal"/> struct.
    /// </summary>
    /// <param name="value">The decimal value from which to construct a <see cref="BigDecimal"/>value.</param>
    public BigDecimal(decimal value)
    {
        NumberInfo = NumberInfo.Create(value);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BigDecimal"/> struct.
    /// </summary>
    /// <param name="value">The bytes from which to construct a <see cref="BigDecimal"/>value.</param>
    public BigDecimal(ReadOnlySpan<byte> value)
    {
        NumberInfo = NumberInfo.Create(value);
    }

    /// <summary>
    /// Gets the underlying <see cref="NumberInfo"/> for the current <see cref="BigDecimal"/> value.
    /// </summary>
    internal NumberInfo NumberInfo { get; }
}
