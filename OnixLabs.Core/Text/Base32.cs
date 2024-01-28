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

namespace OnixLabs.Core.Text;

/// <summary>
/// Represents a Base-32 value.
/// </summary>
public readonly partial struct Base32 : IBaseRepresentation<Base32>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Base32"/> struct.
    /// </summary>
    /// <param name="value">The underlying value.</param>
    /// <param name="alphabet">The alphabet that will be used for Base-32 encoding and decoding operations.</param>
    /// <param name="padding">Determines whether padding should be applied for Base-32 encoding and decoding operations.</param>
    private Base32(byte[] value, Base32Alphabet alphabet, bool padding)
    {
        Value = value.Copy();
        Alphabet = alphabet;
        Padding = padding;
    }

    /// <summary>
    /// Gets the underlying value.
    /// </summary>
    private byte[] Value { get; }

    /// <summary>
    /// Gets the alphabet that will be used for Base-32 encoding and decoding operations.
    /// </summary>
    private Base32Alphabet Alphabet { get; }

    /// <summary>
    /// Gets a value that determines whether padding should be applied for Base-32 encoding and decoding operations.
    /// </summary>
    private bool Padding { get; }
}
