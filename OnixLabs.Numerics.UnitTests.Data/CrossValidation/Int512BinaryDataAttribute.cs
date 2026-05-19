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

namespace OnixLabs.Numerics.UnitTests.Data.CrossValidation;

/// <summary>
/// Supplies pairs of <see cref="Int512"/> values to Theory tests for cross-validation
/// against <see cref="System.Numerics.BigInteger"/>. Each pair combines a value from the
/// curated edge-case set with a random partner. Deterministic for a given seed.
/// </summary>
public sealed class Int512BinaryDataAttribute(int randomCount = 256, int seed = 0x0C0FFEE) : TestDataAttribute
{
    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        Int512[] values = [.. CrossValidationGenerator.GenerateInt512(randomCount, seed)];
        Random partnerRng = new(unchecked(seed ^ 0x5EED));

        foreach (Int512 left in values)
        {
            Int512 right = values[partnerRng.Next(values.Length)];
            yield return [left, right];
        }
    }
}
