using System.Diagnostics.CodeAnalysis;
using AutoFixture;
using FluentAssertions;
using Xunit;

#if DECIMAL
namespace DecimalQuantitativeWorld.Json.Tests.Angular
{
    using DecimalQuantitativeWorld.Angular;
    using DecimalQuantitativeWorld.Json.Angular;
    using DecimalQuantitativeWorld.TestAbstractions;
    using number = System.Decimal;
    using TestsBase = DecimalQuantitativeWorld.Json.Tests.TestsBase;

#else
namespace QuantitativeWorld.Json.Tests.Angular
{
    using QuantitativeWorld.Angular;
    using QuantitativeWorld.Json.Angular;
    using QuantitativeWorld.TestAbstractions;
    using number = System.Double;
    using TestsBase = QuantitativeWorld.Json.Tests.TestsBase;
#endif

    public class AngleUnitJsonConverterTests : TestsBase
    {
        public AngleUnitJsonConverterTests(TestFixture testFixture) : base(testFixture)
        {
        }

        [Theory]
        [InlineData(LinearUnitJsonSerializationFormat.AlwaysFull,
            "{\"Unit\":{\"Name\":\"degree\",\"Abbreviation\":\"deg\",\"Symbol\":\"\\\\u00B0\",\"UnitsPerTurn\":360(\\.0+)?}}")]
        [InlineData(LinearUnitJsonSerializationFormat.PredefinedAsString, "{\"Unit\":\"deg\"}")]
        public void SerializePredefinedUnit_ShouldReturnValidJson(LinearUnitJsonSerializationFormat serializationFormat,
            string expectedJsonPattern)
        {
            // arrange
            var unit = AngleUnit.Degree;
            var converter = new AngleUnitJsonConverter(serializationFormat);

            // act
            string actualJson = Serialize(new SomeUnitOwner<AngleUnit> {Unit = unit}, converter);

            // assert
            actualJson.Should().MatchRegex(expectedJsonPattern);
        }

        [Theory]
        [MemberData(nameof(GetTestData), typeof(AngleUnit), nameof(AngleUnit.GetPredefinedUnits))]
        public void DeserializePredefinedUnitString_ShouldReturnValidResult(AngleUnit expectedUnit)
        {
            // arrange
            string json = $@"{{
  ""Unit"": ""{expectedUnit.Abbreviation}""
}}";
            var converter = new AngleUnitJsonConverter();

            // act
            var result = Deserialize<SomeUnitOwner<AngleUnit>>(json, converter);

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
  ""Symbol"": ""?"",
  ""UnitsPerTurn"": 123.456
}";
            var converter = new AngleUnitJsonConverter();

            // act
            var result = Deserialize<AngleUnit>(json, converter);

            // assert
            result.Name.Should().Be("some unit");
            result.Abbreviation.Should().Be("su");
            result.Symbol.Should().Be("?");
            result.UnitsPerTurn.Should().Be((number) 123.456m);
        }

        [Fact]
        public void DeserializeCustomUnitAsPredefined_ShouldReturnValidResult()
        {
            // arrange
            var someUnit = new AngleUnit(
                name: "some unit",
                abbreviation: "su",
                symbol: "?",
                unitsPerTurn: (number) 123.456m);
            string json = @"{
  ""Unit"": ""su""
}";
            var converter = new AngleUnitJsonConverter(
                serializationFormat: LinearUnitJsonSerializationFormat.PredefinedAsString,
                tryReadCustomPredefinedUnit: (string? value, out AngleUnit predefinedUnit) =>
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
            var result = Deserialize<SomeUnitOwner<AngleUnit>>(json, converter);

            // assert
            result!.Unit.Should().Be(someUnit);
        }

        [Theory]
        [InlineData(LinearUnitJsonSerializationFormat.AlwaysFull)]
        [InlineData(LinearUnitJsonSerializationFormat.PredefinedAsString)]
        public void SerializeAndDeserialize_ShouldBeIdempotent(LinearUnitJsonSerializationFormat serializationFormat)
        {
            // arrange
            var obj = new SomeUnitOwner<AngleUnit> {Unit = Fixture.Create<AngleUnit>()};
            var converter = new AngleUnitJsonConverter(serializationFormat);

            // act
            string serializedObj1 = Serialize(obj, converter);
            var deserializedObj1 = Deserialize<SomeUnitOwner<AngleUnit>>(serializedObj1, converter);
            string serializedObj2 = Serialize(deserializedObj1, converter);
            var deserializedObj2 = Deserialize<SomeUnitOwner<AngleUnit>>(serializedObj2, converter);

            // assert
            deserializedObj1!.Unit.Should().Be(obj.Unit);
            deserializedObj2!.Unit.Should().Be(obj.Unit);

            deserializedObj2.Unit.Should().Be(deserializedObj1.Unit);
            serializedObj2.Should().Be(serializedObj1);
        }
    }
}