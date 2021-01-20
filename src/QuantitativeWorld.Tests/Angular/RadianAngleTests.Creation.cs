using AutoFixture;
using FluentAssertions;
using QuantitativeWorld.Angular;
using QuantitativeWorld.TestAbstractions;
using Xunit;

namespace QuantitativeWorld.Tests.Angular
{
#if DECIMAL
    using number = System.Decimal;
    using Constants = QuantitativeWorld.DecimalConstants;
#else
    using number = System.Double;
    using Constants = QuantitativeWorld.DoubleConstants;
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
