using FluentAssertions;
using QuantitativeWorld.TestAbstractions;
using QuantitativeWorld.Text.Formatting;
using System.Globalization;
using Xunit;

namespace QuantitativeWorld.Tests.Formatting
{
#if DECIMAL
    using number = System.Decimal;
    using Constants = QuantitativeWorld.DecimalConstants;
#else
    using number = System.Double;
    using Constants = QuantitativeWorld.DoubleConstants;
#endif

    partial class LengthFormatterTests
    {
        public class Format : LengthFormatterTests
        {
            public Format(TestFixture testFixture)
                : base(testFixture) { }

            [Theory]
            [InlineData(1d, StandardFormats.SI, "en-US", "1 m", "length in format 'SI' should be printed in metres")]
            [InlineData(1d, StandardFormats.CGS, "en-US", "100 cm", "length in format 'CGS' should be printed in centimetres")]
            [InlineData(1d, StandardFormats.IMP, "en-US", "3.2808399 ft", "length in format 'IMP' should be printed in pounds")]
            [InlineData(1d, StandardFormats.MKS, "en-US", "1 m", "length in format 'MKS' should be printed in metres")]
            [InlineData(1d, StandardFormats.MTS, "en-US", "1 m", "length in format 'MKS' should be printed in metres")]
            [InlineData(0.000000009d, StandardFormats.SI, "en-US", "0.00000001 m", "length in standard format should be printed with max 8 decimal places")]
            [InlineData(123.456d, StandardFormats.SI, "pl-PL", "123,456 m", "length in standard format should be printed with number separator appropriate to given culture")]
            public void StandardFormat_ShouldReturnProperValue(number metres, string standardFormat, string cultureName, string expectedResult, string reason)
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
            [InlineData(1d, "mm", "en-US", "1000 mm")]
            [InlineData(1d, "cm", "en-US", "100 cm")]
            [InlineData(1d, "dm", "en-US", "10 dm")]
            [InlineData(1d, "m", "en-US", "1 m")]
            [InlineData(1d, "km", "en-US", "0.001 km")]
            [InlineData(1d, "in", "en-US", "39.37007874 in")]
            [InlineData(1d, "ft", "en-US", "3.2808399 ft")]
            [InlineData(1d, "yd", "en-US", "1.0936133 yd")]
            [InlineData(100d, "ch", "en-US", "4.97096954 ch")]
            [InlineData(100000d, "mi", "en-US", "62.13711922 mi")]
            [InlineData(100000d, "nmi", "en-US", "53.99568035 nmi")]
            [InlineData(1d, "ft|v0.###", "en-US", "3.281 ft")]
            [InlineData(1d, "ft|ull", "en-US", "3.2808399 feet")]
            [InlineData(1d, "ft|ull|v0.###", "en-US", "3.281 feet")]
            [InlineData(1d, "km", "pl-PL", "0,001 km")]
            public void CustomFormat_ShouldReturnProperValue(number metres, string customFormat, string cultureName, string expectedResult)
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
