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

namespace OnixLabs.Security.Cryptography;

public sealed partial class EcdhPublicKey
{
    private const string KeyName = "ECDH";

    /// <summary>
    /// Creates a new <see cref="NamedPublicKey"/> from the current <see cref="EcdhPublicKey"/> instance.
    /// </summary>
    /// <returns>Returns a new <see cref="NamedPublicKey"/> from the current <see cref="EcdhPublicKey"/> instance.</returns>
    public override NamedPublicKey ToNamedPublicKey() => new(this, KeyName);
}
