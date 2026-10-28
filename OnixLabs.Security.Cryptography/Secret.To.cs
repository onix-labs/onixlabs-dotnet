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

using System;
using System.Security.Cryptography;
using System.Text;
using OnixLabs.Core.Text;

namespace OnixLabs.Security.Cryptography;

public readonly partial struct Secret
{
    /// <summary>
    /// The largest secret length, in bytes, that formatting decrypts into a stack buffer rather than a heap buffer.
    /// Either buffer is zeroed after use.
    /// </summary>
    private const int MaxStackValueLength = 256;

    /// <summary>
    /// Returns a <see cref="string"/> that represents the current object.
    /// </summary>
    /// <param name="encoding">The encoding that will be used to determine the format of the string.</param>
    /// <returns>Returns a <see cref="string"/> that represents the current object.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the current <see cref="Secret"/> has been disposed.</exception>
    public string ToString(Encoding encoding)
    {
        Span<byte> value = Length <= MaxStackValueLength ? stackalloc byte[MaxStackValueLength] : new byte[Length];
        value = value[..Length];

        try
        {
            CopyTo(value);
            return encoding.GetString(value);
        }
        finally
        {
            CryptographicOperations.ZeroMemory(value);
        }
    }

    /// <inheritdoc/>
    /// <exception cref="ObjectDisposedException">Thrown when the current <see cref="Secret"/> has been disposed.</exception>
    public string ToString(IFormatProvider provider)
    {
        Span<byte> value = Length <= MaxStackValueLength ? stackalloc byte[MaxStackValueLength] : new byte[Length];
        value = value[..Length];

        try
        {
            CopyTo(value);
            return IBaseCodec.GetString(value, provider);
        }
        finally
        {
            CryptographicOperations.ZeroMemory(value);
        }
    }

    /// <inheritdoc/>
    /// <exception cref="ObjectDisposedException">Thrown when the current <see cref="Secret"/> has been disposed.</exception>
    public override string ToString() => ToString(Base16FormatProvider.Invariant);
}
