using AutoFixture;
using FluentAssertions;
using Xunit;

#if DECIMAL
namespace DecimalQuantitativeWorld.Tests.Angular
{
    using DecimalQuantitativeWorld.Angular;
    using DecimalQuantitativeWorld.TestAbstractions;
    using Constants = DecimalConstants;
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
        public class Equality : RadianAngleTests
        {
            public Equality(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void DefaultRadianAngle_ShouldBeEqualToZeroRadians()
            {
                // arrange
                var defaultRadianAngle = default(RadianAngle);
                var zeroRadiansRadianAngle = new RadianAngle(Constants.Zero);

                // act
                // assert
                zeroRadiansRadianAngle.Equals(defaultRadianAngle).Should().BeTrue(because: "'new RadianAngle(Constants.Zero)' should be equal 'default(RadianAngle)'");
                defaultRadianAngle.Equals(zeroRadiansRadianAngle).Should().BeTrue(because: "'default(RadianAngle)' should be equal 'new RadianAngle(Constants.Zero)'");
            }

            [Fact]
            public void RadianAngleCreateUsingParamlessConstructor_ShouldBeEqualToZeroRadians()
            {
                // arrange
                var zeroRadiansRadianAngle = new RadianAngle(Constants.Zero);
                var paramlessConstructedRadianAngle = new RadianAngle();

                // act
                // assert
                zeroRadiansRadianAngle.Equals(paramlessConstructedRadianAngle).Should().BeTrue(because: "'new RadianAngle(Constants.Zero)' should be equal 'new RadianAngle()'");
                paramlessConstructedRadianAngle.Equals(zeroRadiansRadianAngle).Should().BeTrue(because: "'new RadianAngle()' should be equal 'new RadianAngle(Constants.Zero)'");
            }

            [Fact]
            public void RadianAnglesOfDifferentUnitsEqualInRadians_ShouldBeEqual()
            {
                // arrange
                var radianAngle1 = Fixture.Create<RadianAngle>();
                var radianAngle2 = new RadianAngle(
                    radians: radianAngle1.Radians);

                // act
                bool equalsResult = radianAngle1.Equals(radianAngle2);

                // assert
                equalsResult.Should().BeTrue(because: $"{radianAngle1.Radians} rad == {radianAngle2.Radians} rad");
            }
        }
    }
}
