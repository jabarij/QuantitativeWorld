using FluentAssertions;
using QuantitativeWorld.TestAbstractions;
using QuantitativeWorld.Text.Formatting;
using Xunit;

namespace QuantitativeWorld.Tests.Formatting
{
    partial class WeightUnitFormatterTests
    {
        public class StringFormat : WeightUnitFormatterTests
        {
            public StringFormat(TestFixture testFixture)
                : base(testFixture) { }

            [Theory]
            [InlineData("unit", "u", "s", "u", "standard format 's' should format weight unit as it's abbreviation")]
            [InlineData("unit", "u", "l", "unit", "standard format 'l' should format weight unit as it's name")]
            [InlineData("unit", "u", "ll", "units", "standard format 'll' should format weight unit as it's pluralized name")]
            public void StringFormat_StandardFormat_ShouldReturnProperValue(string unitName, string unitAbbreviation, string standardFormat, string expectedResult, string reason)
            {
                // arrange
                var formatter = new WeightUnitFormatter();
                var weight = new WeightUnit(
                    name: unitName,
                    abbreviation: unitAbbreviation,
                    valueInKilograms: Fixture.CreatePositive());

                // act
                string actualResult = formatter.Format(standardFormat, weight);

                // assert
                actualResult.Should().Be(expectedResult, because: reason);
            }
        }
    }
}
