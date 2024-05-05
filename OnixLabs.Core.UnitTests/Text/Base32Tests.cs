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

using System.Text;
using OnixLabs.Core.Text;
using Xunit;

namespace OnixLabs.Core.UnitTests.Text;

public sealed class Base32Tests
{
    [Fact(DisplayName = "Base32 should not change when modifying the original byte array")]
    public void Base32ShouldNotChangeWhenModifyingOriginalByteArray()
    {
        // Given
        byte[] bytes = "ABCabc123".ToByteArray();
        Base32 candidate = new(bytes);
        const string expected = "IFBEGYLCMMYTEMY";

        // When
        bytes[0] = 0;
        string actual = candidate.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Base32 should not change when modifying the obtained byte array")]
    public void Base32ShouldNotChangeWhenModifyingObtainedByteArray()
    {
        // Given
        Base32 candidate = new("ABCabc123".ToByteArray());
        const string expected = "IFBEGYLCMMYTEMY";

        // When
        byte[] bytes = candidate.ToByteArray();
        bytes[0] = 0;
        string actual = candidate.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Base32 values should be identical")]
    public void Base32ValuesShouldBeIdentical()
    {
        // Given
        Base32 a = new([0, 255]);
        Base32 b = new([0, 255]);

        // Then
        Assert.Equal(a, b);
        Assert.Equal(a.GetHashCode(), b.GetHashCode());
        Assert.True(a.Equals(b));
        Assert.True(a == b);
        Assert.False(a != b);
    }

    [Fact(DisplayName = "Base32 values should not be identical")]
    public void Base32ValuesShouldNotBeIdentical()
    {
        // Given
        Base32 a = new([0, 255]);
        Base32 b = new([1, 127]);

        // Then
        Assert.NotEqual(a, b);
        Assert.NotEqual(a.GetHashCode(), b.GetHashCode());
        Assert.False(a.Equals(b));
        Assert.False(a == b);
        Assert.True(a != b);
    }

    [Theory(DisplayName = "Base32.Parse should produce the expected result")]
    [InlineData("", "")]
    [InlineData("IFBEGRCFIZDUQSKKJNGE2TSPKBIVEU2UKVLFOWCZLI","ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    [InlineData("MFRGGZDFMZTWQ2LKNNWG23TPOBYXE43UOV3HO6DZPI","abcdefghijklmnopqrstuvwxyz")]
    [InlineData("GAYTEMZUGU3DOOBZ","0123456789")]
    public void Base32ParseShouldProduceExpectedResult(string value, string expected)
    {
        // Given
        Base32 candidate = Base32.Parse(value);

        // When
        string actual = Encoding.UTF8.GetString(candidate.ToByteArray());

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base32.ToString should produce the expected result")]
    [InlineData("", "")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ","IFBEGRCFIZDUQSKKJNGE2TSPKBIVEU2UKVLFOWCZLI")]
    [InlineData("abcdefghijklmnopqrstuvwxyz","MFRGGZDFMZTWQ2LKNNWG23TPOBYXE43UOV3HO6DZPI")]
    [InlineData("0123456789","GAYTEMZUGU3DOOBZ")]
    public void Base32ToStringShouldProduceExpectedResult(string value, string expected)
    {
        // Given
        byte[] bytes = Encoding.UTF8.GetBytes(value);
        Base32 candidate = new(bytes);

        // When
        string actual = candidate.ToString();

        // Then
        Assert.Equal(expected, actual);
    }
}
