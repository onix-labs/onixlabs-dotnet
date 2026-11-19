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
using System.Security.Cryptography;
using OnixLabs.Security.Cryptography.UnitTests.Data;

namespace OnixLabs.Security.Cryptography.UnitTests;

public sealed class AesKeyWrapTests
{
    /// <summary>
    /// Verifies the six test vectors of RFC 3394 §4.1 to §4.6, in both directions.
    /// </summary>
    [Theory(DisplayName = "AesKeyWrap must satisfy the RFC 3394 §4 test vectors")]
    [InlineData( // §4.1: wrap 128 bits of key data with a 128-bit KEK.
        "000102030405060708090A0B0C0D0E0F",
        "00112233445566778899AABBCCDDEEFF",
        "1FA68B0A8112B447AEF34BD8FB5A7B829D3E862371D2CFE5")]
    [InlineData( // §4.2: wrap 128 bits of key data with a 192-bit KEK.
        "000102030405060708090A0B0C0D0E0F1011121314151617",
        "00112233445566778899AABBCCDDEEFF",
        "96778B25AE6CA435F92B5B97C050AED2468AB8A17AD84E5D")]
    [InlineData( // §4.3: wrap 128 bits of key data with a 256-bit KEK.
        "000102030405060708090A0B0C0D0E0F101112131415161718191A1B1C1D1E1F",
        "00112233445566778899AABBCCDDEEFF",
        "64E8C3F9CE0F5BA263E9777905818A2A93C8191E7D6E8AE7")]
    [InlineData( // §4.4: wrap 192 bits of key data with a 192-bit KEK.
        "000102030405060708090A0B0C0D0E0F1011121314151617",
        "00112233445566778899AABBCCDDEEFF0001020304050607",
        "031D33264E15D33268F24EC260743EDCE1C6C7DDEE725A936BA814915C6762D2")]
    [InlineData( // §4.5: wrap 192 bits of key data with a 256-bit KEK.
        "000102030405060708090A0B0C0D0E0F101112131415161718191A1B1C1D1E1F",
        "00112233445566778899AABBCCDDEEFF0001020304050607",
        "A8F9BC1612C68B3FF6E6F4FBE30E71E4769C8B80A32CB8958CD5D17D6B254DA1")]
    [InlineData( // §4.6: wrap 256 bits of key data with a 256-bit KEK.
        "000102030405060708090A0B0C0D0E0F101112131415161718191A1B1C1D1E1F",
        "00112233445566778899AABBCCDDEEFF000102030405060708090A0B0C0D0E0F",
        "28C9F404C4B810F4CBCCB35CFB87F8263F5786E2D80ED326CBC7F0E71A99F43BFB988B9B7A02DD21")]
    public void AesKeyWrapMustSatisfyRfc3394TestVectors(string keyEncryptionKey, string keyData, string ciphertext)
    {
        // Given
        byte[] kek = Convert.FromHexString(keyEncryptionKey);
        byte[] key = Convert.FromHexString(keyData);
        byte[] expected = Convert.FromHexString(ciphertext);

        // When
        byte[] wrapped = AesKeyWrap.Wrap(kek, key);
        byte[] unwrapped = AesKeyWrap.Unwrap(kek, expected);

        // Then
        Assert.Equal(expected, wrapped);
        Assert.Equal(key, unwrapped);
    }

    /// <summary>
    /// Verifies every case of Project Wycheproof's <c>testvectors_v1/aes_wrap_test.json</c>. Valid cases must wrap and unwrap to the
    /// expected
    /// values. Invalid cases must fail to unwrap, and keys that cannot be wrapped must be refused. Acceptable cases, which wrap 8-byte keys
    /// that
    /// RFC 3394 does not allow, may do either; this implementation refuses them.
    /// </summary>
    [Theory(DisplayName = "AesKeyWrap must satisfy Project Wycheproof AES Key Wrap test vectors")]
    [WycheproofAesKeyWrapData]
    public void AesKeyWrapMustSatisfyWycheproofTestVectors(
        int tcId,
        byte[] key,
        byte[] message,
        byte[] ciphertext,
        string result,
        string flags)
    {
        // Given
        _ = (tcId, flags);
        byte[] destination = new byte[Math.Max(0, ciphertext.Length - AesKeyWrap.SemiblockLength)];
        bool canWrap = message.Length >= AesKeyWrap.MinimumKeyLength && message.Length % AesKeyWrap.SemiblockLength == 0;

        // When
        bool unwrapped = AesKeyWrap.TryUnwrap(key, ciphertext, destination, out int bytesWritten);

        // Then
        if (result == "valid")
        {
            Assert.True(unwrapped);
            Assert.Equal(message, destination[..bytesWritten]);
            Assert.Equal(ciphertext, AesKeyWrap.Wrap(key, message));
            return;
        }

        if (unwrapped)
        {
            Assert.Equal("acceptable", result);
            Assert.Equal(message, destination[..bytesWritten]);
            return;
        }

        Assert.Equal(0, bytesWritten);
        Assert.All(destination, value => Assert.Equal(0, value));
        Assert.Throws<CryptographicException>(() => AesKeyWrap.Unwrap(key, ciphertext));

        if (!canWrap) Assert.Throws<ArgumentException>(() => AesKeyWrap.Wrap(key, message));
    }

    [Fact(DisplayName = "AesKeyWrap.TryUnwrap should fail and zero the destination when the wrapped key is modified")]
    public void AesKeyWrapTryUnwrapShouldFailAndZeroDestinationWhenModified()
    {
        // Given
        byte[] kek = Salt.CreateNonZero(32).AsReadOnlySpan().ToArray();
        byte[] key = Salt.CreateNonZero(64).AsReadOnlySpan().ToArray();
        byte[] wrapped = AesKeyWrap.Wrap(kek, key);
        byte[] destination = new byte[key.Length];
        wrapped[^1] ^= 1;

        // When
        bool result = AesKeyWrap.TryUnwrap(kek, wrapped, destination, out int bytesWritten);

        // Then
        Assert.False(result);
        Assert.Equal(0, bytesWritten);
        Assert.Equal(new byte[key.Length], destination);
    }

    [Fact(DisplayName = "AesKeyWrap.TryUnwrap should fail with the wrong key-encryption key")]
    public void AesKeyWrapTryUnwrapShouldFailWithWrongKeyEncryptionKey()
    {
        // Given
        byte[] key = Salt.CreateNonZero(32).AsReadOnlySpan().ToArray();
        byte[] wrapped = AesKeyWrap.Wrap(Salt.CreateNonZero(32).AsReadOnlySpan(), key);

        // When
        bool result = AesKeyWrap.TryUnwrap(Salt.CreateNonZero(32).AsReadOnlySpan(), wrapped, new byte[key.Length], out int _);

        // Then
        Assert.False(result);
    }

    [Theory(DisplayName = "AesKeyWrap should throw ArgumentException for a key-encryption key that is not 16, 24 or 32 bytes")]
    [InlineData(0)]
    [InlineData(15)]
    [InlineData(20)]
    [InlineData(64)]
    public void AesKeyWrapShouldThrowForInvalidKeyEncryptionKeyLength(int length)
    {
        // Given
        byte[] kek = new byte[length];

        // When / Then
        Assert.Throws<ArgumentException>(() => AesKeyWrap.Wrap(kek, new byte[16]));
        Assert.Throws<ArgumentException>(() => AesKeyWrap.Unwrap(kek, new byte[24]));
    }

    [Fact(DisplayName = "AesKeyWrap.TryUnwrap should throw ArgumentException when the destination is too short")]
    public void AesKeyWrapTryUnwrapShouldThrowForShortDestination()
    {
        // Given
        byte[] kek = new byte[16];
        byte[] wrapped = AesKeyWrap.Wrap(kek, new byte[32]);

        // When / Then
        Assert.Throws<ArgumentException>(() => AesKeyWrap.TryUnwrap(kek, wrapped, new byte[31], out int _));
    }

#if NET10_0_OR_GREATER
    /// <summary>
    /// Guards against replacing RFC 3394 with .NET's key wrap, which is RFC 5649 (padded) from .NET 10 and gives different output.
    /// </summary>
    [Fact(DisplayName = "AesKeyWrap.Wrap must differ from Aes.EncryptKeyWrapPadded, which is RFC 5649 and not what JOSE requires")]
    public void AesKeyWrapMustDifferFromEncryptKeyWrapPadded()
    {
        // Given
        byte[] kek = Convert.FromHexString("000102030405060708090A0B0C0D0E0F");
        byte[] key = Convert.FromHexString("00112233445566778899AABBCCDDEEFF");
        using Aes aes = Aes.Create();
        aes.Key = kek;

        // When
        byte[] rfc3394 = AesKeyWrap.Wrap(kek, key);
        byte[] rfc5649 = aes.EncryptKeyWrapPadded(key);

        // Then
        Assert.Equal(rfc3394.Length, rfc5649.Length);
        Assert.NotEqual(rfc3394, rfc5649);
    }
#endif
}
