using AutoFixture;
using FluentAssertions;
using Newtonsoft.Json;
using QuantitativeWorld.Angular;
using QuantitativeWorld.TestAbstractions;
using QuantitativeWorld.Text.Json.Angular;
using Xunit;

namespace QuantitativeWorld.Text.Json.Tests.Angular
{
    public class GeoCoordinateJsonConverterTests : TestsBase
    {
        public GeoCoordinateJsonConverterTests(TestFixture testFixture) : base(testFixture) { }

        [Theory]
        [InlineData(0.5d, -0.5d, "{\"Latitude\":0.5,\"Longitude\":-0.5}")]
        public void Serialize_ShouldReturnValidJson(double latitude, double longitude, string expectedJson)
        {
            // arrange
            var location = new GeoCoordinate(latitude, longitude);
            var converter = new GeoCoordinateJsonConverter();

            // act
            string actualJson = JsonConvert.SerializeObject(location, converter);

            // assert
            actualJson.Should().Be(expectedJson);
        }

        [Theory]
        [InlineData("{'latitude': 0.5, 'longitude': -0.5}", 0.5d, -0.5d)]
        public void DeserializeAsTurns_ShouldReturnValidResult(string json, double expectedLatitude, double expectedLongitude)
        {
            // arrange
            var converter = new GeoCoordinateJsonConverter();

            // act
            var result = JsonConvert.DeserializeObject<GeoCoordinate>(json, converter);

            // assert
            result.Latitude.Should().Be(expectedLatitude);
            result.Longitude.Should().Be(expectedLongitude);
        }

        [Fact]
        public void SerializeAndDeserialize_ShouldBeIdempotent()
        {
            // arrange
            var location = Fixture.Create<GeoCoordinate>();
            var converter = new GeoCoordinateJsonConverter();

            // act
            string serializedGeoCoordinate1 = JsonConvert.SerializeObject(location, converter);
            var deserializedGeoCoordinate1 = JsonConvert.DeserializeObject<GeoCoordinate>(serializedGeoCoordinate1, converter);
            string serializedGeoCoordinate2 = JsonConvert.SerializeObject(deserializedGeoCoordinate1, converter);
            var deserializedGeoCoordinate2 = JsonConvert.DeserializeObject<GeoCoordinate>(serializedGeoCoordinate2, converter);

            // assert
            deserializedGeoCoordinate1.Should().Be(location);
            deserializedGeoCoordinate2.Should().Be(location);

            deserializedGeoCoordinate2.Should().Be(deserializedGeoCoordinate1);
            serializedGeoCoordinate2.Should().Be(serializedGeoCoordinate1);
        }
    }
}
