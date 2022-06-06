// Copyright 2020-2022 ONIXLabs
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

using Xunit;

namespace OnixLabs.Security.Cryptography.UnitTests;

public sealed class Sha3ManagedShake128Tests : Sha3ShakeTestBase
{
    [Theory(DisplayName = "Sha3ManagedShake128 should produce the expected hash for the specified string literal")]
    [InlineData("7f9c2ba4", "", 4)]
    [InlineData("7f9c2ba4e88f827d", "", 8)]
    [InlineData("7f9c2ba4e88f827d616045507605853e", "", 16)]
    [InlineData("7f9c2ba4e88f827d616045507605853ed73b8093f6efbc88eb1a6eacfa66ef26", "", 32)]
    [InlineData("1cd2c71a", "1234567890", 4)]
    [InlineData("1cd2c71a52e3f2a6", "1234567890", 8)]
    [InlineData("1cd2c71a52e3f2a620173e915f17648d", "1234567890", 16)]
    [InlineData("1cd2c71a52e3f2a620173e915f17648dcc43443ef78754302c6b44cf47daf527", "1234567890", 32)]
    [InlineData("5b3b6a58", "ABCDEFGHIJKLMNOPQRSTUVWXYZ", 4)]
    [InlineData("5b3b6a587417f8fa", "ABCDEFGHIJKLMNOPQRSTUVWXYZ", 8)]
    [InlineData("5b3b6a587417f8fa192aba21c16b7b7a", "ABCDEFGHIJKLMNOPQRSTUVWXYZ", 16)]
    [InlineData("5b3b6a587417f8fa192aba21c16b7b7a6375aac5f04e950f", "ABCDEFGHIJKLMNOPQRSTUVWXYZ", 24)]
    [InlineData("961c919c", "abcdefghijklmnopqrstuvwxyz", 4)]
    [InlineData("961c919c0854576e", "abcdefghijklmnopqrstuvwxyz", 8)]
    [InlineData("961c919c0854576e561320e81514bf37", "abcdefghijklmnopqrstuvwxyz", 16)]
    [InlineData("961c919c0854576e561320e81514bf3724197d0715e16a36", "abcdefghijklmnopqrstuvwxyz", 24)]
    public override void TestSha3WithLiteralString(string expected, string literal, int length)
    {
        HashAlgorithm = Sha3.CreateSha3Shake128(length);
        base.TestSha3WithLiteralString(expected, literal, length);
    }

    [Theory(DisplayName = "Sha3ManagedShake128 should produce the expected hash for the specified string template")]
    [InlineData("9d222c79", "a", 1_000_000, 4)]
    [InlineData("9d222c79c4ff9d09", "a", 1_000_000, 8)]
    [InlineData("9d222c79c4ff9d092cf6ca86143aa411", "a", 1_000_000, 16)]
    [InlineData("9d222c79c4ff9d092cf6ca86143aa411e369973808ef97093255826c5572ef58", "a", 1_000_000, 32)]
    public override void TestSha3WithGeneratedString(string expected, string template, int iterations, int length)
    {
        HashAlgorithm = Sha3.CreateSha3Shake128(length);
        base.TestSha3WithGeneratedString(expected, template, iterations, length);
    }
}
