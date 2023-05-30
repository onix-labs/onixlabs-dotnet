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
/// Specifies scale modes.
/// </summary>
public enum ScaleMode
{
    /// <summary>
    /// Adjusts the scale by fixing the position of the current decimal point.
    /// For example, 123.4560 with a scale of 10 will become 123.4560000000
    /// </summary>
    Fixed = 0,

    /// <summary>
    /// Adjusts the scale by preserving the current unscaled value, and floating the position of the current decimal point.
    /// For example, 123.4560 with a scale of 10 will become 0.0001234560
    /// </summary>
    Floating = 1,

    /// <summary>
    /// Adjusts the scale by shifting the current unscaled value absolutely to the left of the current decimal point.
    /// For example, 123.4560 with a scale of 10 will become 1234560.0000000000
    /// </summary>
    Absolute = 2
}
