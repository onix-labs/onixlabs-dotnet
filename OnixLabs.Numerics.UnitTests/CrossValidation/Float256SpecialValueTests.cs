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
/// IEEE 754 binary256 special-value conformance pass for <see cref="Float256"/>. The standard
/// (IEEE 754-2008/2019 §3.6) prescribes binary{k} interchange formats for every k ≥ 128 in
/// multiples of 32, so binary256 has the same NaN/∞/0 semantics as binary128 — only the
/// significand width changes. These tests mirror the Float128 pass; failures here may indicate
/// real conformance bugs.
/// </summary>
public sealed class Float256SpecialValueTests
{
    private static readonly Float256 Two = Float256.One + Float256.One;
    private static readonly Float256 Three = Float256.One + Two;

    // ===== NaN propagation =====

    [Fact(DisplayName = "Float256: NaN + x is NaN for any finite x")]
    public void NaNPlusFiniteIsNaN()
    {
        Assert.True(Float256.IsNaN(Float256.NaN + Float256.One));
        Assert.True(Float256.IsNaN(Float256.NaN + Float256.Zero));
        Assert.True(Float256.IsNaN(Float256.NaN + Float256.MaxValue));
        Assert.True(Float256.IsNaN(Float256.NaN + Float256.NegativeOne));
    }

    [Fact(DisplayName = "Float256: NaN - x and x - NaN are both NaN")]
    public void NaNSubtractionPropagates()
    {
        Assert.True(Float256.IsNaN(Float256.NaN - Float256.One));
        Assert.True(Float256.IsNaN(Float256.One - Float256.NaN));
    }

    [Fact(DisplayName = "Float256: NaN * x is NaN, including NaN * 0")]
    public void NaNMultiplicationPropagates()
    {
        Assert.True(Float256.IsNaN(Float256.NaN * Float256.One));
        Assert.True(Float256.IsNaN(Float256.NaN * Float256.Zero));
        Assert.True(Float256.IsNaN(Float256.NaN * Float256.PositiveInfinity));
    }

    [Fact(DisplayName = "Float256: NaN / x and x / NaN are both NaN")]
    public void NaNDivisionPropagates()
    {
        Assert.True(Float256.IsNaN(Float256.NaN / Float256.One));
        Assert.True(Float256.IsNaN(Float256.One / Float256.NaN));
        Assert.True(Float256.IsNaN(Float256.NaN / Float256.Zero));
    }

    [Fact(DisplayName = "Float256: NaN + NaN is NaN")]
    public void NaNPlusNaNIsNaN()
    {
        Assert.True(Float256.IsNaN(Float256.NaN + Float256.NaN));
    }

    // ===== NaN comparison rules =====

    [Fact(DisplayName = "Float256: NaN is not equal to itself (IEEE 754 §5.11)")]
    public void NaNIsNotEqualToItself()
    {
        Assert.False(Float256.NaN == Float256.NaN);
        Assert.True(Float256.NaN != Float256.NaN);
    }

    [Fact(DisplayName = "Float256: all ordering comparisons involving NaN return false")]
    public void NaNOrderingComparisonsReturnFalse()
    {
        Assert.False(Float256.NaN < Float256.One);
        Assert.False(Float256.NaN > Float256.One);
        Assert.False(Float256.NaN <= Float256.One);
        Assert.False(Float256.NaN >= Float256.One);
        Assert.False(Float256.One < Float256.NaN);
        Assert.False(Float256.One > Float256.NaN);
    }

    // ===== Infinity arithmetic =====

    [Fact(DisplayName = "Float256: ∞ + ∞ = ∞")]
    public void PositiveInfinityPlusItselfIsInfinity()
    {
        Assert.True(Float256.IsPositiveInfinity(Float256.PositiveInfinity + Float256.PositiveInfinity));
    }

    [Fact(DisplayName = "Float256: ∞ - ∞ = NaN")]
    public void PositiveInfinityMinusItselfIsNaN()
    {
        Assert.True(Float256.IsNaN(Float256.PositiveInfinity - Float256.PositiveInfinity));
    }

    [Fact(DisplayName = "Float256: -∞ + ∞ = NaN")]
    public void NegativeInfinityPlusPositiveInfinityIsNaN()
    {
        Assert.True(Float256.IsNaN(Float256.NegativeInfinity + Float256.PositiveInfinity));
    }

    [Fact(DisplayName = "Float256: ∞ × 0 = NaN")]
    public void InfinityTimesZeroIsNaN()
    {
        Assert.True(Float256.IsNaN(Float256.PositiveInfinity * Float256.Zero));
        Assert.True(Float256.IsNaN(Float256.NegativeInfinity * Float256.Zero));
    }

    [Fact(DisplayName = "Float256: ∞ × finite preserves sign and is infinite")]
    public void InfinityTimesFiniteIsInfinity()
    {
        Assert.True(Float256.IsPositiveInfinity(Float256.PositiveInfinity * Float256.One));
        Assert.True(Float256.IsNegativeInfinity(Float256.PositiveInfinity * Float256.NegativeOne));
        Assert.True(Float256.IsNegativeInfinity(Float256.NegativeInfinity * Float256.One));
        Assert.True(Float256.IsPositiveInfinity(Float256.NegativeInfinity * Float256.NegativeOne));
    }

    [Fact(DisplayName = "Float256: ∞ / ∞ = NaN")]
    public void InfinityDividedByInfinityIsNaN()
    {
        Assert.True(Float256.IsNaN(Float256.PositiveInfinity / Float256.PositiveInfinity));
        Assert.True(Float256.IsNaN(Float256.PositiveInfinity / Float256.NegativeInfinity));
    }

    [Fact(DisplayName = "Float256: finite / 0 is signed infinity")]
    public void FiniteDividedByZeroIsSignedInfinity()
    {
        Assert.True(Float256.IsPositiveInfinity(Float256.One / Float256.Zero));
        Assert.True(Float256.IsNegativeInfinity(Float256.NegativeOne / Float256.Zero));
        Assert.True(Float256.IsNegativeInfinity(Float256.One / Float256.NegativeZero));
        Assert.True(Float256.IsPositiveInfinity(Float256.NegativeOne / Float256.NegativeZero));
    }

    [Fact(DisplayName = "Float256: 0 / 0 = NaN")]
    public void ZeroDividedByZeroIsNaN()
    {
        Assert.True(Float256.IsNaN(Float256.Zero / Float256.Zero));
        Assert.True(Float256.IsNaN(Float256.Zero / Float256.NegativeZero));
        Assert.True(Float256.IsNaN(Float256.NegativeZero / Float256.Zero));
    }

    [Fact(DisplayName = "Float256: finite / ∞ = signed zero")]
    public void FiniteDividedByInfinityIsZero()
    {
        Float256 result = Float256.One / Float256.PositiveInfinity;
        Assert.True(Float256.IsZero(result));
        Assert.True(Float256.IsPositive(result));

        Float256 negResult = Float256.One / Float256.NegativeInfinity;
        Assert.True(Float256.IsZero(negResult));
        Assert.True(Float256.IsNegative(negResult));
    }

    // ===== Signed zero =====

    [Fact(DisplayName = "Float256: +0 == -0 but they have distinct signs")]
    public void PositiveAndNegativeZeroCompareEqualButHaveDistinctSigns()
    {
        Assert.True(Float256.Zero == Float256.NegativeZero);
        Assert.True(Float256.IsPositive(Float256.Zero));
        Assert.True(Float256.IsNegative(Float256.NegativeZero));
    }

    [Fact(DisplayName = "Float256: 1 / +0 = +∞, 1 / -0 = -∞")]
    public void OneDividedBySignedZeroProducesSignedInfinity()
    {
        Assert.True(Float256.IsPositiveInfinity(Float256.One / Float256.Zero));
        Assert.True(Float256.IsNegativeInfinity(Float256.One / Float256.NegativeZero));
    }

    [Fact(DisplayName = "Float256: -0 + +0 = +0 (under round-to-nearest)")]
    public void NegativeZeroPlusPositiveZeroIsPositiveZero()
    {
        Float256 result = Float256.NegativeZero + Float256.Zero;
        Assert.True(Float256.IsZero(result));
        Assert.True(Float256.IsPositive(result));
    }

    [Fact(DisplayName = "Float256: x + (-x) = +0 for any finite x")]
    public void XPlusNegativeXIsPositiveZero()
    {
        Float256 result = Float256.One + Float256.NegativeOne;
        Assert.True(Float256.IsZero(result));
        Assert.True(Float256.IsPositive(result));
    }

    [Fact(DisplayName = "Float256: -0 × +0 = -0")]
    public void NegativeZeroTimesPositiveZeroIsNegativeZero()
    {
        Float256 result = Float256.NegativeZero * Float256.Zero;
        Assert.True(Float256.IsZero(result));
        Assert.True(Float256.IsNegative(result));
    }

    // ===== Sqrt edge cases =====

    [Fact(DisplayName = "Float256: sqrt(NaN) = NaN")]
    public void SqrtOfNaNIsNaN()
    {
        Assert.True(Float256.IsNaN(Float256.Sqrt(Float256.NaN)));
    }

    [Fact(DisplayName = "Float256: sqrt(-1) = NaN")]
    public void SqrtOfNegativeIsNaN()
    {
        Assert.True(Float256.IsNaN(Float256.Sqrt(Float256.NegativeOne)));
    }

    [Fact(DisplayName = "Float256: sqrt(-∞) = NaN")]
    public void SqrtOfNegativeInfinityIsNaN()
    {
        Assert.True(Float256.IsNaN(Float256.Sqrt(Float256.NegativeInfinity)));
    }

    [Fact(DisplayName = "Float256: sqrt(+∞) = +∞")]
    public void SqrtOfPositiveInfinityIsPositiveInfinity()
    {
        Assert.True(Float256.IsPositiveInfinity(Float256.Sqrt(Float256.PositiveInfinity)));
    }

    [Fact(DisplayName = "Float256: sqrt(+0) = +0, sqrt(-0) = -0 (IEEE 754 §6.3)")]
    public void SqrtOfSignedZeroPreservesSign()
    {
        Float256 sqrtPosZero = Float256.Sqrt(Float256.Zero);
        Assert.True(Float256.IsZero(sqrtPosZero));
        Assert.True(Float256.IsPositive(sqrtPosZero));

        Float256 sqrtNegZero = Float256.Sqrt(Float256.NegativeZero);
        Assert.True(Float256.IsZero(sqrtNegZero));
        Assert.True(Float256.IsNegative(sqrtNegZero));
    }

    [Fact(DisplayName = "Float256: sqrt(1) = 1, sqrt(4) = 2, sqrt(9) = 3 (exactly representable)")]
    public void SqrtOfPerfectSquares()
    {
        Assert.Equal(Float256.One, Float256.Sqrt(Float256.One));
        Assert.Equal(Two, Float256.Sqrt(Float256.One + Three));
        Assert.Equal(Three, Float256.Sqrt(Three * Three));
    }

    // ===== Predicate self-consistency =====

    [Fact(DisplayName = "Float256: IsNaN is true only for NaN values")]
    public void IsNaNIsOnlyTrueForNaN()
    {
        Assert.True(Float256.IsNaN(Float256.NaN));
        Assert.False(Float256.IsNaN(Float256.Zero));
        Assert.False(Float256.IsNaN(Float256.One));
        Assert.False(Float256.IsNaN(Float256.PositiveInfinity));
        Assert.False(Float256.IsNaN(Float256.NegativeInfinity));
        Assert.False(Float256.IsNaN(Float256.MaxValue));
        Assert.False(Float256.IsNaN(Float256.Epsilon));
    }

    [Fact(DisplayName = "Float256: IsFinite excludes NaN and infinities")]
    public void IsFiniteExcludesNaNAndInfinities()
    {
        Assert.False(Float256.IsFinite(Float256.NaN));
        Assert.False(Float256.IsFinite(Float256.PositiveInfinity));
        Assert.False(Float256.IsFinite(Float256.NegativeInfinity));
        Assert.True(Float256.IsFinite(Float256.Zero));
        Assert.True(Float256.IsFinite(Float256.MaxValue));
        Assert.True(Float256.IsFinite(Float256.Epsilon));
    }

    [Fact(DisplayName = "Float256: IsZero is true for both +0 and -0 only")]
    public void IsZeroAcceptsBothSignedZeros()
    {
        Assert.True(Float256.IsZero(Float256.Zero));
        Assert.True(Float256.IsZero(Float256.NegativeZero));
        Assert.False(Float256.IsZero(Float256.Epsilon));
        Assert.False(Float256.IsZero(Float256.One));
    }

    [Fact(DisplayName = "Float256: IsSubnormal is true for Epsilon and false for One, Zero, and special values")]
    public void IsSubnormalPredicate()
    {
        Assert.True(Float256.IsSubnormal(Float256.Epsilon));
        Assert.False(Float256.IsSubnormal(Float256.One));
        Assert.False(Float256.IsSubnormal(Float256.Zero));
        Assert.False(Float256.IsSubnormal(Float256.NaN));
        Assert.False(Float256.IsSubnormal(Float256.PositiveInfinity));
    }

    [Fact(DisplayName = "Float256: IsNormal is true for One and MaxValue, false for Zero, Epsilon, and special values")]
    public void IsNormalPredicate()
    {
        Assert.True(Float256.IsNormal(Float256.One));
        Assert.True(Float256.IsNormal(Float256.MaxValue));
        Assert.False(Float256.IsNormal(Float256.Zero));
        Assert.False(Float256.IsNormal(Float256.Epsilon));
        Assert.False(Float256.IsNormal(Float256.NaN));
        Assert.False(Float256.IsNormal(Float256.PositiveInfinity));
    }

    // ===== Sign of zero through arithmetic =====

    [Fact(DisplayName = "Float256: positive × negative = negative (including zero results)")]
    public void SignOfMultiplicationFollowsAlgebra()
    {
        Float256 result = Float256.One * Float256.NegativeOne;
        Assert.True(Float256.IsNegative(result));
    }

    [Fact(DisplayName = "Float256: -1 × -1 = +1")]
    public void NegativeTimesNegativeIsPositive()
    {
        Assert.Equal(Float256.One, Float256.NegativeOne * Float256.NegativeOne);
    }

    // ===== Infinity in addition / subtraction =====

    [Fact(DisplayName = "Float256: ∞ + finite = ∞")]
    public void InfinityPlusFiniteIsInfinity()
    {
        Assert.True(Float256.IsPositiveInfinity(Float256.PositiveInfinity + Float256.MaxValue));
        Assert.True(Float256.IsPositiveInfinity(Float256.PositiveInfinity + Float256.NegativeOne));
    }

    [Fact(DisplayName = "Float256: finite - ∞ = -∞")]
    public void FiniteMinusInfinityIsNegativeInfinity()
    {
        Assert.True(Float256.IsNegativeInfinity(Float256.One - Float256.PositiveInfinity));
    }

    // ===== Abs =====

    [Fact(DisplayName = "Float256: Abs(-0) = +0")]
    public void AbsOfNegativeZeroIsPositiveZero()
    {
        Float256 result = Float256.Abs(Float256.NegativeZero);
        Assert.True(Float256.IsZero(result));
        Assert.True(Float256.IsPositive(result));
    }

    [Fact(DisplayName = "Float256: Abs(-∞) = +∞")]
    public void AbsOfNegativeInfinityIsPositiveInfinity()
    {
        Assert.True(Float256.IsPositiveInfinity(Float256.Abs(Float256.NegativeInfinity)));
    }

    [Fact(DisplayName = "Float256: Abs(NaN) is NaN")]
    public void AbsOfNaNIsNaN()
    {
        Assert.True(Float256.IsNaN(Float256.Abs(Float256.NaN)));
    }

    // ===== Boundary representable values =====

    [Fact(DisplayName = "Float256: MaxValue + MaxValue overflows to +∞")]
    public void MaxValuePlusMaxValueIsInfinity()
    {
        Assert.True(Float256.IsPositiveInfinity(Float256.MaxValue + Float256.MaxValue));
    }

    [Fact(DisplayName = "Float256: MinValue - MaxValue underflows to -∞")]
    public void MinValueMinusMaxValueIsNegativeInfinity()
    {
        Assert.True(Float256.IsNegativeInfinity(Float256.MinValue - Float256.MaxValue));
    }

    [Fact(DisplayName = "Float256: Epsilon × Epsilon underflows to zero")]
    public void EpsilonSquaredUnderflowsToZero()
    {
        Assert.True(Float256.IsZero(Float256.Epsilon * Float256.Epsilon));
    }

    // ===== Equality of finite values =====

    [Fact(DisplayName = "Float256: One == One, One != Zero")]
    public void BasicFiniteEqualities()
    {
        Assert.True(Float256.One == Float256.One);
        Assert.False(Float256.One == Float256.Zero);
        Assert.True(Float256.One != Float256.Zero);
    }

    [Fact(DisplayName = "Float256: ∞ == ∞, -∞ == -∞, ∞ != -∞")]
    public void InfinityEqualities()
    {
        Assert.True(Float256.PositiveInfinity == Float256.PositiveInfinity);
        Assert.True(Float256.NegativeInfinity == Float256.NegativeInfinity);
        Assert.False(Float256.PositiveInfinity == Float256.NegativeInfinity);
    }
}
