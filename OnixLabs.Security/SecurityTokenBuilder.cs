// Copyright 2020 ONIXLabs
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
using System.Collections.Generic;
using System.Linq;

namespace OnixLabs.Security;

/// <summary>
/// Represents a <see cref="SecurityToken"/> builder.
/// </summary>
// ReSharper disable HeapView.ObjectAllocation
// ReSharper disable HeapView.ObjectAllocation.Evident
public sealed class SecurityTokenBuilder
{
    private const string LowerCaseCharacters = "abcdefghijklmnopqrstuvwxyz";
    private const string UpperCaseCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const string DigitCharacters = "0123456789";
    private const string SpecialBasicCharacters = "!@#$%^&*";
    private const string SpecialExtendedCharacters = "!@#$%^&*()_+-={}[]:;\"'<>,.?/~`|\\";

    private readonly int length;
    private readonly RandomNumberProvider provider;
    private readonly HashSet<char> characters = [];

    /// <summary>
    /// Initializes a new instance of the <see cref="SecurityTokenBuilder"/> class.
    /// </summary>
    /// <param name="length">The desired length of the generated security token.</param>
    /// <param name="provider">The <see cref="RandomNumberProvider"/> that will be used to randomly select security token characters.</param>
    private SecurityTokenBuilder(int length, RandomNumberProvider provider) => (this.length, this.provider) = (length, provider);

    /// <summary>
    /// Creates a new <see cref="SecurityTokenBuilder"/> instance using a pseudo-random number provider.
    /// </summary>
    /// <param name="length">The desired length of the generated security token.</param>
    /// <param name="random">The underlying <see cref="Random"/> pseudo-random instance.</param>
    /// <returns>Returns a new <see cref="SecurityTokenBuilder"/> instance using a pseudo-random number provider.</returns>
    public static SecurityTokenBuilder CreatePseudoRandom(int length, Random? random = null) =>
        new(length, RandomNumberProvider.CreatePseudoRandomNumberProvider(random ?? new Random()));

    /// <summary>
    /// Creates a new <see cref="SecurityTokenBuilder"/> instance using a pseudo-random number provider.
    /// </summary>
    /// <param name="length">The desired length of the generated security token.</param>
    /// <param name="seed">The seed for the pseudo-random number provider.</param>
    /// <returns>Returns a new <see cref="SecurityTokenBuilder"/> instance using a pseudo-random number provider.</returns>
    public static SecurityTokenBuilder CreatePseudoRandom(int length, int seed) =>
        new(length, RandomNumberProvider.CreatePseudoRandomNumberProvider(new Random(seed)));

    /// <summary>
    /// Creates a new <see cref="SecurityTokenBuilder"/> instance using a secure random number provider.
    /// </summary>
    /// <param name="length">The desired length of the generated security token.</param>
    /// <returns>Returns a new <see cref="SecurityTokenBuilder"/> instance using a secure random number provider.</returns>
    public static SecurityTokenBuilder CreateSecureRandom(int length) =>
        new(length, RandomNumberProvider.CreateSecureRandomNumberProvider());

    /// <summary>
    /// Specifies arbitrarily defined characters that are allowed as part of a generated security token.
    /// </summary>
    /// <param name="allowedCharacters">The characters that are allowed as part of a generated security token.</param>
    /// <returns>Returns the current <see cref="SecurityTokenBuilder"/> instance.</returns>
    public SecurityTokenBuilder UseCharacters(ReadOnlySpan<char> allowedCharacters)
    {
        foreach (char character in allowedCharacters) characters.Add(character);
        return this;
    }

    /// <summary>
    /// Specifies that generated security tokens can use lowercase characters.
    /// </summary>
    /// <returns>Returns the current <see cref="SecurityTokenBuilder"/> instance.</returns>
    public SecurityTokenBuilder UseLowerCase() => UseCharacters(LowerCaseCharacters);

    /// <summary>
    /// Specifies that generated security tokens can use uppercase characters.
    /// </summary>
    /// <returns>Returns the current <see cref="SecurityTokenBuilder"/> instance.</returns>
    public SecurityTokenBuilder UseUpperCase() => UseCharacters(UpperCaseCharacters);

    /// <summary>
    /// Specifies that generated security tokens can use digit characters.
    /// </summary>
    /// <returns>Returns the current <see cref="SecurityTokenBuilder"/> instance.</returns>
    public SecurityTokenBuilder UseDigits() => UseCharacters(DigitCharacters);

    /// <summary>
    /// Specifies that generated security tokens can use basic special characters.
    /// </summary>
    /// <returns>Returns the current <see cref="SecurityTokenBuilder"/> instance.</returns>
    public SecurityTokenBuilder UseBasicSpecialCharacters() => UseCharacters(SpecialBasicCharacters);

    /// <summary>
    /// Specifies that generated security tokens can use extended special characters.
    /// </summary>
    /// <returns>Returns the current <see cref="SecurityTokenBuilder"/> instance.</returns>
    public SecurityTokenBuilder UseExtendedSpecialCharacters() => UseCharacters(SpecialExtendedCharacters);

    /// <summary>
    /// Specifies that generated security tokens can use alphanumeric (lowercase, uppercase and digit) characters.
    /// </summary>
    /// <returns>Returns the current <see cref="SecurityTokenBuilder"/> instance.</returns>
    public SecurityTokenBuilder UseAlphaNumericCharacters() => UseLowerCase().UseUpperCase().UseDigits();

    /// <summary>
    /// Creates a new <see cref="SecurityToken"/> instance from the current <see cref="SecurityTokenBuilder"/> instance.
    /// </summary>
    /// <returns>Returns a new <see cref="SecurityToken"/> instance.</returns>
    public SecurityToken ToSecurityToken()
    {
        Span<char> result = stackalloc char[length];
        List<char> allowedCharacters = characters.ToList();

        for (int index = 0; index < length; index++)
            result[index] = allowedCharacters[provider.GetNextInt(0, allowedCharacters.Count)];

        return new SecurityToken(result);
    }
}
