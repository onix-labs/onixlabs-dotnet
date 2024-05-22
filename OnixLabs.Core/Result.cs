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

namespace OnixLabs.Core;

public abstract class Result<T> : IValueEquatable<Result<T>>
{
    public bool IsSuccess => this is Success<T>;
    public bool IsFailure => this is Failure<T>;

    public static Result<T> Of(Func<T> func)
    {
        try
        {
            return Success(func());
        }
        catch (Exception exception)
        {
            return Failure(exception);
        }
    }

    public static Result<T> Success(T value) => new Success<T>(value);
    public static Result<T> Failure(Exception exception) => new Failure<T>(exception);

    public static implicit operator Result<T>(T value) => Success(value);
    public static explicit operator T(Result<T> value) => value.GetValueOrThrow();

    public static bool operator ==(Result<T> left, Result<T> right) => Equals(left, right);
    public static bool operator !=(Result<T> left, Result<T> right) => !Equals(left, right);

    public bool Equals(Result<T>? other) => this switch
    {
        Success<T> success => other is Success<T> successOther && success.Equals(successOther),
        Failure<T> failure => other is Failure<T> failureOther && failure.Equals(failureOther),
        _ => ReferenceEquals(this, other)
    };

    public override bool Equals(object? obj) => Equals(obj as Result<T>);

    public override int GetHashCode() => default;

    public abstract T? GetValueOrDefault();
    public abstract T GetValueOrDefault(T defaultValue);
    public abstract T GetValueOrThrow();
    public abstract void Match(Action<T> success, Action<Exception> failure);
    public abstract TResult Match<TResult>(Func<T, TResult> success, Func<Exception, TResult> failure);
    public abstract Result<TResult> Select<TResult>(Func<T, TResult> selector) where TResult : notnull;
    public abstract Result<TResult> SelectMany<TResult>(Func<T, Result<TResult>> selector) where TResult : notnull;

    public override string ToString() => this switch
    {
        Success<T> success => success.ToString(),
        Failure<T> failure => failure.ToString(),
        _ => base.ToString() ?? GetType().FullName ?? nameof(Result<T>)
    };
}
