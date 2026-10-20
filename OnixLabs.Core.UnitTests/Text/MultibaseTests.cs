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
using System.Collections.Generic;
using OnixLabs.Core.Text;

namespace OnixLabs.Core.UnitTests.Text;

public sealed class MultibaseTests
{
    // Why: the official vectors name encodings by their Multibase specification names; this maps each to its MultibaseEncoding member.
    private static readonly Dictionary<string, MultibaseEncoding> Encodings = new()
    {
        ["base58btc"] = MultibaseEncoding.Base58Bitcoin,
        ["base16"] = MultibaseEncoding.Base16Lowercase,
        ["base16upper"] = MultibaseEncoding.Base16Uppercase,
        ["base32"] = MultibaseEncoding.UnpaddedBase32Lowercase,
        ["base32upper"] = MultibaseEncoding.UnpaddedBase32Uppercase,
        ["base64"] = MultibaseEncoding.UnpaddedBase64,
        ["base64url"] = MultibaseEncoding.UnpaddedBase64Url,
        ["base64urlpad"] = MultibaseEncoding.PaddedBase64Url
    };

    [Theory(DisplayName = "Multibase.Encode should produce the official multiformats test vectors")]
    [InlineData("base16", "796573206D616E692021", "f796573206d616e692021")]
    [InlineData("base16upper", "796573206D616E692021", "F796573206D616E692021")]
    [InlineData("base32", "796573206D616E692021", "bpfsxgidnmfxgsibb")]
    [InlineData("base32upper", "796573206D616E692021", "BPFSXGIDNMFXGSIBB")]
    [InlineData("base58btc", "796573206D616E692021", "z7paNL19xttacUY")]
    [InlineData("base64", "796573206D616E692021", "meWVzIG1hbmkgIQ")]
    [InlineData("base64url", "796573206D616E692021", "ueWVzIG1hbmkgIQ")]
    [InlineData("base64urlpad", "796573206D616E692021", "UeWVzIG1hbmkgIQ==")]
    [InlineData("base16", "00796573206D616E692021", "f00796573206d616e692021")]
    [InlineData("base16upper", "00796573206D616E692021", "F00796573206D616E692021")]
    [InlineData("base32", "00796573206D616E692021", "bab4wk4zanvqw42jaee")]
    [InlineData("base32upper", "00796573206D616E692021", "BAB4WK4ZANVQW42JAEE")]
    [InlineData("base58btc", "00796573206D616E692021", "z17paNL19xttacUY")]
    [InlineData("base64", "00796573206D616E692021", "mAHllcyBtYW5pICE")]
    [InlineData("base64url", "00796573206D616E692021", "uAHllcyBtYW5pICE")]
    [InlineData("base64urlpad", "00796573206D616E692021", "UAHllcyBtYW5pICE=")]
    [InlineData("base16", "0000796573206D616E692021", "f0000796573206d616e692021")]
    [InlineData("base16upper", "0000796573206D616E692021", "F0000796573206D616E692021")]
    [InlineData("base32", "0000796573206D616E692021", "baaahszltebwwc3tjeaqq")]
    [InlineData("base32upper", "0000796573206D616E692021", "BAAAHSZLTEBWWC3TJEAQQ")]
    [InlineData("base58btc", "0000796573206D616E692021", "z117paNL19xttacUY")]
    [InlineData("base64", "0000796573206D616E692021", "mAAB5ZXMgbWFuaSAh")]
    [InlineData("base64url", "0000796573206D616E692021", "uAAB5ZXMgbWFuaSAh")]
    [InlineData("base64urlpad", "0000796573206D616E692021", "UAAB5ZXMgbWFuaSAh")]
    public void MultibaseEncodeShouldProduceTheOfficialTestVectors(string encodingName, string value, string expected)
    {
        // Given
        MultibaseEncoding encoding = Encodings[encodingName];
        byte[] bytes = Convert.FromHexString(value);

        // When
        string actual = Multibase.Encode(bytes, encoding);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Multibase.Decode should decode the official multiformats test vectors and report their encoding")]
    [InlineData("base16", "796573206D616E692021", "f796573206d616e692021")]
    [InlineData("base16upper", "796573206D616E692021", "F796573206D616E692021")]
    [InlineData("base32", "796573206D616E692021", "bpfsxgidnmfxgsibb")]
    [InlineData("base32upper", "796573206D616E692021", "BPFSXGIDNMFXGSIBB")]
    [InlineData("base58btc", "796573206D616E692021", "z7paNL19xttacUY")]
    [InlineData("base64", "796573206D616E692021", "meWVzIG1hbmkgIQ")]
    [InlineData("base64url", "796573206D616E692021", "ueWVzIG1hbmkgIQ")]
    [InlineData("base64urlpad", "796573206D616E692021", "UeWVzIG1hbmkgIQ==")]
    [InlineData("base16", "00796573206D616E692021", "f00796573206d616e692021")]
    [InlineData("base16upper", "00796573206D616E692021", "F00796573206D616E692021")]
    [InlineData("base32", "00796573206D616E692021", "bab4wk4zanvqw42jaee")]
    [InlineData("base32upper", "00796573206D616E692021", "BAB4WK4ZANVQW42JAEE")]
    [InlineData("base58btc", "00796573206D616E692021", "z17paNL19xttacUY")]
    [InlineData("base64", "00796573206D616E692021", "mAHllcyBtYW5pICE")]
    [InlineData("base64url", "00796573206D616E692021", "uAHllcyBtYW5pICE")]
    [InlineData("base64urlpad", "00796573206D616E692021", "UAHllcyBtYW5pICE=")]
    [InlineData("base16", "0000796573206D616E692021", "f0000796573206d616e692021")]
    [InlineData("base16upper", "0000796573206D616E692021", "F0000796573206D616E692021")]
    [InlineData("base32", "0000796573206D616E692021", "baaahszltebwwc3tjeaqq")]
    [InlineData("base32upper", "0000796573206D616E692021", "BAAAHSZLTEBWWC3TJEAQQ")]
    [InlineData("base58btc", "0000796573206D616E692021", "z117paNL19xttacUY")]
    [InlineData("base64", "0000796573206D616E692021", "mAAB5ZXMgbWFuaSAh")]
    [InlineData("base64url", "0000796573206D616E692021", "uAAB5ZXMgbWFuaSAh")]
    [InlineData("base64urlpad", "0000796573206D616E692021", "UAAB5ZXMgbWFuaSAh")]
    public void MultibaseDecodeShouldDecodeTheOfficialTestVectors(string encodingName, string expected, string value)
    {
        // When
        byte[] bytes = Multibase.Decode(value, out MultibaseEncoding encoding);

        // Then
        Assert.Equal(expected, Convert.ToHexString(bytes));
        Assert.Equal(Encodings[encodingName], encoding);
    }

    [Theory(DisplayName = "Multibase.TryDecode should reject the official case-insensitivity vectors, because decoding is deliberately strict")]
    [InlineData("base16", "f68656c6c6f20776F726C64")]
    [InlineData("base16upper", "F68656c6c6f20776F726C64")]
    [InlineData("base32", "bnbswy3dpeB3W64TMMQ")]
    [InlineData("base32upper", "Bnbswy3dpeB3W64TMMQ")]
    public void MultibaseTryDecodeShouldRejectTheOfficialCaseInsensitivityVectors(string encodingName, string value)
    {
        // When
        bool result = Multibase.TryDecode(value, Encodings[encodingName], out byte[] bytes);

        // Then
        Assert.False(result);
        Assert.Empty(bytes);
    }

    [Theory(DisplayName = "Multibase.TryDecode should reject invalid values and non-final prefixes")]
    [InlineData("")]
    [InlineData("x796573")]
    [InlineData("0101")]
    [InlineData("9573277761329450583662625")]
    [InlineData("k2lcpzo5yikidynfl")]
    [InlineData("bPFSXGIDNMFXGSIBB")]
    [InlineData("BpfsxgIDNMFXGSIBB")]
    [InlineData("f796573206D616E692021")]
    [InlineData("uZh")]
    [InlineData("uZg==")]
    [InlineData("UZg")]
    [InlineData("bmz")]
    [InlineData("Z7Pznk19XTTzBtx")]
    [InlineData("cpfsxgidnmfxgsibb")]
    [InlineData("hxf1zgedpcfzg1ebb")]
    [InlineData("vf5in683dc5n6i811")]
    [InlineData("MeWVzIG1hbmkgIQ==")]
    [InlineData("z0")]
    [InlineData("u+/8")]
    [InlineData("meWVz IG1hbmkgIQ")]
    public void MultibaseTryDecodeShouldRejectInvalidValues(string value)
    {
        // When
        bool result = Multibase.TryDecode(value, out MultibaseEncoding? encoding, out byte[] bytes);

        // Then
        Assert.False(result);
        Assert.Null(encoding);
        Assert.Empty(bytes);
    }

    [Fact(DisplayName = "Multibase.Decode should throw FormatException for an invalid value")]
    public void MultibaseDecodeShouldThrowFormatExceptionForAnInvalidValue()
    {
        // When
        Exception exception = Assert.Throws<FormatException>(() => Multibase.Decode("x796573", out MultibaseEncoding _));

        // Then
        Assert.Equal("The value is not a valid Multibase string.", exception.Message);
    }

    [Fact(DisplayName = "Multibase.Decode with a required encoding should reject a value in a different encoding")]
    public void MultibaseDecodeWithARequiredEncodingShouldRejectAValueInADifferentEncoding()
    {
        // Given
        const string value = "z7paNL19xttacUY";

        // When
        bool result = Multibase.TryDecode(value, MultibaseEncoding.UnpaddedBase64Url, out byte[] bytes);
        Exception exception = Assert.Throws<FormatException>(() => Multibase.Decode(value, MultibaseEncoding.UnpaddedBase64Url));

        // Then
        Assert.False(result);
        Assert.Empty(bytes);
        Assert.Equal("The value is not a valid Multibase string in the required encoding.", exception.Message);
    }

    [Fact(DisplayName = "Multibase.Decode with a required encoding should decode a value in that encoding")]
    public void MultibaseDecodeWithARequiredEncodingShouldDecodeAValueInThatEncoding()
    {
        // When
        byte[] bytes = Multibase.Decode("z7paNL19xttacUY", MultibaseEncoding.Base58Bitcoin);

        // Then
        Assert.Equal("yes mani !"u8.ToArray(), bytes);
    }

    [Fact(DisplayName = "Multibase should round-trip an empty value as its prefix alone")]
    public void MultibaseShouldRoundTripAnEmptyValueAsItsPrefixAlone()
    {
        foreach (MultibaseEncoding encoding in MultibaseEncoding.GetAll())
        {
            // When
            string encoded = Multibase.Encode([], encoding);
            byte[] decoded = Multibase.Decode(encoded, encoding);

            // Then
            Assert.Equal(encoding.Prefix.ToString(), encoded);
            Assert.Empty(decoded);
        }
    }

    [Fact(DisplayName = "Multibase.Encode should throw ArgumentNullException when the encoding is null")]
    public void MultibaseEncodeShouldThrowArgumentNullExceptionWhenTheEncodingIsNull() =>
        Assert.Throws<ArgumentNullException>(() => Multibase.Encode([], null!));

    [Fact(DisplayName = "MultibaseEncoding prefixes should be unique")]
    public void MultibaseEncodingPrefixesShouldBeUnique()
    {
        // Given
        IReadOnlySet<MultibaseEncoding> encodings = MultibaseEncoding.GetAll();
        HashSet<char> prefixes = [];

        // Then
        Assert.Equal(8, encodings.Count);
        foreach (MultibaseEncoding encoding in encodings) Assert.True(prefixes.Add(encoding.Prefix), $"Duplicate prefix '{encoding.Prefix}'.");
    }
}
