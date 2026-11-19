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

public sealed class ConcatKdfTests
{
    /// <summary>
    /// Verifies the ECDH-ES example of RFC 7518 Appendix C: Z, the OtherInfo bytes and the derived 128-bit key are copied from it.
    /// </summary>
    [Fact(DisplayName = "ConcatKdf.DeriveKey must produce the derived key of RFC 7518 Appendix C")]
    public void ConcatKdfMustProduceRfc7518AppendixCDerivedKey()
    {
        // Given
        byte[] z =
        [
            158, 86, 217, 29, 129, 113, 53, 211, 114, 131, 66, 131, 191, 132,
            38, 156, 251, 49, 110, 163, 218, 128, 106, 72, 246, 218, 167, 121,
            140, 254, 144, 196
        ];
        byte[] otherInfo =
        [
            0, 0, 0, 7, 65, 49, 50, 56, 71, 67, 77, 0, 0, 0, 5, 65, 108, 105,
            99, 101, 0, 0, 0, 3, 66, 111, 98, 0, 0, 0, 128
        ];
        byte[] expected = [86, 170, 141, 234, 248, 35, 109, 32, 92, 34, 40, 205, 113, 167, 16, 26];
        byte[] actual = new byte[16];

        // When
        ConcatKdf.DeriveKey(HashAlgorithmName.SHA256, z, actual, otherInfo);

        // Then
        Assert.Equal(expected, actual);
    }

    /// <summary>
    /// Verifies multi-round output against a direct computation of H(counter ‖ Z ‖ OtherInfo) per NIST SP 800-56A Rev. 3 §5.8.2.1,
    /// for lengths either side of each hash block boundary.
    /// </summary>
    [Theory(DisplayName = "ConcatKdf.DeriveKey should concatenate counter-mode hash blocks and truncate to the key length")]
    [InlineData("SHA256", 1)]
    [InlineData("SHA256", 31)]
    [InlineData("SHA256", 32)]
    [InlineData("SHA256", 33)]
    [InlineData("SHA256", 64)]
    [InlineData("SHA256", 100)]
    [InlineData("SHA384", 48)]
    [InlineData("SHA384", 49)]
    [InlineData("SHA512", 64)]
    [InlineData("SHA512", 129)]
    public void ConcatKdfShouldConcatenateCounterModeBlocks(string hashName, int length)
    {
        // Given
        HashAlgorithmName hashAlgorithm = new(hashName);
        byte[] z = Salt.CreateNonZero(48).AsReadOnlySpan().ToArray();
        byte[] otherInfo = Salt.CreateNonZero(40).AsReadOnlySpan().ToArray();
        byte[] expected = new byte[length];
        byte[] actual = new byte[length];

        for (int round = 1, offset = 0; offset < length; round++)
        {
            byte[] input = [(byte)(round >> 24), (byte)(round >> 16), (byte)(round >> 8), (byte)round, ..z, ..otherInfo];
            byte[] block = hashName switch
            {
                "SHA256" => SHA256.HashData(input),
                "SHA384" => SHA384.HashData(input),
                _ => SHA512.HashData(input)
            };
            int count = Math.Min(block.Length, length - offset);
            block.AsSpan(0, count).CopyTo(expected.AsSpan(offset));
            offset += count;
        }

        // When
        ConcatKdf.DeriveKey(hashAlgorithm, z, actual, otherInfo);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ConcatKdf.DeriveKey should throw ArgumentException for an empty output")]
    public void ConcatKdfShouldThrowForEmptyOutput()
    {
        // When / Then
        Assert.Throws<ArgumentException>(() => ConcatKdf.DeriveKey(HashAlgorithmName.SHA256, [1, 2, 3], [], []));
    }

    [Theory(DisplayName = "ConcatKdf.DeriveKey should throw ArgumentOutOfRangeException for an unsupported hash algorithm, as HKDF does")]
    [InlineData("")]
    [InlineData("SHA1")]
    [InlineData("MD5")]
    public void ConcatKdfShouldThrowForUnsupportedHashAlgorithm(string name)
    {
        // Given
        HashAlgorithmName hashAlgorithmName = name.Length == 0 ? default : new HashAlgorithmName(name);

        // When / Then
        Assert.Throws<ArgumentOutOfRangeException>(() => ConcatKdf.DeriveKey(hashAlgorithmName, [1, 2, 3], new byte[16], []));
        Assert.Throws<ArgumentOutOfRangeException>(() => ConcatKdf.DeriveKey(hashAlgorithmName, [1, 2, 3], 16));
    }

    [Fact(DisplayName = "ConcatKdf.DeriveKey with an output length should match the span overload")]
    public void ConcatKdfArrayOverloadShouldMatchSpanOverload()
    {
        // Given
        byte[] sharedSecret = [1, 2, 3, 4, 5, 6, 7, 8];
        byte[] otherInfo = [9, 10, 11];
        byte[] expected = new byte[40];
        ConcatKdf.DeriveKey(HashAlgorithmName.SHA256, sharedSecret, expected, otherInfo);

        // When
        byte[] actual = ConcatKdf.DeriveKey(HashAlgorithmName.SHA256, sharedSecret, 40, otherInfo);
        byte[] withoutOtherInfo = ConcatKdf.DeriveKey(HashAlgorithmName.SHA256, sharedSecret, 40);

        // Then
        Assert.Equal(expected, actual);
        Assert.NotEqual(expected, withoutOtherInfo);
    }

    [Theory(DisplayName = "ConcatKdf.DeriveKey should throw ArgumentOutOfRangeException for a non-positive output length, as HKDF does")]
    [InlineData(0)]
    [InlineData(-1)]
    public void ConcatKdfShouldThrowForNonPositiveOutputLength(int outputLength)
    {
        // When / Then
        Assert.Throws<ArgumentOutOfRangeException>(() => ConcatKdf.DeriveKey(HashAlgorithmName.SHA256, [1, 2, 3], outputLength));
    }

    [Fact(DisplayName = "ConcatKdf.DeriveKey should throw ArgumentNullException for a null shared secret, as HKDF does")]
    public void ConcatKdfShouldThrowForNullSharedSecret()
    {
        // When / Then
        Assert.Throws<ArgumentNullException>(() => ConcatKdf.DeriveKey(HashAlgorithmName.SHA256, null!, 16));
    }
}
