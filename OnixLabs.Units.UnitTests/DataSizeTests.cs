// Copyright 2020-2025 ONIXLabs
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

using System.Globalization;
using OnixLabs.Numerics;

namespace OnixLabs.Units.UnitTests;

public sealed class DataSizeTests
{
    [Fact(DisplayName = "DataSize.Zero should produce the expected result")]
    public void DataSizeZeroShouldProduceExpectedResult()
    {
        // Given / When
        DataSize<Float128> size = DataSize<Float128>.Zero;

        // Then
        Assert.Equal(Float128.Zero, size.Bits);
    }

    [Theory(DisplayName = "DataSize.FromBits should produce the expected Bits")]
    [InlineData("0", "0")]
    [InlineData("8", "8")]
    [InlineData("1000", "1000")]
    [InlineData("1024", "1024")]
    [InlineData("8192", "8192")]
    public void DataSizeFromBitsShouldProduceExpectedBits(string bits, string expectedBits)
    {
        // When
        DataSize<Float128> size = DataSize<Float128>.FromBits(Float128.Parse(bits));

        // Then
        Assert.Equal(Float128.Parse(expectedBits), size.Bits);
    }

    [Theory(DisplayName = "DataSize.FromBytes should produce the expected Bits")]
    [InlineData("0", "0")]
    [InlineData("1", "8")]
    [InlineData("1000", "8000")]
    [InlineData("1024", "8192")]
    [InlineData("2048", "16384")]
    public void DataSizeFromBytesShouldProduceExpectedBits(string bytes, string expectedBits)
    {
        DataSize<Float128> size = DataSize<Float128>.FromBytes(Float128.Parse(bytes));
        Assert.Equal(Float128.Parse(expectedBits), size.Bits);
    }

    [Theory(DisplayName = "DataSize.FromKibiBits should produce the expected Bits")]
    [InlineData("0", "0")]
    [InlineData("1", "1024")]
    [InlineData("2", "2048")]
    [InlineData("8", "8192")]
    [InlineData("10", "10240")]
    public void DataSizeFromKibiBitsShouldProduceExpectedBits(string kibibits, string expectedBits)
    {
        DataSize<Float128> size = DataSize<Float128>.FromKibiBits(Float128.Parse(kibibits));
        Assert.Equal(Float128.Parse(expectedBits), size.Bits);
    }

    [Theory(DisplayName = "DataSize.FromKibiBytes should produce the expected Bits")]
    [InlineData("0", "0")]
    [InlineData("1", "8192")]
    [InlineData("2", "16384")]
    [InlineData("10", "81920")]
    [InlineData("0.5", "4096")]
    public void DataSizeFromKibiBytesShouldProduceExpectedBits(string kibibytes, string expectedBits)
    {
        DataSize<Float128> size = DataSize<Float128>.FromKibiBytes(Float128.Parse(kibibytes));
        Assert.Equal(Float128.Parse(expectedBits), size.Bits);
    }

    [Theory(DisplayName = "DataSize.FromKiloBits should produce the expected Bits")]
    [InlineData("0", "0")]
    [InlineData("1", "1000")]
    [InlineData("8", "8000")]
    [InlineData("10", "10000")]
    [InlineData("0.5", "500")]
    public void DataSizeFromKiloBitsShouldProduceExpectedBits(string kilobits, string expectedBits)
    {
        DataSize<Float128> size = DataSize<Float128>.FromKiloBits(Float128.Parse(kilobits));
        Assert.Equal(Float128.Parse(expectedBits), size.Bits);
    }

    [Theory(DisplayName = "DataSize.FromKiloBytes should produce the expected Bits")]
    [InlineData("0", "0")]
    [InlineData("1", "8000")]
    [InlineData("2", "16000")]
    [InlineData("0.5", "4000")]
    [InlineData("10", "80000")]
    public void DataSizeFromKiloBytesShouldProduceExpectedBits(string kilobytes, string expectedBits)
    {
        DataSize<Float128> size = DataSize<Float128>.FromKiloBytes(Float128.Parse(kilobytes));
        Assert.Equal(Float128.Parse(expectedBits), size.Bits);
    }

    [Theory(DisplayName = "DataSize.FromMebiBits should produce the expected Bits")]
    [InlineData("0", "0")]
    [InlineData("1", "1048576")]
    [InlineData("0.5", "524288")]
    [InlineData("2", "2097152")]
    [InlineData("10", "10485760")]
    public void DataSizeFromMebiBitsShouldProduceExpectedBits(string mebibits, string expectedBits)
    {
        DataSize<Float128> size = DataSize<Float128>.FromMebiBits(Float128.Parse(mebibits));
        Assert.Equal(Float128.Parse(expectedBits), size.Bits);
    }

    [Theory(DisplayName = "DataSize.FromMebiBytes should produce the expected Bits")]
    [InlineData("0", "0")]
    [InlineData("1", "8388608")]
    [InlineData("0.5", "4194304")]
    [InlineData("2", "16777216")]
    [InlineData("10", "83886080")]
    public void DataSizeFromMebiBytesShouldProduceExpectedBits(string mebibytes, string expectedBits)
    {
        DataSize<Float128> size = DataSize<Float128>.FromMebiBytes(Float128.Parse(mebibytes));
        Assert.Equal(Float128.Parse(expectedBits), size.Bits);
    }

    [Theory(DisplayName = "DataSize.FromMegaBits should produce the expected Bits")]
    [InlineData("0", "0")]
    [InlineData("1", "1000000")]
    [InlineData("0.5", "500000")]
    [InlineData("2", "2000000")]
    [InlineData("10", "10000000")]
    public void DataSizeFromMegaBitsShouldProduceExpectedBits(string megabits, string expectedBits)
    {
        DataSize<Float128> size = DataSize<Float128>.FromMegaBits(Float128.Parse(megabits));
        Assert.Equal(Float128.Parse(expectedBits), size.Bits);
    }

    [Theory(DisplayName = "DataSize.FromMegaBytes should produce the expected Bits")]
    [InlineData("0", "0")]
    [InlineData("1", "8000000")]
    [InlineData("0.5", "4000000")]
    [InlineData("2", "16000000")]
    [InlineData("10", "80000000")]
    public void DataSizeFromMegaBytesShouldProduceExpectedBits(string megabytes, string expectedBits)
    {
        DataSize<Float128> size = DataSize<Float128>.FromMegaBytes(Float128.Parse(megabytes));
        Assert.Equal(Float128.Parse(expectedBits), size.Bits);
    }

    [Theory(DisplayName = "DataSize.FromGibiBits should produce the expected Bits")]
    [InlineData("0", "0")]
    [InlineData("1", "1073741824")]
    [InlineData("0.5", "536870912")]
    [InlineData("2", "2147483648")]
    [InlineData("10", "10737418240")]
    public void DataSizeFromGibiBitsShouldProduceExpectedBits(string gibibits, string expectedBits)
    {
        DataSize<Float128> size = DataSize<Float128>.FromGibiBits(Float128.Parse(gibibits));
        Assert.Equal(Float128.Parse(expectedBits), size.Bits);
    }

    [Theory(DisplayName = "DataSize.FromGibiBytes should produce the expected Bits")]
    [InlineData("0", "0")]
    [InlineData("1", "8589934592")]
    [InlineData("0.5", "4294967296")]
    [InlineData("2", "17179869184")]
    [InlineData("10", "85899345920")]
    public void DataSizeFromGibiBytesShouldProduceExpectedBits(string gibibytes, string expectedBits)
    {
        DataSize<Float128> size = DataSize<Float128>.FromGibiBytes(Float128.Parse(gibibytes));
        Assert.Equal(Float128.Parse(expectedBits), size.Bits);
    }

    [Theory(DisplayName = "DataSize.FromGigaBits should produce the expected Bits")]
    [InlineData("0", "0")]
    [InlineData("1", "1000000000")]
    [InlineData("0.5", "500000000")]
    [InlineData("2", "2000000000")]
    [InlineData("10", "10000000000")]
    public void DataSizeFromGigaBitsShouldProduceExpectedBits(string gigabits, string expectedBits)
    {
        DataSize<Float128> size = DataSize<Float128>.FromGigaBits(Float128.Parse(gigabits));
        Assert.Equal(Float128.Parse(expectedBits), size.Bits);
    }

    [Theory(DisplayName = "DataSize.FromGigaBytes should produce the expected Bits")]
    [InlineData("0", "0")]
    [InlineData("1", "8000000000")]
    [InlineData("0.5", "4000000000")]
    [InlineData("2", "16000000000")]
    [InlineData("10", "80000000000")]
    public void DataSizeFromGigaBytesShouldProduceExpectedBits(string gigabytes, string expectedBits)
    {
        DataSize<Float128> size = DataSize<Float128>.FromGigaBytes(Float128.Parse(gigabytes));
        Assert.Equal(Float128.Parse(expectedBits), size.Bits);
    }

    [Theory(DisplayName = "DataSize.FromTebiBits should produce the expected Bits")]
    [InlineData("0", "0")]
    [InlineData("1", "1099511627776")]
    [InlineData("0.5", "549755813888")]
    [InlineData("2", "2199023255552")]
    [InlineData("10", "10995116277760")]
    public void DataSizeFromTebiBitsShouldProduceExpectedBits(string tebibits, string expectedBits)
    {
        DataSize<Float128> size = DataSize<Float128>.FromTebiBits(Float128.Parse(tebibits));
        Assert.Equal(Float128.Parse(expectedBits), size.Bits);
    }

    [Theory(DisplayName = "DataSize.FromTebiBytes should produce the expected Bits")]
    [InlineData("0", "0")]
    [InlineData("1", "8796093022208")]
    [InlineData("0.5", "4398046511104")]
    [InlineData("2", "17592186044416")]
    [InlineData("10", "87960930222080")]
    public void DataSizeFromTebiBytesShouldProduceExpectedBits(string tebibytes, string expectedBits)
    {
        DataSize<Float128> size = DataSize<Float128>.FromTebiBytes(Float128.Parse(tebibytes));
        Assert.Equal(Float128.Parse(expectedBits), size.Bits);
    }

    [Theory(DisplayName = "DataSize.FromTeraBits should produce the expected Bits")]
    [InlineData("0", "0")]
    [InlineData("1", "1000000000000")]
    [InlineData("0.5", "500000000000")]
    [InlineData("2", "2000000000000")]
    [InlineData("10", "10000000000000")]
    public void DataSizeFromTeraBitsShouldProduceExpectedBits(string terabits, string expectedBits)
    {
        DataSize<Float128> size = DataSize<Float128>.FromTeraBits(Float128.Parse(terabits));
        Assert.Equal(Float128.Parse(expectedBits), size.Bits);
    }

    [Theory(DisplayName = "DataSize.FromTeraBytes should produce the expected Bits")]
    [InlineData("0", "0")]
    [InlineData("1", "8000000000000")]
    [InlineData("0.5", "4000000000000")]
    [InlineData("2", "16000000000000")]
    [InlineData("10", "80000000000000")]
    public void DataSizeFromTeraBytesShouldProduceExpectedBits(string terabytes, string expectedBits)
    {
        DataSize<Float128> size = DataSize<Float128>.FromTeraBytes(Float128.Parse(terabytes));
        Assert.Equal(Float128.Parse(expectedBits), size.Bits);
    }

    [Theory(DisplayName = "DataSize.FromPebiBits should produce the expected Bits")]
    [InlineData("0", "0")]
    [InlineData("1", "1125899906842624")]
    [InlineData("0.5", "562949953421312")]
    [InlineData("2", "2251799813685248")]
    [InlineData("10", "11258999068426240")]
    public void DataSizeFromPebiBitsShouldProduceExpectedBits(string pebibits, string expectedBits)
    {
        DataSize<Float128> size = DataSize<Float128>.FromPebiBits(Float128.Parse(pebibits));
        Assert.Equal(Float128.Parse(expectedBits), size.Bits);
    }

    [Theory(DisplayName = "DataSize.FromPebiBytes should produce the expected Bits")]
    [InlineData("0", "0")]
    [InlineData("1", "9007199254740992")]
    [InlineData("0.5", "4503599627370496")]
    [InlineData("2", "18014398509481984")]
    [InlineData("10", "90071992547409920")]
    public void DataSizeFromPebiBytesShouldProduceExpectedBits(string pebibytes, string expectedBits)
    {
        DataSize<Float128> size = DataSize<Float128>.FromPebiBytes(Float128.Parse(pebibytes));
        Assert.Equal(Float128.Parse(expectedBits), size.Bits);
    }

    [Theory(DisplayName = "DataSize.FromPetaBits should produce the expected Bits")]
    [InlineData("0", "0")]
    [InlineData("1", "1000000000000000")]
    [InlineData("0.5", "500000000000000")]
    [InlineData("2", "2000000000000000")]
    [InlineData("10", "10000000000000000")]
    public void DataSizeFromPetaBitsShouldProduceExpectedBits(string petabits, string expectedBits)
    {
        DataSize<Float128> size = DataSize<Float128>.FromPetaBits(Float128.Parse(petabits));
        Assert.Equal(Float128.Parse(expectedBits), size.Bits);
    }

    [Theory(DisplayName = "DataSize.FromPetaBytes should produce the expected Bits")]
    [InlineData("0", "0")]
    [InlineData("1", "8000000000000000")]
    [InlineData("0.5", "4000000000000000")]
    [InlineData("2", "16000000000000000")]
    [InlineData("10", "80000000000000000")]
    public void DataSizeFromPetaBytesShouldProduceExpectedBits(string petabytes, string expectedBits)
    {
        DataSize<Float128> size = DataSize<Float128>.FromPetaBytes(Float128.Parse(petabytes));
        Assert.Equal(Float128.Parse(expectedBits), size.Bits);
    }

    [Theory(DisplayName = "DataSize.FromExbiBits should produce the expected Bits")]
    [InlineData("0", "0")]
    [InlineData("1", "1152921504606846976")]
    [InlineData("0.5", "576460752303423488")]
    [InlineData("2", "2305843009213693952")]
    [InlineData("10", "11529215046068469760")]
    public void DataSizeFromExbiBitsShouldProduceExpectedBits(string exbibits, string expectedBits)
    {
        DataSize<Float128> size = DataSize<Float128>.FromExbiBits(Float128.Parse(exbibits));
        Assert.Equal(Float128.Parse(expectedBits), size.Bits);
    }

    [Theory(DisplayName = "DataSize.FromExbiBytes should produce the expected Bits")]
    [InlineData("0", "0")]
    [InlineData("1", "9223372036854775808")]
    [InlineData("0.5", "4611686018427387904")]
    [InlineData("2", "18446744073709551616")]
    [InlineData("10", "92233720368547758080")]
    public void DataSizeFromExbiBytesShouldProduceExpectedBits(string exbibytes, string expectedBits)
    {
        DataSize<Float128> size = DataSize<Float128>.FromExbiBytes(Float128.Parse(exbibytes));
        Assert.Equal(Float128.Parse(expectedBits), size.Bits);
    }

    [Theory(DisplayName = "DataSize.FromExaBits should produce the expected Bits")]
    [InlineData("0", "0")]
    [InlineData("1", "1000000000000000000")]
    [InlineData("0.5", "500000000000000000")]
    [InlineData("2", "2000000000000000000")]
    [InlineData("10", "10000000000000000000")]
    public void DataSizeFromExaBitsShouldProduceExpectedBits(string exabits, string expectedBits)
    {
        DataSize<Float128> size = DataSize<Float128>.FromExaBits(Float128.Parse(exabits));
        Assert.Equal(Float128.Parse(expectedBits), size.Bits);
    }

    [Theory(DisplayName = "DataSize.FromExaBytes should produce the expected Bits")]
    [InlineData("0", "0")]
    [InlineData("1", "8000000000000000000")]
    [InlineData("0.5", "4000000000000000000")]
    [InlineData("2", "16000000000000000000")]
    [InlineData("10", "80000000000000000000")]
    public void DataSizeFromExaBytesShouldProduceExpectedBits(string exabytes, string expectedBits)
    {
        DataSize<Float128> size = DataSize<Float128>.FromExaBytes(Float128.Parse(exabytes));
        Assert.Equal(Float128.Parse(expectedBits), size.Bits);
    }

    [Theory(DisplayName = "DataSize.FromZebiBits should produce the expected Bits")]
    [InlineData("0", "0")]
    [InlineData("1", "1180591620717411303424")]
    [InlineData("0.5", "590295810358705651712")]
    [InlineData("2", "2361183241434822606848")]
    [InlineData("10", "11805916207174113034240")]
    public void DataSizeFromZebiBitsShouldProduceExpectedBits(string zebibits, string expectedBits)
    {
        DataSize<Float128> size = DataSize<Float128>.FromZebiBits(Float128.Parse(zebibits));
        Assert.Equal(Float128.Parse(expectedBits), size.Bits);
    }

    [Theory(DisplayName = "DataSize.FromZebiBytes should produce the expected Bits")]
    [InlineData("0", "0")]
    [InlineData("1", "9444732965739290427392")]
    [InlineData("0.5", "4722366482869645213696")]
    [InlineData("2", "18889465931478580854784")]
    [InlineData("10", "94447329657392904273920")]
    public void DataSizeFromZebiBytesShouldProduceExpectedBits(string zebibytes, string expectedBits)
    {
        DataSize<Float128> size = DataSize<Float128>.FromZebiBytes(Float128.Parse(zebibytes));
        Assert.Equal(Float128.Parse(expectedBits), size.Bits);
    }

    [Theory(DisplayName = "DataSize.FromZettaBits should produce the expected Bits")]
    [InlineData("0", "0")]
    [InlineData("1", "1000000000000000000000")]
    [InlineData("0.5", "500000000000000000000")]
    [InlineData("2", "2000000000000000000000")]
    [InlineData("10", "10000000000000000000000")]
    public void DataSizeFromZettaBitsShouldProduceExpectedBits(string zettabits, string expectedBits)
    {
        DataSize<Float128> size = DataSize<Float128>.FromZettaBits(Float128.Parse(zettabits));
        Assert.Equal(Float128.Parse(expectedBits), size.Bits);
    }

    [Theory(DisplayName = "DataSize.FromZettaBytes should produce the expected Bits")]
    [InlineData("0", "0")]
    [InlineData("1", "8000000000000000000000")]
    [InlineData("0.5", "4000000000000000000000")]
    [InlineData("2", "16000000000000000000000")]
    [InlineData("10", "80000000000000000000000")]
    public void DataSizeFromZettaBytesShouldProduceExpectedBits(string zettabytes, string expectedBits)
    {
        DataSize<Float128> size = DataSize<Float128>.FromZettaBytes(Float128.Parse(zettabytes));
        Assert.Equal(Float128.Parse(expectedBits), size.Bits);
    }

    [Theory(DisplayName = "DataSize.FromYobiBits should produce the expected Bits")]
    [InlineData("0", "0")]
    [InlineData("1", "1208925819614629174706176")]
    [InlineData("0.5", "604462909807314587353088")]
    [InlineData("2", "2417851639229258349412352")]
    [InlineData("10", "12089258196146291747061760")]
    public void DataSizeFromYobiBitsShouldProduceExpectedBits(string yobibits, string expectedBits)
    {
        DataSize<Float128> size = DataSize<Float128>.FromYobiBits(Float128.Parse(yobibits));
        Assert.Equal(Float128.Parse(expectedBits), size.Bits);
    }

    [Theory(DisplayName = "DataSize.FromYobiBytes should produce the expected Bits")]
    [InlineData("0", "0")]
    [InlineData("1", "9671406556917033397649408")]
    [InlineData("0.5", "4835703278458516698824704")]
    [InlineData("2", "19342813113834066795298816")]
    [InlineData("10", "96714065569170333976494080")]
    public void DataSizeFromYobiBytesShouldProduceExpectedBits(string yobibytes, string expectedBits)
    {
        DataSize<Float128> size = DataSize<Float128>.FromYobiBytes(Float128.Parse(yobibytes));
        Assert.Equal(Float128.Parse(expectedBits), size.Bits);
    }

    [Theory(DisplayName = "DataSize.FromYottaBits should produce the expected Bits")]
    [InlineData("0", "0")]
    [InlineData("1", "1000000000000000000000000")]
    [InlineData("0.5", "500000000000000000000000")]
    [InlineData("2", "2000000000000000000000000")]
    public void DataSizeFromYottaBitsShouldProduceExpectedBits(string yottabits, string expectedBits)
    {
        DataSize<Float128> size = DataSize<Float128>.FromYottaBits(Float128.Parse(yottabits));
        Assert.Equal(Float128.Parse(expectedBits), size.Bits);
    }

    [Theory(DisplayName = "DataSize.FromYottaBytes should produce the expected Bits")]
    [InlineData("0", "0")]
    [InlineData("1", "8000000000000000000000000")]
    [InlineData("0.5", "4000000000000000000000000")]
    [InlineData("2", "16000000000000000000000000")]
    public void DataSizeFromYottaBytesShouldProduceExpectedBits(string yottabytes, string expectedBits)
    {
        DataSize<Float128> size = DataSize<Float128>.FromYottaBytes(Float128.Parse(yottabytes));
        Assert.Equal(Float128.Parse(expectedBits), size.Bits);
    }

    [Fact(DisplayName = "DataSize.Add should produce the expected result")]
    public void DataSizeAddShouldProduceExpectedValue()
    {
        // Given
        DataSize<Float128> left = DataSize<Float128>.FromBytes(Float128.Parse("1500"));
        DataSize<Float128> right = DataSize<Float128>.FromBytes(Float128.Parse("500"));

        // When
        DataSize<Float128> result = left.Add(right);

        // Then
        Assert.Equal(Float128.Parse("2000"), result.Bytes);
    }

    [Fact(DisplayName = "DataSize.Subtract should produce the expected result")]
    public void DataSizeSubtractShouldProduceExpectedValue()
    {
        // Given
        DataSize<Float128> left = DataSize<Float128>.FromBytes(Float128.Parse("1500"));
        DataSize<Float128> right = DataSize<Float128>.FromBytes(Float128.Parse("400"));

        // When
        DataSize<Float128> result = left.Subtract(right);

        // Then
        Assert.Equal(Float128.Parse("1100"), result.Bytes);
    }

    [Fact(DisplayName = "DataSize comparison should produce the expected result (left equal to right)")]
    public void DataSizeComparisonShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given
        DataSize<Float128> left = DataSize<Float128>.FromBytes(Float128.Parse("1234"));
        DataSize<Float128> right = DataSize<Float128>.FromBytes(Float128.Parse("1234"));

        // When / Then
        Assert.Equal(0, DataSize<Float128>.Compare(left, right));
        Assert.Equal(0, left.CompareTo(right));
        Assert.Equal(0, left.CompareTo((object)right));
        Assert.False(left > right);
        Assert.True(left >= right);
        Assert.False(left < right);
        Assert.True(left <= right);
    }

    [Fact(DisplayName = "DataSize comparison should produce the expected result (left greater than right)")]
    public void DataSizeComparisonShouldProduceExpectedLeftGreaterThanRight()
    {
        // Given
        DataSize<Float128> left = DataSize<Float128>.FromBytes(Float128.Parse("4567"));
        DataSize<Float128> right = DataSize<Float128>.FromBytes(Float128.Parse("1234"));

        // When / Then
        Assert.Equal(1, DataSize<Float128>.Compare(left, right));
        Assert.Equal(1, left.CompareTo(right));
        Assert.Equal(1, left.CompareTo((object)right));
        Assert.True(left > right);
        Assert.True(left >= right);
        Assert.False(left < right);
        Assert.False(left <= right);
    }

    [Fact(DisplayName = "DataSize comparison should produce the expected result (left less than right)")]
    public void DataSizeComparisonShouldProduceExpectedLeftLessThanRight()
    {
        // Given
        DataSize<Float128> left = DataSize<Float128>.FromBytes(Float128.Parse("1234"));
        DataSize<Float128> right = DataSize<Float128>.FromBytes(Float128.Parse("4567"));

        // When / Then
        Assert.Equal(-1, DataSize<Float128>.Compare(left, right));
        Assert.Equal(-1, left.CompareTo(right));
        Assert.Equal(-1, left.CompareTo((object)right));
        Assert.False(left > right);
        Assert.False(left >= right);
        Assert.True(left < right);
        Assert.True(left <= right);
    }

    [Fact(DisplayName = "DataSize equality should produce the expected result (left equal to right)")]
    public void DataSizeEqualityShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given
        DataSize<Float128> left = DataSize<Float128>.FromBytes(Float128.Parse("2048"));
        DataSize<Float128> right = DataSize<Float128>.FromBytes(Float128.Parse("2048"));

        // When / Then
        Assert.True(DataSize<Float128>.Equals(left, right));
        Assert.True(left.Equals(right));
        Assert.True(left.Equals((object)right));
        Assert.True(left == right);
        Assert.False(left != right);
    }

    [Fact(DisplayName = "DataSize equality should produce the expected result (left not equal to right)")]
    public void DataSizeEqualityShouldProduceExpectedResultLeftNotEqualToRight()
    {
        // Given
        DataSize<Float128> left = DataSize<Float128>.FromBytes(Float128.Parse("2048"));
        DataSize<Float128> right = DataSize<Float128>.FromBytes(Float128.Parse("4096"));

        // When / Then
        Assert.False(DataSize<Float128>.Equals(left, right));
        Assert.False(left.Equals(right));
        Assert.False(left.Equals((object)right));
        Assert.False(left == right);
        Assert.True(left != right);
    }

    [Fact(DisplayName = "DataSize.ToString should produce the expected result")]
    public void DataSizeToStringShouldProduceExpectedResult()
    {
        // Given / When
        // Use values that render nicely across several specifiers.
        DataSize<Float128> size = DataSize<Float128>.FromBytes(Float128.Parse("1048576")); // 1 MiB

        // Then (explicit formatting + scale)
        Assert.Equal("8,388,608.000 b", $"{size:b3}");
        Assert.Equal("1,048,576.000 B", $"{size:B3}");
        Assert.Equal("8,192.000 Kib", $"{size:Kib3}");
        Assert.Equal("1,024.000 KiB", $"{size:KiB3}");
        Assert.Equal("8,388.608 Kb", $"{size:Kb3}");
        Assert.Equal("1,048.576 KB", $"{size:KB3}");
        Assert.Equal("8.000 Mib", $"{size:Mib3}");
        Assert.Equal("1.000 MiB", $"{size:MiB3}");
    }

    [Fact(DisplayName = "DataSize.ToString should honor custom culture separators")]
    public void DataSizeToStringShouldHonorCustomCulture()
    {
        // Given
        CultureInfo customCulture = new("de-DE");
        DataSize<Float128> size = DataSize<Float128>.FromKiloBytes(Float128.Parse("1234.56")); // 1,234.56 KB

        // When
        string formatted = size.ToString("KB2", customCulture);

        // Then (German uses '.' for thousands and ',' for decimals)
        Assert.Equal("1.234,56 KB", formatted);
    }
}
