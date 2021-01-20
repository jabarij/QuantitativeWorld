using AutoFixture;
using FluentAssertions;
using QuantitativeWorld.TestAbstractions;
using System.Collections.Generic;
using Xunit;

namespace QuantitativeWorld.Tests
{
#if DECIMAL
    using number = System.Decimal;
    using Constants = QuantitativeWorld.DecimalConstants;
#else
    using number = System.Double;
    using Constants = QuantitativeWorld.DoubleConstants;
#endif

    partial class SpeedTests
    {
        public class Creation : SpeedTests
        {
            public Creation(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void ConstructorForMetresPerSecond_ShouldCreateValidSpeed()
            {
                // arrange
                number metresPerSecond = Fixture.Create<number>();

                // act
                var length = new Speed(metresPerSecond);

                // assert
                length.MetresPerSecond.Should().Be(metresPerSecond);
                length.Value.Should().Be(metresPerSecond);
                length.Unit.Should().Be(SpeedUnit.MetrePerSecond);
            }

            [Theory]
            [MemberData(nameof(GetTestData), typeof(Creation), nameof(GetConstructorForValueAndUnitTestData))]
            public void ConstructorForValueAndUnit_ShouldCreateValidSpeed(ConstructorForValueAndUnitTestData testData)
            {
                // arrange
                // act
                var length = new Speed(testData.Value, testData.Unit);

                // assert
                length.MetresPerSecond.Should().BeApproximately(testData.ExpectedMetresPerSecond);
                length.Value.Should().BeApproximately(testData.Value);
                length.Unit.Should().Be(testData.Unit);
            }
            private static IEnumerable<ConstructorForValueAndUnitTestData> GetConstructorForValueAndUnitTestData()
            {
                yield return new ConstructorForValueAndUnitTestData(1d, SpeedUnit.MetrePerSecond, 1d);
                yield return new ConstructorForValueAndUnitTestData(100d, SpeedUnit.KilometrePerHour, 100d / 3.6d);
                yield return new ConstructorForValueAndUnitTestData(10d, SpeedUnit.MilePerHour, 4.4704d);
            }
            public class ConstructorForValueAndUnitTestData
            {
                public ConstructorForValueAndUnitTestData(double value, SpeedUnit unit, double expectedMetresPerSecond)
                {
                    Value = (number)value;
                    Unit = unit;
                    ExpectedMetresPerSecond = (number)expectedMetresPerSecond;
                }
                public ConstructorForValueAndUnitTestData(decimal value, SpeedUnit unit, decimal expectedMetresPerSecond)
                {
                    Value = (number)value;
                    Unit = unit;
                    ExpectedMetresPerSecond = (number)expectedMetresPerSecond;
                }

                public number Value { get; }
                public SpeedUnit Unit { get; }
                public number ExpectedMetresPerSecond { get; }
            }

            [Theory]
            [InlineData(100, 100 / 3.6d)]
            [InlineData(1d, 1d / 3.6d)]
            [InlineData(0.01d, 0.01d / 3.6d)]
            public void FromKilometresPerHour_ShouldCreateValidSpeed(number kilometresPerHour, number metresPerSecond)
            {
                // arrange
                var expectedSpeed = new Speed(metresPerSecond, SpeedUnit.MetrePerSecond);

                // act
                var actualSpeed = new Speed(kilometresPerHour, SpeedUnit.KilometrePerHour);

                // assert
                actualSpeed.MetresPerSecond.Should().BeApproximately(expectedSpeed.MetresPerSecond);
            }
        }
    }
}
