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
    public static implicit operator BigDecimal(BigInteger value)
    {
        throw new NotImplementedException();
    }

    public static implicit operator BigDecimal(sbyte value)
    {
        throw new NotImplementedException();
    }

    public static implicit operator BigDecimal(byte value)
    {
        throw new NotImplementedException();
    }

    public static implicit operator BigDecimal(short value)
    {
        throw new NotImplementedException();
    }

    public static implicit operator BigDecimal(ushort value)
    {
        throw new NotImplementedException();
    }

    public static implicit operator BigDecimal(int value)
    {
        throw new NotImplementedException();
    }

    public static implicit operator BigDecimal(uint value)
    {
        throw new NotImplementedException();
    }

    public static implicit operator BigDecimal(long value)
    {
        throw new NotImplementedException();
    }

    public static implicit operator BigDecimal(ulong value)
    {
        throw new NotImplementedException();
    }

    public static implicit operator BigDecimal(float value)
    {
        throw new NotImplementedException();
    }

    public static implicit operator BigDecimal(double value)
    {
        throw new NotImplementedException();
    }

    public static implicit operator BigDecimal(decimal value)
    {
        throw new NotImplementedException();
    }
}
