using FluentAssertions;
using System.Globalization;
using Xunit;

namespace DataStructures.Tests
{
    partial class WeightTests
    {
        public class Stringifying
        {
            [Theory]
            [InlineData(1000.123456, "CGS", "en-US", "1000123.456 g")]
            [InlineData(1000.123456, "CGS", "pl-PL", "1000123,456 g")]
            [InlineData(453.64836869963072, "IMP", "en-US", "1000.123456 lbs")]
            [InlineData(453.64836869963072, "IMP", "pl-PL", "1000,123456 lbs")]
            //[InlineData(1000.123456, "MKS", "en-US", "1 kg")]
            //[InlineData(1000.123456, "MKS", "pl-PL", "1 kg")]
            //[InlineData(1000.123456, "MTS", "en-US", "0.001 t")]
            //[InlineData(1000.123456, "MTS", "pl-PL", "0,001 t")]
            //[InlineData(1000.123456, "SI", "en-US", "1 kg")]
            //[InlineData(1000.123456, "SI", "pl-PL", "1000,123 kg")]
            public void ToString_StandardFormat_ShouldReturnProperValue(decimal kilograms, string standardFormat, string cultureName, string expectedResult)
            {
                var weight2 = new Weight(1000.123456m, WeightUnit.Pound);
                var weight3 = new UnitConverter().Convert(weight2, WeightUnit.Kilogram);
                var weight4 = new UnitConverter().Convert(weight3, WeightUnit.Pound);
                // arrange
                var weight = new Weight(kilograms, WeightUnit.Kilogram);

                // act
                string actualResult = weight.ToString(standardFormat, CultureInfo.GetCultureInfo(cultureName));

                // assert
                actualResult.Should().Be(expectedResult);
            }
        }
    }
}
