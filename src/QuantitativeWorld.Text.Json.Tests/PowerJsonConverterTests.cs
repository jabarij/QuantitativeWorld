using AutoFixture;
using FluentAssertions;
using Newtonsoft.Json;
using QuantitativeWorld.TestAbstractions;
using System.Collections.Generic;
using Xunit;

namespace QuantitativeWorld.Text.Json.Tests
{
#if DECIMAL
    using number = System.Decimal;
#else
    using number = System.Double;
    using Constants = QuantitativeWorld.DoubleConstants;
#endif

    public class PowerJsonConverterTests : TestsBase
    {
        public PowerJsonConverterTests(TestFixture testFixture) : base(testFixture) { }

        [Theory]
        [MemberData(nameof(GetTestData), typeof(PowerJsonConverterTests), nameof(GetSerializeTestData))]
        public void Serialize_ShouldReturnValidJson(PowerSerializeTestData testData)
        {
            // arrange
            var converter = new PowerJsonConverter(testData.Format, testData.UnitFormat);

            // act
            string actualJson = JsonConvert.SerializeObject(testData.Quantity, converter);

            // assert
            actualJson.Should().MatchRegex(testData.ExpectedJsonPattern);
        }
        private static IEnumerable<PowerSerializeTestData> GetSerializeTestData()
        {
            var testValue = new Power((number)5m, PowerUnit.Kilowatt);
            yield return new PowerSerializeTestData(
                value: testValue,
                format: PowerJsonSerializationFormat.AsWatts,
                unitFormat: LinearUnitJsonSerializationFormat.PredefinedAsString,
                expectedJsonPattern: "{\"Watts\":5000(\\.0+)}");
            yield return new PowerSerializeTestData(
                value: testValue,
                format: PowerJsonSerializationFormat.AsWattsWithUnit,
                unitFormat: LinearUnitJsonSerializationFormat.PredefinedAsString,
                expectedJsonPattern: "{\"Watts\":5000(\\.0+),\"Unit\":\"kW\"}");
            yield return new PowerSerializeTestData(
                value: testValue,
                format: PowerJsonSerializationFormat.AsValueWithUnit,
                unitFormat: LinearUnitJsonSerializationFormat.PredefinedAsString,
                expectedJsonPattern: "{\"Value\":5(\\.0+),\"Unit\":\"kW\"}");
            yield return new PowerSerializeTestData(
                value: testValue,
                format: PowerJsonSerializationFormat.AsWattsWithUnit,
                unitFormat: LinearUnitJsonSerializationFormat.AlwaysFull,
                expectedJsonPattern: "{\"Watts\":5000(\\.0+),\"Unit\":{\"Name\":\"kilowatt\",\"Abbreviation\":\"kW\",\"ValueInWatts\":1000(\\.0+)}}");
            yield return new PowerSerializeTestData(
                value: testValue,
                format: PowerJsonSerializationFormat.AsValueWithUnit,
                unitFormat: LinearUnitJsonSerializationFormat.AlwaysFull,
                expectedJsonPattern: "{\"Value\":5(\\.0+),\"Unit\":{\"Name\":\"kilowatt\",\"Abbreviation\":\"kW\",\"ValueInWatts\":1000(\\.0+)}}");
        }
        public class PowerSerializeTestData : QuantitySerializeTestData<Power>
        {
            public PowerSerializeTestData(
                Power value,
                PowerJsonSerializationFormat format,
                LinearUnitJsonSerializationFormat unitFormat,
                string expectedJsonPattern)
                : base(value, unitFormat, expectedJsonPattern)
            {
                Format = format;
            }

            public PowerJsonSerializationFormat Format { get; set; }
        }

        [Fact]
        public void DeserializeAsWatts_ShouldReturnValidResult()
        {
            // arrange
            string json = @"{
  'watts': 123.456
}";
            var converter = new PowerJsonConverter();
            var expectedResult =
                new Power(watts: (number)123.456m);

            // act
            var result = JsonConvert.DeserializeObject<Power>(json, converter);

            // assert
            result.Should().Be(expectedResult);
        }

        [Fact]
        public void DeserializeAsWattsWithPredefinedUnit_ShouldReturnValidResult()
        {
            // arrange
            string json = @"{
  'watts': 123.456,
  'unit': 'kW'
}";
            var converter = new PowerJsonConverter();
            var expectedResult =
                new Power(watts: (number)123.456m)
                .Convert(PowerUnit.Kilowatt);

            // act
            var result = JsonConvert.DeserializeObject<Power>(json, converter);

            // assert
            result.Should().Be(expectedResult);
        }

        [Fact]
        public void DeserializeAsWattsWithFullySerializedUnit_ShouldReturnValidResult()
        {
            // arrange
            string json = @"{
  'watts': 123.456,
  'unit': {
    'name': 'kilowatt',
    'abbreviation': 'kW',
    'valueInWatts': '1000'
  }
}";
            var converter = new PowerJsonConverter();
            var expectedResult =
                new Power(watts: (number)123.456m)
                .Convert(new PowerUnit(
                    name: "kilowatt",
                    abbreviation: "kW",
                    valueInWatts: (number)1000));

            // act
            var result = JsonConvert.DeserializeObject<Power>(json, converter);

            // assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(PowerJsonSerializationFormat.AsWatts, LinearUnitJsonSerializationFormat.AlwaysFull)]
        [InlineData(PowerJsonSerializationFormat.AsWatts, LinearUnitJsonSerializationFormat.PredefinedAsString)]
        [InlineData(PowerJsonSerializationFormat.AsWattsWithUnit, LinearUnitJsonSerializationFormat.AlwaysFull)]
        [InlineData(PowerJsonSerializationFormat.AsWattsWithUnit, LinearUnitJsonSerializationFormat.PredefinedAsString)]
        public void SerializeAndDeserializeWithWatts_ShouldBeIdempotent(PowerJsonSerializationFormat format, LinearUnitJsonSerializationFormat unitFormat)
        {
            // arrange
            var power = Fixture.Create<Power>();
            var converter = new PowerJsonConverter(format, unitFormat);

            // act
            string serializedPower1 = JsonConvert.SerializeObject(power, converter);
            var deserializedPower1 = JsonConvert.DeserializeObject<Power>(serializedPower1, converter);
            string serializedPower2 = JsonConvert.SerializeObject(deserializedPower1, converter);
            var deserializedPower2 = JsonConvert.DeserializeObject<Power>(serializedPower2, converter);

            // assert
            deserializedPower1.Should().Be(power);
            deserializedPower2.Should().Be(power);

            deserializedPower2.Should().Be(deserializedPower1);
            serializedPower2.Should().Be(serializedPower1);
        }

        [Fact]
        public void SerializeAndDeserializeWithoutWatts_ShouldBeApproximatelyIdempotent()
        {
            // arrange
            var power = Fixture.Create<Power>();
            var converter = new PowerJsonConverter(PowerJsonSerializationFormat.AsValueWithUnit);

            // act
            string serializedPower1 = JsonConvert.SerializeObject(power, converter);
            var deserializedPower1 = JsonConvert.DeserializeObject<Power>(serializedPower1, converter);
            string serializedPower2 = JsonConvert.SerializeObject(deserializedPower1, converter);
            var deserializedPower2 = JsonConvert.DeserializeObject<Power>(serializedPower2, converter);

            // assert
            deserializedPower1.Watts.Should().BeApproximately(power.Watts);
            deserializedPower2.Watts.Should().BeApproximately(power.Watts);

            deserializedPower2.Should().Be(deserializedPower1);
            serializedPower2.Should().Be(serializedPower1);
        }
    }
}
