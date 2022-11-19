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

    public class LengthUnitJsonConverterTests : TestsBase
    {
        public LengthUnitJsonConverterTests(TestFixture testFixture) : base(testFixture) { }

        [Theory]
        [InlineData(LinearUnitJsonSerializationFormat.AlwaysFull, "{\"Unit\":{\"Name\":\"kilometre\",\"Abbreviation\":\"km\",\"ValueInMetres\":1000(\\.0+)?}}")]
        [InlineData(LinearUnitJsonSerializationFormat.PredefinedAsString, "{\"Unit\":\"km\"}")]
        public void SerializePredefinedUnit_ShouldReturnValidJson(LinearUnitJsonSerializationFormat serializationFormat, string expectedJsonPattern)
        {
            // arrange
            var unit = LengthUnit.Kilometre;
            var converter = new LengthUnitJsonConverter(serializationFormat);

            // act
            string actualJson = Serialize(new SomeUnitOwner<LengthUnit> { Unit = unit }, converter);

            // assert
            actualJson.Should().MatchRegex(expectedJsonPattern);
        }

        [Theory]
        [MemberData(nameof(GetTestData), typeof(LengthUnit), nameof(LengthUnit.GetPredefinedUnits))]
        public void DeserializePredefinedUnitString_ShouldReturnValidResult(LengthUnit expectedUnit)
        {
            // arrange
            string json = $@"{{
  ""Unit"": ""{expectedUnit.Abbreviation}""
}}";
            var converter = new LengthUnitJsonConverter();

            // act
            var result = Deserialize<SomeUnitOwner<LengthUnit>>(json, converter);

            // assert
            result.Unit.Should().Be(expectedUnit);
        }

        [Fact]
        public void DeserializeCustomUnit_ShouldReturnValidResult()
        {
            // arrange
            string json = @"{
  ""Name"": ""some unit"",
  ""Abbreviation"": ""su"",
  ""ValueInMetres"": 123.456
}";
            var converter = new LengthUnitJsonConverter();

            // act
            var result = Deserialize<LengthUnit>(json, converter);

            // assert
            result.Name.Should().Be("some unit");
            result.Abbreviation.Should().Be("su");
            result.ValueInMetres.Should().Be((number)123.456m);
        }

        [Fact]
        public void DeserializeCustomUnitAsPredefined_ShouldReturnValidResult()
        {
            // arrange
            var someUnit = new LengthUnit(
                name: "some unit",
                abbreviation: "su",
                valueInMetres: (number)123.456m);
            string json = @"{
  ""Unit"": ""su""
}";
            var converter = new LengthUnitJsonConverter(
                serializationFormat: LinearUnitJsonSerializationFormat.PredefinedAsString,
                tryReadCustomPredefinedUnit: (string value, out LengthUnit predefinedUnit) =>
                {
                    if (value == someUnit.Abbreviation)
                    {
                        predefinedUnit = someUnit;
                        return true;
                    }

                    predefinedUnit = default(LengthUnit);
                    return false;
                });

            // act
            var result = Deserialize<SomeUnitOwner<LengthUnit>>(json, converter);

            // assert
            result.Unit.Should().Be(someUnit);
        }

        [Theory]
        [InlineData(LinearUnitJsonSerializationFormat.AlwaysFull)]
        [InlineData(LinearUnitJsonSerializationFormat.PredefinedAsString)]
        public void SerializeAndDeserialize_ShouldBeIdempotent(LinearUnitJsonSerializationFormat serializationFormat)
        {
            // arrange
            var obj = new SomeUnitOwner<LengthUnit> { Unit = Fixture.Create<LengthUnit>() };
            var converter = new LengthUnitJsonConverter(serializationFormat);

            // act
            string serializedObj1 = Serialize(obj, converter);
            var deserializedObj1 = Deserialize<SomeUnitOwner<LengthUnit>>(serializedObj1, converter);
            string serializedObj2 = Serialize(deserializedObj1, converter);
            var deserializedObj2 = Deserialize<SomeUnitOwner<LengthUnit>>(serializedObj2, converter);

            // assert
            deserializedObj1.Unit.Should().Be(obj.Unit);
            deserializedObj2.Unit.Should().Be(obj.Unit);

            deserializedObj2.Unit.Should().Be(deserializedObj1.Unit);
            serializedObj2.Should().Be(serializedObj1);
        }
    }
}
