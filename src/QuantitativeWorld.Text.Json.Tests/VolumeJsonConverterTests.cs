using AutoFixture;
using FluentAssertions;
using Newtonsoft.Json;
using QuantitativeWorld.TestAbstractions;
using Xunit;

namespace QuantitativeWorld.Text.Json.Tests
{
    public class VolumeJsonConverterTests : TestsBase
    {
        public VolumeJsonConverterTests(TestFixture testFixture) : base(testFixture) { }

        [Theory]
        [InlineData(5000000d, VolumeJsonSerializationFormat.AsCubicMetres, "{\"CubicMetres\":5.0}")]
        [InlineData(5000000d, VolumeJsonSerializationFormat.AsCubicMetresWithUnit, "{\"CubicMetres\":5.0,\"Unit\":{\"Name\":\"cubic centimetre\",\"Abbreviation\":\"cm³\",\"ValueInCubicMetres\":1E-06}}")]
        [InlineData(5000000d, VolumeJsonSerializationFormat.AsValueWithUnit, "{\"Value\":5000000.0,\"Unit\":{\"Name\":\"cubic centimetre\",\"Abbreviation\":\"cm³\",\"ValueInCubicMetres\":1E-06}}")]
        public void Serialize_ShouldReturnValidJson(double cubicCentimetres, VolumeJsonSerializationFormat serializationFormat, string expectedJson)
        {
            // arrange
            var volume = new Volume(cubicCentimetres, VolumeUnit.CubicCentimetre);
            var converter = new VolumeJsonConverter(serializationFormat);

            // act
            string actualJson = JsonConvert.SerializeObject(volume, converter);

            // assert
            actualJson.Should().Be(expectedJson);
        }

        [Fact]
        public void DeserializeAsMetres_ShouldReturnValidResult()
        {
            // arrange
            string json = @"{
  'cubicMetres': 0.123456
}";
            var converter = new VolumeJsonConverter();

            // act
            var result = JsonConvert.DeserializeObject<Volume>(json, converter);

            // assert
            result.CubicMetres.Should().Be(0.123456d);
            result.Value.Should().Be(0.123456d);
            result.Unit.Should().Be(VolumeUnit.CubicMetre);
        }

        [Fact]
        public void DeserializeAsMetresWithUnit_ShouldReturnValidResult()
        {
            // arrange
            string json = @"{
  'cubicMetres': 0.123456,
  'unit': {
    'name': 'cubic centimetre',
    'abbreviation': 'cm³',
    'valueInCubicMetres': '0.000001'
  }
}";
            var converter = new VolumeJsonConverter();

            // act
            var result = JsonConvert.DeserializeObject<Volume>(json, converter);

            // assert
            result.CubicMetres.Should().Be(0.123456d);
            result.Value.Should().BeApproximately(123456d, DoublePrecision);
            result.Unit.Should().Be(VolumeUnit.CubicCentimetre);
        }

        [Fact]
        public void DeserializeAsValueWithUnit_ShouldReturnValidResult()
        {
            // arrange
            string json = @"{
  'value': 123456,
  'unit': {
    'name': 'cubic centimetre',
    'abbreviation': 'cm³',
    'valueInCubicMetres': '0.000001'
  }
}";
            var converter = new VolumeJsonConverter();

            // act
            var result = JsonConvert.DeserializeObject<Volume>(json, converter);

            // assert
            result.CubicMetres.Should().BeApproximately(0.123456d, DoublePrecision);
            result.Value.Should().BeApproximately(123456d, DoublePrecision);
            result.Unit.Should().Be(VolumeUnit.CubicCentimetre);
        }

        [Theory]
        [InlineData(VolumeJsonSerializationFormat.AsCubicMetres)]
        [InlineData(VolumeJsonSerializationFormat.AsCubicMetresWithUnit)]
        public void SerializeAndDeserializeWithMetres_ShouldBeIdempotent(VolumeJsonSerializationFormat serializationFormat)
        {
            // arrange
            var volume = Fixture.Create<Volume>();
            var converter = new VolumeJsonConverter(serializationFormat);

            // act
            string serializedVolume1 = JsonConvert.SerializeObject(volume, converter);
            var deserializedVolume1 = JsonConvert.DeserializeObject<Volume>(serializedVolume1, converter);
            string serializedVolume2 = JsonConvert.SerializeObject(deserializedVolume1, converter);
            var deserializedVolume2 = JsonConvert.DeserializeObject<Volume>(serializedVolume2, converter);

            // assert
            deserializedVolume1.Should().Be(volume);
            deserializedVolume2.Should().Be(volume);

            deserializedVolume2.Should().Be(deserializedVolume1);
            serializedVolume2.Should().Be(serializedVolume1);
        }

        [Fact]
        public void SerializeAndDeserializeWithoutMetres_ShouldBeApproximatelyIdempotent()
        {
            // arrange
            var volume = Fixture.Create<Volume>();
            var converter = new VolumeJsonConverter(VolumeJsonSerializationFormat.AsValueWithUnit);

            // act
            string serializedVolume1 = JsonConvert.SerializeObject(volume, converter);
            var deserializedVolume1 = JsonConvert.DeserializeObject<Volume>(serializedVolume1, converter);
            string serializedVolume2 = JsonConvert.SerializeObject(deserializedVolume1, converter);
            var deserializedVolume2 = JsonConvert.DeserializeObject<Volume>(serializedVolume2, converter);

            // assert
            deserializedVolume1.CubicMetres.Should().BeApproximately(volume.CubicMetres, DoublePrecision);
            deserializedVolume2.CubicMetres.Should().BeApproximately(volume.CubicMetres, DoublePrecision);

            deserializedVolume2.Should().Be(deserializedVolume1);
            serializedVolume2.Should().Be(serializedVolume1);
        }
    }
}
