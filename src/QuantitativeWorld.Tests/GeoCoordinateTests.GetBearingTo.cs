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
            [MemberData(nameof(GetTestData), typeof(GetBearingTo), nameof(GetBearingTestData))]
            
            public void ShouldReturnProperValue(GeoCoordinate from, GeoCoordinate to, RadianAngle expectedResult)
            {
                // arrange
                // act
                var result = from.GetBearingTo(to);

                // assert
                result.Radians.Should().BeApproximately(expectedResult.Radians);
            }
            private static IEnumerable<ITestDataProvider> GetBearingTestData()
            {
                const decimal pi = DecimalConstants.PI;
                yield return new BearingTestData(0m, 0m, 90, 0m, 0m * pi / 180);
                yield return new BearingTestData(0m, 0m, 0m, 90, 90 * pi / 180);
                yield return new BearingTestData(0m, 0m, -90, 0m, 180 * pi / 180);
                yield return new BearingTestData(0m, 0m, 0m, -90, -90 * pi / 180);
                yield return new BearingTestData(0m, 0m, 45, 90, 45 * pi / 180);
                yield return new BearingTestData(0m, 0m, -45, 90, 135 * pi / 180);
                yield return new BearingTestData(0m, 0m, -45, -90, -135 * pi / 180);
                yield return new BearingTestData(0m, 0m, 45, -90, -45 * pi / 180);
                yield return new BearingTestData(50.233620m, 18.991077m, 52.256371m, 21.011800m, 0.54517783m);
            }
            class BearingTestData : ITestDataProvider
            {
                public BearingTestData(GeoCoordinate from, GeoCoordinate to, RadianAngle expected)
                {
                    From = from;
                    To = to;
                    Expected = expected;
                }
                public BearingTestData(decimal fromLatitude, decimal fromLongitude, decimal toLatitude, decimal toLongitude, decimal expectedRadians)
                    : this(new GeoCoordinate((number)fromLatitude, (number)fromLongitude), new GeoCoordinate((number)toLatitude, (number)toLongitude), new RadianAngle((number)expectedRadians)) { }
                public BearingTestData(double fromLatitude, double fromLongitude, double toLatitude, double toLongitude, double expectedRadians)
                    : this(new GeoCoordinate((number)fromLatitude, (number)fromLongitude), new GeoCoordinate((number)toLatitude, (number)toLongitude), new RadianAngle((number)expectedRadians)) { }

                public GeoCoordinate From { get; set; }
                public GeoCoordinate To { get; set; }
                public RadianAngle Expected { get; set; }

                public object[] GetTestParameters() =>
                    new[] { (object)From, To, Expected };
            }
        }
    }
}
