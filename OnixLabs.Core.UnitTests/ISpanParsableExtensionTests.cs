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
using System.Globalization;

namespace OnixLabs.Core.UnitTests;

// ReSharper disable once InconsistentNaming
public sealed class ISpanParsableExtensionTests
{
    [Fact(DisplayName = "ISpanParsable.ParseOrThrow should return the parsed value when the input is valid")]
    public void ParseOrThrowShouldReturnParsedValueWhenInputIsValid()
    {
        // When
        int actual = int.ParseOrThrow("123", CultureInfo.InvariantCulture);

        // Then
        Assert.Equal(123, actual);
    }

    [Fact(DisplayName = "ISpanParsable.ParseOrThrow should throw FormatException when the input cannot be parsed")]
    public void ParseOrThrowShouldThrowWhenInputCannotBeParsed()
    {
        // When / Then
        Assert.Throws<FormatException>(() => int.ParseOrThrow("not-a-number", CultureInfo.InvariantCulture));
    }
}
