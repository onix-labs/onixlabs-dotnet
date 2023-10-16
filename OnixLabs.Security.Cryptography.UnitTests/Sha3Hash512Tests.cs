// Copyright 2020-2023 ONIXLabs
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

public sealed class Sha3Managed512Tests : Sha3HashTestBase
{
    protected override Sha3 HashAlgorithm => Sha3.CreateSha3Hash512();

    [Theory(DisplayName = "Sha3Managed512 should produce the expected hash for the specified message")]
    [InlineData(
        "a69f73cca23a9ac5c8b567dc185a756e97c982164fe25859e0d1dcc1475c80a615b2123af1f5f94c11e3e9402c3ac558f500199d95b6d3e301758586281dcd26",
        "")]
    [InlineData(
        "36dde7d288a2166a651d51ec6ded9e70e72cf6b366293d6f513c75393c57d6f33b949879b9d5e7f7c21cd8c02ede75e74fc54ea15bd043b4df008533fc68ae69",
        "1234567890")]
    [InlineData(
        "69958b041bc72e9922e02cd4250953ee69d5f6e69f97d8def72b34effc0aea2bf5cfe03bd4ada0e271060593395656c1bf9eb68d1fc4cf146f90601152222df7",
        "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    [InlineData(
        "af328d17fa28753a3c9f5cb72e376b90440b96f0289e5703b729324a975ab384eda565fc92aaded143669900d761861687acdc0a5ffa358bd0571aaad80aca68",
        "abcdefghijklmnopqrstuvwxyz")]
    public override void TestSha3WithLiteralString(string expected, string literal)
    {
        base.TestSha3WithLiteralString(expected, literal);
    }

    [Theory(DisplayName = "Sha3Managed512 should produce the expected hash for the specified string template")]
    [InlineData(
        "3c3a876da14034ab60627c077bb98f7e120a2a5370212dffb3385a18d4f38859ed311d0a9d5141ce9cc5c66ee689b266a8aa18ace8282a0e0db596c90b0a7b87",
        "a", 1_000_000)]
    public override void TestSha3WithGeneratedString(string expected, string template, int iterations)
    {
        base.TestSha3WithGeneratedString(expected, template, iterations);
    }
}
