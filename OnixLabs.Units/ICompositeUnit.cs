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
/// Defines a composite unit of measurement composed of two underlying units.
/// </summary>
/// <typeparam name="TLeft">The underlying type of the left-hand unit of measurement.</typeparam>
/// <typeparam name="TRight">The underlying type of the right-hand unit of measurement.</typeparam>
public interface ICompositeUnit<out TLeft, out TRight> where TLeft : struct, IUnit<TLeft> where TRight : struct, IUnit<TRight>
{
    /// <summary>
    /// Gets the left-hand unit of the composite.
    /// </summary>
    TLeft Left { get; }

    /// <summary>
    /// Gets the right-hand unit of the composite.
    /// </summary>
    TRight Right { get; }
}
