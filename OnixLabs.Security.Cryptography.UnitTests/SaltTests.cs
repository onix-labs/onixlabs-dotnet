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

using OnixLabs.Core.Linq;

namespace OnixLabs.Security.Cryptography.UnitTests;

public sealed class SaltTests
{
    [Fact(DisplayName = "Salt should be constructable from bytes")]
    public void SaltShouldBeConstructableFromBytes()
    {
        // Given
        byte[] value = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15];
        const string expected = "000102030405060708090a0b0c0d0e0f";

        // When
        Salt candidate = new(value);
        string actual = candidate.ToString();

        // Then
        Assert.Equal(expected, actual);
        Assert.Equal(16, candidate.Length);
    }

    [Fact(DisplayName = "Salt value should not be modified when altering the original byte array")]
    public void SaltValueShouldNotBeModifiedWhenAlteringTheOriginalByteArray()
    {
        // Given
        byte[] value = [1, 2, 3, 4];
        Salt candidate = new(value);
        const string expected = "01020304";

        // When
        value[0] = 0;
        string actual = candidate.ToString();

        // Then
        Assert.Equal(expected, actual);
        Assert.Equal(4, candidate.Length);
    }

    [Fact(DisplayName = "Salt value should not be modified when altering the obtained byte array")]
    public void SaltValueShouldNotBeModifiedWhenAlteringTheObtainedByteArray()
    {
        // Given
        Salt candidate = new([1, 2, 3, 4]);
        const string expected = "01020304";

        // When
        candidate.AsReadOnlySpan().ToArray()[0] = 0;
        string actual = candidate.ToString();

        // Then
        Assert.Equal(expected, actual);
        Assert.Equal(4, candidate.Length);
    }

    [Fact(DisplayName = "Identical default salt values should be considered equal")]
    public void IdenticalDefaultSaltValuesShouldBeConsideredEqual()
    {
        // Given
        Salt left = new();
        Salt right = default;

        // Then
        Assert.Equal(left, right);
        Assert.Equal(left.GetHashCode(), right.GetHashCode());
        Assert.True(left.Equals(right));
        Assert.True(left == right);
        Assert.False(left != right);

        Assert.Equal(0, left.Length);
        Assert.Equal(0, right.Length);
    }

    [Fact(DisplayName = "Identical salt values should be considered equal")]
    public void IdenticalSaltValuesShouldBeConsideredEqual()
    {
        // Given
        Salt left = new([1, 2, 3, 4]);
        Salt right = new([1, 2, 3, 4]);

        // Then
        Assert.Equal(left, right);
        Assert.Equal(left.GetHashCode(), right.GetHashCode());
        Assert.True(left.Equals(right));
        Assert.True(left == right);
        Assert.False(left != right);

        Assert.Equal(4, left.Length);
        Assert.Equal(4, right.Length);
    }

    [Fact(DisplayName = "Different salt values should not be considered equal")]
    public void DifferentSaltValuesShouldNotBeConsideredEqual()
    {
        // Given
        Salt left = new([1, 2, 3, 4]);
        Salt right = new([5, 6, 7, 8]);

        // Then
        Assert.NotEqual(left, right);
        Assert.NotEqual(left.GetHashCode(), right.GetHashCode());
        Assert.False(left.Equals(right));
        Assert.False(left == right);
        Assert.True(left != right);

        Assert.Equal(4, left.Length);
        Assert.Equal(4, right.Length);
    }

    [Fact(DisplayName = "Identical salt values should produce identical hash codes")]
    public void IdenticalSaltValuesShouldProduceIdenticalSaltCodes()
    {
        // Given
        Salt left = new([1, 2, 3, 4]);
        Salt right = new([1, 2, 3, 4]);

        // When
        int leftHashCode = left.GetHashCode();
        int rightHashCode = right.GetHashCode();

        // Then
        Assert.Equal(leftHashCode, rightHashCode);

        Assert.Equal(4, left.Length);
        Assert.Equal(4, right.Length);
    }

    [Fact(DisplayName = "Different salt values should produce different hash codes")]
    public void DifferentSaltValuesShouldProduceDifferentSaltCodes()
    {
        // Given
        Salt left = new([1, 2, 3, 4]);
        Salt right = new([5, 6, 7, 8]);

        // When
        int leftHashCode = left.GetHashCode();
        int rightHashCode = right.GetHashCode();

        // Then
        Assert.NotEqual(leftHashCode, rightHashCode);

        Assert.Equal(4, left.Length);
        Assert.Equal(4, right.Length);
    }

    [Fact(DisplayName = "Salt.Create should produce a salt of the specified length")]
    public void SaltCreateShouldProduceASaltOfTheSpecifiedLength()
    {
        // Given / When
        const int expected = 32;
        Salt candidate = Salt.Create(expected);

        // Then
        Assert.Equal(expected, candidate.AsReadOnlySpan().Length);

        Assert.Equal(32, candidate.Length);
    }

    [Fact(DisplayName = "Salt.CreateNonZero should produce a salt of the specified length of non-zero bytes")]
    public void SaltCreateNonZeroShouldProduceASaltOfTheSpecifiedLengthOfNonZeroBytes()
    {
        // Given / When
        const int expected = 32;
        Salt candidate = Salt.CreateNonZero(expected);

        // Then
        Assert.Equal(expected, candidate.AsReadOnlySpan().Length);
        Assert.True(candidate.AsReadOnlySpan().ToArray().None(value => value is 0));

        Assert.Equal(32, candidate.Length);
    }
}
