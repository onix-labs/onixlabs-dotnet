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

using OnixLabs.Core.Units;
using Xunit;

namespace OnixLabs.Core.UnitTests.Units;

public sealed class DataSizeTests
{
    [Theory(DisplayName = "DataSize.FromNibbles should produce the expected conversion results")]
    [InlineData(0, 0)]
    [InlineData(1, 4)]
    [InlineData(2, 8)]
    [InlineData(3, 12)]
    [InlineData(4, 16)]
    [InlineData(5, 20)]
    [InlineData(6, 24)]
    [InlineData(7, 28)]
    [InlineData(8, 32)]
    [InlineData(9, 36)]
    [InlineData(10, 40)]
    public void DataSizeFromNibblesShouldProduceTheExpectedConversionResults(double nibbles, double bits)
    {
        // Arrange / Act
        DataSize<double> dataSize = DataSize.FromNibbles(nibbles);

        // Assert
        Assert.Equal(nibbles, dataSize.Nibbles);
        Assert.Equal(bits, dataSize.Bits);
    }

    [Theory(DisplayName = "DataSize.FromBytes should produce the expected conversion results")]
    [InlineData(0, 0)]
    [InlineData(1, 8)]
    [InlineData(2, 16)]
    [InlineData(3, 24)]
    [InlineData(4, 32)]
    [InlineData(5, 40)]
    [InlineData(6, 48)]
    [InlineData(7, 56)]
    [InlineData(8, 64)]
    [InlineData(9, 72)]
    [InlineData(10, 80)]
    public void DataSizeFromBytesShouldProduceTheExpectedConversionResults(double bytes, double bits)
    {
        // Arrange / Act
        DataSize<double> dataSize = DataSize.FromBytes(bytes);

        // Assert
        Assert.Equal(bytes, dataSize.Bytes);
        Assert.Equal(bits, dataSize.Bits);
    }

    [Theory(DisplayName = "DataSize.FromWords should produce the expected conversion results")]
    [InlineData(0, 0)]
    [InlineData(1, 16)]
    [InlineData(2, 32)]
    [InlineData(3, 48)]
    [InlineData(4, 64)]
    [InlineData(5, 80)]
    [InlineData(6, 96)]
    [InlineData(7, 112)]
    [InlineData(8, 128)]
    [InlineData(9, 144)]
    [InlineData(10, 160)]
    public void DataSizeFromWordsShouldProduceTheExpectedConversionResults(double words, double bits)
    {
        // Arrange / Act
        DataSize<double> dataSize = DataSize.FromWords(words);

        // Assert
        Assert.Equal(words, dataSize.Words);
        Assert.Equal(bits, dataSize.Bits);
    }

    [Theory(DisplayName = "DataSize.FromDoubleWords should produce the expected conversion results")]
    [InlineData(0, 0)]
    [InlineData(1, 32)]
    [InlineData(2, 64)]
    [InlineData(3, 96)]
    [InlineData(4, 128)]
    [InlineData(5, 160)]
    [InlineData(6, 192)]
    [InlineData(7, 224)]
    [InlineData(8, 256)]
    [InlineData(9, 288)]
    [InlineData(10, 320)]
    public void DataSizeFromDoubleWordsShouldProduceTheExpectedConversionResults(double doubleWords, double bits)
    {
        // Arrange / Act
        DataSize<double> dataSize = DataSize.FromDoubleWords(doubleWords);

        // Assert
        Assert.Equal(doubleWords, dataSize.DoubleWords);
        Assert.Equal(bits, dataSize.Bits);
    }

    [Theory(DisplayName = "DataSize.FromQuadWords should produce the expected conversion results")]
    [InlineData(0, 0)]
    [InlineData(1, 64)]
    [InlineData(2, 128)]
    [InlineData(3, 192)]
    [InlineData(4, 256)]
    [InlineData(5, 320)]
    [InlineData(6, 384)]
    [InlineData(7, 448)]
    [InlineData(8, 512)]
    [InlineData(9, 576)]
    [InlineData(10, 640)]
    public void DataSizeFromQuadWordsShouldProduceTheExpectedConversionResults(double quadWords, double bits)
    {
        // Arrange / Act
        DataSize<double> dataSize = DataSize.FromQuadWords(quadWords);

        // Assert
        Assert.Equal(quadWords, dataSize.QuadWords);
        Assert.Equal(bits, dataSize.Bits);
    }

    [Theory(DisplayName = "DataSize.FromKiloBits should produce the expected conversion results")]
    [InlineData(0, 0)]
    [InlineData(1, 1000)]
    [InlineData(2, 2000)]
    [InlineData(3, 3000)]
    [InlineData(4, 4000)]
    [InlineData(5, 5000)]
    [InlineData(6, 6000)]
    [InlineData(7, 7000)]
    [InlineData(8, 8000)]
    [InlineData(9, 9000)]
    [InlineData(10, 10000)]
    public void DataSizeFromKiloBitsShouldProduceTheExpectedConversionResults(double kiloBits, double bits)
    {
        // Arrange / Act
        DataSize<double> dataSize = DataSize.FromKiloBits(kiloBits);

        // Assert
        Assert.Equal(kiloBits, dataSize.KiloBits);
        Assert.Equal(bits, dataSize.Bits);
    }

    [Theory(DisplayName = "DataSize.FromKibiBits should produce the expected conversion results")]
    [InlineData(0, 0)]
    [InlineData(1, 1024)]
    [InlineData(2, 2048)]
    [InlineData(3, 3072)]
    [InlineData(4, 4096)]
    [InlineData(5, 5120)]
    [InlineData(6, 6144)]
    [InlineData(7, 7168)]
    [InlineData(8, 8192)]
    [InlineData(9, 9216)]
    [InlineData(10, 10240)]
    public void DataSizeFromKibiBitsShouldProduceTheExpectedConversionResults(double kibiBits, double bits)
    {
        // Arrange / Act
        DataSize<double> dataSize = DataSize.FromKibiBits(kibiBits);

        // Assert
        Assert.Equal(kibiBits, dataSize.KibiBits);
        Assert.Equal(bits, dataSize.Bits);
    }

    [Theory(DisplayName = "DataSize.FromKiloBytes should produce the expected conversion results")]
    [InlineData(0, 0)]
    [InlineData(1, 8000)]
    [InlineData(2, 16000)]
    [InlineData(3, 24000)]
    [InlineData(4, 32000)]
    [InlineData(5, 40000)]
    [InlineData(6, 48000)]
    [InlineData(7, 56000)]
    [InlineData(8, 64000)]
    [InlineData(9, 72000)]
    [InlineData(10, 80000)]
    public void DataSizeFromKiloBytesShouldProduceTheExpectedConversionResults(double kiloBytes, double bits)
    {
        // Arrange / Act
        DataSize<double> dataSize = DataSize.FromKiloBytes(kiloBytes);

        // Assert
        Assert.Equal(kiloBytes, dataSize.KiloBytes);
        Assert.Equal(bits, dataSize.Bits);
    }

    [Theory(DisplayName = "DataSize.FromKibiBytes should produce the expected conversion results")]
    [InlineData(0, 0)]
    [InlineData(1, 8192)]
    [InlineData(2, 16384)]
    [InlineData(3, 24576)]
    [InlineData(4, 32768)]
    [InlineData(5, 40960)]
    [InlineData(6, 49152)]
    [InlineData(7, 57344)]
    [InlineData(8, 65536)]
    [InlineData(9, 73728)]
    [InlineData(10, 81920)]
    public void DataSizeFromKibiBytesShouldProduceTheExpectedConversionResults(double kibiBytes, double bits)
    {
        // Arrange / Act
        DataSize<double> dataSize = DataSize.FromKibiBytes(kibiBytes);

        // Assert
        Assert.Equal(kibiBytes, dataSize.KibiBytes);
        Assert.Equal(bits, dataSize.Bits);
    }

    [Theory(DisplayName = "DataSize.FromMegaBits should produce the expected conversion results")]
    [InlineData(0, 0)]
    [InlineData(1, 1000000)]
    [InlineData(2, 2000000)]
    [InlineData(3, 3000000)]
    [InlineData(4, 4000000)]
    [InlineData(5, 5000000)]
    [InlineData(6, 6000000)]
    [InlineData(7, 7000000)]
    [InlineData(8, 8000000)]
    [InlineData(9, 9000000)]
    [InlineData(10, 10000000)]
    public void DataSizeFromMegaBitsShouldProduceTheExpectedConversionResults(double megaBits, double bits)
    {
        // Arrange / Act
        DataSize<double> dataSize = DataSize.FromMegaBits(megaBits);

        // Assert
        Assert.Equal(megaBits, dataSize.MegaBits);
        Assert.Equal(bits, dataSize.Bits);
    }

    [Theory(DisplayName = "DataSize.FromMebiBits should produce the expected conversion results")]
    [InlineData(0, 0)]
    [InlineData(1, 1048576)]
    [InlineData(2, 2097152)]
    [InlineData(3, 3145728)]
    [InlineData(4, 4194304)]
    [InlineData(5, 5242880)]
    [InlineData(6, 6291456)]
    [InlineData(7, 7340032)]
    [InlineData(8, 8388608)]
    [InlineData(9, 9437184)]
    [InlineData(10, 10485760)]
    public void DataSizeFromMebiBitsShouldProduceTheExpectedConversionResults(double mebiBits, double bits)
    {
        // Arrange / Act
        DataSize<double> dataSize = DataSize.FromMebiBits(mebiBits);

        // Assert
        Assert.Equal(mebiBits, dataSize.MebiBits);
        Assert.Equal(bits, dataSize.Bits);
    }

    [Theory(DisplayName = "DataSize.FromMegaBytes should produce the expected conversion results")]
    [InlineData(0, 0)]
    [InlineData(1, 8000000)]
    [InlineData(2, 16000000)]
    [InlineData(3, 24000000)]
    [InlineData(4, 32000000)]
    [InlineData(5, 40000000)]
    [InlineData(6, 48000000)]
    [InlineData(7, 56000000)]
    [InlineData(8, 64000000)]
    [InlineData(9, 72000000)]
    [InlineData(10, 80000000)]
    public void DataSizeFromMegaBytesShouldProduceTheExpectedConversionResults(double megaBytes, double bits)
    {
        // Arrange / Act
        DataSize<double> dataSize = DataSize.FromMegaBytes(megaBytes);

        // Assert
        Assert.Equal(megaBytes, dataSize.MegaBytes);
        Assert.Equal(bits, dataSize.Bits);
    }

    [Theory(DisplayName = "DataSize.FromMebiBytes should produce the expected conversion results")]
    [InlineData(0, 0)]
    [InlineData(1, 8388608)]
    [InlineData(2, 16777216)]
    [InlineData(3, 25165824)]
    [InlineData(4, 33554432)]
    [InlineData(5, 41943040)]
    [InlineData(6, 50331648)]
    [InlineData(7, 58720256)]
    [InlineData(8, 67108864)]
    [InlineData(9, 75497472)]
    [InlineData(10, 83886080)]
    public void DataSizeFromMebiBytesShouldProduceTheExpectedConversionResults(double mebiBytes, double bits)
    {
        // Arrange / Act
        DataSize<double> dataSize = DataSize.FromMebiBytes(mebiBytes);

        // Assert
        Assert.Equal(mebiBytes, dataSize.MebiBytes);
        Assert.Equal(bits, dataSize.Bits);
    }

    [Theory(DisplayName = "DataSize.FromGigaBits should produce the expected conversion results")]
    [InlineData(0, 0)]
    [InlineData(1, 1000000000)]
    [InlineData(2, 2000000000)]
    [InlineData(3, 3000000000)]
    [InlineData(4, 4000000000)]
    [InlineData(5, 5000000000)]
    [InlineData(6, 6000000000)]
    [InlineData(7, 7000000000)]
    [InlineData(8, 8000000000)]
    [InlineData(9, 9000000000)]
    [InlineData(10, 10000000000)]
    public void DataSizeFromGigaBitsShouldProduceTheExpectedConversionResults(double gigaBits, double bits)
    {
        // Arrange / Act
        DataSize<double> dataSize = DataSize.FromGigaBits(gigaBits);

        // Assert
        Assert.Equal(gigaBits, dataSize.GigaBits);
        Assert.Equal(bits, dataSize.Bits);
    }

    [Theory(DisplayName = "DataSize.FromGibiBits should produce the expected conversion results")]
    [InlineData(0, 0)]
    [InlineData(1, 1073741824)]
    [InlineData(2, 2147483648)]
    [InlineData(3, 3221225472)]
    [InlineData(4, 4294967296)]
    [InlineData(5, 5368709120)]
    [InlineData(6, 6442450944)]
    [InlineData(7, 7516192768)]
    [InlineData(8, 8589934592)]
    [InlineData(9, 9663676416)]
    [InlineData(10, 10737418240)]
    public void DataSizeFromGibiBitsShouldProduceTheExpectedConversionResults(double gibiBits, double bits)
    {
        // Arrange / Act
        DataSize<double> dataSize = DataSize.FromGibiBits(gibiBits);

        // Assert
        Assert.Equal(gibiBits, dataSize.GibiBits);
        Assert.Equal(bits, dataSize.Bits);
    }

    [Theory(DisplayName = "DataSize.FromGigaBytes should produce the expected conversion results")]
    [InlineData(0, 0)]
    [InlineData(1, 8000000000)]
    [InlineData(2, 16000000000)]
    [InlineData(3, 24000000000)]
    [InlineData(4, 32000000000)]
    [InlineData(5, 40000000000)]
    [InlineData(6, 48000000000)]
    [InlineData(7, 56000000000)]
    [InlineData(8, 64000000000)]
    [InlineData(9, 72000000000)]
    [InlineData(10, 80000000000)]
    public void DataSizeFromGigaBytesShouldProduceTheExpectedConversionResults(double gigaBytes, double bits)
    {
        // Arrange / Act
        DataSize<double> dataSize = DataSize.FromGigaBytes(gigaBytes);

        // Assert
        Assert.Equal(gigaBytes, dataSize.GigaBytes);
        Assert.Equal(bits, dataSize.Bits);
    }

    [Theory(DisplayName = "DataSize.FromGibiBytes should produce the expected conversion results")]
    [InlineData(0, 0)]
    [InlineData(1, 8589934592)]
    [InlineData(2, 17179869184)]
    [InlineData(3, 25769803776)]
    [InlineData(4, 34359738368)]
    [InlineData(5, 42949672960)]
    [InlineData(6, 51539607552)]
    [InlineData(7, 60129542144)]
    [InlineData(8, 68719476736)]
    [InlineData(9, 77309411328)]
    [InlineData(10, 85899345920)]
    public void DataSizeFromGibiBytesShouldProduceTheExpectedConversionResults(double gibiBytes, double bits)
    {
        // Arrange / Act
        DataSize<double> dataSize = DataSize.FromGibiBytes(gibiBytes);

        // Assert
        Assert.Equal(gibiBytes, dataSize.GibiBytes);
        Assert.Equal(bits, dataSize.Bits);
    }

    [Theory(DisplayName = "DataSize.FromTeraBits should produce the expected conversion results")]
    [InlineData(0, 0)]
    [InlineData(1, 1000000000000)]
    [InlineData(2, 2000000000000)]
    [InlineData(3, 3000000000000)]
    [InlineData(4, 4000000000000)]
    [InlineData(5, 5000000000000)]
    [InlineData(6, 6000000000000)]
    [InlineData(7, 7000000000000)]
    [InlineData(8, 8000000000000)]
    [InlineData(9, 9000000000000)]
    [InlineData(10, 10000000000000)]
    public void DataSizeFromTeraBitsShouldProduceTheExpectedConversionResults(double teraBits, double bits)
    {
        // Arrange / Act
        DataSize<double> dataSize = DataSize.FromTeraBits(teraBits);

        // Assert
        Assert.Equal(teraBits, dataSize.TeraBits);
        Assert.Equal(bits, dataSize.Bits);
    }

    [Theory(DisplayName = "DataSize.FromTebiBits should produce the expected conversion results")]
    [InlineData(0, 0)]
    [InlineData(1, 1099511627776)]
    [InlineData(2, 2199023255552)]
    [InlineData(3, 3298534883328)]
    [InlineData(4, 4398046511104)]
    [InlineData(5, 5497558138880)]
    [InlineData(6, 6597069766656)]
    [InlineData(7, 7696581394432)]
    [InlineData(8, 8796093022208)]
    [InlineData(9, 9895604649984)]
    [InlineData(10, 10995116277760)]
    public void DataSizeFromTebiBitsShouldProduceTheExpectedConversionResults(double tebiBits, double bits)
    {
        // Arrange / Act
        DataSize<double> dataSize = DataSize.FromTebiBits(tebiBits);

        // Assert
        Assert.Equal(tebiBits, dataSize.TebiBits);
        Assert.Equal(bits, dataSize.Bits);
    }

    [Theory(DisplayName = "DataSize.FromTeraBytes should produce the expected conversion results")]
    [InlineData(0, 0)]
    [InlineData(1, 8000000000000)]
    [InlineData(2, 16000000000000)]
    [InlineData(3, 24000000000000)]
    [InlineData(4, 32000000000000)]
    [InlineData(5, 40000000000000)]
    [InlineData(6, 48000000000000)]
    [InlineData(7, 56000000000000)]
    [InlineData(8, 64000000000000)]
    [InlineData(9, 72000000000000)]
    [InlineData(10, 80000000000000)]
    public void DataSizeFromTeraBytesShouldProduceTheExpectedConversionResults(double teraBytes, double bits)
    {
        // Arrange / Act
        DataSize<double> dataSize = DataSize.FromTeraBytes(teraBytes);

        // Assert
        Assert.Equal(teraBytes, dataSize.TeraBytes);
        Assert.Equal(bits, dataSize.Bits);
    }

    [Theory(DisplayName = "DataSize.FromTebiBytes should produce the expected conversion results")]
    [InlineData(0, 0)]
    [InlineData(1, 8796093022208)]
    [InlineData(2, 17592186044416)]
    [InlineData(3, 26388279066624)]
    [InlineData(4, 35184372088832)]
    [InlineData(5, 43980465111040)]
    [InlineData(6, 52776558133248)]
    [InlineData(7, 61572651155456)]
    [InlineData(8, 70368744177664)]
    [InlineData(9, 79164837199872)]
    [InlineData(10, 87960930222080)]
    public void DataSizeFromTebiBytesShouldProduceTheExpectedConversionResults(double tebiBytes, double bits)
    {
        // Arrange / Act
        DataSize<double> dataSize = DataSize.FromTebiBytes(tebiBytes);

        // Assert
        Assert.Equal(tebiBytes, dataSize.TebiBytes);
        Assert.Equal(bits, dataSize.Bits);
    }

    [Theory(DisplayName = "DataSize.FromPetaBits should produce the expected conversion results")]
    [InlineData(0, 0)]
    [InlineData(1, 1000000000000000)]
    [InlineData(2, 2000000000000000)]
    [InlineData(3, 3000000000000000)]
    [InlineData(4, 4000000000000000)]
    [InlineData(5, 5000000000000000)]
    [InlineData(6, 6000000000000000)]
    [InlineData(7, 7000000000000000)]
    [InlineData(8, 8000000000000000)]
    [InlineData(9, 9000000000000000)]
    [InlineData(10, 10000000000000000)]
    public void DataSizeFromPetaBitsShouldProduceTheExpectedConversionResults(double petaBits, double bits)
    {
        // Arrange / Act
        DataSize<double> dataSize = DataSize.FromPetaBits(petaBits);

        // Assert
        Assert.Equal(petaBits, dataSize.PetaBits);
        Assert.Equal(bits, dataSize.Bits);
    }

    [Theory(DisplayName = "DataSize.FromPebiBits should produce the expected conversion results")]
    [InlineData(0, 0)]
    [InlineData(1, 1125899906842624)]
    [InlineData(2, 2251799813685248)]
    [InlineData(3, 3377699720527872)]
    [InlineData(4, 4503599627370496)]
    [InlineData(5, 5629499534213120)]
    [InlineData(6, 6755399441055744)]
    [InlineData(7, 7881299347898368)]
    [InlineData(8, 9007199254740992)]
    [InlineData(9, 10133099161583616)]
    [InlineData(10, 11258999068426240)]
    public void DataSizeFromPebiBitsShouldProduceTheExpectedConversionResults(double pebiBits, double bits)
    {
        // Arrange / Act
        DataSize<double> dataSize = DataSize.FromPebiBits(pebiBits);

        // Assert
        Assert.Equal(pebiBits, dataSize.PebiBits);
        Assert.Equal(bits, dataSize.Bits);
    }

    [Theory(DisplayName = "DataSize.FromPetaBytes should produce the expected conversion results")]
    [InlineData(0, 0)]
    [InlineData(1, 8000000000000000)]
    [InlineData(2, 16000000000000000)]
    [InlineData(3, 24000000000000000)]
    [InlineData(4, 32000000000000000)]
    [InlineData(5, 40000000000000000)]
    [InlineData(6, 48000000000000000)]
    [InlineData(7, 56000000000000000)]
    [InlineData(8, 64000000000000000)]
    [InlineData(9, 72000000000000000)]
    [InlineData(10, 80000000000000000)]
    public void DataSizeFromPetaBytesShouldProduceTheExpectedConversionResults(double petaBytes, double bits)
    {
        // Arrange / Act
        DataSize<double> dataSize = DataSize.FromPetaBytes(petaBytes);

        // Assert
        Assert.Equal(petaBytes, dataSize.PetaBytes);
        Assert.Equal(bits, dataSize.Bits);
    }

    [Theory(DisplayName = "DataSize.FromPebiBytes should produce the expected conversion results")]
    [InlineData(0, 0)]
    [InlineData(1, 9007199254740992)]
    [InlineData(2, 18014398509481984)]
    [InlineData(3, 27021597764222976)]
    [InlineData(4, 36028797018963970)]
    [InlineData(5, 45035996273704960)]
    [InlineData(6, 54043195528445950)]
    [InlineData(7, 63050394783186940)]
    [InlineData(8, 72057594037927940)]
    [InlineData(9, 81064793292668930)]
    [InlineData(10, 90071992547409920)]
    public void DataSizeFromPebiBytesShouldProduceTheExpectedConversionResults(double pebiBytes, double bits)
    {
        // Arrange / Act
        DataSize<double> dataSize = DataSize.FromPebiBytes(pebiBytes);

        // Assert
        Assert.Equal(pebiBytes, dataSize.PebiBytes);
        Assert.Equal(bits, dataSize.Bits);
    }

    [Theory(DisplayName = "DataSize.FromExaBits should produce the expected conversion results")]
    [InlineData(0, 0)]
    [InlineData(1, 1E+18)]
    [InlineData(2, 2E+18)]
    [InlineData(3, 3E+18)]
    [InlineData(4, 4E+18)]
    [InlineData(5, 5E+18)]
    [InlineData(6, 6E+18)]
    [InlineData(7, 7E+18)]
    [InlineData(8, 8E+18)]
    [InlineData(9, 9E+18)]
    [InlineData(10, 1E+19)]
    public void DataSizeFromExaBitsShouldProduceTheExpectedConversionResults(double exaBits, double bits)
    {
        // Arrange / Act
        DataSize<double> dataSize = DataSize.FromExaBits(exaBits);

        // Assert
        Assert.Equal(exaBits, dataSize.ExaBits);
        Assert.Equal(bits, dataSize.Bits);
    }

    [Theory(DisplayName = "DataSize.FromExbiBits should produce the expected conversion results")]
    [InlineData(0, 0)]
    [InlineData(1, 1.152921504606847E+18)]
    [InlineData(2, 2.305843009213694E+18)]
    [InlineData(3, 3.458764513820541E+18)]
    [InlineData(4, 4.611686018427388E+18)]
    [InlineData(5, 5.764607523034235E+18)]
    [InlineData(6, 6.917529027641082E+18)]
    [InlineData(7, 8.070450532247929E+18)]
    [InlineData(8, 9.223372036854776E+18)]
    [InlineData(9, 1.0376293541461623E+19)]
    [InlineData(10, 1.152921504606847E+19)]
    public void DataSizeFromExbiBitsShouldProduceTheExpectedConversionResults(double exbiBits, double bits)
    {
        // Arrange / Act
        DataSize<double> dataSize = DataSize.FromExbiBits(exbiBits);

        // Assert
        Assert.Equal(exbiBits, dataSize.ExbiBits);
        Assert.Equal(bits, dataSize.Bits);
    }

    [Theory(DisplayName = "DataSize.FromExaBytes should produce the expected conversion results")]
    [InlineData(0, 0)]
    [InlineData(1, 8E+18)]
    [InlineData(2, 1.6E+19)]
    [InlineData(3, 2.4E+19)]
    [InlineData(4, 3.2E+19)]
    [InlineData(5, 4E+19)]
    [InlineData(6, 4.8E+19)]
    [InlineData(7, 5.6E+19)]
    [InlineData(8, 6.4E+19)]
    [InlineData(9, 7.2E+19)]
    [InlineData(10, 8E+19)]
    public void DataSizeFromExaBytesShouldProduceTheExpectedConversionResults(double exaBytes, double bits)
    {
        // Arrange / Act
        DataSize<double> dataSize = DataSize.FromExaBytes(exaBytes);

        // Assert
        Assert.Equal(exaBytes, dataSize.ExaBytes);
        Assert.Equal(bits, dataSize.Bits);
    }

    [Theory(DisplayName = "DataSize.FromExbiBytes should produce the expected conversion results")]
    [InlineData(0, 0)]
    [InlineData(1, 9.223372036854776E+18)]
    [InlineData(2, 1.8446744073709552E+19)]
    [InlineData(3, 2.7670116110564327E+19)]
    [InlineData(4, 3.6893488147419103E+19)]
    [InlineData(5, 4.611686018427388E+19)]
    [InlineData(6, 5.5340232221128655E+19)]
    [InlineData(7, 6.456360425798343E+19)]
    [InlineData(8, 7.378697629483821E+19)]
    [InlineData(9, 8.301034833169298E+19)]
    [InlineData(10, 9.223372036854776E+19)]
    public void DataSizeFromExbiBytesShouldProduceTheExpectedConversionResults(double exbiBytes, double bits)
    {
        // Arrange / Act
        DataSize<double> dataSize = DataSize.FromExbiBytes(exbiBytes);

        // Assert
        Assert.Equal(exbiBytes, dataSize.ExbiBytes);
        Assert.Equal(bits, dataSize.Bits);
    }

    [Theory(DisplayName = "DataSize.FromZettaBits should produce the expected conversion results")]
    [InlineData(0, 0)]
    [InlineData(1, 1E+21)]
    [InlineData(2, 2E+21)]
    [InlineData(3, 3E+21)]
    [InlineData(4, 4E+21)]
    [InlineData(5, 5E+21)]
    [InlineData(6, 6E+21)]
    [InlineData(7, 7E+21)]
    [InlineData(8, 8E+21)]
    [InlineData(9, 9E+21)]
    [InlineData(10, 1E+22)]
    public void DataSizeFromZettaBitsShouldProduceTheExpectedConversionResults(double zettaBits, double bits)
    {
        // Arrange / Act
        DataSize<double> dataSize = DataSize.FromZettaBits(zettaBits);

        // Assert
        Assert.Equal(zettaBits, dataSize.ZettaBits);
        Assert.Equal(bits, dataSize.Bits);
    }

    [Theory(DisplayName = "DataSize.FromZebiBits should produce the expected conversion results")]
    [InlineData(0, 0)]
    [InlineData(1, 1.1805916207174113E+21)]
    [InlineData(2, 2.3611832414348226E+21)]
    [InlineData(3, 3.541774862152234E+21)]
    [InlineData(4, 4.722366482869645E+21)]
    [InlineData(5, 5.902958103587057E+21)]
    [InlineData(6, 7.083549724304468E+21)]
    [InlineData(7, 8.264141345021879E+21)]
    [InlineData(8, 9.44473296573929E+21)]
    [InlineData(9, 1.0625324586456702E+22)]
    [InlineData(10, 1.1805916207174113E+22)]
    public void DataSizeFromZebiBitsShouldProduceTheExpectedConversionResults(double zebiBits, double bits)
    {
        // Arrange / Act
        DataSize<double> dataSize = DataSize.FromZebiBits(zebiBits);

        // Assert
        Assert.Equal(zebiBits, dataSize.ZebiBits);
        Assert.Equal(bits, dataSize.Bits);
    }

    [Theory(DisplayName = "DataSize.FromZettaBytes should produce the expected conversion results")]
    [InlineData(0, 0)]
    [InlineData(1, 8E+21)]
    [InlineData(2, 1.6E+22)]
    [InlineData(3, 2.4E+22)]
    [InlineData(4, 3.2E+22)]
    [InlineData(5, 4E+22)]
    [InlineData(6, 4.8E+22)]
    [InlineData(7, 5.6E+22)]
    [InlineData(8, 6.4E+22)]
    [InlineData(9, 7.2E+22)]
    [InlineData(10, 8E+22)]
    public void DataSizeFromZettaBytesShouldProduceTheExpectedConversionResults(double zettaBytes, double bits)
    {
        // Arrange / Act
        DataSize<double> dataSize = DataSize.FromZettaBytes(zettaBytes);

        // Assert
        Assert.Equal(zettaBytes, dataSize.ZettaBytes);
        Assert.Equal(bits, dataSize.Bits);
    }

    [Theory(DisplayName = "DataSize.FromZebiBytes should produce the expected conversion results")]
    [InlineData(0, 0)]
    [InlineData(1, 9.44473296573929E+21)]
    [InlineData(2, 1.888946593147858E+22)]
    [InlineData(3, 2.833419889721787E+22)]
    [InlineData(4, 3.777893186295716E+22)]
    [InlineData(5, 4.722366482869645E+22)]
    [InlineData(6, 5.666839779443574E+22)]
    [InlineData(7, 6.611313076017503E+22)]
    [InlineData(8, 7.555786372591432E+22)]
    [InlineData(9, 8.500259669165361E+22)]
    [InlineData(10, 9.44473296573929E+22)]
    public void DataSizeFromZebiBytesShouldProduceTheExpectedConversionResults(double zebiBytes, double bits)
    {
        // Arrange / Act
        DataSize<double> dataSize = DataSize.FromZebiBytes(zebiBytes);

        // Assert
        Assert.Equal(zebiBytes, dataSize.ZebiBytes);
        Assert.Equal(bits, dataSize.Bits);
    }

    [Theory(DisplayName = "DataSize.FromYottaBits should produce the expected conversion results")]
    [InlineData(0, 0)]
    [InlineData(1, 1E+24)]
    [InlineData(2, 2E+24)]
    [InlineData(3, 3E+24)]
    [InlineData(4, 4E+24)]
    [InlineData(4.999999999999999, 4.999999999999999E+24)]
    [InlineData(6, 6E+24)]
    [InlineData(7.000000000000001, 7E+24)]
    [InlineData(8, 8E+24)]
    [InlineData(9, 8.999999999999999E+24)]
    [InlineData(9.999999999999998, 9.999999999999999E+24)]
    public void DataSizeFromYottaBitsShouldProduceTheExpectedConversionResults(double yottaBits, double bits)
    {
        // Arrange / Act
        DataSize<double> dataSize = DataSize.FromYottaBits(yottaBits);

        // Assert
        Assert.Equal(yottaBits, dataSize.YottaBits);
        Assert.Equal(bits, dataSize.Bits);
    }

    [Theory(DisplayName = "DataSize.FromYobiBits should produce the expected conversion results")]
    [InlineData(0, 0)]
    [InlineData(1, 1.2089258196146292E+24)]
    [InlineData(2, 2.4178516392292583E+24)]
    [InlineData(3, 3.6267774588438875E+24)]
    [InlineData(4, 4.835703278458517E+24)]
    [InlineData(5, 6.044629098073146E+24)]
    [InlineData(6, 7.253554917687775E+24)]
    [InlineData(7, 8.462480737302404E+24)]
    [InlineData(8, 9.671406556917033E+24)]
    [InlineData(9, 1.0880332376531663E+25)]
    [InlineData(10, 1.2089258196146292E+25)]
    public void DataSizeFromYobiBitsShouldProduceTheExpectedConversionResults(double yobiBits, double bits)
    {
        // Arrange / Act
        DataSize<double> dataSize = DataSize.FromYobiBits(yobiBits);

        // Assert
        Assert.Equal(yobiBits, dataSize.YobiBits);
        Assert.Equal(bits, dataSize.Bits);
    }

    [Theory(DisplayName = "DataSize.FromYottaBytes should produce the expected conversion results")]
    [InlineData(0, 0)]
    [InlineData(1, 8E+24)]
    [InlineData(2, 1.6E+25)]
    [InlineData(3, 2.4E+25)]
    [InlineData(4, 3.2E+25)]
    [InlineData(4.999999999999999, 3.9999999999999995E+25)]
    [InlineData(6, 4.8E+25)]
    [InlineData(7.000000000000001, 5.6E+25)]
    [InlineData(8, 6.4E+25)]
    [InlineData(9, 7.1999999999999994E+25)]
    [InlineData(9.999999999999998, 7.999999999999999E+25)]
    public void DataSizeFromYottaBytesShouldProduceTheExpectedConversionResults(double yottaBytes, double bits)
    {
        // Arrange / Act
        DataSize<double> dataSize = DataSize.FromYottaBytes(yottaBytes);

        // Assert
        Assert.Equal(yottaBytes, dataSize.YottaBytes);
        Assert.Equal(bits, dataSize.Bits);
    }

    [Theory(DisplayName = "DataSize.FromYobiBytes should produce the expected conversion results")]
    [InlineData(0, 0)]
    [InlineData(1, 9.671406556917033E+24)]
    [InlineData(2, 1.9342813113834067E+25)]
    [InlineData(3, 2.90142196707511E+25)]
    [InlineData(4, 3.8685626227668134E+25)]
    [InlineData(5, 4.835703278458517E+25)]
    [InlineData(6, 5.80284393415022E+25)]
    [InlineData(7, 6.769984589841923E+25)]
    [InlineData(8, 7.737125245533627E+25)]
    [InlineData(9, 8.70426590122533E+25)]
    [InlineData(10, 9.671406556917033E+25)]
    public void DataSizeFromYobiBytesShouldProduceTheExpectedConversionResults(double yobiBytes, double bits)
    {
        // Arrange / Act
        DataSize<double> dataSize = DataSize.FromYobiBytes(yobiBytes);

        // Assert
        Assert.Equal(yobiBytes, dataSize.YobiBytes);
        Assert.Equal(bits, dataSize.Bits);
    }
}
