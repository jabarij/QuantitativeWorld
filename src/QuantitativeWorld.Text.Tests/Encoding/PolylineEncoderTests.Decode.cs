using FluentAssertions;
using System.Collections.Generic;
using Xunit;

#if DECIMAL
namespace DecimalQuantitativeWorld.Tests.Encoding
{
    using DecimalQuantitativeWorld.TestAbstractions;
    using DecimalQuantitativeWorld.Text.Encoding;
    using number = System.Decimal;
#else
namespace QuantitativeWorld.Tests.Encoding
{
    using QuantitativeWorld.TestAbstractions;
    using QuantitativeWorld.Text.Encoding;
    using number = System.Double;
#endif

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
                        new GeoCoordinate((number)38.5d, -(number)120.2d),
                        new GeoCoordinate((number)40.7d, -(number)120.95d),
                        new GeoCoordinate((number)43.252d, -(number)126.453d)
                    });
                yield return new DecodeTestData(
                    polyline: "{ig~Fnh`vOmxCc`EpPzyE",
                    expectedResult: new[]
                    {
                        new GeoCoordinate((number)41.82190d, -(number)87.66104d),
                        new GeoCoordinate((number)41.84645d, -(number)87.63014d),
                        new GeoCoordinate((number)41.84364d, -(number)87.66516d)
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
