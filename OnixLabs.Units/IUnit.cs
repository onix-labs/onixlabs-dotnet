// Copyright 2020-2025 ONIXLabs
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

namespace OnixLabs.Units;

public interface IUnit<T> : IEquatable<T>, IComparable<T>, ISpanFormattable where T : struct
{
    static abstract T Add(T left, T right);
    static abstract T Subtract(T left, T right);
    static abstract T Multiply(T left, T right);
    static abstract T Divide(T left, T right);

    static abstract bool Equals(T left, T right);
    static abstract int Compare(T left, T right);

    string ToString(ReadOnlySpan<char> format, IFormatProvider? formatProvider = null);
}
