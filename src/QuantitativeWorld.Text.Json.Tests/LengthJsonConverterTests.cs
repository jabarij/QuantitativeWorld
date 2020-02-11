using AutoFixture;
using FluentAssertions;
using Newtonsoft.Json;
using QuantitativeWorld.TestAbstractions;
using Xunit;

namespace QuantitativeWorld.Text.Json.Tests
{
    public class LengthJsonConverterTests : TestsBase
    {
        public LengthJsonConverterTests(TestFixture testFixture) : base(testFixture) { }

        [Theory]
        [InlineData(123.456, LengthJsonSerializationFormat.AsMetres, "{\"Metres\":0.123456}")]
        [InlineData(123.456, LengthJsonSerializationFormat.AsMetresWithUnit, "{\"Metres\":0.123456,\"Unit\":{\"Name\":\"millimetre\",\"Abbreviation\":\"mm\",\"ValueInMetres\":0.001}}")]
        [InlineData(123.456, LengthJsonSerializationFormat.AsValueWithUnit, "{\"Value\":123.456,\"Unit\":{\"Name\":\"millimetre\",\"Abbreviation\":\"mm\",\"ValueInMetres\":0.001}}")]
        public void Serialize_ShouldReturnValidJson(decimal millimetres, LengthJsonSerializationFormat serializationFormat, string expectedJson)
        {
            // arrange
            var length = new Length(millimetres, LengthUnit.Millimetre);
            var converter = new LengthJsonConverter(serializationFormat);

            // act
            string actualJson = JsonConvert.SerializeObject(length, converter);

            // assert
            actualJson.Should().Be(expectedJson);
        }

        [Fact]
        public void DeserializeAsMetres_ShouldReturnValidResult()
        {
            // arrange
            string json = @"{
  'metres': 0.123456
}";
            var converter = new LengthJsonConverter();

            // act
            var result = JsonConvert.DeserializeObject<Length>(json, converter);

            // assert
            result.Metres.Should().Be(0.123456m);
            result.Value.Should().Be(0.123456m);
            result.Unit.Should().Be(LengthUnit.Metre);
        }

        [Fact]
        public void DeserializeAsMetresWithUnit_ShouldReturnValidResult()
        {
            // arrange
            string json = @"{
  'metres': 0.123456,
  'unit': {
    'name': 'millimetre',
    'abbreviation': 'mm',
    'valueInMetres': '0.001'
  }
}";
            var converter = new LengthJsonConverter();

            // act
            var result = JsonConvert.DeserializeObject<Length>(json, converter);

            // assert
            result.Metres.Should().Be(0.123456m);
            result.Value.Should().Be(123.456m);
            result.Unit.Should().Be(LengthUnit.Millimetre);
        }

        [Fact]
        public void DeserializeAsValueWithUnit_ShouldReturnValidResult()
        {
            // arrange
            string json = @"{
  'value': 123.456,
  'unit': {
    'name': 'millimetre',
    'abbreviation': 'mm',
    'valueInMetres': '0.001'
  }
}";
            var converter = new LengthJsonConverter();

            // act
            var result = JsonConvert.DeserializeObject<Length>(json, converter);

            // assert
            result.Metres.Should().Be(0.123456m);
            result.Value.Should().Be(123.456m);
            result.Unit.Should().Be(LengthUnit.Millimetre);
        }

        [Theory]
        [InlineData(LengthJsonSerializationFormat.AsMetres)]
        [InlineData(LengthJsonSerializationFormat.AsMetresWithUnit)]
        public void SerializeAndDeserializeWithMetres_ShouldBeIdempotent(LengthJsonSerializationFormat serializationFormat)
        {
            // arrange
            var length = Fixture.Create<Length>();
            var converter = new LengthJsonConverter(serializationFormat);

            // act
            string serializedLength1 = JsonConvert.SerializeObject(length, converter);
            var deserializedLength1 = JsonConvert.DeserializeObject<Length>(serializedLength1, converter);
            string serializedLength2 = JsonConvert.SerializeObject(deserializedLength1, converter);
            var deserializedLength2 = JsonConvert.DeserializeObject<Length>(serializedLength2, converter);

            // assert
            deserializedLength1.Should().Be(length);
            deserializedLength2.Should().Be(length);

            deserializedLength2.Should().Be(deserializedLength1);
            serializedLength2.Should().Be(serializedLength1);
        }

        [Fact]
        public void SerializeAndDeserializeWithoutMetres_ShouldBeApproximatelyIdempotent()
        {
            // arrange
            var length = Fixture.Create<Length>();
            var converter = new LengthJsonConverter(LengthJsonSerializationFormat.AsValueWithUnit);

            // act
            string serializedLength1 = JsonConvert.SerializeObject(length, converter);
            var deserializedLength1 = JsonConvert.DeserializeObject<Length>(serializedLength1, converter);
            string serializedLength2 = JsonConvert.SerializeObject(deserializedLength1, converter);
            var deserializedLength2 = JsonConvert.DeserializeObject<Length>(serializedLength2, converter);

            // assert
            deserializedLength1.Metres.Should().BeApproximately(length.Metres, DecimalPrecision);
            deserializedLength2.Metres.Should().BeApproximately(length.Metres, DecimalPrecision);

            deserializedLength2.Should().Be(deserializedLength1);
            serializedLength2.Should().Be(serializedLength1);
        }
    }
}
