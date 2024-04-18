// Copyright 2020-2024 ONIXLabs
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

// ReSharper disable once InconsistentNaming
public sealed class IBaseRepresentationTests
{
    private const string PlainText = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    [Fact(DisplayName = "Base16.ToBase16 should produce the expected result")]
    public void Base16ToBase16ShouldProduceExpectedResult()
    {
        // Given
        IBaseRepresentation candidate = new Base16(PlainText);
        const string expected = "4142434445464748494a4b4c4d4e4f505152535455565758595a6162636465666768696a6b6c6d6e6f707172737475767778797a30313233343536373839";

        // When
        Base16 converted = candidate.ToBase16();
        string actual = converted.ToString();

        // Then
        Assert.Equal(expected, actual);
        Assert.Equal(PlainText, converted.ToPlainTextString());
    }

    [Fact(DisplayName = "Base16.ToBase32 should produce the expected result")]
    public void Base16ToBase32ShouldProduceExpectedResult()
    {
        // Given
        IBaseRepresentation candidate = new Base16(PlainText);
        const string expected = "IFBEGRCFIZDUQSKKJNGE2TSPKBIVEU2UKVLFOWCZLJQWEY3EMVTGO2DJNJVWY3LON5YHC4TTOR2XM53YPF5DAMJSGM2DKNRXHA4Q";

        // When
        Base32 converted = candidate.ToBase32();
        string actual = converted.ToString();

        // Then
        Assert.Equal(expected, actual);
        Assert.Equal(PlainText, converted.ToPlainTextString());
    }

    [Fact(DisplayName = "Base16.ToBase58 should produce the expected result")]
    public void Base16ToBase58ShouldProduceExpectedResult()
    {
        // Given
        IBaseRepresentation candidate = new Base16(PlainText);
        const string expected = "4tJBETbod9RcGRhGUXbdjU3t4jtbW8ySYiRVRn2XmqPVNZyexrFZ3S1mfsisKv2S2vgtSqPYk8WQy35D9dbbW";

        // When
        Base58 converted = candidate.ToBase58();
        string actual = converted.ToString();

        // Then
        Assert.Equal(expected, actual);
        Assert.Equal(PlainText, converted.ToPlainTextString());
    }

    [Fact(DisplayName = "Base16.ToBase64 should produce the expected result")]
    public void Base16ToBase64ShouldProduceExpectedResult()
    {
        // Given
        IBaseRepresentation candidate = new Base16(PlainText);
        const string expected = "QUJDREVGR0hJSktMTU5PUFFSU1RVVldYWVphYmNkZWZnaGlqa2xtbm9wcXJzdHV2d3h5ejAxMjM0NTY3ODk=";

        // When
        Base64 converted = candidate.ToBase64();
        string actual = converted.ToString();

        // Then
        Assert.Equal(expected, actual);
        Assert.Equal(PlainText, converted.ToPlainTextString());
    }

    [Fact(DisplayName = "Base32.ToBase16 should produce the expected result")]
    public void Base32ToBase16ShouldProduceExpectedResult()
    {
        // Given
        IBaseRepresentation candidate = new Base32(PlainText);
        const string expected = "4142434445464748494a4b4c4d4e4f505152535455565758595a6162636465666768696a6b6c6d6e6f707172737475767778797a30313233343536373839";

        // When
        Base16 converted = candidate.ToBase16();
        string actual = converted.ToString();

        // Then
        Assert.Equal(expected, actual);
        Assert.Equal(PlainText, converted.ToPlainTextString());
    }

    [Fact(DisplayName = "Base32.ToBase32 should produce the expected result")]
    public void Base32ToBase32ShouldProduceExpectedResult()
    {
        // Given
        IBaseRepresentation candidate = new Base32(PlainText);
        const string expected = "IFBEGRCFIZDUQSKKJNGE2TSPKBIVEU2UKVLFOWCZLJQWEY3EMVTGO2DJNJVWY3LON5YHC4TTOR2XM53YPF5DAMJSGM2DKNRXHA4Q";

        // When
        Base32 converted = candidate.ToBase32();
        string actual = converted.ToString();

        // Then
        Assert.Equal(expected, actual);
        Assert.Equal(PlainText, converted.ToPlainTextString());
    }

    [Fact(DisplayName = "Base32.ToBase58 should produce the expected result")]
    public void Base32ToBase58ShouldProduceExpectedResult()
    {
        // Given
        IBaseRepresentation candidate = new Base32(PlainText);
        const string expected = "4tJBETbod9RcGRhGUXbdjU3t4jtbW8ySYiRVRn2XmqPVNZyexrFZ3S1mfsisKv2S2vgtSqPYk8WQy35D9dbbW";

        // When
        Base58 converted = candidate.ToBase58();
        string actual = converted.ToString();

        // Then
        Assert.Equal(expected, actual);
        Assert.Equal(PlainText, converted.ToPlainTextString());
    }

    [Fact(DisplayName = "Base32.ToBase64 should produce the expected result")]
    public void Base32ToBase64ShouldProduceExpectedResult()
    {
        // Given
        IBaseRepresentation candidate = new Base32(PlainText);
        const string expected = "QUJDREVGR0hJSktMTU5PUFFSU1RVVldYWVphYmNkZWZnaGlqa2xtbm9wcXJzdHV2d3h5ejAxMjM0NTY3ODk=";

        // When
        Base64 converted = candidate.ToBase64();
        string actual = converted.ToString();

        // Then
        Assert.Equal(expected, actual);
        Assert.Equal(PlainText, converted.ToPlainTextString());
    }

    [Fact(DisplayName = "Base58.ToBase16 should produce the expected result")]
    public void Base58ToBase16ShouldProduceExpectedResult()
    {
        // Given
        IBaseRepresentation candidate = new Base58(PlainText);
        const string expected = "4142434445464748494a4b4c4d4e4f505152535455565758595a6162636465666768696a6b6c6d6e6f707172737475767778797a30313233343536373839";

        // When
        Base16 converted = candidate.ToBase16();
        string actual = converted.ToString();

        // Then
        Assert.Equal(expected, actual);
        Assert.Equal(PlainText, converted.ToPlainTextString());
    }

    [Fact(DisplayName = "Base58.ToBase32 should produce the expected result")]
    public void Base58ToBase32ShouldProduceExpectedResult()
    {
        // Given
        IBaseRepresentation candidate = new Base58(PlainText);
        const string expected = "IFBEGRCFIZDUQSKKJNGE2TSPKBIVEU2UKVLFOWCZLJQWEY3EMVTGO2DJNJVWY3LON5YHC4TTOR2XM53YPF5DAMJSGM2DKNRXHA4Q";

        // When
        Base32 converted = candidate.ToBase32();
        string actual = converted.ToString();

        // Then
        Assert.Equal(expected, actual);
        Assert.Equal(PlainText, converted.ToPlainTextString());
    }

    [Fact(DisplayName = "Base58.ToBase58 should produce the expected result")]
    public void Base58ToBase58ShouldProduceExpectedResult()
    {
        // Given
        IBaseRepresentation candidate = new Base58(PlainText);
        const string expected = "4tJBETbod9RcGRhGUXbdjU3t4jtbW8ySYiRVRn2XmqPVNZyexrFZ3S1mfsisKv2S2vgtSqPYk8WQy35D9dbbW";

        // When
        Base58 converted = candidate.ToBase58();
        string actual = converted.ToString();

        // Then
        Assert.Equal(expected, actual);
        Assert.Equal(PlainText, converted.ToPlainTextString());
    }

    [Fact(DisplayName = "Base58.ToBase64 should produce the expected result")]
    public void Base58ToBase64ShouldProduceExpectedResult()
    {
        // Given
        IBaseRepresentation candidate = new Base58(PlainText);
        const string expected = "QUJDREVGR0hJSktMTU5PUFFSU1RVVldYWVphYmNkZWZnaGlqa2xtbm9wcXJzdHV2d3h5ejAxMjM0NTY3ODk=";

        // When
        Base64 converted = candidate.ToBase64();
        string actual = converted.ToString();

        // Then
        Assert.Equal(expected, actual);
        Assert.Equal(PlainText, converted.ToPlainTextString());
    }

    [Fact(DisplayName = "Base64.ToBase16 should produce the expected result")]
    public void Base64ToBase16ShouldProduceExpectedResult()
    {
        // Given
        IBaseRepresentation candidate = new Base64(PlainText);
        const string expected = "4142434445464748494a4b4c4d4e4f505152535455565758595a6162636465666768696a6b6c6d6e6f707172737475767778797a30313233343536373839";

        // When
        Base16 converted = candidate.ToBase16();
        string actual = converted.ToString();

        // Then
        Assert.Equal(expected, actual);
        Assert.Equal(PlainText, converted.ToPlainTextString());
    }

    [Fact(DisplayName = "Base64.ToBase32 should produce the expected result")]
    public void Base64ToBase32ShouldProduceExpectedResult()
    {
        // Given
        IBaseRepresentation candidate = new Base64(PlainText);
        const string expected = "IFBEGRCFIZDUQSKKJNGE2TSPKBIVEU2UKVLFOWCZLJQWEY3EMVTGO2DJNJVWY3LON5YHC4TTOR2XM53YPF5DAMJSGM2DKNRXHA4Q";

        // When
        Base32 converted = candidate.ToBase32();
        string actual = converted.ToString();

        // Then
        Assert.Equal(expected, actual);
        Assert.Equal(PlainText, converted.ToPlainTextString());
    }

    [Fact(DisplayName = "Base64.ToBase58 should produce the expected result")]
    public void Base64ToBase58ShouldProduceExpectedResult()
    {
        // Given
        IBaseRepresentation candidate = new Base64(PlainText);
        const string expected = "4tJBETbod9RcGRhGUXbdjU3t4jtbW8ySYiRVRn2XmqPVNZyexrFZ3S1mfsisKv2S2vgtSqPYk8WQy35D9dbbW";

        // When
        Base58 converted = candidate.ToBase58();
        string actual = converted.ToString();

        // Then
        Assert.Equal(expected, actual);
        Assert.Equal(PlainText, converted.ToPlainTextString());
    }

    [Fact(DisplayName = "Base64.ToBase64 should produce the expected result")]
    public void Base64ToBase64ShouldProduceExpectedResult()
    {
        // Given
        IBaseRepresentation candidate = new Base64(PlainText);
        const string expected = "QUJDREVGR0hJSktMTU5PUFFSU1RVVldYWVphYmNkZWZnaGlqa2xtbm9wcXJzdHV2d3h5ejAxMjM0NTY3ODk=";

        // When
        Base64 converted = candidate.ToBase64();
        string actual = converted.ToString();

        // Then
        Assert.Equal(expected, actual);
        Assert.Equal(PlainText, converted.ToPlainTextString());
    }
}
