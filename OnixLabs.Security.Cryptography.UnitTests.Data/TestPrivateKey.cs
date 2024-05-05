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

namespace OnixLabs.Security.Cryptography.UnitTests.Data;

public sealed class TestPrivateKey(ReadOnlySpan<byte> value) : PrivateKey(value)
{
    public override PublicKey GetPublicKey() => new TestPublicKey([]);
    public override byte[] ExportPkcs8PrivateKey() => [];
    public override byte[] ExportPkcs8PrivateKey(ReadOnlySpan<char> password, PbeParameters parameters) => [];
}