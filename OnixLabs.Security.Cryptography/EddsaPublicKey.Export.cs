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
using System.Security.Cryptography;

namespace OnixLabs.Security.Cryptography;

public sealed partial class EddsaPublicKey
{
    /// <summary>
    /// The RFC 7468 PEM label used for SubjectPublicKeyInfo-encoded Ed25519 public keys.
    /// </summary>
    private const string PublicKeyLabel = "PUBLIC KEY";

    /// <inheritdoc/>
    public byte[] Export() => KeyData.AsSpan().ToArray();

    /// <inheritdoc/>
    public string ExportPem() => PemEncoding.WriteString(PublicKeyLabel, Ed25519Pkcs8.EncodePublicKey(KeyData));
}
