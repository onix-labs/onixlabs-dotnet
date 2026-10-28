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
using System.Runtime.CompilerServices;

namespace OnixLabs.Core;

/// <summary>
/// Represents an interpolated string handler for precondition messages, which formats the message only when the precondition fails.
/// </summary>
/// <remarks>
/// Why: a precondition such as <c>Require(found, $"Entry '{name}' not found in {typeof(T).Name}.")</c> would otherwise format its
/// message on every call, including the vast majority that succeed. The compiler passes the condition to this handler's constructor, and
/// when the condition holds no formatting happens at all, in the same way as <see cref="System.Diagnostics.Debug.Assert(bool)"/>.
/// It is used automatically for every interpolated string passed as a precondition message; callers never construct it themselves.
/// </remarks>
[InterpolatedStringHandler]
[EditorBrowsable(EditorBrowsableState.Never)]
public ref struct PreconditionInterpolatedStringHandler
{
    private DefaultInterpolatedStringHandler handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="PreconditionInterpolatedStringHandler"/> struct.
    /// </summary>
    /// <param name="literalLength">The number of constant characters outside the interpolation expressions.</param>
    /// <param name="formattedCount">The number of interpolation expressions.</param>
    /// <param name="condition">The precondition being checked; the message is only formatted when it is <see langword="false"/>.</param>
    /// <param name="shouldAppend">When this constructor returns, contains a value indicating whether the message should be formatted.</param>
    public PreconditionInterpolatedStringHandler(int literalLength, int formattedCount, bool condition, out bool shouldAppend)
    {
        shouldAppend = !condition;
        handler = shouldAppend ? new DefaultInterpolatedStringHandler(literalLength, formattedCount) : default;
    }

    /// <summary>
    /// Appends the specified literal text to the message.
    /// </summary>
    /// <param name="value">The literal text to append.</param>
    public void AppendLiteral(string value) => handler.AppendLiteral(value);

    /// <summary>
    /// Appends the specified value to the message.
    /// </summary>
    /// <param name="value">The value to append.</param>
    /// <typeparam name="T">The type of the value to append.</typeparam>
    public void AppendFormatted<T>(T value) => handler.AppendFormatted(value);

    /// <summary>
    /// Appends the specified value to the message, using the specified format.
    /// </summary>
    /// <param name="value">The value to append.</param>
    /// <param name="format">The format string.</param>
    /// <typeparam name="T">The type of the value to append.</typeparam>
    public void AppendFormatted<T>(T value, string? format) => handler.AppendFormatted(value, format);

    /// <summary>
    /// Appends the specified value to the message, using the specified alignment.
    /// </summary>
    /// <param name="value">The value to append.</param>
    /// <param name="alignment">The minimum width; a negative value left-aligns the value.</param>
    /// <typeparam name="T">The type of the value to append.</typeparam>
    public void AppendFormatted<T>(T value, int alignment) => handler.AppendFormatted(value, alignment);

    /// <summary>
    /// Appends the specified value to the message, using the specified alignment and format.
    /// </summary>
    /// <param name="value">The value to append.</param>
    /// <param name="alignment">The minimum width; a negative value left-aligns the value.</param>
    /// <param name="format">The format string.</param>
    /// <typeparam name="T">The type of the value to append.</typeparam>
    public void AppendFormatted<T>(T value, int alignment, string? format) => handler.AppendFormatted(value, alignment, format);

    /// <summary>
    /// Appends the specified characters to the message.
    /// </summary>
    /// <param name="value">The characters to append.</param>
    public void AppendFormatted(ReadOnlySpan<char> value) => handler.AppendFormatted(value);

    /// <summary>
    /// Appends the specified characters to the message, using the specified alignment and format.
    /// </summary>
    /// <param name="value">The characters to append.</param>
    /// <param name="alignment">The minimum width; a negative value left-aligns the value.</param>
    /// <param name="format">The format string, which is ignored for characters.</param>
    public void AppendFormatted(ReadOnlySpan<char> value, int alignment = 0, string? format = null) =>
        handler.AppendFormatted(value, alignment, format);

    /// <summary>
    /// Appends the specified string to the message.
    /// </summary>
    /// <param name="value">The string to append.</param>
    public void AppendFormatted(string? value) => handler.AppendFormatted(value);

    /// <summary>
    /// Appends the specified string to the message, using the specified alignment and format.
    /// </summary>
    /// <param name="value">The string to append.</param>
    /// <param name="alignment">The minimum width; a negative value left-aligns the value.</param>
    /// <param name="format">The format string, which is ignored for strings.</param>
    public void AppendFormatted(string? value, int alignment = 0, string? format = null) => handler.AppendFormatted(value, alignment, format);

    /// <summary>
    /// Appends the specified object to the message, using the specified alignment and format.
    /// </summary>
    /// <param name="value">The object to append.</param>
    /// <param name="alignment">The minimum width; a negative value left-aligns the value.</param>
    /// <param name="format">The format string.</param>
    public void AppendFormatted(object? value, int alignment = 0, string? format = null) => handler.AppendFormatted(value, alignment, format);

    /// <summary>
    /// Gets the formatted message and releases the handler's buffer.
    /// </summary>
    /// <returns>Returns the formatted message.</returns>
    internal string ToStringAndClear() => handler.ToStringAndClear();
}
