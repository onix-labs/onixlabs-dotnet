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

namespace OnixLabs.Core.Numerics;

public readonly partial struct BigDecimal
{
    public static bool IsLessThan(BigDecimal left, BigDecimal right)
    {
        throw new NotImplementedException();
    }

    public static bool operator <(BigDecimal left, BigDecimal right)
    {
        throw new NotImplementedException();
    }

    public bool IsLessThan(BigDecimal right)
    {
        throw new NotImplementedException();
    }
}
