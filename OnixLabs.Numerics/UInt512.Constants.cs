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

public readonly partial struct UInt512
{
    /// <summary>
    /// The total number of bits in the <see cref="UInt512"/> type.
    /// </summary>
    internal const int BitWidth = 512;

    /// <summary>
    /// The number of bits in each half of the <see cref="UInt512"/> type.
    /// </summary>
    internal const int HalfBitWidth = 256;

    /// <summary>Gets the <see cref="UInt512"/> value representing zero.</summary>
    /// <value>Zero.</value>
    public static UInt512 Zero => default;

    /// <summary>Gets the <see cref="UInt512"/> value representing one.</summary>
    /// <value>One.</value>
    public static UInt512 One => new(UInt256.Zero, UInt256.One);

    /// <summary>Gets the smallest possible <see cref="UInt512"/> value.</summary>
    /// <value>Zero.</value>
    public static UInt512 MinValue => default;

    /// <summary>Gets the largest possible <see cref="UInt512"/> value.</summary>
    /// <value>The largest possible <see cref="UInt512"/>.</value>
    public static UInt512 MaxValue => new(UInt256.MaxValue, UInt256.MaxValue);

    /// <summary>Gets the <see cref="UInt512"/> value with every bit set.</summary>
    /// <value>The all-ones value.</value>
    public static UInt512 AllBitsSet => new(UInt256.MaxValue, UInt256.MaxValue);

    /// <inheritdoc cref="IAdditiveIdentity{TSelf,TResult}.AdditiveIdentity"/>
    static UInt512 IAdditiveIdentity<UInt512, UInt512>.AdditiveIdentity => Zero;

    /// <inheritdoc cref="IMultiplicativeIdentity{TSelf,TResult}.MultiplicativeIdentity"/>
    static UInt512 IMultiplicativeIdentity<UInt512, UInt512>.MultiplicativeIdentity => One;

    /// <inheritdoc cref="INumberBase{TSelf}.Radix"/>
    static int INumberBase<UInt512>.Radix => 2;

    /// <inheritdoc cref="IMinMaxValue{TSelf}.MinValue"/>
    static UInt512 IMinMaxValue<UInt512>.MinValue => MinValue;

    /// <inheritdoc cref="IMinMaxValue{TSelf}.MaxValue"/>
    static UInt512 IMinMaxValue<UInt512>.MaxValue => MaxValue;
}
