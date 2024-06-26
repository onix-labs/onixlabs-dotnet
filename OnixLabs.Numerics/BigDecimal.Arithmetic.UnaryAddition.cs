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

namespace OnixLabs.Numerics;

public readonly partial struct BigDecimal
{
    /// <summary>
    /// Computes the unary addition of the specified <see cref="BigDecimal"/> value.
    /// </summary>
    /// <param name="value">The value for which to perform unary addition.</param>
    /// <returns>Returns the unary addition of the specified <see cref="BigDecimal"/> value.</returns>
    public static BigDecimal UnaryAdd(BigDecimal value) => value;

    /// <summary>
    /// Computes the unary addition of the specified <see cref="BigDecimal"/> value.
    /// </summary>
    /// <param name="value">The value for which to perform unary addition.</param>
    /// <returns>Returns the unary addition of the specified <see cref="BigDecimal"/> value.</returns>
    public static BigDecimal operator +(BigDecimal value) => UnaryAdd(value);
}
