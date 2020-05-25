using AutoFixture;
using FluentAssertions;
using QuantitativeWorld.TestAbstractions;
using System.Collections.Generic;
using Xunit;

namespace QuantitativeWorld.Tests
{
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
                double metresPerSecond = Fixture.Create<double>();

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
                length.MetresPerSecond.Should().BeApproximately(testData.ExpectedMetresPerSecond, DoublePrecision);
                length.Value.Should().BeApproximately(testData.Value, DoublePrecision);
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
                    Value = value;
                    Unit = unit;
                    ExpectedMetresPerSecond = expectedMetresPerSecond;
                }

                public double Value { get; }
                public SpeedUnit Unit { get; }
                public double ExpectedMetresPerSecond { get; }
            }

            [Theory]
            [InlineData(100d, 100d / 3.6d)]
            [InlineData(1d, 1d / 3.6d)]
            [InlineData(0.01d, 0.01d / 3.6d)]
            public void FromKilometresPerHour_ShouldCreateValidSpeed(double kilometresPerHour, double metresPerSecond)
            {
                // arrange
                var expectedSpeed = new Speed(metresPerSecond, SpeedUnit.MetrePerSecond);

                // act
                var actualSpeed = new Speed(kilometresPerHour, SpeedUnit.KilometrePerHour);

                // assert
                actualSpeed.MetresPerSecond.Should().BeApproximately(expectedSpeed.MetresPerSecond, DoublePrecision);
            }
        }
    }
}
