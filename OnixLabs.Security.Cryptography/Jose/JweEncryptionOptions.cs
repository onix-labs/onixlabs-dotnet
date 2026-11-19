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
using System.Collections.Generic;

namespace OnixLabs.Security.Cryptography.Jose;

/// <summary>
/// Represents the options with which <see cref="JsonWebEncryption.EncryptAsync"/> encrypts a message.
/// </summary>
public sealed record JweEncryptionOptions
{
    /// <summary>
    /// Gets the key management algorithm, written as <c>alg</c>.
    /// </summary>
    /// <value>One of the <see cref="JweAlgorithm"/> key management values; <see cref="JweAlgorithm.EcdhEsA256Kw"/> by default.</value>
    public string Algorithm { get; init; } = JweAlgorithm.EcdhEsA256Kw;

    /// <summary>
    /// Gets the content encryption algorithm, written as <c>enc</c>.
    /// </summary>
    /// <value>One of the <see cref="JweAlgorithm"/> content encryption values; <see cref="JweAlgorithm.A256CbcHs512"/> by default.</value>
    public string EncryptionAlgorithm { get; init; } = JweAlgorithm.A256CbcHs512;

    /// <summary>
    /// Gets the sender's static key, which ECDH-1PU requires and ECDH-ES must not have.
    /// </summary>
    /// <value>The sender's key agreement, or <see langword="null"/> for ECDH-ES. Its <c>kid</c>, if any, is written as <c>skid</c>.</value>
    public IJweKeyAgreement? Sender { get; init; }

    /// <summary>
    /// Gets the media type, written as <c>typ</c>, such as <c>application/didcomm-encrypted+json</c>.
    /// </summary>
    /// <value>The media type, or <see langword="null"/> to omit <c>typ</c>.</value>
    public string? Type { get; init; }

    /// <summary>
    /// Gets the agreement PartyUInfo, written base64url-encoded as <c>apu</c>; see <see cref="JweAgreementInfo.CreateDidCommApu"/>.
    /// </summary>
    /// <value>The decoded <c>apu</c> value, or <see langword="null"/> to omit it.</value>
    public byte[]? AgreementPartyUInfo { get; init; }

    /// <summary>
    /// Gets the agreement PartyVInfo, written base64url-encoded as <c>apv</c>; see <see cref="JweAgreementInfo.CreateDidCommApv"/>.
    /// </summary>
    /// <value>The decoded <c>apv</c> value, or <see langword="null"/> to omit it.</value>
    public byte[]? AgreementPartyVInfo { get; init; }

    /// <summary>
    /// Gets the members of the JWE Shared Unprotected Header, written as <c>unprotected</c>; they are not authenticated.
    /// </summary>
    /// <value>The string members, or <see langword="null"/> to omit <c>unprotected</c>.</value>
    public IReadOnlyDictionary<string, string>? SharedUnprotectedHeader { get; init; }

    /// <summary>
    /// Gets a factory for the ephemeral key, for reproducing published examples in tests only.
    /// </summary>
    /// <value>A function that returns the ephemeral key agreement for a recipient key's curve, or <see langword="null"/> to generate
    /// one.</value>
    internal Func<JsonWebKey, IJweKeyAgreement>? EphemeralKeyFactory { get; init; }

    /// <summary>
    /// Gets a fixed content encryption key, for reproducing published examples in tests only.
    /// </summary>
    /// <value>The CEK, or <see langword="null"/> to generate one.</value>
    internal byte[]? ContentEncryptionKey { get; init; }

    /// <summary>
    /// Gets a fixed initialization vector, for reproducing published examples in tests only.
    /// </summary>
    /// <value>The IV, or <see langword="null"/> to generate one.</value>
    internal byte[]? InitializationVector { get; init; }
}
