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

namespace OnixLabs.Security.Cryptography.UnitTests;

public sealed class AesCbcHmacSha2Tests
{
    /// <summary>
    /// Verifies the test cases of RFC 7518 Appendix B.1 (A128CBC-HS256), B.2 (A192CBC-HS384) and B.3 (A256CBC-HS512), in both directions.
    /// The values are K, P, IV, A, E and T as given there.
    /// </summary>
    [Theory(DisplayName = "AesCbcHmacSha2 must satisfy the RFC 7518 Appendix B test cases")]
    [InlineData( // RFC 7518 Appendix B.1
        "000102030405060708090A0B0C0D0E0F101112131415161718191A1B1C1D1E1F",
        "41206369706865722073797374656D206D757374206E6F742062652072657175" +
        "6972656420746F206265207365637265742C20616E64206974206D7573742062" +
        "652061626C6520746F2066616C6C20696E746F207468652068616E6473206F66" +
        "2074686520656E656D7920776974686F757420696E636F6E76656E69656E6365",
        "1AF38C2DC2B96FFDD86694092341BC04",
        "546865207365636F6E64207072696E6369706C65206F662041756775737465204B6572636B686F666673",
        "C80EDFA32DDF39D5EF00C0B468834279A2E46A1B8049F792F76BFE54B903A9C9" +
        "A94AC9B47AD2655C5F10F9AEF71427E2FC6F9B3F399A221489F16362C7032336" +
        "09D45AC69864E3321CF82935AC4096C86E133314C54019E8CA7980DFA4B9CF1B" +
        "384C486F3A54C51078158EE5D79DE59FBD34D848B3D69550A67646344427ADE5" +
        "4B8851FFB598F7F80074B9473C82E2DB",
        "652C3FA36B0A7C5B3219FAB3A30BC1C4")]
    [InlineData( // RFC 7518 Appendix B.2
        "000102030405060708090A0B0C0D0E0F101112131415161718191A1B1C1D1E1F202122232425262728292A2B2C2D2E2F",
        "41206369706865722073797374656D206D757374206E6F742062652072657175" +
        "6972656420746F206265207365637265742C20616E64206974206D7573742062" +
        "652061626C6520746F2066616C6C20696E746F207468652068616E6473206F66" +
        "2074686520656E656D7920776974686F757420696E636F6E76656E69656E6365",
        "1AF38C2DC2B96FFDD86694092341BC04",
        "546865207365636F6E64207072696E6369706C65206F662041756775737465204B6572636B686F666673",
        "EA65DA6B59E61EDB419BE62D19712AE5D303EEB50052D0DFD6697F77224C8EDB" +
        "000D279BDC14C1072654BD30944230C657BED4CA0C9F4A8466F22B226D174621" +
        "4BF8CFC2400ADD9F5126E479663FC90B3BED787A2F0FFCBF3904BE2A641D5C21" +
        "05BFE591BAE23B1D7449E532EEF60A9AC8BB6C6B01D35D49787BCD57EF484927" +
        "F280ADC91AC0C4E79C7B11EFC60054E3",
        "8490AC0E58949BFE51875D733F93AC2075168039CCC733D7")]
    [InlineData( // RFC 7518 Appendix B.3
        "000102030405060708090A0B0C0D0E0F101112131415161718191A1B1C1D1E1F202122232425262728292A2B2C2D2E2F303132333435363738393A3B3C3D3E3F",
        "41206369706865722073797374656D206D757374206E6F742062652072657175" +
        "6972656420746F206265207365637265742C20616E64206974206D7573742062" +
        "652061626C6520746F2066616C6C20696E746F207468652068616E6473206F66" +
        "2074686520656E656D7920776974686F757420696E636F6E76656E69656E6365",
        "1AF38C2DC2B96FFDD86694092341BC04",
        "546865207365636F6E64207072696E6369706C65206F662041756775737465204B6572636B686F666673",
        "4AFFAAADB78C31C5DA4B1B590D10FFBD3DD8D5D302423526912DA037ECBCC7BD" +
        "822C301DD67C373BCCB584AD3E9279C2E6D12A1374B77F077553DF829410446B" +
        "36EBD97066296AE6427EA75C2E0846A11A09CCF5370DC80BFECBAD28C73F09B3" +
        "A3B75E662A2594410AE496B2E2E6609E31E6E02CC837F053D21F37FF4F51950B" +
        "BE2638D09DD7A4930930806D0703B1F6",
        "4DD3B4C088A7F45C216839645B2012BF2E6269A8C56A816DBC1B267761955BC5")]
    public void AesCbcHmacSha2MustSatisfyRfc7518AppendixBTestCases(string k, string p, string iv, string a, string e, string t)
    {
        // Given
        using AesCbcHmacSha2 cipher = new(Convert.FromHexString(k));
        byte[] plaintext = Convert.FromHexString(p);
        byte[] initializationVector = Convert.FromHexString(iv);
        byte[] associatedData = Convert.FromHexString(a);
        byte[] expectedCiphertext = Convert.FromHexString(e);
        byte[] expectedTag = Convert.FromHexString(t);
        byte[] tag = new byte[cipher.TagSizeInBytes];

        // When
        byte[] ciphertext = cipher.Encrypt(initializationVector, plaintext, tag, associatedData);
        byte[] decrypted = cipher.Decrypt(initializationVector, expectedCiphertext, expectedTag, associatedData);

        // Then
        Assert.Equal(expectedCiphertext, ciphertext);
        Assert.Equal(expectedTag, tag);
        Assert.Equal(plaintext, decrypted);
    }

    [Theory(DisplayName = "AesCbcHmacSha2.Decrypt should throw AuthenticationTagMismatchException when any input is modified")]
    [InlineData(32, "iv")]
    [InlineData(32, "ciphertext")]
    [InlineData(32, "tag")]
    [InlineData(32, "associatedData")]
    [InlineData(48, "ciphertext")]
    [InlineData(64, "iv")]
    [InlineData(64, "ciphertext")]
    [InlineData(64, "tag")]
    [InlineData(64, "associatedData")]
    public void AesCbcHmacSha2DecryptShouldThrowWhenModified(int keyLength, string modified)
    {
        // Given
        using AesCbcHmacSha2 cipher = new(Salt.CreateNonZero(keyLength).AsReadOnlySpan());
        byte[] iv = Salt.CreateNonZero(AesCbcHmacSha2.IvLength).AsReadOnlySpan().ToArray();
        byte[] associatedData = Salt.CreateNonZero(40).AsReadOnlySpan().ToArray();
        byte[] tag = new byte[cipher.TagSizeInBytes];
        byte[] ciphertext = cipher.Encrypt(iv, Salt.CreateNonZero(100).AsReadOnlySpan(), tag, associatedData);
        byte[] plaintext = new byte[ciphertext.Length];

        byte[] target = modified switch
        {
            "iv" => iv,
            "ciphertext" => ciphertext,
            "tag" => tag,
            _ => associatedData
        };
        target[0] ^= 1;

        // When / Then
        Assert.Throws<AuthenticationTagMismatchException>(() => cipher.Decrypt(iv, ciphertext, tag, plaintext, associatedData));
        Assert.Equal(new byte[plaintext.Length], plaintext);
    }

    /// <summary>
    /// Verifies that the tag is checked before decryption: modifying the last ciphertext block breaks the PKCS #7 padding, so if decryption
    /// ran first it would fail with a padding error rather than a tag mismatch.
    /// </summary>
    [Fact(DisplayName = "AesCbcHmacSha2.Decrypt should check the tag before the padding")]
    public void AesCbcHmacSha2DecryptShouldCheckTagBeforePadding()
    {
        // Given
        using AesCbcHmacSha2 cipher = new(Salt.CreateNonZero(64).AsReadOnlySpan());
        byte[] iv = Salt.CreateNonZero(AesCbcHmacSha2.IvLength).AsReadOnlySpan().ToArray();
        byte[] tag = new byte[cipher.TagSizeInBytes];
        byte[] ciphertext = cipher.Encrypt(iv, new byte[16], tag);

        for (int i = 1; i <= 16; i++)
            ciphertext[^i] ^= 0xFF;

        // When
        Exception exception = Assert.ThrowsAny<CryptographicException>(() => cipher.Decrypt(iv, ciphertext, tag));

        // Then
        Assert.IsType<AuthenticationTagMismatchException>(exception);
    }

    [Theory(DisplayName = "AesCbcHmacSha2 should round-trip plaintexts of every length around the block size")]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(15)]
    [InlineData(16)]
    [InlineData(17)]
    [InlineData(1000)]
    public void AesCbcHmacSha2ShouldRoundTrip(int length)
    {
        // Given
        using AesCbcHmacSha2 cipher = new(Salt.CreateNonZero(64).AsReadOnlySpan());
        byte[] iv = Salt.CreateNonZero(AesCbcHmacSha2.IvLength).AsReadOnlySpan().ToArray();
        byte[] plaintext = length == 0 ? [] : Salt.CreateNonZero(length).AsReadOnlySpan().ToArray();
        byte[] tag = new byte[cipher.TagSizeInBytes];

        // When
        byte[] ciphertext = cipher.Encrypt(iv, plaintext, tag);
        byte[] decrypted = cipher.Decrypt(iv, ciphertext, tag);

        // Then
        Assert.Equal(AesCbcHmacSha2.GetCiphertextLength(length), ciphertext.Length);
        Assert.Equal(plaintext, decrypted);
    }

    [Theory(DisplayName = "AesCbcHmacSha2 should throw ArgumentException for a composite key that is not 32, 48 or 64 bytes")]
    [InlineData(0)]
    [InlineData(16)]
    [InlineData(33)]
    [InlineData(128)]
    public void AesCbcHmacSha2ShouldThrowForInvalidKeyLength(int length)
    {
        // Given
        byte[] key = new byte[length];

        // When / Then
        Assert.Throws<ArgumentException>(() => new AesCbcHmacSha2(key));
    }

    [Fact(DisplayName = "AesCbcHmacSha2 should throw ArgumentException for an IV that is not 16 bytes, or a tag of the wrong length")]
    public void AesCbcHmacSha2ShouldThrowForInvalidIvOrTagLength()
    {
        // Given
        using AesCbcHmacSha2 cipher = new(new byte[32]);

        // When / Then
        Assert.Throws<ArgumentException>(() => cipher.Encrypt(new byte[12], [1, 2, 3], new byte[16]));
        Assert.Throws<ArgumentException>(() => cipher.Encrypt(new byte[16], [1, 2, 3], new byte[32]));
        Assert.Throws<ArgumentException>(() => cipher.Decrypt(new byte[16], new byte[16], new byte[15]));
    }

    [Fact(DisplayName = "AesCbcHmacSha2 should throw ObjectDisposedException after disposal")]
    public void AesCbcHmacSha2ShouldThrowAfterDisposal()
    {
        // Given
        AesCbcHmacSha2 cipher = new(new byte[32]);

        // When
        cipher.Dispose();

        // Then
        Assert.Throws<ObjectDisposedException>(() => cipher.Encrypt(new byte[16], [1, 2, 3], new byte[16]));
    }

    [Theory(DisplayName = "AesCbcHmacSha2 should report its key and tag sizes, as AesGcm does")]
    [InlineData(32, 16)]
    [InlineData(48, 24)]
    [InlineData(64, 32)]
    public void AesCbcHmacSha2ShouldReportKeyAndTagSizes(int keyLength, int tagLength)
    {
        // Given
        byte[] key = new byte[keyLength];

        // When
        using AesCbcHmacSha2 fromArray = new(key);
        using AesCbcHmacSha2 fromSpan = new(key.AsSpan());

        // Then
        Assert.True(AesCbcHmacSha2.IsSupported);
        Assert.Equal(keyLength, fromArray.KeySizeInBytes);
        Assert.Equal(tagLength, fromArray.TagSizeInBytes);
        Assert.Equal(keyLength, fromSpan.KeySizeInBytes);
        Assert.Equal(tagLength, fromSpan.TagSizeInBytes);
        Assert.Equal(16, AesCbcHmacSha2.TagByteSizes.MinSize);
        Assert.Equal(32, AesCbcHmacSha2.TagByteSizes.MaxSize);
        Assert.Equal(8, AesCbcHmacSha2.TagByteSizes.SkipSize);
    }

    [Fact(DisplayName = "AesCbcHmacSha2 should throw ArgumentNullException for a null key")]
    public void AesCbcHmacSha2ShouldThrowForNullKey()
    {
        // When / Then
        Assert.Throws<ArgumentNullException>(() => new AesCbcHmacSha2((byte[])null!));
    }
}
