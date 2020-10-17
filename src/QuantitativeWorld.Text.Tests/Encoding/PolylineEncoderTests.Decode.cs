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
        public class Decode : PolylineEncoderTests
        {
            public Decode(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void NullString_ShouldReturnNullCollection()
            {
                // arrange
                var sut = new PolylineEncoder(PolylineEncoding.Polyline5);

                // act
                var actualResult = sut.Decode(null);

                // assert
                actualResult.Should().BeNull();
            }

            [Fact]
            public void EmptyString_ShouldReturnEmptyCollection()
            {
                // arrange
                var sut = new PolylineEncoder(PolylineEncoding.Polyline5);

                // act
                var actualResult = sut.Decode(string.Empty);

                // assert
                actualResult.Should().BeEmpty();
            }

            [Theory]
            [MemberData(nameof(GetTestData), typeof(Decode), nameof(GetDecodeTestData))]
            public void ShouldReturnProperValue(DecodeTestData testData)
            {
                // arrange
                var sut = new PolylineEncoder(PolylineEncoding.Polyline5);

                // act
                var actualResult = sut.Decode(testData.Polyline);

                // assert
                actualResult.Should().BeEquivalentTo(testData.ExpectedResult);
            }
            private static IEnumerable<DecodeTestData> GetDecodeTestData()
            {
                yield return new DecodeTestData(
                    polyline: "_p~iF~ps|U_ulLnnqC_mqNvxq`@",
                    expectedResult: new[]
                    {
                        new GeoCoordinate(38.5d, -120.2d),
                        new GeoCoordinate(40.7d, -120.95d),
                        new GeoCoordinate(43.252d, -126.453d)
                    });
                yield return new DecodeTestData(
                    polyline: "{ig~Fnh`vOmxCc`EpPzyE",
                    expectedResult: new[]
                    {
                        new GeoCoordinate(41.82190d, -87.66104d),
                        new GeoCoordinate(41.84645d, -87.63014d),
                        new GeoCoordinate(41.84364d, -87.66516d)
                    });
            }
            public class DecodeTestData
            {
                public DecodeTestData(
                    string polyline,
                    IEnumerable<GeoCoordinate> expectedResult)
                {
                    Polyline = polyline;
                    ExpectedResult = expectedResult;
                }

                public IEnumerable<GeoCoordinate> ExpectedResult { get; }
                public string Polyline { get; }
            }
        }
    }
}
