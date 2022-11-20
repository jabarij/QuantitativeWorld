using AutoFixture;
using FluentAssertions;
using Xunit;

#if DECIMAL
using DecimalQuantitativeWorld.Json.Angular;

namespace DecimalQuantitativeWorld.Json.Tests.Angular
{
    using DecimalQuantitativeWorld.Angular;
    using DecimalQuantitativeWorld.TestAbstractions;
    using number = System.Decimal;
    using TestsBase = DecimalQuantitativeWorld.Json.Tests.TestsBase;
#else
using QuantitativeWorld.Json.Angular;

namespace QuantitativeWorld.Json.Tests.Angular
{
    using QuantitativeWorld.Angular;
    using QuantitativeWorld.TestAbstractions;
    using number = System.Double;
    using TestsBase = QuantitativeWorld.Json.Tests.TestsBase;
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
            string actualJson = Serialize(angle, converter);

            // assert
            actualJson.Should().Be(expectedJson);
        }

        [Theory]
        [InlineData(@"{""Radians"": 0.5}", 0.5d)]
        public void DeserializeAsTurns_ShouldReturnValidResult(string json, number expectedRadians)
        {
            // arrange
            var converter = new RadianAngleJsonConverter();

            // act
            var result = Deserialize<RadianAngle>(json, converter);

            // assert
            result.Radians.Should().Be(expectedRadians);
        }

        [Fact]
        public void SerializeAndDeserialize_ShouldBeIdempotent()
        {
            // arrange
            var angle = Fixture.Create<RadianAngle>();
            var converter = new RadianAngleJsonConverter();

            // act
            string serializedRadianAngle1 = Serialize(angle, converter);
            var deserializedRadianAngle1 = Deserialize<RadianAngle>(serializedRadianAngle1, converter);
            string serializedRadianAngle2 = Serialize(deserializedRadianAngle1, converter);
            var deserializedRadianAngle2 = Deserialize<RadianAngle>(serializedRadianAngle2, converter);

            // assert
            deserializedRadianAngle1.Should().Be(angle);
            deserializedRadianAngle2.Should().Be(angle);

            deserializedRadianAngle2.Should().Be(deserializedRadianAngle1);
            serializedRadianAngle2.Should().Be(serializedRadianAngle1);
        }
    }
}
