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

namespace OnixLabs.Core.UnitTests;

// Why: these switch expressions have no default arm. They compile without CS8509 only because Result, Result<T> and Optional<T>
// are closed hierarchies, so a new direct subclass would surface here as a build warning before it reached consumers.
public sealed class ClosedHierarchyTests
{
    [Fact(DisplayName = "Result should be exhaustively matched by its Success and Failure cases")]
    public void ResultShouldBeExhaustivelyMatchedByItsCases()
    {
        // Given
        Result success = Result.Success();
        Result failure = Result.Failure(new InvalidOperationException("failed"));

        // When
        string successText = Describe(success);
        string failureText = Describe(failure);

        // Then
        Assert.Equal("success", successText);
        Assert.Equal("failed", failureText);

        static string Describe(Result result) => result switch
        {
            Success => "success",
            Failure failureResult => failureResult.Exception.Message
        };
    }

    [Fact(DisplayName = "Result<T> should be exhaustively matched by its Success<T> and Failure<T> cases")]
    public void GenericResultShouldBeExhaustivelyMatchedByItsCases()
    {
        // Given
        Result<int> success = Result<int>.Success(42);
        Result<int> failure = Result<int>.Failure(new InvalidOperationException("failed"));

        // When
        string successText = Describe(success);
        string failureText = Describe(failure);

        // Then
        Assert.Equal("42", successText);
        Assert.Equal("failed", failureText);

        static string Describe(Result<int> result) => result switch
        {
            Success<int> successResult => successResult.Value.ToString(),
            Failure<int> failureResult => failureResult.Exception.Message
        };
    }

    [Fact(DisplayName = "Optional<T> should be exhaustively matched by its Some<T> and None<T> cases")]
    public void OptionalShouldBeExhaustivelyMatchedByItsCases()
    {
        // Given
        Optional<int> some = Optional<int>.Some(42);
        Optional<int> none = Optional<int>.None;

        // When
        string someText = Describe(some);
        string noneText = Describe(none);

        // Then
        Assert.Equal("42", someText);
        Assert.Equal("none", noneText);

        static string Describe(Optional<int> optional) => optional switch
        {
            Some<int> someOptional => someOptional.Value.ToString(),
            None<int> => "none"
        };
    }
}
