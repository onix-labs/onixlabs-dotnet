// Copyright © 2020 ONIXLabs
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

public sealed class Sha3Hash224Tests : Sha3HashTestBase
{
    protected override Sha3 HashAlgorithm => Sha3.CreateSha3Hash224();

    [Theory(DisplayName = "Sha3Managed224 should produce the expected hash for the specified string literal")]
    [InlineData("6b4e03423667dbb73b6e15454f0eb1abd4597f9a1b078e3f5b5a6bc7", "")]
    [InlineData("9877af03f5e1919851d0ef4ce6b23f1e85a40b446d93713f4c6e6dcd", "1234567890")]
    [InlineData("beae76edd99d4ad4d398d51c5ea1d8b7b3fa6d49d687b0cb1ec2ec41", "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    [InlineData("5cdeca81e123f87cad96b9cba999f16f6d41549608d4e0f4681b8239", "abcdefghijklmnopqrstuvwxyz")]
    public override void TestSha3WithLiteralString(string expected, string literal)
    {
        base.TestSha3WithLiteralString(expected, literal);
    }

    [Theory(DisplayName = "Sha3Managed224 should produce the expected hash for the specified string template")]
    [InlineData("d69335b93325192e516a912e6d19a15cb51c6ed5c15243e7a7fd653c", "a", 1_000_000)]
    public override void TestSha3WithGeneratedString(string expected, string template, int iterations)
    {
        base.TestSha3WithGeneratedString(expected, template, iterations);
    }
}
