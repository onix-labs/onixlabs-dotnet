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

namespace OnixLabs.Security.Cryptography;

/// <summary>
/// Defines a cryptographic private key that can export a cryptographic public key component.
/// </summary>
/// <typeparam name="T">The underlying type of <see cref="PublicKey"/> that the cryptographic private key exports.</typeparam>
public interface IPublicKeyExportable<out T> where T : PublicKey
{
    /// <summary>
    /// Gets the cryptographic public key component from the current cryptographic private key.
    /// </summary>
    /// <returns>Returns the cryptographic public key component from the current cryptographic private key.</returns>
    T GetPublicKey();
}
