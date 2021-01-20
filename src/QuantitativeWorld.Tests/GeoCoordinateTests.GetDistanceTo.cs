using AutoFixture;
using FluentAssertions;
using QuantitativeWorld.TestAbstractions;
using System;
using Xunit;

namespace QuantitativeWorld.Tests
{
#if DECIMAL
    using number = System.Decimal;
    using Constants = QuantitativeWorld.DecimalConstants;
#else
    using number = System.Double;
    using Constants = QuantitativeWorld.DoubleConstants;
#endif

    partial class GeoCoordinateTests
    {
        public class GetDistanceTo : GeoCoordinateTests
        {
            public GetDistanceTo(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void IsEmpty_ShouldThrow()
            {
                // arrange
                var sut = GeoCoordinate.Empty;

                // act
                Action getDistanceTo = () => sut.GetDistanceTo(Fixture.Create<GeoCoordinate>());

                // assert
                getDistanceTo.Should().Throw<ArgumentException>().And
                    .Message.Should().Be("Cannot compute distance from unknown coordinate.");
            }

            [Fact]
            public void DestinationIsEmpty_ShouldThrow()
            {
                // arrange
                var sut = Fixture.Create<GeoCoordinate>();

                // act
                Action getDistanceTo = () => sut.GetDistanceTo(GeoCoordinate.Empty);

                // assert
                getDistanceTo.Should().Throw<ArgumentException>().And
                    .Message.Should().Be("Cannot compute distance to unknown coordinate.");
            }

            [Theory]
            [InlineData(0d, 0d, 0d, 0d, 0d)]
            [InlineData(0d, -180, 0d, 180, 0d)]
            [InlineData(50.5d, 10.5d, 51.5d, 10.5d, 111195.07d)]
            public void ShouldReturnProperValue(number lat1, number lon1, number lat2, number lon2, number expectedResult)
            {
                // arrange
                var sut = new GeoCoordinate(lat1, lon1);

                // act
                var result = sut.GetDistanceTo(new GeoCoordinate(lat2, lon2));

                // assert
                result.Metres.Should().BeApproximately(expectedResult, Constants.One);
            }
        }
    }
}
