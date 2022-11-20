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

    public class VolumeJsonConverterTests : TestsBase
    {
        public VolumeJsonConverterTests(TestFixture testFixture) : base(testFixture) { }

        [Theory]
        [MemberData(nameof(GetTestData), typeof(VolumeJsonConverterTests), nameof(GetSerializeTestData))]
        public void Serialize_ShouldReturnValidJson(VolumeSerializeTestData testData)
        {
            // arrange
            var converter = new VolumeJsonConverter(testData.Format, testData.UnitFormat);

            // act
            string actualJson = JsonConvert.SerializeObject(testData.Quantity, converter);

            // assert
            actualJson.Should().MatchRegex(testData.ExpectedJsonPattern);
        }
        private static IEnumerable<VolumeSerializeTestData> GetSerializeTestData()
        {
            var testValue = new Volume((number)5000000m, VolumeUnit.CubicCentimetre);
            yield return new VolumeSerializeTestData(
                value: testValue,
                format: VolumeJsonSerializationFormat.AsCubicMetres,
                unitFormat: LinearUnitJsonSerializationFormat.PredefinedAsString,
                expectedJsonPattern: "{\"CubicMetres\":5(\\.0+)}");
            yield return new VolumeSerializeTestData(
                value: testValue,
                format: VolumeJsonSerializationFormat.AsCubicMetresWithUnit,
                unitFormat: LinearUnitJsonSerializationFormat.PredefinedAsString,
                expectedJsonPattern: "{\"CubicMetres\":5(\\.0+),\"Unit\":\"cm³\"}");
            yield return new VolumeSerializeTestData(
                value: testValue,
                format: VolumeJsonSerializationFormat.AsValueWithUnit,
                unitFormat: LinearUnitJsonSerializationFormat.PredefinedAsString,
                expectedJsonPattern: "{\"Value\":5000000(\\.0+),\"Unit\":\"cm³\"}");
            yield return new VolumeSerializeTestData(
                value: testValue,
                format: VolumeJsonSerializationFormat.AsCubicMetresWithUnit,
                unitFormat: LinearUnitJsonSerializationFormat.AlwaysFull,
                expectedJsonPattern: "{\"CubicMetres\":5(\\.0+),\"Unit\":{\"Name\":\"cubic centimetre\",\"Abbreviation\":\"cm³\",\"ValueInCubicMetres\":(0\\.000001|1E-06)}}");
            yield return new VolumeSerializeTestData(
                value: testValue,
                format: VolumeJsonSerializationFormat.AsValueWithUnit,
                unitFormat: LinearUnitJsonSerializationFormat.AlwaysFull,
                expectedJsonPattern: "{\"Value\":5000000(\\.0+),\"Unit\":{\"Name\":\"cubic centimetre\",\"Abbreviation\":\"cm³\",\"ValueInCubicMetres\":(0\\.000001|1E-06)}}");
        }
        public class VolumeSerializeTestData : QuantitySerializeTestData<Volume>
        {
            public VolumeSerializeTestData(
                Volume value,
                VolumeJsonSerializationFormat format,
                LinearUnitJsonSerializationFormat unitFormat,
                string expectedJsonPattern)
                : base(value, unitFormat, expectedJsonPattern)
            {
                Format = format;
            }

            public VolumeJsonSerializationFormat Format { get; set; }
        }

        [Fact]
        public void DeserializeAsCubicMetres_ShouldReturnValidResult()
        {
            // arrange
            string json = @"{
  'cubicMetres': 123.456
}";
            var converter = new VolumeJsonConverter();
            var expectedResult =
                new Volume(cubicMetres: (number)123.456m);

            // act
            var result = JsonConvert.DeserializeObject<Volume>(json, converter);

            // assert
            result.Should().Be(expectedResult);
        }

        [Fact]
        public void DeserializeAsCubicMetresWithPredefinedUnit_ShouldReturnValidResult()
        {
            // arrange
            string json = @"{
  'cubicMetres': 123.456,
  'unit': 'cm³'
}";
            var converter = new VolumeJsonConverter();
            var expectedResult =
                new Volume(cubicMetres: (number)123.456m)
                .Convert(VolumeUnit.CubicMillimetre);

            // act
            var result = JsonConvert.DeserializeObject<Volume>(json, converter);

            // assert
            result.Should().Be(expectedResult);
        }

        [Fact]
        public void DeserializeAsCubicMetresWithFullySerializedUnit_ShouldReturnValidResult()
        {
            // arrange
            string json = @"{
  'cubicMetres': 123.456,
  'unit': {
    'name': 'cubic centimetre',
    'abbreviation': 'cm³',
    'valueInCubicMetres': '0.000001'
  }
}";
            var converter = new VolumeJsonConverter();
            var expectedResult =
                new Volume(cubicMetres: (number)123.456m)
                .Convert(new VolumeUnit(
                    name: "cubic millimetre",
                    abbreviation: "cm³",
                    valueInCubicMetres: (number)0.000001));

            // act
            var result = JsonConvert.DeserializeObject<Volume>(json, converter);

            // assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(VolumeJsonSerializationFormat.AsCubicMetres, LinearUnitJsonSerializationFormat.AlwaysFull)]
        [InlineData(VolumeJsonSerializationFormat.AsCubicMetres, LinearUnitJsonSerializationFormat.PredefinedAsString)]
        [InlineData(VolumeJsonSerializationFormat.AsCubicMetresWithUnit, LinearUnitJsonSerializationFormat.AlwaysFull)]
        [InlineData(VolumeJsonSerializationFormat.AsCubicMetresWithUnit, LinearUnitJsonSerializationFormat.PredefinedAsString)]
        public void SerializeAndDeserializeWithCubicMetres_ShouldBeIdempotent(VolumeJsonSerializationFormat format, LinearUnitJsonSerializationFormat unitFormat)
        {
            // arrange
            var volume = Fixture.Create<Volume>();
            var converter = new VolumeJsonConverter(format, unitFormat);

            // act
            string serializedVolume1 = JsonConvert.SerializeObject(volume, converter);
            var deserializedVolume1 = JsonConvert.DeserializeObject<Volume>(serializedVolume1, converter);
            string serializedVolume2 = JsonConvert.SerializeObject(deserializedVolume1, converter);
            var deserializedVolume2 = JsonConvert.DeserializeObject<Volume>(serializedVolume2, converter);

            // assert
            deserializedVolume1.Should().Be(volume);
            deserializedVolume2.Should().Be(volume);

            deserializedVolume2.Should().Be(deserializedVolume1);
            serializedVolume2.Should().Be(serializedVolume1);
        }

        [Fact]
        public void SerializeAndDeserializeWithoutCubicMetres_ShouldBeApproximatelyIdempotent()
        {
            // arrange
            var volume = Fixture.Create<Volume>();
            var converter = new VolumeJsonConverter(VolumeJsonSerializationFormat.AsValueWithUnit);

            // act
            string serializedVolume1 = JsonConvert.SerializeObject(volume, converter);
            var deserializedVolume1 = JsonConvert.DeserializeObject<Volume>(serializedVolume1, converter);
            string serializedVolume2 = JsonConvert.SerializeObject(deserializedVolume1, converter);
            var deserializedVolume2 = JsonConvert.DeserializeObject<Volume>(serializedVolume2, converter);

            // assert
            deserializedVolume1.CubicMetres.Should().BeApproximately(volume.CubicMetres);
            deserializedVolume2.CubicMetres.Should().BeApproximately(volume.CubicMetres);

            deserializedVolume2.Should().Be(deserializedVolume1);
            serializedVolume2.Should().Be(serializedVolume1);
        }
    }
}
