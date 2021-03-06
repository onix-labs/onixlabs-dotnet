// Copyright 2020-2022 ONIXLabs
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

using System;

namespace OnixLabs.Core.Text;

public readonly partial struct Base64
{
    /// <summary>
    /// Gets an empty Base-64 value.
    /// </summary>
    public static readonly Base64 Empty;

    /// <summary>
    /// Initializes static members of the <see cref="Base64"/> class.
    /// </summary>
    static Base64()
    {
        Empty = FromByteArray(Array.Empty<byte>());
    }
}
