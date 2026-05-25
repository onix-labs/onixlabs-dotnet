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
/// A minimal <see cref="Enumeration{T}"/>-derived type that exposes a public constructor,
/// allowing tests to create arbitrary entries directly — useful for exercising guard
/// behaviour and comparison edge-cases (e.g. equal <see cref="Enumeration{T}.Value"/>
/// with differing <see cref="Enumeration{T}.Name"/>) that the standard
/// <see langword="public"/> <see langword="static"/> <see langword="readonly"/> field
/// pattern cannot express.
/// </summary>
public sealed class TestEnumeration(int value, string name) : Enumeration<TestEnumeration>(value, name);
