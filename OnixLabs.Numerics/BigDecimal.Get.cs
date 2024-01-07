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
    int IFloatingPoint<BigDecimal>.GetExponentByteCount()
    {
        return sizeof(int);
    }

    int IFloatingPoint<BigDecimal>.GetExponentShortestBitLength()
    {
        return sizeof(int) - int.LeadingZeroCount(NumberInfo.Exponent);
    }

    int IFloatingPoint<BigDecimal>.GetSignificandBitLength()
    {
        return NumberInfo.UnscaledValue.ToByteArray().Length * 8;
    }

    int IFloatingPoint<BigDecimal>.GetSignificandByteCount()
    {
        return NumberInfo.UnscaledValue.ToByteArray().Length;
    }
}