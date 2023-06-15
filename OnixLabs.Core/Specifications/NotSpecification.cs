// Copyright 2020-2023 ONIXLabs
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
using System.Linq;
using System.Linq.Expressions;

namespace OnixLabs.Core.Specifications;

/// <summary>
/// Represents a binary NOT negation of a specification.
/// </summary>
/// <typeparam name="T">The underlying type of the specification.</typeparam>
internal sealed class NotSpecification<T> : Specification<T>
{
    /// <summary>
    /// The specification expression.
    /// </summary>
    private readonly Expression<Func<T, bool>> expression;

    /// <summary>
    /// Initializes a new instance of the <see cref="NotSpecification{T}"/> class.
    /// </summary>
    /// <param name="specification">The specification to negate.</param>
    public NotSpecification(Specification<T> specification) => expression = specification.ToExpression();

    /// <summary>
    /// Provides an expression for the specification.
    /// </summary>
    /// <returns>Returns an expression for the specification.</returns>
    public override Expression<Func<T, bool>> ToExpression()
    {
        return Expression.Lambda<Func<T, bool>>(Expression.Not(expression), expression.Parameters.Single());
    }
}
