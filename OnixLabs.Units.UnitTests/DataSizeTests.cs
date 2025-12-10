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

namespace OnixLabs.Units.UnitTests;

public sealed class DataSizeTests
{
    // IEEE-754 binary floating-point arithmetic causes small discrepancies in calculation, therefore we need a tolerance.
    private const double Tolerance = 1e-12;

    [Fact(DisplayName = "DataSize.Zero should produce the expected result")]
    public void DataSizeZeroShouldProduceExpectedResult()
    {
        // Given / When
        DataSize<double> size = DataSize<double>.Zero;

        // Then
        Assert.Equal(0.0, size.Bits, Tolerance);
    }

    [Theory(DisplayName = "DataSize.FromBits should produce the expected Bits")]
    [InlineData(0.0, 0.0)]
    [InlineData(8.0, 8.0)]
    [InlineData(1000.0, 1000.0)]
    [InlineData(1024.0, 1024.0)]
    [InlineData(8192.0, 8192.0)]
    public void DataSizeFromBitsShouldProduceExpectedBits(double bits, double expectedBits)
    {
        // When
        DataSize<double> size = DataSize<double>.FromBits(bits);

        // Then
        Assert.Equal(expectedBits, size.Bits, Tolerance);
    }

    [Theory(DisplayName = "DataSize.FromBytes should produce the expected Bits")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 8.0)]
    [InlineData(1000.0, 8000.0)]
    [InlineData(1024.0, 8192.0)]
    [InlineData(2048.0, 16384.0)]
    public void DataSizeFromBytesShouldProduceExpectedBits(double bytes, double expectedBits)
    {
        DataSize<double> size = DataSize<double>.FromBytes(bytes);
        Assert.Equal(expectedBits, size.Bits, Tolerance);
    }

    [Theory(DisplayName = "DataSize.FromKibiBits should produce the expected Bits")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1024.0)]
    [InlineData(2.0, 2048.0)]
    [InlineData(8.0, 8192.0)]
    [InlineData(10.0, 10240.0)]
    public void DataSizeFromKibiBitsShouldProduceExpectedBits(double kibibits, double expectedBits)
    {
        DataSize<double> size = DataSize<double>.FromKibiBits(kibibits);
        Assert.Equal(expectedBits, size.Bits, Tolerance);
    }

    [Theory(DisplayName = "DataSize.FromKibiBytes should produce the expected Bits")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 8192.0)]
    [InlineData(2.0, 16384.0)]
    [InlineData(10.0, 81920.0)]
    [InlineData(0.5, 4096.0)]
    public void DataSizeFromKibiBytesShouldProduceExpectedBits(double kibibytes, double expectedBits)
    {
        DataSize<double> size = DataSize<double>.FromKibiBytes(kibibytes);
        Assert.Equal(expectedBits, size.Bits, Tolerance);
    }

    [Theory(DisplayName = "DataSize.FromKiloBits should produce the expected Bits")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1000.0)]
    [InlineData(8.0, 8000.0)]
    [InlineData(10.0, 10000.0)]
    [InlineData(0.5, 500.0)]
    public void DataSizeFromKiloBitsShouldProduceExpectedBits(double kilobits, double expectedBits)
    {
        DataSize<double> size = DataSize<double>.FromKiloBits(kilobits);
        Assert.Equal(expectedBits, size.Bits, Tolerance);
    }

    [Theory(DisplayName = "DataSize.FromKiloBytes should produce the expected Bits")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 8000.0)]
    [InlineData(2.0, 16000.0)]
    [InlineData(0.5, 4000.0)]
    [InlineData(10.0, 80000.0)]
    public void DataSizeFromKiloBytesShouldProduceExpectedBits(double kilobytes, double expectedBits)
    {
        DataSize<double> size = DataSize<double>.FromKiloBytes(kilobytes);
        Assert.Equal(expectedBits, size.Bits, Tolerance);
    }

    [Theory(DisplayName = "DataSize.FromMebiBits should produce the expected Bits")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1024.0 * 1024.0)]
    [InlineData(0.5, 0.5 * 1024.0 * 1024.0)]
    [InlineData(2.0, 2.0 * 1024.0 * 1024.0)]
    [InlineData(10.0, 10.0 * 1024.0 * 1024.0)]
    public void DataSizeFromMebiBitsShouldProduceExpectedBits(double mebibits, double expectedBits)
    {
        DataSize<double> size = DataSize<double>.FromMebiBits(mebibits);
        Assert.Equal(expectedBits, size.Bits, Tolerance);
    }

    [Theory(DisplayName = "DataSize.FromMebiBytes should produce the expected Bits")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1024.0 * 1024.0 * 8.0)]
    [InlineData(0.5, 0.5 * 1024.0 * 1024.0 * 8.0)]
    [InlineData(2.0, 2.0 * 1024.0 * 1024.0 * 8.0)]
    [InlineData(10.0, 10.0 * 1024.0 * 1024.0 * 8.0)]
    public void DataSizeFromMebiBytesShouldProduceExpectedBits(double mebibytes, double expectedBits)
    {
        DataSize<double> size = DataSize<double>.FromMebiBytes(mebibytes);
        Assert.Equal(expectedBits, size.Bits, Tolerance);
    }

    [Theory(DisplayName = "DataSize.FromMegaBits should produce the expected Bits")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1000.0 * 1000.0)]
    [InlineData(0.5, 0.5 * 1000.0 * 1000.0)]
    [InlineData(2.0, 2.0 * 1000.0 * 1000.0)]
    [InlineData(10.0, 10.0 * 1000.0 * 1000.0)]
    public void DataSizeFromMegaBitsShouldProduceExpectedBits(double megabits, double expectedBits)
    {
        DataSize<double> size = DataSize<double>.FromMegaBits(megabits);
        Assert.Equal(expectedBits, size.Bits, Tolerance);
    }

    [Theory(DisplayName = "DataSize.FromMegaBytes should produce the expected Bits")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1000.0 * 1000.0 * 8.0)]
    [InlineData(0.5, 0.5 * 1000.0 * 1000.0 * 8.0)]
    [InlineData(2.0, 2.0 * 1000.0 * 1000.0 * 8.0)]
    [InlineData(10.0, 10.0 * 1000.0 * 1000.0 * 8.0)]
    public void DataSizeFromMegaBytesShouldProduceExpectedBits(double megabytes, double expectedBits)
    {
        DataSize<double> size = DataSize<double>.FromMegaBytes(megabytes);
        Assert.Equal(expectedBits, size.Bits, Tolerance);
    }

    [Theory(DisplayName = "DataSize.FromGibiBits should produce the expected Bits")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1024.0 * 1024.0 * 1024.0)]
    [InlineData(0.5, 0.5 * 1024.0 * 1024.0 * 1024.0)]
    [InlineData(2.0, 2.0 * 1024.0 * 1024.0 * 1024.0)]
    [InlineData(10.0, 10.0 * 1024.0 * 1024.0 * 1024.0)]
    public void DataSizeFromGibiBitsShouldProduceExpectedBits(double gibibits, double expectedBits)
    {
        DataSize<double> size = DataSize<double>.FromGibiBits(gibibits);
        Assert.Equal(expectedBits, size.Bits, Tolerance);
    }

    [Theory(DisplayName = "DataSize.FromGibiBytes should produce the expected Bits")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1024.0 * 1024.0 * 1024.0 * 8.0)]
    [InlineData(0.5, 0.5 * 1024.0 * 1024.0 * 1024.0 * 8.0)]
    [InlineData(2.0, 2.0 * 1024.0 * 1024.0 * 1024.0 * 8.0)]
    [InlineData(10.0, 10.0 * 1024.0 * 1024.0 * 1024.0 * 8.0)]
    public void DataSizeFromGibiBytesShouldProduceExpectedBits(double gibibytes, double expectedBits)
    {
        DataSize<double> size = DataSize<double>.FromGibiBytes(gibibytes);
        Assert.Equal(expectedBits, size.Bits, Tolerance);
    }

    [Theory(DisplayName = "DataSize.FromGigaBits should produce the expected Bits")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1000.0 * 1000.0 * 1000.0)]
    [InlineData(0.5, 0.5 * 1000.0 * 1000.0 * 1000.0)]
    [InlineData(2.0, 2.0 * 1000.0 * 1000.0 * 1000.0)]
    [InlineData(10.0, 10.0 * 1000.0 * 1000.0 * 1000.0)]
    public void DataSizeFromGigaBitsShouldProduceExpectedBits(double gigabits, double expectedBits)
    {
        DataSize<double> size = DataSize<double>.FromGigaBits(gigabits);
        Assert.Equal(expectedBits, size.Bits, Tolerance);
    }

    [Theory(DisplayName = "DataSize.FromGigaBytes should produce the expected Bits")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1000.0 * 1000.0 * 1000.0 * 8.0)]
    [InlineData(0.5, 0.5 * 1000.0 * 1000.0 * 1000.0 * 8.0)]
    [InlineData(2.0, 2.0 * 1000.0 * 1000.0 * 1000.0 * 8.0)]
    [InlineData(10.0, 10.0 * 1000.0 * 1000.0 * 1000.0 * 8.0)]
    public void DataSizeFromGigaBytesShouldProduceExpectedBits(double gigabytes, double expectedBits)
    {
        DataSize<double> size = DataSize<double>.FromGigaBytes(gigabytes);
        Assert.Equal(expectedBits, size.Bits, Tolerance);
    }

    [Theory(DisplayName = "DataSize.FromTebiBits should produce the expected Bits")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1024.0 * 1024.0 * 1024.0 * 1024.0)]
    [InlineData(0.5, 0.5 * 1024.0 * 1024.0 * 1024.0 * 1024.0)]
    [InlineData(2.0, 2.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0)]
    [InlineData(10.0, 10.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0)]
    public void DataSizeFromTebiBitsShouldProduceExpectedBits(double tebibits, double expectedBits)
    {
        DataSize<double> size = DataSize<double>.FromTebiBits(tebibits);
        Assert.Equal(expectedBits, size.Bits, Tolerance);
    }

    [Theory(DisplayName = "DataSize.FromTebiBytes should produce the expected Bits")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1024.0 * 1024.0 * 1024.0 * 1024.0 * 8.0)]
    [InlineData(0.5, 0.5 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 8.0)]
    [InlineData(2.0, 2.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 8.0)]
    [InlineData(10.0, 10.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 8.0)]
    public void DataSizeFromTebiBytesShouldProduceExpectedBits(double tebibytes, double expectedBits)
    {
        DataSize<double> size = DataSize<double>.FromTebiBytes(tebibytes);
        Assert.Equal(expectedBits, size.Bits, Tolerance);
    }

    [Theory(DisplayName = "DataSize.FromTeraBits should produce the expected Bits")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1000.0 * 1000.0 * 1000.0 * 1000.0)]
    [InlineData(0.5, 0.5 * 1000.0 * 1000.0 * 1000.0 * 1000.0)]
    [InlineData(2.0, 2.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0)]
    [InlineData(10.0, 10.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0)]
    public void DataSizeFromTeraBitsShouldProduceExpectedBits(double terabits, double expectedBits)
    {
        DataSize<double> size = DataSize<double>.FromTeraBits(terabits);
        Assert.Equal(expectedBits, size.Bits, Tolerance);
    }

    [Theory(DisplayName = "DataSize.FromTeraBytes should produce the expected Bits")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1000.0 * 1000.0 * 1000.0 * 1000.0 * 8.0)]
    [InlineData(0.5, 0.5 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 8.0)]
    [InlineData(2.0, 2.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 8.0)]
    [InlineData(10.0, 10.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 8.0)]
    public void DataSizeFromTeraBytesShouldProduceExpectedBits(double terabytes, double expectedBits)
    {
        DataSize<double> size = DataSize<double>.FromTeraBytes(terabytes);
        Assert.Equal(expectedBits, size.Bits, Tolerance);
    }

    [Theory(DisplayName = "DataSize.FromPebiBits should produce the expected Bits")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0)]
    [InlineData(0.5, 0.5 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0)]
    [InlineData(2.0, 2.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0)]
    [InlineData(10.0, 10.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0)]
    public void DataSizeFromPebiBitsShouldProduceExpectedBits(double pebibits, double expectedBits)
    {
        DataSize<double> size = DataSize<double>.FromPebiBits(pebibits);
        Assert.Equal(expectedBits, size.Bits, Tolerance);
    }

    [Theory(DisplayName = "DataSize.FromPebiBytes should produce the expected Bits")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 8.0)]
    [InlineData(0.5, 0.5 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 8.0)]
    [InlineData(2.0, 2.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 8.0)]
    [InlineData(10.0, 10.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 8.0)]
    public void DataSizeFromPebiBytesShouldProduceExpectedBits(double pebibytes, double expectedBits)
    {
        DataSize<double> size = DataSize<double>.FromPebiBytes(pebibytes);
        Assert.Equal(expectedBits, size.Bits, Tolerance);
    }

    [Theory(DisplayName = "DataSize.FromPetaBits should produce the expected Bits")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0)]
    [InlineData(0.5, 0.5 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0)]
    [InlineData(2.0, 2.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0)]
    [InlineData(10.0, 10.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0)]
    public void DataSizeFromPetaBitsShouldProduceExpectedBits(double petabits, double expectedBits)
    {
        DataSize<double> size = DataSize<double>.FromPetaBits(petabits);
        Assert.Equal(expectedBits, size.Bits, Tolerance);
    }

    [Theory(DisplayName = "DataSize.FromPetaBytes should produce the expected Bits")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 8.0)]
    [InlineData(0.5, 0.5 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 8.0)]
    [InlineData(2.0, 2.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 8.0)]
    [InlineData(10.0, 10.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 8.0)]
    public void DataSizeFromPetaBytesShouldProduceExpectedBits(double petabytes, double expectedBits)
    {
        DataSize<double> size = DataSize<double>.FromPetaBytes(petabytes);
        Assert.Equal(expectedBits, size.Bits, Tolerance);
    }

    [Theory(DisplayName = "DataSize.FromExbiBits should produce the expected Bits")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0)]
    [InlineData(0.5, 0.5 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0)]
    [InlineData(2.0, 2.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0)]
    [InlineData(10.0, 10.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0)]
    public void DataSizeFromExbiBitsShouldProduceExpectedBits(double exbibits, double expectedBits)
    {
        DataSize<double> size = DataSize<double>.FromExbiBits(exbibits);
        Assert.Equal(expectedBits, size.Bits, Tolerance);
    }

    [Theory(DisplayName = "DataSize.FromExbiBytes should produce the expected Bits")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 8.0)]
    [InlineData(0.5, 0.5 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 8.0)]
    [InlineData(2.0, 2.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 8.0)]
    [InlineData(10.0, 10.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 8.0)]
    public void DataSizeFromExbiBytesShouldProduceExpectedBits(double exbibytes, double expectedBits)
    {
        DataSize<double> size = DataSize<double>.FromExbiBytes(exbibytes);
        Assert.Equal(expectedBits, size.Bits, Tolerance);
    }

    [Theory(DisplayName = "DataSize.FromExaBits should produce the expected Bits")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0)]
    [InlineData(0.5, 0.5 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0)]
    [InlineData(2.0, 2.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0)]
    [InlineData(10.0, 10.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0)]
    public void DataSizeFromExaBitsShouldProduceExpectedBits(double exabits, double expectedBits)
    {
        DataSize<double> size = DataSize<double>.FromExaBits(exabits);
        Assert.Equal(expectedBits, size.Bits, Tolerance);
    }

    [Theory(DisplayName = "DataSize.FromExaBytes should produce the expected Bits")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 8.0)]
    [InlineData(0.5, 0.5 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 8.0)]
    [InlineData(2.0, 2.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 8.0)]
    [InlineData(10.0, 10.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 8.0)]
    public void DataSizeFromExaBytesShouldProduceExpectedBits(double exabytes, double expectedBits)
    {
        DataSize<double> size = DataSize<double>.FromExaBytes(exabytes);
        Assert.Equal(expectedBits, size.Bits, Tolerance);
    }

    [Theory(DisplayName = "DataSize.FromZebiBits should produce the expected Bits")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0)]
    [InlineData(0.5, 0.5 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0)]
    [InlineData(2.0, 2.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0)]
    [InlineData(10.0, 10.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0)]
    public void DataSizeFromZebiBitsShouldProduceExpectedBits(double zebibits, double expectedBits)
    {
        DataSize<double> size = DataSize<double>.FromZebiBits(zebibits);
        Assert.Equal(expectedBits, size.Bits, Tolerance);
    }

    [Theory(DisplayName = "DataSize.FromZebiBytes should produce the expected Bits")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 8.0)]
    [InlineData(0.5, 0.5 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 8.0)]
    [InlineData(2.0, 2.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 8.0)]
    [InlineData(10.0, 10.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 8.0)]
    public void DataSizeFromZebiBytesShouldProduceExpectedBits(double zebibytes, double expectedBits)
    {
        DataSize<double> size = DataSize<double>.FromZebiBytes(zebibytes);
        Assert.Equal(expectedBits, size.Bits, Tolerance);
    }

    [Theory(DisplayName = "DataSize.FromZettaBits should produce the expected Bits")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0)]
    [InlineData(0.5, 0.5 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0)]
    [InlineData(2.0, 2.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0)]
    [InlineData(10.0, 10.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0)]
    public void DataSizeFromZettaBitsShouldProduceExpectedBits(double zettabits, double expectedBits)
    {
        DataSize<double> size = DataSize<double>.FromZettaBits(zettabits);
        Assert.Equal(expectedBits, size.Bits, Tolerance);
    }

    [Theory(DisplayName = "DataSize.FromZettaBytes should produce the expected Bits")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 8.0)]
    [InlineData(0.5, 0.5 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 8.0)]
    [InlineData(2.0, 2.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 8.0)]
    [InlineData(10.0, 10.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 8.0)]
    public void DataSizeFromZettaBytesShouldProduceExpectedBits(double zettabytes, double expectedBits)
    {
        DataSize<double> size = DataSize<double>.FromZettaBytes(zettabytes);
        Assert.Equal(expectedBits, size.Bits, Tolerance);
    }

    [Theory(DisplayName = "DataSize.FromYobiBits should produce the expected Bits")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0)]
    [InlineData(0.5, 0.5 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0)]
    [InlineData(2.0, 2.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0)]
    [InlineData(10.0, 10.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0)]
    public void DataSizeFromYobiBitsShouldProduceExpectedBits(double yobibits, double expectedBits)
    {
        DataSize<double> size = DataSize<double>.FromYobiBits(yobibits);
        Assert.Equal(expectedBits, size.Bits, Tolerance);
    }

    [Theory(DisplayName = "DataSize.FromYobiBytes should produce the expected Bits")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 8.0)]
    [InlineData(0.5, 0.5 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 8.0)]
    [InlineData(2.0, 2.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 8.0)]
    [InlineData(10.0, 10.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 1024.0 * 8.0)]
    public void DataSizeFromYobiBytesShouldProduceExpectedBits(double yobibytes, double expectedBits)
    {
        DataSize<double> size = DataSize<double>.FromYobiBytes(yobibytes);
        Assert.Equal(expectedBits, size.Bits, Tolerance);
    }

    [Theory(DisplayName = "DataSize.FromYottaBits should produce the expected Bits")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0)]
    [InlineData(0.5, 0.5 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0)]
    [InlineData(2.0, 2.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0)]
    public void DataSizeFromYottaBitsShouldProduceExpectedBits(double yottabits, double expectedBits)
    {
        DataSize<double> size = DataSize<double>.FromYottaBits(yottabits);
        Assert.Equal(expectedBits, size.Bits, Tolerance);
    }

    [Theory(DisplayName = "DataSize.FromYottaBytes should produce the expected Bits")]
    [InlineData(0.0, 0.0)]
    [InlineData(1.0, 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 8.0)]
    [InlineData(0.5, 0.5 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 8.0)]
    [InlineData(2.0, 2.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 1000.0 * 8.0)]
    public void DataSizeFromYottaBytesShouldProduceExpectedBits(double yottabytes, double expectedBits)
    {
        DataSize<double> size = DataSize<double>.FromYottaBytes(yottabytes);
        Assert.Equal(expectedBits, size.Bits, Tolerance);
    }

    [Fact(DisplayName = "DataSize.Add should produce the expected result")]
    public void DataSizeAddShouldProduceExpectedValue()
    {
        // Given
        DataSize<double> left = DataSize<double>.FromBytes(1500.0);
        DataSize<double> right = DataSize<double>.FromBytes(500.0);

        // When
        DataSize<double> result = left.Add(right);

        // Then
        Assert.Equal(2000.0, result.Bytes, Tolerance);
    }

    [Fact(DisplayName = "DataSize.Subtract should produce the expected result")]
    public void DataSizeSubtractShouldProduceExpectedValue()
    {
        // Given
        DataSize<double> left = DataSize<double>.FromBytes(1500.0);
        DataSize<double> right = DataSize<double>.FromBytes(400.0);

        // When
        DataSize<double> result = left.Subtract(right);

        // Then
        Assert.Equal(1100.0, result.Bytes, Tolerance);
    }

    [Fact(DisplayName = "DataSize.Multiply should produce the expected result")]
    public void DataSizeMultiplyShouldProduceExpectedValue()
    {
        // Given
        DataSize<double> left = DataSize<double>.FromBytes(10.0); // 80 bits
        DataSize<double> right = DataSize<double>.FromBytes(3.0); // 24 bits

        // When
        DataSize<double> result = left.Multiply(right); // 80 * 24 = 1920 bits

        // Then
        Assert.Equal(1920.0, result.Bits, Tolerance);
        Assert.Equal(240.0, result.Bytes, Tolerance);
    }

    [Fact(DisplayName = "DataSize.Divide should produce the expected result")]
    public void DataSizeDivideShouldProduceExpectedValue()
    {
        // Given
        DataSize<double> left = DataSize<double>.FromBytes(100.0); // 800 bits
        DataSize<double> right = DataSize<double>.FromBytes(20.0); // 160 bits

        // When
        DataSize<double> result = left.Divide(right); // 800 / 160 = 5 bits

        // Then
        Assert.Equal(5.0, result.Bits, Tolerance);
        Assert.Equal(0.625, result.Bytes, Tolerance);
    }

    [Fact(DisplayName = "DataSize comparison should produce the expected result (left equal to right)")]
    public void DataSizeComparisonShouldProduceExpectedResultLeftEqualToRight()
    {
        // Given
        DataSize<double> left = DataSize<double>.FromBytes(1234.0);
        DataSize<double> right = DataSize<double>.FromBytes(1234.0);

        // When / Then
        Assert.Equal(0, DataSize<double>.Compare(left, right));
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
        DataSize<double> left = DataSize<double>.FromBytes(4567.0);
        DataSize<double> right = DataSize<double>.FromBytes(1234.0);

        // When / Then
        Assert.Equal(1, DataSize<double>.Compare(left, right));
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
        DataSize<double> left = DataSize<double>.FromBytes(1234.0);
        DataSize<double> right = DataSize<double>.FromBytes(4567.0);

        // When / Then
        Assert.Equal(-1, DataSize<double>.Compare(left, right));
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
        DataSize<double> left = DataSize<double>.FromBytes(2048.0);
        DataSize<double> right = DataSize<double>.FromBytes(2048.0);

        // When / Then
        Assert.True(DataSize<double>.Equals(left, right));
        Assert.True(left.Equals(right));
        Assert.True(left.Equals((object)right));
        Assert.True(left == right);
        Assert.False(left != right);
    }

    [Fact(DisplayName = "DataSize equality should produce the expected result (left not equal to right)")]
    public void DataSizeEqualityShouldProduceExpectedResultLeftNotEqualToRight()
    {
        // Given
        DataSize<double> left = DataSize<double>.FromBytes(2048.0);
        DataSize<double> right = DataSize<double>.FromBytes(4096.0);

        // When / Then
        Assert.False(DataSize<double>.Equals(left, right));
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
        DataSize<double> size = DataSize<double>.FromBytes(1_048_576.0); // 1 MiB

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
        DataSize<double> size = DataSize<double>.FromKiloBytes(1234.56); // 1,234.56 KB

        // When
        string formatted = size.ToString("KB2", customCulture);

        // Then (German uses '.' for thousands and ',' for decimals)
        Assert.Equal("1.234,56 KB", formatted);
    }
}
