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

public readonly partial struct NumberInfo
{
    /// <summary>
    /// Creates a new <see cref="NumberInfo"/> instance from the specified <see cref="IBinaryInteger{TSelf}"/> value.
    /// </summary>
    /// <param name="value">The <see cref="IBinaryInteger{TSelf}"/> value from which to create a new <see cref="NumberInfo"/> instance.</param>
    /// <param name="scale">The desired scale of the <see cref="NumberInfo"/> instance.</param>
    /// <param name="mode">Specifies the scale mode that should be used when scaling the <see cref="IBinaryInteger{TSelf}"/> value. The default value is <see cref="ScaleMode.Fractional"/>.</param>
    /// <typeparam name="T">The underlying <see cref="IBinaryInteger{TSelf}"/> type.</typeparam>
    /// <returns>Returns a new <see cref="NumberInfo"/> instance representing the specified <see cref="IBinaryInteger{TSelf}"/> value.</returns>
    public static NumberInfo Create<T>(T value, int scale = default, ScaleMode mode = default) where T : IBinaryInteger<T>
    {
        Require(scale >= 0, "Scale must be greater than or equal to zero.", nameof(scale));
        RequireIsDefined(mode, nameof(mode));
        BigInteger unscaledValue = value.GetUnscaledInteger(scale, mode);
        return new NumberInfo(unscaledValue, scale);
    }

    /// <summary>
    /// Creates a new <see cref="NumberInfo"/> instance from the specified <see cref="float"/> value.
    /// </summary>
    /// <param name="value">The <see cref="float"/> value from which to create a new <see cref="NumberInfo"/> instance.</param>
    /// <param name="mode">Specifies the conversion mode that should be used when converting the <see cref="float"/> value to a <see cref="NumberInfo"/> instance. The default value is <see cref="ConversionMode.Decimal"/>.</param>
    /// <returns>Returns a new <see cref="NumberInfo"/> instance representing the specified <see cref="float"/> value.</returns>
    public static NumberInfo Create(float value, ConversionMode mode = default)
    {
        RequireIsDefined(mode, nameof(mode));
        return Ieee754Converter.Convert(value, mode);
    }

    /// <summary>
    /// Creates a new <see cref="NumberInfo"/> instance from the specified <see cref="double"/> value.
    /// </summary>
    /// <param name="value">The <see cref="double"/> value from which to create a new <see cref="NumberInfo"/> instance.</param>
    /// <param name="mode">Specifies the conversion mode that should be used when converting the <see cref="double"/> value to a <see cref="NumberInfo"/> instance. The default value is <see cref="ConversionMode.Decimal"/>.</param>
    /// <returns>Returns a new <see cref="NumberInfo"/> instance representing the specified <see cref="double"/> value.</returns>
    public static NumberInfo Create(double value, ConversionMode mode = default)
    {
        RequireIsDefined(mode, nameof(mode));
        return Ieee754Converter.Convert(value, mode);
    }

    /// <summary>
    /// Creates a new <see cref="NumberInfo"/> instance from the specified <see cref="decimal"/> value.
    /// </summary>
    /// <param name="value">The <see cref="decimal"/> value from which to create a new <see cref="NumberInfo"/> instance.</param>
    /// <returns>Returns a new <see cref="NumberInfo"/> instance representing the specified <see cref="decimal"/> value.</returns>
    public static NumberInfo Create(decimal value)
    {
        return Create(value.GetUnscaledValue(), value.Scale);
    }

    /// <summary>
    /// Creates a new <see cref="NumberInfo"/> instance from the specified <see cref="ReadOnlySpan{T}"/> value.
    /// </summary>
    /// <param name="value">The <see cref="ReadOnlySpan{T}"/> value from which to create a new <see cref="NumberInfo"/> instance.</param>
    /// <returns>Returns a new <see cref="NumberInfo"/> instance representing the specified <see cref="ReadOnlySpan{T}"/> value.</returns>
    public static NumberInfo Create(ReadOnlySpan<byte> value)
    {
        return Create(new BigInteger(value[..^4]), BitConverter.ToInt32(value[^4..]));
    }
}
