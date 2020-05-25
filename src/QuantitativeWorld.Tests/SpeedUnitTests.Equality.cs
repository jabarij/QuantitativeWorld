using FluentAssertions;
using QuantitativeWorld.TestAbstractions;
using Xunit;

namespace QuantitativeWorld.Tests
{
    partial class SpeedUnitTests
    {
        public class Equality : SpeedUnitTests
        {
            public Equality(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void DefaultSpeedUnit_ShouldBeEqualToMetrePerSecond()
            {
                // arrange
                var defaultSpeedUnit = default(SpeedUnit);
                var metrePerSecond = SpeedUnit.MetrePerSecond;

                // act
                // assert
                metrePerSecond.Equals(defaultSpeedUnit).Should().BeTrue(because: "'SpeedUnit.MetrePerSecond' should be equal 'default(SpeedUnit)'");
                defaultSpeedUnit.Equals(metrePerSecond).Should().BeTrue(because: "'default(SpeedUnit)' should be equal 'SpeedUnit.MetrePerSecond'");
            }

            [Fact]
            public void ParamlessConstructedSpeedUnit_ShouldBeEqualToMetrePerSecond()
            {
                // arrange
                var paramlessConstructedSpeedUnit = new SpeedUnit();
                var metrePerSecond = SpeedUnit.MetrePerSecond;

                // act
                // assert
                metrePerSecond.Equals(paramlessConstructedSpeedUnit).Should().BeTrue(because: "'SpeedUnit.MetrePerSecond' should be equal 'new SpeedUnit()'");
                paramlessConstructedSpeedUnit.Equals(metrePerSecond).Should().BeTrue(because: "'new SpeedUnit()' should be equal 'SpeedUnit.MetrePerSecond'");
            }
        }
    }
}
