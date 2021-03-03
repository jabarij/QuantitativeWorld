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
    using Constants = DoubleConstants;
    using number = System.Double;
#endif

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
                number radians = Fixture.Create<number>();

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
                radianAngle.Radians.Should().BeApproximately((number)angle.Convert(AngleUnit.Radian).Value);
            }
        }
    }
}
