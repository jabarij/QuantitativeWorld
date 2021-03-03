using AutoFixture;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

#if DECIMAL
namespace DecimalQuantitativeWorld.Tests
{
    using DecimalQuantitativeWorld.Angular;
    using DecimalQuantitativeWorld.TestAbstractions;
    using Constants = DecimalConstants;
    using number = Decimal;
#else
namespace QuantitativeWorld.Tests
{
    using QuantitativeWorld.Angular;
    using QuantitativeWorld.TestAbstractions;
    using Constants = DoubleConstants;
    using number = Double;
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
                const number zero = (number)0;
                const number pi = Constants.PI;
                yield return new BearingTestData(zero, zero, 90, zero, zero * pi / 180);
                yield return new BearingTestData(zero, zero, zero, 90, 90 * pi / 180);
                yield return new BearingTestData(zero, zero, -90, zero, 180 * pi / 180);
                yield return new BearingTestData(zero, zero, zero, -90, -90 * pi / 180);
                yield return new BearingTestData(zero, zero, 45, 90, 45 * pi / 180);
                yield return new BearingTestData(zero, zero, -45, 90, 135 * pi / 180);
                yield return new BearingTestData(zero, zero, -45, -90, -135 * pi / 180);
                yield return new BearingTestData(zero, zero, 45, -90, -45 * pi / 180);
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
