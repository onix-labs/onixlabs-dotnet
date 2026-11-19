// Copyright 2020 ONIXLabs
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
using System.Buffers.Binary;

namespace OnixLabs.Security.Cryptography;

/// <summary>
/// Represents an element of the prime field GF(p) used by secp256k1, where p = 2^256 - 2^32 - 977 (SEC 2 v2 §2.4.1).
/// Values are stored fully reduced, in [0, p), as four 64-bit limbs in little-endian order.
/// </summary>
/// <remarks>
/// Why: secp256k1 is only used here to verify signatures, so every value this type handles is public, and its operations are not constant
/// time; <see cref="Equals(Secp256k1FieldElement)"/>, for example, returns at the first difference. It must not be used with secret values,
/// such as for signing.
/// </remarks>
internal readonly struct Secp256k1FieldElement : IEquatable<Secp256k1FieldElement>
{
    /// <summary>
    /// The value 2^256 - p = 2^32 + 977, which reduction multiplies the high half of a product by, because 2^256 ≡ 2^32 + 977 (mod p).
    /// </summary>
    private const ulong ReductionConstant = 0x1000003D1;

    /// <summary>
    /// The least significant limb of the field prime p.
    /// </summary>
    private const ulong PrimeLimb0 = 0xFFFFFFFEFFFFFC2F;

    /// <summary>
    /// The additive identity (zero) of the field.
    /// </summary>
    public static readonly Secp256k1FieldElement Zero = new(0, 0, 0, 0);

    /// <summary>
    /// The multiplicative identity (one) of the field.
    /// </summary>
    public static readonly Secp256k1FieldElement One = new(1, 0, 0, 0);

    /// <summary>
    /// The exponent p - 2, as 32 big-endian bytes, by which <see cref="Invert"/> raises its argument.
    /// </summary>
    private static readonly byte[] InverseExponent =
        Convert.FromHexString("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFFC2D");

    /// <summary>
    /// The exponent (p + 1) / 4, as 32 big-endian bytes, by which <see cref="TrySquareRoot"/> raises its argument.
    /// </summary>
    private static readonly byte[] SquareRootExponent =
        Convert.FromHexString("3FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFBFFFFF0C");

    /// <summary>
    /// The least significant 64-bit limb.
    /// </summary>
    private readonly ulong l0;

    /// <summary>
    /// The second 64-bit limb.
    /// </summary>
    private readonly ulong l1;

    /// <summary>
    /// The third 64-bit limb.
    /// </summary>
    private readonly ulong l2;

    /// <summary>
    /// The most significant 64-bit limb.
    /// </summary>
    private readonly ulong l3;

    /// <summary>
    /// Initializes a new instance of the <see cref="Secp256k1FieldElement"/> struct from limbs that are already reduced.
    /// </summary>
    /// <param name="l0">The least significant 64-bit limb.</param>
    /// <param name="l1">The second 64-bit limb.</param>
    /// <param name="l2">The third 64-bit limb.</param>
    /// <param name="l3">The most significant 64-bit limb.</param>
    private Secp256k1FieldElement(ulong l0, ulong l1, ulong l2, ulong l3)
    {
        this.l0 = l0;
        this.l1 = l1;
        this.l2 = l2;
        this.l3 = l3;
    }

    /// <summary>
    /// Gets a value indicating whether this field element is zero.
    /// </summary>
    /// <value><see langword="true"/> if this field element is zero; otherwise, <see langword="false"/>.</value>
    public bool IsZero => (l0 | l1 | l2 | l3) == 0;

    /// <summary>
    /// Gets a value indicating whether this field element, as an integer in [0, p), is odd.
    /// </summary>
    /// <value><see langword="true"/> if the least significant bit is one; otherwise, <see langword="false"/>.</value>
    public bool IsOdd => (l0 & 1) == 1;

    /// <summary>
    /// Computes the sum of the specified field elements.
    /// </summary>
    /// <param name="left">The value to add to.</param>
    /// <param name="right">The value to add.</param>
    /// <returns>Returns the sum of the specified field elements modulo p.</returns>
    public static Secp256k1FieldElement operator +(in Secp256k1FieldElement left, in Secp256k1FieldElement right)
    {
        ulong carry = 0;
        ulong r0 = AddWithCarry(left.l0, right.l0, ref carry);
        ulong r1 = AddWithCarry(left.l1, right.l1, ref carry);
        ulong r2 = AddWithCarry(left.l2, right.l2, ref carry);
        ulong r3 = AddWithCarry(left.l3, right.l3, ref carry);

        return Reduce(r0, r1, r2, r3, carry);
    }

    /// <summary>
    /// Computes the difference of the specified field elements.
    /// </summary>
    /// <param name="left">The value to subtract from.</param>
    /// <param name="right">The value to subtract.</param>
    /// <returns>Returns the difference of the specified field elements modulo p.</returns>
    public static Secp256k1FieldElement operator -(in Secp256k1FieldElement left, in Secp256k1FieldElement right)
    {
        ulong borrow = 0;
        ulong r0 = SubtractWithBorrow(left.l0, right.l0, ref borrow);
        ulong r1 = SubtractWithBorrow(left.l1, right.l1, ref borrow);
        ulong r2 = SubtractWithBorrow(left.l2, right.l2, ref borrow);
        ulong r3 = SubtractWithBorrow(left.l3, right.l3, ref borrow);

        if (borrow == 0) return new Secp256k1FieldElement(r0, r1, r2, r3);

        // Why: a borrow means the true result is r - 2^256; adding p is the same as subtracting 2^256 - p, the reduction constant.
        borrow = 0;
        r0 = SubtractWithBorrow(r0, ReductionConstant, ref borrow);
        r1 = SubtractWithBorrow(r1, 0, ref borrow);
        r2 = SubtractWithBorrow(r2, 0, ref borrow);
        r3 = SubtractWithBorrow(r3, 0, ref borrow);

        return new Secp256k1FieldElement(r0, r1, r2, r3);
    }

    /// <summary>
    /// Computes the product of the specified field elements.
    /// </summary>
    /// <param name="left">The value to multiply.</param>
    /// <param name="right">The value to multiply by.</param>
    /// <returns>Returns the product of the specified field elements modulo p.</returns>
    /// <remarks>
    /// Why: the 4 × 4 schoolbook product is unrolled, with <see cref="Math.BigMul(ulong, ulong, out ulong)"/> and explicit carries, so that
    /// every
    /// limb stays in a register. It is the inner operation of verification. The previous loop over spans of <see cref="UInt128"/> made
    /// secp256k1 verification markedly slower; the result is the same 512-bit product.
    /// </remarks>
    public static Secp256k1FieldElement operator *(in Secp256k1FieldElement left, in Secp256k1FieldElement right)
    {
        ulong a0 = left.l0, a1 = left.l1, a2 = left.l2, a3 = left.l3;
        ulong b0 = right.l0, b1 = right.l1, b2 = right.l2, b3 = right.l3;

        // @formatter:off
        ulong c = MultiplyAdd(a0, b0, 0, 0, out ulong r0);
        c = MultiplyAdd(a0, b1, c, 0, out ulong r1);
        c = MultiplyAdd(a0, b2, c, 0, out ulong r2);
        ulong r4 = MultiplyAdd(a0, b3, c, 0, out ulong r3);

        c = MultiplyAdd(a1, b0, r1, 0, out r1);
        c = MultiplyAdd(a1, b1, r2, c, out r2);
        c = MultiplyAdd(a1, b2, r3, c, out r3);
        ulong r5 = MultiplyAdd(a1, b3, r4, c, out r4);

        c = MultiplyAdd(a2, b0, r2, 0, out r2);
        c = MultiplyAdd(a2, b1, r3, c, out r3);
        c = MultiplyAdd(a2, b2, r4, c, out r4);
        ulong r6 = MultiplyAdd(a2, b3, r5, c, out r5);

        c = MultiplyAdd(a3, b0, r3, 0, out r3);
        c = MultiplyAdd(a3, b1, r4, c, out r4);
        c = MultiplyAdd(a3, b2, r5, c, out r5);
        ulong r7 = MultiplyAdd(a3, b3, r6, c, out r6);

        // Why: 2^256 ≡ c (mod p), so the 512-bit product hi * 2^256 + lo reduces to lo + hi * c,
        // a 290-bit value whose top limb is then folded the same way.
        c = MultiplyAdd(r4, ReductionConstant, r0, 0, out r0);
        c = MultiplyAdd(r5, ReductionConstant, r1, c, out r1);
        c = MultiplyAdd(r6, ReductionConstant, r2, c, out r2);
        c = MultiplyAdd(r7, ReductionConstant, r3, c, out r3);
        // @formatter:on

        return Reduce(r0, r1, r2, r3, c);
    }

    /// <summary>
    /// Computes the square of the specified field element.
    /// </summary>
    /// <param name="value">The field element to square.</param>
    /// <returns>Returns the square of the specified field element modulo p.</returns>
    public static Secp256k1FieldElement Square(in Secp256k1FieldElement value) => value * value;

    /// <summary>
    /// Computes the specified field element raised to the specified power, by square-and-multiply. Not constant time.
    /// </summary>
    /// <param name="value">The base field element.</param>
    /// <param name="exponent">The 256-bit exponent, as 32 big-endian bytes.</param>
    /// <returns>Returns the field element raised to the specified power, modulo p.</returns>
    public static Secp256k1FieldElement Power(in Secp256k1FieldElement value, ReadOnlySpan<byte> exponent)
    {
        Secp256k1FieldElement result = One;

        foreach (byte octet in exponent)
        {
            for (int bit = 7; bit >= 0; bit--)
            {
                result = Square(result);
                if (((octet >> bit) & 1) == 1) result *= value;
            }
        }

        return result;
    }

    /// <summary>
    /// Computes the multiplicative inverse of the specified field element as value^(p - 2), by Fermat's little theorem.
    /// </summary>
    /// <param name="value">The field element to invert; zero inverts to zero.</param>
    /// <returns>Returns the multiplicative inverse of the specified field element modulo p.</returns>
    public static Secp256k1FieldElement Invert(in Secp256k1FieldElement value) =>
        Power(value, InverseExponent);

    /// <summary>
    /// Attempts to compute a square root of the specified field element as value^((p + 1) / 4), which is valid because p ≡ 3 (mod 4).
    /// </summary>
    /// <param name="value">The field element whose square root to compute.</param>
    /// <param name="root">When this method returns, contains a square root of <paramref name="value"/>, if one exists.</param>
    /// <returns>Returns <see langword="true"/> if <paramref name="value"/> is a square; otherwise, <see langword="false"/>.</returns>
    public static bool TrySquareRoot(in Secp256k1FieldElement value, out Secp256k1FieldElement root)
    {
        root = Power(value, SquareRootExponent);
        return Square(root) == value;
    }

    /// <summary>
    /// Attempts to decode a field element from 32 big-endian bytes, as SEC 1 v2 §2.3.6 defines.
    /// </summary>
    /// <param name="bytes">The 32 big-endian bytes to decode.</param>
    /// <param name="element">When this method returns, contains the decoded field element, if the bytes represent one.</param>
    /// <returns>Returns <see langword="true"/> if the bytes are 32 long and represent an integer in [0, p); otherwise,
    /// <see langword="false"/>.</returns>
    public static bool TryFromBytes(ReadOnlySpan<byte> bytes, out Secp256k1FieldElement element)
    {
        element = Zero;

        if (bytes.Length != 32) return false;

        ulong l3 = BinaryPrimitives.ReadUInt64BigEndian(bytes[..8]);
        ulong l2 = BinaryPrimitives.ReadUInt64BigEndian(bytes[8..16]);
        ulong l1 = BinaryPrimitives.ReadUInt64BigEndian(bytes[16..24]);
        ulong l0 = BinaryPrimitives.ReadUInt64BigEndian(bytes[24..32]);

        if (IsAtLeastPrime(l0, l1, l2, l3)) return false;

        element = new Secp256k1FieldElement(l0, l1, l2, l3);
        return true;
    }

    /// <summary>
    /// Writes this field element as 32 big-endian bytes.
    /// </summary>
    /// <param name="destination">The 32-byte destination buffer.</param>
    public void WriteBytes(Span<byte> destination)
    {
        BinaryPrimitives.WriteUInt64BigEndian(destination[..8], l3);
        BinaryPrimitives.WriteUInt64BigEndian(destination[8..16], l2);
        BinaryPrimitives.WriteUInt64BigEndian(destination[16..24], l1);
        BinaryPrimitives.WriteUInt64BigEndian(destination[24..32], l0);
    }

    /// <summary>
    /// Determines whether the specified field elements are equal.
    /// </summary>
    /// <param name="left">The first field element to compare.</param>
    /// <param name="right">The second field element to compare.</param>
    /// <returns>Returns <see langword="true"/> if the field elements are equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(in Secp256k1FieldElement left, in Secp256k1FieldElement right) => left.Equals(right);

    /// <summary>
    /// Determines whether the specified field elements are not equal.
    /// </summary>
    /// <param name="left">The first field element to compare.</param>
    /// <param name="right">The second field element to compare.</param>
    /// <returns>Returns <see langword="true"/> if the field elements are not equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(in Secp256k1FieldElement left, in Secp256k1FieldElement right) => !left.Equals(right);

    /// <inheritdoc/>
    public bool Equals(Secp256k1FieldElement other) => l0 == other.l0 && l1 == other.l1 && l2 == other.l2 && l3 == other.l3;

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is Secp256k1FieldElement other && Equals(other);

    /// <inheritdoc/>
    public override int GetHashCode() => HashCode.Combine(l0, l1, l2, l3);

    /// <summary>
    /// Reduces a value of up to 320 bits, given as four limbs and a small top limb, to its representative in [0, p).
    /// </summary>
    /// <param name="r0">The least significant limb.</param>
    /// <param name="r1">The second limb.</param>
    /// <param name="r2">The third limb.</param>
    /// <param name="r3">The fourth limb.</param>
    /// <param name="top">The limb above the fourth, which must be less than 2^34.</param>
    /// <returns>Returns the reduced field element.</returns>
    private static Secp256k1FieldElement Reduce(ulong r0, ulong r1, ulong r2, ulong r3, ulong top)
    {
        while (top != 0)
        {
            ulong carry = 0;
            UInt128 term = ((UInt128)top * ReductionConstant) + r0;
            r0 = (ulong)term;
            carry = (ulong)(term >> 64);
            r1 = AddWithCarry(r1, 0, ref carry);
            r2 = AddWithCarry(r2, 0, ref carry);
            r3 = AddWithCarry(r3, 0, ref carry);
            top = carry;
        }

        if (IsAtLeastPrime(r0, r1, r2, r3))
        {
            ulong carry = 0;
            r0 = AddWithCarry(r0, ReductionConstant, ref carry);
            r1 = AddWithCarry(r1, 0, ref carry);
            r2 = AddWithCarry(r2, 0, ref carry);
            r3 = AddWithCarry(r3, 0, ref carry);
        }

        return new Secp256k1FieldElement(r0, r1, r2, r3);
    }

    /// <summary>
    /// Determines whether the 256-bit value with the specified limbs is at least p.
    /// </summary>
    /// <param name="r0">The least significant limb.</param>
    /// <param name="r1">The second limb.</param>
    /// <param name="r2">The third limb.</param>
    /// <param name="r3">The most significant limb.</param>
    /// <returns>Returns <see langword="true"/> if the value is in [p, 2^256); otherwise, <see langword="false"/>.</returns>
    private static bool IsAtLeastPrime(ulong r0, ulong r1, ulong r2, ulong r3) =>
        r3 == ulong.MaxValue && r2 == ulong.MaxValue && r1 == ulong.MaxValue && r0 >= PrimeLimb0;

    /// <summary>
    /// Computes the 128-bit value <paramref name="left"/> · <paramref name="right"/> + <paramref name="first"/> +
    /// <paramref name="second"/>,
    /// which cannot overflow.
    /// </summary>
    /// <param name="left">The first factor.</param>
    /// <param name="right">The second factor.</param>
    /// <param name="first">The first addend.</param>
    /// <param name="second">The second addend.</param>
    /// <param name="low">When this method returns, contains the low 64 bits of the result.</param>
    /// <returns>Returns the high 64 bits of the result.</returns>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    private static ulong MultiplyAdd(ulong left, ulong right, ulong first, ulong second, out ulong low)
    {
        ulong high = Math.BigMul(left, right, out low);

        low += first;
        high += low < first ? 1UL : 0UL;
        low += second;
        high += low < second ? 1UL : 0UL;

        return high;
    }

    /// <summary>
    /// Adds two limbs and an incoming carry.
    /// </summary>
    /// <param name="left">The first limb.</param>
    /// <param name="right">The second limb.</param>
    /// <param name="carry">The incoming carry, 0 or 1, which receives the outgoing carry.</param>
    /// <returns>Returns the low 64 bits of the sum.</returns>
    private static ulong AddWithCarry(ulong left, ulong right, ref ulong carry)
    {
        UInt128 sum = (UInt128)left + right + carry;
        carry = (ulong)(sum >> 64);
        return (ulong)sum;
    }

    /// <summary>
    /// Subtracts a limb and an incoming borrow from another limb.
    /// </summary>
    /// <param name="left">The limb to subtract from.</param>
    /// <param name="right">The limb to subtract.</param>
    /// <param name="borrow">The incoming borrow, 0 or 1, which receives the outgoing borrow.</param>
    /// <returns>Returns the low 64 bits of the difference.</returns>
    private static ulong SubtractWithBorrow(ulong left, ulong right, ref ulong borrow)
    {
        UInt128 difference = (UInt128)left - right - borrow;
        borrow = (ulong)(difference >> 127);
        return (ulong)difference;
    }
}
