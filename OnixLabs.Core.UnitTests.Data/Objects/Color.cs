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

namespace OnixLabs.Core.UnitTests.Data.Objects;

public sealed class Color : Enumeration<Color>
{
    public static readonly Color Red = new(1, nameof(Red));
    public static readonly Color Green = new(2, nameof(Green));
    public static readonly Color Blue = new(3, nameof(Blue));

    private Color(int value, string name) : base(value, name)
    {
    }
}
