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
using System.Linq;
using OnixLabs.Core.Linq;

namespace OnixLabs.Core.Text;

public readonly partial struct Base64
{
    /// <summary>
    /// Checks for equality between the current instance and another object.
    /// </summary>
    /// <param name="other">The object to check for equality.</param>
    /// <returns>Returns true if the object is equal to the current instance; otherwise, false.</returns>
    public bool Equals(Base64 other)
    {
        return other.Value.SequenceEqual(Value);
    }

    /// <summary>
    /// Checks for equality between the current instance and another object.
    /// </summary>
    /// <param name="obj">The object to check for equality.</param>
    /// <returns>Returns true if the object is equal to the current instance; otherwise, false.</returns>
    public override bool Equals(object? obj)
    {
        return obj is Base64 other && Equals(other);
    }

    /// <summary>
    /// Serves as a hash code function for the current instance.
    /// </summary>
    /// <returns>Returns a hash code for the current instance.</returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(Value.GetContentHashCode());
    }

    /// <summary>
    /// Performs an equality check between two object instances.
    /// </summary>
    /// <param name="left">The left-hand instance to compare.</param>
    /// <param name="right">The right-hand instance to compare.</param>
    /// <returns>True if the instances are equal; otherwise, false.</returns>
    public static bool operator ==(Base64 left, Base64 right)
    {
        return Equals(left, right);
    }

    /// <summary>
    /// Performs an inequality check between two object instances.
    /// </summary>
    /// <param name="left">The left-hand instance to compare.</param>
    /// <param name="right">The right-hand instance to compare.</param>
    /// <returns>True if the instances are not equal; otherwise, false.</returns>
    public static bool operator !=(Base64 left, Base64 right)
    {
        return !Equals(left, right);
    }
}
