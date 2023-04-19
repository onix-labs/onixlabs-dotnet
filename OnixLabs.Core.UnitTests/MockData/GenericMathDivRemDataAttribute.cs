using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit.Sdk;

namespace OnixLabs.Core.UnitTests.MockData;

public sealed class GenericMathDivRemDataAttribute : DataAttribute
{
    private readonly int count;

    public GenericMathDivRemDataAttribute(int count)
    {
        this.count = count;
    }

    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        int skip = int.MaxValue / count;

        for (int index = 1; index <= count; index++)
        {
            int dividend = unchecked(index * skip);
            int divisor = unchecked(int.MaxValue - dividend);

            (int quotient, int remainder) = Math.DivRem(dividend, divisor);

            yield return new object[] { dividend, divisor, quotient, remainder };
        }
    }
}
