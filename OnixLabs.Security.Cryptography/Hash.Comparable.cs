using System;
using System.Numerics;

namespace OnixLabs.Security.Cryptography;

public readonly partial struct Hash : IComparable<Hash>
{
    public int CompareTo(Hash other)
    {
        BigInteger left = new(ToByteArray());
        BigInteger right = new(other.ToByteArray());
        return left.CompareTo(right);
    }
}
