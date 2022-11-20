using System.Diagnostics.CodeAnalysis;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using AutoFixture;
using FluentAssertions;
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

    public class VolumeUnitJsonConverterTests : TestsBase
    {
        public VolumeUnitJsonConverterTests(TestFixture testFixture) : base(testFixture)
        {
        }

        protected override void Configure(JsonSerializerOptions options)
        {
            base.Configure(options);
            options.Encoder = JavaScriptEncoder
                .Create(UnicodeRanges.BasicLatin, UnicodeRanges.SuperscriptsandSubscripts);
        }

        [Theory]
        [InlineData(LinearUnitJsonSerializationFormat.AlwaysFull,
            "{\"Unit\":{\"Name\":\"cubic centimetre\",\"Abbreviation\":\"cm\\\\u00B3\",\"ValueInCubicMetres\":(0\\.000001|1E-06)}}")]
        [InlineData(LinearUnitJsonSerializationFormat.PredefinedAsString, "{\"Unit\":\"cm\\\\u00B3\"}")]
        public void SerializePredefinedUnit_ShouldReturnValidJson(LinearUnitJsonSerializationFormat serializationFormat,
            string expectedJsonPattern)
        {
            // arrange
            var unit = VolumeUnit.CubicCentimetre;
            var converter = new VolumeUnitJsonConverter(serializationFormat);

            // act
            string actualJson = Serialize(new SomeUnitOwner<VolumeUnit> {Unit = unit}, converter);

            // assert
            actualJson.Should().MatchRegex(expectedJsonPattern);
        }

        [Theory]
        [MemberData(nameof(GetTestData), typeof(VolumeUnit), nameof(VolumeUnit.GetPredefinedUnits))]
        public void DeserializePredefinedUnitString_ShouldReturnValidResult(VolumeUnit expectedUnit)
        {
            // arrange
            string json = $@"{{
  ""Unit"": ""{expectedUnit.Abbreviation}""
}}";
            var converter = new VolumeUnitJsonConverter();

            // act
            var result = Deserialize<SomeUnitOwner<VolumeUnit>>(json, converter);

            // assert
            result!.Unit.Should().Be(expectedUnit);
        }

        [Fact]
        public void DeserializeCustomUnit_ShouldReturnValidResult()
        {
            // arrange
            string json = @"{
  ""Name"": ""some unit"",
  ""Abbreviation"": ""su"",
  ""ValueInCubicMetres"": 123.456
}";
            var converter = new VolumeUnitJsonConverter();

            // act
            var result = Deserialize<VolumeUnit>(json, converter);

            // assert
            result.Name.Should().Be("some unit");
            result.Abbreviation.Should().Be("su");
            result.ValueInCubicMetres.Should().Be((number) 123.456m);
        }

        [Fact]
        public void DeserializeCustomUnitAsPredefined_ShouldReturnValidResult()
        {
            // arrange
            var someUnit = new VolumeUnit(
                name: "some unit",
                abbreviation: "su",
                valueInCubicMetres: (number) 123.456m);
            string json = @"{
  ""Unit"": ""su""
}";
            var converter = new VolumeUnitJsonConverter(
                serializationFormat: LinearUnitJsonSerializationFormat.PredefinedAsString,
                tryReadCustomPredefinedUnit: (string? value, out VolumeUnit predefinedUnit) =>
                {
                    if (value == someUnit.Abbreviation)
                    {
                        predefinedUnit = someUnit;
                        return true;
                    }

                    predefinedUnit = default;
                    return false;
                });

            // act
            var result = Deserialize<SomeUnitOwner<VolumeUnit>>(json, converter);

            // assert
            result!.Unit.Should().Be(someUnit);
        }

        [Theory]
        [InlineData(LinearUnitJsonSerializationFormat.AlwaysFull)]
        [InlineData(LinearUnitJsonSerializationFormat.PredefinedAsString)]
        public void SerializeAndDeserialize_ShouldBeIdempotent(LinearUnitJsonSerializationFormat serializationFormat)
        {
            // arrange
            var obj = new SomeUnitOwner<VolumeUnit> {Unit = Fixture.Create<VolumeUnit>()};
            var converter = new VolumeUnitJsonConverter(serializationFormat);

            // act
            string serializedObj1 = Serialize(obj, converter);
            var deserializedObj1 = Deserialize<SomeUnitOwner<VolumeUnit>>(serializedObj1, converter);
            string serializedObj2 = Serialize(deserializedObj1, converter);
            var deserializedObj2 = Deserialize<SomeUnitOwner<VolumeUnit>>(serializedObj2, converter);

            // assert
            deserializedObj1!.Unit.Should().Be(obj.Unit);
            deserializedObj2!.Unit.Should().Be(obj.Unit);

            deserializedObj2.Unit.Should().Be(deserializedObj1.Unit);
            serializedObj2.Should().Be(serializedObj1);
        }
    }
}