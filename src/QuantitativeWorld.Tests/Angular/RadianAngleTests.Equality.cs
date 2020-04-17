using AutoFixture;
using FluentAssertions;
using QuantitativeWorld.Angular;
using QuantitativeWorld.TestAbstractions;
using Xunit;

namespace QuantitativeWorld.Tests.Angular
{
    partial class RadianAngleTests
    {
        public class Equality : RadianAngleTests
        {
            public Equality(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void DefaultRadianAngle_ShouldBeEqualToZeroRadians()
            {
                // arrange
                var defaultRadianAngle = default(RadianAngle);
                var zeroRadiansRadianAngle = new RadianAngle(0m);

                // act
                // assert
                zeroRadiansRadianAngle.Equals(defaultRadianAngle).Should().BeTrue(because: "'new RadianAngle(0m)' should be equal 'default(RadianAngle)'");
                defaultRadianAngle.Equals(zeroRadiansRadianAngle).Should().BeTrue(because: "'default(RadianAngle)' should be equal 'new RadianAngle(0m)'");
            }

            [Fact]
            public void RadianAngleCreateUtinsParamlessConstructor_ShouldBeEqualToZeroRadians()
            {
                // arrange
                var zeroRadiansRadianAngle = new RadianAngle(0m);
                var paramlessConstructedRadianAngle = new RadianAngle();

                // act
                // assert
                zeroRadiansRadianAngle.Equals(paramlessConstructedRadianAngle).Should().BeTrue(because: "'new RadianAngle(0m)' should be equal 'new RadianAngle()'");
                paramlessConstructedRadianAngle.Equals(zeroRadiansRadianAngle).Should().BeTrue(because: "'new RadianAngle()' should be equal 'new RadianAngle(0m)'");
            }

            [Fact]
            public void RadianAnglesOfDifferentUnitsEqualInRadians_ShouldBeEqual()
            {
                // arrange
                var radianRadianAngle1 = new RadianAngle(
                    radians: Fixture.Create<decimal>());
                var radianRadianAngle2 = new RadianAngle(
                    radians: radianRadianAngle1.Radians);

                // act
                bool equalsResult = radianRadianAngle1.Equals(radianRadianAngle2);

                // assert
                equalsResult.Should().BeTrue(because: $"{radianRadianAngle1.Radians} rad == {radianRadianAngle2.Radians} rad");
            }
        }
    }
}
