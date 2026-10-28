// Copyright 2020 ONIXLabs
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

namespace OnixLabs.Core.UnitTests.Data;

/// <summary>
/// An <see cref="Enumeration{T}"/> whose entries clash: <see cref="Alpha"/> and <see cref="Bravo"/> share a value, and
/// <see cref="Alpha"/> and <see cref="AlphaAgain"/> share a name, so lookups by that value or name are ambiguous.
/// </summary>
public sealed class ClashingEnumeration : Enumeration<ClashingEnumeration>
{
    public static readonly ClashingEnumeration Alpha = new(1, nameof(Alpha));
    public static readonly ClashingEnumeration Bravo = new(1, nameof(Bravo));
    public static readonly ClashingEnumeration AlphaAgain = new(2, nameof(Alpha));
    public static readonly ClashingEnumeration Charlie = new(3, nameof(Charlie));

    private ClashingEnumeration(int value, string name) : base(value, name)
    {
    }
}
