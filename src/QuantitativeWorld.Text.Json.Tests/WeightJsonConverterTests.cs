using AutoFixture;
using FluentAssertions;
using Newtonsoft.Json;
using QuantitativeWorld.TestAbstractions;
using Xunit;

namespace QuantitativeWorld.Text.Json.Tests
{
    public class WeightJsonConverterTests : TestsBase
    {
        public WeightJsonConverterTests(TestFixture testFixture) : base(testFixture) { }

        [Theory]
        [InlineData(123.456, WeightJsonSerializationFormat.AsKilograms, "{\"Kilograms\":0.123456}")]
        [InlineData(123.456, WeightJsonSerializationFormat.AsKilogramsWithUnit, "{\"Kilograms\":0.123456,\"Unit\":{\"Name\":\"gram\",\"Abbreviation\":\"g\",\"ValueInKilograms\":0.001}}")]
        [InlineData(123.456, WeightJsonSerializationFormat.AsValueWithUnit, "{\"Value\":123.456,\"Unit\":{\"Name\":\"gram\",\"Abbreviation\":\"g\",\"ValueInKilograms\":0.001}}")]
        public void Serialize_ShouldReturnValidJson(decimal grams, WeightJsonSerializationFormat serializationFormat, string expectedJson)
        {
            // arrange
            var weight = new Weight(grams, WeightUnit.Gram);
            var converter = new WeightJsonConverter(serializationFormat);

            // act
            string actualJson = JsonConvert.SerializeObject(weight, converter);

            // assert
            actualJson.Should().Be(expectedJson);
        }

        [Fact]
        public void DeserializeAsKilograms_ShouldReturnValidResult()
        {
            // arrange
            string json = @"{
  'kilograms': 0.123456
}";
            var converter = new WeightJsonConverter();

            // act
            var result = JsonConvert.DeserializeObject<Weight>(json, converter);

            // assert
            result.Kilograms.Should().Be(0.123456m);
            result.Value.Should().Be(0.123456m);
            result.Unit.Should().Be(WeightUnit.Kilogram);
        }

        [Fact]
        public void DeserializeAsKilogramsWithUnit_ShouldReturnValidResult()
        {
            // arrange
            string json = @"{
  'kilograms': 0.123456,
  'unit': {
    'name': 'gram',
    'abbreviation': 'g',
    'valueInKilograms': '0.001'
  }
}";
            var converter = new WeightJsonConverter();

            // act
            var result = JsonConvert.DeserializeObject<Weight>(json, converter);

            // assert
            result.Kilograms.Should().Be(0.123456m);
            result.Value.Should().Be(123.456m);
            result.Unit.Should().Be(WeightUnit.Gram);
        }

        [Fact]
        public void DeserializeAsValueWithUnit_ShouldReturnValidResult()
        {
            // arrange
            string json = @"{
  'value': 123.456,
  'unit': {
    'name': 'gram',
    'abbreviation': 'g',
    'valueInKilograms': '0.001'
  }
}";
            var converter = new WeightJsonConverter();

            // act
            var result = JsonConvert.DeserializeObject<Weight>(json, converter);

            // assert
            result.Kilograms.Should().Be(0.123456m);
            result.Value.Should().Be(123.456m);
            result.Unit.Should().Be(WeightUnit.Gram);
        }

        [Theory]
        [InlineData(WeightJsonSerializationFormat.AsKilograms)]
        [InlineData(WeightJsonSerializationFormat.AsKilogramsWithUnit)]
        public void SerializeAndDeserializeWithKilograms_ShouldBeIdempotent(WeightJsonSerializationFormat serializationFormat)
        {
            // arrange
            var weight = Fixture.Create<Weight>();
            var converter = new WeightJsonConverter(serializationFormat);

            // act
            string serializedWeight1 = JsonConvert.SerializeObject(weight, converter);
            var deserializedWeight1 = JsonConvert.DeserializeObject<Weight>(serializedWeight1, converter);
            string serializedWeight2 = JsonConvert.SerializeObject(deserializedWeight1, converter);
            var deserializedWeight2 = JsonConvert.DeserializeObject<Weight>(serializedWeight2, converter);

            // assert
            deserializedWeight1.Should().Be(weight);
            deserializedWeight2.Should().Be(weight);

            deserializedWeight2.Should().Be(deserializedWeight1);
            serializedWeight2.Should().Be(serializedWeight1);
        }

        [Fact]
        public void SerializeAndDeserializeWithoutKilograms_ShouldBeApproximatelyIdempotent()
        {
            // arrange
            var weight = Fixture.Create<Weight>();
            var converter = new WeightJsonConverter(WeightJsonSerializationFormat.AsValueWithUnit);

            // act
            string serializedWeight1 = JsonConvert.SerializeObject(weight, converter);
            var deserializedWeight1 = JsonConvert.DeserializeObject<Weight>(serializedWeight1, converter);
            string serializedWeight2 = JsonConvert.SerializeObject(deserializedWeight1, converter);
            var deserializedWeight2 = JsonConvert.DeserializeObject<Weight>(serializedWeight2, converter);

            // assert
            deserializedWeight1.Kilograms.Should().BeApproximately(weight.Kilograms, DecimalPrecision);
            deserializedWeight2.Kilograms.Should().BeApproximately(weight.Kilograms, DecimalPrecision);

            deserializedWeight2.Should().Be(deserializedWeight1);
            serializedWeight2.Should().Be(serializedWeight1);
        }
    }
}
