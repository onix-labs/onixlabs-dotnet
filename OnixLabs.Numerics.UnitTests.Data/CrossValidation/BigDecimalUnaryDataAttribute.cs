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

namespace OnixLabs.Numerics.UnitTests.Data.CrossValidation;

/// <summary>
/// Supplies single <see cref="BigDecimal"/> values to Theory tests for cross-validation,
/// drawn from the curated edge-case set plus deterministically-seeded random values.
/// </summary>
public sealed class BigDecimalUnaryDataAttribute(int randomCount = 128, int seed = 0x0C0FFEE) : TestDataAttribute
{
    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        foreach (BigDecimal value in CrossValidationGenerator.GenerateBigDecimal(randomCount, seed))
            yield return [value];
    }
}
