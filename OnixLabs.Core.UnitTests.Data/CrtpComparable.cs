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

namespace OnixLabs.Core.UnitTests.Data;

/// <summary>
/// Mirror of the F-bounded / CRTP pattern used by <c>OnixLabs.Core.Enumeration&lt;T&gt;</c>:
/// the base class implements <see cref="IComparable{T}"/> against the derived type rather
/// than itself. Used to exercise the overload of <c>CompareToObject</c> that targets
/// <see cref="IComparable{T}"/> directly (as opposed to the self-comparable form).
/// </summary>
/// <typeparam name="T">The concrete derived type, constrained to <see cref="CrtpComparable{T}"/>.</typeparam>
public abstract class CrtpComparable<T>(int order) : IComparable<T> where T : CrtpComparable<T>
{
    private int Order { get; } = order;

    public int CompareTo(T? other) => other is null ? 1 : Order.CompareTo(other.Order);
}

/// <summary>
/// A concrete derived type for use in tests that need a CRTP-shaped comparable receiver.
/// </summary>
public sealed class CrtpComparableDerived(int order) : CrtpComparable<CrtpComparableDerived>(order);
