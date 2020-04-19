using AutoFixture;
using FluentAssertions;
using QuantitativeWorld.Angular;
using QuantitativeWorld.TestAbstractions;
using Xunit;

namespace QuantitativeWorld.Tests.Angular
{
    partial class RadianAngleTests
    {
        public class Creation : RadianAngleTests
        {
            public Creation(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void Constructor_ShouldCreateValidRadianAngle()
            {
                // arrange
                double radians = Fixture.Create<double>();

                // act
                var radianAngle = new RadianAngle(radians);

                // assert
                radianAngle.Radians.Should().Be(radians);
            }

            [Fact]
            public void FromAngle_ShouldCreateValidRadianAngle()
            {
                // arrange
                var angle = Fixture.Create<Angle>();

                // act
                var radianAngle = RadianAngle.FromAngle(angle);

                // assert
                radianAngle.Radians.Should().BeApproximately((double)angle.Convert(AngleUnit.Radian).Value, DoublePrecision);
            }
        }
    }
}
