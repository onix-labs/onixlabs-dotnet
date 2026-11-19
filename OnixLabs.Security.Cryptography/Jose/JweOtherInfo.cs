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
using System.Buffers.Binary;
using System.Text;

namespace OnixLabs.Security.Cryptography.Jose;

/// <summary>
/// Represents the construction of the Concat KDF <c>OtherInfo</c> that JOSE uses for ECDH-ES (RFC 7518 §4.6.2) and ECDH-1PU
/// (draft-madden-jose-ecdh-1pu-04 §2.3).
/// </summary>
public static class JweOtherInfo
{
    /// <summary>
    /// Creates <c>OtherInfo = AlgorithmID ‖ PartyUInfo ‖ PartyVInfo ‖ SuppPubInfo ‖ SuppPrivInfo</c> for key agreement with key wrapping.
    /// </summary>
    /// <param name="algorithm">
    /// The ASCII value of AlgorithmID's data: the <c>alg</c> header value in key wrapping mode, or the <c>enc</c> value in direct mode.
    /// </param>
    /// <param name="partyUInfo">The decoded <c>apu</c> header value, or empty if there is none.</param>
    /// <param name="partyVInfo">The decoded <c>apv</c> header value, or empty if there is none.</param>
    /// <param name="keyDataLengthInBits">The length of the derived key in bits, keydatalen.</param>
    /// <param name="tag">
    /// For ECDH-1PU in key wrapping mode, the JWE authentication tag, which becomes the cctag appended to SuppPubInfo as
    /// <c>Datalen ‖ Data</c>; otherwise empty, the default.
    /// </param>
    /// <returns>
    /// Returns the <c>OtherInfo</c> bytes, in which AlgorithmID, PartyUInfo and PartyVInfo are each prefixed with their length as a 32-bit
    /// big-endian integer, SuppPubInfo is keydatalen as a 32-bit big-endian integer followed by any cctag, and SuppPrivInfo is empty.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="keyDataLengthInBits"/> is not positive.</exception>
    public static byte[] Create(
        string algorithm,
        ReadOnlySpan<byte> partyUInfo,
        ReadOnlySpan<byte> partyVInfo,
        int keyDataLengthInBits,
        ReadOnlySpan<byte> tag = default)
    {
        ArgumentNullException.ThrowIfNull(algorithm);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(keyDataLengthInBits);

        byte[] algorithmId = Encoding.ASCII.GetBytes(algorithm);
        int length = (4 + algorithmId.Length) + (4 + partyUInfo.Length) + (4 + partyVInfo.Length) + 4 + (tag.IsEmpty ? 0 : 4 + tag.Length);
        byte[] result = new byte[length];
        int offset = 0;

        WriteWithLength(result, ref offset, algorithmId);
        WriteWithLength(result, ref offset, partyUInfo);
        WriteWithLength(result, ref offset, partyVInfo);
        BinaryPrimitives.WriteInt32BigEndian(result.AsSpan(offset), keyDataLengthInBits);
        offset += 4;

        if (!tag.IsEmpty) WriteWithLength(result, ref offset, tag);

        return result;
    }

    /// <summary>
    /// Writes the specified data, prefixed with its length as a 32-bit big-endian integer.
    /// </summary>
    /// <param name="destination">The buffer to write to.</param>
    /// <param name="offset">The offset at which to write, which is advanced past the written bytes.</param>
    /// <param name="data">The data to write.</param>
    private static void WriteWithLength(byte[] destination, ref int offset, ReadOnlySpan<byte> data)
    {
        BinaryPrimitives.WriteInt32BigEndian(destination.AsSpan(offset), data.Length);
        data.CopyTo(destination.AsSpan(offset + 4));
        offset += 4 + data.Length;
    }
}
