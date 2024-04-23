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

namespace OnixLabs.Core;

/// <summary>
/// Defines a type that is convertible to a binary representation.
/// </summary>
public interface IBinaryConvertible
{
    /// <summary>
    /// Gets the underlying <see cref="byte"/> array represented by the current <see typeparam="IBinaryPrimitive"/> value.
    /// </summary>
    /// <returns>Returns the underlying <see cref="byte"/> array represented by the current <see typeparam="IBinaryPrimitive"/> value.</returns>
    public byte[] ToByteArray();

    /// <summary>
    /// Converts the current <see cref="IBinaryConvertible"/> to a <see cref="Base16"/> value.
    /// </summary>
    /// <returns>Returns the current <see cref="IBinaryConvertible"/> to a <see cref="Base16"/> value.</returns>
    public Base16 ToBase16() => new(ToByteArray());

    /// <summary>
    /// Converts the current <see cref="IBinaryConvertible"/> to a <see cref="Base32"/> value.
    /// </summary>
    /// <returns>Returns the current <see cref="IBinaryConvertible"/> to a <see cref="Base32"/> value.</returns>
    public Base32 ToBase32() => new(ToByteArray());

    /// <summary>
    /// Converts the current <see cref="IBinaryConvertible"/> to a <see cref="Base58"/> value.
    /// </summary>
    /// <returns>Returns the current <see cref="IBinaryConvertible"/> to a <see cref="Base58"/> value.</returns>
    public Base58 ToBase58() => new(ToByteArray());

    /// <summary>
    /// Converts the current <see cref="IBinaryConvertible"/> to a <see cref="Base64"/> value.
    /// </summary>
    /// <returns>Returns the current <see cref="IBinaryConvertible"/> to a <see cref="Base64"/> value.</returns>
    public Base64 ToBase64() => new(ToByteArray());
}
