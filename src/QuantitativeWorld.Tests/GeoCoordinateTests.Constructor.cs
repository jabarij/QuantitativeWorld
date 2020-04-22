using FluentAssertions;
using QuantitativeWorld.Angular;
using QuantitativeWorld.TestAbstractions;
using System;
using Xunit;

namespace QuantitativeWorld.Tests
{
    partial class GeoCoordinateTests
    {
        public class ParameterlessConstructor : GeoCoordinateTests
        {
            public ParameterlessConstructor(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void ShouldInitializeCoordinatesAsNaN()
            {
                // arrange
                // act
                var sut = new GeoCoordinate();

                // assert
                sut.Latitude.Should().Be(double.NaN);
                sut.Longitude.Should().Be(double.NaN);
            }
        }

        public class Constructor_Doubles : GeoCoordinateTests
        {
            public Constructor_Doubles(TestFixture testFixture) : base(testFixture) { }

            [Theory]
            [InlineData(91d)]
            [InlineData(-91d)]
            public void LatitudeOutOfRange_ShouldThrow(double latitude)
            {
                // arrange
                Action create = () => new GeoCoordinate(latitude, 0d);

                // act
                // assert
                var exception = create.Should().Throw<ArgumentOutOfRangeException>().And;
                exception.ParamName.Should().Be("latitude");
                exception.ActualValue.Should().Be(latitude);
            }

            [Theory]
            [InlineData(181d)]
            [InlineData(-181d)]
            public void LongitudeOutOfRange_ShouldThrow(double longitude)
            {
                // arrange
                Action create = () => new GeoCoordinate(0d, longitude);

                // act
                // assert
                var exception = create.Should().Throw<ArgumentOutOfRangeException>().And;
                exception.ParamName.Should().Be("longitude");
                exception.ActualValue.Should().Be(longitude);
            }

            [Fact]
            public void ShouldCreateWithGivenValues()
            {
                // arrange
                double latitude = Fixture.CreateInRange(GeoCoordinate.MinLatitude, GeoCoordinate.MaxLatitude);
                double longitude = Fixture.CreateInRange(GeoCoordinate.MinLongitude, GeoCoordinate.MaxLongitude);

                // act
                var sut = new GeoCoordinate(latitude, longitude);

                // assert
                sut.Latitude.Should().BeApproximately(latitude, DoublePrecision);
                sut.Longitude.Should().BeApproximately(longitude, DoublePrecision);
            }
        }

        public class Constructor_DegreeAngles : GeoCoordinateTests
        {
            public Constructor_DegreeAngles(TestFixture testFixture) : base(testFixture) { }

            [Theory]
            [InlineData(91d * 3600d)]
            [InlineData(-91d * 3600d)]
            public void LatitudeOutOfRange_ShouldThrow(double latitudeTotalSeconds)
            {
                // arrange
                var latitude = new DegreeAngle(latitudeTotalSeconds);

                // act
                Action create = () => new GeoCoordinate(latitude, DegreeAngle.Zero);

                // assert
                var exception = create.Should().Throw<ArgumentOutOfRangeException>().And;
                exception.ParamName.Should().Be("latitude");
                exception.ActualValue.Should().Be(latitude);
            }

            [Theory]
            [InlineData(181d * 3600d)]
            [InlineData(-181d * 3600d)]
            public void LongitudeOutOfRange_ShouldThrow(double longitudeTotalSeconds)
            {
                // arrange
                var longitude = new DegreeAngle(longitudeTotalSeconds);

                // act
                Action create = () => new GeoCoordinate(DegreeAngle.Zero, longitude);

                // assert
                var exception = create.Should().Throw<ArgumentOutOfRangeException>().And;
                exception.ParamName.Should().Be("longitude");
                exception.ActualValue.Should().Be(longitude);
            }

            [Fact]
            public void ShouldCreateWithGivenValues()
            {
                // arrange
                var latitude = new DegreeAngle(Fixture.CreateInRange(GeoCoordinate.MinLatitude, GeoCoordinate.MaxLatitude));
                var longitude = new DegreeAngle(Fixture.CreateInRange(GeoCoordinate.MinLongitude, GeoCoordinate.MaxLongitude));

                // act
                var sut = new GeoCoordinate(latitude, longitude);

                // assert
                sut.LatitudeDegrees.Should().Be(latitude);
                sut.LongitudeDegrees.Should().Be(longitude);
            }
        }

        public class Constructor_RadianAngles : GeoCoordinateTests
        {
            public Constructor_RadianAngles(TestFixture testFixture) : base(testFixture) { }

            [Theory]
            [InlineData(91d)]
            [InlineData(-91d)]
            public void LatitudeOutOfRange_ShouldThrow(double latitudeRadians)
            {
                // arrange
                var latitude = new RadianAngle(latitudeRadians);

                // act
                Action create = () => new GeoCoordinate(latitude, RadianAngle.Zero);

                // assert
                var exception = create.Should().Throw<ArgumentOutOfRangeException>().And;
                exception.ParamName.Should().Be("latitude");
                exception.ActualValue.Should().Be(latitude);
            }

            [Theory]
            [InlineData(181d)]
            [InlineData(-181d)]
            public void LongitudeOutOfRange_ShouldThrow(double longitudeRadians)
            {
                // arrange
                var longitude = new RadianAngle(longitudeRadians);

                // act
                Action create = () => new GeoCoordinate(RadianAngle.Zero, longitude);

                // assert
                var exception = create.Should().Throw<ArgumentOutOfRangeException>().And;
                exception.ParamName.Should().Be("longitude");
                exception.ActualValue.Should().Be(longitude);
            }

            [Fact]
            public void ShouldCreateWithGivenValues()
            {
                // arrange
                var latitude = new RadianAngle(Fixture.CreateInRange(GeoCoordinate.MinLatitudeRadians.Radians, GeoCoordinate.MaxLatitudeRadians.Radians));
                var longitude = new RadianAngle(Fixture.CreateInRange(GeoCoordinate.MinLongitudeRadians.Radians, GeoCoordinate.MaxLongitudeRadians.Radians));

                // act
                var sut = new GeoCoordinate(latitude, longitude);

                // assert
                sut.LatitudeRadians.Should().Be(latitude);
                sut.LongitudeRadians.Should().Be(longitude);
            }
        }
    }
}
