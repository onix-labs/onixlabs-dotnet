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

public sealed class Failure<T> : Result<T>, IValueEquatable<Failure<T>>
{
    public Failure(Exception exception) => Exception = exception;

    public Exception Exception { get; }

    public static bool operator ==(Failure<T> left, Failure<T> right) => Equals(left, right);

    public static bool operator !=(Failure<T> left, Failure<T> right) => !Equals(left, right);

    public bool Equals(Failure<T>? other) => ReferenceEquals(this, other) || other is not null && Equals(other.Exception, Exception);

    public override bool Equals(object? obj) => Equals(obj as Failure<T>);

    public override int GetHashCode() => Exception.GetHashCode();

    public override T? GetValueOrDefault() => default;

    public override T GetValueOrDefault(T defaultValue) => defaultValue;

    public override T GetValueOrThrow() => throw Exception;

    public override void Match(Action<T> success, Action<Exception> failure) => failure(Exception);

    public override TResult Match<TResult>(Func<T, TResult> success, Func<Exception, TResult> failure) => failure(Exception);

    public override Result<TResult> Select<TResult>(Func<T, TResult> selector) => Result<TResult>.Failure(Exception);

    public override Result<TResult> SelectMany<TResult>(Func<T, Result<TResult>> selector) => Result<TResult>.Failure(Exception);

    public override string ToString() => Exception.ToString();
}
