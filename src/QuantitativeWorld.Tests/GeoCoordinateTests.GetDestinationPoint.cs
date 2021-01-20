using AutoFixture;
using FluentAssertions;
using QuantitativeWorld.Angular;
using QuantitativeWorld.TestAbstractions;
using System;
using System.Collections.Generic;
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
        public class GetDestinationPoint : GeoCoordinateTests
        {
            public GetDestinationPoint(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void IsEmpty_ShouldThrow()
            {
                // arrange
                var sut = GeoCoordinate.Empty;

                // act
                Action getDistanceTo = () => sut.GetDestinationPoint(Fixture.Create<Length>(), new DegreeAngle(Fixture.CreateInRange((number)(-180), 180)));

                // assert
                getDistanceTo.Should().Throw<ArgumentException>().And
                    .Message.Should().Be("Cannot compute destination point from unknown coordinate.");
            }

            private const number TenDegreeOfLatitudeInMeters = (number)1111960m;
            private const number TenDegreeOfLongitudeInMetersAtTheEquator = (number)1111960m;
            [Theory]
            [MemberData(nameof(GetTestData), typeof(GetDestinationPoint), nameof(GetDestinationPointTestData))]
            public void ShouldReturnProperValue(GeoCoordinate from, Length distance, RadianAngle bearing, GeoCoordinate expected)
            {
                // arrange
                number precision = (number)0.01m;

                // act
                var result = from.GetDestinationPoint(distance, bearing);

                // assert
                result.Latitude.TotalDegrees.Should().BeApproximately(expected.Latitude.TotalDegrees, precision);
                result.Longitude.TotalDegrees.Should().BeApproximately(expected.Longitude.TotalDegrees, precision);
            }
            private static IEnumerable<DestinationPointTestData> GetDestinationPointTestData()
            {
                const number zero = (number)0m;
                const number half = (number)0.5m;
                const number one = (number)1m;
                const number two = (number)2m;
                const number ten = (number)10m;
                yield return new DestinationPointTestData(zero, zero, TenDegreeOfLatitudeInMeters, zero, ten, zero);
                yield return new DestinationPointTestData(zero, zero, TenDegreeOfLatitudeInMeters, Constants.PI, -ten, zero);
                yield return new DestinationPointTestData(zero, zero, TenDegreeOfLatitudeInMeters, -Constants.PI, -ten, zero);
                yield return new DestinationPointTestData(zero, zero, TenDegreeOfLatitudeInMeters, 2 * Constants.PI, ten, zero);
                yield return new DestinationPointTestData(zero, zero, TenDegreeOfLongitudeInMetersAtTheEquator, half * Constants.PI, zero, ten);
                yield return new DestinationPointTestData(zero, zero, TenDegreeOfLongitudeInMetersAtTheEquator, (one + half) * Constants.PI, zero, -ten);
                yield return new DestinationPointTestData(zero, zero, TenDegreeOfLongitudeInMetersAtTheEquator, (two + half) * Constants.PI, zero, ten);
            }
            private class DestinationPointTestData : ITestDataProvider
            {
                public DestinationPointTestData(GeoCoordinate from, Length distance, RadianAngle bearing, GeoCoordinate expected)
                {
                    From = from;
                    Distance = distance;
                    Bearing = bearing;
                    Expected = expected;
                }
                public DestinationPointTestData(
                    decimal fromLatitude,
                    decimal fromLongitude,
                    decimal distanceInMetres,
                    decimal bearingInRadians,
                    decimal expectedLatitude,
                    decimal expectedLongitude)
                    : this(
                          from: new GeoCoordinate((number)fromLatitude, (number)fromLongitude),
                          distance: new Length((number)distanceInMetres),
                          bearing: new RadianAngle((number)bearingInRadians),
                          expected: new GeoCoordinate((number)expectedLatitude, (number)expectedLongitude))
                { }
                public DestinationPointTestData(
                    double fromLatitude,
                    double fromLongitude,
                    double distanceInMetres,
                    double bearingInRadians,
                    double expectedLatitude,
                    double expectedLongitude)
                    : this(
                          from: new GeoCoordinate((number)fromLatitude, (number)fromLongitude),
                          distance: new Length((number)distanceInMetres),
                          bearing: new RadianAngle((number)bearingInRadians),
                          expected: new GeoCoordinate((number)expectedLatitude, (number)expectedLongitude))
                { }

                public GeoCoordinate From { get; }
                public Length Distance { get; }
                public RadianAngle Bearing { get; }
                public GeoCoordinate Expected { get; }

                public object[] GetTestParameters() =>
                    new[] { (object)From, Distance, Bearing, Expected };
            }
        }
    }
}
