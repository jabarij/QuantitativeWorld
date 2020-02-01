using FluentAssertions;
using System.Collections.Generic;
using System.Diagnostics;
using Xunit;

namespace QuantitativeWorld.Tests
{
    partial class LinearUnitConverterTests
    {
        public class ConvertValue : LinearUnitConverterTests
        {
            public ConvertValue(TestFixture testFixture)
                : base(testFixture) { }

            [Theory]
            [MemberData(nameof(GetTestData), typeof(ConvertValue), nameof(GetConvertTestData))]
            public void ShouldConvertToExpectedValue(ConvertValueTestData testData)
            {
                // arrange
                var converter = new LinearUnitConverter<LengthUnit>();

                // act
                decimal actualValue = converter.ConvertValue(testData.Value, testData.SourceUnit, testData.TargetUnit);

                // assert
                actualValue.Should().Be(testData.ExpectedValue);
            }

            private static IEnumerable<ConvertValueTestData> GetConvertTestData()
            {
                yield return new ConvertValueTestData(1 / (0.0254m * 12m * 3m * 22m * 80m), LengthUnit.Mile, LengthUnit.Metre, 1m);
                //yield return new ConvertValueTestData(0.0006213711922373339696174342m, LengthUnit.Mile, LengthUnit.Metre, 1m);
            }

            [DebuggerDisplay("{Value} {SourceUnit} should be {ExpectedValue} {TargetUnit}")]
            public class ConvertValueTestData
            {
                public ConvertValueTestData(decimal value, LengthUnit sourceUnit, LengthUnit targetUnit, decimal expectedValue)
                {
                    Value = value;
                    SourceUnit = sourceUnit;
                    TargetUnit = targetUnit;
                    ExpectedValue = expectedValue;
                }

                public decimal Value { get; }
                public LengthUnit SourceUnit { get; }
                public LengthUnit TargetUnit { get; }
                public decimal ExpectedValue { get; }
            }
        }
    }
}
