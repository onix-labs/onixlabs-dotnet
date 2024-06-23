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
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OnixLabs.Security.Cryptography.UnitTests;

public sealed class HashAlgorithmExtensionTests
{
    [Fact(DisplayName = "HashAlgorithm.ComputeHash should produce the expected result with two rounds")]
    public void HashAlgorithmComputeHashShouldProduceExpectedResultWithTwoRounds()
    {
        // Given
        using HashAlgorithm algorithm = SHA256.Create();
        const string expected = "efaaeb3b1d1d85e8587ef0527ca43b9575ce8149ba1ee41583d3d19bd130daf8";

        // When
        byte[] bytes = algorithm.ComputeHash("abc123", rounds: 2);
        string actual = Convert.ToHexString(bytes).ToLower();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "HashAlgorithm.ComputeHashAsync should produce the expected result with two rounds")]
    public async Task HashAlgorithmComputeHashAsyncShouldProduceExpectedResultWithTwoRounds()
    {
        // Given
        using HashAlgorithm algorithm = SHA256.Create();
        Stream data = new MemoryStream("abc123"u8.ToArray());
        const string expected = "efaaeb3b1d1d85e8587ef0527ca43b9575ce8149ba1ee41583d3d19bd130daf8";

        // When
        byte[] bytes = await algorithm.ComputeHashAsync(data, 2);
        string actual = Convert.ToHexString(bytes).ToLower();

        // Then
        Assert.Equal(expected, actual);
    }
}
