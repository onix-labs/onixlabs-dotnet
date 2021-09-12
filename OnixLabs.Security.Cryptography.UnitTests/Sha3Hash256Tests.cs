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
    public sealed class Sha3Hash256Tests : Sha3HashTestBase
    {
        protected override Sha3 HashAlgorithm => Sha3.CreateSha3Hash256();

        [Theory(DisplayName = "Sha3Managed256 should produce the expected hash for the specified string literal")]
        [InlineData("a7ffc6f8bf1ed76651c14756a061d662f580ff4de43b49fa82d80a4b80f8434a", "")]
        [InlineData("01da8843e976913aa5c15a62d45f1c9267391dcbd0a76ad411919043f374a163", "1234567890")]
        [InlineData("738eeb2d4adf0d452456695011bb252bd4701a0ae78fdd3fc945a963bceb1702", "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
        [InlineData("7cab2dc765e21b241dbc1c255ce620b29f527c6d5e7f5f843e56288f0d707521", "abcdefghijklmnopqrstuvwxyz")]
        public override void TestSha3WithLiteralString(string expected, string literal)
        {
            base.TestSha3WithLiteralString(expected, literal);
        }

        [Theory(DisplayName = "Sha3Managed256 should produce a valid hash result for the given string template")]
        [InlineData("5c8875ae474a3634ba4fd55ec85bffd661f32aca75c6d699d0cdcb6c115891c1", "a", 1_000_000)]
        public override void TestSha3WithGeneratedString(string expected, string template, int iterations)
        {
            base.TestSha3WithGeneratedString(expected, template, iterations);
        }
    }
}
