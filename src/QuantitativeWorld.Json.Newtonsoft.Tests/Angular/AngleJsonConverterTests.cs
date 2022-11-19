using AutoFixture;
using FluentAssertions;
using Newtonsoft.Json;
using System.Collections.Generic;
using Xunit;

#if DECIMAL
namespace DecimalQuantitativeWorld.Json.Newtonsoft.Tests.Angular
{
    using DecimalQuantitativeWorld.Angular;
    using DecimalQuantitativeWorld.TestAbstractions;
    using DecimalQuantitativeWorld.Json.Newtonsoft.Angular;
    using number = System.Decimal;
#else
namespace QuantitativeWorld.Json.Newtonsoft.Tests.Angular
{
    using QuantitativeWorld.Angular;
    using QuantitativeWorld.TestAbstractions;
    using QuantitativeWorld.Json.Newtonsoft.Angular;
    using number = System.Double;
#endif

    public class AngleJsonConverterTests : TestsBase
    {
        public AngleJsonConverterTests(TestFixture testFixture) : base(testFixture) { }

        [Theory]
        [MemberData(nameof(GetTestData), typeof(AngleJsonConverterTests), nameof(GetSerializeTestData))]
        public void Serialize_ShouldReturnValidJson(AngleSerializeTestData testData)
        {
            // arrange
            var converter = new AngleJsonConverter(testData.Format, testData.UnitFormat);

            // act
            string actualJson = JsonConvert.SerializeObject(testData.Quantity, converter);

            // assert
            actualJson.Should().MatchRegex(testData.ExpectedJsonPattern);
        }
        private static IEnumerable<AngleSerializeTestData> GetSerializeTestData()
        {
            var testValue = new Angle((number)180m, AngleUnit.Degree);
            yield return new AngleSerializeTestData(
                value: testValue,
                format: AngleJsonSerializationFormat.AsTurns,
                unitFormat: LinearUnitJsonSerializationFormat.PredefinedAsString,
                expectedJsonPattern: "{\"Turns\":0.5}");
            yield return new AngleSerializeTestData(
                value: testValue,
                format: AngleJsonSerializationFormat.AsTurnsWithUnit,
                unitFormat: LinearUnitJsonSerializationFormat.PredefinedAsString,
                expectedJsonPattern: "{\"Turns\":0.5,\"Unit\":\"deg\"}");
            yield return new AngleSerializeTestData(
                value: testValue,
                format: AngleJsonSerializationFormat.AsValueWithUnit,
                unitFormat: LinearUnitJsonSerializationFormat.PredefinedAsString,
                expectedJsonPattern: "{\"Value\":180(\\.0+),\"Unit\":\"deg\"}");
            yield return new AngleSerializeTestData(
                value: testValue,
                format: AngleJsonSerializationFormat.AsTurnsWithUnit,
                unitFormat: LinearUnitJsonSerializationFormat.AlwaysFull,
                expectedJsonPattern: "{\"Turns\":0.5,\"Unit\":{\"Name\":\"degree\",\"Abbreviation\":\"deg\",\"Symbol\":\"°\",\"UnitsPerTurn\":360(\\.0+)}}");
            yield return new AngleSerializeTestData(
                value: testValue,
                format: AngleJsonSerializationFormat.AsValueWithUnit,
                unitFormat: LinearUnitJsonSerializationFormat.AlwaysFull,
                expectedJsonPattern: "{\"Value\":180(\\.0+),\"Unit\":{\"Name\":\"degree\",\"Abbreviation\":\"deg\",\"Symbol\":\"°\",\"UnitsPerTurn\":360(\\.0+)}}");
        }
        public class AngleSerializeTestData : QuantitySerializeTestData<Angle>
        {
            public AngleSerializeTestData(
                Angle value,
                AngleJsonSerializationFormat format,
                LinearUnitJsonSerializationFormat unitFormat,
                string expectedJsonPattern)
                : base(value, unitFormat, expectedJsonPattern)
            {
                Format = format;
            }

            public AngleJsonSerializationFormat Format { get; set; }
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
            result.Turns.Should().Be(0.5d);
            result.Value.Should().Be(0.5d);
            result.Unit.Should().Be(AngleUnit.Turn);
        }

        [Fact]
        public void DeserializeAsTurnsWithPredefinedUnit_ShouldReturnValidResult()
        {
            // arrange
            string json = @"{
  'turns': 123.456,
  'unit': 'deg'
}";
            var converter = new AngleJsonConverter();
            var expectedResult =
                new Angle(turns: (number)123.456m)
                .Convert(AngleUnit.Degree);

            // act
            var result = JsonConvert.DeserializeObject<Angle>(json, converter);

            // assert
            result.Should().Be(expectedResult);
        }

        [Fact]
        public void DeserializeAsTurnsWithFullySerializedUnit_ShouldReturnValidResult()
        {
            // arrange
            string json = @"{
  'turns': 123.456,
  'unit': {
    'name': 'degree',
    'abbreviation': 'deg',
    'symbol': '°',
    'unitsPerTurn': '360'
  }
}";
            var converter = new AngleJsonConverter();
            var expectedResult =
                new Angle(turns: (number)123.456m)
                .Convert(new AngleUnit(
                    name: "degree",
                    abbreviation: "deg",
                    symbol: "°",
                    unitsPerTurn: (number)360));

            // act
            var result = JsonConvert.DeserializeObject<Angle>(json, converter);

            // assert
            result.Should().Be(expectedResult);
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
            result.Turns.Should().Be(0.5d);
            result.Value.Should().Be(180d);
            result.Unit.Should().Be(AngleUnit.Degree);
        }

        [Theory]
        [InlineData(AngleJsonSerializationFormat.AsTurns, LinearUnitJsonSerializationFormat.AlwaysFull)]
        [InlineData(AngleJsonSerializationFormat.AsTurns, LinearUnitJsonSerializationFormat.PredefinedAsString)]
        [InlineData(AngleJsonSerializationFormat.AsTurnsWithUnit, LinearUnitJsonSerializationFormat.AlwaysFull)]
        [InlineData(AngleJsonSerializationFormat.AsTurnsWithUnit, LinearUnitJsonSerializationFormat.PredefinedAsString)]
        public void SerializeAndDeserializeWithTurns_ShouldBeIdempotent(AngleJsonSerializationFormat format, LinearUnitJsonSerializationFormat unitFormat)
        {
            // arrange
            var angle = Fixture.Create<Angle>();
            var converter = new AngleJsonConverter(format, unitFormat);

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
            deserializedAngle1.Turns.Should().BeApproximately(angle.Turns);
            deserializedAngle2.Turns.Should().BeApproximately(angle.Turns);

            deserializedAngle2.Should().Be(deserializedAngle1);
            serializedAngle2.Should().Be(serializedAngle1);
        }
    }
}
