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

namespace OnixLabs.Numerics;

public readonly partial struct Float128
{
    /// <summary>
    /// Returns a <see cref="Float128"/> value with the magnitude of <paramref name="magnitude"/> and the sign of <paramref name="sign"/>.
    /// </summary>
    /// <param name="magnitude">The value whose magnitude is used for the result.</param>
    /// <param name="sign">The value whose sign bit is used for the result.</param>
    /// <returns>Returns a <see cref="Float128"/> value with the magnitude of <paramref name="magnitude"/> and the sign bit of <paramref name="sign"/>.</returns>
    public static Float128 CopySign(Float128 magnitude, Float128 sign) => new((magnitude.RawBits & ~SignMask) | (sign.RawBits & SignMask));
}
