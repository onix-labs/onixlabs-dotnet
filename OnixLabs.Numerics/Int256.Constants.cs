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

public readonly partial struct Int256
{
    /// <summary>
    /// The total number of bits in the <see cref="Int256"/> type.
    /// </summary>
    internal const int BitWidth = 256;

    /// <summary>
    /// The number of bits in each half of the <see cref="Int256"/> type.
    /// </summary>
    internal const int HalfBitWidth = 128;

    /// <summary>
    /// The bit position of the sign bit within the <see cref="Int256"/> layout.
    /// </summary>
    internal const int SignBitPosition = BitWidth - 1;

    /// <summary>
    /// The mask covering the sign bit only, in the upper half.
    /// </summary>
    internal static readonly UInt128 SignBitMask = UInt128.One << (HalfBitWidth - 1);

    /// <summary>
    /// Gets the <see cref="Int256"/> value representing zero.
    /// </summary>
    /// <value>The <see cref="Int256"/> value representing zero.</value>
    public static Int256 Zero => default;

    /// <summary>
    /// Gets the <see cref="Int256"/> value representing one.
    /// </summary>
    /// <value>The <see cref="Int256"/> value representing one.</value>
    public static Int256 One => new(UInt128.Zero, UInt128.One);

    /// <summary>
    /// Gets the <see cref="Int256"/> value representing negative one (all bits set in two's-complement).
    /// </summary>
    /// <value>The <see cref="Int256"/> value representing negative one.</value>
    public static Int256 NegativeOne => new(UInt128.MaxValue, UInt128.MaxValue);

    /// <summary>
    /// Gets the smallest possible <see cref="Int256"/> value (<c>-2^255</c>).
    /// </summary>
    /// <value>The smallest possible <see cref="Int256"/> value.</value>
    public static Int256 MinValue => new(SignBitMask, UInt128.Zero);

    /// <summary>
    /// Gets the largest possible <see cref="Int256"/> value (<c>2^255 - 1</c>).
    /// </summary>
    /// <value>The largest possible <see cref="Int256"/> value.</value>
    public static Int256 MaxValue => new(~SignBitMask, UInt128.MaxValue);

    /// <summary>
    /// Gets the <see cref="Int256"/> value with every bit set (which equals <see cref="NegativeOne"/>).
    /// </summary>
    /// <value>The <see cref="Int256"/> value whose bit pattern is all ones.</value>
    public static Int256 AllBitsSet => new(UInt128.MaxValue, UInt128.MaxValue);

    /// <inheritdoc cref="IAdditiveIdentity{TSelf,TResult}.AdditiveIdentity"/>
    static Int256 IAdditiveIdentity<Int256, Int256>.AdditiveIdentity => Zero;

    /// <inheritdoc cref="IMultiplicativeIdentity{TSelf,TResult}.MultiplicativeIdentity"/>
    static Int256 IMultiplicativeIdentity<Int256, Int256>.MultiplicativeIdentity => One;

    /// <inheritdoc cref="INumberBase{TSelf}.Radix"/>
    static int INumberBase<Int256>.Radix => 2;

    /// <inheritdoc cref="IMinMaxValue{TSelf}.MinValue"/>
    static Int256 IMinMaxValue<Int256>.MinValue => MinValue;

    /// <inheritdoc cref="IMinMaxValue{TSelf}.MaxValue"/>
    static Int256 IMinMaxValue<Int256>.MaxValue => MaxValue;

    /// <inheritdoc cref="ISignedNumber{TSelf}.NegativeOne"/>
    static Int256 ISignedNumber<Int256>.NegativeOne => NegativeOne;
}
