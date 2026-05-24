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

using System.Numerics;

namespace OnixLabs.Numerics;

public readonly partial struct BigDecimal
{
    /// <inheritdoc cref="IFloatingPoint{TSelf}.GetExponentByteCount"/>
    int IFloatingPoint<BigDecimal>.GetExponentByteCount() => sizeof(int);

    /// <inheritdoc cref="IFloatingPoint{TSelf}.GetExponentShortestBitLength"/>
    int IFloatingPoint<BigDecimal>.GetExponentShortestBitLength()
    {
        int exponent = number.Exponent;

        // LeadingZeroCount counts bits, so the bit width is sizeof(int) * 8. A negative exponent needs one
        // additional bit for the sign, mirroring the shortest two's complement length used by the integer types.
        return exponent >= 0
            ? sizeof(int) * 8 - int.LeadingZeroCount(exponent)
            : sizeof(int) * 8 + 1 - int.LeadingZeroCount(~exponent);
    }

    /// <inheritdoc cref="IFloatingPoint{TSelf}.GetSignificandBitLength"/>
    int IFloatingPoint<BigDecimal>.GetSignificandBitLength() => number.Significand.GetByteCount() * 8;

    /// <inheritdoc cref="IFloatingPoint{TSelf}.GetSignificandByteCount"/>
    int IFloatingPoint<BigDecimal>.GetSignificandByteCount() => number.Significand.GetByteCount();
}
