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
