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
        [InlineData(5000d, PowerJsonSerializationFormat.AsWatts, "{\"Watts\":5.0}")]
        [InlineData(5000d, PowerJsonSerializationFormat.AsWattsWithUnit, "{\"Watts\":5.0,\"Unit\":{\"Name\":\"milliwatt\",\"Abbreviation\":\"mW\",\"ValueInWatts\":0.001}}")]
        [InlineData(5000d, PowerJsonSerializationFormat.AsValueWithUnit, "{\"Value\":5000.0,\"Unit\":{\"Name\":\"milliwatt\",\"Abbreviation\":\"mW\",\"ValueInWatts\":0.001}}")]
        public void Serialize_ShouldReturnValidJson(double milliwatts, PowerJsonSerializationFormat serializationFormat, string expectedJson)
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
            result.Watts.Should().Be(0.123456d);
            result.Value.Should().Be(0.123456d);
            result.Unit.Should().Be(PowerUnit.Watt);
        }

        [Fact]
        public void DeserializeAsWattsWithUnit_ShouldReturnValidResult()
        {
            // arrange
            string json = @"{
  'watts': 5,
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
            result.Watts.Should().Be(5d);
            result.Value.Should().Be(5000d);
            result.Unit.Should().Be(PowerUnit.Milliwatt);
        }

        [Fact]
        public void DeserializeAsValueWithUnit_ShouldReturnValidResult()
        {
            // arrange
            string json = @"{
  'value': 5000,
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
            result.Watts.Should().Be(5d);
            result.Value.Should().Be(5000d);
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
            deserializedPower1.Watts.Should().BeApproximately(power.Watts, DoublePrecision);
            deserializedPower2.Watts.Should().BeApproximately(power.Watts, DoublePrecision);

            deserializedPower2.Should().Be(deserializedPower1);
            serializedPower2.Should().Be(serializedPower1);
        }
    }
}
