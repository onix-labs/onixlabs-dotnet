// Copyright © 2020 ONIXLabs
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

using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Xunit;

namespace OnixLabs.Security.Cryptography.UnitTests;

public abstract class Sha3ShakeTestBase
{
    protected Sha3 HashAlgorithm { get; set; }

    public virtual void TestSha3WithLiteralString(string expected, string literal, int length)
    {
        string actual = ComputeHash(HashAlgorithm, literal);
        Assert.Equal(expected, actual);
    }

    public virtual void TestSha3WithGeneratedString(string expected, string template, int iterations, int length)
    {
        string actual = ComputeHash(HashAlgorithm, string.Concat(Enumerable.Repeat(template, iterations)));
        Assert.Equal(expected, actual);
    }

    private static string ComputeHash(HashAlgorithm algorithm, string plainText)
    {
        byte[] plainTextBytes = Encoding.Default.GetBytes(plainText);
        byte[] hashedBytes = algorithm.ComputeHash(plainTextBytes);
        return Hash.Create(hashedBytes).ToString();
    }
}
