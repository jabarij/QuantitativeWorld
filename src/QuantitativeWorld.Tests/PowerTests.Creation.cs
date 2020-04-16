using AutoFixture;
using FluentAssertions;
using QuantitativeWorld.TestAbstractions;
using System.Collections.Generic;
using Xunit;

namespace QuantitativeWorld.Tests
{
    partial class PowerTests
    {
        public class Creation : PowerTests
        {
            public Creation(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void ConstructorForWatts_ShouldCreateValidPower()
            {
                // arrange
                decimal watts = Fixture.Create<decimal>();

                // act
                var power = new Power(watts);

                // assert
                power.Watts.Should().Be(watts);
                power.Value.Should().Be(watts);
                power.Unit.Should().Be(PowerUnit.Watt);
            }

            [Theory]
            [MemberData(nameof(GetTestData), typeof(Creation), nameof(GetConstructorForValueAndUnitTestData))]
            public void ConstructorForValueAndUnit_ShouldCreateValidPower(ConstructorForValueAndUnitTestData testData)
            {
                // arrange
                // act
                var power = new Power(testData.Value, testData.Unit);

                // assert
                power.Watts.Should().BeApproximately(testData.ExpectedWatts, DecimalPrecision);
                power.Value.Should().Be(testData.Value);
                power.Unit.Should().Be(testData.Unit);
            }
            private static IEnumerable<ConstructorForValueAndUnitTestData> GetConstructorForValueAndUnitTestData()
            {
                yield return new ConstructorForValueAndUnitTestData(1m, PowerUnit.Watt, 1m);
                yield return new ConstructorForValueAndUnitTestData(1000m, PowerUnit.Milliwatt, 1m);
                yield return new ConstructorForValueAndUnitTestData(0.001m, PowerUnit.Kilowatt, 1m);
                yield return new ConstructorForValueAndUnitTestData(0.000001m, PowerUnit.Megawatt, 1m);
                yield return new ConstructorForValueAndUnitTestData(1 / (76.0402249m * 9.80665m), PowerUnit.MechanicalHorsepower, 1m);
            }
            public class ConstructorForValueAndUnitTestData
            {
                public ConstructorForValueAndUnitTestData(decimal value, PowerUnit unit, decimal expectedWatts)
                {
                    Value = value;
                    Unit = unit;
                    ExpectedWatts = expectedWatts;
                }

                public decimal Value { get; }
                public PowerUnit Unit { get; }
                public decimal ExpectedWatts { get; }
            }

            [Theory]
            [InlineData(0.001, 1000)]
            [InlineData(1, 1000000)]
            [InlineData(1000, 1000000000)]
            public void FromKilowatts_ShouldCreateValidPower(decimal kilowatts, decimal milliwatts)
            {
                // arrange
                var expectedPower = new Power(milliwatts, PowerUnit.Milliwatt);

                // act
                var actualPower = new Power(kilowatts, PowerUnit.Kilowatt);

                // assert
                actualPower.Should().Be(expectedPower);
            }
        }
    }
}
