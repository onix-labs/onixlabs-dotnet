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

using OnixLabs.Core.Text;
using Xunit;

namespace OnixLabs.Core.UnitTests;

public sealed class Base64Tests
{
    [Fact(DisplayName = "Identical Base64 values produce identical hash codes.")]
    public void IdenticalBase64ValuesProduceIdenticalHashCodes()
    {
        // Arrange
        Base64 a = Base64.FromString("abcdefghijklmnopqrstuvwxyz");
        Base64 b = Base64.FromString("abcdefghijklmnopqrstuvwxyz");

        // Act
        int hashCodeA = a.GetHashCode();
        int hashCodeB = b.GetHashCode();

        // Assert
        Assert.Equal(hashCodeA, hashCodeB);
    }

    [Theory(DisplayName = "Base64_FromString should produce the expected Base-64 value.")]
    [InlineData("MTIzNDU2Nzg5MA==", "1234567890")]
    [InlineData("QUJDREVGR0hJSktMTU5PUFFSU1RVVldYWVo=", "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    [InlineData("YWJjZGVmZ2hpamtsbW5vcHFyc3R1dnd4eXo=", "abcdefghijklmnopqrstuvwxyz")]
    public void Base64FromStringShouldProduceTheExpectedBase64Value(string expected, string value)
    {
        // Arrange
        Base64 candidate = Base64.FromString(value);

        // Act
        string actual = candidate.ToString();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base64_Parse should produce the expected plain text value.")]
    [InlineData("1234567890", "MTIzNDU2Nzg5MA==")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "QUJDREVGR0hJSktMTU5PUFFSU1RVVldYWVo=")]
    [InlineData("abcdefghijklmnopqrstuvwxyz", "YWJjZGVmZ2hpamtsbW5vcHFyc3R1dnd4eXo=")]
    public void Base64ParseShouldProduceTheExpectedPlainTextValue(string expected, string value)
    {
        // Arrange
        Base64 candidate = Base64.Parse(value);

        // Act
        string actual = candidate.ToPlainTextString();

        // Assert
        Assert.Equal(expected, actual);
    }
}
