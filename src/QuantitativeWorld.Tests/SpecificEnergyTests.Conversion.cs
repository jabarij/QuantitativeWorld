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
    using number = System.Double;
#endif

    partial class SpecificEnergyTests
    {
        public class Convert : SpecificEnergyTests
        {
            public Convert(TestFixture testFixture)
                : base(testFixture) { }

            [Theory]
            [MemberData(nameof(GetTestData), typeof(Convert), nameof(GetConvertTestData))]
            public void ShouldConvertToExpectedValue(SpecificEnergy originalSpecificEnergy, SpecificEnergyUnit targetUnit, SpecificEnergy expectedSpecificEnergy)
            {
                // arrange
                // act
                var actualSpecificEnergy = originalSpecificEnergy.Convert(targetUnit);

                // assert
                actualSpecificEnergy.JoulesPerKilogram.Should().BeApproximately(expectedSpecificEnergy.JoulesPerKilogram);
                actualSpecificEnergy.Value.Should().BeApproximately(expectedSpecificEnergy.Value);
                actualSpecificEnergy.Unit.Should().Be(targetUnit);
            }
            private static IEnumerable<ITestDataProvider> GetConvertTestData()
            {
                //yield return new ConvertTestData(1234.56m, SpecificEnergyUnit.JoulePerKilogram, 1.23456m, SpecificEnergyUnit.KilojoulePerKilogram);
                //yield return new ConvertTestData(1.23456m, SpecificEnergyUnit.KilojoulePerKilogram, 1234.56m, SpecificEnergyUnit.JoulePerKilogram);

                yield return new ConvertTestData(1234.56m, SpecificEnergyUnit.KilojoulePerHundredGrams, 295.0669216061185m, SpecificEnergyUnit.KilocaloriePerHundredGrams);
                yield return new ConvertTestData(1234.56m, SpecificEnergyUnit.KilocaloriePerHundredGrams, 5165.39904m, SpecificEnergyUnit.KilojoulePerHundredGrams);
            }
            class ConvertTestData : ConversionTestData<SpecificEnergy>, ITestDataProvider
            {
                public ConvertTestData(decimal originalValue, SpecificEnergyUnit originalUnit, decimal expectedValue, SpecificEnergyUnit expectedUnit)
                    : base(new SpecificEnergy((number)originalValue, originalUnit), new SpecificEnergy((number)expectedValue, expectedUnit)) { }
                public ConvertTestData(double originalValue, SpecificEnergyUnit originalUnit, double expectedValue, SpecificEnergyUnit expectedUnit)
                    : base(new SpecificEnergy((number)originalValue, originalUnit), new SpecificEnergy((number)expectedValue, expectedUnit)) { }

                public object[] GetTestParameters() =>
                    new[] { (object)OriginalValue, ExpectedValue.Unit, ExpectedValue };
            }

            [Fact]
            public void MultipleSerialConversion_ShouldHaveSameValueAtTheEnd()
            {
                // arrange
                var units = new List<SpecificEnergyUnit>
                {
                    SpecificEnergyUnit.JoulePerKilogram,
                    SpecificEnergyUnit.KilojoulePerKilogram,
                    SpecificEnergyUnit.MegajoulePerKilogram,
                    SpecificEnergyUnit.JoulePerGram,
                    SpecificEnergyUnit.KilojoulePerGram,
                    SpecificEnergyUnit.CaloriePerKilogram,
                    SpecificEnergyUnit.CaloriePerGram,
                    SpecificEnergyUnit.KilocaloriePerKilogram,
                    SpecificEnergyUnit.KilocaloriePerGram,
                    SpecificEnergyUnit.KilojoulePerHundredGrams,
                    SpecificEnergyUnit.KilocaloriePerHundredGrams,
                    SpecificEnergyUnit.JoulePerKilogram
                };
                var initialSpecificEnergy = new SpecificEnergy((number)0.12345678m, units.First());
                SpecificEnergy? finalSpecificEnergy = null;

                // act
                units.ForEach(u => finalSpecificEnergy = (finalSpecificEnergy ?? initialSpecificEnergy).Convert(u));

                // assert
                finalSpecificEnergy.Should().Be(initialSpecificEnergy);
            }
        }
    }
}
