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

using System.Numerics;

namespace OnixLabs.Core.UnitTests.Data.Objects;

public sealed class Numeric<T> : IEquatable<Numeric<T>> where T : INumber<T>
{
    public Numeric(T value)
    {
        Value = value;
    }

    public T Value { get; }

    public bool Equals(Numeric<T>? other)
    {
        return other is not null && other.Value == Value;
    }

    public override bool Equals(object? obj)
    {
        return obj is Numeric<T> other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Value);
    }

    public override string ToString()
    {
        return this.ToRecordString();
    }
}
