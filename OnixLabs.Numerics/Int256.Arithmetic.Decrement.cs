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

public readonly partial struct Int256
{
    /// <summary>Decrements the specified <see cref="Int256"/> value by one.</summary>
    /// <param name="value">The value to decrement.</param>
    /// <returns>Returns <paramref name="value"/> decremented by one, wrapping on underflow.</returns>
    public static Int256 operator --(Int256 value) => value - One;

    /// <summary>Decrements the specified <see cref="Int256"/> value by one, throwing on underflow.</summary>
    /// <param name="value">The value to decrement.</param>
    /// <returns>Returns <paramref name="value"/> decremented by one.</returns>
    /// <exception cref="OverflowException">Thrown when the decrement underflows.</exception>
    public static Int256 operator checked --(Int256 value) => checked(value - One);
}
