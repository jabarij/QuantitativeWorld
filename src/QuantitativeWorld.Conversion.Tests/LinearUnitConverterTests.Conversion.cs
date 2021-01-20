using FluentAssertions;
using QuantitativeWorld.TestAbstractions;
using System.Collections.Generic;
using Xunit;

namespace QuantitativeWorld.Conversion.Tests
{
#if DECIMAL
    using number = System.Decimal;
    using Constants = QuantitativeWorld.DecimalConstants;
#else
    using number = System.Double;
    using Constants = QuantitativeWorld.DoubleConstants;
#endif

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
                number actualValue = converter.ConvertValue(testData.Value, testData.SourceUnit, testData.TargetUnit);

                // assert
                actualValue.Should().BeApproximately(testData.ExpectedValue);
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
                    Value = (number)value;
                    SourceUnit = sourceUnit;
                    TargetUnit = targetUnit;
                    ExpectedValue = (number)expectedValue;
                }
                public ConvertValueTestData(decimal value, LengthUnit sourceUnit, LengthUnit targetUnit, decimal expectedValue)
                {
                    Value = (number)value;
                    SourceUnit = sourceUnit;
                    TargetUnit = targetUnit;
                    ExpectedValue = (number)expectedValue;
                }

                public number Value { get; }
                public LengthUnit SourceUnit { get; }
                public LengthUnit TargetUnit { get; }
                public number ExpectedValue { get; }

                public override string ToString() =>
                    $"{Value} {SourceUnit} should be {ExpectedValue} {TargetUnit}";
            }
        }
    }
}
