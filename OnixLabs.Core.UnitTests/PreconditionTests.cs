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
using Xunit;
using static OnixLabs.Core.Preconditions;

namespace OnixLabs.Core.UnitTests;

public sealed class PreconditionTests
{
    [Fact(DisplayName = "Check should throw an ArgumentException when the condition is false")]
    public void CheckShouldProduceExpectedResult()
    {
        // When
        InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => Check(false));

        // Then
        Assert.Equal("Check requirement failed.", exception.Message);
    }

    [Fact(DisplayName = "CheckNotNull should throw an ArgumentNullException when the condition is null")]
    public void CheckNotNullShouldProduceExpectedResult()
    {
        // When
        InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => CheckNotNull<object>(null));

        // Then
        Assert.Equal("Argument must not be null.", exception.Message);
    }

    [Fact(DisplayName = "Require should throw an ArgumentException when the condition is false")]
    public void RequireShouldProduceExpectedResult()
    {
        // When
        ArgumentException exception = Assert.Throws<ArgumentException>(() => Require(false));

        // Then
        Assert.Equal("Argument requirement failed.", exception.Message);
    }

    [Fact(DisplayName = "RequireNotNull should throw an ArgumentNullException when the condition is null")]
    public void RequireNotNullShouldProduceExpectedResult()
    {
        // When
        ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => RequireNotNull<object>(null));

        // Then
        Assert.Equal("Argument must not be null.", exception.Message);
    }
}
