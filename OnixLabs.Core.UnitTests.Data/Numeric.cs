// Copyright 2020 ONIXLabs
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

namespace OnixLabs.Core.UnitTests.Data;

public sealed class Numeric<T>(T value) : IEquatable<Numeric<T>> where T : INumber<T>
{
    public T Value { get; } = value;

    public bool Equals(Numeric<T>? other) => other is not null && other.Value == Value;

    public override bool Equals(object? obj) => obj is Numeric<T> other && Equals(other);

    public override int GetHashCode() => HashCode.Combine(Value);

    public override string ToString() => this.ToRecordString();
}
