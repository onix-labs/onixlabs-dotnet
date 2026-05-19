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

public sealed class Float128ConstructorBitsTests
{
    public static TheoryData<ulong, ulong> BitPatterns() => new()
    {
        { 0x0000_0000_0000_0000UL, 0x0000_0000_0000_0000UL },
        { 0x3FFF_0000_0000_0000UL, 0x0000_0000_0000_0000UL },
        { 0xBFFF_0000_0000_0000UL, 0x0000_0000_0000_0000UL },
        { 0x7FFE_FFFF_FFFF_FFFFUL, 0xFFFF_FFFF_FFFF_FFFFUL },
        { 0xFFFE_FFFF_FFFF_FFFFUL, 0xFFFF_FFFF_FFFF_FFFFUL },
        { 0x7FFF_0000_0000_0000UL, 0x0000_0000_0000_0000UL },
        { 0xFFFF_0000_0000_0000UL, 0x0000_0000_0000_0000UL },
        { 0x7FFF_8000_0000_0000UL, 0x0000_0000_0000_0000UL },
        { 0x0000_0000_0000_0000UL, 0x0000_0000_0000_0001UL },
    };

    [MemberData(nameof(BitPatterns))]
    [Theory(DisplayName = "Float128(UInt128) constructor should preserve the specified bit pattern")]
    public void Float128ConstructorShouldPreserveBitPattern(ulong high, ulong low)
    {
        // Given
        UInt128 expected = new(high, low);

        // When
        UInt128 actual = new Float128(expected).Bits;

        // Then
        Assert.Equal(expected, actual);
    }
}
