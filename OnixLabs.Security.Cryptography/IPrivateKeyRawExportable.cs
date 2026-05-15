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

namespace OnixLabs.Security.Cryptography;

/// <summary>
/// Defines a cryptographic private key that can be exported in its raw binary form.
/// </summary>
public interface IPrivateKeyRawExportable
{
    /// <summary>
    /// Exports the cryptographic private key data.
    /// </summary>
    /// <returns>Returns a new <see cref="byte"/> array instance containing the cryptographic private key data.</returns>
    byte[] Export();
}
