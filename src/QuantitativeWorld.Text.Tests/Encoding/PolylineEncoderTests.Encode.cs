using FluentAssertions;
using QuantitativeWorld.TestAbstractions;
using QuantitativeWorld.Text.Encoding;
using System.Collections.Generic;
using System.Globalization;
using Xunit;

namespace QuantitativeWorld.Tests.Encoding
{
    partial class PolylineEncoderTests
    {
        public class Encode : PolylineEncoderTests
        {
            public Encode(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void NullCollection_ShouldReturnNullString()
            {
                // arrange
                var sut = new PolylineEncoder(PolylineEncoding.Polyline5);

                // act
                string actualResult = sut.Encode(null);

                // assert
                actualResult.Should().BeNull();
            }

            [Fact]
            public void EmptyCollection_ShouldReturnEmptyString()
            {
                // arrange
                var sut = new PolylineEncoder(PolylineEncoding.Polyline5);

                // act
                string actualResult = sut.Encode(new GeoCoordinate[0]);

                // assert
                actualResult.Should().BeEmpty();
            }

            [Theory]
            [MemberData(nameof(GetTestData), typeof(Encode), nameof(GetEncodeTestData))]
            public void ShouldReturnProperValue(EncodeTestData testData)
            {
                // arrange
                var sut = new PolylineEncoder(PolylineEncoding.Polyline5);

                // act
                string actualResult = sut.Encode(testData.Coordinates);

                // assert
                actualResult.Should().Be(testData.ExpectedResult);
            }
            private static IEnumerable<EncodeTestData> GetEncodeTestData()
            {
                yield return new EncodeTestData(
                    coordinates: new[]
                    {
                        new GeoCoordinate(38.5d, -120.2d),
                        new GeoCoordinate(40.7d, -120.95d),
                        new GeoCoordinate(43.252d, -126.453d)
                    },
                    expectedResult: "_p~iF~ps|U_ulLnnqC_mqNvxq`@");
                yield return new EncodeTestData(
                    coordinates: new[]
                    {
                        new GeoCoordinate(41.82190d, -87.66104d),
                        new GeoCoordinate(41.84645d, -87.63014d),
                        new GeoCoordinate(41.84364d, -87.66516d)
                    },
                    expectedResult: "{ig~Fnh`vOmxCc`EpPzyE");
            }
            public class EncodeTestData
            {
                public EncodeTestData(
                    IEnumerable<GeoCoordinate> coordinates,
                    string expectedResult)
                {
                    Coordinates = coordinates;
                    ExpectedResult = expectedResult;
                }

                public IEnumerable<GeoCoordinate> Coordinates { get; }
                public string ExpectedResult { get; }
            }
        }
    }
}
