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
/// Supplies pairs of <see cref="BigDecimal"/> values to Theory tests for cross-validation
/// against an exact rational oracle. Each pair combines a value from the curated edge-case
/// set with a random partner. Deterministic for a given seed.
/// </summary>
public sealed class BigDecimalBinaryDataAttribute(int randomCount = 128, int seed = 0x0C0FFEE) : TestDataAttribute
{
    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        BigDecimal[] values = [.. CrossValidationGenerator.GenerateBigDecimal(randomCount, seed)];
        Random partnerRng = new(unchecked(seed ^ 0x5EED));

        foreach (BigDecimal left in values)
        {
            BigDecimal right = values[partnerRng.Next(values.Length)];
            yield return [left, right];
        }
    }
}
