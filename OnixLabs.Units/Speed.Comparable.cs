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

public readonly partial struct Speed<T>
{
    /// <summary>
    /// Compares the magnitude of the current instance against the magnitude of the specified <see cref="ICompositeUnit{TLeft,TRight}"/>.
    /// </summary>
    /// <param name="other">The other <see cref="ICompositeUnit{TLeft,TRight}"/> to compare to the current instance.</param>
    /// <returns>Returns a signed integer that indicates the relative order of the values being compared.</returns>
    public int CompareTo(ICompositeUnit<Distance<T>, Time<T>>? other)
    {
        if (other is null) return 1;

        T leftMagnitude = Magnitude(Left, Right);
        T rightMagnitude = Magnitude(other.Left, other.Right);

        return leftMagnitude.CompareTo(rightMagnitude);
    }

    /// <inheritdoc/>
    public int CompareTo(object? obj) => obj switch
    {
        null => 1,
        Speed<T> other => CompareTo(other),
        _ => throw new ArgumentException($"Object must be of type {nameof(Speed<T>)}.", nameof(obj))
    };
}
