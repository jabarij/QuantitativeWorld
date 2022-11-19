using AutoFixture;
using FluentAssertions;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using Xunit;

#if DECIMAL
namespace DecimalQuantitativeWorld.Json.Tests.Angular
{
    using DecimalQuantitativeWorld.Angular;
    using DecimalQuantitativeWorld.Json.Angular;
    using DecimalQuantitativeWorld.TestAbstractions;
    using number = System.Decimal;
    using TestsBase = DecimalQuantitativeWorld.Json.Tests.TestsBase;

#else
namespace QuantitativeWorld.Json.Tests.Angular
{
    using QuantitativeWorld.Angular;
    using QuantitativeWorld.Json.Angular;
    using QuantitativeWorld.TestAbstractions;
    using number = System.Double;
    using TestsBase = QuantitativeWorld.Json.Tests.TestsBase;
#endif

    public class AngleJsonConverterTests : TestsBase
    {
        public AngleJsonConverterTests(TestFixture testFixture) : base(testFixture)
        {
        }

        protected override void Configure(JsonSerializerOptions options)
        {
            base.Configure(options);
            options.Encoder = JavaScriptEncoder
                .Create(UnicodeRanges.BasicLatin, UnicodeRange.Create('\u00B0', '\u00B0'));
        }

        [Theory]
        [MemberData(nameof(GetTestData), typeof(AngleJsonConverterTests), nameof(GetSerializeTestData))]
        public void Serialize_ShouldReturnValidJson(AngleSerializeTestData testData)
        {
            // arrange
            var converter = new AngleJsonConverter(testData.Format);
            var unitConverter = new AngleUnitJsonConverter(testData.UnitFormat);

            // act
            string actualJson = Serialize(testData.Quantity, converter, unitConverter);

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
                expectedJsonPattern: "{\"Value\":180(\\.0+)?,\"Unit\":\"deg\"}");
            yield return new AngleSerializeTestData(
                value: testValue,
                format: AngleJsonSerializationFormat.AsTurnsWithUnit,
                unitFormat: LinearUnitJsonSerializationFormat.AlwaysFull,
                expectedJsonPattern:
                "{\"Turns\":0.5,\"Unit\":{\"Name\":\"degree\",\"Abbreviation\":\"deg\",\"Symbol\":\"°\",\"UnitsPerTurn\":360(\\.0+)?}}");
            yield return new AngleSerializeTestData(
                value: testValue,
                format: AngleJsonSerializationFormat.AsValueWithUnit,
                unitFormat: LinearUnitJsonSerializationFormat.AlwaysFull,
                expectedJsonPattern:
                "{\"Value\":180(\\.0+)?,\"Unit\":{\"Name\":\"degree\",\"Abbreviation\":\"deg\",\"Symbol\":\"°\",\"UnitsPerTurn\":360(\\.0+)?}}");
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
  ""Turns"": 0.5
}";
            var converter = new AngleJsonConverter();
            var unitConverter = new AngleUnitJsonConverter();

            // act
            var result = Deserialize<Angle>(json, converter, unitConverter);

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
  ""Turns"": 123.456,
  ""Unit"": ""deg""
}";
            var converter = new AngleJsonConverter();
            var unitConverter = new AngleUnitJsonConverter();
            var expectedResult =
                new Angle(turns: (number)123.456m)
                    .Convert(AngleUnit.Degree);

            // act
            var result = Deserialize<Angle>(json, converter, unitConverter);

            // assert
            result.Should().Be(expectedResult);
        }

        [Fact]
        public void DeserializeAsTurnsWithFullySerializedUnit_ShouldReturnValidResult()
        {
            // arrange
            string json = @"{
  ""Turns"": 123.456,
  ""Unit"": {
    ""Name"": ""degree"",
    ""Abbreviation"": ""deg"",
    ""Symbol"": ""°"",
    ""UnitsPerTurn"": 360
  }
}";
            var converter = new AngleJsonConverter();
            var unitConverter = new AngleUnitJsonConverter();
            var expectedResult =
                new Angle(turns: (number)123.456m)
                    .Convert(new AngleUnit(
                        name: "degree",
                        abbreviation: "deg",
                        symbol: "°",
                        unitsPerTurn: (number)360));

            // act
            var result = Deserialize<Angle>(json, converter, unitConverter);

            // assert
            result.Should().Be(expectedResult);
        }

        [Fact]
        public void DeserializeAsValueWithUnit_ShouldReturnValidResult()
        {
            // arrange
            string json = @"{
  ""Value"": 180,
  ""Unit"": {
    ""Name"": ""degree"",
    ""Abbreviation"": ""deg"",
    ""Symbol"": ""°"",
    ""UnitsPerTurn"": 360
  }
}";
            var converter = new AngleJsonConverter();
            var unitConverter = new AngleUnitJsonConverter();

            // act
            var result = Deserialize<Angle>(json, converter, unitConverter);

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
        public void SerializeAndDeserializeWithTurns_ShouldBeIdempotent(AngleJsonSerializationFormat format,
            LinearUnitJsonSerializationFormat unitFormat)
        {
            // arrange
            var angle = Fixture.Create<Angle>();
            var converter = new AngleJsonConverter(format);
            var unitConverter = new AngleUnitJsonConverter(unitFormat);

            // act
            string serializedAngle1 = Serialize(angle, converter, unitConverter);
            var deserializedAngle1 = Deserialize<Angle>(serializedAngle1, converter, unitConverter);
            string serializedAngle2 = Serialize(deserializedAngle1, converter, unitConverter);
            var deserializedAngle2 = Deserialize<Angle>(serializedAngle2, converter, unitConverter);

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
            var unitConverter = new AngleUnitJsonConverter();

            // act
            string serializedAngle1 = Serialize(angle, converter, unitConverter);
            var deserializedAngle1 = Deserialize<Angle>(serializedAngle1, converter, unitConverter);
            string serializedAngle2 = Serialize(deserializedAngle1, converter, unitConverter);
            var deserializedAngle2 = Deserialize<Angle>(serializedAngle2, converter, unitConverter);

            // assert
            deserializedAngle1.Turns.Should().BeApproximately(angle.Turns);
            deserializedAngle2.Turns.Should().BeApproximately(angle.Turns);

            deserializedAngle2.Should().Be(deserializedAngle1);
            serializedAngle2.Should().Be(serializedAngle1);
        }
    }
}