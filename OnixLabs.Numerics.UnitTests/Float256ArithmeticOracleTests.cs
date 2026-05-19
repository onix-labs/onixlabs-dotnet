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

namespace OnixLabs.Numerics.UnitTests;

public sealed class Float256ArithmeticOracleTests
{
    [Fact(DisplayName = "Float256.Add matches BigDecimal oracle for assorted finite values")]
    public void Float256AddMatchesBigDecimalOracle()
    {
        (string a, string b)[] cases =
        {
            ("3.14159265358979323846264338327950288419716939937510582097494459230781640628", "2.71828182845904523536028747135266249775724709369995957496696762772407663035"),
            ("1.5", "2.5"),
            ("1E50", "1E-50"),
            ("-1.5", "1.5"),
            ("1.0000000000000000000000000000000000000000000000000000000000000000000001", "1"),
            ("1", "-1"),
            ("1E100", "1E-100"),
        };

        foreach ((string aStr, string bStr) in cases)
        {
            Float256 a = Float256.Parse(aStr);
            Float256 b = Float256.Parse(bStr);
            BigDecimal exact = (BigDecimal)a + (BigDecimal)b;
            Float256 expected = (Float256)exact;
            Float256 actual = Float256.Add(a, b);
            Assert.Equal(expected, actual);
        }
    }

    [Fact(DisplayName = "Float256.Subtract matches BigDecimal oracle for assorted finite values")]
    public void Float256SubtractMatchesBigDecimalOracle()
    {
        (string a, string b)[] cases =
        {
            ("3.14159265358979323846264338327950288419716939937510582097494459230781640628", "2.71828182845904523536028747135266249775724709369995957496696762772407663035"),
            ("1.5", "2.5"),
            ("1E50", "1E-50"),
            ("-1.5", "1.5"),
            ("1.0000000000000000000000000000000000000000000000000000000000000000000001", "1"),
            ("1", "1"),
            ("1E100", "1E-100"),
        };

        foreach ((string aStr, string bStr) in cases)
        {
            Float256 a = Float256.Parse(aStr);
            Float256 b = Float256.Parse(bStr);
            BigDecimal exact = (BigDecimal)a - (BigDecimal)b;
            Float256 expected = (Float256)exact;
            Float256 actual = Float256.Subtract(a, b);
            Assert.Equal(expected, actual);
        }
    }

    [Fact(DisplayName = "Float256.Multiply matches BigDecimal oracle for assorted finite values")]
    public void Float256MultiplyMatchesBigDecimalOracle()
    {
        (string a, string b)[] cases =
        {
            ("3.14159265358979323846264338327950288", "2.71828182845904523536028747135266250"),
            ("1.5", "2.5"),
            ("1E50", "1E-50"),
            ("-1.5", "1.5"),
            ("0.5", "0.5"),
            ("1.4142135623730950488016887242096980", "1.4142135623730950488016887242096980"),
            ("1E100", "1E-100"),
        };

        foreach ((string aStr, string bStr) in cases)
        {
            Float256 a = Float256.Parse(aStr);
            Float256 b = Float256.Parse(bStr);
            BigDecimal exact = (BigDecimal)a * (BigDecimal)b;
            Float256 expected = (Float256)exact;
            Float256 actual = Float256.Multiply(a, b);
            Assert.Equal(expected, actual);
        }
    }

    [Fact(DisplayName = "Float256.Divide matches BigDecimal oracle for assorted finite values")]
    public void Float256DivideMatchesBigDecimalOracle()
    {
        (string a, string b)[] cases =
        {
            ("3.14159265358979323846264338327950288", "2.71828182845904523536028747135266250"),
            ("1", "3"),
            ("2", "7"),
            ("1.5", "0.5"),
            ("1E50", "1E-50"),
            ("-1", "3"),
            ("1.4142135623730950488016887242096980", "2"),
            ("1E100", "1E50"),
        };

        foreach ((string aStr, string bStr) in cases)
        {
            Float256 a = Float256.Parse(aStr);
            Float256 b = Float256.Parse(bStr);
            BigDecimal numerator = (BigDecimal)a;
            BigDecimal denominator = (BigDecimal)b;
            BigDecimal scaled = new(numerator.UnscaledValue * System.Numerics.BigInteger.Pow(10, 80), numerator.Scale + 80);
            BigDecimal exact = scaled / denominator;
            Float256 expected = (Float256)exact;
            Float256 actual = Float256.Divide(a, b);
            Assert.Equal(expected, actual);
        }
    }

    [Fact(DisplayName = "Float256.FusedMultiplyAdd matches BigDecimal oracle for assorted finite values")]
    public void Float256FusedMultiplyAddMatchesBigDecimalOracle()
    {
        (string a, string b, string c)[] cases =
        {
            ("3.14159265358979323846264338327950288", "2.71828182845904523536028747135266250", "1.41421356237309504880168872420969807"),
            ("1.5", "2.5", "3.5"),
            ("1E10", "1E10", "1E20"),
            ("1E-10", "1E10", "1"),
            ("-1.5", "1.5", "2.25"),
            ("1.000000000000000000000000000000000000000000000000000001", "1.000000000000000000000000000000000000000000000000000001", "-1"),
            ("0.5", "0.5", "0.25"),
            ("7", "11", "13"),
            ("1.0001", "1.0001", "-1.0002"),
        };

        foreach ((string aStr, string bStr, string cStr) in cases)
        {
            Float256 a = Float256.Parse(aStr);
            Float256 b = Float256.Parse(bStr);
            Float256 c = Float256.Parse(cStr);

            BigDecimal exact = (BigDecimal)a * (BigDecimal)b + (BigDecimal)c;
            Float256 expected = (Float256)exact;
            Float256 actual = Float256.FusedMultiplyAdd(a, b, c);

            Assert.Equal(expected, actual);
        }
    }

    [Fact(DisplayName = "Float256.Add with cancellation should produce small magnitude")]
    public void Float256AddWithCancellationShouldProduceSmallMagnitude()
    {
        Float256 a = Float256.Parse("1.0000000000000000000000000000000000000000000000000000000000000000000001");
        Float256 b = Float256.Parse("-1.0000000000000000000000000000000000000000000000000000000000000000000001");
        Float256 result = a + b;
        Assert.Equal(Float256.Zero, result);
    }

    [Fact(DisplayName = "Float256.Multiply small integers should match exact computation")]
    public void Float256MultiplySmallIntegersShouldMatchExact()
    {
        for (int a = -10; a <= 10; a++)
        {
            for (int b = -10; b <= 10; b++)
            {
                Float256 result = (Float256)a * (Float256)b;
                Float256 expected = (Float256)(a * b);
                Assert.Equal(expected, result);
            }
        }
    }

    [Fact(DisplayName = "Float256.FusedMultiplyAdd of small integers matches exact computation")]
    public void Float256FusedMultiplyAddSmallIntegersMatchesExact()
    {
        for (int a = -5; a <= 5; a++)
        {
            for (int b = -5; b <= 5; b++)
            {
                for (int c = -5; c <= 5; c++)
                {
                    Float256 result = Float256.FusedMultiplyAdd((Float256)a, (Float256)b, (Float256)c);
                    Float256 expected = (Float256)(a * b + c);
                    Assert.Equal(expected, result);
                }
            }
        }
    }

    [Fact(DisplayName = "Float256.Truncate matches BigDecimal oracle for assorted finite values")]
    public void Float256TruncateMatchesBigDecimalOracle()
    {
        string[] cases =
        {
            "3.14159265358979323846264338327950288",
            "-3.14159265358979323846264338327950288",
            "1E50",
            "0.5",
            "-0.5",
            "1000.999",
            "-1000.001",
            "0",
        };

        foreach (string s in cases)
        {
            Float256 value = Float256.Parse(s);
            Float256 expected = (Float256)(BigDecimal)(System.Numerics.BigInteger)(BigDecimal)value;
            if (Float256.IsZero(expected) && Float256.IsNegative(value)) expected = Float256.NegativeZero;
            Float256 actual = Float256.Truncate(value);
            Assert.Equal(expected, actual);
        }
    }

    [Fact(DisplayName = "Float256.Round with ToEven matches BigDecimal oracle for assorted finite values")]
    public void Float256RoundToEvenMatchesBigDecimalOracle()
    {
        (string input, string expected)[] cases =
        {
            ("0.5", "0"),
            ("1.5", "2"),
            ("2.5", "2"),
            ("3.5", "4"),
            ("-0.5", "0"),
            ("-1.5", "-2"),
            ("3.14159265358979323846", "3"),
            ("-3.14159265358979323846", "-3"),
            ("0.49999999999999999999999999999999999999999", "0"),
        };

        foreach ((string inputStr, string expectedStr) in cases)
        {
            Float256 input = Float256.Parse(inputStr);
            Float256 expected = Float256.Parse(expectedStr);
            Float256 actual = Float256.Round(input);
            Assert.Equal(expected, actual);
        }
    }
}
