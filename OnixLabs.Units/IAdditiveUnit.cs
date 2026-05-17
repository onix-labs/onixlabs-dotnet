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
/// Defines a unit of measurement that supports additive arithmetic (addition and subtraction).
/// </summary>
/// <typeparam name="TSelf">The underlying type of the unit of measurement.</typeparam>
public interface IAdditiveUnit<TSelf> : IUnit<TSelf> where TSelf : struct
{
    /// <summary>
    /// Computes the sum of the specified <typeparamref name="TSelf"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to add to.</param>
    /// <param name="right">The right-hand value to add.</param>
    /// <returns>Returns the sum of the specified <typeparamref name="TSelf"/> values.</returns>
    static abstract TSelf Add(TSelf left, TSelf right);

    /// <summary>
    /// Computes the difference between the specified <typeparamref name="TSelf"/> values.
    /// </summary>
    /// <param name="left">The left-hand value to subtract from.</param>
    /// <param name="right">The right-hand value to subtract.</param>
    /// <returns>Returns the difference between the specified <typeparamref name="TSelf"/> values.</returns>
    static abstract TSelf Subtract(TSelf left, TSelf right);
}
