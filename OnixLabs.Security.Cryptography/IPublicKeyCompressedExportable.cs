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

using System.Security.Cryptography;

namespace OnixLabs.Security.Cryptography;

/// <summary>
/// Defines an elliptic curve public key that can be exported as a SEC 1 compressed point.
/// </summary>
public interface IPublicKeyCompressedExportable
{
    /// <summary>
    /// Exports the public key as a SEC 1 v2 §2.3.3 compressed point: a <c>0x02</c> or <c>0x03</c> prefix giving the parity of the y-coordinate,
    /// followed by the big-endian x-coordinate.
    /// </summary>
    /// <returns>Returns a new <see cref="byte"/> array containing the compressed point.</returns>
    /// <exception cref="CryptographicException">Thrown when the key is not on the P-256, P-384 or P-521 curve.</exception>
    byte[] ExportCompressed();
}
