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

using System.Security.Cryptography;
using Xunit;

namespace OnixLabs.Security.Cryptography.UnitTests;

public sealed class RsaKeyTests
{
    [Fact(DisplayName = "RSA sign and verify with two identical keys should succeed")]
    public void RsaSignAndVerifyWithTwoIdenticalKeysShouldSucceed()
    {
        // Given
        byte[] data = Salt.CreateNonZero(2048).ToByteArray();
        HashAlgorithmName algorithm = HashAlgorithmName.SHA256;
        RSASignaturePadding padding = RSASignaturePadding.Pkcs1;
        IRsaPrivateKey privateKey1 = RsaPrivateKey.Create();
        IRsaPrivateKey privateKey2 = new RsaPrivateKey(privateKey1.ToByteArray());
        IRsaPublicKey publicKey1 = privateKey1.GetPublicKey();
        IRsaPublicKey publicKey2 = privateKey2.GetPublicKey();

        // When
        DigitalSignature signature1 = new(privateKey1.SignData(data, algorithm, padding));
        DigitalSignature signature2 = new(privateKey2.SignData(data, algorithm, padding));

        // Then
        Assert.True(publicKey1.IsDataValid(signature1, data, algorithm, padding));
        Assert.True(publicKey1.IsDataValid(signature2, data, algorithm, padding));
        Assert.True(publicKey2.IsDataValid(signature1, data, algorithm, padding));
        Assert.True(publicKey2.IsDataValid(signature2, data, algorithm, padding));
    }
}
