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

namespace OnixLabs.Numerics.UnitTests.CrossValidation;

/// <summary>
/// IEEE 754 binary128 special-value conformance pass for <see cref="Float128"/>. Covers the
/// behaviour the spec mandates for the universal corner cases — NaN propagation, ±∞ arithmetic,
/// signed zero, sqrt boundaries, and comparison rules involving NaN. These tests do not exercise
/// rounding precision; that's a separate, larger investment.
/// </summary>
public sealed class Float128SpecialValueTests
{
    private static readonly Float128 Two = Float128.One + Float128.One;
    private static readonly Float128 Three = Float128.One + Two;

    // ===== NaN propagation =====

    [Fact(DisplayName = "Float128: NaN + x is NaN for any finite x")]
    public void NaNPlusFiniteIsNaN()
    {
        Assert.True(Float128.IsNaN(Float128.NaN + Float128.One));
        Assert.True(Float128.IsNaN(Float128.NaN + Float128.Zero));
        Assert.True(Float128.IsNaN(Float128.NaN + Float128.MaxValue));
        Assert.True(Float128.IsNaN(Float128.NaN + Float128.NegativeOne));
    }

    [Fact(DisplayName = "Float128: NaN - x and x - NaN are both NaN")]
    public void NaNSubtractionPropagates()
    {
        Assert.True(Float128.IsNaN(Float128.NaN - Float128.One));
        Assert.True(Float128.IsNaN(Float128.One - Float128.NaN));
    }

    [Fact(DisplayName = "Float128: NaN * x is NaN, including NaN * 0")]
    public void NaNMultiplicationPropagates()
    {
        Assert.True(Float128.IsNaN(Float128.NaN * Float128.One));
        Assert.True(Float128.IsNaN(Float128.NaN * Float128.Zero));
        Assert.True(Float128.IsNaN(Float128.NaN * Float128.PositiveInfinity));
    }

    [Fact(DisplayName = "Float128: NaN / x and x / NaN are both NaN")]
    public void NaNDivisionPropagates()
    {
        Assert.True(Float128.IsNaN(Float128.NaN / Float128.One));
        Assert.True(Float128.IsNaN(Float128.One / Float128.NaN));
        Assert.True(Float128.IsNaN(Float128.NaN / Float128.Zero));
    }

    [Fact(DisplayName = "Float128: NaN + NaN is NaN")]
    public void NaNPlusNaNIsNaN()
    {
        Assert.True(Float128.IsNaN(Float128.NaN + Float128.NaN));
    }

    // ===== NaN comparison rules =====

    [Fact(DisplayName = "Float128: NaN is not equal to itself (IEEE 754 §5.11)")]
    public void NaNIsNotEqualToItself()
    {
        Assert.False(Float128.NaN == Float128.NaN);
        Assert.True(Float128.NaN != Float128.NaN);
    }

    [Fact(DisplayName = "Float128: all ordering comparisons involving NaN return false")]
    public void NaNOrderingComparisonsReturnFalse()
    {
        Assert.False(Float128.NaN < Float128.One);
        Assert.False(Float128.NaN > Float128.One);
        Assert.False(Float128.NaN <= Float128.One);
        Assert.False(Float128.NaN >= Float128.One);
        Assert.False(Float128.One < Float128.NaN);
        Assert.False(Float128.One > Float128.NaN);
    }

    // ===== Infinity arithmetic =====

    [Fact(DisplayName = "Float128: ∞ + ∞ = ∞")]
    public void PositiveInfinityPlusItselfIsInfinity()
    {
        Assert.True(Float128.IsPositiveInfinity(Float128.PositiveInfinity + Float128.PositiveInfinity));
    }

    [Fact(DisplayName = "Float128: ∞ - ∞ = NaN")]
    public void PositiveInfinityMinusItselfIsNaN()
    {
        Assert.True(Float128.IsNaN(Float128.PositiveInfinity - Float128.PositiveInfinity));
    }

    [Fact(DisplayName = "Float128: -∞ + ∞ = NaN")]
    public void NegativeInfinityPlusPositiveInfinityIsNaN()
    {
        Assert.True(Float128.IsNaN(Float128.NegativeInfinity + Float128.PositiveInfinity));
    }

    [Fact(DisplayName = "Float128: ∞ × 0 = NaN")]
    public void InfinityTimesZeroIsNaN()
    {
        Assert.True(Float128.IsNaN(Float128.PositiveInfinity * Float128.Zero));
        Assert.True(Float128.IsNaN(Float128.NegativeInfinity * Float128.Zero));
    }

    [Fact(DisplayName = "Float128: ∞ × finite preserves sign and is infinite")]
    public void InfinityTimesFiniteIsInfinity()
    {
        Assert.True(Float128.IsPositiveInfinity(Float128.PositiveInfinity * Float128.One));
        Assert.True(Float128.IsNegativeInfinity(Float128.PositiveInfinity * Float128.NegativeOne));
        Assert.True(Float128.IsNegativeInfinity(Float128.NegativeInfinity * Float128.One));
        Assert.True(Float128.IsPositiveInfinity(Float128.NegativeInfinity * Float128.NegativeOne));
    }

    [Fact(DisplayName = "Float128: ∞ / ∞ = NaN")]
    public void InfinityDividedByInfinityIsNaN()
    {
        Assert.True(Float128.IsNaN(Float128.PositiveInfinity / Float128.PositiveInfinity));
        Assert.True(Float128.IsNaN(Float128.PositiveInfinity / Float128.NegativeInfinity));
    }

    [Fact(DisplayName = "Float128: finite / 0 is signed infinity")]
    public void FiniteDividedByZeroIsSignedInfinity()
    {
        Assert.True(Float128.IsPositiveInfinity(Float128.One / Float128.Zero));
        Assert.True(Float128.IsNegativeInfinity(Float128.NegativeOne / Float128.Zero));
        Assert.True(Float128.IsNegativeInfinity(Float128.One / Float128.NegativeZero));
        Assert.True(Float128.IsPositiveInfinity(Float128.NegativeOne / Float128.NegativeZero));
    }

    [Fact(DisplayName = "Float128: 0 / 0 = NaN")]
    public void ZeroDividedByZeroIsNaN()
    {
        Assert.True(Float128.IsNaN(Float128.Zero / Float128.Zero));
        Assert.True(Float128.IsNaN(Float128.Zero / Float128.NegativeZero));
        Assert.True(Float128.IsNaN(Float128.NegativeZero / Float128.Zero));
    }

    [Fact(DisplayName = "Float128: finite / ∞ = signed zero")]
    public void FiniteDividedByInfinityIsZero()
    {
        Float128 result = Float128.One / Float128.PositiveInfinity;
        Assert.True(Float128.IsZero(result));
        Assert.True(Float128.IsPositive(result));

        Float128 negResult = Float128.One / Float128.NegativeInfinity;
        Assert.True(Float128.IsZero(negResult));
        Assert.True(Float128.IsNegative(negResult));
    }

    // ===== Signed zero =====

    [Fact(DisplayName = "Float128: +0 == -0 but they have distinct signs")]
    public void PositiveAndNegativeZeroCompareEqualButHaveDistinctSigns()
    {
        Assert.True(Float128.Zero == Float128.NegativeZero);
        Assert.True(Float128.IsPositive(Float128.Zero));
        Assert.True(Float128.IsNegative(Float128.NegativeZero));
    }

    [Fact(DisplayName = "Float128: 1 / +0 = +∞, 1 / -0 = -∞")]
    public void OneDividedBySignedZeroProducesSignedInfinity()
    {
        Assert.True(Float128.IsPositiveInfinity(Float128.One / Float128.Zero));
        Assert.True(Float128.IsNegativeInfinity(Float128.One / Float128.NegativeZero));
    }

    [Fact(DisplayName = "Float128: -0 + +0 = +0 (under round-to-nearest)")]
    public void NegativeZeroPlusPositiveZeroIsPositiveZero()
    {
        Float128 result = Float128.NegativeZero + Float128.Zero;
        Assert.True(Float128.IsZero(result));
        Assert.True(Float128.IsPositive(result));
    }

    [Fact(DisplayName = "Float128: x + (-x) = +0 for any finite x")]
    public void XPlusNegativeXIsPositiveZero()
    {
        Float128 result = Float128.One + Float128.NegativeOne;
        Assert.True(Float128.IsZero(result));
        Assert.True(Float128.IsPositive(result));
    }

    [Fact(DisplayName = "Float128: -0 × +0 = -0")]
    public void NegativeZeroTimesPositiveZeroIsNegativeZero()
    {
        Float128 result = Float128.NegativeZero * Float128.Zero;
        Assert.True(Float128.IsZero(result));
        Assert.True(Float128.IsNegative(result));
    }

    // ===== Sqrt edge cases =====

    [Fact(DisplayName = "Float128: sqrt(NaN) = NaN")]
    public void SqrtOfNaNIsNaN()
    {
        Assert.True(Float128.IsNaN(Float128.Sqrt(Float128.NaN)));
    }

    [Fact(DisplayName = "Float128: sqrt(-1) = NaN")]
    public void SqrtOfNegativeIsNaN()
    {
        Assert.True(Float128.IsNaN(Float128.Sqrt(Float128.NegativeOne)));
    }

    [Fact(DisplayName = "Float128: sqrt(-∞) = NaN")]
    public void SqrtOfNegativeInfinityIsNaN()
    {
        Assert.True(Float128.IsNaN(Float128.Sqrt(Float128.NegativeInfinity)));
    }

    [Fact(DisplayName = "Float128: sqrt(+∞) = +∞")]
    public void SqrtOfPositiveInfinityIsPositiveInfinity()
    {
        Assert.True(Float128.IsPositiveInfinity(Float128.Sqrt(Float128.PositiveInfinity)));
    }

    [Fact(DisplayName = "Float128: sqrt(+0) = +0, sqrt(-0) = -0 (IEEE 754 §6.3)")]
    public void SqrtOfSignedZeroPreservesSign()
    {
        Float128 sqrtPosZero = Float128.Sqrt(Float128.Zero);
        Assert.True(Float128.IsZero(sqrtPosZero));
        Assert.True(Float128.IsPositive(sqrtPosZero));

        Float128 sqrtNegZero = Float128.Sqrt(Float128.NegativeZero);
        Assert.True(Float128.IsZero(sqrtNegZero));
        Assert.True(Float128.IsNegative(sqrtNegZero));
    }

    [Fact(DisplayName = "Float128: sqrt(1) = 1, sqrt(4) = 2, sqrt(9) = 3 (exactly representable)")]
    public void SqrtOfPerfectSquares()
    {
        Assert.Equal(Float128.One, Float128.Sqrt(Float128.One));
        Assert.Equal(Two, Float128.Sqrt(Float128.One + Three));
        Assert.Equal(Three, Float128.Sqrt(Three * Three));
    }

    // ===== Predicate self-consistency =====

    [Fact(DisplayName = "Float128: IsNaN is true only for NaN values")]
    public void IsNaNIsOnlyTrueForNaN()
    {
        Assert.True(Float128.IsNaN(Float128.NaN));
        Assert.False(Float128.IsNaN(Float128.Zero));
        Assert.False(Float128.IsNaN(Float128.One));
        Assert.False(Float128.IsNaN(Float128.PositiveInfinity));
        Assert.False(Float128.IsNaN(Float128.NegativeInfinity));
        Assert.False(Float128.IsNaN(Float128.MaxValue));
        Assert.False(Float128.IsNaN(Float128.Epsilon));
    }

    [Fact(DisplayName = "Float128: IsFinite excludes NaN and infinities")]
    public void IsFiniteExcludesNaNAndInfinities()
    {
        Assert.False(Float128.IsFinite(Float128.NaN));
        Assert.False(Float128.IsFinite(Float128.PositiveInfinity));
        Assert.False(Float128.IsFinite(Float128.NegativeInfinity));
        Assert.True(Float128.IsFinite(Float128.Zero));
        Assert.True(Float128.IsFinite(Float128.MaxValue));
        Assert.True(Float128.IsFinite(Float128.Epsilon));
    }

    [Fact(DisplayName = "Float128: IsZero is true for both +0 and -0 only")]
    public void IsZeroAcceptsBothSignedZeros()
    {
        Assert.True(Float128.IsZero(Float128.Zero));
        Assert.True(Float128.IsZero(Float128.NegativeZero));
        Assert.False(Float128.IsZero(Float128.Epsilon));
        Assert.False(Float128.IsZero(Float128.One));
    }

    [Fact(DisplayName = "Float128: IsSubnormal is true for Epsilon and false for One, Zero, and special values")]
    public void IsSubnormalPredicate()
    {
        Assert.True(Float128.IsSubnormal(Float128.Epsilon));
        Assert.False(Float128.IsSubnormal(Float128.One));
        Assert.False(Float128.IsSubnormal(Float128.Zero));
        Assert.False(Float128.IsSubnormal(Float128.NaN));
        Assert.False(Float128.IsSubnormal(Float128.PositiveInfinity));
    }

    [Fact(DisplayName = "Float128: IsNormal is true for One and MaxValue, false for Zero, Epsilon, and special values")]
    public void IsNormalPredicate()
    {
        Assert.True(Float128.IsNormal(Float128.One));
        Assert.True(Float128.IsNormal(Float128.MaxValue));
        Assert.False(Float128.IsNormal(Float128.Zero));
        Assert.False(Float128.IsNormal(Float128.Epsilon));
        Assert.False(Float128.IsNormal(Float128.NaN));
        Assert.False(Float128.IsNormal(Float128.PositiveInfinity));
    }

    // ===== Sign of zero through arithmetic =====

    [Fact(DisplayName = "Float128: positive × negative = negative (including zero results)")]
    public void SignOfMultiplicationFollowsAlgebra()
    {
        Float128 result = Float128.One * Float128.NegativeOne;
        Assert.True(Float128.IsNegative(result));
    }

    [Fact(DisplayName = "Float128: -1 × -1 = +1")]
    public void NegativeTimesNegativeIsPositive()
    {
        Assert.Equal(Float128.One, Float128.NegativeOne * Float128.NegativeOne);
    }

    // ===== Infinity in addition / subtraction =====

    [Fact(DisplayName = "Float128: ∞ + finite = ∞")]
    public void InfinityPlusFiniteIsInfinity()
    {
        Assert.True(Float128.IsPositiveInfinity(Float128.PositiveInfinity + Float128.MaxValue));
        Assert.True(Float128.IsPositiveInfinity(Float128.PositiveInfinity + Float128.NegativeOne));
    }

    [Fact(DisplayName = "Float128: finite - ∞ = -∞")]
    public void FiniteMinusInfinityIsNegativeInfinity()
    {
        Assert.True(Float128.IsNegativeInfinity(Float128.One - Float128.PositiveInfinity));
    }

    // ===== Abs =====

    [Fact(DisplayName = "Float128: Abs(-0) = +0")]
    public void AbsOfNegativeZeroIsPositiveZero()
    {
        Float128 result = Float128.Abs(Float128.NegativeZero);
        Assert.True(Float128.IsZero(result));
        Assert.True(Float128.IsPositive(result));
    }

    [Fact(DisplayName = "Float128: Abs(-∞) = +∞")]
    public void AbsOfNegativeInfinityIsPositiveInfinity()
    {
        Assert.True(Float128.IsPositiveInfinity(Float128.Abs(Float128.NegativeInfinity)));
    }

    [Fact(DisplayName = "Float128: Abs(NaN) is NaN")]
    public void AbsOfNaNIsNaN()
    {
        Assert.True(Float128.IsNaN(Float128.Abs(Float128.NaN)));
    }

    // ===== Boundary representable values =====

    [Fact(DisplayName = "Float128: MaxValue + MaxValue overflows to +∞")]
    public void MaxValuePlusMaxValueIsInfinity()
    {
        Assert.True(Float128.IsPositiveInfinity(Float128.MaxValue + Float128.MaxValue));
    }

    [Fact(DisplayName = "Float128: MinValue - MaxValue underflows to -∞")]
    public void MinValueMinusMaxValueIsNegativeInfinity()
    {
        Assert.True(Float128.IsNegativeInfinity(Float128.MinValue - Float128.MaxValue));
    }

    [Fact(DisplayName = "Float128: Epsilon × Epsilon underflows to zero")]
    public void EpsilonSquaredUnderflowsToZero()
    {
        Assert.True(Float128.IsZero(Float128.Epsilon * Float128.Epsilon));
    }

    // ===== Equality of finite values =====

    [Fact(DisplayName = "Float128: One == One, One != Zero")]
    public void BasicFiniteEqualities()
    {
        Assert.True(Float128.One == Float128.One);
        Assert.False(Float128.One == Float128.Zero);
        Assert.True(Float128.One != Float128.Zero);
    }

    [Fact(DisplayName = "Float128: ∞ == ∞, -∞ == -∞, ∞ != -∞")]
    public void InfinityEqualities()
    {
        Assert.True(Float128.PositiveInfinity == Float128.PositiveInfinity);
        Assert.True(Float128.NegativeInfinity == Float128.NegativeInfinity);
        Assert.False(Float128.PositiveInfinity == Float128.NegativeInfinity);
    }
}
