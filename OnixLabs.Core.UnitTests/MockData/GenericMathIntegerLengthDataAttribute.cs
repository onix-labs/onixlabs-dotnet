using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit.Sdk;

namespace OnixLabs.Core.UnitTests.MockData;

public sealed class GenericMathIntegerLengthDataAttribute : DataAttribute
{
    private readonly int count;

    public GenericMathIntegerLengthDataAttribute(int count)
    {
        this.count = count;
    }

    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        Random random = new();

        for (int index = 0; index <= count; index++)
        {
            int value = random.Next(int.MinValue, int.MaxValue);
            int expectedLength = value.ToString().TrimStart('-').Length;

            yield return new object[] { value, expectedLength };
        }
    }
}
