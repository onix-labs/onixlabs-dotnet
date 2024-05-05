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

using System.Security.Cryptography;

namespace OnixLabs.Security.Cryptography;

/// <summary>
///  Defines a mechanism for computing the <see cref="Hash"/> of a data structure.
/// </summary>
public interface IHashable
{
    /// <summary>
    /// Computes the <see cref="Hash"/> hash of the current object.
    /// </summary>
    /// <returns>Returns the computed <see cref="Hash"/> hash of the current object.</returns>
    Hash ComputeHash(HashAlgorithm algorithm);
}
