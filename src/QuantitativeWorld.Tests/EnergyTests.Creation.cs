using AutoFixture;
using FluentAssertions;
using QuantitativeWorld.TestAbstractions;
using System.Collections.Generic;
using Xunit;

namespace QuantitativeWorld.Tests
{
#if DECIMAL
    using number = System.Decimal;
#else
    using number = System.Double;
    using Constants = QuantitativeWorld.DoubleConstants;
#endif

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
                number watts = Fixture.Create<number>();

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
                var energy = new Energy(testData.OriginalValue.Value, testData.OriginalValue.Unit);

                // assert
                energy.Joules.Should().BeApproximately(testData.ExpectedValue.Joules);
                energy.Value.Should().Be(testData.OriginalValue.Value);
                energy.Unit.Should().Be(testData.OriginalValue.Unit);
            }
            private static IEnumerable<ConstructorForValueAndUnitTestData> GetConstructorForValueAndUnitTestData()
            {
                yield return new ConstructorForValueAndUnitTestData(1m, EnergyUnit.Joule, 1m);
                yield return new ConstructorForValueAndUnitTestData(0.001m, EnergyUnit.Kilojoule, 1m);
                yield return new ConstructorForValueAndUnitTestData(0.000001m, EnergyUnit.Megajoule, 1m);
            }
            public class ConstructorForValueAndUnitTestData : ConversionTestData<Energy>
            {
                public ConstructorForValueAndUnitTestData(decimal value, EnergyUnit unit, decimal expectedJoules)
                    : base(new Energy((number)value, unit), new Energy((number)expectedJoules)) { }
            }

            [Theory]
            [InlineData(1, 1000)]
            [InlineData(1000, 1000000)]
            public void FromKilojoules_ShouldCreateValidEnergy(number kilojoules, number wattSeconds)
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
