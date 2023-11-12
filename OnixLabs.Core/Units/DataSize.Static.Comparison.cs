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

using System.Numerics;

namespace OnixLabs.Core.Units;

public static partial class DataSize
{
    /// <summary>
    /// Compares two <see cref="DataSize{T}"/> values and returns an integer that indicates
    /// whether the left-hand value is less than, equal to, or greater than the right-hand value. 
    /// </summary>
    /// <param name="left">The left-hand value to compare.</param>
    /// <param name="right">The right-hand value to compare.</param>
    /// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> type that represents the <see cref="DataSize{T}"/> unit.</typeparam>
    /// <returns>Returns a value that indicates the relative order of the objects being compared.</returns>
    public static int Compare<T>(DataSize<T> left, DataSize<T> right) where T : IFloatingPoint<T>
    {
        return DataSize<T>.Compare(left, right);
    }

    /// <summary>
    /// Compares two instances of <see cref="DataSize{T}"/> to determine whether their values are equal.
    /// </summary>
    /// <param name="left">The left-hand value to compare.</param>
    /// <param name="right">The right-hand value to compare.</param>
    /// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> type that represents the <see cref="DataSize{T}"/> unit.</typeparam>
    /// <returns>Returns true if the two specified instances are equal; otherwise, false.</returns>
    public static bool Equals<T>(DataSize<T> left, DataSize<T> right) where T : IFloatingPoint<T>
    {
        return DataSize<T>.Equals(left, right);
    }
}
