// Copyright 2020-2023 ONIXLabs
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

namespace OnixLabs.Core.Numerics;

public readonly partial struct BigDecimal
{
    static bool INumberBase<BigDecimal>.TryConvertFromChecked<TOther>(TOther value, out BigDecimal result)
    {
        switch (value)
        {
            case BigInteger:
                result = (BigInteger)(object)value;
                return true;
            case sbyte:
                result = (sbyte)(object)value;
                return true;
            case byte:
                result = (byte)(object)value;
                return true;
            case short:
                result = (short)(object)value;
                return true;
            case ushort:
                result = (ushort)(object)value;
                return true;
            case int:
                result = (int)(object)value;
                return true;
            case uint:
                result = (uint)(object)value;
                return true;
            case long:
                result = (long)(object)value;
                return true;
            case ulong:
                result = (ulong)(object)value;
                return true;
            case decimal:
                result = (decimal)(object)value;
                return true;
            case double:
                result = (double)(object)value;
                return true;
            case float:
                result = (float)(object)value;
                return true;
            default:
                result = Zero;
                return false;
        }
    }

    static bool INumberBase<BigDecimal>.TryConvertFromSaturating<TOther>(TOther value, out BigDecimal result)
    {
        throw new NotImplementedException();
    }

    static bool INumberBase<BigDecimal>.TryConvertFromTruncating<TOther>(TOther value, out BigDecimal result)
    {
        throw new NotImplementedException();
    }

    static bool INumberBase<BigDecimal>.TryConvertToChecked<TOther>(BigDecimal value, out TOther result)
    {
        throw new NotImplementedException();
    }

    static bool INumberBase<BigDecimal>.TryConvertToSaturating<TOther>(BigDecimal value, out TOther result)
    {
        throw new NotImplementedException();
    }

    static bool INumberBase<BigDecimal>.TryConvertToTruncating<TOther>(BigDecimal value, out TOther result)
    {
        throw new NotImplementedException();
    }
}
