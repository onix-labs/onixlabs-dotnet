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
using OnixLabs.Core;

namespace OnixLabs.Security.Cryptography;

public abstract partial class PrivateKey
{
    /// <summary>
    /// Gets the underlying <see cref="T:Byte[]"/> representation of the current <see cref="PrivateKey"/> instance.
    /// </summary>
    /// <returns>Return the underlying <see cref="T:Byte[]"/> representation of the current <see cref="PrivateKey"/> instance.</returns>
    public byte[] ToByteArray()
    {
        return KeyData.Copy();
    }

    /// <summary>
    /// Returns a <see cref="String"/> that represents the current object.
    /// </summary>
    /// <returns>Returns <see cref="String"/> that represents the current object.</returns>
    public override string ToString()
    {
        return Convert.ToHexString(KeyData).ToLower();
    }
}
