using FluentAssertions;
using QuantitativeWorld.Angular;
using QuantitativeWorld.TestAbstractions;
using System;
using System.Collections.Generic;
using Xunit;

namespace QuantitativeWorld.Tests
{
#if DECIMAL
    using Constants = QuantitativeWorld.DecimalConstants;
    using number = System.Decimal;
#else
    using number = System.Double;
    using Constants = QuantitativeWorld.DoubleConstants;
#endif

    partial class GeoCoordinateTests
    {
        public class ParameterlessConstructor : GeoCoordinateTests
        {
            public ParameterlessConstructor(TestFixture testFixture) : base(testFixture) { }

#if !DECIMAL
            [Fact]
            public void ShouldInitializeCoordinatesAsDefaultDegreeAngles()
            {
                // arrange
                // act
                var sut = new GeoCoordinate();

                // assert
                sut.Latitude.Should().Be(default(DegreeAngle));
                sut.Longitude.Should().Be(default(DegreeAngle));
            }
#endif
        }

        public class Constructor_Doubles : GeoCoordinateTests
        {
            public Constructor_Doubles(TestFixture testFixture) : base(testFixture) { }

            [Theory]
            [InlineData(91)]
            [InlineData(-91)]
            public void LatitudeOutOfRange_ShouldThrow(number latitude)
            {
                // arrange
                Action create = () => new GeoCoordinate(latitude, Constants.Zero);

                // act
                // assert
                var exception = create.Should().Throw<ArgumentOutOfRangeException>().And;
                exception.ParamName.Should().Be("latitude");
                exception.ActualValue.Should().Be(new DegreeAngle(latitude * 3600));
            }

            [Theory]
            [InlineData(181)]
            [InlineData(-181)]
            public void LongitudeOutOfRange_ShouldThrow(number longitude)
            {
                // arrange
                Action create = () => new GeoCoordinate(Constants.Zero, longitude);

                // act
                // assert
                var exception = create.Should().Throw<ArgumentOutOfRangeException>().And;
                exception.ParamName.Should().Be("longitude");
                exception.ActualValue.Should().Be(new DegreeAngle(longitude * 3600));
            }

            [Fact]
            public void ShouldCreateWithGivenValues()
            {
                // arrange
                number latitude = Fixture.CreateInRange(GeoCoordinate.MinLatitude.TotalDegrees, GeoCoordinate.MaxLatitude.TotalDegrees);
                number longitude = Fixture.CreateInRange(GeoCoordinate.MinLongitude.TotalDegrees, GeoCoordinate.MaxLongitude.TotalDegrees);

                // act
                var sut = new GeoCoordinate(latitude, longitude);

                // assert
                sut.Latitude.TotalDegrees.Should().BeApproximately(latitude);
                sut.Longitude.TotalDegrees.Should().BeApproximately(longitude);
            }
        }

        public class Constructor_DegreeAngles : GeoCoordinateTests
        {
            public Constructor_DegreeAngles(TestFixture testFixture) : base(testFixture) { }

            [Theory]
            [InlineData(91d * 3600d)]
            [InlineData(-91d * 3600d)]
            public void LatitudeOutOfRange_ShouldThrow(number latitudeTotalSeconds)
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
            public void LongitudeOutOfRange_ShouldThrow(number longitudeTotalSeconds)
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

            [Theory]
            [MemberData(nameof(GetTestData), typeof(Constructor_DegreeAngles), nameof(GetCreateWithGivenValuesTestData))]
            public void ShouldCreateWithGivenValues(number latitudeDegrees, number longitudeDegrees)
            {
                // arrange
                var latitude = new DegreeAngle(latitudeDegrees);
                var longitude = new DegreeAngle(longitudeDegrees);

                // act
                var sut = new GeoCoordinate(latitude, longitude);

                // assert
                sut.Latitude.Should().Be(latitude);
                sut.Longitude.Should().Be(longitude);
            }
            private static IEnumerable<ITestDataProvider> GetCreateWithGivenValuesTestData()
            {
                yield return new CreateWithGivenValuesTestData(0d, 0d);
                yield return new CreateWithGivenValuesTestData(-90d, 0d);
                yield return new CreateWithGivenValuesTestData(-45d, 0d);
                yield return new CreateWithGivenValuesTestData(45d, 0d);
                yield return new CreateWithGivenValuesTestData(90d, 0d);
                yield return new CreateWithGivenValuesTestData(0d, -180d);
                yield return new CreateWithGivenValuesTestData(0d, -90d);
                yield return new CreateWithGivenValuesTestData(0d, 90d);
                yield return new CreateWithGivenValuesTestData(0d, 180d);
            }
            private class CreateWithGivenValuesTestData : ITestDataProvider
            {
                public CreateWithGivenValuesTestData(decimal latitude, decimal longitude)
                {
                    Latitude = (number)latitude;
                    Longitude = (number)longitude;
                }
                public CreateWithGivenValuesTestData(double latitude, double longitude)
                {
                    Latitude = (number)latitude;
                    Longitude = (number)longitude;
                }

                public number Latitude { get; set; }
                public number Longitude { get; set; }

                public object[] GetTestParameters() =>
                    new[] { (object)Latitude, Longitude };
            }
        }

        public class Constructor_RadianAngles : GeoCoordinateTests
        {
            public Constructor_RadianAngles(TestFixture testFixture) : base(testFixture) { }

            [Theory]
            [InlineData(91d)]
            [InlineData(-91d)]
            public void LatitudeOutOfRange_ShouldThrow(number latitudeRadians)
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
            public void LongitudeOutOfRange_ShouldThrow(number longitudeRadians)
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

            [Theory]
            [MemberData(nameof(GetTestData), typeof(Constructor_RadianAngles), nameof(GetCreateWithGivenValuesTestData))]
            public void ShouldCreateWithGivenValues(number latitudeRadians, number longitudeRadians)
            {
                // arrange
                var latitude = new RadianAngle(latitudeRadians);
                var longitude = new RadianAngle(longitudeRadians);

                // act
                var sut = new GeoCoordinate(latitude, longitude);

                // assert
                sut.LatitudeRadians.Should().Be(latitude);
                sut.LongitudeRadians.Should().Be(longitude);
            }
            private static IEnumerable<ITestDataProvider> GetCreateWithGivenValuesTestData()
            {
                const decimal pi = DecimalConstants.PI;
                yield return new CreateWithGivenValuesTestData(0m, 0m);
                yield return new CreateWithGivenValuesTestData(-pi / 2m, 0m);
                yield return new CreateWithGivenValuesTestData(-pi / 4m, 0m);
                yield return new CreateWithGivenValuesTestData(pi / 4m, 0m);
                yield return new CreateWithGivenValuesTestData(pi / 2m, 0m);
                yield return new CreateWithGivenValuesTestData(0m, -0.99m * pi);
                yield return new CreateWithGivenValuesTestData(0m, -0.5m * pi);
                yield return new CreateWithGivenValuesTestData(0m, 0.5m * pi);
                yield return new CreateWithGivenValuesTestData(0m, 0.99m * pi);
            }
            private class CreateWithGivenValuesTestData : ITestDataProvider
            {
                public CreateWithGivenValuesTestData(decimal latitude, decimal longitude)
                {
                    Latitude = (number)latitude;
                    Longitude = (number)longitude;
                }
                public CreateWithGivenValuesTestData(double latitude, double longitude)
                {
                    Latitude = (number)latitude;
                    Longitude = (number)longitude;
                }

                public number Latitude { get; set; }
                public number Longitude { get; set; }

                public object[] GetTestParameters() =>
                    new[] { (object)Latitude, Longitude };
            }
        }
    }
}
