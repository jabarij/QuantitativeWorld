using AutoFixture;
using FluentAssertions;
using Newtonsoft.Json;
using QuantitativeWorld.TestAbstractions;
using Xunit;

namespace QuantitativeWorld.Text.Json.Tests
{
    public class PowerJsonConverterTests : TestsBase
    {
        public PowerJsonConverterTests(TestFixture testFixture) : base(testFixture) { }

        [Theory]
        [InlineData(123.456, PowerJsonSerializationFormat.AsWatts, "{\"Watts\":0.123456}")]
        [InlineData(123.456, PowerJsonSerializationFormat.AsWattsWithUnit, "{\"Watts\":0.123456,\"Unit\":{\"Name\":\"milliwatt\",\"Abbreviation\":\"mW\",\"ValueInWatts\":0.001}}")]
        [InlineData(123.456, PowerJsonSerializationFormat.AsValueWithUnit, "{\"Value\":123.456,\"Unit\":{\"Name\":\"milliwatt\",\"Abbreviation\":\"mW\",\"ValueInWatts\":0.001}}")]
        public void Serialize_ShouldReturnValidJson(decimal milliwatts, PowerJsonSerializationFormat serializationFormat, string expectedJson)
        {
            // arrange
            var power = new Power(milliwatts, PowerUnit.Milliwatt);
            var converter = new PowerJsonConverter(serializationFormat);

            // act
            string actualJson = JsonConvert.SerializeObject(power, converter);

            // assert
            actualJson.Should().Be(expectedJson);
        }

        [Fact]
        public void DeserializeAsWatts_ShouldReturnValidResult()
        {
            // arrange
            string json = @"{
  'watts': 0.123456
}";
            var converter = new PowerJsonConverter();

            // act
            var result = JsonConvert.DeserializeObject<Power>(json, converter);

            // assert
            result.Watts.Should().Be(0.123456m);
            result.Value.Should().Be(0.123456m);
            result.Unit.Should().Be(PowerUnit.Watt);
        }

        [Fact]
        public void DeserializeAsWattsWithUnit_ShouldReturnValidResult()
        {
            // arrange
            string json = @"{
  'watts': 0.123456,
  'unit': {
    'name': 'milliwatt',
    'abbreviation': 'mW',
    'valueInWatts': '0.001'
  }
}";
            var converter = new PowerJsonConverter();

            // act
            var result = JsonConvert.DeserializeObject<Power>(json, converter);

            // assert
            result.Watts.Should().Be(0.123456m);
            result.Value.Should().Be(123.456m);
            result.Unit.Should().Be(PowerUnit.Milliwatt);
        }

        [Fact]
        public void DeserializeAsValueWithUnit_ShouldReturnValidResult()
        {
            // arrange
            string json = @"{
  'value': 123.456,
  'unit': {
    'name': 'milliwatt',
    'abbreviation': 'mW',
    'valueInWatts': '0.001'
  }
}";
            var converter = new PowerJsonConverter();

            // act
            var result = JsonConvert.DeserializeObject<Power>(json, converter);

            // assert
            result.Watts.Should().Be(0.123456m);
            result.Value.Should().Be(123.456m);
            result.Unit.Should().Be(PowerUnit.Milliwatt);
        }

        [Theory]
        [InlineData(PowerJsonSerializationFormat.AsWatts)]
        [InlineData(PowerJsonSerializationFormat.AsWattsWithUnit)]
        public void SerializeAndDeserializeWithWatts_ShouldBeIdempotent(PowerJsonSerializationFormat serializationFormat)
        {
            // arrange
            var power = Fixture.Create<Power>();
            var converter = new PowerJsonConverter(serializationFormat);

            // act
            string serializedPower1 = JsonConvert.SerializeObject(power, converter);
            var deserializedPower1 = JsonConvert.DeserializeObject<Power>(serializedPower1, converter);
            string serializedPower2 = JsonConvert.SerializeObject(deserializedPower1, converter);
            var deserializedPower2 = JsonConvert.DeserializeObject<Power>(serializedPower2, converter);

            // assert
            deserializedPower1.Should().Be(power);
            deserializedPower2.Should().Be(power);

            deserializedPower2.Should().Be(deserializedPower1);
            serializedPower2.Should().Be(serializedPower1);
        }

        [Fact]
        public void SerializeAndDeserializeWithoutWatts_ShouldBeApproximatelyIdempotent()
        {
            // arrange
            var power = Fixture.Create<Power>();
            var converter = new PowerJsonConverter(PowerJsonSerializationFormat.AsValueWithUnit);

            // act
            string serializedPower1 = JsonConvert.SerializeObject(power, converter);
            var deserializedPower1 = JsonConvert.DeserializeObject<Power>(serializedPower1, converter);
            string serializedPower2 = JsonConvert.SerializeObject(deserializedPower1, converter);
            var deserializedPower2 = JsonConvert.DeserializeObject<Power>(serializedPower2, converter);

            // assert
            deserializedPower1.Watts.Should().BeApproximately(power.Watts, DecimalPrecision);
            deserializedPower2.Watts.Should().BeApproximately(power.Watts, DecimalPrecision);

            deserializedPower2.Should().Be(deserializedPower1);
            serializedPower2.Should().Be(serializedPower1);
        }
    }
}
