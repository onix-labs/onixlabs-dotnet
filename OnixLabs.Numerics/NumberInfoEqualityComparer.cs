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
using System.Collections;
using System.Collections.Generic;

namespace OnixLabs.Numerics;

/// <summary>
/// Represents an equality comparer for comparing <see cref="NumberInfo"/> values.
/// </summary>
public abstract class NumberInfoEqualityComparer : IEqualityComparer<NumberInfo>, IEqualityComparer
{
    /// <summary>
    /// Gets a <see cref="NumberInfoEqualityComparer"/> that strictly compares <see cref="NumberInfo"/> values.
    /// </summary>
    public static readonly NumberInfoEqualityComparer Strict = new NumberInfoStrictEqualityComparer();

    /// <summary>
    /// Gets a <see cref="NumberInfoEqualityComparer"/> that semantically compares <see cref="NumberInfo"/> values.
    /// </summary>
    public static readonly NumberInfoEqualityComparer Semantic = new NumberInfoSemanticEqualityComparer();

    /// <summary>
    /// Prevents a default instance of the <see cref="NumberInfoEqualityComparer"/> class from being created.
    /// </summary>
    private NumberInfoEqualityComparer()
    {
    }

    /// <summary>Determines whether the specified <see cref="NumberInfo"/> values are equal.</summary>
    /// <param name="x">The first object of type <see cref="NumberInfo"/> to compare.</param>
    /// <param name="y">The second object of type <see cref="NumberInfo"/> to compare.</param>
    /// <returns> Returns <see langword="true" /> if the specified <see cref="NumberInfo"/> values are equal; otherwise, <see langword="false" />.</returns>
    public abstract bool Equals(NumberInfo x, NumberInfo y);

    /// <summary>Returns a hash code for the specified <see cref="NumberInfo"/> value.</summary>
    /// <param name="obj">The <see cref="NumberInfo"/> value for which a hash code is to be returned.</param>
    /// <returns>Returns a hash code for the specified <see cref="NumberInfo"/> value.</returns>
    public abstract int GetHashCode(NumberInfo obj);

    /// <summary>Determines whether the specified objects are equal.</summary>
    /// <param name="x">The first object to compare.</param>
    /// <param name="y">The second object to compare.</param>
    /// <returns>Returns <see langword="true" /> if the specified objects are both of type <see cref="NumberInfo"/> and are equal; otherwise, <see langword="false" />.</returns>
    public new bool Equals(object? x, object? y)
    {
        return x is NumberInfo left && y is NumberInfo right && Equals(left, right);
    }

    /// <summary>Returns a hash code for the specified object.</summary>
    /// <param name="obj">The <see cref="object" /> for which a hash code is to be returned.</param>
    /// <returns>A hash code for the specified object.</returns>
    public int GetHashCode(object obj)
    {
        return obj is NumberInfo value ? GetHashCode(value) : obj.GetHashCode();
    }

    /// <summary>
    /// Represents an equality comparer that compares <see cref="NumberInfo"/> values using strict equality.
    /// Strict equality is determined by comparing <see cref="NumberInfo.UnscaledValue"/> and <see cref="NumberInfo.Scale"/> properties.
    /// </summary>
    private sealed class NumberInfoStrictEqualityComparer : NumberInfoEqualityComparer
    {
        /// <summary>Determines whether the specified <see cref="NumberInfo"/> values are equal.</summary>
        /// <param name="x">The first object of type <see cref="NumberInfo"/> to compare.</param>
        /// <param name="y">The second object of type <see cref="NumberInfo"/> to compare.</param>
        /// <returns> Returns <see langword="true" /> if the specified <see cref="NumberInfo"/> values are equal; otherwise, <see langword="false" />.</returns>
        public override bool Equals(NumberInfo x, NumberInfo y)
        {
            return x.UnscaledValue == y.UnscaledValue && x.Scale == y.Scale;
        }

        /// <summary>Returns a hash code for the specified <see cref="NumberInfo"/> value.</summary>
        /// <param name="obj">The <see cref="NumberInfo"/> value for which a hash code is to be returned.</param>
        /// <returns>Returns a hash code for the specified <see cref="NumberInfo"/> value.</returns>
        public override int GetHashCode(NumberInfo obj)
        {
            return HashCode.Combine(obj.UnscaledValue, obj.Scale);
        }
    }

    /// <summary>
    /// Represents an equality comparer that compares <see cref="NumberInfo"/> values using semantic equality.
    /// Semantic equality is determined by comparing <see cref="NumberInfo.Significand"/> and <see cref="NumberInfo.Exponent"/> properties.
    /// </summary>
    private sealed class NumberInfoSemanticEqualityComparer : NumberInfoEqualityComparer
    {
        /// <summary>Determines whether the specified <see cref="NumberInfo"/> values are equal.</summary>
        /// <param name="x">The first object of type <see cref="NumberInfo"/> to compare.</param>
        /// <param name="y">The second object of type <see cref="NumberInfo"/> to compare.</param>
        /// <returns> Returns <see langword="true" /> if the specified <see cref="NumberInfo"/> values are equal; otherwise, <see langword="false" />.</returns>
        public override bool Equals(NumberInfo x, NumberInfo y)
        {
            return NumberInfoOrdinalityComparer.Default.IsEqual(x, y);
        }

        /// <summary>Returns a hash code for the specified <see cref="NumberInfo"/> value.</summary>
        /// <param name="obj">The <see cref="NumberInfo"/> value for which a hash code is to be returned.</param>
        /// <returns>Returns a hash code for the specified <see cref="NumberInfo"/> value.</returns>
        public override int GetHashCode(NumberInfo obj)
        {
            return HashCode.Combine(obj.Significand, obj.Exponent);
        }
    }
}
