// Copyright 2020-2023 ONIXLabs
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

namespace OnixLabs.Core.Numerics;

/// <summary>
/// Represents decimal expansion information.
/// </summary>
public abstract partial class DecimalExpansion
{
    /// <summary>
    /// Creates a new instance of the <see cref="DecimalExpansion"/> class.
    /// </summary>
    internal DecimalExpansion()
    {
    }

    /// <summary>
    /// Gets the length of the integer part of the decimal expansion.
    /// </summary>
    public abstract int IntegerLength { get; }

    /// <summary>
    /// Gets the truncating fraction length of the decimal expansion.
    /// A truncating fraction length depends on the type of decimal expansion:
    /// <list type="bullet">
    ///     <item>The truncating fraction length of a <see cref="TerminatingDecimalExpansion"/> is the length of the full, terminating decimal expansion.</item>
    ///     <item>The truncating fraction length of a <see cref="RepeatingDecimalExpansion"/> is the length of the repetend offset, plus the length of a single repetend repetition.</item>
    ///     <item>The truncating fraction length of a <see cref="UnknownDecimalExpansion"/> is zero, since the length of the decimal expansion is unknown.</item>
    /// </list>
    /// </summary>
    internal abstract int TruncatingFractionLength { get; }

    /// <summary>
    /// Returns a <see cref="string"/> that represents the current object.
    /// </summary>
    /// <returns>A <see cref="string"/> that represents the current object.</returns>
    public sealed override string ToString()
    {
        return this.ToRecordString();
    }
}
