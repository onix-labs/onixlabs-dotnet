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

using System.Numerics;

namespace OnixLabs.Units;

/// <summary>
/// Defines a unit of measurement that has a magnitudinal representation.
/// </summary>
/// <typeparam name="T">The underlying <see cref="IFloatingPoint{TSelf}"/> type of the unit of measurement.</typeparam>
public interface IMagnitudinalUnit<out T> where T : IFloatingPoint<T>
{
    /// <summary>
    /// Gets the magnitudinal representation for the current unit of measurement.
    /// </summary>
    T Magnitude { get; }
}
