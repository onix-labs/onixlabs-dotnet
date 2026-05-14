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
using System.IO;
using OnixLabs.Core;

namespace OnixLabs.Security.Cryptography;

/// <summary>
/// Defines an EdDSA cryptographic public key, as specified in RFC 8032 (Ed25519, PureEdDSA).
/// </summary>
public interface IEddsaPublicKey : IPublicKeyImportable<EddsaPublicKey>, IPublicKeyExportable, IBinaryConvertible
{
    /// <summary>
    /// Determines whether the specified data was signed by the EdDSA cryptographic private key that corresponds to the current
    /// EdDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current EdDSA cryptographic public key.</param>
    /// <param name="data">The unsigned data to validate against the current EdDSA cryptographic public key.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the specified data was signed by the EdDSA cryptographic private key that corresponds to the
    /// current EdDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature; otherwise, <see langword="false"/>.
    /// </returns>
    bool IsDataValid(ReadOnlySpan<byte> signature, ReadOnlySpan<byte> data);

    /// <summary>
    /// Determines whether the specified data was signed by the EdDSA cryptographic private key that corresponds to the current
    /// EdDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current EdDSA cryptographic public key.</param>
    /// <param name="data">The unsigned data to validate against the current EdDSA cryptographic public key.</param>
    /// <param name="offset">The offset into the <see cref="ReadOnlySpan{T}"/> from which to begin using data.</param>
    /// <param name="count">The number of bytes in the <see cref="ReadOnlySpan{T}"/> to use as data.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the specified data was signed by the EdDSA cryptographic private key that corresponds to the
    /// current EdDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature; otherwise, <see langword="false"/>.
    /// </returns>
    bool IsDataValid(ReadOnlySpan<byte> signature, ReadOnlySpan<byte> data, int offset, int count);

    /// <summary>
    /// Determines whether the specified data was signed by the EdDSA cryptographic private key that corresponds to the current
    /// EdDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current EdDSA cryptographic public key.</param>
    /// <param name="data">The unsigned data to validate against the current EdDSA cryptographic public key.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the specified data was signed by the EdDSA cryptographic private key that corresponds to the
    /// current EdDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature; otherwise, <see langword="false"/>.
    /// </returns>
    bool IsDataValid(ReadOnlySpan<byte> signature, Stream data);

    /// <summary>
    /// Determines whether the specified data was signed by the EdDSA cryptographic private key that corresponds to the current
    /// EdDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current EdDSA cryptographic public key.</param>
    /// <param name="data">The unsigned data to validate against the current EdDSA cryptographic public key.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the specified data was signed by the EdDSA cryptographic private key that corresponds to the
    /// current EdDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature; otherwise, <see langword="false"/>.
    /// </returns>
    bool IsDataValid(ReadOnlySpan<byte> signature, IBinaryConvertible data);

    /// <summary>
    /// Determines whether the specified data was signed by the EdDSA cryptographic private key that corresponds to the current
    /// EdDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current EdDSA cryptographic public key.</param>
    /// <param name="data">The unsigned data to validate against the current EdDSA cryptographic public key.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the specified data was signed by the EdDSA cryptographic private key that corresponds to the
    /// current EdDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature; otherwise, <see langword="false"/>.
    /// </returns>
    bool IsDataValid(DigitalSignature signature, ReadOnlySpan<byte> data);

    /// <summary>
    /// Determines whether the specified data was signed by the EdDSA cryptographic private key that corresponds to the current
    /// EdDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current EdDSA cryptographic public key.</param>
    /// <param name="data">The unsigned data to validate against the current EdDSA cryptographic public key.</param>
    /// <param name="offset">The offset into the <see cref="ReadOnlySpan{T}"/> from which to begin using data.</param>
    /// <param name="count">The number of bytes in the <see cref="ReadOnlySpan{T}"/> to use as data.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the specified data was signed by the EdDSA cryptographic private key that corresponds to the
    /// current EdDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature; otherwise, <see langword="false"/>.
    /// </returns>
    bool IsDataValid(DigitalSignature signature, ReadOnlySpan<byte> data, int offset, int count);

    /// <summary>
    /// Determines whether the specified data was signed by the EdDSA cryptographic private key that corresponds to the current
    /// EdDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current EdDSA cryptographic public key.</param>
    /// <param name="data">The unsigned data to validate against the current EdDSA cryptographic public key.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the specified data was signed by the EdDSA cryptographic private key that corresponds to the
    /// current EdDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature; otherwise, <see langword="false"/>.
    /// </returns>
    bool IsDataValid(DigitalSignature signature, Stream data);

    /// <summary>
    /// Determines whether the specified data was signed by the EdDSA cryptographic private key that corresponds to the current
    /// EdDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current EdDSA cryptographic public key.</param>
    /// <param name="data">The unsigned data to validate against the current EdDSA cryptographic public key.</param>
    /// <returns>
    /// Returns <see langword="true"/> if the specified data was signed by the EdDSA cryptographic private key that corresponds to the
    /// current EdDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature; otherwise, <see langword="false"/>.
    /// </returns>
    bool IsDataValid(DigitalSignature signature, IBinaryConvertible data);

    /// <summary>
    /// Verifies that the specified data was signed by the EdDSA cryptographic private key that corresponds to the current
    /// EdDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current EdDSA cryptographic public key.</param>
    /// <param name="data">The unsigned data to validate against the current EdDSA cryptographic public key.</param>
    void VerifyData(ReadOnlySpan<byte> signature, ReadOnlySpan<byte> data);

    /// <summary>
    /// Verifies that the specified data was signed by the EdDSA cryptographic private key that corresponds to the current
    /// EdDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current EdDSA cryptographic public key.</param>
    /// <param name="data">The unsigned data to validate against the current EdDSA cryptographic public key.</param>
    /// <param name="offset">The offset into the <see cref="ReadOnlySpan{T}"/> from which to begin using data.</param>
    /// <param name="count">The number of bytes in the <see cref="ReadOnlySpan{T}"/> to use as data.</param>
    void VerifyData(ReadOnlySpan<byte> signature, ReadOnlySpan<byte> data, int offset, int count);

    /// <summary>
    /// Verifies that the specified data was signed by the EdDSA cryptographic private key that corresponds to the current
    /// EdDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current EdDSA cryptographic public key.</param>
    /// <param name="data">The unsigned data to validate against the current EdDSA cryptographic public key.</param>
    void VerifyData(ReadOnlySpan<byte> signature, Stream data);

    /// <summary>
    /// Verifies that the specified data was signed by the EdDSA cryptographic private key that corresponds to the current
    /// EdDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current EdDSA cryptographic public key.</param>
    /// <param name="data">The unsigned data to validate against the current EdDSA cryptographic public key.</param>
    void VerifyData(ReadOnlySpan<byte> signature, IBinaryConvertible data);

    /// <summary>
    /// Verifies that the specified data was signed by the EdDSA cryptographic private key that corresponds to the current
    /// EdDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current EdDSA cryptographic public key.</param>
    /// <param name="data">The unsigned data to validate against the current EdDSA cryptographic public key.</param>
    void VerifyData(DigitalSignature signature, ReadOnlySpan<byte> data);

    /// <summary>
    /// Verifies that the specified data was signed by the EdDSA cryptographic private key that corresponds to the current
    /// EdDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current EdDSA cryptographic public key.</param>
    /// <param name="data">The unsigned data to validate against the current EdDSA cryptographic public key.</param>
    /// <param name="offset">The offset into the <see cref="ReadOnlySpan{T}"/> from which to begin using data.</param>
    /// <param name="count">The number of bytes in the <see cref="ReadOnlySpan{T}"/> to use as data.</param>
    void VerifyData(DigitalSignature signature, ReadOnlySpan<byte> data, int offset, int count);

    /// <summary>
    /// Verifies that the specified data was signed by the EdDSA cryptographic private key that corresponds to the current
    /// EdDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current EdDSA cryptographic public key.</param>
    /// <param name="data">The unsigned data to validate against the current EdDSA cryptographic public key.</param>
    void VerifyData(DigitalSignature signature, Stream data);

    /// <summary>
    /// Verifies that the specified data was signed by the EdDSA cryptographic private key that corresponds to the current
    /// EdDSA cryptographic public key, and that the signed data matches the specified cryptographic digital signature.
    /// </summary>
    /// <param name="signature">The signature to validate against the current EdDSA cryptographic public key.</param>
    /// <param name="data">The unsigned data to validate against the current EdDSA cryptographic public key.</param>
    void VerifyData(DigitalSignature signature, IBinaryConvertible data);
}
