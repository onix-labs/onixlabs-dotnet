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

public readonly partial struct UInt256
{
    /// <summary>
    /// The total number of bits in the <see cref="UInt256"/> type.
    /// </summary>
    internal const int BitWidth = 256;

    /// <summary>
    /// The number of bits in each half of the <see cref="UInt256"/> type.
    /// </summary>
    internal const int HalfBitWidth = 128;

    /// <summary>
    /// Gets the <see cref="UInt256"/> value representing zero.
    /// </summary>
    /// <value>The <see cref="UInt256"/> value representing zero.</value>
    public static UInt256 Zero => default;

    /// <summary>
    /// Gets the <see cref="UInt256"/> value representing one.
    /// </summary>
    /// <value>The <see cref="UInt256"/> value representing one.</value>
    public static UInt256 One => new(UInt128.Zero, UInt128.One);

    /// <summary>
    /// Gets the smallest possible <see cref="UInt256"/> value.
    /// </summary>
    /// <value>The smallest possible <see cref="UInt256"/> value (zero).</value>
    public static UInt256 MinValue => default;

    /// <summary>
    /// Gets the largest possible <see cref="UInt256"/> value.
    /// </summary>
    /// <value>The largest possible <see cref="UInt256"/> value.</value>
    public static UInt256 MaxValue => new(UInt128.MaxValue, UInt128.MaxValue);

    /// <summary>
    /// Gets the <see cref="UInt256"/> value with every bit set.
    /// </summary>
    /// <value>The <see cref="UInt256"/> value whose bit pattern is all ones.</value>
    public static UInt256 AllBitsSet => new(UInt128.MaxValue, UInt128.MaxValue);

    /// <inheritdoc cref="IAdditiveIdentity{TSelf,TResult}.AdditiveIdentity"/>
    static UInt256 IAdditiveIdentity<UInt256, UInt256>.AdditiveIdentity => Zero;

    /// <inheritdoc cref="IMultiplicativeIdentity{TSelf,TResult}.MultiplicativeIdentity"/>
    static UInt256 IMultiplicativeIdentity<UInt256, UInt256>.MultiplicativeIdentity => One;

    /// <inheritdoc cref="INumberBase{TSelf}.Radix"/>
    static int INumberBase<UInt256>.Radix => 2;

    /// <inheritdoc cref="IMinMaxValue{TSelf}.MinValue"/>
    static UInt256 IMinMaxValue<UInt256>.MinValue => MinValue;

    /// <inheritdoc cref="IMinMaxValue{TSelf}.MaxValue"/>
    static UInt256 IMinMaxValue<UInt256>.MaxValue => MaxValue;
}
