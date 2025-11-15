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
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using OnixLabs.Core.Linq;

namespace OnixLabs.Core;

/// <summary>
/// Represents the base class for implementing composable specifications using logical operations.
/// </summary>
/// <typeparam name="T">The underlying type of the subject to which the specification applies.</typeparam>
// ReSharper disable PossibleMultipleEnumeration
// ReSharper disable MemberCanBePrivate.Global
public abstract class Specification<T>
{
    /// <summary>
    /// Represents an empty specification that always evaluates to <see langword="true"/>.
    /// </summary>
    public static readonly Specification<T> Empty = new BooleanSpecification<T>(true);

    /// <summary>
    /// Gets the underlying expression criteria of the current specification.
    /// </summary>
    public abstract Expression<Func<T, bool>> Criteria { get; }

    /// <summary>
    /// Combines multiple specifications using a logical AND operation.
    /// </summary>
    /// <param name="specifications">A collection of specifications to combine.</param>
    /// <returns>
    /// Returns a combined specification that evaluates to <see langword="true"/> if all the provided specifications are satisfied,
    /// or the identity specification (always <see langword="true"/>) if no specifications are provided.
    /// </returns>
    public static Specification<T> And(params IEnumerable<Specification<T>> specifications) => specifications.IsNotEmpty()
        ? specifications.Aggregate((left, right) => left.And(right))
        : new BooleanSpecification<T>(true);

    /// <summary>
    /// Combines multiple specifications using a logical OR operation.
    /// </summary>
    /// <param name="specifications">A collection of specifications to combine.</param>
    /// <returns>
    /// Returns a combined specification that evaluates to <see langword="true"/> if any of the provided specifications are satisfied,
    /// or the identity specification (always <see langword="false"/>) if no specifications are provided.
    /// </returns>
    public static Specification<T> Or(params IEnumerable<Specification<T>> specifications) => specifications.IsNotEmpty()
        ? specifications.Aggregate((left, right) => left.Or(right))
        : new BooleanSpecification<T>(false);

    /// <summary>
    /// Combines the current specification with another using a logical AND operation.
    /// </summary>
    /// <param name="other">The other specification to combine with.</param>
    /// <returns>
    /// Returns a combined specification that evaluates to <see langword="true"/> if both specifications are satisfied;
    /// otherwise, the specification evaluates to <see langword="false"/>.
    /// </returns>
    public Specification<T> And(Specification<T> other) => new AndSpecification<T>(this, other);

    /// <summary>
    /// Combines the current specification with another using a logical OR operation.
    /// </summary>
    /// <param name="other">The other specification to combine with.</param>
    /// <returns>
    /// Returns a combined specification that evaluates to <see langword="true"/> if either specification is satisfied;
    /// otherwise, the specification evaluates to <see langword="false"/>.
    /// </returns>
    public Specification<T> Or(Specification<T> other) => new OrSpecification<T>(this, other);

    /// <summary>
    /// Creates a specification that negates the current specification's logic using a logical NOT operation.
    /// </summary>
    /// <returns>
    /// Returns a specification that evaluates to <see langword="true"/> if the current specification is not satisfied;
    /// otherwise, the specification evaluates to <see langword="false"/>.
    /// </returns>
    public Specification<T> Not() => new NotSpecification<T>(this);

    /// <summary>
    /// Evaluates whether the specified subject satisfies the current specification.
    /// </summary>
    /// <param name="subject">The subject to evaluate.</param>
    /// <returns>Returns <see langword="true"/> if the subject satisfies the specification; otherwise, <see langword="false"/>.</returns>
    public bool IsSatisfiedBy(T subject) => Criteria.Compile().Invoke(subject);
}

/// <summary>
/// Represents a specification that wraps an expression argument.
/// </summary>
/// <param name="criteria">The expression criteria to wrap.</param>
/// <typeparam name="T">The underlying type of the subject to which the specification applies.</typeparam>
public class CriteriaSpecification<T>(Expression<Func<T, bool>> criteria) : Specification<T>
{
    /// <summary>
    /// Gets the underlying expression criteria of the current specification.
    /// </summary>
    public override Expression<Func<T, bool>> Criteria => criteria;
}

/// <summary>
/// Represents a specification that combines two specifications using a logical AND operation.
/// </summary>
/// <typeparam name="T">The underlying type of the subject to which the specification applies.</typeparam>
file sealed class AndSpecification<T>(Specification<T> left, Specification<T> right) :
    CriteriaSpecification<T>(left.Criteria.And(right.Criteria));

/// <summary>
/// Represents a specification that combines two specifications using a logical OR operation.
/// </summary>
/// <typeparam name="T">The underlying type of the subject to which the specification applies.</typeparam>
file sealed class OrSpecification<T>(Specification<T> left, Specification<T> right) :
    CriteriaSpecification<T>(left.Criteria.Or(right.Criteria));

/// <summary>
/// Represents a specification that negates another specification's logic using a logical NOT operation.
/// </summary>
/// <typeparam name="T">The underlying type of the subject to which the specification applies.</typeparam>
file sealed class NotSpecification<T>(Specification<T> specification) :
    CriteriaSpecification<T>(specification.Criteria.Not());

/// <summary>
/// Represents a specification with a constant boolean value.
/// </summary>
/// <param name="value">The constant boolean value that the specification will return.</param>
/// <typeparam name="T">The underlying type of the subject to which the specification applies.</typeparam>
file sealed class BooleanSpecification<T>(bool value) :
    CriteriaSpecification<T>(_ => value);
