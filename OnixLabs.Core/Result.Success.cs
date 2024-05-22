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

public sealed class Success<T> : Result<T>, IValueEquatable<Success<T>>
{
    public Success(T value) => Value = value;

    public T Value { get; }

    public static bool operator ==(Success<T> left, Success<T> right) => Equals(left, right);

    public static bool operator !=(Success<T> left, Success<T> right) => Equals(left, right);

    public bool Equals(Success<T>? other) => ReferenceEquals(this, other) || other is not null && Equals(other.Value, Value);

    public override bool Equals(object? obj) => Equals(obj as Success<T>);

    public override int GetHashCode() => Value?.GetHashCode() ?? default;

    public override T? GetValueOrDefault() => Value;

    public override T GetValueOrDefault(T defaultValue) => Value ?? defaultValue;

    public override T GetValueOrThrow() => Value;

    public override void Match(Action<T> success, Action<Exception> failure) => success(Value);

    public override TResult Match<TResult>(Func<T, TResult> success, Func<Exception, TResult> failure) => success(Value);

    public override Result<TResult> Select<TResult>(Func<T, TResult> selector) => selector(Value);

    public override Result<TResult> SelectMany<TResult>(Func<T, Result<TResult>> selector) => selector(Value);

    public override string ToString() => Value?.ToString() ?? string.Empty;
}
