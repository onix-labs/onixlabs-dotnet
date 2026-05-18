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

public readonly partial struct Float128
{
    /// <summary>
    /// Gets the absolute value of the specified <see cref="Float128"/> value.
    /// </summary>
    /// <param name="value">The <see cref="Float128"/> from which to obtain an absolute value.</param>
    /// <returns>Returns the absolute value of the specified <see cref="Float128"/> value.</returns>
    /// <remarks>
    /// The result preserves NaN payloads; for any NaN input, the result is also a NaN.
    /// Negative zero becomes positive zero, and negative infinity becomes positive infinity.
    /// </remarks>
    public static Float128 Abs(Float128 value) => new(value.Bits & ~SignMask);
}
