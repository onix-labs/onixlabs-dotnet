// Copyright 2020-2025 ONIXLabs
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

namespace OnixLabs.Core.Text;

/// <summary>
/// Provides extension methods for <see cref="IBaseValue{T}"/> instances.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public static class IBaseValueExtensions
{
    /// <summary>
    /// Provides extension methods for <see cref="IBaseValue{T}"/> instances.
    /// </summary>
    /// <typeparam name="T">The underlying <see cref="IBaseValue{T}"/> type.</typeparam>
    extension<T>(T) where T : struct, IBaseValue<T>
    {
        /// <summary>
        /// Determines whether the specified <paramref name="left"/> <typeparamref name="T"/>
        /// value is equal to the specified <paramref name="right"/> <typeparamref name="T"/> value.
        /// </summary>
        /// <param name="left">The left-hand value to compare.</param>
        /// <param name="right">The right-hand value to compare.</param>
        /// <returns>
        /// Returns <see langword="true"/> if the specified <paramref name="left"/> <typeparamref name="T"/> value is equal to
        /// the specified <paramref name="right"/> <typeparamref name="T"/> value; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator ==(T left, T right) => T.Equals(left, right);

        /// <summary>
        /// Determines whether the specified <paramref name="left"/> <typeparamref name="T"/>
        /// value is not equal to the specified <paramref name="right"/> <typeparamref name="T"/> value.
        /// </summary>
        /// <param name="left">The left-hand value to compare.</param>
        /// <param name="right">The right-hand value to compare.</param>
        /// <returns>
        /// Returns <see langword="true"/> if the specified <paramref name="left"/> <typeparamref name="T"/> value is not equal to
        /// the specified <paramref name="right"/> <typeparamref name="T"/> value; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator !=(T left, T right) => !T.Equals(left, right);
    }
}
