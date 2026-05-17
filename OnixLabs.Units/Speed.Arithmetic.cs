// Copyright 2020-2025 ONIXLabs
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

namespace OnixLabs.Units;

public readonly partial struct Speed<T>
{
    /// <inheritdoc/>
    public static Speed<T> Add(Speed<T> left, Speed<T> right)
    {
        T sum = Magnitude(left.Left, left.Right) + Magnitude(right.Left, right.Right);
        return new Speed<T>(Distance<T>.FromMeters(sum), Time<T>.FromSeconds(T.One));
    }

    /// <inheritdoc/>
    public static Speed<T> Subtract(Speed<T> left, Speed<T> right)
    {
        T difference = Magnitude(left.Left, left.Right) - Magnitude(right.Left, right.Right);
        return new Speed<T>(Distance<T>.FromMeters(difference), Time<T>.FromSeconds(T.One));
    }
}
