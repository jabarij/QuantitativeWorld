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
                double watts = Fixture.Create<double>();

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
                power.Watts.Should().BeApproximately(testData.ExpectedWatts, DoublePrecision);
                power.Value.Should().Be(testData.Value);
                power.Unit.Should().Be(testData.Unit);
            }
            private static IEnumerable<ConstructorForValueAndUnitTestData> GetConstructorForValueAndUnitTestData()
            {
                yield return new ConstructorForValueAndUnitTestData(1d, PowerUnit.Watt, 1d);
                yield return new ConstructorForValueAndUnitTestData(1000d, PowerUnit.Milliwatt, 1d);
                yield return new ConstructorForValueAndUnitTestData(0.001d, PowerUnit.Kilowatt, 1d);
                yield return new ConstructorForValueAndUnitTestData(0.000001d, PowerUnit.Megawatt, 1d);
                yield return new ConstructorForValueAndUnitTestData(1 / (76.0402249d * 9.80665d), PowerUnit.MechanicalHorsepower, 1d);
            }
            public class ConstructorForValueAndUnitTestData
            {
                public ConstructorForValueAndUnitTestData(double value, PowerUnit unit, double expectedWatts)
                {
                    Value = value;
                    Unit = unit;
                    ExpectedWatts = expectedWatts;
                }

                public double Value { get; }
                public PowerUnit Unit { get; }
                public double ExpectedWatts { get; }
            }

            [Theory]
            [InlineData(0.001, 1000)]
            [InlineData(1, 1000000)]
            [InlineData(1000, 1000000000)]
            public void FromKilowatts_ShouldCreateValidPower(double kilowatts, double milliwatts)
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
