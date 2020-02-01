using QuantitativeWorld.Formatting;
using FluentAssertions;
using System.Globalization;
using Xunit;

namespace QuantitativeWorld.Tests
{
    partial class LengthFormatterTests
    {
        public class Format : LengthFormatterTests
        {
            public Format(TestFixture testFixture)
                : base(testFixture) { }

            [Theory]
            [InlineData(1, StandardFormats.SI, "en-US", "1 m", "length in format 'SI' should be printed in metres")]
            [InlineData(1, StandardFormats.CGS, "en-US", "100 cm", "length in format 'CGS' should be printed in centimetres")]
            [InlineData(1, StandardFormats.IMP, "en-US", "3.2808399 ft", "length in format 'IMP' should be printed in pounds")]
            [InlineData(1, StandardFormats.MKS, "en-US", "1 m", "length in format 'MKS' should be printed in metres")]
            [InlineData(1, StandardFormats.MTS, "en-US", "1 m", "length in format 'MKS' should be printed in metres")]
            [InlineData(0.000000009, StandardFormats.SI, "en-US", "0.00000001 m", "length in standard format should be printed with max 8 decimal places")]
            [InlineData(123.456, StandardFormats.SI, "pl-PL", "123,456 m", "length in standard format should be printed with decimal separator appropriate to given culture")]
            public void StandardFormat_ShouldReturnProperValue(decimal metres, string standardFormat, string cultureName, string expectedResult, string reason)
            {
                // arrange
                var formatter = new LengthFormatter(CultureInfo.GetCultureInfo(cultureName));
                var length = new Length(metres, LengthUnit.Metre);

                // act
                string actualResult = formatter.Format(standardFormat, length);

                // assert
                actualResult.Should().Be(expectedResult, because: reason);
            }

            [Theory]
            [InlineData(1, "mm", "en-US", "1000 mm")]
            [InlineData(1, "cm", "en-US", "100 cm")]
            [InlineData(1, "dm", "en-US", "10 dm")]
            [InlineData(1, "m", "en-US", "1 m")]
            [InlineData(1, "km", "en-US", "0.001 km")]
            [InlineData(1, "in", "en-US", "39.37007874 in")]
            [InlineData(1, "ft", "en-US", "3.2808399 ft")]
            [InlineData(1, "yd", "en-US", "1.0936133 yd")]
            [InlineData(100, "ch", "en-US", "4.97096954 ch")]
            [InlineData(100000, "mi", "en-US", "62.13711922 mi")]
            [InlineData(100000, "nmi", "en-US", "53.99568035 nmi")]
            [InlineData(1, "ft|v0.###", "en-US", "3.281 ft")]
            [InlineData(1, "ft|ull", "en-US", "3.2808399 feet")]
            [InlineData(1, "ft|ull|v0.###", "en-US", "3.281 feet")]
            [InlineData(1, "km", "pl-PL", "0,001 km")]
            public void CustomFormat_ShouldReturnProperValue(decimal metres, string customFormat, string cultureName, string expectedResult)
            {
                // arrange
                var formatter = new LengthFormatter(CultureInfo.GetCultureInfo(cultureName));
                var length = new Length(metres, LengthUnit.Metre);

                // act
                string actualResult = formatter.Format(customFormat, length);

                // assert
                actualResult.Should().Be(expectedResult);
            }
        }
    }
}
