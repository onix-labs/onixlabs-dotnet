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
    [Fact(DisplayName = "Type.GetSimpleName from non-generic type should produce the expected type name")]
    public void GetSimpleNameShouldProduceExpectedResultFromNonGenericType()
    {
        // Given
        Type type = typeof(object);
        const string expected = "Object";

        // When
        string actual = type.GetSimpleName();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Type.GetSimpleName from generic type should produce the expected type name")]
    public void GetSimpleNameShouldProduceExpectedResultFromGenericType()
    {
        // Given
        Type type = typeof(Dictionary<string, List<int>>);
        const string expected = "Dictionary";

        // When
        string actual = type.GetSimpleName();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Type.GetFormattedName from non-generic type should produce the expected type name")]
    public void GetFormattedNameShouldProduceExpectedResultFromNonGenericType()
    {
        // Given
        Type type = typeof(object);
        const string expected = "Object";

        // When
        string actual = type.GetFormattedName();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Type.GetFormattedName from generic type should produce the expected type name")]
    public void GetFormattedNameShouldProduceExpectedResultFromGenericType()
    {
        // Given
        Type type = typeof(Dictionary<string, List<int>>);
        const string expected = "Dictionary<String, List<Int32>>";

        // When
        string actual = type.GetFormattedName();

        // Then
        Assert.Equal(expected, actual);
    }
}
