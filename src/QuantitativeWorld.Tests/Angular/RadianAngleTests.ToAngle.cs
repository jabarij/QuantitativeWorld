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
        public class ToAngle : RadianAngleTests
        {
            public ToAngle(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void ShouldProduceValidResultInRadians()
            {
                // arrange
                var sut = CreateRadianAngle();

                // act
                var result = sut.ToAngle();

                // assert
                result.Value.Should().BeApproximately((number)sut.Radians);
                result.Unit.Should().Be(AngleUnit.Radian);
            }
        }
    }
}
