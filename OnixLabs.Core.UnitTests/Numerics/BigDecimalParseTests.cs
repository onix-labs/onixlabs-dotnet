// Copyright 2020-2022 ONIXLabs
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
using Xunit;

namespace OnixLabs.Core.UnitTests.Numerics;

public sealed class BigDecimalParseTests
{
    [Theory(DisplayName = "BigDecimal.Parse should produce the expected result")]
    [InlineData("0", "0")]
    [InlineData("0.0", "0.0")]
    [InlineData("0.00", "0.00")]
    [InlineData("0.000", "0.000")]
    [InlineData("0.0000", "0.0000")]
    [InlineData("0.00000", "0.00000")]
    [InlineData("0.000000", "0.000000")]
    [InlineData("0.0000000", "0.0000000")]
    [InlineData("0.00000000", "0.00000000")]
    [InlineData("0.000000000", "0.000000000")]
    [InlineData("0.0000000000", "0.0000000000")]
    [InlineData("0.00000000000000000000", "0.00000000000000000000")]
    [InlineData("1", "1")]
    [InlineData("0.1", "0.1")]
    [InlineData("0.01", "0.01")]
    [InlineData("0.001", "0.001")]
    [InlineData("0.0001", "0.0001")]
    [InlineData("0.00001", "0.00001")]
    [InlineData("0.000001", "0.000001")]
    [InlineData("0.0000001", "0.0000001")]
    [InlineData("0.00000001", "0.00000001")]
    [InlineData("0.000000001", "0.000000001")]
    [InlineData("0.0000000001", "0.0000000001")]
    [InlineData("0.00000000000000000001", "0.00000000000000000001")]
    [InlineData("-1", "-1")]
    [InlineData("-0.1", "-0.1")]
    [InlineData("-0.01", "-0.01")]
    [InlineData("-0.001", "-0.001")]
    [InlineData("-0.0001", "-0.0001")]
    [InlineData("-0.00001", "-0.00001")]
    [InlineData("-0.000001", "-0.000001")]
    [InlineData("-0.0000001", "-0.0000001")]
    [InlineData("-0.00000001", "-0.00000001")]
    [InlineData("-0.000000001", "-0.000000001")]
    [InlineData("-0.0000000001", "-0.0000000001")]
    [InlineData("-0.00000000000000000001", "-0.00000000000000000001")]
    [InlineData("127", "127")]
    [InlineData("12.7", "12.7")]
    [InlineData("1.27", "1.27")]
    [InlineData("0.127", "0.127")]
    [InlineData("0.0127", "0.0127")]
    [InlineData("0.00127", "0.00127")]
    [InlineData("0.000127", "0.000127")]
    [InlineData("0.0000127", "0.0000127")]
    [InlineData("0.00000127", "0.00000127")]
    [InlineData("0.000000127", "0.000000127")]
    [InlineData("0.0000000127", "0.0000000127")]
    [InlineData("0.00000000000000000127", "0.00000000000000000127")]
    [InlineData("-128", "-128")]
    [InlineData("-12.8", "-12.8")]
    [InlineData("-1.28", "-1.28")]
    [InlineData("-0.128", "-0.128")]
    [InlineData("-0.0128", "-0.0128")]
    [InlineData("-0.00128", "-0.00128")]
    [InlineData("-0.000128", "-0.000128")]
    [InlineData("-0.0000128", "-0.0000128")]
    [InlineData("-0.00000128", "-0.00000128")]
    [InlineData("-0.000000128", "-0.000000128")]
    [InlineData("-0.0000000128", "-0.0000000128")]
    [InlineData("-0.00000000000000000128", "-0.00000000000000000128")]
    [InlineData("32767", "32767")]
    [InlineData("3276.7", "3276.7")]
    [InlineData("327.67", "327.67")]
    [InlineData("32.767", "32.767")]
    [InlineData("3.2767", "3.2767")]
    [InlineData("0.32767", "0.32767")]
    [InlineData("0.032767", "0.032767")]
    [InlineData("0.0032767", "0.0032767")]
    [InlineData("0.00032767", "0.00032767")]
    [InlineData("0.000032767", "0.000032767")]
    [InlineData("0.0000032767", "0.0000032767")]
    [InlineData("0.00000000000000032767", "0.00000000000000032767")]
    [InlineData("-32768", "-32768")]
    [InlineData("-3276.8", "-3276.8")]
    [InlineData("-327.68", "-327.68")]
    [InlineData("-32.768", "-32.768")]
    [InlineData("-3.2768", "-3.2768")]
    [InlineData("-0.32768", "-0.32768")]
    [InlineData("-0.032768", "-0.032768")]
    [InlineData("-0.0032768", "-0.0032768")]
    [InlineData("-0.00032768", "-0.00032768")]
    [InlineData("-0.000032768", "-0.000032768")]
    [InlineData("-0.0000032768", "-0.0000032768")]
    [InlineData("-0.00000000000000032768", "-0.00000000000000032768")]
    [InlineData("2147483647", "2147483647")]
    [InlineData("214748364.7", "214748364.7")]
    [InlineData("21474836.47", "21474836.47")]
    [InlineData("2147483.647", "2147483.647")]
    [InlineData("214748.3647", "214748.3647")]
    [InlineData("21474.83647", "21474.83647")]
    [InlineData("2147.483647", "2147.483647")]
    [InlineData("214.7483647", "214.7483647")]
    [InlineData("21.47483647", "21.47483647")]
    [InlineData("2.147483647", "2.147483647")]
    [InlineData("0.2147483647", "0.2147483647")]
    [InlineData("0.00000000002147483647", "0.00000000002147483647")]
    [InlineData("-2147483648", "-2147483648")]
    [InlineData("-214748364.8", "-214748364.8")]
    [InlineData("-21474836.48", "-21474836.48")]
    [InlineData("-2147483.648", "-2147483.648")]
    [InlineData("-214748.3648", "-214748.3648")]
    [InlineData("-21474.83648", "-21474.83648")]
    [InlineData("-2147.483648", "-2147.483648")]
    [InlineData("-214.7483648", "-214.7483648")]
    [InlineData("-21.47483648", "-21.47483648")]
    [InlineData("-2.147483648", "-2.147483648")]
    [InlineData("-0.2147483648", "-0.2147483648")]
    [InlineData("-0.00000000002147483648", "-0.00000000002147483648")]
    [InlineData("9223372036854775807", "9223372036854775807")]
    [InlineData("922337203685477580.7", "922337203685477580.7")]
    [InlineData("92233720368547758.07", "92233720368547758.07")]
    [InlineData("9223372036854775.807", "9223372036854775.807")]
    [InlineData("922337203685477.5807", "922337203685477.5807")]
    [InlineData("92233720368547.75807", "92233720368547.75807")]
    [InlineData("9223372036854.775807", "9223372036854.775807")]
    [InlineData("922337203685.4775807", "922337203685.4775807")]
    [InlineData("92233720368.54775807", "92233720368.54775807")]
    [InlineData("9223372036.854775807", "9223372036.854775807")]
    [InlineData("922337203.6854775807", "922337203.6854775807")]
    [InlineData("0.09223372036854775807", "0.09223372036854775807")]
    [InlineData("-9223372036854775808", "-9223372036854775808")]
    [InlineData("-922337203685477580.8", "-922337203685477580.8")]
    [InlineData("-92233720368547758.08", "-92233720368547758.08")]
    [InlineData("-9223372036854775.808", "-9223372036854775.808")]
    [InlineData("-922337203685477.5808", "-922337203685477.5808")]
    [InlineData("-92233720368547.75808", "-92233720368547.75808")]
    [InlineData("-9223372036854.775808", "-9223372036854.775808")]
    [InlineData("-922337203685.4775808", "-922337203685.4775808")]
    [InlineData("-92233720368.54775808", "-92233720368.54775808")]
    [InlineData("-9223372036.854775808", "-9223372036.854775808")]
    [InlineData("-922337203.6854775808", "-922337203.6854775808")]
    [InlineData("-0.09223372036854775808", "-0.09223372036854775808")]
    [InlineData("18446744073709551615", "18446744073709551615")]
    [InlineData("1844674407370955161.5", "1844674407370955161.5")]
    [InlineData("184467440737095516.15", "184467440737095516.15")]
    [InlineData("18446744073709551.615", "18446744073709551.615")]
    [InlineData("1844674407370955.1615", "1844674407370955.1615")]
    [InlineData("184467440737095.51615", "184467440737095.51615")]
    [InlineData("18446744073709.551615", "18446744073709.551615")]
    [InlineData("1844674407370.9551615", "1844674407370.9551615")]
    [InlineData("184467440737.09551615", "184467440737.09551615")]
    [InlineData("18446744073.709551615", "18446744073.709551615")]
    [InlineData("1844674407.3709551615", "1844674407.3709551615")]
    [InlineData("0.18446744073709551615", "0.18446744073709551615")]
    [InlineData("0%", "0")]
    [InlineData("0%.", "0")]
    [InlineData("0.%", "0")]
    [InlineData("0%.0", "0.0")]
    [InlineData("0.%0", "0.0")]
    [InlineData("0.0%", "0.0")]
    [InlineData("0%.00", "0.00")]
    [InlineData("0.%00", "0.00")]
    [InlineData("0.0%0", "0.00")]
    [InlineData("0.00%", "0.00")]
    [InlineData("1*2*3*4*5*6*7*8*9*0.1*2*3*4*5*6*7*8*9*0", "1234567890.1234567890")]
    public void BigDecimalParse(string value, string expected)
    {
        // Arrange
        BigDecimal candidate = BigDecimal.Parse(value);

        // Act
        string actual = candidate.ToString();

        // Assert
        Assert.Equal(expected, actual);
    }
}
