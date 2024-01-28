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

using System.Globalization;

namespace OnixLabs.Numerics;

public readonly partial struct NumberInfo
{
    /// <summary>
    /// Gets a <see cref="NumberInfo"/> instance representing zero.
    /// </summary>
    public static NumberInfo Zero => new(0, 0);

    /// <summary>
    /// Gets a <see cref="NumberInfo"/> instance representing one.
    /// </summary>
    public static NumberInfo One => new(1, 0);

    /// <summary>
    /// Gets a <see cref="NumberInfo"/> instance representing negative one.
    /// </summary>
    public static NumberInfo NegativeOne => new(-1, 0);

    /// <summary>
    /// Gets the default number format.
    /// </summary>
    private const string DefaultNumberFormat = "G";

    /// <summary>
    /// Gets the default number styles for parsing decimal values.
    /// </summary>
    private const NumberStyles DefaultNumberStyles = NumberStyles.Any;

    /// <summary>
    /// Gets the default culture for formatting and parsing operations.
    /// </summary>
    private static readonly CultureInfo DefaultCulture = CultureInfo.CurrentCulture;
}
