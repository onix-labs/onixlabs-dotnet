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

public sealed class Base32CodecPaddedBase32HexTests
{
    [Theory(DisplayName = "Base32Codec.Encode should produce the expected result")]
    [InlineData("", "")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ","85146H258P3KGIAA9D64QJIFA18L4KQKALB5EM2PB8======")]
    [InlineData("abcdefghijklmnopqrstuvwxyz","C5H66P35CPJMGQBADDM6QRJFE1ON4SRKELR7EU3PF8======")]
    [InlineData("0123456789","60OJ4CPK6KR3EE1P")]
    public void Base32CodecEncodeShouldProduceExpectedResult(string value, string expected)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base32;
        byte[] bytes = value.ToByteArray();

        // When
        string actual = codec.Encode(bytes, Base32FormatProvider.PaddedBase32Hex);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base32Codec.Decode should produce the expected result")]
    [InlineData("", "")]
    [InlineData("85146H258P3KGIAA9D64QJIFA18L4KQKALB5EM2PB8======","ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    [InlineData("C5H66P35CPJMGQBADDM6QRJFE1ON4SRKELR7EU3PF8======","abcdefghijklmnopqrstuvwxyz")]
    [InlineData("60OJ4CPK6KR3EE1P","0123456789")]
    public void Base32CodecDecodeShouldProduceExpectedResult(string value, string expected)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base32;

        // When
        byte[] bytes = codec.Decode(value, Base32FormatProvider.PaddedBase32Hex);
        string actual = Encoding.UTF8.GetString(bytes);

        // Then
        Assert.Equal(expected, actual);
    }
}
