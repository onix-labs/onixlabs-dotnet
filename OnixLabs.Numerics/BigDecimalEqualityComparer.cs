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

using System.Collections;
using System.Collections.Generic;

namespace OnixLabs.Numerics;

/// <summary>
/// Represents an equality comparer for comparing <see cref="BigDecimal"/> values.
/// </summary>
public abstract class BigDecimalEqualityComparer : IEqualityComparer<BigDecimal>, IEqualityComparer
{
    /// <summary>
    /// Gets an equality comparer that strictly compares <see cref="BigDecimal"/> values.
    /// </summary>
    public static readonly BigDecimalEqualityComparer Strict = new BigDecimalStrictEqualityComparer();

    /// <summary>
    /// Gets an equality comparer that semantically compares <see cref="BigDecimal"/> values.
    /// </summary>
    public static readonly BigDecimalEqualityComparer Semantic = new BigDecimalSemanticEqualityComparer();

    /// <summary>
    /// Prevents a default instance of the <see cref="BigDecimalEqualityComparer"/> class from being created.
    /// </summary>
    private BigDecimalEqualityComparer()
    {
    }

    /// <summary>Determines whether the specified <see cref="BigDecimal"/> values are equal.</summary>
    /// <param name="x">The first object of type <see cref="BigDecimal"/> to compare.</param>
    /// <param name="y">The second object of type <see cref="BigDecimal"/> to compare.</param>
    /// <returns> Returns <see langword="true" /> if the specified <see cref="BigDecimal"/> values are equal; otherwise, <see langword="false" />.</returns>
    public abstract bool Equals(BigDecimal x, BigDecimal y);

    /// <summary>Returns a hash code for the specified <see cref="BigDecimal"/> value.</summary>
    /// <param name="obj">The <see cref="BigDecimal"/> value for which a hash code is to be returned.</param>
    /// <returns>Returns a hash code for the specified <see cref="BigDecimal"/> value.</returns>
    public abstract int GetHashCode(BigDecimal obj);

    /// <summary>Determines whether the specified objects are equal.</summary>
    /// <param name="x">The first object to compare.</param>
    /// <param name="y">The second object to compare.</param>
    /// <returns>Returns <see langword="true" /> if the specified objects are both of type <see cref="BigDecimal"/> and are equal; otherwise, <see langword="false" />.</returns>
    public new bool Equals(object? x, object? y) => x is BigDecimal left && y is BigDecimal right && Equals(left, right);

    /// <summary>Returns a hash code for the specified object.</summary>
    /// <param name="obj">The <see cref="object" /> for which a hash code is to be returned.</param>
    /// <returns>A hash code for the specified object.</returns>
    public int GetHashCode(object obj) => obj is BigDecimal value ? GetHashCode(value) : obj.GetHashCode();

    /// <summary>
    /// Represents an equality comparer that compares <see cref="BigDecimal"/> values using strict equality.
    /// Strict equality is determined by comparing <see cref="BigDecimal.UnscaledValue"/> and <see cref="BigDecimal.Scale"/> properties.
    /// </summary>
    private sealed class BigDecimalStrictEqualityComparer : BigDecimalEqualityComparer
    {
        /// <summary>Determines whether the specified <see cref="BigDecimal"/> values are equal.</summary>
        /// <param name="x">The first object of type <see cref="BigDecimal"/> to compare.</param>
        /// <param name="y">The second object of type <see cref="BigDecimal"/> to compare.</param>
        /// <returns> Returns <see langword="true" /> if the specified <see cref="BigDecimal"/> values are equal; otherwise, <see langword="false" />.</returns>
        public override bool Equals(BigDecimal x, BigDecimal y) => NumberInfoEqualityComparer.Strict
            .Equals(x.ToNumberInfo(), y.ToNumberInfo());

        /// <summary>Returns a hash code for the specified object.</summary>
        /// <param name="obj">The <see cref="object" /> for which a hash code is to be returned.</param>
        /// <returns>A hash code for the specified object.</returns>
        public override int GetHashCode(BigDecimal obj) => NumberInfoEqualityComparer.Strict
            .GetHashCode(obj.ToNumberInfo());
    }

    /// <summary>
    /// Represents an equality comparer that compares <see cref="BigDecimal"/> values using semantic equality.
    /// Semantic equality is determined by comparing that <see cref="BigDecimal"/> values are equivalent, even when their scale differs.
    /// </summary>
    private sealed class BigDecimalSemanticEqualityComparer : BigDecimalEqualityComparer
    {
        /// <summary>Determines whether the specified <see cref="BigDecimal"/> values are equal.</summary>
        /// <param name="x">The first object of type <see cref="BigDecimal"/> to compare.</param>
        /// <param name="y">The second object of type <see cref="BigDecimal"/> to compare.</param>
        /// <returns> Returns <see langword="true" /> if the specified <see cref="BigDecimal"/> values are equal; otherwise, <see langword="false" />.</returns>
        public override bool Equals(BigDecimal x, BigDecimal y) => NumberInfoEqualityComparer.Semantic
            .Equals(x.ToNumberInfo(), y.ToNumberInfo());

        /// <summary>Returns a hash code for the specified object.</summary>
        /// <param name="obj">The <see cref="object" /> for which a hash code is to be returned.</param>
        /// <returns>A hash code for the specified object.</returns>
        public override int GetHashCode(BigDecimal obj) => NumberInfoEqualityComparer.Semantic
            .GetHashCode(obj.ToNumberInfo());
    }
}
