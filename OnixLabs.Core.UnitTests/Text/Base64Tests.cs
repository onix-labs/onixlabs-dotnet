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

using OnixLabs.Core.Text;
using Xunit;

namespace OnixLabs.Core.UnitTests.Text;

public sealed class Base64Tests
{
    [Fact(DisplayName = "Base64 values should be identical")]
    public void Base64ValuesShouldBeIdentical()
    {
        // Given
        Base64 a = Base64.Create("abcdefghijklmnopqrstuvwxyz");
        Base64 b = Base64.Create("abcdefghijklmnopqrstuvwxyz");

        // When
        int hashCodeA = a.GetHashCode();
        int hashCodeB = b.GetHashCode();

        // Then
        Assert.Equal(hashCodeA, hashCodeB);
    }

    [Theory(DisplayName = "Base64.Create should produce the expected Base-64 value")]
    [InlineData("MTIzNDU2Nzg5MA==", "1234567890")]
    [InlineData("QUJDREVGR0hJSktMTU5PUFFSU1RVVldYWVo=", "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    [InlineData("YWJjZGVmZ2hpamtsbW5vcHFyc3R1dnd4eXo=", "abcdefghijklmnopqrstuvwxyz")]
    public void CreateShouldProduceExpectedResult(string expected, string value)
    {
        // Given
        Base64 candidate = Base64.Create(value);

        // When
        string actual = candidate.ToString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base64.Parse should produce the expected plain text value")]
    [InlineData("1234567890", "MTIzNDU2Nzg5MA==")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "QUJDREVGR0hJSktMTU5PUFFSU1RVVldYWVo=")]
    [InlineData("abcdefghijklmnopqrstuvwxyz", "YWJjZGVmZ2hpamtsbW5vcHFyc3R1dnd4eXo=")]
    public void ParseShouldProduceExpectedResult(string expected, string value)
    {
        // Given
        Base64 candidate = Base64.Parse(value);

        // When
        string actual = candidate.ToPlainTextString();

        // Then
        Assert.Equal(expected, actual);
    }
}
