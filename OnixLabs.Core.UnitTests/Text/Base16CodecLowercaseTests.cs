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

public sealed class Base16CodecLowercaseTests
{
    [Theory(DisplayName = "Base16Codec.Encode should produce the expected result")]
    [InlineData("", "")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ","4142434445464748494a4b4c4d4e4f505152535455565758595a")]
    [InlineData("abcdefghijklmnopqrstuvwxyz","6162636465666768696a6b6c6d6e6f707172737475767778797a")]
    [InlineData("0123456789","30313233343536373839")]
    public void Base16CodecEncodeShouldProduceExpectedResult(string value, string expected)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base16;
        byte[] bytes = value.ToByteArray();

        // When
        string actual = codec.Encode(bytes, Base16FormatProvider.Lowercase);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base16Codec.Decode should produce the expected result")]
    [InlineData("", "")]
    [InlineData("4142434445464748494a4b4c4d4e4f505152535455565758595a","ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    [InlineData("6162636465666768696a6b6c6d6e6f707172737475767778797a","abcdefghijklmnopqrstuvwxyz")]
    [InlineData("30313233343536373839","0123456789")]
    public void Base16CodecDecodeShouldProduceExpectedResult(string value, string expected)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base16;

        // When
        byte[] bytes = codec.Decode(value, Base16FormatProvider.Lowercase);
        string actual = Encoding.UTF8.GetString(bytes);

        // Then
        Assert.Equal(expected, actual);
    }
}
