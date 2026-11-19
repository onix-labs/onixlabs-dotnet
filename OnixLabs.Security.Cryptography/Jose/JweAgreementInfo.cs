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
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace OnixLabs.Security.Cryptography.Jose;

/// <summary>
/// Represents the <c>apu</c> and <c>apv</c> values that DIDComm Messaging v2.1 defines for its encrypted messages.
/// </summary>
public static class JweAgreementInfo
{
    /// <summary>
    /// Creates the DIDComm <c>apv</c> value: the SHA-256 hash of the recipients' key identifiers, sorted and joined with <c>.</c>.
    /// </summary>
    /// <param name="recipientKeyIds">The key identifiers (<c>kid</c>) of every recipient of the message.</param>
    /// <returns>Returns the decoded <c>apv</c> value, 32 bytes, which is written to the header base64url-encoded.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="recipientKeyIds"/> is empty.</exception>
    /// <remarks>Why: the key identifiers are sorted by ordinal comparison of their UTF-16 code units, so that every party computes the same
    /// order.</remarks>
    public static byte[] CreateDidCommApv(IEnumerable<string> recipientKeyIds)
    {
        RequireNotNull(recipientKeyIds);

        string[] sorted = recipientKeyIds.Order(StringComparer.Ordinal).ToArray();
        Require(sorted.Length > 0, "At least one recipient key identifier is required.", nameof(recipientKeyIds));

        return SHA256.HashData(Encoding.UTF8.GetBytes(string.Join('.', sorted)));
    }

    /// <summary>
    /// Creates the DIDComm <c>apu</c> value for authenticated encryption: the sender's key identifier, the same value as <c>skid</c>.
    /// </summary>
    /// <param name="senderKeyId">The sender's key identifier.</param>
    /// <returns>Returns the decoded <c>apu</c> value, the UTF-8 bytes of <paramref name="senderKeyId"/>.</returns>
    public static byte[] CreateDidCommApu(string senderKeyId)
    {
        RequireNotNull(senderKeyId);
        return Encoding.UTF8.GetBytes(senderKeyId);
    }
}
