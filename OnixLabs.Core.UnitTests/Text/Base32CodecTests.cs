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
using System.Text;
using OnixLabs.Core.Text;
using OnixLabs.Core.UnitTests.Data;

namespace OnixLabs.Core.UnitTests.Text;

public sealed class Base32CodecTests
{
    [Theory(DisplayName = "Base32Codec.Encode should produce the expected result (UnpaddedRfc4648Uppercase)")]
    [InlineData("", "")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "IFBEGRCFIZDUQSKKJNGE2TSPKBIVEU2UKVLFOWCZLI")]
    [InlineData("abcdefghijklmnopqrstuvwxyz", "MFRGGZDFMZTWQ2LKNNWG23TPOBYXE43UOV3HO6DZPI")]
    [InlineData("0123456789", "GAYTEMZUGU3DOOBZ")]
    public void Base32CodecEncodeShouldProduceExpectedResultUnpaddedRfc4648Uppercase(string value, string expected)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base32;
        byte[] bytes = value.ToByteArray();

        // When
        string actual = codec.Encode(bytes);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base32Codec.Decode should produce the expected result (UnpaddedRfc4648Uppercase)")]
    [InlineData("", "")]
    [InlineData("IFBEGRCFIZDUQSKKJNGE2TSPKBIVEU2UKVLFOWCZLI", "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    [InlineData("MFRGGZDFMZTWQ2LKNNWG23TPOBYXE43UOV3HO6DZPI", "abcdefghijklmnopqrstuvwxyz")]
    [InlineData("GAYTEMZUGU3DOOBZ", "0123456789")]
    public void Base32CodecDecodeShouldProduceExpectedResultUnpaddedRfc4648Uppercase(string value, string expected)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base32;

        // When
        byte[] bytes = codec.Decode(value);
        string actual = Encoding.UTF8.GetString(bytes);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base32Codec.Encode should produce the expected result (UnpaddedBase32HexUppercase)")]
    [InlineData("", "")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "85146H258P3KGIAA9D64QJIFA18L4KQKALB5EM2PB8")]
    [InlineData("abcdefghijklmnopqrstuvwxyz", "C5H66P35CPJMGQBADDM6QRJFE1ON4SRKELR7EU3PF8")]
    [InlineData("0123456789", "60OJ4CPK6KR3EE1P")]
    public void Base32CodecEncodeShouldProduceExpectedResultBase32Hex(string value, string expected)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base32;
        byte[] bytes = value.ToByteArray();

        // When
        string actual = codec.Encode(bytes, Base32FormatProvider.UnpaddedBase32HexUppercase);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base32Codec.Decode should produce the expected result (UnpaddedBase32HexUppercase)")]
    [InlineData("", "")]
    [InlineData("85146H258P3KGIAA9D64QJIFA18L4KQKALB5EM2PB8", "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    [InlineData("C5H66P35CPJMGQBADDM6QRJFE1ON4SRKELR7EU3PF8", "abcdefghijklmnopqrstuvwxyz")]
    [InlineData("60OJ4CPK6KR3EE1P", "0123456789")]
    public void Base32CodecDecodeShouldProduceExpectedResultBase32Hex(string value, string expected)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base32;

        // When
        byte[] bytes = codec.Decode(value, Base32FormatProvider.UnpaddedBase32HexUppercase);
        string actual = Encoding.UTF8.GetString(bytes);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base32Codec.Encode should produce the expected result (UnpaddedCrockford)")]
    [InlineData("", "")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "85146H258S3MGJAA9D64TKJFA18N4MTMANB5EP2SB8")]
    [InlineData("abcdefghijklmnopqrstuvwxyz", "C5H66S35CSKPGTBADDP6TVKFE1RQ4WVMENV7EY3SF8")]
    [InlineData("0123456789", "60RK4CSM6MV3EE1S")]
    public void Base32CodecEncodeShouldProduceExpectedResultCrockford(string value, string expected)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base32;
        byte[] bytes = value.ToByteArray();

        // When
        string actual = codec.Encode(bytes, Base32FormatProvider.UnpaddedCrockford);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base32Codec.Decode should produce the expected result (UnpaddedCrockford)")]
    [InlineData("", "")]
    [InlineData("85146H258S3MGJAA9D64TKJFA18N4MTMANB5EP2SB8", "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    [InlineData("C5H66S35CSKPGTBADDP6TVKFE1RQ4WVMENV7EY3SF8", "abcdefghijklmnopqrstuvwxyz")]
    [InlineData("60RK4CSM6MV3EE1S", "0123456789")]
    public void Base32CodecDecodeShouldProduceExpectedResultCrockford(string value, string expected)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base32;

        // When
        byte[] bytes = codec.Decode(value, Base32FormatProvider.UnpaddedCrockford);
        string actual = Encoding.UTF8.GetString(bytes);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base32Codec.Encode should produce the expected result (UnpaddedGeoHash)")]
    [InlineData("", "")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "85146j258t3nhkbb9e64umkgb18p4nunbpc5fq2tc8")]
    [InlineData("abcdefghijklmnopqrstuvwxyz", "d5j66t35dtmqhucbeeq6uvmgf1sr4wvnfpv7fy3tg8")]
    [InlineData("0123456789", "60sm4dtn6nv3ff1t")]
    public void Base32CodecEncodeShouldProduceExpectedResultGeoHash(string value, string expected)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base32;
        byte[] bytes = value.ToByteArray();

        // When
        string actual = codec.Encode(bytes, Base32FormatProvider.UnpaddedGeoHash);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base32Codec.Decode should produce the expected result (UnpaddedGeoHash)")]
    [InlineData("", "")]
    [InlineData("85146j258t3nhkbb9e64umkgb18p4nunbpc5fq2tc8", "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    [InlineData("d5j66t35dtmqhucbeeq6uvmgf1sr4wvnfpv7fy3tg8", "abcdefghijklmnopqrstuvwxyz")]
    [InlineData("60sm4dtn6nv3ff1t", "0123456789")]
    public void Base32CodecDecodeShouldProduceExpectedResultGeoHash(string value, string expected)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base32;

        // When
        byte[] bytes = codec.Decode(value, Base32FormatProvider.UnpaddedGeoHash);
        string actual = Encoding.UTF8.GetString(bytes);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base32Codec.Encode should produce the expected result (UnpaddedZBase32)")]
    [InlineData("", "")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "efbrgtnfe3dwo1kkjpgr4u1xkbeirw4wkimfqsn3me")]
    [InlineData("abcdefghijklmnopqrstuvwxyz", "cftgg3dfc3uso4mkppsg45uxqbazrh5wqi58q6d3xe")]
    [InlineData("0123456789", "gyaurc3wgw5dqqb3")]
    public void Base32CodecEncodeShouldProduceExpectedResultZBase32(string value, string expected)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base32;
        byte[] bytes = value.ToByteArray();

        // When
        string actual = codec.Encode(bytes, Base32FormatProvider.UnpaddedZBase32);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base32Codec.Decode should produce the expected result (UnpaddedZBase32)")]
    [InlineData("", "")]
    [InlineData("efbrgtnfe3dwo1kkjpgr4u1xkbeirw4wkimfqsn3me", "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    [InlineData("cftgg3dfc3uso4mkppsg45uxqbazrh5wqi58q6d3xe", "abcdefghijklmnopqrstuvwxyz")]
    [InlineData("gyaurc3wgw5dqqb3", "0123456789")]
    public void Base32CodecDecodeShouldProduceExpectedResultZBase32(string value, string expected)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base32;

        // When
        byte[] bytes = codec.Decode(value, Base32FormatProvider.UnpaddedZBase32);
        string actual = Encoding.UTF8.GetString(bytes);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base32Codec.Encode should produce the expected result (PaddedRfc4648Uppercase)")]
    [InlineData("", "")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "IFBEGRCFIZDUQSKKJNGE2TSPKBIVEU2UKVLFOWCZLI======")]
    [InlineData("abcdefghijklmnopqrstuvwxyz", "MFRGGZDFMZTWQ2LKNNWG23TPOBYXE43UOV3HO6DZPI======")]
    [InlineData("0123456789", "GAYTEMZUGU3DOOBZ")]
    public void Base32CodecEncodeShouldProduceExpectedResultPaddedRfc4648Uppercase(string value, string expected)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base32;
        byte[] bytes = value.ToByteArray();

        // When
        string actual = codec.Encode(bytes, Base32FormatProvider.PaddedRfc4648Uppercase);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base32Codec.Decode should produce the expected result (PaddedRfc4648Uppercase)")]
    [InlineData("", "")]
    [InlineData("IFBEGRCFIZDUQSKKJNGE2TSPKBIVEU2UKVLFOWCZLI======", "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    [InlineData("MFRGGZDFMZTWQ2LKNNWG23TPOBYXE43UOV3HO6DZPI======", "abcdefghijklmnopqrstuvwxyz")]
    [InlineData("GAYTEMZUGU3DOOBZ", "0123456789")]
    public void Base32CodecDecodeShouldProduceExpectedResultPaddedRfc4648Uppercase(string value, string expected)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base32;

        // When
        byte[] bytes = codec.Decode(value, Base32FormatProvider.PaddedRfc4648Uppercase);
        string actual = Encoding.UTF8.GetString(bytes);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base32Codec.Encode should produce the expected result (PaddedBase32HexUppercase)")]
    [InlineData("", "")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "85146H258P3KGIAA9D64QJIFA18L4KQKALB5EM2PB8======")]
    [InlineData("abcdefghijklmnopqrstuvwxyz", "C5H66P35CPJMGQBADDM6QRJFE1ON4SRKELR7EU3PF8======")]
    [InlineData("0123456789", "60OJ4CPK6KR3EE1P")]
    public void Base32CodecEncodeShouldProduceExpectedResultPaddedBase32HexUppercase(string value, string expected)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base32;
        byte[] bytes = value.ToByteArray();

        // When
        string actual = codec.Encode(bytes, Base32FormatProvider.PaddedBase32HexUppercase);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base32Codec.Decode should produce the expected result (PaddedBase32HexUppercase)")]
    [InlineData("", "")]
    [InlineData("85146H258P3KGIAA9D64QJIFA18L4KQKALB5EM2PB8======", "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    [InlineData("C5H66P35CPJMGQBADDM6QRJFE1ON4SRKELR7EU3PF8======", "abcdefghijklmnopqrstuvwxyz")]
    [InlineData("60OJ4CPK6KR3EE1P", "0123456789")]
    public void Base32CodecDecodeShouldProduceExpectedResultPaddedBase32HexUppercase(string value, string expected)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base32;

        // When
        byte[] bytes = codec.Decode(value, Base32FormatProvider.PaddedBase32HexUppercase);
        string actual = Encoding.UTF8.GetString(bytes);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base32Codec.Encode should produce the expected result (PaddedCrockford)")]
    [InlineData("", "")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "85146H258S3MGJAA9D64TKJFA18N4MTMANB5EP2SB8======")]
    [InlineData("abcdefghijklmnopqrstuvwxyz", "C5H66S35CSKPGTBADDP6TVKFE1RQ4WVMENV7EY3SF8======")]
    [InlineData("0123456789", "60RK4CSM6MV3EE1S")]
    public void Base32CodecEncodeShouldProduceExpectedResultPaddedCrockford(string value, string expected)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base32;
        byte[] bytes = value.ToByteArray();

        // When
        string actual = codec.Encode(bytes, Base32FormatProvider.PaddedCrockford);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base32Codec.Decode should produce the expected result (PaddedCrockford)")]
    [InlineData("", "")]
    [InlineData("85146H258S3MGJAA9D64TKJFA18N4MTMANB5EP2SB8======", "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    [InlineData("C5H66S35CSKPGTBADDP6TVKFE1RQ4WVMENV7EY3SF8======", "abcdefghijklmnopqrstuvwxyz")]
    [InlineData("60RK4CSM6MV3EE1S", "0123456789")]
    public void Base32CodecDecodeShouldProduceExpectedResultPaddedCrockford(string value, string expected)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base32;

        // When
        byte[] bytes = codec.Decode(value, Base32FormatProvider.PaddedCrockford);
        string actual = Encoding.UTF8.GetString(bytes);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base32Codec.Encode should produce the expected result (PaddedGeoHash)")]
    [InlineData("", "")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "85146j258t3nhkbb9e64umkgb18p4nunbpc5fq2tc8======")]
    [InlineData("abcdefghijklmnopqrstuvwxyz", "d5j66t35dtmqhucbeeq6uvmgf1sr4wvnfpv7fy3tg8======")]
    [InlineData("0123456789", "60sm4dtn6nv3ff1t")]
    public void Base32CodecEncodeShouldProduceExpectedResultPaddedGeoHash(string value, string expected)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base32;
        byte[] bytes = value.ToByteArray();

        // When
        string actual = codec.Encode(bytes, Base32FormatProvider.PaddedGeoHash);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base32Codec.Decode should produce the expected result (PaddedGeoHash)")]
    [InlineData("", "")]
    [InlineData("85146j258t3nhkbb9e64umkgb18p4nunbpc5fq2tc8======", "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    [InlineData("d5j66t35dtmqhucbeeq6uvmgf1sr4wvnfpv7fy3tg8======", "abcdefghijklmnopqrstuvwxyz")]
    [InlineData("60sm4dtn6nv3ff1t", "0123456789")]
    public void Base32CodecDecodeShouldProduceExpectedResultPaddedGeoHash(string value, string expected)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base32;

        // When
        byte[] bytes = codec.Decode(value, Base32FormatProvider.PaddedGeoHash);
        string actual = Encoding.UTF8.GetString(bytes);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base32Codec.Encode should produce the expected result (PaddedZBase32)")]
    [InlineData("", "")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "efbrgtnfe3dwo1kkjpgr4u1xkbeirw4wkimfqsn3me======")]
    [InlineData("abcdefghijklmnopqrstuvwxyz", "cftgg3dfc3uso4mkppsg45uxqbazrh5wqi58q6d3xe======")]
    [InlineData("0123456789", "gyaurc3wgw5dqqb3")]
    public void Base32CodecEncodeShouldProduceExpectedResultPaddedZBase32(string value, string expected)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base32;
        byte[] bytes = value.ToByteArray();

        // When
        string actual = codec.Encode(bytes, Base32FormatProvider.PaddedZBase32);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base32Codec.Decode should produce the expected result (PaddedZBase32)")]
    [InlineData("", "")]
    [InlineData("efbrgtnfe3dwo1kkjpgr4u1xkbeirw4wkimfqsn3me======", "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    [InlineData("cftgg3dfc3uso4mkppsg45uxqbazrh5wqi58q6d3xe======", "abcdefghijklmnopqrstuvwxyz")]
    [InlineData("gyaurc3wgw5dqqb3", "0123456789")]
    public void Base32CodecDecodeShouldProduceExpectedResultPaddedZBase32(string value, string expected)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base32;

        // When
        byte[] bytes = codec.Decode(value, Base32FormatProvider.PaddedZBase32);
        string actual = Encoding.UTF8.GetString(bytes);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Base32Codec.Encode should throw FormatException when the format provider is invalid")]
    public void Base32CodecEncodeShouldThrowFormatExceptionWhenFormatProviderIsInvalid()
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base32;
        byte[] bytes = "Hello, World!".ToByteArray();

        // When
        Exception exception = Assert.Throws<FormatException>(() => codec.Encode(bytes, InvalidFormatProvider.Instance));

        // Then
        Assert.Equal("Encoding operation failed due to an invalid value or format provider.", exception.Message);
    }

    [Fact(DisplayName = "Base32Codec.Decode should throw FormatException when the format provider is invalid")]
    public void Base32CodecDecodeShouldThrowFormatExceptionWhenTheFormatProviderIsInvalid()
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base32;
        const string value = "IFBEGRCFIZDUQSKKJNGE2TSPKBIVEU2UKVLFOWCZLI";

        // When
        Exception exception = Assert.Throws<FormatException>(() => codec.Decode(value, InvalidFormatProvider.Instance));

        // Then
        Assert.Equal("Decoding operation failed due to an invalid value or format provider.", exception.Message);
    }

    [Fact(DisplayName = "Base32Codec.Decode should throw FormatException when the value is invalid")]
    public void Base32CodecDecodeShouldThrowFormatExceptionWhenTheValueIsInvalid()
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base32;
        const string value = "*INVALID_VALUE*";

        // When
        Exception exception = Assert.Throws<FormatException>(() => codec.Decode(value));

        // Then
        Assert.Equal("Decoding operation failed due to an invalid value or format provider.", exception.Message);
    }

    [Fact(DisplayName = "Base32Codec.TryEncode should return false when the format provider is invalid")]
    public void Base32CodecTryEncodeShouldReturnFalseWhenValueIsInvalid()
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base32;
        byte[] bytes = "Hello, World!".ToByteArray();

        // When
        bool result = codec.TryEncode(bytes, InvalidFormatProvider.Instance, out string _);

        // Then
        Assert.False(result);
    }

    [Fact(DisplayName = "Base32Codec.TryEncode should return false when the value is invalid")]
    public void Base32CodecTryEncodeShouldReturnFalseWhenTheValueIsInvalid()
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base32;
        byte[] bytes = new byte[int.MaxValue / 4];

        // When
        bool result = codec.TryEncode(bytes, Base32FormatProvider.UnpaddedRfc4648Uppercase, out string _);

        // Then
        Assert.False(result);
    }

    [Fact(DisplayName = "Base32Codec.TryDecode should return false when trying to decode a non-padded value with a padded format provider")]
    public void Base32CodecTryDecodeShouldReturnFalseWhenTryingToDecodeANonPaddedValueWithAPaddedFormatProvider()
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base32;

        // When
        bool result = codec.TryDecode("IFBEGRCFIZDUQSKKJNGE2TSPKBIVEU2UKVLFOWCZLI", Base32FormatProvider.PaddedRfc4648Uppercase, out byte[] _);

        // Then
        Assert.False(result);
    }

    [Fact(DisplayName = "Base32Codec.TryDecode should return false when trying to decode a sequence of padding charachers")]
    public void Base32CodecTryDecodeShouldReturnFalseWhenTryingToDecodeASequenceOfPaddingCharacters()
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base32;

        // When
        bool result = codec.TryDecode("========", Base32FormatProvider.PaddedRfc4648Uppercase, out byte[] _);

        // Then
        Assert.False(result);
    }

    [Fact(DisplayName = "Base32Codec.TryDecode should return false when the value is invalid")]
    public void Base32CodecTryDecodeShouldReturnFalseWhenTheValueIsInvalid()
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base32;
        string value = new('A', int.MaxValue / 4);

        // When
        bool result = codec.TryDecode(value, Base32FormatProvider.UnpaddedRfc4648Uppercase, out byte[] _);

        // Then
        Assert.False(result);
    }

    [Theory(DisplayName = "Base32Codec should produce the RFC 4648 §10 test vectors for each format provider")]
    [InlineData("PaddedRfc4648Uppercase", "", "")]
    [InlineData("PaddedRfc4648Uppercase", "66", "MY======")]
    [InlineData("PaddedRfc4648Uppercase", "666F", "MZXQ====")]
    [InlineData("PaddedRfc4648Uppercase", "666F6F", "MZXW6===")]
    [InlineData("PaddedRfc4648Uppercase", "666F6F62", "MZXW6YQ=")]
    [InlineData("PaddedRfc4648Uppercase", "666F6F6261", "MZXW6YTB")]
    [InlineData("PaddedRfc4648Uppercase", "666F6F626172", "MZXW6YTBOI======")]
    [InlineData("UnpaddedRfc4648Uppercase", "", "")]
    [InlineData("UnpaddedRfc4648Uppercase", "66", "MY")]
    [InlineData("UnpaddedRfc4648Uppercase", "666F", "MZXQ")]
    [InlineData("UnpaddedRfc4648Uppercase", "666F6F", "MZXW6")]
    [InlineData("UnpaddedRfc4648Uppercase", "666F6F62", "MZXW6YQ")]
    [InlineData("UnpaddedRfc4648Uppercase", "666F6F6261", "MZXW6YTB")]
    [InlineData("UnpaddedRfc4648Uppercase", "666F6F626172", "MZXW6YTBOI")]
    [InlineData("PaddedBase32HexUppercase", "", "")]
    [InlineData("PaddedBase32HexUppercase", "66", "CO======")]
    [InlineData("PaddedBase32HexUppercase", "666F", "CPNG====")]
    [InlineData("PaddedBase32HexUppercase", "666F6F", "CPNMU===")]
    [InlineData("PaddedBase32HexUppercase", "666F6F62", "CPNMUOG=")]
    [InlineData("PaddedBase32HexUppercase", "666F6F6261", "CPNMUOJ1")]
    [InlineData("PaddedBase32HexUppercase", "666F6F626172", "CPNMUOJ1E8======")]
    [InlineData("UnpaddedBase32HexUppercase", "", "")]
    [InlineData("UnpaddedBase32HexUppercase", "66", "CO")]
    [InlineData("UnpaddedBase32HexUppercase", "666F", "CPNG")]
    [InlineData("UnpaddedBase32HexUppercase", "666F6F", "CPNMU")]
    [InlineData("UnpaddedBase32HexUppercase", "666F6F62", "CPNMUOG")]
    [InlineData("UnpaddedBase32HexUppercase", "666F6F6261", "CPNMUOJ1")]
    [InlineData("UnpaddedBase32HexUppercase", "666F6F626172", "CPNMUOJ1E8")]
    [InlineData("PaddedRfc4648Lowercase", "", "")]
    [InlineData("PaddedRfc4648Lowercase", "66", "my======")]
    [InlineData("PaddedRfc4648Lowercase", "666F", "mzxq====")]
    [InlineData("PaddedRfc4648Lowercase", "666F6F", "mzxw6===")]
    [InlineData("PaddedRfc4648Lowercase", "666F6F62", "mzxw6yq=")]
    [InlineData("PaddedRfc4648Lowercase", "666F6F6261", "mzxw6ytb")]
    [InlineData("PaddedRfc4648Lowercase", "666F6F626172", "mzxw6ytboi======")]
    [InlineData("UnpaddedRfc4648Lowercase", "", "")]
    [InlineData("UnpaddedRfc4648Lowercase", "66", "my")]
    [InlineData("UnpaddedRfc4648Lowercase", "666F", "mzxq")]
    [InlineData("UnpaddedRfc4648Lowercase", "666F6F", "mzxw6")]
    [InlineData("UnpaddedRfc4648Lowercase", "666F6F62", "mzxw6yq")]
    [InlineData("UnpaddedRfc4648Lowercase", "666F6F6261", "mzxw6ytb")]
    [InlineData("UnpaddedRfc4648Lowercase", "666F6F626172", "mzxw6ytboi")]
    [InlineData("PaddedRfc4648Invariant", "", "")]
    [InlineData("PaddedRfc4648Invariant", "66", "MY======")]
    [InlineData("PaddedRfc4648Invariant", "666F", "MZXQ====")]
    [InlineData("PaddedRfc4648Invariant", "666F6F", "MZXW6===")]
    [InlineData("PaddedRfc4648Invariant", "666F6F62", "MZXW6YQ=")]
    [InlineData("PaddedRfc4648Invariant", "666F6F6261", "MZXW6YTB")]
    [InlineData("PaddedRfc4648Invariant", "666F6F626172", "MZXW6YTBOI======")]
    [InlineData("UnpaddedRfc4648Invariant", "", "")]
    [InlineData("UnpaddedRfc4648Invariant", "66", "MY")]
    [InlineData("UnpaddedRfc4648Invariant", "666F", "MZXQ")]
    [InlineData("UnpaddedRfc4648Invariant", "666F6F", "MZXW6")]
    [InlineData("UnpaddedRfc4648Invariant", "666F6F62", "MZXW6YQ")]
    [InlineData("UnpaddedRfc4648Invariant", "666F6F6261", "MZXW6YTB")]
    [InlineData("UnpaddedRfc4648Invariant", "666F6F626172", "MZXW6YTBOI")]
    [InlineData("PaddedBase32HexLowercase", "", "")]
    [InlineData("PaddedBase32HexLowercase", "66", "co======")]
    [InlineData("PaddedBase32HexLowercase", "666F", "cpng====")]
    [InlineData("PaddedBase32HexLowercase", "666F6F", "cpnmu===")]
    [InlineData("PaddedBase32HexLowercase", "666F6F62", "cpnmuog=")]
    [InlineData("PaddedBase32HexLowercase", "666F6F6261", "cpnmuoj1")]
    [InlineData("PaddedBase32HexLowercase", "666F6F626172", "cpnmuoj1e8======")]
    [InlineData("UnpaddedBase32HexLowercase", "", "")]
    [InlineData("UnpaddedBase32HexLowercase", "66", "co")]
    [InlineData("UnpaddedBase32HexLowercase", "666F", "cpng")]
    [InlineData("UnpaddedBase32HexLowercase", "666F6F", "cpnmu")]
    [InlineData("UnpaddedBase32HexLowercase", "666F6F62", "cpnmuog")]
    [InlineData("UnpaddedBase32HexLowercase", "666F6F6261", "cpnmuoj1")]
    [InlineData("UnpaddedBase32HexLowercase", "666F6F626172", "cpnmuoj1e8")]
    [InlineData("PaddedBase32HexInvariant", "", "")]
    [InlineData("PaddedBase32HexInvariant", "66", "CO======")]
    [InlineData("PaddedBase32HexInvariant", "666F", "CPNG====")]
    [InlineData("PaddedBase32HexInvariant", "666F6F", "CPNMU===")]
    [InlineData("PaddedBase32HexInvariant", "666F6F62", "CPNMUOG=")]
    [InlineData("PaddedBase32HexInvariant", "666F6F6261", "CPNMUOJ1")]
    [InlineData("PaddedBase32HexInvariant", "666F6F626172", "CPNMUOJ1E8======")]
    [InlineData("UnpaddedBase32HexInvariant", "", "")]
    [InlineData("UnpaddedBase32HexInvariant", "66", "CO")]
    [InlineData("UnpaddedBase32HexInvariant", "666F", "CPNG")]
    [InlineData("UnpaddedBase32HexInvariant", "666F6F", "CPNMU")]
    [InlineData("UnpaddedBase32HexInvariant", "666F6F62", "CPNMUOG")]
    [InlineData("UnpaddedBase32HexInvariant", "666F6F6261", "CPNMUOJ1")]
    [InlineData("UnpaddedBase32HexInvariant", "666F6F626172", "CPNMUOJ1E8")]
    public void Base32CodecShouldProduceRfc4648TestVectorsForEachFormatProvider(string providerName, string bytes, string encoded)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base32;
        Base32FormatProvider provider = Base32FormatProvider.FromName(providerName);

        // When
        string actualEncoded = codec.Encode(Convert.FromHexString(bytes), provider);
        string actualBytes = Convert.ToHexString(codec.Decode(encoded, provider));

        // Then
        Assert.Equal(encoded, actualEncoded);
        Assert.Equal(bytes, actualBytes);
    }

    [Theory(DisplayName = "Base32Codec.TryDecode should reject non-canonical values for each format provider")]
    [InlineData("UnpaddedRfc4648Uppercase", "MZ")]
    [InlineData("UnpaddedRfc4648Uppercase", "MZXR")]
    [InlineData("UnpaddedRfc4648Uppercase", "MZXW7")]
    [InlineData("UnpaddedRfc4648Uppercase", "M")]
    [InlineData("UnpaddedRfc4648Uppercase", "MYA")]
    [InlineData("UnpaddedRfc4648Uppercase", "MZXW6Y")]
    [InlineData("UnpaddedRfc4648Uppercase", "MY======")]
    [InlineData("UnpaddedRfc4648Uppercase", "MY==")]
    [InlineData("UnpaddedRfc4648Uppercase", "my")]
    [InlineData("UnpaddedRfc4648Uppercase", "MZXW 6YTB")]
    [InlineData("UnpaddedRfc4648Uppercase", "MZXW-6YTB")]
    [InlineData("PaddedRfc4648Uppercase", "MY")]
    [InlineData("PaddedRfc4648Uppercase", "MY=====")]
    [InlineData("PaddedRfc4648Uppercase", "MZ======")]
    [InlineData("PaddedRfc4648Uppercase", "M=======")]
    [InlineData("PaddedRfc4648Uppercase", "MYA=====")]
    [InlineData("PaddedRfc4648Uppercase", "MZXW6Y==")]
    [InlineData("PaddedRfc4648Uppercase", "MY==============")]
    [InlineData("PaddedRfc4648Uppercase", "MY=A====")]
    [InlineData("PaddedRfc4648Uppercase", "MY======MY======")]
    [InlineData("UnpaddedBase32HexUppercase", "CP")]
    [InlineData("UnpaddedBase32HexUppercase", "COA")]
    [InlineData("PaddedBase32HexUppercase", "CP======")]
    [InlineData("UnpaddedRfc4648Uppercase", "mzxw6ytb")]
    [InlineData("UnpaddedRfc4648Lowercase", "MZXW6YTB")]
    [InlineData("UnpaddedRfc4648Lowercase", "mz")]
    [InlineData("PaddedRfc4648Lowercase", "MY======")]
    [InlineData("UnpaddedRfc4648Invariant", "mZXW6YTB")]
    [InlineData("UnpaddedRfc4648Invariant", "MZXW6YTb")]
    [InlineData("UnpaddedRfc4648Invariant", "mz")]
    [InlineData("PaddedRfc4648Invariant", "mY======")]
    [InlineData("UnpaddedBase32HexUppercase", "cpnmuoj1")]
    [InlineData("UnpaddedBase32HexLowercase", "CPNMUOJ1")]
    [InlineData("UnpaddedBase32HexInvariant", "cPNMUOJ1")]
    [InlineData("PaddedBase32HexInvariant", "cp======")]
    [InlineData("UnpaddedZBase32", "c3")]
    [InlineData("UnpaddedGeoHash", "dt")]
    [InlineData("UnpaddedCrockford", "CS")]
    [InlineData("UnpaddedCrockford", "CU")]
    [InlineData("UnpaddedCrockford", "C=")]
    [InlineData("PaddedCrockford", "CS======")]
    [InlineData("PaddedCrockford", "CR")]
    public void Base32CodecTryDecodeShouldRejectNonCanonicalValuesForEachFormatProvider(string providerName, string value)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base32;
        Base32FormatProvider provider = Base32FormatProvider.FromName(providerName);

        // When
        bool success = codec.TryDecode(value, provider, out byte[] _);

        // Then
        Assert.False(success);
    }

    [Theory(DisplayName = "Base32Codec should only accept values that re-encode to themselves for each format provider")]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void Base32CodecShouldOnlyAcceptValuesThatReEncodeToThemselves(int seed)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base32;
        Random random = new(seed);

        foreach (Base32FormatProvider provider in Base32FormatProvider.GetAll())
        {
            for (int length = 1; length <= 64; length++)
            {
                byte[] bytes = new byte[length];
                random.NextBytes(bytes);
                string encoded = codec.Encode(bytes, provider);
                int lastIndex = encoded.TrimEnd('=').Length - 1;

                // When
                byte[] decoded = codec.Decode(encoded, provider);

                // Then
                Assert.Equal(bytes, decoded);

                foreach (char character in provider.Alphabet)
                {
                    string candidate = string.Concat(encoded.AsSpan(0, lastIndex), [character], encoded.AsSpan(lastIndex + 1));
                    if (codec.TryDecode(candidate, provider, out byte[] candidateBytes)) Assert.Equal(candidate, codec.Encode(candidateBytes, provider));
                }
            }
        }
    }

    [Theory(DisplayName = "Base32Codec.Decode should apply the Crockford decoding rules")]
    [InlineData("UnpaddedCrockford", "60rk4csm6mv3ee1s", "0123456789")]
    [InlineData("UnpaddedCrockford", "6ORK4CSM6MV3EE1S", "0123456789")]
    [InlineData("UnpaddedCrockford", "6oRK4CSM6MV3EE1S", "0123456789")]
    [InlineData("UnpaddedCrockford", "60RK4CSM6MV3EEIS", "0123456789")]
    [InlineData("UnpaddedCrockford", "60RK4CSM6MV3EEiS", "0123456789")]
    [InlineData("UnpaddedCrockford", "60RK4CSM6MV3EELS", "0123456789")]
    [InlineData("UnpaddedCrockford", "60RK4CSM6MV3EElS", "0123456789")]
    [InlineData("UnpaddedCrockford", "60RK-4CSM-6MV3-EE1S", "0123456789")]
    [InlineData("PaddedCrockford", "60rk-4csm-6mv3-ee1s", "0123456789")]
    [InlineData("PaddedCrockford", "85146h258s3mgjaa9d64tkjfa18n4mtmanb5ep2sb8======", "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    [InlineData("PaddedCrockford", "85146-H258S-3MGJA-A9D64-TKJFA-18N4M-TMANB-5EP2S-B8======", "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    public void Base32CodecDecodeShouldApplyTheCrockfordDecodingRules(string providerName, string value, string expected)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base32;
        Base32FormatProvider provider = Base32FormatProvider.FromName(providerName);

        // When
        byte[] bytes = codec.Decode(value, provider);
        string actual = Encoding.UTF8.GetString(bytes);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base32Codec.Decode should accept either case for the invariant format providers")]
    [InlineData("UnpaddedRfc4648Invariant", "MZXW6YTBOI", "666F6F626172")]
    [InlineData("UnpaddedRfc4648Invariant", "mzxw6ytboi", "666F6F626172")]
    [InlineData("PaddedRfc4648Invariant", "MZXW6YTBOI======", "666F6F626172")]
    [InlineData("PaddedRfc4648Invariant", "mzxw6ytboi======", "666F6F626172")]
    [InlineData("UnpaddedBase32HexInvariant", "CPNMUOJ1E8", "666F6F626172")]
    [InlineData("UnpaddedBase32HexInvariant", "cpnmuoj1e8", "666F6F626172")]
    [InlineData("PaddedBase32HexInvariant", "CPNMUOJ1E8======", "666F6F626172")]
    [InlineData("PaddedBase32HexInvariant", "cpnmuoj1e8======", "666F6F626172")]
    public void Base32CodecDecodeShouldAcceptEitherCaseForTheInvariantFormatProviders(string providerName, string value, string expected)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base32;
        Base32FormatProvider provider = Base32FormatProvider.FromName(providerName);

        // When
        string actual = Convert.ToHexString(codec.Decode(value, provider));

        // Then
        Assert.Equal(expected, actual);
    }
}
