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

public sealed class Base32CodecGeoHashTests
{
    [Theory(DisplayName = "Base32Codec.Encode should produce the expected result")]
    [InlineData("", "")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ","85146j258t3nhkbb9e64umkgb18p4nunbpc5fq2tc8")]
    [InlineData("abcdefghijklmnopqrstuvwxyz","d5j66t35dtmqhucbeeq6uvmgf1sr4wvnfpv7fy3tg8")]
    [InlineData("0123456789","60sm4dtn6nv3ff1t")]
    public void Base32CodecEncodeShouldProduceExpectedResult(string value, string expected)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base32;
        byte[] bytes = value.ToByteArray();

        // When
        string actual = codec.Encode(bytes, Base32FormatProvider.GeoHash);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "Base32Codec.Decode should produce the expected result")]
    [InlineData("", "")]
    [InlineData("85146j258t3nhkbb9e64umkgb18p4nunbpc5fq2tc8","ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    [InlineData("d5j66t35dtmqhucbeeq6uvmgf1sr4wvnfpv7fy3tg8","abcdefghijklmnopqrstuvwxyz")]
    [InlineData("60sm4dtn6nv3ff1t","0123456789")]
    public void Base32CodecDecodeShouldProduceExpectedResult(string value, string expected)
    {
        // Given
        IBaseCodec codec = IBaseCodec.Base32;

        // When
        byte[] bytes = codec.Decode(value, Base32FormatProvider.GeoHash);
        string actual = Encoding.UTF8.GetString(bytes);

        // Then
        Assert.Equal(expected, actual);
    }
}