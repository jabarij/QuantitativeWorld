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

    public class SpeedJsonConverterTests : TestsBase
    {
        public SpeedJsonConverterTests(TestFixture testFixture) : base(testFixture) { }

        [Theory]
        [MemberData(nameof(GetTestData), typeof(SpeedJsonConverterTests), nameof(GetSerializeTestData))]
        public void Serialize_ShouldReturnValidJson(SpeedSerializeTestData testData)
        {
            // arrange
            var converter = new SpeedJsonConverter(testData.Format, testData.UnitFormat);

            // act
            string actualJson = JsonConvert.SerializeObject(testData.Quantity, converter);

            // assert
            actualJson.Should().MatchRegex(testData.ExpectedJsonPattern);
        }
        private static IEnumerable<SpeedSerializeTestData> GetSerializeTestData()
        {
            var testValue = new Speed((number)50m, SpeedUnit.KilometrePerHour);
            yield return new SpeedSerializeTestData(
                value: testValue,
                format: SpeedJsonSerializationFormat.AsMetresPerSecond,
                unitFormat: LinearUnitJsonSerializationFormat.PredefinedAsString,
                expectedJsonPattern: "{\"MetresPerSecond\":13\\.8888888888888+\\d*}");
            yield return new SpeedSerializeTestData(
                value: testValue,
                format: SpeedJsonSerializationFormat.AsMetresPerSecondWithUnit,
                unitFormat: LinearUnitJsonSerializationFormat.PredefinedAsString,
                expectedJsonPattern: "{\"MetresPerSecond\":13\\.8888888888888+\\d*,\"Unit\":\"km/h\"}");
            yield return new SpeedSerializeTestData(
                value: testValue,
                format: SpeedJsonSerializationFormat.AsValueWithUnit,
                unitFormat: LinearUnitJsonSerializationFormat.PredefinedAsString,
                expectedJsonPattern: "{\"Value\":50(\\.0+),\"Unit\":\"km/h\"}");
            yield return new SpeedSerializeTestData(
                value: testValue,
                format: SpeedJsonSerializationFormat.AsMetresPerSecondWithUnit,
                unitFormat: LinearUnitJsonSerializationFormat.AlwaysFull,
                expectedJsonPattern: "{\"MetresPerSecond\":13\\.8888888888888+\\d*,\"Unit\":{\"Name\":\"kilometre per hour\",\"Abbreviation\":\"km/h\",\"ValueInMetresPerSecond\":0\\.277777777777777+\\d*}}");
            yield return new SpeedSerializeTestData(
                value: testValue,
                format: SpeedJsonSerializationFormat.AsValueWithUnit,
                unitFormat: LinearUnitJsonSerializationFormat.AlwaysFull,
                expectedJsonPattern: "{\"Value\":50(\\.0+),\"Unit\":{\"Name\":\"kilometre per hour\",\"Abbreviation\":\"km/h\",\"ValueInMetresPerSecond\":0\\.277777777777777+\\d*}}");
        }
        public class SpeedSerializeTestData : QuantitySerializeTestData<Speed>
        {
            public SpeedSerializeTestData(
                Speed value,
                SpeedJsonSerializationFormat format,
                LinearUnitJsonSerializationFormat unitFormat,
                string expectedJsonPattern)
                : base(value, unitFormat, expectedJsonPattern)
            {
                Format = format;
            }

            public SpeedJsonSerializationFormat Format { get; set; }
        }

        [Fact]
        public void DeserializeAsMetresPerSecond_ShouldReturnValidResult()
        {
            // arrange
            string json = @"{
  'metresPerSecond': 123.456
}";
            var converter = new SpeedJsonConverter();
            var expectedResult =
                new Speed(metresPerSecond: (number)123.456m);

            // act
            var result = JsonConvert.DeserializeObject<Speed>(json, converter);

            // assert
            result.Should().Be(expectedResult);
        }

        [Fact]
        public void DeserializeAsMetresPerSecondWithPredefinedUnit_ShouldReturnValidResult()
        {
            // arrange
            string json = @"{
  'metresPerSecond': 123.456,
  'unit': 'km/h'
}";
            var converter = new SpeedJsonConverter();
            var expectedResult =
                new Speed(metresPerSecond: (number)123.456m)
                .Convert(SpeedUnit.KilometrePerHour);

            // act
            var result = JsonConvert.DeserializeObject<Speed>(json, converter);

            // assert
            result.Should().Be(expectedResult);
        }

        [Fact]
        public void DeserializeAsMetresPerSecondWithFullySerializedUnit_ShouldReturnValidResult()
        {
            // arrange
            string json = @"{
  'metresPerSecond': 123.456,
  'unit': {
    'name': 'kilometre per hour',
    'abbreviation': 'km/h',
    'valueInMetresPerSecond': '0.27777777777777779'
  }
}";
            var converter = new SpeedJsonConverter();
            var expectedResult =
                new Speed(metresPerSecond: (number)123.456m)
                .Convert(new SpeedUnit(
                    name: "kilometre per hour",
                    abbreviation: "km/h",
                    valueInMetresPerSecond: (number)0.27777777777777779m));

            // act
            var result = JsonConvert.DeserializeObject<Speed>(json, converter);

            // assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(SpeedJsonSerializationFormat.AsMetresPerSecond, LinearUnitJsonSerializationFormat.AlwaysFull)]
        [InlineData(SpeedJsonSerializationFormat.AsMetresPerSecond, LinearUnitJsonSerializationFormat.PredefinedAsString)]
        [InlineData(SpeedJsonSerializationFormat.AsMetresPerSecondWithUnit, LinearUnitJsonSerializationFormat.AlwaysFull)]
        [InlineData(SpeedJsonSerializationFormat.AsMetresPerSecondWithUnit, LinearUnitJsonSerializationFormat.PredefinedAsString)]
        public void SerializeAndDeserializeWithMetresPerSecond_ShouldBeIdempotent(SpeedJsonSerializationFormat format, LinearUnitJsonSerializationFormat unitFormat)
        {
            // arrange
            var speed = Fixture.Create<Speed>();
            var converter = new SpeedJsonConverter(format, unitFormat);

            // act
            string serializedSpeed1 = JsonConvert.SerializeObject(speed, converter);
            var deserializedSpeed1 = JsonConvert.DeserializeObject<Speed>(serializedSpeed1, converter);
            string serializedSpeed2 = JsonConvert.SerializeObject(deserializedSpeed1, converter);
            var deserializedSpeed2 = JsonConvert.DeserializeObject<Speed>(serializedSpeed2, converter);

            // assert
            deserializedSpeed1.Should().Be(speed);
            deserializedSpeed2.Should().Be(speed);

            deserializedSpeed2.Should().Be(deserializedSpeed1);
            serializedSpeed2.Should().Be(serializedSpeed1);
        }

        [Fact]
        public void SerializeAndDeserializeWithoutMetresPerSecond_ShouldBeApproximatelyIdempotent()
        {
            // arrange
            var speed = Fixture.Create<Speed>();
            var converter = new SpeedJsonConverter(SpeedJsonSerializationFormat.AsValueWithUnit);

            // act
            string serializedSpeed1 = JsonConvert.SerializeObject(speed, converter);
            var deserializedSpeed1 = JsonConvert.DeserializeObject<Speed>(serializedSpeed1, converter);
            string serializedSpeed2 = JsonConvert.SerializeObject(deserializedSpeed1, converter);
            var deserializedSpeed2 = JsonConvert.DeserializeObject<Speed>(serializedSpeed2, converter);

            // assert
            deserializedSpeed1.MetresPerSecond.Should().BeApproximately(speed.MetresPerSecond);
            deserializedSpeed2.MetresPerSecond.Should().BeApproximately(speed.MetresPerSecond);

            deserializedSpeed2.Should().Be(deserializedSpeed1);
            serializedSpeed2.Should().Be(serializedSpeed1);
        }
    }
}
