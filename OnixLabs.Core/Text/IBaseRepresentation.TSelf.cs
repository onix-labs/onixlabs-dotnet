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

namespace OnixLabs.Core.Text;

/// <summary>
/// Defines a number base representation.
/// </summary>
/// <typeparam name="TSelf">The underlying type of the number base representation.</typeparam>
public interface IBaseRepresentation<TSelf> : IBaseRepresentation, IEquatable<TSelf>, ISpanParsable<TSelf> where TSelf : struct, IBaseRepresentation<TSelf>
{
    /// <summary>
    /// Gets an empty <see typeparam="TSelf"/> value.
    /// </summary>
    public static abstract TSelf Empty { get; }

    /// <summary>
    /// Performs an equality check between two <see typeparam="TSelf"/> instances.
    /// </summary>
    /// <param name="left">The left-hand <see typeparam="TSelf"/> instance to compare.</param>
    /// <param name="right">The right-hand <see typeparam="TSelf"/> instance to compare.</param>
    /// <returns>Returns <see langword="true"/> if the instances are equal; otherwise, <see langword="false"/>.</returns>
    public static abstract bool operator ==(TSelf left, TSelf right);

    /// <summary>
    /// Performs an inequality check between two <see typeparam="TSelf"/> instances.
    /// </summary>
    /// <param name="left">The left-hand <see typeparam="TSelf"/> instance to compare.</param>
    /// <param name="right">The right-hand <see typeparam="TSelf"/> instance to compare.</param>
    /// <returns>Returns <see langword="true"/> if the instances are not equal; otherwise, <see langword="false"/>.</returns>
    public static abstract bool operator !=(TSelf left, TSelf right);
}
