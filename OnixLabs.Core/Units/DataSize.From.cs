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

using OnixLabs.Core.Numerics;

namespace OnixLabs.Core.Units;

public readonly partial struct DataSize<T>
{
    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified bit value.
    /// </summary>
    /// <param name="value">The value from which to create a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified bit value.</returns>
    public static DataSize<T> FromBits(T value)
    {
        return new DataSize<T>(value);
    }

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified nibble value.
    /// </summary>
    /// <param name="value">The value from which to create a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified nibble value.</returns>
    public static DataSize<T> FromNibbles(T value)
    {
        return FromBits(value * BitsPerNibble);
    }

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified byte value.
    /// </summary>
    /// <param name="value">The value from which to create a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified byte value.</returns>
    public static DataSize<T> FromBytes(T value)
    {
        return FromBits(value * BitsPerByte);
    }

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified word value.
    /// </summary>
    /// <param name="value">The value from which to create a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified word value.</returns>
    public static DataSize<T> FromWords(T value)
    {
        return FromBits(value * BitsPerWord);
    }

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified double-word value.
    /// </summary>
    /// <param name="value">The value from which to create a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified double-word value.</returns>
    public static DataSize<T> FromDoubleWords(T value)
    {
        return FromBits(value * BitsPerDoubleWord);
    }

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified quad-word value.
    /// </summary>
    /// <param name="value">The value from which to create a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified quad-word value.</returns>
    public static DataSize<T> FromQuadWords(T value)
    {
        return FromBits(value * BitsPerQuadWord);
    }

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified kilobit value.
    /// </summary>
    /// <param name="value">The value from which to create a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified kilobit value.</returns>
    public static DataSize<T> FromKiloBits(T value)
    {
        return GetUnscaledBits(value, DecimalThousand, 1);
    }

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified kibibit value.
    /// </summary>
    /// <param name="value">The value from which to create a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified kibibit value.</returns>
    public static DataSize<T> FromKibiBits(T value)
    {
        return GetUnscaledBits(value, BinaryThousand, 1);
    }

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified kilobyte value.
    /// </summary>
    /// <param name="value">The value from which to create a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified kilobyte value.</returns>
    public static DataSize<T> FromKiloBytes(T value)
    {
        return GetUnscaledBytes(value, DecimalThousand, 1);
    }

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified kibibyte value.
    /// </summary>
    /// <param name="value">The value from which to create a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified kibibyte value.</returns>
    public static DataSize<T> FromKibiBytes(T value)
    {
        return GetUnscaledBytes(value, BinaryThousand, 1);
    }

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified megabit value.
    /// </summary>
    /// <param name="value">The value from which to create a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified megabit value.</returns>
    public static DataSize<T> FromMegaBits(T value)
    {
        return GetUnscaledBits(value, DecimalThousand, 2);
    }

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified mebibit value.
    /// </summary>
    /// <param name="value">The value from which to create a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified mebibit value.</returns>
    public static DataSize<T> FromMebiBits(T value)
    {
        return GetUnscaledBits(value, BinaryThousand, 2);
    }

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified megabyte value.
    /// </summary>
    /// <param name="value">The value from which to create a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified megabyte value.</returns>
    public static DataSize<T> FromMegaBytes(T value)
    {
        return GetUnscaledBytes(value, DecimalThousand, 2);
    }

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified mebibyte value.
    /// </summary>
    /// <param name="value">The value from which to create a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified mebibyte value.</returns>
    public static DataSize<T> FromMebiBytes(T value)
    {
        return GetUnscaledBytes(value, BinaryThousand, 2);
    }

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified gigabit value.
    /// </summary>
    /// <param name="value">The value from which to create a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified gigabit value.</returns>
    public static DataSize<T> FromGigaBits(T value)
    {
        return GetUnscaledBits(value, DecimalThousand, 3);
    }

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified gibibit value.
    /// </summary>
    /// <param name="value">The value from which to create a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified gibibit value.</returns>
    public static DataSize<T> FromGibiBits(T value)
    {
        return GetUnscaledBits(value, BinaryThousand, 3);
    }

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified gigabyte value.
    /// </summary>
    /// <param name="value">The value from which to create a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified gigabyte value.</returns>
    public static DataSize<T> FromGigaBytes(T value)
    {
        return GetUnscaledBytes(value, DecimalThousand, 3);
    }

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified gibibyte value.
    /// </summary>
    /// <param name="value">The value from which to create a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified gibibyte value.</returns>
    public static DataSize<T> FromGibiBytes(T value)
    {
        return GetUnscaledBytes(value, BinaryThousand, 3);
    }

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified terabit value.
    /// </summary>
    /// <param name="value">The value from which to create a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified terabit value.</returns>
    public static DataSize<T> FromTeraBits(T value)
    {
        return GetUnscaledBits(value, DecimalThousand, 4);
    }

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified tebibit value.
    /// </summary>
    /// <param name="value">The value from which to create a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified tebibit value.</returns>
    public static DataSize<T> FromTebiBits(T value)
    {
        return GetUnscaledBits(value, BinaryThousand, 4);
    }

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified terabyte value.
    /// </summary>
    /// <param name="value">The value from which to create a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified terabyte value.</returns>
    public static DataSize<T> FromTeraBytes(T value)
    {
        return GetUnscaledBytes(value, DecimalThousand, 4);
    }

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified tebibyte value.
    /// </summary>
    /// <param name="value">The value from which to create a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified tebibyte value.</returns>
    public static DataSize<T> FromTebiBytes(T value)
    {
        return GetUnscaledBytes(value, BinaryThousand, 4);
    }

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified petabit value.
    /// </summary>
    /// <param name="value">The value from which to create a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified petabit value.</returns>
    public static DataSize<T> FromPetaBits(T value)
    {
        return GetUnscaledBits(value, DecimalThousand, 5);
    }

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified pebibit value.
    /// </summary>
    /// <param name="value">The value from which to create a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified pebibit value.</returns>
    public static DataSize<T> FromPebiBits(T value)
    {
        return GetUnscaledBits(value, BinaryThousand, 5);
    }

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified petabyte value.
    /// </summary>
    /// <param name="value">The value from which to create a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified petabyte value.</returns>
    public static DataSize<T> FromPetaBytes(T value)
    {
        return GetUnscaledBytes(value, DecimalThousand, 5);
    }

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified pebibyte value.
    /// </summary>
    /// <param name="value">The value from which to create a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified pebibyte value.</returns>
    public static DataSize<T> FromPebiBytes(T value)
    {
        return GetUnscaledBytes(value, BinaryThousand, 5);
    }

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified exabit value.
    /// </summary>
    /// <param name="value">The value from which to create a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified exabit value.</returns>
    public static DataSize<T> FromExaBits(T value)
    {
        return GetUnscaledBits(value, DecimalThousand, 6);
    }

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified exbibit value.
    /// </summary>
    /// <param name="value">The value from which to create a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified exbibit value.</returns>
    public static DataSize<T> FromExbiBits(T value)
    {
        return GetUnscaledBits(value, BinaryThousand, 6);
    }

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified exabyte value.
    /// </summary>
    /// <param name="value">The value from which to create a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified exabyte value.</returns>
    public static DataSize<T> FromExaBytes(T value)
    {
        return GetUnscaledBytes(value, DecimalThousand, 6);
    }

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified exbibyte value.
    /// </summary>
    /// <param name="value">The value from which to create a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified exbibyte value.</returns>
    public static DataSize<T> FromExbiBytes(T value)
    {
        return GetUnscaledBytes(value, BinaryThousand, 6);
    }

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified zettabit value.
    /// </summary>
    /// <param name="value">The value from which to create a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified zettabit value.</returns>
    public static DataSize<T> FromZettaBits(T value)
    {
        return GetUnscaledBits(value, DecimalThousand, 7);
    }

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified zebibit value.
    /// </summary>
    /// <param name="value">The value from which to create a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified zebibit value.</returns>
    public static DataSize<T> FromZebiBits(T value)
    {
        return GetUnscaledBits(value, BinaryThousand, 7);
    }

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified zettabyte value.
    /// </summary>
    /// <param name="value">The value from which to create a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified zettabyte value.</returns>
    public static DataSize<T> FromZettaBytes(T value)
    {
        return GetUnscaledBytes(value, DecimalThousand, 7);
    }

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified zebibyte value.
    /// </summary>
    /// <param name="value">The value from which to create a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified zebibyte value.</returns>
    public static DataSize<T> FromZebiBytes(T value)
    {
        return GetUnscaledBytes(value, BinaryThousand, 7);
    }

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified yottabit value.
    /// </summary>
    /// <param name="value">The value from which to create a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified yottabit value.</returns>
    public static DataSize<T> FromYottaBits(T value)
    {
        return GetUnscaledBits(value, DecimalThousand, 8);
    }

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified yobibit value.
    /// </summary>
    /// <param name="value">The value from which to create a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified yobibit value.</returns>
    public static DataSize<T> FromYobiBits(T value)
    {
        return GetUnscaledBits(value, BinaryThousand, 8);
    }

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified yottabyte value.
    /// </summary>
    /// <param name="value">The value from which to create a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified yottabyte value.</returns>
    public static DataSize<T> FromYottaBytes(T value)
    {
        return GetUnscaledBytes(value, DecimalThousand, 8);
    }

    /// <summary>
    /// Creates a new <see cref="DataSize{T}"/> instance from the specified yobibyte value.
    /// </summary>
    /// <param name="value">The value from which to create a new <see cref="DataSize{T}"/> instance.</param>
    /// <returns>Returns a new <see cref="DataSize{T}"/> instance from the specified yobibyte value.</returns>
    public static DataSize<T> FromYobiBytes(T value)
    {
        return GetUnscaledBytes(value, BinaryThousand, 8);
    }

    /// <summary>
    /// Gets the number of bits in the specified value.
    /// </summary>
    /// <param name="value">The value from which to calculate the number of bits.</param>
    /// <param name="thousand">A value equalling either <see cref="DecimalThousand"/> or <see cref="BinaryThousand"/> to raise.</param>
    /// <param name="exponent">The exponent to raise the thousand value by.</param>
    /// <returns>Returns the number of bits in the specified value.</returns>
    private static DataSize<T> GetUnscaledBits(T value, T thousand, int exponent)
    {
        return new DataSize<T>(value * GenericMath.Pow(thousand, exponent));
    }

    /// <summary>
    /// Gets the number of bits in the specified value.
    /// </summary>
    /// <param name="value">The value from which to calculate the number of bits.</param>
    /// <param name="thousand">A value equalling either <see cref="DecimalThousand"/> or <see cref="BinaryThousand"/> to raise.</param>
    /// <param name="exponent">The exponent to raise the thousand value by.</param>
    /// <returns>Returns the number of bits in the specified value.</returns>
    private static DataSize<T> GetUnscaledBytes(T value, T thousand, int exponent)
    {
        return new DataSize<T>(value * (GenericMath.Pow(thousand, exponent) * BitsPerByte));
    }
}
