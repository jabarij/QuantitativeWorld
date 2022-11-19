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

    public class EnergyUnitJsonConverterTests : TestsBase
    {
        public EnergyUnitJsonConverterTests(TestFixture testFixture) : base(testFixture) { }

        [Theory]
        [InlineData(LinearUnitJsonSerializationFormat.AlwaysFull, "{\"Unit\":{\"Name\":\"kilojoule\",\"Abbreviation\":\"kJ\",\"ValueInJoules\":1000(\\.0+)?}}")]
        [InlineData(LinearUnitJsonSerializationFormat.PredefinedAsString, "{\"Unit\":\"kJ\"}")]
        public void SerializePredefinedUnit_ShouldReturnValidJson(LinearUnitJsonSerializationFormat serializationFormat, string expectedJsonPattern)
        {
            // arrange
            var unit = EnergyUnit.Kilojoule;
            var converter = new EnergyUnitJsonConverter(serializationFormat);

            // act
            string actualJson = Serialize(new SomeUnitOwner<EnergyUnit> { Unit = unit }, converter);

            // assert
            actualJson.Should().MatchRegex(expectedJsonPattern);
        }

        [Theory]
        [MemberData(nameof(GetTestData), typeof(EnergyUnit), nameof(EnergyUnit.GetPredefinedUnits))]
        public void DeserializePredefinedUnitString_ShouldReturnValidResult(EnergyUnit expectedUnit)
        {
            // arrange
            string json = $@"{{
  ""Unit"": ""{expectedUnit.Abbreviation}""
}}";
            var converter = new EnergyUnitJsonConverter();

            // act
            var result = Deserialize<SomeUnitOwner<EnergyUnit>>(json, converter);

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
  ""ValueInJoules"": 123.456
}";
            var converter = new EnergyUnitJsonConverter();

            // act
            var result = Deserialize<EnergyUnit>(json, converter);

            // assert
            result.Name.Should().Be("some unit");
            result.Abbreviation.Should().Be("su");
            result.ValueInJoules.Should().Be((number)123.456m);
        }

        [Fact]
        public void DeserializeCustomUnitAsPredefined_ShouldReturnValidResult()
        {
            // arrange
            var someUnit = new EnergyUnit(
                name: "some unit",
                abbreviation: "su",
                valueInJoules: (number)123.456m);
            string json = @"{
  ""Unit"": ""su""
}";
            var converter = new EnergyUnitJsonConverter(
                serializationFormat: LinearUnitJsonSerializationFormat.PredefinedAsString,
                tryReadCustomPredefinedUnit: (string value, out EnergyUnit predefinedUnit) =>
                {
                    if (value == someUnit.Abbreviation)
                    {
                        predefinedUnit = someUnit;
                        return true;
                    }

                    predefinedUnit = default(EnergyUnit);
                    return false;
                });

            // act
            var result = Deserialize<SomeUnitOwner<EnergyUnit>>(json, converter);

            // assert
            result.Unit.Should().Be(someUnit);
        }

        [Theory]
        [InlineData(LinearUnitJsonSerializationFormat.AlwaysFull)]
        [InlineData(LinearUnitJsonSerializationFormat.PredefinedAsString)]
        public void SerializeAndDeserialize_ShouldBeIdempotent(LinearUnitJsonSerializationFormat serializationFormat)
        {
            // arrange
            var obj = new SomeUnitOwner<EnergyUnit> { Unit = Fixture.Create<EnergyUnit>() };
            var converter = new EnergyUnitJsonConverter(serializationFormat);

            // act
            string serializedObj1 = Serialize(obj, converter);
            var deserializedObj1 = Deserialize<SomeUnitOwner<EnergyUnit>>(serializedObj1, converter);
            string serializedObj2 = Serialize(deserializedObj1, converter);
            var deserializedObj2 = Deserialize<SomeUnitOwner<EnergyUnit>>(serializedObj2, converter);

            // assert
            deserializedObj1.Unit.Should().Be(obj.Unit);
            deserializedObj2.Unit.Should().Be(obj.Unit);

            deserializedObj2.Unit.Should().Be(deserializedObj1.Unit);
            serializedObj2.Should().Be(serializedObj1);
        }
    }
}
