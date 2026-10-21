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
    [Theory(DisplayName = "Base32Codec.Encode should produce the expected result (Rfc4648)")]
    [InlineData("", "")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "IFBEGRCFIZDUQSKKJNGE2TSPKBIVEU2UKVLFOWCZLI")]
    [InlineData("abcdefghijklmnopqrstuvwxyz", "MFRGGZDFMZTWQ2LKNNWG23TPOBYXE43UOV3HO6DZPI")]
    [InlineData("0123456789", "GAYTEMZUGU3DOOBZ")]
    public void Base32CodecEncodeShouldProduceExpectedResultRfc4648(string value, string expected)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base32;
        byte[] bytes = value.ToByteArray();

        // When
        string actual = codec.Encode(bytes);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base32Codec.Decode should produce the expected result (Rfc4648)")]
    [InlineData("", "")]
    [InlineData("IFBEGRCFIZDUQSKKJNGE2TSPKBIVEU2UKVLFOWCZLI", "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    [InlineData("MFRGGZDFMZTWQ2LKNNWG23TPOBYXE43UOV3HO6DZPI", "abcdefghijklmnopqrstuvwxyz")]
    [InlineData("GAYTEMZUGU3DOOBZ", "0123456789")]
    public void Base32CodecDecodeShouldProduceExpectedResultRfc4648(string value, string expected)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base32;

        // When
        byte[] bytes = codec.Decode(value);
        string actual = Encoding.UTF8.GetString(bytes);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base32Codec.Encode should produce the expected result (Base32Hex)")]
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
        string actual = codec.Encode(bytes, Base32FormatProvider.Base32Hex);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base32Codec.Decode should produce the expected result (Base32Hex)")]
    [InlineData("", "")]
    [InlineData("85146H258P3KGIAA9D64QJIFA18L4KQKALB5EM2PB8", "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    [InlineData("C5H66P35CPJMGQBADDM6QRJFE1ON4SRKELR7EU3PF8", "abcdefghijklmnopqrstuvwxyz")]
    [InlineData("60OJ4CPK6KR3EE1P", "0123456789")]
    public void Base32CodecDecodeShouldProduceExpectedResultBase32Hex(string value, string expected)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base32;

        // When
        byte[] bytes = codec.Decode(value, Base32FormatProvider.Base32Hex);
        string actual = Encoding.UTF8.GetString(bytes);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base32Codec.Encode should produce the expected result (Crockford)")]
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
        string actual = codec.Encode(bytes, Base32FormatProvider.Crockford);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base32Codec.Decode should produce the expected result (Crockford)")]
    [InlineData("", "")]
    [InlineData("85146H258S3MGJAA9D64TKJFA18N4MTMANB5EP2SB8", "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    [InlineData("C5H66S35CSKPGTBADDP6TVKFE1RQ4WVMENV7EY3SF8", "abcdefghijklmnopqrstuvwxyz")]
    [InlineData("60RK4CSM6MV3EE1S", "0123456789")]
    public void Base32CodecDecodeShouldProduceExpectedResultCrockford(string value, string expected)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base32;

        // When
        byte[] bytes = codec.Decode(value, Base32FormatProvider.Crockford);
        string actual = Encoding.UTF8.GetString(bytes);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base32Codec.Encode should produce the expected result (GeoHash)")]
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
        string actual = codec.Encode(bytes, Base32FormatProvider.GeoHash);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base32Codec.Decode should produce the expected result (GeoHash)")]
    [InlineData("", "")]
    [InlineData("85146j258t3nhkbb9e64umkgb18p4nunbpc5fq2tc8", "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    [InlineData("d5j66t35dtmqhucbeeq6uvmgf1sr4wvnfpv7fy3tg8", "abcdefghijklmnopqrstuvwxyz")]
    [InlineData("60sm4dtn6nv3ff1t", "0123456789")]
    public void Base32CodecDecodeShouldProduceExpectedResultGeoHash(string value, string expected)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base32;

        // When
        byte[] bytes = codec.Decode(value, Base32FormatProvider.GeoHash);
        string actual = Encoding.UTF8.GetString(bytes);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base32Codec.Encode should produce the expected result (ZBase32)")]
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
        string actual = codec.Encode(bytes, Base32FormatProvider.ZBase32);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base32Codec.Decode should produce the expected result (ZBase32)")]
    [InlineData("", "")]
    [InlineData("efbrgtnfe3dwo1kkjpgr4u1xkbeirw4wkimfqsn3me", "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    [InlineData("cftgg3dfc3uso4mkppsg45uxqbazrh5wqi58q6d3xe", "abcdefghijklmnopqrstuvwxyz")]
    [InlineData("gyaurc3wgw5dqqb3", "0123456789")]
    public void Base32CodecDecodeShouldProduceExpectedResultZBase32(string value, string expected)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base32;

        // When
        byte[] bytes = codec.Decode(value, Base32FormatProvider.ZBase32);
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
        bool result = codec.TryEncode(bytes, Base32FormatProvider.Rfc4648, out string _);

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
        bool result = codec.TryDecode(value, Base32FormatProvider.Rfc4648, out byte[] _);

        // Then
        Assert.False(result);
    }
}
