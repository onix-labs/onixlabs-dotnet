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

using System.ComponentModel;
using System.Linq;

namespace OnixLabs.Core.Linq;

/// <summary>
/// Provides LINQ-like extension methods for <see cref="IQueryable{T}"/> instances.
/// </summary>
// ReSharper disable InconsistentNaming
[EditorBrowsable(EditorBrowsableState.Never)]
public static class IQueryableExtensions
{
    private const string QueryableNullExceptionMessage = "Queryable must not be null.";
    private const string SpecificationNullExceptionMessage = "Specification must not be null.";

    /// <summary>
    /// Provides LINQ-like extension methods for <see cref="IQueryable{T}"/> instances.
    /// </summary>
    /// <param name="receiver">The current <see cref="IQueryable{T}"/> instance.</param>
    /// <typeparam name="T">The underlying type of the current <see cref="IQueryable{T}"/> instance.</typeparam>
    extension<T>(IQueryable<T> receiver)
    {
        /// <summary>
        /// Filters a sequence of values based on a specification.
        /// </summary>
        /// <param name="specification">The <see cref="Specification{T}"/> to filter by.</param>
        /// <returns>Returns an <see cref="IQueryable{T}"/> that contains elements from the input sequence that satisfy the specification.</returns>
        public IQueryable<T> Where(Specification<T> specification)
        {
            RequireNotNull(receiver, QueryableNullExceptionMessage, nameof(receiver));
            RequireNotNull(specification, SpecificationNullExceptionMessage, nameof(specification));

            return receiver.Where(specification.Criteria);
        }

        /// <summary>
        /// Filters a sequence of values based on a negated specification.
        /// </summary>
        /// <param name="specification">The <see cref="Specification{T}"/> to filter by.</param>
        /// <returns>Returns an <see cref="IQueryable{T}"/> that contains elements from the input sequence that satisfy the negated specification.</returns>
        public IQueryable<T> WhereNot(Specification<T> specification)
        {
            RequireNotNull(receiver, QueryableNullExceptionMessage, nameof(receiver));
            RequireNotNull(specification, SpecificationNullExceptionMessage, nameof(specification));

            return receiver.Where(specification.Not().Criteria);
        }
    }
}
