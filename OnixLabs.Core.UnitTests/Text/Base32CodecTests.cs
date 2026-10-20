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
    [Theory(DisplayName = "Base32Codec.Encode should produce the expected result (UnpaddedRfc4648)")]
    [InlineData("", "")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "IFBEGRCFIZDUQSKKJNGE2TSPKBIVEU2UKVLFOWCZLI")]
    [InlineData("abcdefghijklmnopqrstuvwxyz", "MFRGGZDFMZTWQ2LKNNWG23TPOBYXE43UOV3HO6DZPI")]
    [InlineData("0123456789", "GAYTEMZUGU3DOOBZ")]
    public void Base32CodecEncodeShouldProduceExpectedResultUnpaddedRfc4648(string value, string expected)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base32;
        byte[] bytes = value.ToByteArray();

        // When
        string actual = codec.Encode(bytes);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base32Codec.Decode should produce the expected result (UnpaddedRfc4648)")]
    [InlineData("", "")]
    [InlineData("IFBEGRCFIZDUQSKKJNGE2TSPKBIVEU2UKVLFOWCZLI", "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    [InlineData("MFRGGZDFMZTWQ2LKNNWG23TPOBYXE43UOV3HO6DZPI", "abcdefghijklmnopqrstuvwxyz")]
    [InlineData("GAYTEMZUGU3DOOBZ", "0123456789")]
    public void Base32CodecDecodeShouldProduceExpectedResultUnpaddedRfc4648(string value, string expected)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base32;

        // When
        byte[] bytes = codec.Decode(value);
        string actual = Encoding.UTF8.GetString(bytes);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base32Codec.Encode should produce the expected result (UnpaddedBase32Hex)")]
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
        string actual = codec.Encode(bytes, Base32FormatProvider.UnpaddedBase32Hex);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base32Codec.Decode should produce the expected result (UnpaddedBase32Hex)")]
    [InlineData("", "")]
    [InlineData("85146H258P3KGIAA9D64QJIFA18L4KQKALB5EM2PB8", "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    [InlineData("C5H66P35CPJMGQBADDM6QRJFE1ON4SRKELR7EU3PF8", "abcdefghijklmnopqrstuvwxyz")]
    [InlineData("60OJ4CPK6KR3EE1P", "0123456789")]
    public void Base32CodecDecodeShouldProduceExpectedResultBase32Hex(string value, string expected)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base32;

        // When
        byte[] bytes = codec.Decode(value, Base32FormatProvider.UnpaddedBase32Hex);
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

    [Theory(DisplayName = "Base32Codec.Encode should produce the expected result (PaddedRfc4648)")]
    [InlineData("", "")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "IFBEGRCFIZDUQSKKJNGE2TSPKBIVEU2UKVLFOWCZLI======")]
    [InlineData("abcdefghijklmnopqrstuvwxyz", "MFRGGZDFMZTWQ2LKNNWG23TPOBYXE43UOV3HO6DZPI======")]
    [InlineData("0123456789", "GAYTEMZUGU3DOOBZ")]
    public void Base32CodecEncodeShouldProduceExpectedResultPaddedRfc4648(string value, string expected)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base32;
        byte[] bytes = value.ToByteArray();

        // When
        string actual = codec.Encode(bytes, Base32FormatProvider.PaddedRfc4648);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base32Codec.Decode should produce the expected result (PaddedRfc4648)")]
    [InlineData("", "")]
    [InlineData("IFBEGRCFIZDUQSKKJNGE2TSPKBIVEU2UKVLFOWCZLI======", "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    [InlineData("MFRGGZDFMZTWQ2LKNNWG23TPOBYXE43UOV3HO6DZPI======", "abcdefghijklmnopqrstuvwxyz")]
    [InlineData("GAYTEMZUGU3DOOBZ", "0123456789")]
    public void Base32CodecDecodeShouldProduceExpectedResultPaddedRfc4648(string value, string expected)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base32;

        // When
        byte[] bytes = codec.Decode(value, Base32FormatProvider.PaddedRfc4648);
        string actual = Encoding.UTF8.GetString(bytes);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base32Codec.Encode should produce the expected result (PaddedBase32Hex)")]
    [InlineData("", "")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "85146H258P3KGIAA9D64QJIFA18L4KQKALB5EM2PB8======")]
    [InlineData("abcdefghijklmnopqrstuvwxyz", "C5H66P35CPJMGQBADDM6QRJFE1ON4SRKELR7EU3PF8======")]
    [InlineData("0123456789", "60OJ4CPK6KR3EE1P")]
    public void Base32CodecEncodeShouldProduceExpectedResultPaddedBase32Hex(string value, string expected)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base32;
        byte[] bytes = value.ToByteArray();

        // When
        string actual = codec.Encode(bytes, Base32FormatProvider.PaddedBase32Hex);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base32Codec.Decode should produce the expected result (PaddedBase32Hex)")]
    [InlineData("", "")]
    [InlineData("85146H258P3KGIAA9D64QJIFA18L4KQKALB5EM2PB8======", "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    [InlineData("C5H66P35CPJMGQBADDM6QRJFE1ON4SRKELR7EU3PF8======", "abcdefghijklmnopqrstuvwxyz")]
    [InlineData("60OJ4CPK6KR3EE1P", "0123456789")]
    public void Base32CodecDecodeShouldProduceExpectedResultPaddedBase32Hex(string value, string expected)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base32;

        // When
        byte[] bytes = codec.Decode(value, Base32FormatProvider.PaddedBase32Hex);
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
        bool result = codec.TryEncode(bytes, Base32FormatProvider.UnpaddedRfc4648, out string _);

        // Then
        Assert.False(result);
    }

    [Fact(DisplayName = "Base32Codec.TryDecode should return false when trying to decode a non-padded value with a padded format provider")]
    public void Base32CodecTryDecodeShouldReturnFalseWhenTryingToDecodeANonPaddedValueWithAPaddedFormatProvider()
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base32;

        // When
        bool result = codec.TryDecode("IFBEGRCFIZDUQSKKJNGE2TSPKBIVEU2UKVLFOWCZLI", Base32FormatProvider.PaddedRfc4648, out byte[] _);

        // Then
        Assert.False(result);
    }

    [Fact(DisplayName = "Base32Codec.TryDecode should return false when trying to decode a sequence of padding charachers")]
    public void Base32CodecTryDecodeShouldReturnFalseWhenTryingToDecodeASequenceOfPaddingCharacters()
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base32;

        // When
        bool result = codec.TryDecode("========", Base32FormatProvider.PaddedRfc4648, out byte[] _);

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
        bool result = codec.TryDecode(value, Base32FormatProvider.UnpaddedRfc4648, out byte[] _);

        // Then
        Assert.False(result);
    }

    [Theory(DisplayName = "Base32Codec should produce the RFC 4648 §10 test vectors for each format provider")]
    [InlineData("PaddedRfc4648", "", "")]
    [InlineData("PaddedRfc4648", "66", "MY======")]
    [InlineData("PaddedRfc4648", "666F", "MZXQ====")]
    [InlineData("PaddedRfc4648", "666F6F", "MZXW6===")]
    [InlineData("PaddedRfc4648", "666F6F62", "MZXW6YQ=")]
    [InlineData("PaddedRfc4648", "666F6F6261", "MZXW6YTB")]
    [InlineData("PaddedRfc4648", "666F6F626172", "MZXW6YTBOI======")]
    [InlineData("UnpaddedRfc4648", "", "")]
    [InlineData("UnpaddedRfc4648", "66", "MY")]
    [InlineData("UnpaddedRfc4648", "666F", "MZXQ")]
    [InlineData("UnpaddedRfc4648", "666F6F", "MZXW6")]
    [InlineData("UnpaddedRfc4648", "666F6F62", "MZXW6YQ")]
    [InlineData("UnpaddedRfc4648", "666F6F6261", "MZXW6YTB")]
    [InlineData("UnpaddedRfc4648", "666F6F626172", "MZXW6YTBOI")]
    [InlineData("PaddedBase32Hex", "", "")]
    [InlineData("PaddedBase32Hex", "66", "CO======")]
    [InlineData("PaddedBase32Hex", "666F", "CPNG====")]
    [InlineData("PaddedBase32Hex", "666F6F", "CPNMU===")]
    [InlineData("PaddedBase32Hex", "666F6F62", "CPNMUOG=")]
    [InlineData("PaddedBase32Hex", "666F6F6261", "CPNMUOJ1")]
    [InlineData("PaddedBase32Hex", "666F6F626172", "CPNMUOJ1E8======")]
    [InlineData("UnpaddedBase32Hex", "", "")]
    [InlineData("UnpaddedBase32Hex", "66", "CO")]
    [InlineData("UnpaddedBase32Hex", "666F", "CPNG")]
    [InlineData("UnpaddedBase32Hex", "666F6F", "CPNMU")]
    [InlineData("UnpaddedBase32Hex", "666F6F62", "CPNMUOG")]
    [InlineData("UnpaddedBase32Hex", "666F6F6261", "CPNMUOJ1")]
    [InlineData("UnpaddedBase32Hex", "666F6F626172", "CPNMUOJ1E8")]
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
    [InlineData("UnpaddedRfc4648", "MZ")]
    [InlineData("UnpaddedRfc4648", "MZXR")]
    [InlineData("UnpaddedRfc4648", "MZXW7")]
    [InlineData("UnpaddedRfc4648", "M")]
    [InlineData("UnpaddedRfc4648", "MYA")]
    [InlineData("UnpaddedRfc4648", "MZXW6Y")]
    [InlineData("UnpaddedRfc4648", "MY======")]
    [InlineData("UnpaddedRfc4648", "MY==")]
    [InlineData("UnpaddedRfc4648", "my")]
    [InlineData("UnpaddedRfc4648", "MZXW 6YTB")]
    [InlineData("UnpaddedRfc4648", "MZXW-6YTB")]
    [InlineData("PaddedRfc4648", "MY")]
    [InlineData("PaddedRfc4648", "MY=====")]
    [InlineData("PaddedRfc4648", "MZ======")]
    [InlineData("PaddedRfc4648", "M=======")]
    [InlineData("PaddedRfc4648", "MYA=====")]
    [InlineData("PaddedRfc4648", "MZXW6Y==")]
    [InlineData("PaddedRfc4648", "MY==============")]
    [InlineData("PaddedRfc4648", "MY=A====")]
    [InlineData("PaddedRfc4648", "MY======MY======")]
    [InlineData("UnpaddedBase32Hex", "CP")]
    [InlineData("UnpaddedBase32Hex", "COA")]
    [InlineData("PaddedBase32Hex", "CP======")]
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
}
