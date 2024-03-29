using FluentAssertions;
using System.Globalization;
using Xunit;

#if DECIMAL
namespace DecimalQuantitativeWorld.Tests.Formatting
{
    using DecimalQuantitativeWorld.TestAbstractions;
    using DecimalQuantitativeWorld.Text.Formatting;
    using number = System.Decimal;
#else
namespace QuantitativeWorld.Tests.Formatting
{
    using QuantitativeWorld.TestAbstractions;
    using QuantitativeWorld.Text.Formatting;
    using number = System.Double;
#endif

    partial class WeightFormatterTests
    {
        public class StringFormat : WeightFormatterTests
        {
            public StringFormat(TestFixture testFixture)
                : base(testFixture) { }

            [Theory]
            [InlineData(1d, "Weight is: {0:" + StandardFormats.SI + "}", "en-US", "Weight is: 1 kg", "weight in format 'SI' should be printed in kilograms")]
            [InlineData(1d, "Weight is: {0:" + StandardFormats.CGS + "}", "en-US", "Weight is: 1000 g", "weight in format 'CGS' should be printed in grams")]
            [InlineData(1d, "Weight is: {0:" + StandardFormats.IMP + "}", "en-US", "Weight is: 2.20462262 lb", "weight in format 'IMP' should be printed in pounds")]
            [InlineData(1d, "Weight is: {0:" + StandardFormats.MKS + "}", "en-US", "Weight is: 1 kg", "weight in format 'MKS' should be printed in kilograms")]
            [InlineData(1d, "Weight is: {0:" + StandardFormats.MTS + "}", "en-US", "Weight is: 0.001 t", "weight in format 'MKS' should be printed in tons")]
            [InlineData(0.000000009d, "Weight is: {0:" + StandardFormats.SI + "}", "en-US", "Weight is: 0.00000001 kg", "weight in standard format should be printed with max 8 decimal places")]
            [InlineData(123.456d, "Weight is: {0:" + StandardFormats.SI + "}", "pl-PL", "Weight is: 123,456 kg", "weight in standard format should be printed with number separator appropriate to given culture")]
            public void StringFormat_StandardFormat_ShouldReturnProperValue(number kilograms, string format, string cultureName, string expectedResult, string reason)
            {
                // arrange
                var formatter = new WeightFormatter(CultureInfo.GetCultureInfo(cultureName));
                var weight = new Weight(kilograms, WeightUnit.Kilogram);

                // act
                string actualResult = string.Format(formatter, format, weight);

                // assert
                actualResult.Should().Be(expectedResult, because: reason);
            }

            [Theory]
            [InlineData(1d, "Weight is: {0:kg}", "en-US", "Weight is: 1 kg")]
            [InlineData(1d, "Weight is: {0:t}", "en-US", "Weight is: 0.001 t")]
            [InlineData(1d, "Weight is: {0:g}", "en-US", "Weight is: 1000 g")]
            [InlineData(1d, "Weight is: {0:lb}", "en-US", "Weight is: 2.20462262 lb")]
            [InlineData(1d, "Weight is: {0:lb|v0.###}", "en-US", "Weight is: 2.205 lb")]
            [InlineData(1d, "Weight is: {0:lb|ull}", "en-US", "Weight is: 2.20462262 pounds")]
            [InlineData(1d, "Weight is: {0:lb|ull|v0.###}", "en-US", "Weight is: 2.205 pounds")]
            public void StringFormat_CustomFormat_ShouldReturnProperValue(number kilograms, string customFormat, string cultureName, string expectedResult)
            {
                // arrange
                var formatter = new WeightFormatter(CultureInfo.GetCultureInfo(cultureName));
                var weight = new Weight(kilograms, WeightUnit.Kilogram);

                // act
                string actualResult = string.Format(formatter, customFormat, weight);

                // assert
                actualResult.Should().Be(expectedResult);
            }
        }
    }
}
