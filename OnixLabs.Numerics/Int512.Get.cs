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
using System.Numerics;

namespace OnixLabs.Numerics;

public readonly partial struct Int512
{
    /// <summary>Gets the number of bytes that will be written by <see cref="TryWriteBigEndian"/> or <see cref="TryWriteLittleEndian"/>.</summary>
    /// <returns>Returns the number of bytes needed to write this value in either endianness.</returns>
    public int GetByteCount() => 64;

    /// <summary>Gets the length, in bits, of the shortest two's-complement representation of the current value.</summary>
    /// <returns>Returns the shortest bit length of the current value.</returns>
    public int GetShortestBitLength()
    {
        if (IsNegative(this))
        {
            UInt256 invertedUpper = ~upper;
            UInt256 invertedLower = ~lower;
            if (!UInt256.IsZero(invertedUpper)) return HalfBitWidth + invertedUpper.GetShortestBitLength() + 1;
            if (!UInt256.IsZero(invertedLower)) return invertedLower.GetShortestBitLength() + 1;
            return 1;
        }

        if (!UInt256.IsZero(upper)) return HalfBitWidth + upper.GetShortestBitLength() + 1;
        if (!UInt256.IsZero(lower)) return lower.GetShortestBitLength() + 1;
        return 0;
    }

    /// <inheritdoc cref="IBinaryInteger{TSelf}.GetByteCount"/>
    int IBinaryInteger<Int512>.GetByteCount() => GetByteCount();

    /// <inheritdoc cref="IBinaryInteger{TSelf}.GetShortestBitLength"/>
    int IBinaryInteger<Int512>.GetShortestBitLength() => GetShortestBitLength();
}
