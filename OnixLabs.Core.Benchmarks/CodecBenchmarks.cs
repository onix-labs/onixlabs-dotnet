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

using System.Security.Cryptography;
using BenchmarkDotNet.Attributes;
using OnixLabs.Core.Text;

namespace OnixLabs.Core.Benchmarks;

/// <summary>
/// Represents benchmarks for encoding to and decoding from Base16, Base32, Base58 and Base64 with each type's default format provider.
/// </summary>
// Why: not sealed, because BenchmarkDotNet generates a class that derives from each benchmark class.
public class CodecBenchmarks
{
    private Base16 base16;
    private Base32 base32;
    private Base58 base58;
    private Base64 base64;
    private string base16Text = string.Empty;
    private string base32Text = string.Empty;
    private string base58Text = string.Empty;
    private string base64Text = string.Empty;

    /// <summary>
    /// Gets or sets the number of bytes to encode.
    /// </summary>
    /// <value>The number of bytes to encode; 32 is a typical hash or key, 1024 a typical payload.</value>
    [Params(32, 1024)]
    public int Size { get; set; }

    /// <summary>
    /// Creates random input of the configured size and its encoded forms.
    /// </summary>
    [GlobalSetup]
    public void Setup()
    {
        byte[] data = RandomNumberGenerator.GetBytes(Size);

        base16 = new Base16(data);
        base32 = new Base32(data);
        base58 = new Base58(data);
        base64 = new Base64(data);

        base16Text = base16.ToString();
        base32Text = base32.ToString();
        base58Text = base58.ToString();
        base64Text = base64.ToString();
    }

    /// <summary>
    /// Encodes the input as Base16.
    /// </summary>
    /// <returns>Returns the encoded string.</returns>
    [Benchmark]
    public string EncodeBase16() => base16.ToString();

    /// <summary>
    /// Decodes the input from Base16.
    /// </summary>
    /// <returns>Returns the decoded value.</returns>
    [Benchmark]
    public Base16 DecodeBase16() => Base16.Parse(base16Text);

    /// <summary>
    /// Encodes the input as Base32.
    /// </summary>
    /// <returns>Returns the encoded string.</returns>
    [Benchmark]
    public string EncodeBase32() => base32.ToString();

    /// <summary>
    /// Decodes the input from Base32.
    /// </summary>
    /// <returns>Returns the decoded value.</returns>
    [Benchmark]
    public Base32 DecodeBase32() => Base32.Parse(base32Text);

    /// <summary>
    /// Encodes the input as Base58.
    /// </summary>
    /// <returns>Returns the encoded string.</returns>
    [Benchmark]
    public string EncodeBase58() => base58.ToString();

    /// <summary>
    /// Decodes the input from Base58.
    /// </summary>
    /// <returns>Returns the decoded value.</returns>
    [Benchmark]
    public Base58 DecodeBase58() => Base58.Parse(base58Text);

    /// <summary>
    /// Encodes the input as Base64.
    /// </summary>
    /// <returns>Returns the encoded string.</returns>
    [Benchmark]
    public string EncodeBase64() => base64.ToString();

    /// <summary>
    /// Decodes the input from Base64.
    /// </summary>
    /// <returns>Returns the decoded value.</returns>
    [Benchmark]
    public Base64 DecodeBase64() => Base64.Parse(base64Text);
}
