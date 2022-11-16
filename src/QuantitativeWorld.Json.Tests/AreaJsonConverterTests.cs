using AutoFixture;
using FluentAssertions;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using Xunit;

#if DECIMAL
namespace DecimalQuantitativeWorld.Json.Tests
{
    using DecimalQuantitativeWorld.TestAbstractions;
    using number = System.Decimal;

#else
namespace QuantitativeWorld.Json.Tests
{
    using QuantitativeWorld.TestAbstractions;
    using number = System.Double;
#endif

    public class AreaJsonConverterTests : TestsBase
    {
        public AreaJsonConverterTests(TestFixture testFixture) : base(testFixture)
        {
        }

        protected override void Configure(JsonSerializerOptions options)
        {
            base.Configure(options);
            options.Encoder = JavaScriptEncoder
                .Create(UnicodeRanges.BasicLatin, UnicodeRanges.SuperscriptsandSubscripts);
        }

        [Theory]
        [MemberData(nameof(GetTestData), typeof(AreaJsonConverterTests), nameof(GetSerializeTestData))]
        public void Serialize_ShouldReturnValidJson(AreaSerializeTestData testData)
        {
            // arrange
            var converter = new AreaJsonConverter(testData.Format);
            var unitConverter = new AreaUnitJsonConverter(testData.UnitFormat);

            // act
            string actualJson = Serialize(testData.Quantity, converter, unitConverter);

            // assert
            actualJson.Should().MatchRegex(testData.ExpectedJsonPattern);
        }

        private static IEnumerable<AreaSerializeTestData> GetSerializeTestData()
        {
            var testValue = new Area((number)5000000m, AreaUnit.SquareMillimetre);
            yield return new AreaSerializeTestData(
                value: testValue,
                format: AreaJsonSerializationFormat.AsSquareMetres,
                unitFormat: LinearUnitJsonSerializationFormat.PredefinedAsString,
                expectedJsonPattern: "{\"SquareMetres\":5(\\.0+)?}");
            yield return new AreaSerializeTestData(
                value: testValue,
                format: AreaJsonSerializationFormat.AsSquareMetresWithUnit,
                unitFormat: LinearUnitJsonSerializationFormat.PredefinedAsString,
                expectedJsonPattern: "{\"SquareMetres\":5(\\.0+)?,\"Unit\":\"mm\\\\u00B2\"}");
            yield return new AreaSerializeTestData(
                value: testValue,
                format: AreaJsonSerializationFormat.AsValueWithUnit,
                unitFormat: LinearUnitJsonSerializationFormat.PredefinedAsString,
                expectedJsonPattern: "{\"Value\":5000000(\\.0+)?,\"Unit\":\"mm\\\\u00B2\"}");
            yield return new AreaSerializeTestData(
                value: testValue,
                format: AreaJsonSerializationFormat.AsSquareMetresWithUnit,
                unitFormat: LinearUnitJsonSerializationFormat.AlwaysFull,
                expectedJsonPattern:
                "{\"SquareMetres\":5(\\.0+)?,\"Unit\":{\"Name\":\"square millimetre\",\"Abbreviation\":\"mm\\\\u00B2\",\"ValueInSquareMetres\":(0\\.000001|1E-06)}}");
            yield return new AreaSerializeTestData(
                value: testValue,
                format: AreaJsonSerializationFormat.AsValueWithUnit,
                unitFormat: LinearUnitJsonSerializationFormat.AlwaysFull,
                expectedJsonPattern:
                "{\"Value\":5000000(\\.0+)?,\"Unit\":{\"Name\":\"square millimetre\",\"Abbreviation\":\"mm\\\\u00B2\",\"ValueInSquareMetres\":(0\\.000001|1E-06)}}");
        }

        public class AreaSerializeTestData : QuantitySerializeTestData<Area>
        {
            public AreaSerializeTestData(
                Area value,
                AreaJsonSerializationFormat format,
                LinearUnitJsonSerializationFormat unitFormat,
                string expectedJsonPattern)
                : base(value, unitFormat, expectedJsonPattern)
            {
                Format = format;
            }

            public AreaJsonSerializationFormat Format { get; set; }
        }

        [Fact]
        public void DeserializeAsSquareMetres_ShouldReturnValidResult()
        {
            // arrange
            string json = @"{
  ""SquareMetres"": 123.456
}";
            var converter = new AreaJsonConverter();
            var unitConverter = new AreaUnitJsonConverter();
            var expectedResult =
                new Area(squareMetres: (number)123.456m);

            // act
            var result = Deserialize<Area>(json, converter, unitConverter);

            // assert
            result.Should().Be(expectedResult);
        }

        [Fact]
        public void DeserializeAsSquareMetresWithPredefinedUnit_ShouldReturnValidResult()
        {
            // arrange
            string json = @"{
  ""SquareMetres"": 123.456,
  ""Unit"": ""mm\u00B2""
}";
            var converter = new AreaJsonConverter();
            var unitConverter = new AreaUnitJsonConverter();
            var expectedResult =
                new Area(squareMetres: (number)123.456m)
                    .Convert(AreaUnit.SquareMillimetre);

            // act
            var result = Deserialize<Area>(json, converter, unitConverter);

            // assert
            result.Should().Be(expectedResult);
        }

        [Fact]
        public void DeserializeAsSquareMetresWithFullySerializedUnit_ShouldReturnValidResult()
        {
            // arrange
            string json = @"{
  ""SquareMetres"": 123.456,
  ""Unit"": {
    ""Name"": ""square millimetre"",
    ""Abbreviation"": ""mm2"",
    ""ValueInSquareMetres"": 0.000001
  }
}";
            var converter = new AreaJsonConverter();
            var unitConverter = new AreaUnitJsonConverter();
            var expectedResult =
                new Area(squareMetres: (number)123.456m)
                    .Convert(new AreaUnit(
                        name: "square millimetre",
                        abbreviation: "mm2",
                        valueInSquareMetres: (number)0.000001));

            // act
            var result = Deserialize<Area>(json, converter, unitConverter);

            // assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(AreaJsonSerializationFormat.AsSquareMetres, LinearUnitJsonSerializationFormat.AlwaysFull)]
        [InlineData(AreaJsonSerializationFormat.AsSquareMetres, LinearUnitJsonSerializationFormat.PredefinedAsString)]
        [InlineData(AreaJsonSerializationFormat.AsSquareMetresWithUnit, LinearUnitJsonSerializationFormat.AlwaysFull)]
        [InlineData(AreaJsonSerializationFormat.AsSquareMetresWithUnit,
            LinearUnitJsonSerializationFormat.PredefinedAsString)]
        public void SerializeAndDeserializeWithSquareMetres_ShouldBeIdempotent(AreaJsonSerializationFormat format,
            LinearUnitJsonSerializationFormat unitFormat)
        {
            // arrange
            var area = Fixture.Create<Area>();
            var converter = new AreaJsonConverter(format);
            var unitConverter = new AreaUnitJsonConverter(unitFormat);

            // act
            string serializedArea1 = Serialize(area, converter, unitConverter);
            var deserializedArea1 = Deserialize<Area>(serializedArea1, converter, unitConverter);
            string serializedArea2 = Serialize(deserializedArea1, converter, unitConverter);
            var deserializedArea2 = Deserialize<Area>(serializedArea2, converter, unitConverter);

            // assert
            deserializedArea1.Should().Be(area);
            deserializedArea2.Should().Be(area);

            deserializedArea2.Should().Be(deserializedArea1);
            serializedArea2.Should().Be(serializedArea1);
        }

        [Fact]
        public void SerializeAndDeserializeWithoutSquareMetres_ShouldBeApproximatelyIdempotent()
        {
            // arrange
            var area = Fixture.Create<Area>();
            var converter = new AreaJsonConverter(AreaJsonSerializationFormat.AsValueWithUnit);
            var unitConverter = new AreaUnitJsonConverter();

            // act
            string serializedArea1 = Serialize(area, converter, unitConverter);
            var deserializedArea1 = Deserialize<Area>(serializedArea1, converter, unitConverter);
            string serializedArea2 = Serialize(deserializedArea1, converter, unitConverter);
            var deserializedArea2 = Deserialize<Area>(serializedArea2, converter, unitConverter);

            // assert
            deserializedArea1.SquareMetres.Should().BeApproximately(area.SquareMetres);
            deserializedArea2.SquareMetres.Should().BeApproximately(area.SquareMetres);

            deserializedArea2.Should().Be(deserializedArea1);
            serializedArea2.Should().Be(serializedArea1);
        }
    }
}