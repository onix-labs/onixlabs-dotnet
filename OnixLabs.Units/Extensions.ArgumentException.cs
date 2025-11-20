// Copyright 2020-2025 ONIXLabs
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
using System.Runtime.CompilerServices;

namespace OnixLabs.Units;

internal static class ArgumentExceptionExtensions
{
    extension(ArgumentException)
    {
        public static ArgumentException InvalidFormat(
            ReadOnlySpan<char> format,
            string specifiers,
            [CallerArgumentExpression(nameof(format))]
            string? parameterName = null
        ) => new($"Format '{format.ToString()}' is invalid. Valid format specifiers are: {specifiers}. " +
                 $"Format specifiers may also be suffixed with a scale value.", parameterName);
    }
}
