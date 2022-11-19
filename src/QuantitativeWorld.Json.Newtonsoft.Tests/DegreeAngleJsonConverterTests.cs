using AutoFixture;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

#if DECIMAL
namespace DecimalQuantitativeWorld.Json.Newtonsoft.Tests
{
    using DecimalQuantitativeWorld.TestAbstractions;
    using number = System.Decimal;
#else
namespace QuantitativeWorld.Json.Newtonsoft.Tests
{
    using QuantitativeWorld.TestAbstractions;
    using number = System.Double;
#endif

    public class TimeJsonConverterTests : TestsBase
    {
        public TimeJsonConverterTests(TestFixture testFixture) : base(testFixture) { }

        [Theory]
        [InlineData(TimeJsonSerializationFormat.Short, 30.5d, "{\"TotalSeconds\":30.5}")]
        [InlineData(TimeJsonSerializationFormat.Short, -30.5d, "{\"TotalSeconds\":-30.5}")]
        [InlineData(TimeJsonSerializationFormat.Extended, 30.5d, "{\"Hours\":0,\"Minutes\":0,\"Seconds\":30.5,\"IsNegative\":false}")]
        [InlineData(TimeJsonSerializationFormat.Extended, -30.5d, "{\"Hours\":0,\"Minutes\":0,\"Seconds\":30.5,\"IsNegative\":true}")]
        [InlineData(TimeJsonSerializationFormat.Extended, 180 * 3600d + 30 * 60d + 30.5d, "{\"Hours\":180,\"Minutes\":30,\"Seconds\":30.5,\"IsNegative\":false}")]
        [InlineData(TimeJsonSerializationFormat.Extended, -(180 * 3600d + 30 * 60d + 30.5d), "{\"Hours\":180,\"Minutes\":30,\"Seconds\":30.5,\"IsNegative\":true}")]
        public void Serialize_ShouldReturnValidJson(TimeJsonSerializationFormat format, number totalSeconds, string expectedJson)
        {
            // arrange
            var angle = new Time(totalSeconds);
            var converter = new TimeJsonConverter(format);

            // act
            string actualJson = JsonConvert.SerializeObject(angle, converter);

            // assert
            actualJson.Should().Be(expectedJson);
        }

        [Theory]
        //[InlineData("{'totalSeconds': 180}", 180)]
        [InlineData("{'hours':180,'minutes':30,'seconds':30.5,'isNegative':true}", -(180 * 3600d + 30 * 60d + 30.5d))]
        public void DeserializeAsTurns_ShouldReturnValidResult(string json, number expectedTotalSeconds)
        {
            // arrange
            var converter = new TimeJsonConverter();

            // act
            var result = JsonConvert.DeserializeObject<Time>(json, converter);

            // assert
            result.TotalSeconds.Should().Be(expectedTotalSeconds);
        }

        [Fact]
        public void SerializeAndDeserialize_ShouldBeIdempotent()
        {
            // arrange
            var angle = Fixture.Create<Time>();
            var converter = new TimeJsonConverter();

            // act
            string serializedTime1 = JsonConvert.SerializeObject(angle, converter);
            var deserializedTime1 = JsonConvert.DeserializeObject<Time>(serializedTime1, converter);
            string serializedTime2 = JsonConvert.SerializeObject(deserializedTime1, converter);
            var deserializedTime2 = JsonConvert.DeserializeObject<Time>(serializedTime2, converter);

            // assert
            deserializedTime1.Should().Be(angle);
            deserializedTime2.Should().Be(angle);

            deserializedTime2.Should().Be(deserializedTime1);
            serializedTime2.Should().Be(serializedTime1);
        }
    }
}
