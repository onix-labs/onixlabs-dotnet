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
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;

namespace OnixLabs.Core.Linq;

/// <summary>
/// Provides extension methods for <see cref="Expression{TDelegate}"/>.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public static class ExpressionExtensions
{
    private const string ParameterName = "$param";

    /// <summary>
    /// Combines two <see cref="Expression{TDelegate}"/> instances into a single expression using the logical AND operator.
    /// </summary>
    /// <param name="left">The left-hand expression to combine.</param>
    /// <param name="right">The right-hand expression to combine.</param>
    /// <typeparam name="T">The underlying type of the expression.</typeparam>
    /// <returns>
    /// Returns a new <see cref="Expression{TDelegate}"/> that combines two <see cref="Expression{TDelegate}"/> instances
    /// into a single expression using the logical AND operator.
    /// </returns>
    /// <remarks>
    /// Calling this method introduces a new expression parameter (named <c>$param</c>) to ensure both expressions share the same parameter.
    /// Internal or nested lambda parameters that do not match the replaced parameter remain untouched.
    /// </remarks>
    public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
    {
        ParameterExpression parameter = Expression.Parameter(typeof(T), ParameterName);
        Expression leftBody = new ReplaceParameterVisitor(left.Parameters.First(), parameter).Visit(left.Body);
        Expression rightBody = new ReplaceParameterVisitor(right.Parameters.First(), parameter).Visit(right.Body);
        BinaryExpression binaryExpression = Expression.AndAlso(leftBody, rightBody);

        return Expression.Lambda<Func<T, bool>>(binaryExpression, parameter);
    }

    /// <summary>
    /// Combines two <see cref="Expression{TDelegate}"/> instances into a single expression using the logical OR operator.
    /// </summary>
    /// <param name="left">The left-hand expression to combine.</param>
    /// <param name="right">The right-hand expression to combine.</param>
    /// <typeparam name="T">The underlying type of the expression.</typeparam>
    /// <returns>
    /// Returns a new <see cref="Expression{TDelegate}"/> that combines two <see cref="Expression{TDelegate}"/> instances
    /// into a single expression using the logical OR operator.
    /// </returns>
    /// <remarks>
    /// Calling this method introduces a new expression parameter (named <c>$param</c>) to ensure both expressions share the same parameter.
    /// Internal or nested lambda parameters that do not match the replaced parameter remain untouched.
    /// </remarks>
    public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
    {
        ParameterExpression parameter = Expression.Parameter(typeof(T), ParameterName);
        Expression leftBody = new ReplaceParameterVisitor(left.Parameters.First(), parameter).Visit(left.Body);
        Expression rightBody = new ReplaceParameterVisitor(right.Parameters.First(), parameter).Visit(right.Body);
        BinaryExpression binaryExpression = Expression.OrElse(leftBody, rightBody);

        return Expression.Lambda<Func<T, bool>>(binaryExpression, parameter);
    }

    /// <summary>
    /// Negates the current <see cref="Expression{TDelegate}"/> instance using the logical NOT operator.
    /// </summary>
    /// <param name="expression">The expression to negate.</param>
    /// <typeparam name="T">The underlying type of the expression.</typeparam>
    /// <returns>
    /// Returns a new <see cref="Expression{TDelegate}"/> that negates the current <see cref="Expression{TDelegate}"/> instance using the logical NOT operator.
    /// </returns>
    /// <remarks>
    /// Calling this method introduces a new expression parameter (named <c>$param</c>) to ensure a uniform parameter expression.
    /// Internal or nested lambda parameters that do not match the replaced parameter remain untouched.
    /// </remarks>
    public static Expression<Func<T, bool>> Not<T>(this Expression<Func<T, bool>> expression)
    {
        ParameterExpression parameter = Expression.Parameter(typeof(T), ParameterName);
        Expression expressionBody = new ReplaceParameterVisitor(expression.Parameters.First(), parameter).Visit(expression.Body);

        UnaryExpression unaryExpression = Expression.Not(expressionBody);
        return Expression.Lambda<Func<T, bool>>(unaryExpression, parameter);
    }

    /// <summary>
    /// Represents an expression visitor that replaces parameter expressions in an expression tree.
    /// </summary>
    /// <remarks>
    /// This class is useful when combining multiple expression trees that each use their own parameter expressions.
    /// It ensures all expressions use a consistent parameter expression, which is necessary for creating valid and
    /// executable combined expressions.
    /// </remarks>
    private sealed class ReplaceParameterVisitor(ParameterExpression source, ParameterExpression target) : ExpressionVisitor
    {
        protected override Expression VisitParameter(ParameterExpression node) =>
            node == source ? target : base.VisitParameter(node);
    }
}
