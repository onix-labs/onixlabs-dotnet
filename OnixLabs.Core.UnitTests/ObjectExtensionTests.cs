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
using OnixLabs.Core.UnitTests.Data.Objects;
using Xunit;
using Record = OnixLabs.Core.UnitTests.Data.Objects.Record;

namespace OnixLabs.Core.UnitTests;

public sealed class ObjectExtensionTests
{
    [Fact(DisplayName = "Object.ToRecordString should produce a record formatted string")]
    public void ToRecordStringShouldProduceExpectedResult()
    {
        // Given
        Record record = new("abc", 123, DateTime.Now, Guid.NewGuid());
        string expected = record.ToString();

        // When
        string actual = record.ToRecordString();

        // Then
        Assert.Equal(expected, actual);
    }
}
