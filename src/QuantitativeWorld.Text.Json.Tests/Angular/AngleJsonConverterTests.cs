using AutoFixture;
using FluentAssertions;
using Newtonsoft.Json;
using QuantitativeWorld.Angular;
using QuantitativeWorld.TestAbstractions;
using QuantitativeWorld.Text.Json.Angular;
using Xunit;

namespace QuantitativeWorld.Text.Json.Tests.Angular
{
    public class AngleJsonConverterTests : TestsBase
    {
        public AngleJsonConverterTests(TestFixture testFixture) : base(testFixture) { }

        [Theory]
        [InlineData(180, AngleJsonSerializationFormat.AsTurns, "{\"Turns\":0.5}")]
        [InlineData(180, AngleJsonSerializationFormat.AsTurnsWithUnit, "{\"Turns\":0.5,\"Unit\":{\"Name\":\"degree\",\"Abbreviation\":\"deg\",\"Symbol\":\"°\",\"UnitsPerTurn\":360.0}}")]
        [InlineData(180, AngleJsonSerializationFormat.AsValueWithUnit, "{\"Value\":180.0,\"Unit\":{\"Name\":\"degree\",\"Abbreviation\":\"deg\",\"Symbol\":\"°\",\"UnitsPerTurn\":360.0}}")]
        public void Serialize_ShouldReturnValidJson(decimal degrees, AngleJsonSerializationFormat serializationFormat, string expectedJson)
        {
            // arrange
            var angle = new Angle(degrees, AngleUnit.Degree);
            var converter = new AngleJsonConverter(serializationFormat);

            // act
            string actualJson = JsonConvert.SerializeObject(angle, converter);

            // assert
            actualJson.Should().Be(expectedJson);
        }

        [Fact]
        public void DeserializeAsTurns_ShouldReturnValidResult()
        {
            // arrange
            string json = @"{
  'turns': 0.5
}";
            var converter = new AngleJsonConverter();

            // act
            var result = JsonConvert.DeserializeObject<Angle>(json, converter);

            // assert
            result.Turns.Should().Be(0.5m);
            result.Value.Should().Be(0.5m);
            result.Unit.Should().Be(AngleUnit.Turn);
        }

        [Fact]
        public void DeserializeAsTurnsWithUnit_ShouldReturnValidResult()
        {
            // arrange
            string json = @"{
  'turns': 0.5,
  'unit': {
    'name': 'degree',
    'abbreviation': 'deg',
    'symbol': '°',
    'unitsPerTurn': 360
  }
}";
            var converter = new AngleJsonConverter();

            // act
            var result = JsonConvert.DeserializeObject<Angle>(json, converter);

            // assert
            result.Turns.Should().Be(0.5m);
            result.Value.Should().Be(180m);
            result.Unit.Should().Be(AngleUnit.Degree);
        }

        [Fact]
        public void DeserializeAsValueWithUnit_ShouldReturnValidResult()
        {
            // arrange
            string json = @"{
  'value': 180,
  'unit': {
    'name': 'degree',
    'abbreviation': 'deg',
    'symbol': '°',
    'unitsPerTurn': 360
  }
}";
            var converter = new AngleJsonConverter();

            // act
            var result = JsonConvert.DeserializeObject<Angle>(json, converter);

            // assert
            result.Turns.Should().Be(0.5m);
            result.Value.Should().Be(180m);
            result.Unit.Should().Be(AngleUnit.Degree);
        }

        [Theory]
        [InlineData(AngleJsonSerializationFormat.AsTurns)]
        [InlineData(AngleJsonSerializationFormat.AsTurnsWithUnit)]
        public void SerializeAndDeserializeWithTurns_ShouldBeIdempotent(AngleJsonSerializationFormat serializationFormat)
        {
            // arrange
            var angle = Fixture.Create<Angle>();
            var converter = new AngleJsonConverter(serializationFormat);

            // act
            string serializedAngle1 = JsonConvert.SerializeObject(angle, converter);
            var deserializedAngle1 = JsonConvert.DeserializeObject<Angle>(serializedAngle1, converter);
            string serializedAngle2 = JsonConvert.SerializeObject(deserializedAngle1, converter);
            var deserializedAngle2 = JsonConvert.DeserializeObject<Angle>(serializedAngle2, converter);

            // assert
            deserializedAngle1.Should().Be(angle);
            deserializedAngle2.Should().Be(angle);

            deserializedAngle2.Should().Be(deserializedAngle1);
            serializedAngle2.Should().Be(serializedAngle1);
        }

        [Fact]
        public void SerializeAndDeserializeWithoutTurns_ShouldBeApproximatelyIdempotent()
        {
            // arrange
            var angle = Fixture.Create<Angle>();
            var converter = new AngleJsonConverter(AngleJsonSerializationFormat.AsValueWithUnit);

            // act
            string serializedAngle1 = JsonConvert.SerializeObject(angle, converter);
            var deserializedAngle1 = JsonConvert.DeserializeObject<Angle>(serializedAngle1, converter);
            string serializedAngle2 = JsonConvert.SerializeObject(deserializedAngle1, converter);
            var deserializedAngle2 = JsonConvert.DeserializeObject<Angle>(serializedAngle2, converter);

            // assert
            deserializedAngle1.Turns.Should().BeApproximately(angle.Turns, DecimalPrecision);
            deserializedAngle2.Turns.Should().BeApproximately(angle.Turns, DecimalPrecision);

            deserializedAngle2.Should().Be(deserializedAngle1);
            serializedAngle2.Should().Be(serializedAngle1);
        }
    }
}
