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
/// Represents a Base-16 (hexadecimal) value.
/// </summary>
public readonly partial struct Base16 : IBaseRepresentation<Base16>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Base16"/> struct.
    /// </summary>
    /// <param name="value">The underlying value.</param>
    private Base16(byte[] value)
    {
        Value = value.Copy();
    }

    /// <summary>
    /// Gets the underlying value.
    /// </summary>
    private byte[] Value { get; }
}
