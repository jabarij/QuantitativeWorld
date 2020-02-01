using QuantitativeWorld.Formatting;
using FluentAssertions;
using System.Globalization;
using Xunit;

namespace QuantitativeWorld.Tests
{
    partial class LengthFormatterTests
    {
        public class StringFormat : LengthFormatterTests
        {
            public StringFormat(TestFixture testFixture)
                : base(testFixture) { }

            [Theory]
            [InlineData(1, "Length is: {0:" + StandardFormats.SI + "}", "en-US", "Length is: 1 m", "length in format 'SI' should be printed in metres")]
            [InlineData(1, "Length is: {0:" + StandardFormats.CGS + "}", "en-US", "Length is: 100 cm", "length in format 'CGS' should be printed in millimetres")]
            [InlineData(1, "Length is: {0:" + StandardFormats.IMP + "}", "en-US", "Length is: 3.2808399 ft", "length in format 'IMP' should be printed in feet")]
            [InlineData(1, "Length is: {0:" + StandardFormats.MKS + "}", "en-US", "Length is: 1 m", "length in format 'MKS' should be printed in metres")]
            [InlineData(1, "Length is: {0:" + StandardFormats.MTS + "}", "en-US", "Length is: 1 m", "length in format 'MKS' should be printed in metres")]
            [InlineData(0.000000009, "Length is: {0:" + StandardFormats.SI + "}", "en-US", "Length is: 0.00000001 m", "length in standard format should be printed with max 8 decimal places")]
            [InlineData(123.456, "Length is: {0:" + StandardFormats.SI + "}", "pl-PL", "Length is: 123,456 m", "length in standard format should be printed with decimal separator appropriate to given culture")]
            public void StringFormat_StandardFormat_ShouldReturnProperValue(decimal kilograms, string format, string cultureName, string expectedResult, string reason)
            {
                // arrange
                var formatter = new LengthFormatter(CultureInfo.GetCultureInfo(cultureName));
                var length = new Length(kilograms, LengthUnit.Metre);

                // act
                string actualResult = string.Format(formatter, format, length);

                // assert
                actualResult.Should().Be(expectedResult, because: reason);
            }

            [Theory]
            [InlineData(1, "Length is: {0:m}", "en-US", "Length is: 1 m")]
            [InlineData(1, "Length is: {0:km}", "en-US", "Length is: 0.001 km")]
            [InlineData(1, "Length is: {0:mm}", "en-US", "Length is: 1000 mm")]
            [InlineData(1, "Length is: {0:ft}", "en-US", "Length is: 3.2808399 ft")]
            [InlineData(1, "Length is: {0:ft|v0.###}", "en-US", "Length is: 3.281 ft")]
            [InlineData(1, "Length is: {0:ft|ull}", "en-US", "Length is: 3.2808399 feet")]
            [InlineData(1, "Length is: {0:ft|ull|v0.###}", "en-US", "Length is: 3.281 feet")]
            [InlineData(1, "D�ugo�� wynosi: {0:km}", "pl-PL", "D�ugo�� wynosi: 0,001 km")]
            public void StringFormat_CustomFormat_ShouldReturnProperValue(decimal kilograms, string customFormat, string cultureName, string expectedResult)
            {
                // arrange
                var formatter = new LengthFormatter(CultureInfo.GetCultureInfo(cultureName));
                var length = new Length(kilograms, LengthUnit.Metre);

                // act
                string actualResult = string.Format(formatter, customFormat, length);

                // assert
                actualResult.Should().Be(expectedResult);
            }
        }
    }
}
