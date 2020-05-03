using AutoFixture;
using FluentAssertions;
using Newtonsoft.Json;
using QuantitativeWorld.TestAbstractions;
using Xunit;

namespace QuantitativeWorld.Text.Json.Tests
{
    public class AreaJsonConverterTests : TestsBase
    {
        public AreaJsonConverterTests(TestFixture testFixture) : base(testFixture) { }

        [Theory]
        [InlineData(5000000d, AreaJsonSerializationFormat.AsSquareMetres, "{\"SquareMetres\":5.0}")]
        [InlineData(5000000d, AreaJsonSerializationFormat.AsSquareMetresWithUnit, "{\"SquareMetres\":5.0,\"Unit\":{\"Name\":\"square millimetre\",\"Abbreviation\":\"mm²\",\"ValueInSquareMetres\":1E-06}}")]
        [InlineData(5000000d, AreaJsonSerializationFormat.AsValueWithUnit, "{\"Value\":5000000.0,\"Unit\":{\"Name\":\"square millimetre\",\"Abbreviation\":\"mm²\",\"ValueInSquareMetres\":1E-06}}")]
        public void Serialize_ShouldReturnValidJson(double squareMillimetres, AreaJsonSerializationFormat serializationFormat, string expectedJson)
        {
            // arrange
            var area = new Area(squareMillimetres, AreaUnit.SquareMillimetre);
            var converter = new AreaJsonConverter(serializationFormat);

            // act
            string actualJson = JsonConvert.SerializeObject(area, converter);

            // assert
            actualJson.Should().Be(expectedJson);
        }

        [Fact]
        public void DeserializeAsMetres_ShouldReturnValidResult()
        {
            // arrange
            string json = @"{
  'squareMetres': 0.123456
}";
            var converter = new AreaJsonConverter();

            // act
            var result = JsonConvert.DeserializeObject<Area>(json, converter);

            // assert
            result.SquareMetres.Should().Be(0.123456d);
            result.Value.Should().Be(0.123456d);
            result.Unit.Should().Be(AreaUnit.SquareMetre);
        }

        [Fact]
        public void DeserializeAsMetresWithUnit_ShouldReturnValidResult()
        {
            // arrange
            string json = @"{
  'squareMetres': 0.123456,
  'unit': {
    'name': 'square millimetre',
    'abbreviation': 'mm²',
    'valueInSquareMetres': '0.000001'
  }
}";
            var converter = new AreaJsonConverter();

            // act
            var result = JsonConvert.DeserializeObject<Area>(json, converter);

            // assert
            result.SquareMetres.Should().Be(0.123456d);
            result.Value.Should().BeApproximately(123456d, DoublePrecision);
            result.Unit.Should().Be(AreaUnit.SquareMillimetre);
        }

        [Fact]
        public void DeserializeAsValueWithUnit_ShouldReturnValidResult()
        {
            // arrange
            string json = @"{
  'value': 123456,
  'unit': {
    'name': 'square millimetre',
    'abbreviation': 'mm²',
    'valueInSquareMetres': '0.000001'
  }
}";
            var converter = new AreaJsonConverter();

            // act
            var result = JsonConvert.DeserializeObject<Area>(json, converter);

            // assert
            result.SquareMetres.Should().BeApproximately(0.123456d, DoublePrecision);
            result.Value.Should().BeApproximately(123456d, DoublePrecision);
            result.Unit.Should().Be(AreaUnit.SquareMillimetre);
        }

        [Theory]
        [InlineData(AreaJsonSerializationFormat.AsSquareMetres)]
        [InlineData(AreaJsonSerializationFormat.AsSquareMetresWithUnit)]
        public void SerializeAndDeserializeWithMetres_ShouldBeIdempotent(AreaJsonSerializationFormat serializationFormat)
        {
            // arrange
            var area = Fixture.Create<Area>();
            var converter = new AreaJsonConverter(serializationFormat);

            // act
            string serializedArea1 = JsonConvert.SerializeObject(area, converter);
            var deserializedArea1 = JsonConvert.DeserializeObject<Area>(serializedArea1, converter);
            string serializedArea2 = JsonConvert.SerializeObject(deserializedArea1, converter);
            var deserializedArea2 = JsonConvert.DeserializeObject<Area>(serializedArea2, converter);

            // assert
            deserializedArea1.Should().Be(area);
            deserializedArea2.Should().Be(area);

            deserializedArea2.Should().Be(deserializedArea1);
            serializedArea2.Should().Be(serializedArea1);
        }

        [Fact]
        public void SerializeAndDeserializeWithoutMetres_ShouldBeApproximatelyIdempotent()
        {
            // arrange
            var area = Fixture.Create<Area>();
            var converter = new AreaJsonConverter(AreaJsonSerializationFormat.AsValueWithUnit);

            // act
            string serializedArea1 = JsonConvert.SerializeObject(area, converter);
            var deserializedArea1 = JsonConvert.DeserializeObject<Area>(serializedArea1, converter);
            string serializedArea2 = JsonConvert.SerializeObject(deserializedArea1, converter);
            var deserializedArea2 = JsonConvert.DeserializeObject<Area>(serializedArea2, converter);

            // assert
            deserializedArea1.SquareMetres.Should().BeApproximately(area.SquareMetres, DoublePrecision);
            deserializedArea2.SquareMetres.Should().BeApproximately(area.SquareMetres, DoublePrecision);

            deserializedArea2.Should().Be(deserializedArea1);
            serializedArea2.Should().Be(serializedArea1);
        }
    }
}
