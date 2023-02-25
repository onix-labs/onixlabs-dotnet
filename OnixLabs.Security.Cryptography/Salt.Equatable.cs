using System;
using System.Linq;
using OnixLabs.Core.Linq;

namespace OnixLabs.Security.Cryptography;

public readonly partial struct Salt : IEquatable<Salt>
{
    /// <summary>
    /// Checks for equality between this instance and another object.
    /// </summary>
    /// <param name="other">The object to check for equality.</param>
    /// <returns>true if the object is equal to this instance; otherwise, false.</returns>
    public bool Equals(Salt other)
    {
        return other.Value.SequenceEqual(Value);
    }

    /// <summary>
    /// Checks for equality between this instance and another object.
    /// </summary>
    /// <param name="obj">The object to check for equality.</param>
    /// <returns>true if the object is equal to this instance; otherwise, false.</returns>
    public override bool Equals(object? obj)
    {
        return obj is Salt other && Equals(other);
    }

    /// <summary>
    /// Serves as a hash code function for this instance.
    /// </summary>
    /// <returns>A hash code for this instance.</returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(Value.GetContentHashCode());
    }

    /// <summary>
    /// Performs an equality check between two object instances.
    /// </summary>
    /// <param name="left">Instance a.</param>
    /// <param name="right">Instance b.</param>
    /// <returns>True if the instances are equal; otherwise, false.</returns>
    public static bool operator ==(Salt left, Salt right)
    {
        return Equals(left, right);
    }

    /// <summary>
    /// Performs an inequality check between two object instances.
    /// </summary>
    /// <param name="left">Instance a.</param>
    /// <param name="right">Instance b.</param>
    /// <returns>True if the instances are not equal; otherwise, false.</returns>
    public static bool operator !=(Salt left, Salt right)
    {
        return !Equals(left, right);
    }
}
