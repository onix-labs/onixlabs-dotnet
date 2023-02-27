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

using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit.Sdk;

namespace OnixLabs.Core.UnitTests.MockData;

public sealed class BigDecimalArithmeticDataAttribute : DataAttribute
{
    private readonly decimal left;
    private readonly decimal right;
    private readonly decimal skip;
    private readonly int count;
    private readonly ArithmeticOperator arithmeticOperator;

    public BigDecimalArithmeticDataAttribute(ArithmeticOperator arithmeticOperator, double left, double right, double skip, int count)
    {
        this.arithmeticOperator = arithmeticOperator;
        this.left = (decimal)left;
        this.right = (decimal)right;
        this.skip = (decimal)skip;
        this.count = count;
    }

    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        decimal leftValue = left;
        decimal rightValue = right;

        for (int iterator = 0; iterator < count; iterator++)
        {
            yield return new object[] { leftValue, rightValue, Compute(leftValue, rightValue) };
            leftValue += skip;
            rightValue += skip;
        }
    }

    private decimal Compute(decimal leftValue, decimal rightValue)
    {
        return arithmeticOperator switch
        {
            ArithmeticOperator.Addition => leftValue + rightValue,
            ArithmeticOperator.Subtraction => leftValue - rightValue,
            ArithmeticOperator.Multiplication => leftValue * rightValue,
            ArithmeticOperator.Division => leftValue / rightValue,
            _ => throw new ArgumentOutOfRangeException(nameof(arithmeticOperator))
        };
    }
}
