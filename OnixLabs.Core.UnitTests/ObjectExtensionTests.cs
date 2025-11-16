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
using System.Threading.Tasks;
using OnixLabs.Core.UnitTests.Data;

namespace OnixLabs.Core.UnitTests;

public sealed class ObjectExtensionTests
{
    [Fact(DisplayName = "Apply should produce the expected result (reference type)")]
    public void ApplyShouldProduceExpectedResultReferenceType()
    {
        // Given
        Mutable value = new() { Value = 123 };

        // When
        Mutable result = value.Apply(it => it.Value = 456);

        // Then
        Assert.Equal(456, value.Value);
        Assert.Same(value, result);
    }

    [Fact(DisplayName = "Apply should produce the expected result (value type)")]
    public void ApplyShouldProduceExpectedResultValueType()
    {
        // Given
        int value = 123;

        // When
        value = value.Apply(it => it * 2);

        // Then
        Assert.Equal(246, value);
    }

    [Fact(DisplayName = "CompareToObject should produce zero if the current IComparable<T> is equal to the specified object.")]
    public void CompareToObjectShouldProduceZeroIfTheCurrentIComparableIsEqualToTheSpecifiedObject()
    {
        // Given
        const int expected = 0;

        // When
        int actual = 123.CompareToObject(123);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "CompareToObject should produce positive one if the specified object is null.")]
    public void CompareToObjectShouldProducePositiveOneIfTheSpecifiedObjectIsNull()
    {
        // Given
        const int expected = 1;

        // When
        int actual = 123.CompareToObject(null);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "CompareToObject should produce positive one if the current IComparable<T> greater than the specified object.")]
    public void CompareToObjectShouldProducePositiveOneIfTheCurrentIComparableIsGreaterThanTheSpecifiedObject()
    {
        // Given
        const int expected = 1;

        // When
        int actual = 124.CompareToObject(123);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "CompareToObject should produce negative one if the current IComparable<T> greater than the specified object.")]
    public void CompareToObjectShouldProduceNegativeOneIfTheCurrentIComparableIsGreaterThanTheSpecifiedObject()
    {
        // Given
        const int expected = -1;

        // When
        int actual = 122.CompareToObject(123);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "CompareToObject should throw ArgumentException if the specified object is of the incorrect type.")]
    public void CompareToObjectShouldThrowArgumentExceptionIfSpecifiedObjectIsOfIncorrectType()
    {
        // When
        Exception exception = Assert.Throws<ArgumentException>(() => 122.CompareToObject(123.456));

        // Then
        Assert.Equal("Object must be of type System.Int32 (Parameter 'right')", exception.Message);
    }

    [Theory(DisplayName = "IsWithinRangeInclusive should produce the expected result")]
    [InlineData(2, 1, 3, true)]
    [InlineData(1, 1, 3, true)]
    [InlineData(3, 1, 3, true)]
    [InlineData(0, 1, 3, false)]
    [InlineData(4, 1, 3, false)]
    public void IsWithinRangeInclusiveShouldProduceExpectedResult(int value, int min, int max, bool expected)
    {
        // When
        bool actual = value.IsWithinRangeInclusive(min, max);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "IsWithinRangeExclusive should produce the expected result")]
    [InlineData(2, 1, 3, true)]
    [InlineData(1, 1, 3, false)]
    [InlineData(3, 1, 3, false)]
    [InlineData(0, 1, 3, false)]
    [InlineData(4, 1, 3, false)]
    public void IsWithinRangeExclusiveShouldProduceExpectedResult(int value, int min, int max, bool expected)
    {
        // When
        bool actual = value.IsWithinRangeExclusive(min, max);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Let should produce the expected result")]
    public void LetShouldProduceExpectedResult()
    {
        // Given
        const string value = "123";

        // When
        int result = value
            .Let(int.Parse)
            .Let(it => it * 2);

        // Then
        Assert.Equal(246, result);
    }

    [Fact(DisplayName = "ToRecordString should produce null when the object is null")]
    public void ToRecordStringShouldProduceNullWhenObjectIsNull()
    {
        // Given
        const string expected = "null";
        Record<decimal>? record = null;

        // When
        string actual = record.ToRecordString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ToRecordString should produce a record formatted string")]
    public void ToRecordStringShouldProduceExpectedResult()
    {
        // Given
        const string expected = "Record { Text = abc, Number = 123, Value = 123.456, Values = null }";
        Record<decimal> record = new("abc", 123, 123.456m);

        // When
        string actual = record.ToRecordString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ToRecordString should produce a record formatted string with nullable property")]
    public void ToRecordStringShouldProduceExpectedResultWithNullableProperty()
    {
        // Given
        const string expected = "Record { Text = abc, Number = 123, Value = null, Values = null }";
        Record<decimal?> record = new("abc", 123, null);

        // When
        string actual = record.ToRecordString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ToRecordString should produce a record formatted string with collection")]
    public void ToRecordStringShouldProduceExpectedResultWithCollection()
    {
        // Given
        const string expected = "Record { Text = abc, Number = 123, Value = null, Values = [1, 2, 1.23, null] }";
        Record<decimal?> record = new("abc", 123, null, [1, 2, 1.23m, null]);

        // When
        string actual = record.ToRecordString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ToRecordString should produce an empty record")]
    public void ToRecordStringShouldProduceEmptyRecord()
    {
        // Given
        const string expected = "Int32 { }";
        const int record = 123;

        // When
        string actual = record.ToRecordString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ToRecordString catch should produce an empty record")]
    public void ToRecordStringCatchShouldProduceEmptyRecord()
    {
        // Given
        const string expected = "String { }";
        const string record = "";

        // When
        string actual = record.ToRecordString();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ToStringOrNull should produce null when the object is null")]
    public void ToStringOrNullShouldProduceNullWhenObjectIsNull()
    {
        // Given
        const string expected = "null";
        object? value = null;

        // When
        string actual = value.ToStringOrNull();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ToStringOrNull should produce expected result when the object is not null")]
    public void ToStringOrNullShouldProduceExpectedResultWhenObjectIsNotNull()
    {
        // Given
        const string expected = "abc";
        const string value = "abc";

        // When
        string actual = value.ToStringOrNull();

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "ToOptional should produce the expected result when using a non-null reference type")]
    public void ToOptionalShouldProduceExpectedResultWhenUsingNonNullReferenceType()
    {
        // Given
        const string expected = "abc";

        // When
        Optional<string> optional = expected.ToOptional();

        // Then
        Some<string> some = Assert.IsType<Some<string>>(optional);
        Assert.Equal(expected, some.Value);
    }

    [Fact(DisplayName = "ToOptional should produce the expected result when using a null reference type")]
    public void ToOptionalShouldProduceExpectedResultWhenUsingNullReferenceType()
    {
        // Given
        const string? expected = null;

        // When
        Optional<string> optional = expected.ToOptional();

        // Then
        Assert.IsType<None<string>>(optional);
    }

    [Fact(DisplayName = "ToOptional should produce the expected result when using a non-null value type")]
    public void ToOptionalShouldProduceExpectedResultWhenUsingNonNullValueType()
    {
        // Given
        const int expected = 123;

        // When
        Optional<int> optional = expected.ToOptional();

        // Then
        Some<int> some = Assert.IsType<Some<int>>(optional);
        Assert.Equal(expected, some.Value);
    }

    [Fact(DisplayName = "ToOptional should produce the expected result when using a null value type")]
    public void ToOptionalShouldProduceExpectedResultWhenUsingNullValueType()
    {
        // Given
        int? expected = null;

        // When
        Optional<int> optional = expected.ToOptional();

        // Then
        Assert.IsType<None<int>>(optional);
    }

    [Fact(DisplayName = "ToOptionalAsync should produce the expected result when using a non-null reference type")]
    public async Task ToOptionalAsyncShouldProduceExpectedResultWhenUsingNonNullReferenceType()
    {
        // Given
        const string expected = "abc";

        // When
        Optional<string> optional = await Task.FromResult<string?>(expected).ToOptionalAsync(token: TestContext.Current.CancellationToken);

        // Then
        Some<string> some = Assert.IsType<Some<string>>(optional);
        Assert.Equal(expected, some.Value);
    }

    [Fact(DisplayName = "ToOptionalAsync should produce the expected result when using a null reference type")]
    public async Task ToOptionalAsyncShouldProduceExpectedResultWhenUsingNullReferenceType()
    {
        // Given
        const string? expected = null;

        // When
        Optional<string> optional = await Task.FromResult(expected).ToOptionalAsync(token: TestContext.Current.CancellationToken);

        // Then
        Assert.IsType<None<string>>(optional);
    }

    [Fact(DisplayName = "ToOptionalAsync should produce the expected result when using a non-null value type")]
    public async Task ToOptionalAsyncShouldProduceExpectedResultWhenUsingNonNullValueType()
    {
        // Given
        const int expected = 123;

        // When
        Optional<int> optional = await Task.FromResult(expected).ToOptionalAsync(token: TestContext.Current.CancellationToken);

        // Then
        Some<int> some = Assert.IsType<Some<int>>(optional);
        Assert.Equal(expected, some.Value);
    }

    [Fact(DisplayName = "ToOptionalAsync should produce the expected result when using a null value type")]
    public async Task ToOptionalAsyncShouldProduceExpectedResultWhenUsingNullValueType()
    {
        // Given
        int? expected = null;

        // When
        Optional<int> optional = await Task.FromResult(expected).ToOptionalAsync(token: TestContext.Current.CancellationToken);

        // Then
        Assert.IsType<None<int>>(optional);
    }

    [Fact(DisplayName = "ToSuccess should produce the expected result")]
    public void ToSuccessShouldProduceTheExpectedResult()
    {
        // Given
        const string expected = "abc";

        // When
        Result<string> result = expected.ToSuccess();

        // Then
        Success<string> success = Assert.IsType<Success<string>>(result);
        Assert.Equal(expected, success.Value);
    }

    [Fact(DisplayName = "ToSuccessAsync should produce the expected result")]
    public async Task ToSuccessAsyncShouldProduceTheExpectedResult()
    {
        // Given
        const string expected = "abc";

        // When
        Task<string> task = Task.FromResult(expected);
        Result<string> result = await task.ToSuccessAsync(token: TestContext.Current.CancellationToken);

        // Then
        Success<string> success = Assert.IsType<Success<string>>(result);
        Assert.Equal(expected, success.Value);
    }

    [Fact(DisplayName = "TryGetNonNull should produce the expected result (true)")]
    public void TryGetNotNullShouldProduceExpectedResultTrue()
    {
        // Given
        const string? value = "Hello, World!";

        // When
        bool result = value.TryGetNonNull(out string output);

        // Then
        Assert.True(result);
        Assert.NotNull(output);
    }

    [Fact(DisplayName = "TryGetNonNull should produce the expected result (false)")]
    public void TryGetNotNullShouldProduceExpectedResultFalse()
    {
        // Given
        const string? value = null;

        // When
        bool result = value.TryGetNonNull(out string? output);

        // Then
        Assert.False(result);
        Assert.Null(output);
    }
}
