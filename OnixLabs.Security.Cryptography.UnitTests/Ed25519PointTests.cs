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
using System.Numerics;

namespace OnixLabs.Security.Cryptography.UnitTests;

public sealed class Ed25519PointTests
{
    private static readonly BigInteger P = BigInteger.Pow(2, 255) - 19;

    [Fact(DisplayName = "Ed25519Point equality should hold for the same point reached by different projective representations")]
    public void PointEqualityShouldHoldForTheSamePointReachedByDifferentProjectiveRepresentations()
    {
        // Given
        Ed25519Point basePoint = Ed25519Point.BasePoint;

        // When
        Ed25519Point twiceByDoubling = Ed25519Point.Double(basePoint);
        Ed25519Point twiceByAddition = basePoint + basePoint;
        Ed25519Point fourTimesByDoubling = Ed25519Point.Double(twiceByDoubling);
        Ed25519Point fourTimesByAddition = twiceByAddition + basePoint + basePoint;

        // Then
        Assert.True(twiceByDoubling == twiceByAddition);
        Assert.True(fourTimesByDoubling == fourTimesByAddition);
        Assert.Equal(twiceByDoubling.GetHashCode(), twiceByAddition.GetHashCode());
        Assert.Equal(Encode(fourTimesByDoubling), Encode(fourTimesByAddition));
    }

    [Fact(DisplayName = "Ed25519Point equality should not hold for points that share only their x-coordinate")]
    public void PointEqualityShouldNotHoldForPointsThatShareOnlyTheirXCoordinate()
    {
        // Given
        Ed25519Point point = Ed25519Point.BasePoint;
        byte[] encoding = Encode(point);
        byte signOfX = (byte)(encoding[31] & 0x80);
        encoding[31] &= 0x7F;
        BigInteger y = new(encoding, isUnsigned: true, isBigEndian: false);

        // (x, -y) satisfies -x^2 + y^2 = 1 + d·x^2·y^2 whenever (x, y) does, so it is on the curve with the same x-coordinate.
        byte[] sameXEncoding = new byte[32];
        (P - y).TryWriteBytes(sameXEncoding, out int _, isUnsigned: true, isBigEndian: false);
        sameXEncoding[31] |= signOfX;

        // When
        bool decoded = Ed25519Point.TryDecode(sameXEncoding, out Ed25519Point sameX);

        // Then
        Assert.True(decoded);
        Assert.False(point == sameX);
        Assert.True(point != sameX);
    }

    [Fact(DisplayName = "Ed25519Point equality should not hold for points that share only their y-coordinate")]
    public void PointEqualityShouldNotHoldForPointsThatShareOnlyTheirYCoordinate()
    {
        // Given
        Ed25519Point point = Ed25519Point.BasePoint;

        // When
        Ed25519Point negated = -point;

        // Then
        Assert.False(point == negated);
        Assert.True(point != negated);
        Assert.NotEqual(Encode(point), Encode(negated));
    }

    /// <summary>
    /// Encodes a curve point.
    /// </summary>
    /// <param name="point">The point to encode.</param>
    /// <returns>Returns the 32-byte encoding of the point.</returns>
    private static byte[] Encode(Ed25519Point point)
    {
        byte[] result = new byte[32];
        point.Encode(result);
        return result;
    }
}
