using System;
using System.Numerics;
using OnixLabs.Core;

namespace OnixLabs.Security.Cryptography;

public readonly partial struct Hash : IComparable, IComparable<Hash>
{
    /// <summary>
    /// Compares the current instance with another object of the same type and returns an integer that indicates
    /// whether the current instance precedes, follows, or occurs in the same position in the sort order as the
    /// other object.
    /// </summary>
    /// <param name="obj">An object to compare with this instance.</param>
    /// <returns>Returns a value that indicates the relative order of the objects being compared.</returns>
    public int CompareTo(object? obj)
    {
        return this.CompareObject(obj);
    }

    /// <summary>
    /// Compares the current instance with another object of the same type and returns an integer that indicates
    /// whether the current instance precedes, follows, or occurs in the same position in the sort order as the
    /// other object.
    /// </summary>
    /// <param name="other">An object to compare with this instance.</param>
    /// <returns>Returns a value that indicates the relative order of the objects being compared.</returns>
    public int CompareTo(Hash other)
    {
        BigInteger left = new(ToByteArray());
        BigInteger right = new(other.ToByteArray());

        return left.CompareTo(right);
    }
}
