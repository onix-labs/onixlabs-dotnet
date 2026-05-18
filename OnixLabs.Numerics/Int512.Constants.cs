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

public readonly partial struct Int512
{
    /// <summary>
    /// The total number of bits in the <see cref="Int512"/> type.
    /// </summary>
    internal const int BitWidth = 512;

    /// <summary>
    /// The number of bits in each half of the <see cref="Int512"/> type.
    /// </summary>
    internal const int HalfBitWidth = 256;

    /// <summary>
    /// The bit position of the sign bit within the <see cref="Int512"/> layout.
    /// </summary>
    internal const int SignBitPosition = BitWidth - 1;

    /// <summary>
    /// The mask covering the sign bit only, in the upper half.
    /// </summary>
    internal static readonly UInt256 SignBitMask = UInt256.One << (HalfBitWidth - 1);

    /// <summary>
    /// Gets the <see cref="Int512"/> value representing zero.
    /// </summary>
    /// <value>The <see cref="Int512"/> value representing zero.</value>
    public static Int512 Zero => default;

    /// <summary>
    /// Gets the <see cref="Int512"/> value representing one.
    /// </summary>
    /// <value>The <see cref="Int512"/> value representing one.</value>
    public static Int512 One => new(UInt256.Zero, UInt256.One);

    /// <summary>
    /// Gets the <see cref="Int512"/> value representing negative one (all bits set in two's-complement).
    /// </summary>
    /// <value>The <see cref="Int512"/> value representing negative one.</value>
    public static Int512 NegativeOne => new(UInt256.MaxValue, UInt256.MaxValue);

    /// <summary>
    /// Gets the smallest possible <see cref="Int512"/> value (<c>-2^511</c>).
    /// </summary>
    /// <value>The smallest possible <see cref="Int512"/> value.</value>
    public static Int512 MinValue => new(SignBitMask, UInt256.Zero);

    /// <summary>
    /// Gets the largest possible <see cref="Int512"/> value (<c>2^511 - 1</c>).
    /// </summary>
    /// <value>The largest possible <see cref="Int512"/> value.</value>
    public static Int512 MaxValue => new(~SignBitMask, UInt256.MaxValue);

    /// <summary>
    /// Gets the <see cref="Int512"/> value with every bit set (which equals <see cref="NegativeOne"/>).
    /// </summary>
    /// <value>The <see cref="Int512"/> value whose bit pattern is all ones.</value>
    public static Int512 AllBitsSet => new(UInt256.MaxValue, UInt256.MaxValue);

    /// <inheritdoc cref="IAdditiveIdentity{TSelf,TResult}.AdditiveIdentity"/>
    static Int512 IAdditiveIdentity<Int512, Int512>.AdditiveIdentity => Zero;

    /// <inheritdoc cref="IMultiplicativeIdentity{TSelf,TResult}.MultiplicativeIdentity"/>
    static Int512 IMultiplicativeIdentity<Int512, Int512>.MultiplicativeIdentity => One;

    /// <inheritdoc cref="INumberBase{TSelf}.Radix"/>
    static int INumberBase<Int512>.Radix => 2;

    /// <inheritdoc cref="IMinMaxValue{TSelf}.MinValue"/>
    static Int512 IMinMaxValue<Int512>.MinValue => MinValue;

    /// <inheritdoc cref="IMinMaxValue{TSelf}.MaxValue"/>
    static Int512 IMinMaxValue<Int512>.MaxValue => MaxValue;

    /// <inheritdoc cref="ISignedNumber{TSelf}.NegativeOne"/>
    static Int512 ISignedNumber<Int512>.NegativeOne => NegativeOne;
}
