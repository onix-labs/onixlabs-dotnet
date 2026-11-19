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
using System.Threading;
using System.Threading.Tasks;

namespace OnixLabs.Security.Cryptography.Jose;

/// <summary>
/// Defines a signer of JWS signing input, which lets a signature be made wherever the private key is kept, such as inside a key store.
/// </summary>
public interface IJwsSigner
{
    /// <summary>
    /// Gets the JWS algorithm the signer uses, which is written to the protected header as <c>alg</c>.
    /// </summary>
    /// <value>The JWS <c>alg</c> value, for example <see cref="JwsAlgorithm.EdDsa"/>.</value>
    string Algorithm { get; }

    /// <summary>
    /// Gets the identifier of the signing key, which is written to the protected header as <c>kid</c>.
    /// </summary>
    /// <value>The key identifier, or <see langword="null"/> to omit <c>kid</c>.</value>
    string? KeyId { get; }

    /// <summary>
    /// Signs the specified JWS signing input.
    /// </summary>
    /// <param name="signingInput">The ASCII bytes of BASE64URL(protected header) ‖ '.' ‖ BASE64URL(payload).</param>
    /// <param name="cancellationToken">A token used to cancel the operation.</param>
    /// <returns>
    /// Returns the raw JWS signature: 64 bytes for EdDSA, or the IEEE P1363 concatenation r ‖ s for ECDSA, as RFC 7518 §3.4 requires.
    /// </returns>
    ValueTask<byte[]> SignAsync(ReadOnlyMemory<byte> signingInput, CancellationToken cancellationToken = default);
}
