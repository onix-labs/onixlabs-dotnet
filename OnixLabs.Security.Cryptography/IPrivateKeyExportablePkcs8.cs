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

using System;
using System.Security.Cryptography;

namespace OnixLabs.Security.Cryptography;

/// <summary>
/// Defines a cryptographic private key that can be exported in PKCS #8 format.
/// </summary>
public interface IPrivateKeyExportablePkcs8
{
    /// <summary>
    /// Exports the cryptographic private key data in PKCS #8 format.
    /// </summary>
    /// <returns>Returns a new <see cref="T:Byte[]"/> instance containing the cryptographic private key data in PKCS #8 format.</returns>
    byte[] ExportPkcs8PrivateKey();

    /// <summary>
    /// Exports the cryptographic private key data in encrypted PKCS #8 format.
    /// </summary>
    /// <param name="password">The password to use for encryption.</param>
    /// <param name="parameters">The parameters required for password based encryption.</param>
    /// <returns>Returns a new <see cref="T:Byte[]"/> instance containing the cryptographic private key data in PKCS #8 format.</returns>
    byte[] ExportPkcs8PrivateKey(ReadOnlySpan<char> password, PbeParameters parameters);
}
