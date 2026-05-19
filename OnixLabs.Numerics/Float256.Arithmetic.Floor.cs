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

public readonly partial struct Float256
{
    /// <summary>Returns the largest integer less than or equal to the specified <see cref="Float256"/> value.</summary>
    /// <param name="value">The value whose floor is to be computed.</param>
    /// <returns>Returns the floor of <paramref name="value"/>.</returns>
    public static Float256 Floor(Float256 value)
    {
        if (!IsFinite(value) || IsZero(value)) return value;
        Float256 truncated = Truncate(value);
        if (truncated == value) return value;
        return IsNegative(value) ? truncated - One : truncated;
    }
}
