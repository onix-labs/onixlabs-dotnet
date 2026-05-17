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

namespace OnixLabs.Units;

public readonly partial struct Speed<T>
{
    /// <inheritdoc/>
    /// <remarks>
    /// The denominator is set to <c>1 s</c> rather than <see cref="Time{T}.Zero"/> to avoid a <c>0 / 0</c> magnitude,
    /// which would yield <c>NaN</c> for floating-point types or throw for integer-shaped types, breaking equality,
    /// comparison, and formatting. With a non-zero denominator the magnitude is a genuine <c>0</c>.
    /// </remarks>
    public static Speed<T> Zero => new(Distance<T>.Zero, Time<T>.FromSeconds(T.One));

    private const string DefaultFormat = "m/s";
}
