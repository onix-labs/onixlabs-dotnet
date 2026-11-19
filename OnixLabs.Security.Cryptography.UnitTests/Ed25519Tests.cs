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

public sealed class Ed25519Tests
{
    /// <summary>
    /// Verifies the first two test vectors of RFC 8032 §7.1 through <see cref="Ed25519"/> directly.
    /// </summary>
    [Theory(DisplayName = "Ed25519 must satisfy the RFC 8032 §7.1 test vectors")]
    [InlineData(
        "9d61b19deffd5a60ba844af492ec2cc44449c5697b326919703bac031cae7f60",
        "d75a980182b10ab7d54bfed3c964073a0ee172f3daa62325af021a68f707511a",
        "",
        "e5564300c360ac729086e2cc806e828a84877f1eb8e5d974d873e065224901555fb8821590a33bacc61e39701cf9b46bd25bf5f0595bbe24655141438e7a100b")]
    [InlineData(
        "4ccd089b28ff96da9db6c346ec114e0f5b8a319f35aba624da8cf6ed4fb8a6fb",
        "3d4017c3e843895a92b70aa74d1b7ebc9c982ccf2ec4968cc0cd55f12af4660c",
        "72",
        "92a009a9f0d4cab8720e820b5f642540a2b27b5416503f8fb3762223ebdb69da085ac1e43e15996e458f3613d0f11d8c387b2eaeb4302aeeb00d291612bb0c00")]
    public void Ed25519MustSatisfyRfc8032TestVectors(string seed, string publicKey, string message, string signature)
    {
        // Given
        using Ed25519 privateKey = Ed25519.ImportPrivateKey(Convert.FromHexString(seed));
        using Ed25519 verifier = Ed25519.ImportPublicKey(Convert.FromHexString(publicKey));
        byte[] data = Convert.FromHexString(message);

        // When
        byte[] actual = privateKey.SignData(data);

        // Then
        Assert.Equal(publicKey, Convert.ToHexString(privateKey.ExportPublicKey()).ToLowerInvariant());
        Assert.Equal(signature, Convert.ToHexString(actual).ToLowerInvariant());
        Assert.True(verifier.VerifyData(data, actual));
        Assert.True(privateKey.VerifyData(data, actual));
    }

    [Fact(DisplayName = "Ed25519 should round-trip PKCS#8 and SubjectPublicKeyInfo, matching EddsaPrivateKey and EddsaPublicKey")]
    public void Ed25519ShouldMatchKeyTypeEncodings()
    {
        // Given
        EddsaPrivateKey privateKey = EddsaPrivateKey.Create();

        // When
        using Ed25519 fromPkcs8 = Ed25519.ImportPkcs8PrivateKey(privateKey.ExportPkcs8());
        using Ed25519 fromSpki = Ed25519.ImportSubjectPublicKeyInfo(fromPkcs8.ExportSubjectPublicKeyInfo());

        // Then
        Assert.Equal(privateKey.Export(), fromPkcs8.ExportPrivateKey());
        Assert.Equal(privateKey.ExportPkcs8(), fromPkcs8.ExportPkcs8PrivateKey());
        Assert.Equal(privateKey.GetPublicKey().Export(), fromSpki.ExportPublicKey());
    }

    [Fact(DisplayName = "Ed25519 should report faults with the same exceptions as .NET's signing classes")]
    public void Ed25519ShouldReportFaultsLikeDotNet()
    {
        // Given
        using Ed25519 key = Ed25519.GenerateKey();
        using Ed25519 publicOnly = Ed25519.ImportPublicKey(key.ExportPublicKey());
        byte[] signature = key.SignData([1, 2, 3]);

        // When / Then
        Assert.True(Ed25519.IsSupported);
        Assert.Throws<ArgumentException>(() => Ed25519.ImportPrivateKey(new byte[31]));
        Assert.Throws<ArgumentException>(() => Ed25519.ImportPublicKey(new byte[33]));
        Assert.Throws<ArgumentException>(() => key.SignData([1, 2, 3], new byte[63]));
        Assert.Throws<CryptographicException>(() => publicOnly.SignData([1, 2, 3]));
        Assert.Throws<CryptographicException>(() => publicOnly.ExportPrivateKey());
        Assert.True(publicOnly.VerifyData([1, 2, 3], signature));
        Assert.False(publicOnly.VerifyData([1, 2, 4], signature));
        Assert.False(publicOnly.VerifyData([1, 2, 3], signature[..63]));

        key.Dispose();
        Assert.Throws<ObjectDisposedException>(() => key.SignData([1, 2, 3]));
    }
}
