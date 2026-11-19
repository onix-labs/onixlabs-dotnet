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

/// <summary>
/// Tests AES Key Wrap (RFC 3394) through <see cref="Aes"/>'s key wrap methods: the library's own on .NET 8, 9 and 10, and .NET's on .NET
/// 11. The same tests run on every target, so they show that both behave alike.
/// </summary>
/// <remarks>
/// Why: failures are checked as any <see cref="CryptographicException"/>, not exactly that type, because .NET's implementation on Linux
/// throws a type derived from it.
/// </remarks>
public sealed class AesKeyWrapTests
{
    /// <summary>
    /// Verifies the six test vectors of RFC 3394 §4.1 to §4.6, in both directions.
    /// </summary>
    [Theory(DisplayName = "Aes.EncryptKeyWrap and DecryptKeyWrap must satisfy the RFC 3394 §4 test vectors")]
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
        using Aes aes = CreateAes(Convert.FromHexString(keyEncryptionKey));
        byte[] key = Convert.FromHexString(keyData);
        byte[] expected = Convert.FromHexString(ciphertext);

        // When
        byte[] wrapped = aes.EncryptKeyWrap(key);
        byte[] unwrapped = aes.DecryptKeyWrap(expected);

        // Then
        Assert.Equal(expected, wrapped);
        Assert.Equal(key, unwrapped);
        Assert.Equal(expected.Length, Aes.GetKeyWrapLength(key.Length));
    }

    /// <summary>
    /// Verifies every case of Project Wycheproof's <c>testvectors_v1/aes_wrap_test.json</c>. Valid cases must wrap and unwrap to the
    /// expected values. Invalid cases must fail to unwrap: with <see cref="ArgumentException"/> if the wrapped key is not a valid length,
    /// or <see cref="CryptographicException"/> if it fails the integrity check. Acceptable cases, which wrap 8-byte keys that RFC 3394 does
    /// not allow, may do either; .NET refuses them, and so does the library.
    /// </summary>
    [Theory(DisplayName = "Aes key wrap must satisfy Project Wycheproof AES Key Wrap test vectors")]
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
        using Aes aes = CreateAes(key);
        byte[] destination = new byte[Math.Max(0, ciphertext.Length - 8)];
        bool isValidLength = ciphertext.Length >= 24 && ciphertext.Length % 8 == 0;
        bool canWrap = message.Length >= 16 && message.Length % 8 == 0;

        // When
        Exception? exception = Record.Exception(() => aes.DecryptKeyWrap(ciphertext, destination));

        // Then
        if (result == "valid")
        {
            Assert.Null(exception);
            Assert.Equal(message, destination);
            Assert.Equal(ciphertext, aes.EncryptKeyWrap(message));
            return;
        }

        if (exception is null)
        {
            Assert.Equal("acceptable", result);
            Assert.Equal(message, destination);
            return;
        }

        if (isValidLength) Assert.IsAssignableFrom<CryptographicException>(exception);
        else Assert.IsType<ArgumentException>(exception);

        Assert.All(destination, value => Assert.Equal(0, value));

        if (!canWrap) Assert.Throws<ArgumentException>(() => aes.EncryptKeyWrap(message));
    }

    [Fact(DisplayName = "Aes.TryDecryptKeyWrap should throw and zero the destination when the wrapped key is modified")]
    public void AesTryDecryptKeyWrapShouldThrowAndZeroDestinationWhenModified()
    {
        // Given
        using Aes aes = CreateAes(Salt.CreateNonZero(32).AsReadOnlySpan().ToArray());
        byte[] key = Salt.CreateNonZero(64).AsReadOnlySpan().ToArray();
        byte[] wrapped = aes.EncryptKeyWrap(key);
        byte[] destination = new byte[key.Length];
        wrapped[^1] ^= 1;

        // When
        Exception? exception = Record.Exception(() => aes.TryDecryptKeyWrap(wrapped, destination, out int _));

        // Then
        Assert.IsAssignableFrom<CryptographicException>(exception);
        Assert.Equal(new byte[key.Length], destination);
    }

    [Fact(DisplayName = "Aes.DecryptKeyWrap should throw CryptographicException with the wrong key-encryption key")]
    public void AesDecryptKeyWrapShouldThrowWithWrongKeyEncryptionKey()
    {
        // Given
        using Aes sender = CreateAes(Salt.CreateNonZero(32).AsReadOnlySpan().ToArray());
        using Aes recipient = CreateAes(Salt.CreateNonZero(32).AsReadOnlySpan().ToArray());
        byte[] wrapped = sender.EncryptKeyWrap(Salt.CreateNonZero(32).AsReadOnlySpan().ToArray());

        // When / Then
        Assert.ThrowsAny<CryptographicException>(() => recipient.DecryptKeyWrap(wrapped));
    }

    [Theory(DisplayName = "Aes.GetKeyWrapLength should throw ArgumentOutOfRangeException for a length that cannot be wrapped")]
    [InlineData(0)]
    [InlineData(8)]
    [InlineData(12)]
    [InlineData(17)]
    public void AesGetKeyWrapLengthShouldThrowForInvalidLength(int length)
    {
        // When / Then
        Assert.Throws<ArgumentOutOfRangeException>(() => Aes.GetKeyWrapLength(length));
    }

    [Fact(DisplayName = "Aes.EncryptKeyWrap should throw ArgumentException unless the destination is exactly the wrapped length")]
    public void AesEncryptKeyWrapShouldThrowForWrongDestinationLength()
    {
        // Given
        using Aes aes = CreateAes(new byte[16]);

        // When / Then
        Assert.Throws<ArgumentException>(() => aes.EncryptKeyWrap(new byte[16], new byte[23]));
        Assert.Throws<ArgumentException>(() => aes.EncryptKeyWrap(new byte[16], new byte[25]));
    }

    [Fact(DisplayName = "Aes.TryDecryptKeyWrap should return false when the destination is too short")]
    public void AesTryDecryptKeyWrapShouldReturnFalseForShortDestination()
    {
        // Given
        using Aes aes = CreateAes(new byte[16]);
        byte[] wrapped = aes.EncryptKeyWrap(new byte[32]);

        // When
        bool result = aes.TryDecryptKeyWrap(wrapped, new byte[31], out int bytesWritten);

        // Then
        Assert.False(result);
        Assert.Equal(0, bytesWritten);
        Assert.Throws<ArgumentException>(() => aes.DecryptKeyWrap(wrapped, new byte[31]));
    }

    [Fact(DisplayName = "Aes key wrap should leave the caller's cipher mode and padding unchanged")]
    public void AesKeyWrapShouldLeaveCipherSettingsUnchanged()
    {
        // Given
        using Aes aes = CreateAes(new byte[16]);
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;

        // When
        byte[] wrapped = aes.EncryptKeyWrap(new byte[16]);
        _ = aes.DecryptKeyWrap(wrapped);

        // Then
        Assert.Equal(CipherMode.CBC, aes.Mode);
        Assert.Equal(PaddingMode.PKCS7, aes.Padding);
    }

#if NET10_0_OR_GREATER
    /// <summary>
    /// Guards against replacing RFC 3394 with .NET's padded key wrap, RFC 5649, which .NET has from .NET 10 and which gives different
    /// output.
    /// </summary>
    [Fact(DisplayName = "Aes.EncryptKeyWrap must differ from Aes.EncryptKeyWrapPadded, which is RFC 5649 and not what JOSE requires")]
    public void AesEncryptKeyWrapMustDifferFromEncryptKeyWrapPadded()
    {
        // Given
        using Aes aes = CreateAes(Convert.FromHexString("000102030405060708090A0B0C0D0E0F"));
        byte[] key = Convert.FromHexString("00112233445566778899AABBCCDDEEFF");

        // When
        byte[] rfc3394 = aes.EncryptKeyWrap(key);
        byte[] rfc5649 = aes.EncryptKeyWrapPadded(key);

        // Then
        Assert.Equal(rfc3394.Length, rfc5649.Length);
        Assert.NotEqual(rfc3394, rfc5649);
    }
#endif

    /// <summary>
    /// Creates an <see cref="Aes"/> instance keyed with the specified key.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <returns>Returns a new <see cref="Aes"/> instance.</returns>
    private static Aes CreateAes(byte[] key)
    {
        Aes aes = Aes.Create();
        aes.Key = key;
        return aes;
    }
}
