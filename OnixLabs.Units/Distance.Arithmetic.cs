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

public readonly partial struct Distance<T>
{
    /// <inheritdoc/>
    public static Distance<T> Add(Distance<T> left, Distance<T> right) => new(left.QuectoMeters + right.QuectoMeters);

    /// <inheritdoc/>
    public static Distance<T> Subtract(Distance<T> left, Distance<T> right) => new(left.QuectoMeters - right.QuectoMeters);

    /// <inheritdoc/>
    public static Distance<T> Multiply(Distance<T> left, Distance<T> right) => new(left.QuectoMeters * right.QuectoMeters);

    /// <inheritdoc/>
    public static Distance<T> Divide(Distance<T> left, Distance<T> right) => new(left.QuectoMeters / right.QuectoMeters);
}
