using AutoFixture;
using FluentAssertions;
using Newtonsoft.Json;
using QuantitativeWorld.TestAbstractions;
using Xunit;

namespace QuantitativeWorld.Text.Json.Tests
{
    public class EnergyJsonConverterTests : TestsBase
    {
        public EnergyJsonConverterTests(TestFixture testFixture) : base(testFixture) { }

        [Theory]
        [InlineData(5d, EnergyJsonSerializationFormat.AsJoules, "{\"Joules\":5000.0}")]
        [InlineData(5d, EnergyJsonSerializationFormat.AsJoulesWithUnit, "{\"Joules\":5000.0,\"Unit\":{\"Name\":\"kilojoule\",\"Abbreviation\":\"kJ\",\"ValueInJoules\":1000.0}}")]
        [InlineData(5d, EnergyJsonSerializationFormat.AsValueWithUnit, "{\"Value\":5.0,\"Unit\":{\"Name\":\"kilojoule\",\"Abbreviation\":\"kJ\",\"ValueInJoules\":1000.0}}")]
        public void Serialize_ShouldReturnValidJson(double kilojoules, EnergyJsonSerializationFormat serializationFormat, string expectedJson)
        {
            // arrange
            var energy = new Energy(kilojoules, EnergyUnit.Kilojoule);
            var converter = new EnergyJsonConverter(serializationFormat);

            // act
            string actualJson = JsonConvert.SerializeObject(energy, converter);

            // assert
            actualJson.Should().Be(expectedJson);
        }

        [Fact]
        public void DeserializeAsJoules_ShouldReturnValidResult()
        {
            // arrange
            string json = @"{
  'joules': 0.123456
}";
            var converter = new EnergyJsonConverter();

            // act
            var result = JsonConvert.DeserializeObject<Energy>(json, converter);

            // assert
            result.Joules.Should().Be(0.123456d);
            result.Value.Should().Be(0.123456d);
            result.Unit.Should().Be(EnergyUnit.Joule);
        }

        [Fact]
        public void DeserializeAsJoulesWithUnit_ShouldReturnValidResult()
        {
            // arrange
            string json = @"{
  'joules': 5000,
  'unit': {
    'name': 'kilojoule',
    'abbreviation': 'kJ',
    'valueInJoules': 1000
  }
}";
            var converter = new EnergyJsonConverter();

            // act
            var result = JsonConvert.DeserializeObject<Energy>(json, converter);

            // assert
            result.Joules.Should().Be(5000d);
            result.Value.Should().Be(5d);
            result.Unit.Should().Be(EnergyUnit.Kilojoule);
        }

        [Fact]
        public void DeserializeAsValueWithUnit_ShouldReturnValidResult()
        {
            // arrange
            string json = @"{
  'joules': 5000,
  'unit': {
    'name': 'kilojoule',
    'abbreviation': 'kJ',
    'valueInJoules': 1000
  }
}";
            var converter = new EnergyJsonConverter();

            // act
            var result = JsonConvert.DeserializeObject<Energy>(json, converter);

            // assert
            result.Joules.Should().Be(5000d);
            result.Value.Should().Be(5d);
            result.Unit.Should().Be(EnergyUnit.Kilojoule);
        }

        [Theory]
        [InlineData(EnergyJsonSerializationFormat.AsJoules)]
        [InlineData(EnergyJsonSerializationFormat.AsJoulesWithUnit)]
        public void SerializeAndDeserializeWithJoules_ShouldBeIdempotent(EnergyJsonSerializationFormat serializationFormat)
        {
            // arrange
            var energy = Fixture.Create<Energy>();
            var converter = new EnergyJsonConverter(serializationFormat);

            // act
            string serializedEnergy1 = JsonConvert.SerializeObject(energy, converter);
            var deserializedEnergy1 = JsonConvert.DeserializeObject<Energy>(serializedEnergy1, converter);
            string serializedEnergy2 = JsonConvert.SerializeObject(deserializedEnergy1, converter);
            var deserializedEnergy2 = JsonConvert.DeserializeObject<Energy>(serializedEnergy2, converter);

            // assert
            deserializedEnergy1.Should().Be(energy);
            deserializedEnergy2.Should().Be(energy);

            deserializedEnergy2.Should().Be(deserializedEnergy1);
            serializedEnergy2.Should().Be(serializedEnergy1);
        }

        [Fact]
        public void SerializeAndDeserializeWithoutJoules_ShouldBeApproximatelyIdempotent()
        {
            // arrange
            var energy = Fixture.Create<Energy>();
            var converter = new EnergyJsonConverter(EnergyJsonSerializationFormat.AsValueWithUnit);

            // act
            string serializedEnergy1 = JsonConvert.SerializeObject(energy, converter);
            var deserializedEnergy1 = JsonConvert.DeserializeObject<Energy>(serializedEnergy1, converter);
            string serializedEnergy2 = JsonConvert.SerializeObject(deserializedEnergy1, converter);
            var deserializedEnergy2 = JsonConvert.DeserializeObject<Energy>(serializedEnergy2, converter);

            // assert
            deserializedEnergy1.Joules.Should().BeApproximately(energy.Joules, DoublePrecision);
            deserializedEnergy2.Joules.Should().BeApproximately(energy.Joules, DoublePrecision);

            deserializedEnergy2.Should().Be(deserializedEnergy1);
            serializedEnergy2.Should().Be(serializedEnergy1);
        }
    }
}
