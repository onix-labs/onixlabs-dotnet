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

namespace OnixLabs.Security.Cryptography;

public sealed partial class RsaPrivateKey
{
    /// <summary>
    /// Checks for equality between this instance and another object.
    /// </summary>
    /// <param name="other">The object to check for equality.</param>
    /// <returns>true if the object is equal to this instance; otherwise, <see langword="false"/>.</returns>
    public override bool Equals(PrivateKey? other)
    {
        return base.Equals(other) && other is RsaPrivateKey rsaOther && rsaOther.Padding == Padding;
    }
}
