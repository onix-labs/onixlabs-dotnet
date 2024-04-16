// Copyright © 2020 ONIXLabs
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
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace OnixLabs.Core.Text;

public readonly partial struct Base58
{
    /// <summary>
    /// The size of a Base-58 checksum.
    /// </summary>
    private const int ChecksumSize = 4;

    /// <summary>
    /// Computes a Base-58 checksum.
    /// </summary>
    /// <param name="value">The value for which to compute a Base-58 checksum.</param>
    /// <returns>Returns the computed Base-58 checksum.</returns>
    private static byte[] ComputeChecksum(byte[] value)
    {
        byte[] hashedValue = SHA256.HashData(value);
        byte[] checksum = SHA256.HashData(hashedValue);
        byte[] result = new byte[ChecksumSize];

        Buffer.BlockCopy(checksum, 0, result, 0, result.Length);

        return result;
    }

    /// <summary>
    /// Adds a checksum to the specified byte array.
    /// </summary>
    /// <param name="value">The value for which to add a checksum.</param>
    /// <returns>Returns the original value with a checksum.</returns>
    private static byte[] AddChecksum(byte[] value) => ComputeChecksum(value)
        .Apply(checksum => value.ConcatenateWith(checksum));

    /// <summary>
    /// Removes a checksum from the specified byte array.
    /// </summary>
    /// <param name="value">The value for which to remove a checksum.</param>
    /// <returns>Returns the original value without a checksum.</returns>
    private static byte[] RemoveChecksum(byte[] value) => value.Copy(0, value.Length - ChecksumSize);

    /// <summary>
    /// Gets a checksum from the specified byte array.
    /// </summary>
    /// <param name="value">The value from which to obtain a checksum.</param>
    /// <returns>Returns a checksum from the specified byte array.</returns>
    private static byte[] GetChecksum(byte[] value) => value.Copy(value.Length - ChecksumSize - 1, ChecksumSize);

    /// <summary>
    /// Verifies a Base-58 checksum.
    /// </summary>
    /// <param name="value">The value for which to verify its checksum.</param>
    /// <exception cref="FormatException">If the Base-58 checksum is invalid.</exception>
    private static void VerifyChecksum(byte[] value)
    {
        byte[] valueWithoutChecksum = RemoveChecksum(value);
        byte[] originalChecksum = GetChecksum(value);
        byte[] computedChecksum = ComputeChecksum(valueWithoutChecksum);

        if (!originalChecksum.SequenceEqual(computedChecksum))
            throw new FormatException("Base-58 checksum is invalid.");
    }
}
