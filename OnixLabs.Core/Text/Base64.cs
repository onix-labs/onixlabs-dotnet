// Copyright 2020-2022 ONIXLabs
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
/// Represents a Base-64 value.
/// </summary>
public readonly partial struct Base64
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Base64"/> struct.
    /// </summary>
    /// <param name="value">The underlying value.</param>
    private Base64(byte[] value)
    {
        Value = value;
    }

    /// <summary>
    /// Gets the underlying value.
    /// </summary>
    private byte[] Value { get; }
}
