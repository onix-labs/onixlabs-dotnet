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
