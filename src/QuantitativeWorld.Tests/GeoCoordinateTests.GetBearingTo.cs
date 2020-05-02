using AutoFixture;
using FluentAssertions;
using QuantitativeWorld.TestAbstractions;
using System;
using Xunit;

namespace QuantitativeWorld.Tests
{
    partial class GeoCoordinateTests
    {
        public class GetBearingTo : GeoCoordinateTests
        {
            public GetBearingTo(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void IsEmpty_ShouldThrow()
            {
                // arrange
                var sut = GeoCoordinate.Empty;

                // act
                Action getDistanceTo = () => sut.GetBearingTo(Fixture.Create<GeoCoordinate>());

                // assert
                getDistanceTo.Should().Throw<ArgumentException>().And
                    .Message.Should().Be("Cannot compute bearing from unknown coordinate.");
            }

            [Fact]
            public void DestinationIsEmpty_ShouldThrow()
            {
                // arrange
                var sut = Fixture.Create<GeoCoordinate>();

                // act
                Action getDistanceTo = () => sut.GetBearingTo(GeoCoordinate.Empty);

                // assert
                getDistanceTo.Should().Throw<ArgumentException>().And
                    .Message.Should().Be("Cannot compute bearing to unknown coordinate.");
            }

            [Theory]
            [InlineData(0d, 0d, 90d, 0d, 0d * Math.PI / 180d)]
            [InlineData(0d, 0d, 0d, 90d, 90d * Math.PI / 180d)]
            [InlineData(0d, 0d, -90d, 0d, 180d * Math.PI / 180d)]
            [InlineData(0d, 0d, 0d, -90d, -90d * Math.PI / 180d)]
            [InlineData(0d, 0d, 45d, 90d, 45d * Math.PI / 180d)]
            [InlineData(0d, 0d, -45d, 90d, 135d * Math.PI / 180d)]
            [InlineData(0d, 0d, -45d, -90d, -135d * Math.PI / 180d)]
            [InlineData(0d, 0d, 45d, -90d, -45d * Math.PI / 180d)]
            [InlineData(50.233620d, 18.991077d, 52.256371d, 21.011800d, 0.54517783d)]
            public void ShouldReturnProperValue(double lat1, double lon1, double lat2, double lon2, double expectedResult)
            {
                // arrange
                var sut = new GeoCoordinate(lat1, lon1);

                // act
                var result = sut.GetBearingTo(new GeoCoordinate(lat2, lon2));

                // assert
                result.Radians.Should().BeApproximately(expectedResult, 0.005d);
            }
        }
    }
}
