using AutoFixture;
using FluentAssertions;
using Newtonsoft.Json;
using System.Collections.Generic;
using Xunit;

#if DECIMAL
namespace DecimalQuantitativeWorld.Text.Json.Tests
{
    using DecimalQuantitativeWorld.TestAbstractions;
    using number = System.Decimal;
#else
namespace QuantitativeWorld.Text.Json.Tests
{
    using QuantitativeWorld.TestAbstractions;
    using number = System.Double;
#endif

    public class EnergyJsonConverterTests : TestsBase
    {
        public EnergyJsonConverterTests(TestFixture testFixture) : base(testFixture) { }

        [Theory]
        [MemberData(nameof(GetTestData), typeof(EnergyJsonConverterTests), nameof(GetSerializeTestData))]
        public void Serialize_ShouldReturnValidJson(EnergySerializeTestData testData)
        {
            // arrange
            var converter = new EnergyJsonConverter(testData.Format, testData.UnitFormat);

            // act
            string actualJson = JsonConvert.SerializeObject(testData.Quantity, converter);

            // assert
            actualJson.Should().MatchRegex(testData.ExpectedJsonPattern);
        }
        private static IEnumerable<EnergySerializeTestData> GetSerializeTestData()
        {
            var testValue = new Energy((number)5m, EnergyUnit.Kilojoule);
            yield return new EnergySerializeTestData(
                value: testValue,
                format: EnergyJsonSerializationFormat.AsJoules,
                unitFormat: LinearUnitJsonSerializationFormat.PredefinedAsString,
                expectedJsonPattern: "{\"Joules\":5000(\\.0+)}");
            yield return new EnergySerializeTestData(
                value: testValue,
                format: EnergyJsonSerializationFormat.AsJoulesWithUnit,
                unitFormat: LinearUnitJsonSerializationFormat.PredefinedAsString,
                expectedJsonPattern: "{\"Joules\":5000(\\.0+),\"Unit\":\"kJ\"}");
            yield return new EnergySerializeTestData(
                value: testValue,
                format: EnergyJsonSerializationFormat.AsValueWithUnit,
                unitFormat: LinearUnitJsonSerializationFormat.PredefinedAsString,
                expectedJsonPattern: "{\"Value\":5(\\.0+),\"Unit\":\"kJ\"}");
            yield return new EnergySerializeTestData(
                value: testValue,
                format: EnergyJsonSerializationFormat.AsJoulesWithUnit,
                unitFormat: LinearUnitJsonSerializationFormat.AlwaysFull,
                expectedJsonPattern: "{\"Joules\":5000(\\.0+),\"Unit\":{\"Name\":\"kilojoule\",\"Abbreviation\":\"kJ\",\"ValueInJoules\":1000(\\.0+)}}");
            yield return new EnergySerializeTestData(
                value: testValue,
                format: EnergyJsonSerializationFormat.AsValueWithUnit,
                unitFormat: LinearUnitJsonSerializationFormat.AlwaysFull,
                expectedJsonPattern: "{\"Value\":5(\\.0+),\"Unit\":{\"Name\":\"kilojoule\",\"Abbreviation\":\"kJ\",\"ValueInJoules\":1000(\\.0+)}}");
        }
        public class EnergySerializeTestData : QuantitySerializeTestData<Energy>
        {
            public EnergySerializeTestData(
                Energy value,
                EnergyJsonSerializationFormat format,
                LinearUnitJsonSerializationFormat unitFormat,
                string expectedJsonPattern)
                : base(value, unitFormat, expectedJsonPattern)
            {
                Format = format;
            }

            public EnergyJsonSerializationFormat Format { get; set; }
        }

        [Fact]
        public void DeserializeAsJoules_ShouldReturnValidResult()
        {
            // arrange
            string json = @"{
  'joules': 123.456
}";
            var converter = new EnergyJsonConverter();
            var expectedResult =
                new Energy(joules: (number)123.456m);

            // act
            var result = JsonConvert.DeserializeObject<Energy>(json, converter);

            // assert
            result.Should().Be(expectedResult);
        }

        [Fact]
        public void DeserializeAsJoulesWithPredefinedUnit_ShouldReturnValidResult()
        {
            // arrange
            string json = @"{
  'joules': 123.456,
  'unit': 'kJ'
}";
            var converter = new EnergyJsonConverter();
            var expectedResult =
                new Energy(joules: (number)123.456m)
                .Convert(EnergyUnit.Kilojoule);

            // act
            var result = JsonConvert.DeserializeObject<Energy>(json, converter);

            // assert
            result.Should().Be(expectedResult);
        }

        [Fact]
        public void DeserializeAsJoulesWithFullySerializedUnit_ShouldReturnValidResult()
        {
            // arrange
            string json = @"{
  'joules': 123.456,
  'unit': {
    'name': 'kilojoule',
    'abbreviation': 'kJ',
    'valueInJoules': '1000'
  }
}";
            var converter = new EnergyJsonConverter();
            var expectedResult =
                new Energy(joules: (number)123.456m)
                .Convert(new EnergyUnit(
                    name: "kilojoule",
                    abbreviation: "kJ",
                    valueInJoules: (number)1000));

            // act
            var result = JsonConvert.DeserializeObject<Energy>(json, converter);

            // assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(EnergyJsonSerializationFormat.AsJoules, LinearUnitJsonSerializationFormat.AlwaysFull)]
        [InlineData(EnergyJsonSerializationFormat.AsJoules, LinearUnitJsonSerializationFormat.PredefinedAsString)]
        [InlineData(EnergyJsonSerializationFormat.AsJoulesWithUnit, LinearUnitJsonSerializationFormat.AlwaysFull)]
        [InlineData(EnergyJsonSerializationFormat.AsJoulesWithUnit, LinearUnitJsonSerializationFormat.PredefinedAsString)]
        public void SerializeAndDeserializeWithJoules_ShouldBeIdempotent(EnergyJsonSerializationFormat format, LinearUnitJsonSerializationFormat unitFormat)
        {
            // arrange
            var energy = Fixture.Create<Energy>();
            var converter = new EnergyJsonConverter(format, unitFormat);

            // act
            string serializedEnergy1 = JsonConvert.SerializeObject(energy, converter);
            var deserializedEnergy1 = JsonConvert.DeserializeObject<Energy>(serializedEnergy1, converter);
            string serializedEnergy2 = JsonConvert.SerializeObject(deserializedEnergy1, converter);
            var deserializedEnergy2 = JsonConvert.DeserializeObject<Energy>(serializedEnergy2, converter);

            // assert
            deserializedEnergy1.Should().Be(energy);
            deserializedEnergy2.Should().Be(energy);

            deserializedEnergy2.Should().Be(deserializedEnergy1);
            serializedEnergy2.Should().Be(serializedEnergy1);
        }

        [Fact]
        public void SerializeAndDeserializeWithoutJoules_ShouldBeApproximatelyIdempotent()
        {
            // arrange
            var energy = Fixture.Create<Energy>();
            var converter = new EnergyJsonConverter(EnergyJsonSerializationFormat.AsValueWithUnit);

            // act
            string serializedEnergy1 = JsonConvert.SerializeObject(energy, converter);
            var deserializedEnergy1 = JsonConvert.DeserializeObject<Energy>(serializedEnergy1, converter);
            string serializedEnergy2 = JsonConvert.SerializeObject(deserializedEnergy1, converter);
            var deserializedEnergy2 = JsonConvert.DeserializeObject<Energy>(serializedEnergy2, converter);

            // assert
            deserializedEnergy1.Joules.Should().BeApproximately(energy.Joules);
            deserializedEnergy2.Joules.Should().BeApproximately(energy.Joules);

            deserializedEnergy2.Should().Be(deserializedEnergy1);
            serializedEnergy2.Should().Be(serializedEnergy1);
        }
    }
}
