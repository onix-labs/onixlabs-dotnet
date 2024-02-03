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

using System.Security.Cryptography;

namespace OnixLabs.Security.Cryptography;

public readonly partial struct DigitalSignature
{
    /// <summary>
    /// Determines whether this <see cref="DigitalSignature"/> is valid, given the specified unsigned data and public key.
    /// </summary>
    /// <param name="unsignedData">The unsigned data to validate.</param>
    /// <param name="key">The public key to validate.</param>
    /// <returns>Returns <see langword="true"/> if this <see cref="DigitalSignature"/> is valid; otherwise, <see langword="false"/>.</returns>
    public bool IsDataValid(byte[] unsignedData, PublicKey key)
    {
        return key.IsDataValid(this, unsignedData);
    }

    /// <summary>
    /// Determines whether this <see cref="DigitalSignature"/> is valid, given the specified unsigned hash and public key.
    /// </summary>
    /// <param name="unsignedHash">The unsigned data to validate.</param>
    /// <param name="key">The public key to validate.</param>
    /// <returns>Returns <see langword="true"/> if this <see cref="DigitalSignature"/> is valid; otherwise, <see langword="false"/>.</returns>
    public bool IsHashValid(byte[] unsignedHash, PublicKey key)
    {
        return key.IsHashValid(this, unsignedHash);
    }

    /// <summary>
    /// Determines whether this <see cref="DigitalSignature"/> is valid, given the specified unsigned hash and public key.
    /// </summary>
    /// <param name="unsignedHash">The unsigned data to validate.</param>
    /// <param name="key">The public key to validate.</param>
    /// <returns>Returns <see langword="true"/> if this <see cref="DigitalSignature"/> is valid; otherwise, <see langword="false"/>.</returns>
    public bool IsHashValid(Hash unsignedHash, PublicKey key)
    {
        byte[] unsignedHashBytes = unsignedHash.ToByteArray();
        return IsHashValid(unsignedHashBytes, key);
    }

    /// <summary>
    /// Verifies this <see cref="DigitalSignature"/>.
    /// </summary>
    /// <param name="unsignedData">The unsigned data to verify.</param>
    /// <param name="key">The public key to verify.</param>
    /// <exception cref="CryptographicException">If this <see cref="DigitalSignature"/> was not signed by the specified key.</exception>
    public void VerifyData(byte[] unsignedData, PublicKey key)
    {
        if (!IsDataValid(unsignedData, key))
        {
            throw new CryptographicException("The specified digital signature was not signed with this key.");
        }
    }

    /// <summary>
    /// Verifies this <see cref="DigitalSignature"/>.
    /// </summary>
    /// <param name="unsignedHash">The unsigned hash to verify.</param>
    /// <param name="key">The public key to verify.</param>
    /// <exception cref="CryptographicException">If this <see cref="DigitalSignature"/> was not signed by the specified key.</exception>
    public void VerifyHash(byte[] unsignedHash, PublicKey key)
    {
        if (!IsHashValid(unsignedHash, key))
        {
            throw new CryptographicException("The specified digital signature was not signed with this key.");
        }
    }

    /// <summary>
    /// Verifies this <see cref="DigitalSignature"/>.
    /// </summary>
    /// <param name="unsignedHash">The unsigned hash to verify.</param>
    /// <param name="key">The public key to verify.</param>
    /// <exception cref="CryptographicException">If this <see cref="DigitalSignature"/> was not signed by the specified key.</exception>
    public void VerifyHash(Hash unsignedHash, PublicKey key)
    {
        byte[] unsignedHashBytes = unsignedHash.ToByteArray();
        VerifyHash(unsignedHashBytes, key);
    }
}
