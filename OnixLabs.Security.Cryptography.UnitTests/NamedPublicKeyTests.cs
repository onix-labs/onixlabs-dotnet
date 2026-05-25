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

namespace OnixLabs.Security.Cryptography.UnitTests;

public sealed class NamedPublicKeyTests
{
    [Fact(DisplayName = "NamedPublicKey constructor should throw ArgumentNullException for a null public key")]
    public void NamedPublicKeyConstructorShouldThrowForNullPublicKey()
    {
        // When / Then
        Assert.Throws<ArgumentNullException>(() => new NamedPublicKey(null!, "ECDSA"));
    }

    [Fact(DisplayName = "Default NamedPublicKey should return an empty span without throwing")]
    public void DefaultNamedPublicKeyShouldReturnAnEmptySpan()
    {
        // Given
        NamedPublicKey candidate = default;

        // When
        ReadOnlySpan<byte> actual = candidate.AsReadOnlySpan();

        // Then
        Assert.True(actual.IsEmpty);
    }

    [Fact(DisplayName = "Default NamedPublicKey should produce a string without throwing")]
    public void DefaultNamedPublicKeyShouldProduceAStringWithoutThrowing()
    {
        // Given
        NamedPublicKey candidate = default;

        // When
        string actual = candidate.ToString();

        // Then
        Assert.Equal(":", actual);
    }
}
