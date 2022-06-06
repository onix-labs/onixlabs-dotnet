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

namespace OnixLabs.Security.Cryptography.UnitTests
{
    public sealed class Sha3ManagedShake256Tests : Sha3ShakeTestBase
    {
        [Theory(DisplayName = "Sha3ManagedShake256 should produce the expected hash for the specified string literal")]
        [InlineData("46b9dd2b", "", 4)]
        [InlineData("46b9dd2b0ba88d13", "", 8)]
        [InlineData("46b9dd2b0ba88d13233b3feb743eeb24", "", 16)]
        [InlineData("46b9dd2b0ba88d13233b3feb743eeb243fcd52ea62b81b82b50c27646ed5762f", "", 32)]
        [InlineData("cd65a4e5", "1234567890", 4)]
        [InlineData("cd65a4e553405b50", "1234567890", 8)]
        [InlineData("cd65a4e553405b50c2f37001ea81905f", "1234567890", 16)]
        [InlineData("cd65a4e553405b50c2f37001ea81905f36d650cc775fdad898b2e343644cb3db", "1234567890", 32)]
        [InlineData("fa8775b6", "ABCDEFGHIJKLMNOPQRSTUVWXYZ", 4)]
        [InlineData("fa8775b64bf3aaf1", "ABCDEFGHIJKLMNOPQRSTUVWXYZ", 8)]
        [InlineData("fa8775b64bf3aaf10d7f473c460f4d23", "ABCDEFGHIJKLMNOPQRSTUVWXYZ", 16)]
        [InlineData("fa8775b64bf3aaf10d7f473c460f4d2361f56ff34ae7267a", "ABCDEFGHIJKLMNOPQRSTUVWXYZ", 24)]
        [InlineData("b7b78b04", "abcdefghijklmnopqrstuvwxyz", 4)]
        [InlineData("b7b78b04a3dd30a2", "abcdefghijklmnopqrstuvwxyz", 8)]
        [InlineData("b7b78b04a3dd30a265c8886c33fda947", "abcdefghijklmnopqrstuvwxyz", 16)]
        [InlineData("b7b78b04a3dd30a265c8886c33fda94799853de5d3d10541", "abcdefghijklmnopqrstuvwxyz", 24)]
        public override void TestSha3WithLiteralString(string expected, string literal, int length)
        {
            HashAlgorithm = Sha3.CreateSha3Shake256(length);
            base.TestSha3WithLiteralString(expected, literal, length);
        }

        [Theory(DisplayName = "Sha3ManagedShake256 should produce the expected hash for the specified string template")]
        [InlineData("3578a7a4", "a", 1_000_000, 4)]
        [InlineData("3578a7a4ca913756", "a", 1_000_000, 8)]
        [InlineData("3578a7a4ca9137569cdf76ed617d31bb", "a", 1_000_000, 16)]
        [InlineData("3578a7a4ca9137569cdf76ed617d31bb994fca9c1bbf8b184013de8234dfd13a", "a", 1_000_000, 32)]
        public override void TestSha3WithGeneratedString(string expected, string template, int iterations, int length)
        {
            HashAlgorithm = Sha3.CreateSha3Shake256(length);
            base.TestSha3WithGeneratedString(expected, template, iterations, length);
        }
    }
}
