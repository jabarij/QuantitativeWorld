using AutoFixture;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

#if DECIMAL
namespace DecimalQuantitativeWorld.Json.Newtonsoft.Tests.Angular
{
    using DecimalQuantitativeWorld.Angular;
    using DecimalQuantitativeWorld.TestAbstractions;
    using DecimalQuantitativeWorld.Json.Newtonsoft.Angular;
    using number = System.Decimal;
#else
namespace QuantitativeWorld.Json.Newtonsoft.Tests.Angular
{
    using QuantitativeWorld.Angular;
    using QuantitativeWorld.TestAbstractions;
    using QuantitativeWorld.Json.Newtonsoft.Angular;
    using number = System.Double;
#endif

    public class DegreeAngleJsonConverterTests : TestsBase
    {
        public DegreeAngleJsonConverterTests(TestFixture testFixture) : base(testFixture) { }

        [Theory]
        [InlineData(DegreeAngleJsonSerializationFormat.Short, 30.5d, "{\"TotalSeconds\":30.5}")]
        [InlineData(DegreeAngleJsonSerializationFormat.Short, -30.5d, "{\"TotalSeconds\":-30.5}")]
        [InlineData(DegreeAngleJsonSerializationFormat.Extended, 30.5d, "{\"Circles\":0,\"Degrees\":0,\"Minutes\":0,\"Seconds\":30.5,\"IsNegative\":false}")]
        [InlineData(DegreeAngleJsonSerializationFormat.Extended, -30.5d, "{\"Circles\":0,\"Degrees\":0,\"Minutes\":0,\"Seconds\":30.5,\"IsNegative\":true}")]
        [InlineData(DegreeAngleJsonSerializationFormat.Extended, 1 * 1296000d + 180 * 3600d + 30 * 60d + 30.5d, "{\"Circles\":1,\"Degrees\":180,\"Minutes\":30,\"Seconds\":30.5,\"IsNegative\":false}")]
        [InlineData(DegreeAngleJsonSerializationFormat.Extended, -(1 * 1296000d + 180 * 3600d + 30 * 60d + 30.5d), "{\"Circles\":1,\"Degrees\":180,\"Minutes\":30,\"Seconds\":30.5,\"IsNegative\":true}")]
        public void Serialize_ShouldReturnValidJson(DegreeAngleJsonSerializationFormat format, number totalSeconds, string expectedJson)
        {
            // arrange
            var angle = new DegreeAngle(totalSeconds);
            var converter = new DegreeAngleJsonConverter(format);

            // act
            string actualJson = JsonConvert.SerializeObject(angle, converter);

            // assert
            actualJson.Should().Be(expectedJson);
        }

        [Theory]
        //[InlineData("{'totalSeconds': 180}", 180)]
        [InlineData("{'circles':1,'degrees':180,'minutes':30,'seconds':30.5,'isNegative':true}", -(1 * 1296000d + 180 * 3600d + 30 * 60d + 30.5d))]
        public void DeserializeAsTurns_ShouldReturnValidResult(string json, number expectedTotalSeconds)
        {
            // arrange
            var converter = new DegreeAngleJsonConverter();

            // act
            var result = JsonConvert.DeserializeObject<DegreeAngle>(json, converter);

            // assert
            result.TotalSeconds.Should().Be(expectedTotalSeconds);
        }

        [Fact]
        public void SerializeAndDeserialize_ShouldBeIdempotent()
        {
            // arrange
            var angle = Fixture.Create<DegreeAngle>();
            var converter = new DegreeAngleJsonConverter();

            // act
            string serializedDegreeAngle1 = JsonConvert.SerializeObject(angle, converter);
            var deserializedDegreeAngle1 = JsonConvert.DeserializeObject<DegreeAngle>(serializedDegreeAngle1, converter);
            string serializedDegreeAngle2 = JsonConvert.SerializeObject(deserializedDegreeAngle1, converter);
            var deserializedDegreeAngle2 = JsonConvert.DeserializeObject<DegreeAngle>(serializedDegreeAngle2, converter);

            // assert
            deserializedDegreeAngle1.Should().Be(angle);
            deserializedDegreeAngle2.Should().Be(angle);

            deserializedDegreeAngle2.Should().Be(deserializedDegreeAngle1);
            serializedDegreeAngle2.Should().Be(serializedDegreeAngle1);
        }
    }
}
