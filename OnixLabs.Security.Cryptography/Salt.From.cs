namespace OnixLabs.Security.Cryptography;

public readonly partial struct Salt
{
    /// <summary>
    /// Creates a <see cref="Salt"/> instance from a <see cref="byte"/> array.
    /// </summary>
    /// <param name="value">The <see cref="byte"/> array to represent as a salt.</param>
    /// <returns>A new <see cref="Salt"/> instance.</returns>
    public static Salt FromByteArray(byte[] value)
    {
        return new Salt(value);
    }
}
