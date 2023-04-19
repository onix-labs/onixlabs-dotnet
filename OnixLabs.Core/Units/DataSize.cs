// Copyright 2020-2023 ONIXLabs
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
using OnixLabs.Core.Numerics;
using static OnixLabs.Core.Preconditions;

namespace OnixLabs.Core.Units;

/// <summary>
/// Represents a unit of DataSize.
/// </summary>
/// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> type that represents the current <see cref="DataSize{T}"/> unit.</typeparam>
public readonly partial struct DataSize<T> where T : IFloatingPoint<T>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DataSize{T}" /> struct.
    /// </summary>
    /// <param name="bits">The initial DataSize value in Bits.</param>
    private DataSize(T bits)
    {
        Require(bits >= T.Zero, $"Cannot initialize {nameof(DataSize)} from negative value.", nameof(bits));
        Bits = bits;
    }

    /// <summary>
    /// Gets the current <see cref="DataSize{T}"/> in bits.
    /// </summary>
    public T Bits { get; }

    /// <summary>
    /// Gets the current <see cref="DataSize{T}"/> in nibbles.
    /// </summary>
    public T Nibbles => Bits / BitsPerNibble;

    /// <summary>
    /// Gets the current <see cref="DataSize{T}"/> in bytes.
    /// </summary>
    public T Bytes => Bits / BitsPerByte;

    /// <summary>
    /// Gets the current <see cref="DataSize{T}"/> in words.
    /// </summary>
    public T Words => Bits / BitsPerWord;

    /// <summary>
    /// Gets the current <see cref="DataSize{T}"/> in double-words.
    /// </summary>
    public T DoubleWords => Bits / BitsPerDoubleWord;

    /// <summary>
    /// Gets the current <see cref="DataSize{T}"/> in quad-words.
    /// </summary>
    public T QuadWords => Bits / BitsPerQuadWord;

    /// <summary>
    /// Gets the current <see cref="DataSize{T}"/> in kilobits.
    /// </summary>
    public T KiloBits => GetScaledBits(DecimalThousand, 1);

    /// <summary>
    /// Gets the current <see cref="DataSize{T}"/> in kibibits.
    /// </summary>
    public T KibiBits => GetScaledBits(BinaryThousand, 1);

    /// <summary>
    /// Gets the current <see cref="DataSize{T}"/> in kilobytes.
    /// </summary>
    public T KiloBytes => GetScaledBytes(DecimalThousand, 1);

    /// <summary>
    /// Gets the current <see cref="DataSize{T}"/> in kibibytes.
    /// </summary>
    public T KibiBytes => GetScaledBytes(BinaryThousand, 1);

    /// <summary>
    /// Gets the current <see cref="DataSize{T}"/> in megabits.
    /// </summary>
    public T MegaBits => GetScaledBits(DecimalThousand, 2);

    /// <summary>
    /// Gets the current <see cref="DataSize{T}"/> in mebibits.
    /// </summary>
    public T MebiBits => GetScaledBits(BinaryThousand, 2);

    /// <summary>
    /// Gets the current <see cref="DataSize{T}"/> in megabytes.
    /// </summary>
    public T MegaBytes => GetScaledBytes(DecimalThousand, 2);

    /// <summary>
    /// Gets the current <see cref="DataSize{T}"/> in mebibytes.
    /// </summary>
    public T MebiBytes => GetScaledBytes(BinaryThousand, 2);

    /// <summary>
    /// Gets the current <see cref="DataSize{T}"/> in gigabits.
    /// </summary>
    public T GigaBits => GetScaledBits(DecimalThousand, 3);

    /// <summary>
    /// Gets the current <see cref="DataSize{T}"/> in gibibits.
    /// </summary>
    public T GibiBits => GetScaledBits(BinaryThousand, 3);

    /// <summary>
    /// Gets the current <see cref="DataSize{T}"/> in gigbytes.
    /// </summary>
    public T GigaBytes => GetScaledBytes(DecimalThousand, 3);

    /// <summary>
    /// Gets the current <see cref="DataSize{T}"/> in gibibytes.
    /// </summary>
    public T GibiBytes => GetScaledBytes(BinaryThousand, 3);

    /// <summary>
    /// Gets the current <see cref="DataSize{T}"/> in terabits.
    /// </summary>
    public T TeraBits => GetScaledBits(DecimalThousand, 4);

    /// <summary>
    /// Gets the current <see cref="DataSize{T}"/> in tebibits.
    /// </summary>
    public T TebiBits => GetScaledBits(BinaryThousand, 4);

    /// <summary>
    /// Gets the current <see cref="DataSize{T}"/> in terabytes.
    /// </summary>
    public T TeraBytes => GetScaledBytes(DecimalThousand, 4);

    /// <summary>
    /// Gets the current <see cref="DataSize{T}"/> in tebibytes.
    /// </summary>
    public T TebiBytes => GetScaledBytes(BinaryThousand, 4);

    /// <summary>
    /// Gets the current <see cref="DataSize{T}"/> in petabits.
    /// </summary>
    public T PetaBits => GetScaledBits(DecimalThousand, 5);

    /// <summary>
    /// Gets the current <see cref="DataSize{T}"/> in pebibits.
    /// </summary>
    public T PebiBits => GetScaledBits(BinaryThousand, 5);

    /// <summary>
    /// Gets the current <see cref="DataSize{T}"/> in petabytes.
    /// </summary>
    public T PetaBytes => GetScaledBytes(DecimalThousand, 5);

    /// <summary>
    /// Gets the current <see cref="DataSize{T}"/> in pebibytes.
    /// </summary>
    public T PebiBytes => GetScaledBytes(BinaryThousand, 5);

    /// <summary>
    /// Gets the current <see cref="DataSize{T}"/> in exabits.
    /// </summary>
    public T ExaBits => GetScaledBits(DecimalThousand, 6);

    /// <summary>
    /// Gets the current <see cref="DataSize{T}"/> in exbibits.
    /// </summary>
    public T ExbiBits => GetScaledBits(BinaryThousand, 6);

    /// <summary>
    /// Gets the current <see cref="DataSize{T}"/> in exabytes.
    /// </summary>
    public T ExaBytes => GetScaledBytes(DecimalThousand, 6);

    /// <summary>
    /// Gets the current <see cref="DataSize{T}"/> in exbibytes.
    /// </summary>
    public T ExbiBytes => GetScaledBytes(BinaryThousand, 6);

    /// <summary>
    /// Gets the current <see cref="DataSize{T}"/> in zettabits.
    /// </summary>
    public T ZettaBits => GetScaledBits(DecimalThousand, 7);

    /// <summary>
    /// Gets the current <see cref="DataSize{T}"/> in zebibits.
    /// </summary>
    public T ZebiBits => GetScaledBits(BinaryThousand, 7);

    /// <summary>
    /// Gets the current <see cref="DataSize{T}"/> in zettabytes.
    /// </summary>
    public T ZettaBytes => GetScaledBytes(DecimalThousand, 7);

    /// <summary>
    /// Gets the current <see cref="DataSize{T}"/> in zebibytes.
    /// </summary>
    public T ZebiBytes => GetScaledBytes(BinaryThousand, 7);

    /// <summary>
    /// Gets the current <see cref="DataSize{T}"/> in yottabits.
    /// </summary>
    public T YottaBits => GetScaledBits(DecimalThousand, 8);

    /// <summary>
    /// Gets the current <see cref="DataSize{T}"/> in yobibits.
    /// </summary>
    public T YobiBits => GetScaledBits(BinaryThousand, 8);

    /// <summary>
    /// Gets the current <see cref="DataSize{T}"/> in yottabytes.
    /// </summary>
    public T YottaBytes => GetScaledBytes(DecimalThousand, 8);

    /// <summary>
    /// Gets the current <see cref="DataSize{T}"/> in yobibytes.
    /// </summary>
    public T YobiBytes => GetScaledBytes(BinaryThousand, 8);

    /// <summary>
    /// Calculates the current <see cref="Bits"/> divided by the specified power.
    /// </summary>
    /// <param name="thousand">A value equalling either <see cref="DecimalThousand"/> or <see cref="BinaryThousand"/> to raise.</param>
    /// <param name="exponent">The exponent to raise the thousand value by.</param>
    /// <returns>Returns the current <see cref="Bits"/> divided by the specified power.</returns>
    private T GetScaledBits(T thousand, int exponent)
    {
        return Bits / GenericMath.Pow(thousand, exponent);
    }

    /// <summary>
    /// Calculates the current <see cref="Bytes"/> divided by the specified power.
    /// </summary>
    /// <param name="thousand">A value equalling either <see cref="DecimalThousand"/> or <see cref="BinaryThousand"/> to raise.</param>
    /// <param name="exponent">The exponent to raise the thousand value by.</param>
    /// <returns>Returns the current <see cref="Bytes"/> divided by the specified power.</returns>
    private T GetScaledBytes(T thousand, int exponent)
    {
        return Bits / (GenericMath.Pow(thousand, exponent) * BitsPerByte);
    }
}
