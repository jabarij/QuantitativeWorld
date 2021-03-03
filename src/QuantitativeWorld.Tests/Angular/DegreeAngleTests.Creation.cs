using AutoFixture;
using FluentAssertions;
using Xunit;

#if DECIMAL
namespace DecimalQuantitativeWorld.Tests.Angular
{
    using DecimalQuantitativeWorld.Angular;
    using DecimalQuantitativeWorld.TestAbstractions;
    using number = System.Decimal;
#else
namespace QuantitativeWorld.Tests.Angular
{
    using QuantitativeWorld.Angular;
    using QuantitativeWorld.TestAbstractions;
    using number = System.Double;
#endif

    partial class DegreeAngleTests
    {
        public class Creation : DegreeAngleTests
        {
            public Creation(TestFixture testFixture)
                : base(testFixture) { }

            [Theory]
            [InlineData(0.5, 0, 0, 0, 0.5, false)]
            [InlineData(-0.5, 0, 0, 0, 0.5, true)]
            [InlineData(1 * 1296000 + 180 * 3600 + 30 * 60 + 30.5, 1, 180, 30, 30.5, false)]
            [InlineData(-(1 * 1296000 + 180 * 3600 + 30 * 60 + 30.5), 1, 180, 30, 30.5, true)]
            public void ConstructorForTotalSeconds_ShouldCreateValidDegreeAngle(number totalSeconds, int expectedCircles, int expectedDegrees, int expectedMinutes, number expectedSeconds, bool expectedIsNegative)
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
            public void FromAngle_ShouldCreateValidDegreeAngle()
            {
                // arrange
                var angle = Fixture.Create<Angle>();

                // act
                var degreeAngle = DegreeAngle.FromAngle(angle);

                // assert
                degreeAngle.TotalSeconds.Should().BeApproximately((number)angle.Convert(AngleUnit.Arcsecond).Value);
            }
        }
    }
}
