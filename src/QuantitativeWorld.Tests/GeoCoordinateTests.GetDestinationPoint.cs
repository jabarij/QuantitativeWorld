using AutoFixture;
using FluentAssertions;
using QuantitativeWorld.Angular;
using QuantitativeWorld.TestAbstractions;
using System;
using Xunit;

namespace QuantitativeWorld.Tests
{
    partial class GeoCoordinateTests
    {
        public class GetDestinationPoint : GeoCoordinateTests
        {
            public GetDestinationPoint(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void IsEmpty_ShouldThrow()
            {
                // arrange
                var sut = GeoCoordinate.Empty;

                // act
                Action getDistanceTo = () => sut.GetDestinationPoint(Fixture.Create<Length>(), new DegreeAngle(Fixture.CreateInRange(-180d, 180d)));

                // assert
                getDistanceTo.Should().Throw<ArgumentException>().And
                    .Message.Should().Be("Cannot compute destination point from unknown coordinate.");
            }

            private const double OneDegreeOfLatitudeInMeters = 111000d;
            private const double OneDegreeOfLongitudeInMetersAtTheEquator = 111321d;
            [Theory]
            [InlineData(0d, 0d, OneDegreeOfLatitudeInMeters, 0d, 1d, 0d)]
            [InlineData(0d, 0d, OneDegreeOfLatitudeInMeters, Math.PI, -1d, 0d)]
            [InlineData(0d, 0d, OneDegreeOfLatitudeInMeters, -Math.PI, -1d, 0d)]
            [InlineData(0d, 0d, OneDegreeOfLatitudeInMeters, 2 * Math.PI, 1d, 0d)]
            [InlineData(0d, 0d, OneDegreeOfLongitudeInMetersAtTheEquator, 0.5d * Math.PI, 0d, 1d)]
            [InlineData(0d, 0d, OneDegreeOfLongitudeInMetersAtTheEquator, 1.5d * Math.PI, 0d, -1d)]
            [InlineData(0d, 0d, OneDegreeOfLongitudeInMetersAtTheEquator, 2.5d * Math.PI, 0d, 1d)]
            [InlineData(50.233620d, 18.991077d, 170000d, 0.545178d, 51.53388889d, 20.26555556d)]
            [InlineData(50.233620d, 18.991077d, 270000000d, 0.545178d, -34.83527778d, -20.16722222d)]
            [InlineData(50.23362d, 18.991077d, 17000d, 0.54517663057804711d, -25.77861111d, -176.2261111d)]
            public void ShouldReturnProperValue(double lat, double lon, double metres, double bearing, double expectedLatitude, double expectedLongitude)
            {
                // arrange
                var sut = new GeoCoordinate(lat, lon);
                var distance = new Length(metres, LengthUnit.Kilometre);
                var bearingAngle = new RadianAngle(bearing);

                // act
                var result = sut.GetDestinationPoint(distance, bearingAngle);

                // assert
                result.Latitude.Should().BeApproximately(expectedLatitude, 0.005d);
                result.Longitude.Should().BeApproximately(expectedLongitude, 0.005d);
            }
        }
    }
}
