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

using System;
using System.Collections.Generic;
using OnixLabs.Core.Reflection;
using Xunit;

namespace OnixLabs.Core.UnitTests.Reflection;

public sealed class TypeExtensionTests
{
    [Theory(DisplayName = "Type.GetName should produce the expected result")]
    [InlineData(typeof(object), "Object")]
    [InlineData(typeof(List<>), "List")]
    [InlineData(typeof(Dictionary<,>), "Dictionary")]
    public void TypeGetNameShouldProduceExpectedResult(Type type, string expected)
    {
        // When
        string actual = type.GetName();

        // Then
        Assert.Equal(expected, actual);
    }
}
