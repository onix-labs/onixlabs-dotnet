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

namespace OnixLabs.Core.Numerics;

public readonly partial struct BigDecimal
{
    public static explicit operator BigInteger(BigDecimal value)
    {
        throw new NotImplementedException();
    }

    public static explicit operator sbyte(BigDecimal value)
    {
        throw new NotImplementedException();
    }

    public static explicit operator byte(BigDecimal value)
    {
        throw new NotImplementedException();
    }

    public static explicit operator short(BigDecimal value)
    {
        throw new NotImplementedException();
    }

    public static explicit operator ushort(BigDecimal value)
    {
        throw new NotImplementedException();
    }

    public static explicit operator int(BigDecimal value)
    {
        throw new NotImplementedException();
    }

    public static explicit operator uint(BigDecimal value)
    {
        throw new NotImplementedException();
    }

    public static explicit operator long(BigDecimal value)
    {
        throw new NotImplementedException();
    }

    public static explicit operator ulong(BigDecimal value)
    {
        throw new NotImplementedException();
    }

    public static explicit operator float(BigDecimal value)
    {
        throw new NotImplementedException();
    }

    public static explicit operator double(BigDecimal value)
    {
        throw new NotImplementedException();
    }

    public static explicit operator decimal(BigDecimal value)
    {
        throw new NotImplementedException();
    }
}
