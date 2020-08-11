using AutoFixture;
using FluentAssertions;
using Newtonsoft.Json;
using QuantitativeWorld.TestAbstractions;
using Xunit;

namespace QuantitativeWorld.Text.Json.Tests
{
    public class SpeedJsonConverterTests : TestsBase
    {
        public SpeedJsonConverterTests(TestFixture testFixture) : base(testFixture) { }

        [Theory]
        [InlineData(50d, SpeedJsonSerializationFormat.AsMetresPerSecond, "{\"MetresPerSecond\":13.888888888888889}")]
        [InlineData(50d, SpeedJsonSerializationFormat.AsMetresPerSecondWithUnit, "{\"MetresPerSecond\":13.888888888888889,\"Unit\":{\"Name\":\"kilometre per hour\",\"Abbreviation\":\"km/h\",\"ValueInMetresPerSecond\":0.27777777777777779}}")]
        [InlineData(50d, SpeedJsonSerializationFormat.AsValueWithUnit, "{\"Value\":50.0,\"Unit\":{\"Name\":\"kilometre per hour\",\"Abbreviation\":\"km/h\",\"ValueInMetresPerSecond\":0.27777777777777779}}")]
        public void Serialize_ShouldReturnValidJson(double kilometresPerHour, SpeedJsonSerializationFormat serializationFormat, string expectedJson)
        {
            // arrange
            var speed = new Speed(kilometresPerHour, SpeedUnit.KilometrePerHour);
            var converter = new SpeedJsonConverter(serializationFormat);

            // act
            string actualJson = JsonConvert.SerializeObject(speed, converter);

            // assert
            actualJson.Should().Be(expectedJson);
        }

        [Fact]
        public void DeserializeAsMetresPerSecond_ShouldReturnValidResult()
        {
            // arrange
            string json = @"{
  'metresPerSecond': 123.456
}";
            var converter = new SpeedJsonConverter();

            // act
            var result = JsonConvert.DeserializeObject<Speed>(json, converter);

            // assert
            result.MetresPerSecond.Should().Be(123.456d);
            result.Value.Should().Be(123.456d);
            result.Unit.Should().Be(SpeedUnit.MetrePerSecond);
        }

        [Fact]
        public void DeserializeAsMetresPerSecondWithUnit_ShouldReturnValidResult()
        {
            // arrange
            string json = @"{
  'metresPerSecond': 123.456,
  'unit': {
    'name': 'kilometre per hour',
    'abbreviation': 'km/h',
    'valueInMetresPerSecond': '0.27777777777777779'
  }
}";
            var converter = new SpeedJsonConverter();

            // act
            var result = JsonConvert.DeserializeObject<Speed>(json, converter);

            // assert
            result.MetresPerSecond.Should().Be(123.456d);
            result.Value.Should().BeApproximately(444.4416d, DoublePrecision);
            result.Unit.Should().Be(SpeedUnit.KilometrePerHour);
        }

        [Fact]
        public void DeserializeAsValueWithUnit_ShouldReturnValidResult()
        {
            // arrange
            string json = @"{
  'value': 123.456,
  'unit': {
    'name': 'kilometre per hour',
    'abbreviation': 'km/h',
    'valueInMetresPerSecond': '0.27777777777777779'
  }
}";
            var converter = new SpeedJsonConverter();

            // act
            var result = JsonConvert.DeserializeObject<Speed>(json, converter);

            // assert
            result.MetresPerSecond.Should().BeApproximately(34.293333333333337d, DoublePrecision);
            result.Value.Should().BeApproximately(123.456d, DoublePrecision);
            result.Unit.Should().Be(SpeedUnit.KilometrePerHour);
        }

        [Theory]
        [InlineData(SpeedJsonSerializationFormat.AsMetresPerSecond)]
        [InlineData(SpeedJsonSerializationFormat.AsMetresPerSecondWithUnit)]
        public void SerializeAndDeserializeWithMetresPerSecond_ShouldBeIdempotent(SpeedJsonSerializationFormat serializationFormat)
        {
            // arrange
            var speed = Fixture.Create<Speed>();
            var converter = new SpeedJsonConverter(serializationFormat);

            // act
            string serializedSpeed1 = JsonConvert.SerializeObject(speed, converter);
            var deserializedSpeed1 = JsonConvert.DeserializeObject<Speed>(serializedSpeed1, converter);
            string serializedSpeed2 = JsonConvert.SerializeObject(deserializedSpeed1, converter);
            var deserializedSpeed2 = JsonConvert.DeserializeObject<Speed>(serializedSpeed2, converter);

            // assert
            deserializedSpeed1.Should().Be(speed);
            deserializedSpeed2.Should().Be(speed);

            deserializedSpeed2.Should().Be(deserializedSpeed1);
            serializedSpeed2.Should().Be(serializedSpeed1);
        }

        [Fact]
        public void SerializeAndDeserializeWithoutMetresPerSecond_ShouldBeApproximatelyIdempotent()
        {
            // arrange
            var speed = Fixture.Create<Speed>();
            var converter = new SpeedJsonConverter(SpeedJsonSerializationFormat.AsValueWithUnit);

            // act
            string serializedSpeed1 = JsonConvert.SerializeObject(speed, converter);
            var deserializedSpeed1 = JsonConvert.DeserializeObject<Speed>(serializedSpeed1, converter);
            string serializedSpeed2 = JsonConvert.SerializeObject(deserializedSpeed1, converter);
            var deserializedSpeed2 = JsonConvert.DeserializeObject<Speed>(serializedSpeed2, converter);

            // assert
            deserializedSpeed1.MetresPerSecond.Should().BeApproximately(speed.MetresPerSecond, DoublePrecision);
            deserializedSpeed2.MetresPerSecond.Should().BeApproximately(speed.MetresPerSecond, DoublePrecision);

            deserializedSpeed2.Should().Be(deserializedSpeed1);
            serializedSpeed2.Should().Be(serializedSpeed1);
        }
    }
}
