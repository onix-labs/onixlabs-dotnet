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

using System.Threading;
using System.Threading.Tasks;

namespace OnixLabs.Security.Cryptography.Jose;

/// <summary>
/// Defines one party's side of an ECDH key agreement, which lets the agreement be computed wherever the private key is kept, such as inside
/// a key store.
/// </summary>
public interface IJweKeyAgreement
{
    /// <summary>
    /// Gets the public key of the current party, whose <c>kid</c> identifies it in a JWE.
    /// </summary>
    /// <value>The public JWK, without a private part: an X25519 octet key pair, or an elliptic curve key on P-256, P-384 or P-521.</value>
    JsonWebKey PublicKey { get; }

    /// <summary>
    /// Computes the raw shared secret Z between the current party's private key and the specified public key, without hashing it.
    /// </summary>
    /// <param name="otherPartyPublicKey">The other party's public key, on the same curve as <see cref="PublicKey"/>.</param>
    /// <param name="cancellationToken">A token used to cancel the operation.</param>
    /// <returns>
    /// Returns the raw X25519 output, or the x-coordinate of the shared point for a NIST curve. Implementations must reject a public key
    /// that is not on the curve, and an all-zero X25519 output.
    /// </returns>
    ValueTask<Secret> DeriveSharedSecretAsync(JsonWebKey otherPartyPublicKey, CancellationToken cancellationToken = default);
}
