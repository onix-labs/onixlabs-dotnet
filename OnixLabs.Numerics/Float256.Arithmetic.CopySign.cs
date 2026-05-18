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

namespace OnixLabs.Numerics;

public readonly partial struct Float256
{
    /// <summary>Copies the sign of <paramref name="sign"/> onto the magnitude of <paramref name="value"/>.</summary>
    /// <param name="value">The value whose magnitude is to be used.</param>
    /// <param name="sign">The value whose sign is to be used.</param>
    /// <returns>Returns a <see cref="Float256"/> with the magnitude of <paramref name="value"/> and the sign of <paramref name="sign"/>.</returns>
    public static Float256 CopySign(Float256 value, Float256 sign)
    {
        UInt256 magnitude = value.RawBits & ~SignMask;
        UInt256 signBit = sign.RawBits & SignMask;
        return new Float256(magnitude | signBit);
    }
}
