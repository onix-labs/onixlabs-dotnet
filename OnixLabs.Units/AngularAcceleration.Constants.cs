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

public readonly partial struct AngularAcceleration<T>
{
    /// <inheritdoc/>
    /// <remarks>Non-zero denominator (1 s) avoids 0/0 = NaN; numerator zero gives a genuine zero magnitude.</remarks>
    public static AngularAcceleration<T> Zero => new(AngularVelocity<T>.Zero, Time<T>.FromSeconds(T.One));

    private const string DefaultFormat = "rad/s/s";
}
