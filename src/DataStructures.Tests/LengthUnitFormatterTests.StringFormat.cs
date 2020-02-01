using AutoFixture;
using QuantitativeWorld.Formatting;
using FluentAssertions;
using Xunit;

namespace QuantitativeWorld.Tests
{
    partial class LengthUnitFormatterTests
    {
        public class StringFormat : LengthUnitFormatterTests
        {
            public StringFormat(TestFixture testFixture)
                : base(testFixture) { }

            [Theory]
            [InlineData("unit", "u", "s", "u", "standard format 's' should format length unit as it's abbreviation")]
            [InlineData("unit", "u", "l", "unit", "standard format 'l' should format length unit as it's name")]
            [InlineData("unit", "u", "ll", "units", "standard format 'll' should format length unit as it's pluralized name")]
            public void StringFormat_StandardFormat_ShouldReturnProperValue(string unitName, string unitAbbreviation, string standardFormat, string expectedResult, string reason)
            {
                // arrange
                var formatter = new LengthUnitFormatter();
                var length = new LengthUnit(
                    name: unitName,
                    abbreviation: unitAbbreviation,
                    valueInMetres: Fixture.Create<decimal>());

                // act
                string actualResult = formatter.Format(standardFormat, length);

                // assert
                actualResult.Should().Be(expectedResult, because: reason);
            }
        }
    }
}
