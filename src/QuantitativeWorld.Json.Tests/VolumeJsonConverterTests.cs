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

    public class VolumeJsonConverterTests : TestsBase
    {
        public VolumeJsonConverterTests(TestFixture testFixture) : base(testFixture)
        {
        }

        protected override void Configure(JsonSerializerOptions options)
        {
            base.Configure(options);
            options.Encoder = JavaScriptEncoder
                .Create(UnicodeRanges.BasicLatin, UnicodeRanges.SuperscriptsandSubscripts);
        }

        [Theory]
        [MemberData(nameof(GetTestData), typeof(VolumeJsonConverterTests), nameof(GetSerializeTestData))]
        public void Serialize_ShouldReturnValidJson(VolumeSerializeTestData testData)
        {
            // arrange
            var converter = new VolumeJsonConverter(testData.Format);
            var unitConverter = new VolumeUnitJsonConverter(testData.UnitFormat);

            // act
            string actualJson = Serialize(testData.Quantity, converter, unitConverter);

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
                expectedJsonPattern: "{\"CubicMetres\":5(\\.0+)?}");
            yield return new VolumeSerializeTestData(
                value: testValue,
                format: VolumeJsonSerializationFormat.AsCubicMetresWithUnit,
                unitFormat: LinearUnitJsonSerializationFormat.PredefinedAsString,
                expectedJsonPattern: "{\"CubicMetres\":5(\\.0+)?,\"Unit\":\"cm\\\\u00B3\"}");
            yield return new VolumeSerializeTestData(
                value: testValue,
                format: VolumeJsonSerializationFormat.AsValueWithUnit,
                unitFormat: LinearUnitJsonSerializationFormat.PredefinedAsString,
                expectedJsonPattern: "{\"Value\":5000000(\\.0+)?,\"Unit\":\"cm\\\\u00B3\"}");
            yield return new VolumeSerializeTestData(
                value: testValue,
                format: VolumeJsonSerializationFormat.AsCubicMetresWithUnit,
                unitFormat: LinearUnitJsonSerializationFormat.AlwaysFull,
                expectedJsonPattern:
                "{\"CubicMetres\":5(\\.0+)?,\"Unit\":{\"Name\":\"cubic centimetre\",\"Abbreviation\":\"cm\\\\u00B3\",\"ValueInCubicMetres\":(0\\.000001|1E-06)}}");
            yield return new VolumeSerializeTestData(
                value: testValue,
                format: VolumeJsonSerializationFormat.AsValueWithUnit,
                unitFormat: LinearUnitJsonSerializationFormat.AlwaysFull,
                expectedJsonPattern:
                "{\"Value\":5000000(\\.0+)?,\"Unit\":{\"Name\":\"cubic centimetre\",\"Abbreviation\":\"cm\\\\u00B3\",\"ValueInCubicMetres\":(0\\.000001|1E-06)}}");
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
  ""CubicMetres"": 123.456
}";
            var converter = new VolumeJsonConverter();
            var expectedResult =
                new Volume(cubicMetres: (number)123.456m);

            // act
            var result = Deserialize<Volume>(json, converter);

            // assert
            result.Should().Be(expectedResult);
        }

        [Fact]
        public void DeserializeAsCubicMetresWithPredefinedUnit_ShouldReturnValidResult()
        {
            // arrange
            string json = @"{
  ""CubicMetres"": 123.456,
  ""Unit"": ""cm\u00B3""
}";
            var converter = new VolumeJsonConverter();
            var unitConverter = new VolumeUnitJsonConverter();
            var expectedResult =
                new Volume(cubicMetres: (number)123.456m)
                    .Convert(VolumeUnit.CubicMillimetre);

            // act
            var result = Deserialize<Volume>(json, converter, unitConverter);

            // assert
            result.Should().Be(expectedResult);
        }

        [Fact]
        public void DeserializeAsCubicMetresWithFullySerializedUnit_ShouldReturnValidResult()
        {
            // arrange
            string json = @"{
  ""CubicMetres"": 123.456,
  ""Unit"": {
    ""Name"": ""cubic centimetre"",
    ""Abbreviation"": ""cm\u00B3"",
    ""ValueInCubicMetres"": 0.000001
  }
}";
            var converter = new VolumeJsonConverter();
            var unitConverter = new VolumeUnitJsonConverter();
            var expectedResult =
                new Volume(cubicMetres: (number)123.456m)
                    .Convert(new VolumeUnit(
                        name: "cubic millimetre",
                        abbreviation: "cm³",
                        valueInCubicMetres: (number)0.000001));

            // act
            var result = Deserialize<Volume>(json, converter, unitConverter);

            // assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(VolumeJsonSerializationFormat.AsCubicMetres, LinearUnitJsonSerializationFormat.AlwaysFull)]
        [InlineData(VolumeJsonSerializationFormat.AsCubicMetres, LinearUnitJsonSerializationFormat.PredefinedAsString)]
        [InlineData(VolumeJsonSerializationFormat.AsCubicMetresWithUnit, LinearUnitJsonSerializationFormat.AlwaysFull)]
        [InlineData(VolumeJsonSerializationFormat.AsCubicMetresWithUnit,
            LinearUnitJsonSerializationFormat.PredefinedAsString)]
        public void SerializeAndDeserializeWithCubicMetres_ShouldBeIdempotent(VolumeJsonSerializationFormat format,
            LinearUnitJsonSerializationFormat unitFormat)
        {
            // arrange
            var volume = Fixture.Create<Volume>();
            var converter = new VolumeJsonConverter(format);
            var unitConverter = new VolumeUnitJsonConverter(unitFormat);

            // act
            string serializedVolume1 = Serialize(volume, converter, unitConverter);
            var deserializedVolume1 = Deserialize<Volume>(serializedVolume1, converter, unitConverter);
            string serializedVolume2 = Serialize(deserializedVolume1, converter, unitConverter);
            var deserializedVolume2 = Deserialize<Volume>(serializedVolume2, converter, unitConverter);

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
            var unitConverter = new VolumeUnitJsonConverter();

            // act
            string serializedVolume1 = Serialize(volume, converter, unitConverter);
            var deserializedVolume1 = Deserialize<Volume>(serializedVolume1, converter, unitConverter);
            string serializedVolume2 = Serialize(deserializedVolume1, converter, unitConverter);
            var deserializedVolume2 = Deserialize<Volume>(serializedVolume2, converter, unitConverter);

            // assert
            deserializedVolume1.CubicMetres.Should().BeApproximately(volume.CubicMetres);
            deserializedVolume2.CubicMetres.Should().BeApproximately(volume.CubicMetres);

            deserializedVolume2.Should().Be(deserializedVolume1);
            serializedVolume2.Should().Be(serializedVolume1);
        }
    }
}