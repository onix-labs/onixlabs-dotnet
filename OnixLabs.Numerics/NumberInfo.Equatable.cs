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

public readonly partial struct NumberInfo : IEquatable<NumberInfo>
{
    public bool Equals(NumberInfo other)
    {
        return other.Scale == Scale
               && other.Exponent == Exponent
               && other.Precision == Precision
               && other.Sign == Sign
               && other.UnscaledValue == UnscaledValue
               && other.Significand == Significand;
    }

    public override bool Equals(object? obj)
    {
        return obj is NumberInfo other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(UnscaledValue, Scale, Significand, Exponent, Precision, Sign);
    }

    public static bool operator ==(NumberInfo left, NumberInfo right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(NumberInfo left, NumberInfo right)
    {
        return !Equals(left, right);
    }
}
