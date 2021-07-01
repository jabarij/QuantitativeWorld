using AutoFixture;
using FluentAssertions;
using System.Collections.Generic;
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
        public class Creation : SpecificEnergyTests
        {
            public Creation(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void ConstructorForJoulesPerKilogram_ShouldCreateValidSpecificEnergy()
            {
                // arrange
                number joulesPerKilogram = Fixture.Create<number>();

                // act
                var specificEnergy = new SpecificEnergy(joulesPerKilogram);

                // assert
                specificEnergy.JoulesPerKilogram.Should().Be(joulesPerKilogram);
                specificEnergy.Value.Should().Be(joulesPerKilogram);
                specificEnergy.Unit.Should().Be(SpecificEnergyUnit.JoulePerKilogram);
            }

            [Theory]
            [MemberData(nameof(GetTestData), typeof(Creation), nameof(GetConstructorForValueAndUnitTestData))]
            public void ConstructorForValueAndUnit_ShouldCreateValidSpecificEnergy(ConstructorForValueAndUnitTestData testData)
            {
                // arrange
                // act
                var specificEnergy = new SpecificEnergy(testData.OriginalValue.Value, testData.OriginalValue.Unit);

                // assert
                specificEnergy.JoulesPerKilogram.Should().BeApproximately(testData.ExpectedValue.JoulesPerKilogram);
                specificEnergy.Value.Should().BeApproximately(testData.OriginalValue.Value);
                specificEnergy.Unit.Should().Be(testData.OriginalValue.Unit);
            }
            private static IEnumerable<ConstructorForValueAndUnitTestData> GetConstructorForValueAndUnitTestData()
            {
                yield return new ConstructorForValueAndUnitTestData(1m, SpecificEnergyUnit.JoulePerKilogram, 1m);
                yield return new ConstructorForValueAndUnitTestData(1m, SpecificEnergyUnit.KilojoulePerKilogram, 1000m);
                yield return new ConstructorForValueAndUnitTestData(1m, SpecificEnergyUnit.MegajoulePerKilogram, 1000000m);
                yield return new ConstructorForValueAndUnitTestData(1m, SpecificEnergyUnit.JoulePerGram, 1000m);
                yield return new ConstructorForValueAndUnitTestData(1m, SpecificEnergyUnit.KilojoulePerGram, 1000000m);

                yield return new ConstructorForValueAndUnitTestData(1m, SpecificEnergyUnit.CaloriePerKilogram, 4.184m);
                yield return new ConstructorForValueAndUnitTestData(1m, SpecificEnergyUnit.CaloriePerGram, 4184m);
                yield return new ConstructorForValueAndUnitTestData(1m, SpecificEnergyUnit.KilocaloriePerKilogram, 4184m);
                yield return new ConstructorForValueAndUnitTestData(1m, SpecificEnergyUnit.KilocaloriePerGram, 4184000m);

                yield return new ConstructorForValueAndUnitTestData(1m, SpecificEnergyUnit.KilojoulePerHundredGrams, 10000m);
                yield return new ConstructorForValueAndUnitTestData(1m, SpecificEnergyUnit.KilocaloriePerHundredGrams, 41840m);
            }
            public class ConstructorForValueAndUnitTestData : ConversionTestData<SpecificEnergy>
            {
                public ConstructorForValueAndUnitTestData(decimal value, SpecificEnergyUnit unit, decimal expectedJoulesPerKilogram)
                    : base(new SpecificEnergy((number)value, unit), new SpecificEnergy((number)expectedJoulesPerKilogram)) { }
                public ConstructorForValueAndUnitTestData(double value, SpecificEnergyUnit unit, double expectedJoulesPerKilogram)
                    : base(new SpecificEnergy((number)value, unit), new SpecificEnergy((number)expectedJoulesPerKilogram)) { }
            }

            [Theory]
            [InlineData(0.1, 100)]
            [InlineData(1, 1000)]
            [InlineData(10, 10000)]
            public void FromKilojoulesPerKilogram_ShouldCreateValidSpecificEnergy(number kilojoulesPerKilogram, number joulesPerKilogram)
            {
                // arrange
                var expectedSpecificEnergy = new SpecificEnergy(joulesPerKilogram, SpecificEnergyUnit.JoulePerKilogram);

                // act
                var actualSpecificEnergy = new SpecificEnergy(kilojoulesPerKilogram, SpecificEnergyUnit.KilojoulePerKilogram);

                // assert
                actualSpecificEnergy.Should().Be(expectedSpecificEnergy);
            }
        }
    }
}
