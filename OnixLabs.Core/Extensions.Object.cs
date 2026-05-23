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
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OnixLabs.Core.Linq;
using OnixLabs.Core.Reflection;
using OnixLabs.Core.Text;

namespace OnixLabs.Core;

/// <summary>
/// Provides extension methods for <see cref="object"/> instances.
/// </summary>
// ReSharper disable ConvertToExtensionBlock
[EditorBrowsable(EditorBrowsableState.Never)]
public static class ObjectExtensions
{
    private const string Null = "null";
    private const string ObjectOpenBracket = " { ";
    private const string ObjectCloseBracket = " }";
    private const string ObjectEmptyBrackets = " { }";
    private const string ObjectPropertySeparator = ", ";
    private const string ObjectPropertyAssignment = " = ";

    /// <summary>
    /// Calls the specified <see cref="Action{T}"/> with the current <paramref name="value"/>.
    /// </summary>
    /// <param name="value">The value to pass to the specified <see cref="Action{T}"/>.</param>
    /// <param name="action">The action into which the current <paramref name="value"/> will be passed.</param>
    /// <typeparam name="T">The underlying type of the current value.</typeparam>
    /// <returns>Returns the current <paramref name="value"/> once the specified <see cref="Action{T}"/> has been executed.</returns>
    public static T Apply<T>(this T value, Action<T> action) where T : class
    {
        ArgumentNullException.RequireNotNull(action, "Action must not be null.");
        action(value);
        return value;
    }

    /// <summary>
    /// Calls the specified <see cref="Func{T, TResult}"/> with the current <paramref name="value"/>.
    /// </summary>
    /// <param name="value">The value to pass to the specified <see cref="Func{T, TResult}"/>.</param>
    /// <param name="function">The function into which the current <paramref name="value"/> will be passed.</param>
    /// <typeparam name="T">The underlying type of the current value.</typeparam>
    /// <returns>Returns the result of the specified <see cref="Func{T, TResult}"/>.</returns>
    public static T Apply<T>(this T value, Func<T, T> function) where T : struct
    {
        ArgumentNullException.RequireNotNull(function, "Function must not be null.");
        return function(value);
    }

    /// <summary>
    /// Calls the specified <see cref="Func{T, TResult}"/> with the current <paramref name="value"/> as its argument and returns the result.
    /// </summary>
    /// <param name="value">The value to pass to the specified <see cref="Func{T, TResult}"/>.</param>
    /// <param name="function">The function into which the current <paramref name="value"/> will be passed.</param>
    /// <typeparam name="TSource">The underlying type of the current value.</typeparam>
    /// <typeparam name="TResult">The underlying type of the result value.</typeparam>
    /// <returns>Returns the result of the specified <see cref="Func{T, TResult}"/>.</returns>
    public static TResult Let<TSource, TResult>(this TSource value, Func<TSource, TResult> function)
    {
        ArgumentNullException.RequireNotNull(function, "Function must not be null.");
        return function(value);
    }

    /// <summary>
    /// Gets a record-like <see cref="String"/> representation of the current <see cref="Object"/> instance.
    /// </summary>
    /// <param name="value">The current <see cref="Object"/> instance.</param>
    /// <remarks>This method is designed specifically for record-like objects and may produce undesirable results when applied to primitive-like objects.</remarks>
    /// <returns>Returns a record-like <see cref="String"/> representation of the current <see cref="Object"/> instance.</returns>
    public static string ToRecordString(this object? value)
    {
        if (value is null) return Null;

        Type type = value.GetType();

        try
        {
            // ReSharper disable once HeapView.ObjectAllocation.Evident
            StringBuilder builder = new();
            IEnumerable<PropertyInfo> properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            builder.Append(type.GetCSharpTypeDeclaration());

            if (properties.IsEmpty())
                return builder.Append(ObjectEmptyBrackets).ToString();

            builder.Append(ObjectOpenBracket);

            // ReSharper disable once HeapView.ObjectAllocation.Possible
            foreach (PropertyInfo property in properties)
            {
                builder.Append(property.Name).Append(ObjectPropertyAssignment);

                object? propertyValue = property.GetValue(value);

                switch (propertyValue)
                {
                    case null:
                        builder.Append(Null);
                        break;
                    case string:
                        builder.Append(propertyValue.ToStringOrNull());
                        break;
                    case IEnumerable enumerable:
                        builder.Append(enumerable.ToCollectionString());
                        break;
                    default:
                        builder.Append(propertyValue.ToStringOrNull());
                        break;
                }

                builder.Append(ObjectPropertySeparator);
            }

            return builder.TrimEnd(ObjectPropertySeparator).Append(ObjectCloseBracket).ToString();
        }
        catch
        {
            return string.Concat(type.GetCSharpTypeDeclaration(), ObjectEmptyBrackets);
        }
    }

    /// <summary>
    /// Obtains a <see cref="String"/> representation of the current <see cref="Object"/>, or a string literal null if the current object is <see langword="null"/>.
    /// </summary>
    /// <param name="value">The current <see cref="Object"/> instance.</param>
    /// <returns>Returns a <see cref="String"/> representation of the current <see cref="Object"/>, or a string literal null if the current object is <see langword="null"/>.</returns>
    public static string ToStringOrNull(this object? value) => value?.ToString() ?? Null;

    /// <summary>
    /// Obtains an <see cref="Optional{T}"/> representation of the current <see cref="Object"/>.
    /// </summary>
    /// <param name="value">The <see cref="Object"/> to wrap as an <see cref="Optional{T}"/> value.</param>
    /// <typeparam name="T">The underlying type of the value.</typeparam>
    /// <returns>Returns an <see cref="Optional{T}"/> representation of the current <see cref="Object"/>.</returns>
    public static Optional<T> ToOptional<T>(this T? value) where T : notnull => Optional<T>.Of(value);

    /// <summary>
    /// Obtains an <see cref="Optional{T}"/> representation of the current <see cref="Object"/>.
    /// </summary>
    /// <param name="value">The <see cref="Object"/> to wrap as an <see cref="Optional{T}"/> value.</param>
    /// <typeparam name="T">The underlying type of the value.</typeparam>
    /// <returns>Returns an <see cref="Optional{T}"/> representation of the current <see cref="Object"/>.</returns>
    public static Optional<T> ToOptional<T>(this T? value) where T : struct => Optional<T>.Of(value);

    /// <summary>
    /// Asynchronously obtains an <see cref="Optional{T}"/> representation of the current reference-type <see cref="Object"/>.
    /// </summary>
    /// <param name="value">The <see cref="Object"/> to wrap as an <see cref="Optional{T}"/> value.</param>
    /// <param name="token">The cancellation token that can be used to cancel long-running tasks.</param>
    /// <typeparam name="T">The underlying type of the value.</typeparam>
    /// <returns>Returns an <see cref="Optional{T}"/> representation of the current <see cref="Object"/>.</returns>
    public static async Task<Optional<T>> ToOptionalAsync<T>(this Task<T?> value, CancellationToken token = default) where T : notnull =>
        Optional<T>.Of(await value.WaitAsync(token).ConfigureAwait(false));

    /// <summary>
    /// Asynchronously obtains an <see cref="Optional{T}"/> representation of the current value-type <see cref="Object"/>.
    /// </summary>
    /// <param name="value">The <see cref="Object"/> to wrap as an <see cref="Optional{T}"/> value.</param>
    /// <param name="token">The cancellation token that can be used to cancel long-running tasks.</param>
    /// <typeparam name="T">The underlying type of the value.</typeparam>
    /// <returns>Returns an <see cref="Optional{T}"/> representation of the current <see cref="Object"/>.</returns>
    public static async Task<Optional<T>> ToOptionalAsync<T>(this Task<T?> value, CancellationToken token = default) where T : struct =>
        Optional<T>.Of(await value.WaitAsync(token).ConfigureAwait(false));

    /// <summary>
    /// Obtains a <see cref="Success{T}"/> representation of the current <see cref="Object"/>.
    /// </summary>
    /// <param name="value">The <see cref="Object"/> to wrap as a <see cref="Success{T}"/> result.</param>
    /// <typeparam name="T">The underlying type of the value.</typeparam>
    /// <returns>Returns a <see cref="Success{T}"/> representation of the current <see cref="Object"/>.</returns>
    public static Success<T> ToSuccess<T>(this T value) => Result<T>.Success(value);

    /// <summary>
    /// Asynchronously obtains a <see cref="Success{T}"/> representation of the current <see cref="Object"/>.
    /// </summary>
    /// <param name="value">The <see cref="Object"/> to wrap as a <see cref="Success{T}"/> result.</param>
    /// <param name="token">The cancellation token that can be used to cancel long-running tasks.</param>
    /// <typeparam name="T">The underlying type of the value.</typeparam>
    /// <returns>Returns a <see cref="Success{T}"/> representation of the current <see cref="Object"/>.</returns>
    public static async Task<Success<T>> ToSuccessAsync<T>(this Task<T> value, CancellationToken token = default) =>
        Result<T>.Success(await value.WaitAsync(token).ConfigureAwait(false));

    /// <summary>
    /// Attempts to extract a non-null value from the current nullable <see cref="Object"/>.
    /// </summary>
    /// <param name="value">The current nullable <see cref="Object"/> to test for nullability.</param>
    /// <param name="result">The non-null value when this method returns <see langword="true"/>; otherwise, <see langword="null"/>.</param>
    /// <typeparam name="T">The underlying type of the value.</typeparam>
    /// <returns>Returns <see langword="true"/> if the current nullable <see cref="Object"/> is not null; otherwise, <see langword="false"/>.</returns>
    public static bool TryGetNonNull<T>(this T? value, [NotNullWhen(true)] out T result)
    {
        if (value is null)
        {
            result = default!;
            return false;
        }

        result = value;
        return true;
    }
}
