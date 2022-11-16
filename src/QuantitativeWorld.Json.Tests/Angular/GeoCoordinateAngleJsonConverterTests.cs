using AutoFixture;
using FluentAssertions;
using System.Collections.Generic;
using Xunit;

#if DECIMAL
namespace DecimalQuantitativeWorld.Json.Tests.Angular
{
    using DecimalQuantitativeWorld.TestAbstractions;
    using number = System.Decimal;
    using TestsBase = DecimalQuantitativeWorld.Json.Tests.TestsBase;
#else
namespace QuantitativeWorld.Json.Tests.Angular
{
    using QuantitativeWorld.TestAbstractions;
    using number = System.Double;
    using TestsBase = QuantitativeWorld.Json.Tests.TestsBase;
#endif

    public class GeoCoordinateJsonConverterTests : TestsBase
    {
        public GeoCoordinateJsonConverterTests(TestFixture testFixture) : base(testFixture) { }

        [Theory]
        [MemberData(nameof(GetTestData), typeof(GeoCoordinateJsonConverterTests), nameof(GetSerializeTestData))]
        public void Serialize_ShouldReturnValidJson(GeoCoordinate location, string expectedJson)
        {
            // arrange
            var converter = new GeoCoordinateJsonConverter();

            // act
            string actualJson = Serialize(location, converter);

            // assert
            actualJson.Should().Be(expectedJson);
        }
        private static IEnumerable<SerializeTestData> GetSerializeTestData()
        {
            yield return new SerializeTestData(0.5m, -0.5m, "{\"Latitude\":0.5,\"Longitude\":-0.5}");
        }
        private class SerializeTestData : ITestDataProvider
        {
            public SerializeTestData(GeoCoordinate value, string expectedJson)
            {
                Value = value;
                ExpectedJson = expectedJson;
            }
            public SerializeTestData(decimal latitude, decimal longitude, string expectedJson)
                : this(new GeoCoordinate((number)latitude, (number)longitude), expectedJson) { }
            public SerializeTestData(double latitude, double longitude, string expectedJson)
                : this(new GeoCoordinate((number)latitude, (number)longitude), expectedJson) { }

            public GeoCoordinate Value { get; }
            public string ExpectedJson { get; }

            public object[] GetTestParameters() =>
                new[] { (object)Value, ExpectedJson };
        }

        [Theory]
        [MemberData(nameof(GetTestData), typeof(GeoCoordinateJsonConverterTests), nameof(GetDeserializeTestData))]
        public void Deserialize_ShouldReturnValidResult(string json, GeoCoordinate expected)
        {
            // arrange
            var converter = new GeoCoordinateJsonConverter();

            // act
            var result = Deserialize<GeoCoordinate>(json, converter);

            // assert
            result.Should().Be(expected);
        }
        private static IEnumerable<DeserializeTestData> GetDeserializeTestData()
        {
            yield return new DeserializeTestData(@"{""Latitude"": 0.5, ""Longitude"": -0.5}", 0.5m, -0.5m);
        }
        private class DeserializeTestData : ITestDataProvider
        {
            public DeserializeTestData(string json, GeoCoordinate expected)
            {
                Json = json;
                Expected = expected;
            }
            public DeserializeTestData(string json, decimal expectedLatitude, decimal expectedLongitude)
                : this(json, new GeoCoordinate((number)expectedLatitude, (number)expectedLongitude)) { }
            public DeserializeTestData(string json, double expectedLatitude, double expectedLongitude)
                : this(json, new GeoCoordinate((number)expectedLatitude, (number)expectedLongitude)) { }

            public string Json { get; }
            public GeoCoordinate Expected { get; }

            public object[] GetTestParameters() =>
                new[] { (object)Json, Expected };
        }

        [Fact]
        public void SerializeAndDeserialize_ShouldBeIdempotent()
        {
            // arrange
            var location = Fixture.Create<GeoCoordinate>();
            var converter = new GeoCoordinateJsonConverter();

            // act
            string serializedGeoCoordinate1 = Serialize(location, converter);
            var deserializedGeoCoordinate1 = Deserialize<GeoCoordinate>(serializedGeoCoordinate1, converter);
            string serializedGeoCoordinate2 = Serialize(deserializedGeoCoordinate1, converter);
            var deserializedGeoCoordinate2 = Deserialize<GeoCoordinate>(serializedGeoCoordinate2, converter);

            // assert
            deserializedGeoCoordinate1.Should().Be(location);
            deserializedGeoCoordinate2.Should().Be(location);

            deserializedGeoCoordinate2.Should().Be(deserializedGeoCoordinate1);
            serializedGeoCoordinate2.Should().Be(serializedGeoCoordinate1);
        }
    }
}
