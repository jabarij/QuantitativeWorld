using AutoFixture;
using FluentAssertions;
using Newtonsoft.Json;
using System.Collections.Generic;
using Xunit;

#if DECIMAL
namespace DecimalQuantitativeWorld.Json.Newtonsoft.Tests
{
    using DecimalQuantitativeWorld.TestAbstractions;
    using number = System.Decimal;
#else
namespace QuantitativeWorld.Json.Newtonsoft.Tests
{
    using QuantitativeWorld.TestAbstractions;
    using number = System.Double;
#endif

    public class AreaJsonConverterTests : TestsBase
    {
        public AreaJsonConverterTests(TestFixture testFixture) : base(testFixture) { }

        [Theory]
        [MemberData(nameof(GetTestData), typeof(AreaJsonConverterTests), nameof(GetSerializeTestData))]
        public void Serialize_ShouldReturnValidJson(AreaSerializeTestData testData)
        {
            // arrange
            var converter = new AreaJsonConverter(testData.Format, testData.UnitFormat);

            // act
            string actualJson = JsonConvert.SerializeObject(testData.Quantity, converter);

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
                expectedJsonPattern: "{\"SquareMetres\":5(\\.0+)}");
            yield return new AreaSerializeTestData(
                value: testValue,
                format: AreaJsonSerializationFormat.AsSquareMetresWithUnit,
                unitFormat: LinearUnitJsonSerializationFormat.PredefinedAsString,
                expectedJsonPattern: "{\"SquareMetres\":5(\\.0+),\"Unit\":\"mm²\"}");
            yield return new AreaSerializeTestData(
                value: testValue,
                format: AreaJsonSerializationFormat.AsValueWithUnit,
                unitFormat: LinearUnitJsonSerializationFormat.PredefinedAsString,
                expectedJsonPattern: "{\"Value\":5000000(\\.0+),\"Unit\":\"mm²\"}");
            yield return new AreaSerializeTestData(
                value: testValue,
                format: AreaJsonSerializationFormat.AsSquareMetresWithUnit,
                unitFormat: LinearUnitJsonSerializationFormat.AlwaysFull,
                expectedJsonPattern: "{\"SquareMetres\":5(\\.0+),\"Unit\":{\"Name\":\"square millimetre\",\"Abbreviation\":\"mm²\",\"ValueInSquareMetres\":(0\\.000001|1E-06)}}");
            yield return new AreaSerializeTestData(
                value: testValue,
                format: AreaJsonSerializationFormat.AsValueWithUnit,
                unitFormat: LinearUnitJsonSerializationFormat.AlwaysFull,
                expectedJsonPattern: "{\"Value\":5000000(\\.0+),\"Unit\":{\"Name\":\"square millimetre\",\"Abbreviation\":\"mm²\",\"ValueInSquareMetres\":(0\\.000001|1E-06)}}");
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
  'squareMetres': 123.456
}";
            var converter = new AreaJsonConverter();
            var expectedResult =
                new Area(squareMetres: (number)123.456m);

            // act
            var result = JsonConvert.DeserializeObject<Area>(json, converter);

            // assert
            result.Should().Be(expectedResult);
        }

        [Fact]
        public void DeserializeAsSquareMetresWithPredefinedUnit_ShouldReturnValidResult()
        {
            // arrange
            string json = @"{
  'squareMetres': 123.456,
  'unit': 'mm²'
}";
            var converter = new AreaJsonConverter();
            var expectedResult =
                new Area(squareMetres: (number)123.456m)
                .Convert(AreaUnit.SquareMillimetre);

            // act
            var result = JsonConvert.DeserializeObject<Area>(json, converter);

            // assert
            result.Should().Be(expectedResult);
        }

        [Fact]
        public void DeserializeAsSquareMetresWithFullySerializedUnit_ShouldReturnValidResult()
        {
            // arrange
            string json = @"{
  'squareMetres': 123.456,
  'unit': {
    'name': 'square millimetre',
    'abbreviation': 'mm²',
    'valueInSquareMetres': '0.000001'
  }
}";
            var converter = new AreaJsonConverter();
            var expectedResult =
                new Area(squareMetres: (number)123.456m)
                .Convert(new AreaUnit(
                    name: "square millimetre",
                    abbreviation: "mm²",
                    valueInSquareMetres: (number)0.000001));

            // act
            var result = JsonConvert.DeserializeObject<Area>(json, converter);

            // assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(AreaJsonSerializationFormat.AsSquareMetres, LinearUnitJsonSerializationFormat.AlwaysFull)]
        [InlineData(AreaJsonSerializationFormat.AsSquareMetres, LinearUnitJsonSerializationFormat.PredefinedAsString)]
        [InlineData(AreaJsonSerializationFormat.AsSquareMetresWithUnit, LinearUnitJsonSerializationFormat.AlwaysFull)]
        [InlineData(AreaJsonSerializationFormat.AsSquareMetresWithUnit, LinearUnitJsonSerializationFormat.PredefinedAsString)]
        public void SerializeAndDeserializeWithSquareMetres_ShouldBeIdempotent(AreaJsonSerializationFormat format, LinearUnitJsonSerializationFormat unitFormat)
        {
            // arrange
            var area = Fixture.Create<Area>();
            var converter = new AreaJsonConverter(format, unitFormat);

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
        public void SerializeAndDeserializeWithoutSquareMetres_ShouldBeApproximatelyIdempotent()
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
            deserializedArea1.SquareMetres.Should().BeApproximately(area.SquareMetres);
            deserializedArea2.SquareMetres.Should().BeApproximately(area.SquareMetres);

            deserializedArea2.Should().Be(deserializedArea1);
            serializedArea2.Should().Be(serializedArea1);
        }
    }
}
