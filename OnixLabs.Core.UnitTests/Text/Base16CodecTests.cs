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

public sealed class Base16CodecTests
{
    [Theory(DisplayName = "Base16Codec.Encode should produce the expected result")]
    [InlineData("", "")]
    [InlineData("0123456789", "30313233343536373839")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "4142434445464748494A4B4C4D4E4F505152535455565758595A")]
    [InlineData("abcdefghijklmnopqrstuvwxyz", "6162636465666768696A6B6C6D6E6F707172737475767778797A")]
    public void Base16CodecEncodeShouldProduceExpectedResult(string value, string expected)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base16;
        byte[] bytes = value.ToByteArray();

        // When
        string actual = codec.Encode(bytes);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base16Codec.Decode should produce the expected result")]
    [InlineData("", "")]
    [InlineData("30313233343536373839", "0123456789")]
    [InlineData("4142434445464748494A4B4C4D4E4F505152535455565758595A", "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    [InlineData("6162636465666768696A6B6C6D6E6F707172737475767778797A", "abcdefghijklmnopqrstuvwxyz")]
    public void Base16CodecDecodeShouldProduceExpectedResult(string value, string expected)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base16;

        // When
        byte[] bytes = codec.Decode(value);
        string actual = Encoding.UTF8.GetString(bytes);

        // Then
        Assert.Equal(expected, actual);
    }
}
