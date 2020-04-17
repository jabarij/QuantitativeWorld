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
                decimal radians = Fixture.Create<decimal>();

                // act
                var radianRadianAngle = new RadianAngle(radians);

                // assert
                radianRadianAngle.Radians.Should().Be(radians);
            }
        }
    }
}
