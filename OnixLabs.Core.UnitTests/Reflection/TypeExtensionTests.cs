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

using System;
using System.Collections.Generic;
using OnixLabs.Core.Reflection;
using Xunit;

namespace OnixLabs.Core.UnitTests.Reflection;

public sealed class TypeExtensionTests
{
    [Theory(DisplayName = "Type.GetName should produce the expected result")]
    [InlineData(typeof(object), TypeNameFlags.None, "Object")]
    [InlineData(typeof(List<>), TypeNameFlags.None, "List")]
    [InlineData(typeof(Dictionary<,>), TypeNameFlags.None, "Dictionary")]
    [InlineData(typeof(object), TypeNameFlags.UseFullNames, "System.Object")]
    [InlineData(typeof(List<>), TypeNameFlags.UseFullNames, "System.Collections.Generic.List")]
    [InlineData(typeof(Dictionary<,>), TypeNameFlags.UseFullNames, "System.Collections.Generic.Dictionary")]
    [InlineData(typeof(object), TypeNameFlags.UseGenericTypeArguments, "Object")]
    [InlineData(typeof(List<>), TypeNameFlags.UseGenericTypeArguments, "List<>")]
    [InlineData(typeof(Dictionary<,>), TypeNameFlags.UseGenericTypeArguments, "Dictionary<>")]
    [InlineData(typeof(List<Action<int>>), TypeNameFlags.UseGenericTypeArguments, "List<Action<Int32>>")]
    [InlineData(typeof(Dictionary<string, ISet<Guid>>), TypeNameFlags.UseGenericTypeArguments, "Dictionary<String, ISet<Guid>>")]
    [InlineData(typeof(List<Action<int>>), TypeNameFlags.All, "System.Collections.Generic.List<System.Action<System.Int32>>")]
    [InlineData(typeof(Dictionary<string, ISet<Guid>>), TypeNameFlags.All, "System.Collections.Generic.Dictionary<System.String, System.Collections.Generic.ISet<System.Guid>>")]
    public void TypeGetNameShouldProduceExpectedResult(Type type, TypeNameFlags flags, string expected)
    {
        // When
        string actual = type.GetName(flags);

        // Then
        Assert.Equal(expected, actual);
    }
}
