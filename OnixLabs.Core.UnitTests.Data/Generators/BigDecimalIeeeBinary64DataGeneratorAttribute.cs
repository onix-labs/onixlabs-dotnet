// Copyright 2020-2023 ONIXLabs
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

using System.Reflection;
using Xunit.Sdk;

namespace OnixLabs.Core.UnitTests.Data.Generators;

public sealed class BigDecimalIeeeBinary64DataGeneratorAttribute : DataAttribute
{
    private readonly double[] values =
    {
        // General test values
        0.0, double.MinValue, double.MaxValue, double.E, double.Epsilon, double.Pi, double.Tau,

        // Positive one with positive exponent
        1.0, 1e+1, 1e+2, 1e+3, 1e+4, 1e+5, 1e+6, 1e+7, 1e+8, 1e+9, 1e+10,
        1e+11, 1e+12, 1e+13, 1e+14, 1e+15, 1e+16, 1e+17, 1e+18, 1e+19, 1e+20, 1e+100, 1e+308,

        // Positive one with negative exponent
        1e-1, 1e-2, 1e-3, 1e-4, 1e-5, 1e-6, 1e-7, 1e-8, 1e-9, 1e-10,
        1e-11, 1e-12, 1e-13, 1e-14, 1e-15, 1e-16, 1e-17, 1e-18, 1e-19, 1e-20, 1e-100, 1e-320,

        // Positive two with positive exponent
        2.0, 2e+1, 2e+2, 2e+3, 2e+4, 2e+5, 2e+6, 2e+7, 2e+8, 2e+9, 2e+10,
        2e+11, 2e+12, 2e+13, 2e+14, 2e+15, 2e+16, 2e+17, 2e+18, 2e+19, 2e+20, 2e+100, 2e+307,

        // Positive two with negative exponent
        2e-1, 2e-2, 2e-3, 2e-4, 2e-5, 2e-6, 2e-7, 2e-8, 2e-9, 2e-10,
        2e-11, 2e-12, 2e-13, 2e-14, 2e-15, 2e-16, 2e-17, 2e-18, 2e-19, 2e-20, 2e-100, 2e-320,

        // Positive three with positive exponent
        3.0, 3e+1, 3e+2, 3e+3, 3e+4, 3e+5, 3e+6, 3e+7, 3e+8, 3e+9, 3e+10,
        3e+11, 3e+12, 3e+13, 3e+14, 3e+15, 3e+16, 3e+17, 3e+18, 3e+19, 3e+20, 3e+100, 3e+307,

        // Positive three with negative exponent
        3e-1, 3e-2, 3e-3, 3e-4, 3e-5, 3e-6, 3e-7, 3e-8, 3e-9, 3e-10,
        3e-11, 3e-12, 3e-13, 3e-14, 3e-15, 3e-16, 3e-17, 3e-18, 3e-19, 3e-20, 3e-100, 3e-320,

        // Positive four with positive exponent
        4.0, 4e+1, 4e+2, 4e+3, 4e+4, 4e+5, 4e+6, 4e+7, 4e+8, 4e+9, 4e+10,
        4e+11, 4e+12, 4e+13, 4e+14, 4e+15, 4e+16, 4e+17, 4e+18, 4e+19, 4e+20, 4e+100, 4e+307,

        // Positive four with negative exponent
        4e-1, 4e-2, 4e-3, 4e-4, 4e-5, 4e-6, 4e-7, 4e-8, 4e-9, 4e-10,
        4e-11, 4e-12, 4e-13, 4e-14, 4e-15, 4e-16, 4e-17, 4e-18, 4e-19, 4e-20, 4e-100, 4e-320,

        // Positive five with positive exponent
        5.0, 5e+1, 5e+2, 5e+3, 5e+4, 5e+5, 5e+6, 5e+7, 5e+8, 5e+9, 5e+10,
        5e+11, 5e+12, 5e+13, 5e+14, 5e+15, 5e+16, 5e+17, 5e+18, 5e+19, 5e+20, 5e+100, 5e+307,

        // Positive five with negative exponent
        5e-1, 5e-2, 5e-3, 5e-4, 5e-5, 5e-6, 5e-7, 5e-8, 5e-9, 5e-10,
        5e-11, 5e-12, 5e-13, 5e-14, 5e-15, 5e-16, 5e-17, 5e-18, 5e-19, 5e-20, 5e-100, 5e-320,

        // Positive six with positive exponent
        6.0, 6e+1, 6e+2, 6e+3, 6e+4, 6e+5, 6e+6, 6e+7, 6e+8, 6e+9, 6e+10,
        6e+11, 6e+12, 6e+13, 6e+14, 6e+15, 6e+16, 6e+17, 6e+18, 6e+19, 6e+20, 6e+100, 6e+307,

        // Positive six with negative exponent
        6e-1, 6e-2, 6e-3, 6e-4, 6e-5, 6e-6, 6e-7, 6e-8, 6e-9, 6e-10,
        6e-11, 6e-12, 6e-13, 6e-14, 6e-15, 6e-16, 6e-17, 6e-18, 6e-19, 6e-20, 6e-100, 6e-320,

        // Positive seven with positive exponent
        7.0, 7e+1, 7e+2, 7e+3, 7e+4, 7e+5, 7e+6, 7e+7, 7e+8, 7e+9, 7e+10,
        7e+11, 7e+12, 7e+13, 7e+14, 7e+15, 7e+16, 7e+17, 7e+18, 7e+19, 7e+20, 7e+100, 7e+307,

        // Positive seven with negative exponent
        7e-1, 7e-2, 7e-3, 7e-4, 7e-5, 7e-6, 7e-7, 7e-8, 7e-9, 7e-10,
        7e-11, 7e-12, 7e-13, 7e-14, 7e-15, 7e-16, 7e-17, 7e-18, 7e-19, 7e-20, 7e-100, 7e-320,

        // Positive eight with positive exponent
        8.0, 8e+1, 8e+2, 8e+3, 8e+4, 8e+5, 8e+6, 8e+7, 8e+8, 8e+9, 8e+10,
        8e+11, 8e+12, 8e+13, 8e+14, 8e+15, 8e+16, 8e+17, 8e+18, 8e+19, 8e+20, 8e+100, 8e+307,

        // Positive eight with negative exponent
        8e-1, 8e-2, 8e-3, 8e-4, 8e-5, 8e-6, 8e-7, 8e-8, 8e-9, 8e-10,
        8e-11, 8e-12, 8e-13, 8e-14, 8e-15, 8e-16, 8e-17, 8e-18, 8e-19, 8e-20, 8e-100, 8e-320,

        // Positive nine with positive exponent
        9.0, 9e+1, 9e+2, 9e+3, 9e+4, 9e+5, 9e+6, 9e+7, 9e+8, 9e+9, 9e+10,
        9e+11, 9e+12, 9e+13, 9e+14, 9e+15, 9e+16, 9e+17, 9e+18, 9e+19, 9e+20, 9e+100, 9e+307,

        // Positive nine with negative exponent
        9e-1, 9e-2, 9e-3, 9e-4, 9e-5, 9e-6, 9e-7, 9e-8, 9e-9, 9e-10,
        9e-11, 9e-12, 9e-13, 9e-14, 9e-15, 9e-16, 9e-17, 9e-18, 9e-19, 9e-20, 9e-100, 9e-320,

        // Negative one with positive exponent
        -1.0, -1e+1, -1e+2, -1e+3, -1e+4, -1e+5, -1e+6, -1e+7, -1e+8, -1e+9, -1e+10,
        -1e+11, -1e+12, -1e+13, -1e+14, -1e+15, -1e+16, -1e+17, -1e+18, -1e+19, -1e+20, -1e+100, -1e+308,

        // Negative one with negative exponent
        -1e-1, -1e-2, -1e-3, -1e-4, -1e-5, -1e-6, -1e-7, -1e-8, -1e-9, -1e-10,
        -1e-11, -1e-12, -1e-13, -1e-14, -1e-15, -1e-16, -1e-17, -1e-18, -1e-19, -1e-20, -1e-100, -1e-320,

        // Negative two with positive exponent
        -2.0, -2e+1, -2e+2, -2e+3, -2e+4, -2e+5, -2e+6, -2e+7, -2e+8, -2e+9, -2e+10,
        -2e+11, -2e+12, -2e+13, -2e+14, -2e+15, -2e+16, -2e+17, -2e+18, -2e+19, -2e+20, -2e+100, -2e+307,

        // Negative two with negative exponent
        -2e-1, -2e-2, -2e-3, -2e-4, -2e-5, -2e-6, -2e-7, -2e-8, -2e-9, -2e-10,
        -2e-11, -2e-12, -2e-13, -2e-14, -2e-15, -2e-16, -2e-17, -2e-18, -2e-19, -2e-20, -2e-100, -2e-320,

        // Negative three with positive exponent
        -3.0, -3e+1, -3e+2, -3e+3, -3e+4, -3e+5, -3e+6, -3e+7, -3e+8, -3e+9, -3e+10,
        -3e+11, -3e+12, -3e+13, -3e+14, -3e+15, -3e+16, -3e+17, -3e+18, -3e+19, -3e+20, -3e+100, -3e+307,

        // Negative three with negative exponent
        -3e-1, -3e-2, -3e-3, -3e-4, -3e-5, -3e-6, -3e-7, -3e-8, -3e-9, -3e-10,
        -3e-11, -3e-12, -3e-13, -3e-14, -3e-15, -3e-16, -3e-17, -3e-18, -3e-19, -3e-20, -3e-100, -3e-320,

        // Negative four with positive exponent
        -4.0, -4e+1, -4e+2, -4e+3, -4e+4, -4e+5, -4e+6, -4e+7, -4e+8, -4e+9, -4e+10,
        -4e+11, -4e+12, -4e+13, -4e+14, -4e+15, -4e+16, -4e+17, -4e+18, -4e+19, -4e+20, -4e+100, -4e+307,

        // Negative four with negative exponent
        -4e-1, -4e-2, -4e-3, -4e-4, -4e-5, -4e-6, -4e-7, -4e-8, -4e-9, -4e-10,
        -4e-11, -4e-12, -4e-13, -4e-14, -4e-15, -4e-16, -4e-17, -4e-18, -4e-19, -4e-20, -4e-100, -4e-320,

        // Negative five with positive exponent
        -5.0, -5e+1, -5e+2, -5e+3, -5e+4, -5e+5, -5e+6, -5e+7, -5e+8, -5e+9, -5e+10,
        -5e+11, -5e+12, -5e+13, -5e+14, -5e+15, -5e+16, -5e+17, -5e+18, -5e+19, -5e+20, -5e+100, -5e+307,

        // Negative five with negative exponent
        -5e-1, -5e-2, -5e-3, -5e-4, -5e-5, -5e-6, -5e-7, -5e-8, -5e-9, -5e-10,
        -5e-11, -5e-12, -5e-13, -5e-14, -5e-15, -5e-16, -5e-17, -5e-18, -5e-19, -5e-20, -5e-100, -5e-320,

        // Negative six with positive exponent
        -6.0, -6e+1, -6e+2, -6e+3, -6e+4, -6e+5, -6e+6, -6e+7, -6e+8, -6e+9, -6e+10,
        -6e+11, -6e+12, -6e+13, -6e+14, -6e+15, -6e+16, -6e+17, -6e+18, -6e+19, -6e+20, -6e+100, -6e+307,

        // Negative six with negative exponent
        -6e-1, -6e-2, -6e-3, -6e-4, -6e-5, -6e-6, -6e-7, -6e-8, -6e-9, -6e-10,
        -6e-11, -6e-12, -6e-13, -6e-14, -6e-15, -6e-16, -6e-17, -6e-18, -6e-19, -6e-20, -6e-100, -6e-320,

        // Negative seven with positive exponent
        -7.0, -7e+1, -7e+2, -7e+3, -7e+4, -7e+5, -7e+6, -7e+7, -7e+8, -7e+9, -7e+10,
        -7e+11, -7e+12, -7e+13, -7e+14, -7e+15, -7e+16, -7e+17, -7e+18, -7e+19, -7e+20, -7e+100, -7e+307,

        // Negative seven with negative exponent
        -7e-1, -7e-2, -7e-3, -7e-4, -7e-5, -7e-6, -7e-7, -7e-8, -7e-9, -7e-10,
        -7e-11, -7e-12, -7e-13, -7e-14, -7e-15, -7e-16, -7e-17, -7e-18, -7e-19, -7e-20, -7e-100, -7e-320,

        // Negative eight with positive exponent
        -8.0, -8e+1, -8e+2, -8e+3, -8e+4, -8e+5, -8e+6, -8e+7, -8e+8, -8e+9, -8e+10,
        -8e+11, -8e+12, -8e+13, -8e+14, -8e+15, -8e+16, -8e+17, -8e+18, -8e+19, -8e+20, -8e+100, -8e+307,

        // Negative eight with negative exponent
        -8e-1, -8e-2, -8e-3, -8e-4, -8e-5, -8e-6, -8e-7, -8e-8, -8e-9, -8e-10,
        -8e-11, -8e-12, -8e-13, -8e-14, -8e-15, -8e-16, -8e-17, -8e-18, -8e-19, -8e-20, -8e-100, -8e-320,

        // Negative nine with positive exponent
        -9.0, -9e+1, -9e+2, -9e+3, -9e+4, -9e+5, -9e+6, -9e+7, -9e+8, -9e+9, -9e+10,
        -9e+11, -9e+12, -9e+13, -9e+14, -9e+15, -9e+16, -9e+17, -9e+18, -9e+19, -9e+20, -9e+100, -9e+307,

        // Negative nine with negative exponent
        -9e-1, -9e-2, -9e-3, -9e-4, -9e-5, -9e-6, -9e-7, -9e-8, -9e-9, -9e-10,
        -9e-11, -9e-12, -9e-13, -9e-14, -9e-15, -9e-16, -9e-17, -9e-18, -9e-19, -9e-20, -9e-100, -9e-320,

        // Positive point shifting
        1234567890123456,
        123456789012345.6,
        12345678901234.56,
        1234567890123.456,
        123456789012.3456,
        12345678901.23456,
        1234567890.123456,
        123456789.0123456,
        12345678.90123456,
        1234567.890123456,
        123456.7890123456,
        12345.67890123456,
        1234.567890123456,
        123.4567890123456,
        12.34567890123456,
        1.234567890123456,

        // Negative point shifting
        -1234567890123456,
        -123456789012345.6,
        -12345678901234.56,
        -1234567890123.456,
        -123456789012.3456,
        -12345678901.23456,
        -1234567890.123456,
        -123456789.0123456,
        -12345678.90123456,
        -1234567.890123456,
        -123456.7890123456,
        -12345.67890123456,
        -1234.567890123456,
        -123.4567890123456,
        -12.34567890123456,
        -1.234567890123456,
    };

    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        return values.Select(value => new object[] { value, GetNumberFormattedAsInflatedFixedPoint(value) });
    }

    private static string GetNumberFormattedAsInflatedFixedPoint(double value)
    {
        return value.ToString("F5000").TrimEnd('0').TrimEnd('.');
    }
}
