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

public sealed class Base58CodecFlickrTests
{
    [Theory(DisplayName = "Base58Codec.Encode should produce the expected result")]
    [InlineData("", "")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ","2ZUfwsirsqj6erKTQGm2pdbKcMh1t46cMXzd")]
    [InlineData("abcdefghijklmnopqrstuvwxyz","3YXt3U1HFx8vKFTJj92EAipcC4byHHs1V25E")]
    [InlineData("0123456789","3H37nBFNNx8E1r")]
    public void Base58CodecEncodeShouldProduceExpectedResult(string value, string expected)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base58;
        byte[] bytes = value.ToByteArray();

        // When
        string actual = codec.Encode(bytes, Base58FormatProvider.Flickr);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base58Codec.Decode should produce the expected result")]
    [InlineData("", "")]
    [InlineData("2ZUfwsirsqj6erKTQGm2pdbKcMh1t46cMXzd","ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    [InlineData("3YXt3U1HFx8vKFTJj92EAipcC4byHHs1V25E","abcdefghijklmnopqrstuvwxyz")]
    [InlineData("3H37nBFNNx8E1r","0123456789")]
    public void Base58CodecDecodeShouldProduceExpectedResult(string value, string expected)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base58;

        // When
        byte[] bytes = codec.Decode(value, Base58FormatProvider.Flickr);
        string actual = Encoding.UTF8.GetString(bytes);

        // Then
        Assert.Equal(expected, actual);
    }
}
