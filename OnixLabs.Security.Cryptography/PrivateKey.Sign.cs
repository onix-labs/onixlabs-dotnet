// Copyright 2020-2023 ONIXLabs
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

namespace OnixLabs.Security.Cryptography;

public abstract partial class PrivateKey
{
    /// <summary>
    /// Computes a <see cref="DigitalSignature"/> from the specified unsigned data.
    /// </summary>
    /// <param name="unsignedData">The unsigned data from which to compute a <see cref="DigitalSignature"/>.</param>
    /// <returns>Returns a <see cref="DigitalSignature"/> from the specified unsigned data.</returns>
    public abstract DigitalSignature SignData(byte[] unsignedData);

    /// <summary>
    /// Computes a <see cref="DigitalSignature"/> from the specified unsigned hash.
    /// </summary>
    /// <param name="unsignedHash">The unsigned hash from which to compute a <see cref="DigitalSignature"/>.</param>
    /// <returns>Returns a <see cref="DigitalSignature"/> from the specified unsigned hash.</returns>
    public abstract DigitalSignature SignHash(byte[] unsignedHash);

    /// <summary>
    /// Computes a <see cref="DigitalSignature"/> from the specified unsigned hash.
    /// </summary>
    /// <param name="unsignedHash">The unsigned hash from which to compute a <see cref="DigitalSignature"/>.</param>
    /// <returns>Returns a <see cref="DigitalSignature"/> from the specified unsigned hash.</returns>
    public DigitalSignature SignHash(Hash unsignedHash)
    {
        byte[] unsignedHashBytes = unsignedHash.ToByteArray();
        return SignHash(unsignedHashBytes);
    }
}
