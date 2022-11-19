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

    public class WeightUnitJsonConverterTests : TestsBase
    {
        public WeightUnitJsonConverterTests(TestFixture testFixture) : base(testFixture) { }

        [Theory]
        [InlineData(LinearUnitJsonSerializationFormat.AlwaysFull, "{\"Unit\":{\"Name\":\"gram\",\"Abbreviation\":\"g\",\"ValueInKilograms\":0.001}}")]
        [InlineData(LinearUnitJsonSerializationFormat.PredefinedAsString, "{\"Unit\":\"g\"}")]
        public void SerializePredefinedUnit_ShouldReturnValidJson(LinearUnitJsonSerializationFormat serializationFormat, string expectedJsonPattern)
        {
            // arrange
            var unit = WeightUnit.Gram;
            var converter = new WeightUnitJsonConverter(serializationFormat);

            // act
            string actualJson = Serialize(new SomeUnitOwner<WeightUnit> { Unit = unit }, converter);

            // assert
            actualJson.Should().MatchRegex(expectedJsonPattern);
        }

        [Theory]
        [MemberData(nameof(GetTestData), typeof(WeightUnit), nameof(WeightUnit.GetPredefinedUnits))]
        public void DeserializePredefinedUnitString_ShouldReturnValidResult(WeightUnit expectedUnit)
        {
            // arrange
            string json = $@"{{
  ""Unit"": ""{expectedUnit.Abbreviation}""
}}";
            var converter = new WeightUnitJsonConverter();

            // act
            var result = Deserialize<SomeUnitOwner<WeightUnit>>(json, converter);

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
  ""ValueInKilograms"": 123.456
}";
            var converter = new WeightUnitJsonConverter();

            // act
            var result = Deserialize<WeightUnit>(json, converter);

            // assert
            result.Name.Should().Be("some unit");
            result.Abbreviation.Should().Be("su");
            result.ValueInKilograms.Should().Be((number)123.456m);
        }

        [Fact]
        public void DeserializeCustomUnitAsPredefined_ShouldReturnValidResult()
        {
            // arrange
            var someUnit = new WeightUnit(
                name: "some unit",
                abbreviation: "su",
                valueInKilograms: (number)123.456m);
            string json = @"{
  ""Unit"": ""su""
}";
            var converter = new WeightUnitJsonConverter(
                serializationFormat: LinearUnitJsonSerializationFormat.PredefinedAsString,
                tryReadCustomPredefinedUnit: (string value, out WeightUnit predefinedUnit) =>
                {
                    if (value == someUnit.Abbreviation)
                    {
                        predefinedUnit = someUnit;
                        return true;
                    }

                    predefinedUnit = default(WeightUnit);
                    return false;
                });

            // act
            var result = Deserialize<SomeUnitOwner<WeightUnit>>(json, converter);

            // assert
            result.Unit.Should().Be(someUnit);
        }

        [Theory]
        [InlineData(LinearUnitJsonSerializationFormat.AlwaysFull)]
        [InlineData(LinearUnitJsonSerializationFormat.PredefinedAsString)]
        public void SerializeAndDeserialize_ShouldBeIdempotent(LinearUnitJsonSerializationFormat serializationFormat)
        {
            // arrange
            var obj = new SomeUnitOwner<WeightUnit> { Unit = Fixture.Create<WeightUnit>() };
            var converter = new WeightUnitJsonConverter(serializationFormat);

            // act
            string serializedObj1 = Serialize(obj, converter);
            var deserializedObj1 = Deserialize<SomeUnitOwner<WeightUnit>>(serializedObj1, converter);
            string serializedObj2 = Serialize(deserializedObj1, converter);
            var deserializedObj2 = Deserialize<SomeUnitOwner<WeightUnit>>(serializedObj2, converter);

            // assert
            deserializedObj1.Unit.Should().Be(obj.Unit);
            deserializedObj2.Unit.Should().Be(obj.Unit);

            deserializedObj2.Unit.Should().Be(deserializedObj1.Unit);
            serializedObj2.Should().Be(serializedObj1);
        }
    }
}
