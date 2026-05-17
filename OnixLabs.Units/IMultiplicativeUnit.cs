// Copyright 2020-2026 ONIXLabs
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

namespace OnixLabs.Units;

/// <summary>
/// Defines a unit of measurement that supports multiplicative arithmetic (multiplication and division).
/// </summary>
/// <typeparam name="TSelf">The underlying type of the unit of measurement.</typeparam>
public interface IMultiplicativeUnit<TSelf> : IUnit<TSelf> where TSelf : struct
{
    /// <summary>
    /// Computes the product of the specified <typeparamref name="TSelf"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to multiply.</param>
    /// <param name="right">The right-hand value to multiply by.</param>
    /// <returns>Returns the product of the specified <typeparamref name="TSelf"/> values.</returns>
    static abstract TSelf Multiply(TSelf left, TSelf right);

    /// <summary>
    /// Computes the quotient of the specified <typeparamref name="TSelf"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to divide.</param>
    /// <param name="right">The right-hand value to divide by.</param>
    /// <returns>Returns the quotient of the specified <typeparamref name="TSelf"/> values.</returns>
    static abstract TSelf Divide(TSelf left, TSelf right);
}
