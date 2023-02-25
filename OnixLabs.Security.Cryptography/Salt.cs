namespace OnixLabs.Security.Cryptography;

/// <summary>
/// Represents a cryptographically secure random value.
/// </summary>
public readonly partial struct Salt
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Salt"/> struct.
    /// </summary>
    /// <param name="value">The underlying cryptographically secure random value.</param>
    private Salt(byte[] value)
    {
        Value = value;
    }

    /// <summary>
    /// Gets the underlying cryptographically secure random value.
    /// </summary>
    private byte[] Value { get; }
}
