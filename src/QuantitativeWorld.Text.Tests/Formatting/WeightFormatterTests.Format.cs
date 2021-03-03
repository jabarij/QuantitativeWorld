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
        public class Format : WeightFormatterTests
        {
            public Format(TestFixture testFixture)
                : base(testFixture) { }

            [Theory]
            [InlineData(1d, StandardFormats.SI, "en-US", "1 kg", "weight in format 'SI' should be printed in kilograms")]
            [InlineData(1d, StandardFormats.CGS, "en-US", "1000 g", "weight in format 'CGS' should be printed in grams")]
            [InlineData(1d, StandardFormats.IMP, "en-US", "2.20462262 lbs", "weight in format 'IMP' should be printed in pounds")]
            [InlineData(1d, StandardFormats.MKS, "en-US", "1 kg", "weight in format 'MKS' should be printed in kilograms")]
            [InlineData(1d, StandardFormats.MTS, "en-US", "0.001 t", "weight in format 'MKS' should be printed in tons")]
            [InlineData(0.000000009d, StandardFormats.SI, "en-US", "0.00000001 kg", "weight in standard format should be printed with max 8 decimal places")]
            [InlineData(123.456d, StandardFormats.SI, "pl-PL", "123,456 kg", "weight in standard format should be printed with number separator appropriate to given culture")]
            public void StandardFormat_ShouldReturnProperValue(number kilograms, string standardFormat, string cultureName, string expectedResult, string reason)
            {
                // arrange
                var formatter = new WeightFormatter(CultureInfo.GetCultureInfo(cultureName));
                var weight = new Weight(kilograms, WeightUnit.Kilogram);

                // act
                string actualResult = formatter.Format(standardFormat, weight);

                // assert
                actualResult.Should().Be(expectedResult, because: reason);
            }

            [Theory]
            [InlineData(1d, "kg", "en-US", "1 kg")]
            [InlineData(1d, "t", "en-US", "0.001 t")]
            [InlineData(1d, "g", "en-US", "1000 g")]
            [InlineData(1d, "lbs", "en-US", "2.20462262 lbs")]
            [InlineData(1d, "lbs|v0.###", "en-US", "2.205 lbs")]
            [InlineData(1d, "lbs|ull", "en-US", "2.20462262 pounds")]
            [InlineData(1d, "lbs|ull|v0.###", "en-US", "2.205 pounds")]
            [InlineData(1d, "t", "pl-PL", "0,001 t")]
            public void CustomFormat_ShouldReturnProperValue(number kilograms, string customFormat, string cultureName, string expectedResult)
            {
                // arrange
                var formatter = new WeightFormatter(CultureInfo.GetCultureInfo(cultureName));
                var weight = new Weight(kilograms, WeightUnit.Kilogram);

                // act
                string actualResult = formatter.Format(customFormat, weight);

                // assert
                actualResult.Should().Be(expectedResult);
            }
        }
    }
}
