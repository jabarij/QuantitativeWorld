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
    using Constants = DoubleConstants;
    using number = System.Double;
#endif

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
                number watts = Fixture.Create<number>();

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
                var power = new Power(testData.OriginalValue.Value, testData.OriginalValue.Unit);

                // assert
                power.Watts.Should().BeApproximately(testData.ExpectedValue.Watts);
                power.Value.Should().Be(testData.OriginalValue.Value);
                power.Unit.Should().Be(testData.OriginalValue.Unit);
            }
            private static IEnumerable<ConstructorForValueAndUnitTestData> GetConstructorForValueAndUnitTestData()
            {
                yield return new ConstructorForValueAndUnitTestData(1m, PowerUnit.Watt, 1m, PowerUnit.Watt);
                yield return new ConstructorForValueAndUnitTestData(1000m, PowerUnit.Milliwatt, 1m, PowerUnit.Watt);
                yield return new ConstructorForValueAndUnitTestData(0.001m, PowerUnit.Kilowatt, 1m, PowerUnit.Watt);
                yield return new ConstructorForValueAndUnitTestData(0.000001m, PowerUnit.Megawatt, 1m, PowerUnit.Watt);
                yield return new ConstructorForValueAndUnitTestData(1 / (76.0402249m * 9.80665m), PowerUnit.MechanicalHorsepower, 1m, PowerUnit.Watt);
            }
            public class ConstructorForValueAndUnitTestData : ConversionTestData<Power>
            {
                public ConstructorForValueAndUnitTestData(decimal originalValue, PowerUnit originalUnit, decimal expectedValue, PowerUnit expectedUnit)
                    : base(new Power((number)originalValue, originalUnit), new Power((number)expectedValue, expectedUnit)) { }
                public ConstructorForValueAndUnitTestData(double originalValue, PowerUnit originalUnit, double expectedValue, PowerUnit expectedUnit)
                    : base(new Power((number)originalValue, originalUnit), new Power((number)expectedValue, expectedUnit)) { }
            }

            [Theory]
            [InlineData(0.001, 1000)]
            [InlineData(1, 1000000)]
            [InlineData(1000, 1000000000)]
            public void FromKilowatts_ShouldCreateValidPower(number kilowatts, number milliwatts)
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
