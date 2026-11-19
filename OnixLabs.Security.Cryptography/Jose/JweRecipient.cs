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
using System.Text.Json;

namespace OnixLabs.Security.Cryptography.Jose;

/// <summary>
/// Represents one recipient of a JSON Web Encryption: its per-recipient unprotected header and its encrypted key (RFC 7516 §7.2.1).
/// </summary>
public sealed class JweRecipient
{
    /// <summary>
    /// The encrypted (wrapped) content encryption key.
    /// </summary>
    private readonly byte[] encryptedKey;

    /// <summary>
    /// Initializes a new instance of the <see cref="JweRecipient"/> class.
    /// </summary>
    /// <param name="header">The per-recipient unprotected header, or <see langword="null"/>.</param>
    /// <param name="encryptedKey">The encrypted key.</param>
    internal JweRecipient(JsonElement? header, byte[] encryptedKey)
    {
        Header = header;
        this.encryptedKey = encryptedKey;
        KeyId = header is { } value ? JoseJson.GetOptionalString(value, "kid") : null;
    }

    /// <summary>
    /// Gets the per-recipient unprotected header.
    /// </summary>
    /// <value>The header as a JSON object, or <see langword="null"/> if there is none.</value>
    public JsonElement? Header { get; }

    /// <summary>
    /// Gets the recipient's key identifier, from <c>kid</c> in the per-recipient header.
    /// </summary>
    /// <value>The key identifier, or <see langword="null"/> if there is none.</value>
    public string? KeyId { get; }

    /// <summary>
    /// Gets the encrypted key.
    /// </summary>
    /// <value>The content encryption key, wrapped for this recipient.</value>
    public ReadOnlySpan<byte> EncryptedKey => encryptedKey;
}
