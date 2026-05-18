// Copyright © 2020 ONIXLabs
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

namespace OnixLabs.Numerics.UnitTests;

public sealed class Float256ConstructorBitsTests
{
    public static TheoryData<ulong, ulong, ulong, ulong> BitPatterns() => new()
    {
        { 0x0000_0000_0000_0000UL, 0x0000_0000_0000_0000UL, 0x0000_0000_0000_0000UL, 0x0000_0000_0000_0000UL },
        { 0x3FFF_F000_0000_0000UL, 0x0000_0000_0000_0000UL, 0x0000_0000_0000_0000UL, 0x0000_0000_0000_0000UL },
        { 0xBFFF_F000_0000_0000UL, 0x0000_0000_0000_0000UL, 0x0000_0000_0000_0000UL, 0x0000_0000_0000_0000UL },
        { 0x7FFF_EFFF_FFFF_FFFFUL, 0xFFFF_FFFF_FFFF_FFFFUL, 0xFFFF_FFFF_FFFF_FFFFUL, 0xFFFF_FFFF_FFFF_FFFFUL },
        { 0xFFFF_EFFF_FFFF_FFFFUL, 0xFFFF_FFFF_FFFF_FFFFUL, 0xFFFF_FFFF_FFFF_FFFFUL, 0xFFFF_FFFF_FFFF_FFFFUL },
        { 0x7FFF_F000_0000_0000UL, 0x0000_0000_0000_0000UL, 0x0000_0000_0000_0000UL, 0x0000_0000_0000_0000UL },
        { 0xFFFF_F000_0000_0000UL, 0x0000_0000_0000_0000UL, 0x0000_0000_0000_0000UL, 0x0000_0000_0000_0000UL },
        { 0x7FFF_F800_0000_0000UL, 0x0000_0000_0000_0000UL, 0x0000_0000_0000_0000UL, 0x0000_0000_0000_0000UL },
        { 0x0000_0000_0000_0000UL, 0x0000_0000_0000_0000UL, 0x0000_0000_0000_0000UL, 0x0000_0000_0000_0001UL },
    };

    [MemberData(nameof(BitPatterns))]
    [Theory(DisplayName = "Float256(UInt256) constructor should preserve the specified bit pattern")]
    public void Float256ConstructorShouldPreserveBitPattern(ulong highHigh, ulong highLow, ulong lowHigh, ulong lowLow)
    {
        // Given
        UInt128 expectedHigh = new(highHigh, highLow);
        UInt128 expectedLow = new(lowHigh, lowLow);
        UInt256 expected = new(expectedHigh, expectedLow);

        // When
        UInt256 actual = new Float256(expected).RawBits;

        // Then
        Assert.Equal(expected, actual);
    }
}
