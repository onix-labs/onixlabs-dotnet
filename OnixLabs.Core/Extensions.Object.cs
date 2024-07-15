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
using System.Reflection;
using System.Text;
using OnixLabs.Core.Linq;
using OnixLabs.Core.Reflection;
using OnixLabs.Core.Text;

namespace OnixLabs.Core;

/// <summary>
/// Provides extension methods for objects.
/// </summary>
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
    /// Determines whether the current <see cref="IComparable{T}"/> value falls within range, inclusive of the specified minimum and maximum values.
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The inclusive minimum value.</param>
    /// <param name="max">The inclusive maximum value.</param>
    /// <typeparam name="T">The underlying <see cref="IComparable{T}"/> type.</typeparam>
    /// <returns>
    /// Returns <see langword="true"/> if the current <see cref="IComparable{T}"/> value falls within range,
    /// inclusive of the specified minimum and maximum values; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool IsWithinRangeInclusive<T>(this T value, T min, T max) where T : IComparable<T> =>
        value.CompareTo(min) is 0 or 1 && value.CompareTo(max) is 0 or -1;

    /// <summary>
    /// Determines whether the current <see cref="IComparable{T}"/> value falls within range, exclusive of the specified minimum and maximum values.
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <param name="min">The exclusive minimum value.</param>
    /// <param name="max">The exclusive maximum value.</param>
    /// <typeparam name="T">The underlying <see cref="IComparable{T}"/> type.</typeparam>
    /// <returns>
    /// Returns <see langword="true"/> if the current <see cref="IComparable{T}"/> value falls within range,
    /// exclusive of the specified minimum and maximum values; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool IsWithinRangeExclusive<T>(this T value, T min, T max) where T : IComparable<T> =>
        value.CompareTo(min) is 1 && value.CompareTo(max) is -1;

    /// <summary>
    /// Compares the current <typeparamref name="T"/> instance with the specified <typeparamref name="T"/> instance.
    /// </summary>
    /// <param name="left">The left-hand <typeparamref name="T"/> instance to compare.</param>
    /// <param name="right">The right-hand <typeparamref name="T"/> instance to compare.</param>
    /// <typeparam name="T">The underlying type of the current <typeparamref name="T"/>.</typeparam>
    /// <returns>Returns a signed integer that indicates the relative order of the objects being compared.</returns>
    public static int CompareToNullable<T>(this T left, T? right) where T : struct, IComparable<T> =>
        right.HasValue ? left.CompareTo(right.Value) : 1;

    /// <summary>
    /// Compares the current <see cref="IComparable{T}"/> instance with the specified <see cref="Object"/> instance.
    /// </summary>
    /// <param name="left">The left-hand <see cref="IComparable{T}"/> instance to compare.</param>
    /// <param name="right">The right-hand <see cref="Object"/> instance to compare.</param>
    /// <typeparam name="T">The underlying type of the current <see cref="IComparable{T}"/>.</typeparam>
    /// <returns>Returns a signed integer that indicates the relative order of the objects being compared.</returns>
    /// <exception cref="ArgumentException">If the specified object is not <see langword="null"/>, or of the specified type.</exception>
    public static int CompareToObject<T>(this IComparable<T> left, object? right) => right switch
    {
        null => 1,
        T other => left.CompareTo(other),
        _ => throw new ArgumentException($"Object must be of type {typeof(T).FullName}", nameof(right))
    };

    /// <summary>
    /// Compares the current <see cref="IComparable{T}"/> instance with the specified <see cref="Object"/> instance.
    /// </summary>
    /// <param name="left">The left-hand <see cref="IComparable{T}"/> instance to compare.</param>
    /// <param name="right">The right-hand <see cref="Object"/> instance to compare.</param>
    /// <typeparam name="T">The underlying type of the current <see cref="IComparable{T}"/>.</typeparam>
    /// <returns>Returns a signed integer that indicates the relative order of the objects being compared.</returns>
    /// <exception cref="ArgumentException">If the specified object is not <see langword="null"/>, or of the specified type.</exception>
    public static int CompareToObject<T>(this T left, object? right) where T : IComparable<T> => right switch
    {
        null => 1,
        T other => left.CompareTo(other),
        _ => throw new ArgumentException($"Object must be of type {typeof(T).FullName}", nameof(right))
    };

    /// <summary>
    /// Gets a record-like <see cref="String"/> representation of the current <see cref="Object"/> instance.
    /// <remarks>This method is designed specifically for record-like objects and may produce undesirable results when applied to primitive-like objects.</remarks>
    /// </summary>
    /// <param name="value">The current <see cref="Object"/> instance.</param>
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

            builder.Append(type.GetName());

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
            return string.Concat(type.GetName(), ObjectEmptyBrackets);
        }
    }

    /// <summary>
    /// Obtains a <see cref="String"/> representation of the current <see cref="Object"/>, or a string literal null if the current object is <see langword="null"/>.
    /// </summary>
    /// <param name="value">The current <see cref="Object"/> from which to obtain a <see cref="String"/> representation.</param>
    /// <returns>Returns a <see cref="String"/> representation of the current <see cref="Object"/>, or a string literal null if the current object is <see langword="null"/>.</returns>
    public static string ToStringOrNull(this object? value) => value?.ToString() ?? Null;
}
