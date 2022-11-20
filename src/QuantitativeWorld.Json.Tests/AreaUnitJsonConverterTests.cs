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

    public class AreaUnitJsonConverterTests : TestsBase
    {
        public AreaUnitJsonConverterTests(TestFixture testFixture) : base(testFixture)
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
            "{\"Unit\":{\"Name\":\"square millimetre\",\"Abbreviation\":\"mm\\\\u00B2\",\"ValueInSquareMetres\":(0\\.000001|1E-06)}}")]
        [InlineData(LinearUnitJsonSerializationFormat.PredefinedAsString, "{\"Unit\":\"mm\\\\u00B2\"}")]
        public void SerializePredefinedUnit_ShouldReturnValidJson(LinearUnitJsonSerializationFormat serializationFormat,
            string expectedJsonPattern)
        {
            // arrange
            var unit = AreaUnit.SquareMillimetre;
            var converter = new AreaUnitJsonConverter(serializationFormat);

            // act
            string actualJson = Serialize(new SomeUnitOwner<AreaUnit> {Unit = unit}, converter);

            // assert
            actualJson.Should().MatchRegex(expectedJsonPattern);
        }

        [Theory]
        [MemberData(nameof(GetTestData), typeof(AreaUnit), nameof(AreaUnit.GetPredefinedUnits))]
        public void DeserializePredefinedUnitString_ShouldReturnValidResult(AreaUnit expectedUnit)
        {
            // arrange
            string json = $@"{{
  ""Unit"": ""{expectedUnit.Abbreviation}""
}}";
            var converter = new AreaUnitJsonConverter();

            // act
            var result = Deserialize<SomeUnitOwner<AreaUnit>>(json, converter);

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
  ""ValueInSquareMetres"": 123.456
}";
            var converter = new AreaUnitJsonConverter();

            // act
            var result = Deserialize<AreaUnit>(json, converter);

            // assert
            result.Name.Should().Be("some unit");
            result.Abbreviation.Should().Be("su");
            result.ValueInSquareMetres.Should().Be((number) 123.456m);
        }

        [Fact]
        public void DeserializeCustomUnitAsPredefined_ShouldReturnValidResult()
        {
            // arrange
            var someUnit = new AreaUnit(
                name: "some unit",
                abbreviation: "su",
                valueInSquareMetres: (number) 123.456m);
            string json = @"{
  ""Unit"": ""su""
}";
            var converter = new AreaUnitJsonConverter(
                serializationFormat: LinearUnitJsonSerializationFormat.PredefinedAsString,
                tryReadCustomPredefinedUnit: (string? value, out AreaUnit predefinedUnit) =>
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
            var result = Deserialize<SomeUnitOwner<AreaUnit>>(json, converter);

            // assert
            result!.Unit.Should().Be(someUnit);
        }

        [Theory]
        [InlineData(LinearUnitJsonSerializationFormat.AlwaysFull)]
        [InlineData(LinearUnitJsonSerializationFormat.PredefinedAsString)]
        public void SerializeAndDeserialize_ShouldBeIdempotent(LinearUnitJsonSerializationFormat serializationFormat)
        {
            // arrange
            var obj = new SomeUnitOwner<AreaUnit> {Unit = Fixture.Create<AreaUnit>()};
            var converter = new AreaUnitJsonConverter(serializationFormat);

            // act
            string serializedObj1 = Serialize(obj, converter);
            var deserializedObj1 = Deserialize<SomeUnitOwner<AreaUnit>>(serializedObj1, converter);
            string serializedObj2 = Serialize(deserializedObj1, converter);
            var deserializedObj2 = Deserialize<SomeUnitOwner<AreaUnit>>(serializedObj2, converter);

            // assert
            deserializedObj1!.Unit.Should().Be(obj.Unit);
            deserializedObj2!.Unit.Should().Be(obj.Unit);

            deserializedObj2.Unit.Should().Be(deserializedObj1.Unit);
            serializedObj2.Should().Be(serializedObj1);
        }
    }
}