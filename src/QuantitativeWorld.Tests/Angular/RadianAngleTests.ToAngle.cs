using FluentAssertions;
using QuantitativeWorld.Angular;
using QuantitativeWorld.TestAbstractions;
using Xunit;

namespace QuantitativeWorld.Tests.Angular
{
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
                result.Value.Should().Be(sut.Radians);
                result.Unit.Should().Be(AngleUnit.Radian);
            }
        }
    }
}
