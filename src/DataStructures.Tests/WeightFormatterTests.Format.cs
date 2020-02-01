using QuantitativeWorld.Formatting;
using FluentAssertions;
using System.Globalization;
using Xunit;

namespace QuantitativeWorld.Tests
{
    partial class WeightFormatterTests
    {
        public class Format : WeightFormatterTests
        {
            public Format(TestFixture testFixture)
                : base(testFixture) { }

            [Theory]
            [InlineData(1, StandardFormats.SI, "en-US", "1 kg", "weight in format 'SI' should be printed in kilograms")]
            [InlineData(1, StandardFormats.CGS, "en-US", "1000 g", "weight in format 'CGS' should be printed in grams")]
            [InlineData(1, StandardFormats.IMP, "en-US", "2.20462262 lbs", "weight in format 'IMP' should be printed in pounds")]
            [InlineData(1, StandardFormats.MKS, "en-US", "1 kg", "weight in format 'MKS' should be printed in kilograms")]
            [InlineData(1, StandardFormats.MTS, "en-US", "0.001 t", "weight in format 'MKS' should be printed in tons")]
            [InlineData(0.000000009, StandardFormats.SI, "en-US", "0.00000001 kg", "weight in standard format should be printed with max 8 decimal places")]
            [InlineData(123.456, StandardFormats.SI, "pl-PL", "123,456 kg", "weight in standard format should be printed with decimal separator appropriate to given culture")]
            public void StandardFormat_ShouldReturnProperValue(decimal kilograms, string standardFormat, string cultureName, string expectedResult, string reason)
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
            [InlineData(1, "kg", "en-US", "1 kg")]
            [InlineData(1, "t", "en-US", "0.001 t")]
            [InlineData(1, "g", "en-US", "1000 g")]
            [InlineData(1, "lbs", "en-US", "2.20462262 lbs")]
            [InlineData(1, "lbs|v0.###", "en-US", "2.205 lbs")]
            [InlineData(1, "lbs|ull", "en-US", "2.20462262 pounds")]
            [InlineData(1, "lbs|ull|v0.###", "en-US", "2.205 pounds")]
            [InlineData(1, "t", "pl-PL", "0,001 t")]
            public void CustomFormat_ShouldReturnProperValue(decimal kilograms, string customFormat, string cultureName, string expectedResult)
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
