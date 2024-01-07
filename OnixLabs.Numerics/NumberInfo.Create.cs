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
    public static NumberInfo Create<T>(T value, int scale = default, ScaleMode mode = default) where T : IBinaryInteger<T>
    {
        Require(scale >= 0, "Scale must be greater than, or equal to zero.", nameof(scale));
        RequireIsDefined(mode, nameof(mode));

        BigInteger unscaledValue;

        if (scale == 0 || mode == ScaleMode.Fractional) unscaledValue = value.ToBigInteger();
        else unscaledValue = value.ToBigInteger() * BigInteger.Pow(10, scale);

        return new NumberInfo(unscaledValue, scale);
    }

    public static NumberInfo Create(float value, ConversionMode mode = default)
    {
        RequireIsDefined(mode, nameof(mode));
        return Ieee754Converter.Convert(value, mode);
    }

    public static NumberInfo Create(double value, ConversionMode mode = default)
    {
        RequireIsDefined(mode, nameof(mode));
        return Ieee754Converter.Convert(value, mode);
    }

    public static NumberInfo Create(decimal value)
    {
        return Create(value.GetUnscaledValue(), value.Scale);
    }

    public static NumberInfo Create(ReadOnlySpan<byte> value)
    {
        return Create(new BigInteger(value[..^4]), BitConverter.ToInt32(value[^4..]));
    }
}
