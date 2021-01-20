using AutoFixture;
using FluentAssertions;
using Newtonsoft.Json;
using QuantitativeWorld.Angular;
using QuantitativeWorld.TestAbstractions;
using QuantitativeWorld.Text.Json.Angular;
using Xunit;

namespace QuantitativeWorld.Text.Json.Tests.Angular
{
#if DECIMAL
    using number = System.Decimal;
    using Constants = QuantitativeWorld.DecimalConstants;
#else
    using number = System.Double;
    using Constants = QuantitativeWorld.DoubleConstants;
#endif

    public class RadianAngleJsonConverterTests : TestsBase
    {
        public RadianAngleJsonConverterTests(TestFixture testFixture) : base(testFixture) { }

        [Theory]
        [InlineData(0.5d, "{\"Radians\":0.5}")]
        public void Serialize_ShouldReturnValidJson(number radians, string expectedJson)
        {
            // arrange
            var angle = new RadianAngle(radians);
            var converter = new GeoCoordinateJsonConverter();

            // act
            string actualJson = JsonConvert.SerializeObject(angle, converter);

            // assert
            actualJson.Should().Be(expectedJson);
        }

        [Theory]
        [InlineData("{'radians': 0.5}", 0.5d)]
        public void DeserializeAsTurns_ShouldReturnValidResult(string json, number expectedRadians)
        {
            // arrange
            var converter = new GeoCoordinateJsonConverter();

            // act
            var result = JsonConvert.DeserializeObject<RadianAngle>(json, converter);

            // assert
            result.Radians.Should().Be(expectedRadians);
        }

        [Fact]
        public void SerializeAndDeserialize_ShouldBeIdempotent()
        {
            // arrange
            var angle = Fixture.Create<RadianAngle>();
            var converter = new GeoCoordinateJsonConverter();

            // act
            string serializedRadianAngle1 = JsonConvert.SerializeObject(angle, converter);
            var deserializedRadianAngle1 = JsonConvert.DeserializeObject<RadianAngle>(serializedRadianAngle1, converter);
            string serializedRadianAngle2 = JsonConvert.SerializeObject(deserializedRadianAngle1, converter);
            var deserializedRadianAngle2 = JsonConvert.DeserializeObject<RadianAngle>(serializedRadianAngle2, converter);

            // assert
            deserializedRadianAngle1.Should().Be(angle);
            deserializedRadianAngle2.Should().Be(angle);

            deserializedRadianAngle2.Should().Be(deserializedRadianAngle1);
            serializedRadianAngle2.Should().Be(serializedRadianAngle1);
        }
    }
}
