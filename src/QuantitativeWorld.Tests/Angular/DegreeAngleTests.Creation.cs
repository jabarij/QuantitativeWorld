using AutoFixture;
using FluentAssertions;
using QuantitativeWorld.Angular;
using QuantitativeWorld.TestAbstractions;
using Xunit;

namespace QuantitativeWorld.Tests.Angular
{
    partial class DegreeAngleTests
    {
        public class Creation : DegreeAngleTests
        {
            public Creation(TestFixture testFixture)
                : base(testFixture) { }

            [Theory]
            [InlineData(0.5d, 0, 0, 0, 0.5d, false)]
            [InlineData(-0.5d, 0, 0, 0, 0.5d, true)]
            [InlineData(1 * 1296000d + 180 * 3600d + 30 * 60d + 30.5d, 1, 180, 30, 30.5d, false)]
            [InlineData(-(1 * 1296000d + 180 * 3600d + 30 * 60d + 30.5d), 1, 180, 30, 30.5d, true)]
            public void ConstructorForTotalSeconds_ShouldCreateValidDegreeAngle(float totalSeconds, int expectedCircles, int expectedDegrees, int expectedMinutes, double expectedSeconds, bool expectedIsNegative)
            {
                // arrange
                // act
                var degreeAngle = new DegreeAngle(totalSeconds);

                // assert
                degreeAngle.TotalSeconds.Should().Be(totalSeconds);
                degreeAngle.Circles.Should().Be(expectedCircles);
                degreeAngle.Degrees.Should().Be(expectedDegrees);
                degreeAngle.Minutes.Should().Be(expectedMinutes);
                degreeAngle.Seconds.Should().Be(expectedSeconds);
                degreeAngle.IsNegative.Should().Be(expectedIsNegative);
            }

            [Fact]
            public void FromAngle_ShouldCreateValidRadianAngle()
            {
                // arrange
                var angle = Fixture.Create<Angle>();

                // act
                var radianAngle = DegreeAngle.FromAngle(angle);

                // assert
                radianAngle.TotalSeconds.Should().BeApproximately((double)angle.Convert(AngleUnit.Arcsecond).Value, DoublePrecision);
            }
        }
    }
}
