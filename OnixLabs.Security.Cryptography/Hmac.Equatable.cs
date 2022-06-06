// Copyright 2020-2022 ONIXLabs
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

namespace OnixLabs.Security.Cryptography;

public readonly partial struct Hmac : IEquatable<Hmac>
{
    /// <summary>
    /// Performs an equality check between two object instances.
    /// </summary>
    /// <param name="a">Instance a.</param>
    /// <param name="b">Instance b.</param>
    /// <returns>True if the instances are equal; otherwise, false.</returns>
    public static bool operator ==(Hmac a, Hmac b)
    {
        return Equals(a, b);
    }

    /// <summary>
    /// Performs an inequality check between two object instances.
    /// </summary>
    /// <param name="a">Instance a.</param>
    /// <param name="b">Instance b.</param>
    /// <returns>True if the instances are not equal; otherwise, false.</returns>
    public static bool operator !=(Hmac a, Hmac b)
    {
        return !Equals(a, b);
    }

    /// <summary>
    /// Checks for equality between this instance and another object.
    /// </summary>
    /// <param name="other">The object to check for equality.</param>
    /// <returns>true if the object is equal to this instance; otherwise, false.</returns>
    public bool Equals(Hmac other)
    {
        return other.Hash == Hash && other.Data.SequenceEqual(Data);
    }

    /// <summary>
    /// Checks for equality between this instance and another object.
    /// </summary>
    /// <param name="obj">The object to check for equality.</param>
    /// <returns>true if the object is equal to this instance; otherwise, false.</returns>
    public override bool Equals(object? obj)
    {
        return obj is Hmac other && Equals(other);
    }

    /// <summary>
    /// Serves as a hash code function for this instance.
    /// </summary>
    /// <returns>A hash code for this instance.</returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(Hash, Data.GetContentHashCode());
    }
}
