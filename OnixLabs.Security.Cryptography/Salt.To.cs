using OnixLabs.Core;
using OnixLabs.Core.Text;

namespace OnixLabs.Security.Cryptography;

public readonly partial struct Salt
{
    /// <summary>
    /// Returns a <see cref="byte"/> array containing the underlying salt data.
    /// </summary>
    /// <returns>A <see cref="byte"/> array containing the underlying salt data.</returns>
    public byte[] ToByteArray()
    {
        return Value.Copy();
    }

    /// <summary>
    /// Creates a <see cref="Hash"/> from the current salt data.
    /// </summary>
    /// <param name="type">The <see cref="HashAlgorithmType"/> of the hash to produce.</param>
    /// <returns>Returns a <see cref="Hash"/> of the current salt data.</returns>
    public Hash ToHash(HashAlgorithmType type)
    {
        return Hash.ComputeHash(Value, type);
    }

    /// <summary>
    /// Creates a <see cref="Hash"/> from the current salt data.
    /// </summary>
    /// <param name="type">The <see cref="HashAlgorithmType"/> of the hash to produce.</param>
    /// <param name="length">The length of the hash to produce.</param>
    /// <returns>Returns a <see cref="Hash"/> of the current salt data.</returns>
    public Hash ToHash(HashAlgorithmType type, int length)
    {
        return Hash.ComputeHash(Value, type, length);
    }

    /// <summary>
    /// Returns a <see cref="string"/> that represents the current object.
    /// </summary>
    /// <returns>A <see cref="string"/> that represents the current object.</returns>
    public override string ToString()
    {
        return Base16.FromByteArray(Value).ToString();
    }
}
