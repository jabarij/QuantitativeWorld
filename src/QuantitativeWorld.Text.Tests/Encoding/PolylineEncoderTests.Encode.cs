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
                        new GeoCoordinate((number)38.5d, -(number)120.2d),
                        new GeoCoordinate((number)40.7d, -(number)120.95d),
                        new GeoCoordinate((number)43.252d, -(number)126.453d)
                    },
                    expectedResult: "_p~iF~ps|U_ulLnnqC_mqNvxq`@");
                yield return new EncodeTestData(
                    coordinates: new[]
                    {
                        new GeoCoordinate((number)41.82190d, -(number)87.66104d),
                        new GeoCoordinate((number)41.84645d, -(number)87.63014d),
                        new GeoCoordinate((number)41.84364d, -(number)87.66516d)
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
