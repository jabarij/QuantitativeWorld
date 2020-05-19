using AutoFixture;
using FluentAssertions;
using QuantitativeWorld.TestAbstractions;
using System.Collections.Generic;
using Xunit;

namespace QuantitativeWorld.Tests
{
    partial class EnergyTests
    {
        public class Creation : EnergyTests
        {
            public Creation(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void ConstructorForJoules_ShouldCreateValidEnergy()
            {
                // arrange
                double watts = Fixture.Create<double>();

                // act
                var energy = new Energy(watts);

                // assert
                energy.Joules.Should().Be(watts);
                energy.Value.Should().Be(watts);
                energy.Unit.Should().Be(EnergyUnit.Joule);
            }

            [Theory]
            [MemberData(nameof(GetTestData), typeof(Creation), nameof(GetConstructorForValueAndUnitTestData))]
            public void ConstructorForValueAndUnit_ShouldCreateValidEnergy(ConstructorForValueAndUnitTestData testData)
            {
                // arrange
                // act
                var energy = new Energy(testData.Value, testData.Unit);

                // assert
                energy.Joules.Should().BeApproximately(testData.ExpectedJoules, DoublePrecision);
                energy.Value.Should().Be(testData.Value);
                energy.Unit.Should().Be(testData.Unit);
            }
            private static IEnumerable<ConstructorForValueAndUnitTestData> GetConstructorForValueAndUnitTestData()
            {
                yield return new ConstructorForValueAndUnitTestData(1d, EnergyUnit.Joule, 1d);
                yield return new ConstructorForValueAndUnitTestData(0.001d, EnergyUnit.Kilojoule, 1d);
                yield return new ConstructorForValueAndUnitTestData(0.000001d, EnergyUnit.Megajoule, 1d);
            }
            public class ConstructorForValueAndUnitTestData
            {
                public ConstructorForValueAndUnitTestData(double value, EnergyUnit unit, double expectedJoules)
                {
                    Value = value;
                    Unit = unit;
                    ExpectedJoules = expectedJoules;
                }

                public double Value { get; }
                public EnergyUnit Unit { get; }
                public double ExpectedJoules { get; }
            }

            [Theory]
            [InlineData(1, 1000)]
            [InlineData(1000, 1000000)]
            public void FromKilojoules_ShouldCreateValidEnergy(double kilojoules, double wattSeconds)
            {
                // arrange
                var expectedEnergy = new Energy(wattSeconds, EnergyUnit.WattSecond);

                // act
                var actualEnergy = new Energy(kilojoules, EnergyUnit.Kilojoule);

                // assert
                actualEnergy.Should().Be(expectedEnergy);
            }
        }
    }
}
