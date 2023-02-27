using System;

namespace OnixLabs.Security.Cryptography.UnitTests.MockData;

public sealed record Person(string FirstName, string LastName, DateOnly Birthday) : IHashable
{
    public Hash ComputeHash()
    {
        return Hash.ComputeSha2Hash256($"{FirstName}{LastName}{Birthday}");
    }
}
