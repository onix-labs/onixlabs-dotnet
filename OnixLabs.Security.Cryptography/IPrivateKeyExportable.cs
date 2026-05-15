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
/// Defines a cryptographic private key that can be exported in raw binary, raw RFC 7468 PEM,
/// PKCS #8 binary, and PKCS #8 RFC 7468 PEM forms.
/// </summary>
public interface IPrivateKeyExportable :
    IPrivateKeyRawExportable,
    IPrivateKeyRawPemExportable,
    IPrivateKeyPkcs8Exportable,
    IPrivateKeyPkcs8PemExportable;
