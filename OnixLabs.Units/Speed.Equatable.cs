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
using System.Diagnostics.CodeAnalysis;

namespace OnixLabs.Units;

public readonly partial struct Speed<T>
{
    /// <summary>
    /// Determines whether the specified <see cref="ICompositeUnit{TLeft,TRight}"/> represents the same magnitude as the current instance.
    /// </summary>
    /// <param name="other">The other <see cref="ICompositeUnit{TLeft,TRight}"/> to compare to the current instance.</param>
    /// <returns>Returns <see langword="true"/> if the specified <see cref="ICompositeUnit{TLeft,TRight}"/> represents the same magnitude as the current instance; otherwise <see langword="false"/>.</returns>
    public bool Equals(ICompositeUnit<Distance<T>, Time<T>>? other)
    {
        if (other is null) return false;

        T leftMagnitude = Magnitude(Left, Right);
        T rightMagnitude = Magnitude(other.Left, other.Right);

        return leftMagnitude == rightMagnitude;
    }

    /// <inheritdoc/>
    public override bool Equals([NotNullWhen(true)] object? obj) =>
        obj is Speed<T> other && Equals(other);

    /// <inheritdoc/>
    public override int GetHashCode() => HashCode.Combine(Magnitude(Left, Right));
}
