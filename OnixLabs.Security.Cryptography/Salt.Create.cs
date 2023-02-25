using System.Security.Cryptography;

namespace OnixLabs.Security.Cryptography;

public readonly partial struct Salt
{
    /// <summary>
    /// Creates a <see cref="Salt"/> of the specified length.
    /// </summary>
    /// <param name="length">The length of the salt to create.</param>
    /// <returns>Returns a new <see cref="Salt"/> instance of the specified length.</returns>
    public static Salt Create(int length)
    {
        using RandomNumberGenerator generator = RandomNumberGenerator.Create();
        byte[] value = new byte[length];
        generator.GetBytes(value);

        return FromByteArray(value);
    }

    /// <summary>
    /// Creates a non-zero <see cref="Salt"/> of the specified length.
    /// </summary>
    /// <param name="length">The length of the salt to create.</param>
    /// <returns>Returns a new non-zero <see cref="Salt"/> instance of the specified length.</returns>
    public static Salt CreateNonZero(int length)
    {
        using RandomNumberGenerator generator = RandomNumberGenerator.Create();
        byte[] value = new byte[length];
        generator.GetNonZeroBytes(value);

        return FromByteArray(value);
    }
}
