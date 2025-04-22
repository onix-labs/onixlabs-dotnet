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

namespace OnixLabs.Playground;

internal static class Program
{
    private static void Main()
    {
        decimal value = 100.13m;
        decimal round = value.SetScale(2);

        Console.WriteLine(round);
    }

    public static decimal SetScale(this decimal value, int scale)
    {
        Require(scale >= 0, "Scale must be greater than, or equal to zero.");

        // Count actual decimal places
        int actualScale = GetDecimalPlaces(value);

        if (actualScale == scale)
        {
            return value;
        }

        if (actualScale < scale)
        {
            // Pad with zeroes by scaling and descaling
            decimal factor = Pow10(scale - actualScale);
            return value * factor / factor;
        }

        else // actualScale > scale
        {
            // Check if the digits beyond the desired scale are zero
            decimal factor = Pow10(actualScale - scale);
            decimal remainder = (value * factor) % 1;

            if (remainder != 0)
                throw new InvalidOperationException($"Cannot reduce scale without losing precision: {value}");

            return Math.Truncate(value * Pow10(scale)) / Pow10(scale);
        }
    }

    private static int GetDecimalPlaces(decimal value)
    {
        int[] bits = decimal.GetBits(value);
        byte scale = (byte)((bits[3] >> 16) & 0x7F);
        return scale;
    }

    private static decimal Pow10(int exp)
    {
        decimal result = 1m;
        for (int i = 0; i < exp; i++)
            result *= 10;
        return result;
    }
}
