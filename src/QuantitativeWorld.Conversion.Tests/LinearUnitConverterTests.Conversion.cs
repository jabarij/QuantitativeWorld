using FluentAssertions;
using QuantitativeWorld.TestAbstractions;
using System.Collections.Generic;
using Xunit;

namespace QuantitativeWorld.Conversion.Tests
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
                double actualValue = converter.ConvertValue(testData.Value, testData.SourceUnit, testData.TargetUnit);

                // assert
                actualValue.Should().BeApproximately(testData.ExpectedValue, DoublePrecision);
            }

            private static IEnumerable<ConvertValueTestData> GetConvertTestData()
            {
                yield return new ConvertValueTestData(1 / (0.0254d * 12d * 3d * 5.5d * 4d * 10d * 8d), LengthUnit.Mile, LengthUnit.Metre, 1d);
                yield return new ConvertValueTestData(0.0006213711922373339696174342d, LengthUnit.Mile, LengthUnit.Metre, 1d);
            }

            public class ConvertValueTestData
            {
                public ConvertValueTestData(double value, LengthUnit sourceUnit, LengthUnit targetUnit, double expectedValue)
                {
                    Value = value;
                    SourceUnit = sourceUnit;
                    TargetUnit = targetUnit;
                    ExpectedValue = expectedValue;
                }

                public double Value { get; }
                public LengthUnit SourceUnit { get; }
                public LengthUnit TargetUnit { get; }
                public double ExpectedValue { get; }

                public override string ToString() =>
                    $"{Value} {SourceUnit} should be {ExpectedValue} {TargetUnit}";
            }
        }
    }
}
