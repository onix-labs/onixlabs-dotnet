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

using System.Numerics;

namespace OnixLabs.Core.Numerics;

/// <summary>
/// Provides generic mathematical functions.
/// </summary>
internal static class GenericMath
{
    /// <summary>
    /// Calculates the power of the specified value, raised by the specified exponent.
    /// </summary>
    /// <param name="value">The value to raise to the power of the specified exponent.</param>
    /// <param name="exponent">The specified exponent to raise the value by.</param>
    /// <typeparam name="T">The underlying <see cref="INumber{TSelf}"/> type.</typeparam>
    /// <returns>Returns the power of the specified value, raised by the specified exponent.</returns>
    public static T Pow<T>(T value, int exponent) where T : INumber<T>
    {
        if (exponent == 0) return T.One;
        if (exponent == 1 || value == T.One) return value;

        T result = value;
        int count = int.Abs(exponent);

        while (--count > 0) result *= value;

        return exponent > 1 ? result : T.One / result;
    }
}
