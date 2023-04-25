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

using OnixLabs.Core.Collections;

namespace OnixLabs.Core.Text;

public readonly partial struct Base32
{
    /// <summary>
    /// Gets the default padding option when creating new Base-32 instances.
    /// </summary>
    private const bool DefaultPadding = true;
    
    /// <summary>
    /// Gets an empty <see cref="Base32"/> value.
    /// </summary>
    public static Base32 Empty => Create(Collection.EmptyArray<byte>());
}
