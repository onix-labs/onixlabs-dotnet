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

public abstract partial class PublicKey
{
    /// <summary>
    /// Determines whether the specified <see cref="DigitalSignature"/> was signed by the private component of this public key.
    /// </summary>
    /// <param name="signature">The <see cref="DigitalSignature"/> to validate.</param>
    /// <param name="unsignedData">The unsigned data to validate.</param>
    /// <returns>Returns <see langword="true"/> if the specified <see cref="DigitalSignature"/> was signed by the private component of this public key; otherwise, <see langword="false"/>.</returns>
    public abstract bool IsDataValid(DigitalSignature signature, byte[] unsignedData);

    /// <summary>
    /// Determines whether the specified <see cref="DigitalSignature"/> was signed by the private component of this public key.
    /// </summary>
    /// <param name="signature">The <see cref="DigitalSignature"/> to validate.</param>
    /// <param name="unsignedHash">The unsigned hash to validate.</param>
    /// <returns>Returns <see langword="true"/> if the specified <see cref="DigitalSignature"/> was signed by the private component of this public key; otherwise, <see langword="false"/>.</returns>
    public abstract bool IsHashValid(DigitalSignature signature, byte[] unsignedHash);

    /// <summary>
    /// Determines whether the specified <see cref="DigitalSignature"/> was signed by the private component of this public key.
    /// </summary>
    /// <param name="signature">The <see cref="DigitalSignature"/> to validate.</param>
    /// <param name="unsignedHash">The unsigned hash to validate.</param>
    /// <returns>Returns <see langword="true"/> if the specified <see cref="DigitalSignature"/> was signed by the private component of this public key; otherwise, <see langword="false"/>.</returns>
    public bool IsHashValid(DigitalSignature signature, Hash unsignedHash)
    {
        byte[] unsignedHashBytes = unsignedHash.ToByteArray();
        return IsHashValid(signature, unsignedHashBytes);
    }

    /// <summary>
    /// Verifies whether the specified <see cref="DigitalSignature"/> was signed by the private component of this public key.
    /// </summary>
    /// <param name="signature">The <see cref="DigitalSignature"/> to verify.</param>
    /// <param name="unsignedData">he unsigned data to verify.</param>
    /// <exception cref="CryptographicException">If the specified <see cref="DigitalSignature"/> was not signed by the private component of this public key.</exception>
    public void VerifyData(DigitalSignature signature, byte[] unsignedData)
    {
        if (!IsDataValid(signature, unsignedData))
        {
            throw new CryptographicException("The specified digital signature was not signed with this key.");
        }
    }

    /// <summary>
    /// Verifies whether the specified <see cref="DigitalSignature"/> was signed by the private component of this public key.
    /// </summary>
    /// <param name="signature">The <see cref="DigitalSignature"/> to verify.</param>
    /// <param name="unsignedHash">he unsigned hash to verify.</param>
    /// <exception cref="CryptographicException">If the specified <see cref="DigitalSignature"/> was not signed by the private component of this public key.</exception>
    public void VerifyHash(DigitalSignature signature, byte[] unsignedHash)
    {
        if (!IsHashValid(signature, unsignedHash))
        {
            throw new CryptographicException("The specified digital signature was not signed with this key.");
        }
    }

    /// <summary>
    /// Verifies whether the specified <see cref="DigitalSignature"/> was signed by the private component of this public key.
    /// </summary>
    /// <param name="signature">The <see cref="DigitalSignature"/> to verify.</param>
    /// <param name="unsignedHash">he unsigned hash to verify.</param>
    /// <exception cref="CryptographicException">If the specified <see cref="DigitalSignature"/> was not signed by the private component of this public key.</exception>
    public void VerifyHash(DigitalSignature signature, Hash unsignedHash)
    {
        byte[] unsignedHashBytes = unsignedHash.ToByteArray();
        VerifyHash(signature, unsignedHashBytes);
    }
}
