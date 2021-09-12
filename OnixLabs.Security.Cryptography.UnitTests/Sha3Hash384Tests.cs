// Copyright 2020-2021 ONIXLabs
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

namespace OnixLabs.Security.Cryptography.UnitTests
{
    public sealed class Sha3Managed384Tests : Sha3HashTestBase
    {
        protected override Sha3 HashAlgorithm => Sha3.CreateSha3Hash384();

        [Theory(DisplayName = "Sha3Managed384 should produce the expected hash for the specified string literal")]
        [InlineData(
            "0c63a75b845e4f7d01107d852e4c2485c51a50aaaa94fc61995e71bbee983a2ac3713831264adb47fb6bd1e058d5f004",
            ""
        )]
        [InlineData(
            "6fdddab7d670f202629531c1a51b32ca30696d0af4dd5b0fbb5f82c0aba5e505110455f37d7ef73950c2bb0495a38f56",
            "1234567890"
        )]
        [InlineData(
            "284da0df47fc9e75a4ef1248f69ca0d12a5d44508942e63b03b8c227510c2e1b43400009fcd36c0acc941679e5024a04",
            "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        )]
        [InlineData(
            "fed399d2217aaf4c717ad0c5102c15589e1c990cc2b9a5029056a7f7485888d6ab65db2370077a5cadb53fc9280d278f",
            "abcdefghijklmnopqrstuvwxyz"
        )]
        public override void TestSha3WithLiteralString(string expected, string literal)
        {
            base.TestSha3WithLiteralString(expected, literal);
        }

        [Theory(DisplayName = "Sha3Managed384 should produce a valid hash result for the given string template")]
        [InlineData("eee9e24d78c1855337983451df97c8ad9eedf256c6334f8e948d252d5e0e76847aa0774ddb90a842190d2c558b4b8340",
            "a", 1_000_000)]
        public override void TestSha3WithGeneratedString(string expected, string template, int iterations)
        {
            base.TestSha3WithGeneratedString(expected, template, iterations);
        }
    }
}
