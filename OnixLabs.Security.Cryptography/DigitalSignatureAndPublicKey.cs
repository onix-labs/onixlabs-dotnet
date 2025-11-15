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

/// <summary>
/// Represents a pair of a cryptography digital signature, and a named cryptographic public key.
/// </summary>
/// <param name="Signature">The underlying cryptographic digital signature.</param>
/// <param name="Key">The underlying named cryptographic public key.</param>
// ReSharper disable UnusedType.Global NotAccessedPositionalProperty.Global
public readonly record struct DigitalSignatureAndPublicKey(DigitalSignature Signature, NamedPublicKey Key);
