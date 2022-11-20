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

    public class WeightJsonConverterTests : TestsBase
    {
        public WeightJsonConverterTests(TestFixture testFixture) : base(testFixture) { }

        [Theory]
        [MemberData(nameof(GetTestData), typeof(WeightJsonConverterTests), nameof(GetSerializeTestData))]
        public void Serialize_ShouldReturnValidJson(WeightSerializeTestData testData)
        {
            // arrange
            var converter = new WeightJsonConverter(testData.Format, testData.UnitFormat);

            // act
            string actualJson = JsonConvert.SerializeObject(testData.Quantity, converter);

            // assert
            actualJson.Should().MatchRegex(testData.ExpectedJsonPattern);
        }
        private static IEnumerable<WeightSerializeTestData> GetSerializeTestData()
        {
            var testValue = new Weight((number)5000m, WeightUnit.Gram);
            yield return new WeightSerializeTestData(
                value: testValue,
                format: WeightJsonSerializationFormat.AsKilograms,
                unitFormat: LinearUnitJsonSerializationFormat.PredefinedAsString,
                expectedJsonPattern: "{\"Kilograms\":5(\\.0+)}");
            yield return new WeightSerializeTestData(
                value: testValue,
                format: WeightJsonSerializationFormat.AsKilogramsWithUnit,
                unitFormat: LinearUnitJsonSerializationFormat.PredefinedAsString,
                expectedJsonPattern: "{\"Kilograms\":5(\\.0+),\"Unit\":\"g\"}");
            yield return new WeightSerializeTestData(
                value: testValue,
                format: WeightJsonSerializationFormat.AsValueWithUnit,
                unitFormat: LinearUnitJsonSerializationFormat.PredefinedAsString,
                expectedJsonPattern: "{\"Value\":5000(\\.0+),\"Unit\":\"g\"}");
            yield return new WeightSerializeTestData(
                value: testValue,
                format: WeightJsonSerializationFormat.AsKilogramsWithUnit,
                unitFormat: LinearUnitJsonSerializationFormat.AlwaysFull,
                expectedJsonPattern: "{\"Kilograms\":5(\\.0+),\"Unit\":{\"Name\":\"gram\",\"Abbreviation\":\"g\",\"ValueInKilograms\":0.001}}");
            yield return new WeightSerializeTestData(
                value: testValue,
                format: WeightJsonSerializationFormat.AsValueWithUnit,
                unitFormat: LinearUnitJsonSerializationFormat.AlwaysFull,
                expectedJsonPattern: "{\"Value\":5000(\\.0+),\"Unit\":{\"Name\":\"gram\",\"Abbreviation\":\"g\",\"ValueInKilograms\":0.001}}");
        }
        public class WeightSerializeTestData : QuantitySerializeTestData<Weight>
        {
            public WeightSerializeTestData(
                Weight value,
                WeightJsonSerializationFormat format,
                LinearUnitJsonSerializationFormat unitFormat,
                string expectedJsonPattern)
                : base(value, unitFormat, expectedJsonPattern)
            {
                Format = format;
            }

            public WeightJsonSerializationFormat Format { get; set; }
        }

        [Fact]
        public void DeserializeAsKilograms_ShouldReturnValidResult()
        {
            // arrange
            string json = @"{
  'kilograms': 123.456
}";
            var converter = new WeightJsonConverter();
            var expectedResult =
                new Weight(kilograms: (number)123.456m);

            // act
            var result = JsonConvert.DeserializeObject<Weight>(json, converter);

            // assert
            result.Should().Be(expectedResult);
        }

        [Fact]
        public void DeserializeAsKilogramsWithPredefinedUnit_ShouldReturnValidResult()
        {
            // arrange
            string json = @"{
  'kilograms': 123.456,
  'unit': 'g'
}";
            var converter = new WeightJsonConverter();
            var expectedResult =
                new Weight(kilograms: (number)123.456m)
                .Convert(WeightUnit.Gram);

            // act
            var result = JsonConvert.DeserializeObject<Weight>(json, converter);

            // assert
            result.Should().Be(expectedResult);
        }

        [Fact]
        public void DeserializeAsKilogramsWithFullySerializedUnit_ShouldReturnValidResult()
        {
            // arrange
            string json = @"{
  'kilograms': 123.456,
  'unit': {
    'name': 'gram',
    'abbreviation': 'g',
    'valueInKilograms': '0.001'
  }
}";
            var converter = new WeightJsonConverter();
            var expectedResult =
                new Weight(kilograms: (number)123.456m)
                .Convert(new WeightUnit(
                    name: "gram",
                    abbreviation: "g",
                    valueInKilograms: (number)0.001m));

            // act
            var result = JsonConvert.DeserializeObject<Weight>(json, converter);

            // assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(WeightJsonSerializationFormat.AsKilograms, LinearUnitJsonSerializationFormat.AlwaysFull)]
        [InlineData(WeightJsonSerializationFormat.AsKilograms, LinearUnitJsonSerializationFormat.PredefinedAsString)]
        [InlineData(WeightJsonSerializationFormat.AsKilogramsWithUnit, LinearUnitJsonSerializationFormat.AlwaysFull)]
        [InlineData(WeightJsonSerializationFormat.AsKilogramsWithUnit, LinearUnitJsonSerializationFormat.PredefinedAsString)]
        public void SerializeAndDeserializeWithKilograms_ShouldBeIdempotent(WeightJsonSerializationFormat format, LinearUnitJsonSerializationFormat unitFormat)
        {
            // arrange
            var weight = Fixture.Create<Weight>();
            var converter = new WeightJsonConverter(format, unitFormat);

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
            deserializedWeight1.Kilograms.Should().BeApproximately(weight.Kilograms);
            deserializedWeight2.Kilograms.Should().BeApproximately(weight.Kilograms);

            deserializedWeight2.Should().Be(deserializedWeight1);
            serializedWeight2.Should().Be(serializedWeight1);
        }
    }
}
