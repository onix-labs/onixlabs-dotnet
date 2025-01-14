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
/// Provides LINQ-like extension methods for <see cref="IQueryable{T}"/>.
/// </summary>
// ReSharper disable InconsistentNaming
[EditorBrowsable(EditorBrowsableState.Never)]
public static class IQueryableExtensions
{
    private const string QueryableNullExceptionMessage = "Queryable must not be null.";
    private const string SpecificationNullExceptionMessage = "Specification must not be null.";

    /// <summary>
    /// Filters a sequence of values based on a specification.
    /// </summary>
    /// <param name="queryable">The current <see cref="IQueryable{T}"/> to filter.</param>
    /// <param name="specification">The <see cref="Specification{T}"/> to filter by.</param>
    /// <typeparam name="T">The underlying type of the <see cref="IQueryable{T}"/>.</typeparam>
    /// <returns>Returns an <see cref="IQueryable{T}"/> that contains elements from the input sequence that satisfy the specification.</returns>
    public static IQueryable<T> Where<T>(this IQueryable<T> queryable, Specification<T> specification)
    {
        RequireNotNull(queryable, QueryableNullExceptionMessage, nameof(queryable));
        RequireNotNull(specification, SpecificationNullExceptionMessage, nameof(specification));

        return queryable.Where(specification.Criteria);
    }

    /// <summary>
    /// Filters a sequence of values based on a negated specification.
    /// </summary>
    /// <param name="queryable">The current <see cref="IQueryable{T}"/> to filter.</param>
    /// <param name="specification">The <see cref="Specification{T}"/> to filter by.</param>
    /// <typeparam name="T">The underlying type of the <see cref="IQueryable{T}"/>.</typeparam>
    /// <returns>Returns an <see cref="IQueryable{T}"/> that contains elements from the input sequence that satisfy the negated specification.</returns>
    public static IQueryable<T> WhereNot<T>(this IQueryable<T> queryable, Specification<T> specification)
    {
        RequireNotNull(queryable, QueryableNullExceptionMessage, nameof(queryable));
        RequireNotNull(specification, SpecificationNullExceptionMessage, nameof(specification));

        return queryable.Where(specification.Not().Criteria);
    }
}
