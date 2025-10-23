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

public sealed class NamedHashTests
{
    [Fact(DisplayName = "NamedHash.ToString should produce the expected result")]
    public void NamedHashToStringShouldProduceExpectedResult()
    {
        // Given
        const string expected = "SHA256:9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08";
        Hash hash = Hash.Compute(SHA256.Create(), "test");
        NamedHash namedHash = hash.ToNamedHash(HashAlgorithmName.SHA256);

        // When
        string actual = namedHash.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "NamedHash.Parse should produce the expected result")]
    public void NamedHashParseShouldProduceExpectedResult()
    {
        // Given
        const string expected = "SHA256:9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08";

        // When
        NamedHash hash = NamedHash.Parse(expected);
        string actual = hash.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "NamedHash.Parse should throw FormatException")]
    public void NamedHashParseShouldThrowFormatException()
    {
        // Given
        const string expected = "SHA256:InvalidData";

        // When
        Exception exception = Assert.Throws<FormatException>(() => NamedHash.Parse(expected));

        // Then
        Assert.Equal("The input string 'SHA256:InvalidData' was not in a correct format.", exception.Message);
    }
}
