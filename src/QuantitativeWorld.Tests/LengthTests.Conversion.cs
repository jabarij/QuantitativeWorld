using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

#if DECIMAL
namespace DecimalQuantitativeWorld.Tests
{
    using DecimalQuantitativeWorld.TestAbstractions;
    using number = System.Decimal;
#else
namespace QuantitativeWorld.Tests
{
    using QuantitativeWorld.TestAbstractions;
    using Constants = DoubleConstants;
    using number = System.Double;
#endif

    partial class LengthTests
    {
        public class Convert : LengthTests
        {
            public Convert(TestFixture testFixture)
                : base(testFixture) { }

            [Theory]
            [MemberData(nameof(GetTestData), typeof(Convert), nameof(GetConvertTestData))]
            public void ShouldConvertToExpectedValue(Length originalLength, LengthUnit targetUnit, Length expectedLength)
            {
                // arrange
                // act
                var actualLength = originalLength.Convert(targetUnit);

                // assert
                actualLength.Metres.Should().BeApproximately(expectedLength.Metres);
                actualLength.Value.Should().BeApproximately(expectedLength.Value);
                actualLength.Unit.Should().Be(targetUnit);
            }

            [Fact]
            public void MultipleSerialConversion_ShouldHaveSameValueAtTheEnd()
            {
                // arrange
                var units = new List<LengthUnit>
                {
                    LengthUnit.Metre,
                    LengthUnit.Millimetre,
                    LengthUnit.Kilometre,
                    LengthUnit.Foot,
                    LengthUnit.Inch,
                    LengthUnit.Metre
                };
                var initialLength = new Length((number)0.12345678m, units.First());
                Length? finalLength = null;

                // act
                units.ForEach(u => finalLength = (finalLength ?? initialLength).Convert(u));

                // assert
                finalLength.Should().Be(initialLength);
            }

            private static IEnumerable<ITestDataProvider> GetConvertTestData()
            {
                yield return new ConvertTestData(12.3456m, LengthUnit.Metre, 123.456m, LengthUnit.Decimetre);
                yield return new ConvertTestData(1234.56m, LengthUnit.Decimetre, 123.456m, LengthUnit.Metre);

                yield return new ConvertTestData(123.456m, LengthUnit.Metre, 0.123456m, LengthUnit.Kilometre);
                yield return new ConvertTestData(0.123456m, LengthUnit.Kilometre, 123.456m, LengthUnit.Metre);

                yield return new ConvertTestData(37.6293888m, LengthUnit.Metre, 123.456m, LengthUnit.Foot);
                yield return new ConvertTestData(123.456m, LengthUnit.Foot, 37.6293888m, LengthUnit.Metre);
            }

            class ConvertTestData : ConversionTestData<Length>, ITestDataProvider
            {
                public ConvertTestData(decimal originalValue, LengthUnit originalUnit, decimal expectedValue, LengthUnit expectedUnit)
                    : base(new Length((number)originalValue, originalUnit), new Length((number)expectedValue, expectedUnit)) { }
                public ConvertTestData(double originalValue, LengthUnit originalUnit, double expectedValue, LengthUnit expectedUnit)
                    : base(new Length((number)originalValue, originalUnit), new Length((number)expectedValue, expectedUnit)) { }

                public object[] GetTestParameters() =>
                    new[] { (object)OriginalValue, ExpectedValue.Unit, ExpectedValue };
            }
        }
    }
}
