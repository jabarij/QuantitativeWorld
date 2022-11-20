using System.Diagnostics.CodeAnalysis;
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

    public class PowerUnitJsonConverterTests : TestsBase
    {
        public PowerUnitJsonConverterTests(TestFixture testFixture) : base(testFixture)
        {
        }

        [Theory]
        [InlineData(LinearUnitJsonSerializationFormat.AlwaysFull,
            "{\"Unit\":{\"Name\":\"kilowatt\",\"Abbreviation\":\"kW\",\"ValueInWatts\":1000(\\.0+)?}}")]
        [InlineData(LinearUnitJsonSerializationFormat.PredefinedAsString, "{\"Unit\":\"kW\"}")]
        public void SerializePredefinedUnit_ShouldReturnValidJson(LinearUnitJsonSerializationFormat serializationFormat,
            string expectedJsonPattern)
        {
            // arrange
            var unit = PowerUnit.Kilowatt;
            var converter = new PowerUnitJsonConverter(serializationFormat);

            // act
            string actualJson = Serialize(new SomeUnitOwner<PowerUnit> {Unit = unit}, converter);

            // assert
            actualJson.Should().MatchRegex(expectedJsonPattern);
        }

        [Theory]
        [MemberData(nameof(GetTestData), typeof(PowerUnit), nameof(PowerUnit.GetPredefinedUnits))]
        public void DeserializePredefinedUnitString_ShouldReturnValidResult(PowerUnit expectedUnit)
        {
            // arrange
            string json = $@"{{
  ""Unit"": ""{expectedUnit.Abbreviation}""
}}";
            var converter = new PowerUnitJsonConverter();

            // act
            var result = Deserialize<SomeUnitOwner<PowerUnit>>(json, converter);

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
  ""ValueInWatts"": 123.456
}";
            var converter = new PowerUnitJsonConverter();

            // act
            var result = Deserialize<PowerUnit>(json, converter);

            // assert
            result.Name.Should().Be("some unit");
            result.Abbreviation.Should().Be("su");
            result.ValueInWatts.Should().Be((number) 123.456m);
        }

        [Fact]
        public void DeserializeCustomUnitAsPredefined_ShouldReturnValidResult()
        {
            // arrange
            var someUnit = new PowerUnit(
                name: "some unit",
                abbreviation: "su",
                valueInWatts: (number) 123.456m);
            string json = @"{
  ""Unit"": ""su""
}";
            var converter = new PowerUnitJsonConverter(
                serializationFormat: LinearUnitJsonSerializationFormat.PredefinedAsString,
                tryReadCustomPredefinedUnit: (string? value, out PowerUnit predefinedUnit) =>
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
            var result = Deserialize<SomeUnitOwner<PowerUnit>>(json, converter);

            // assert
            result!.Unit.Should().Be(someUnit);
        }

        [Theory]
        [InlineData(LinearUnitJsonSerializationFormat.AlwaysFull)]
        [InlineData(LinearUnitJsonSerializationFormat.PredefinedAsString)]
        public void SerializeAndDeserialize_ShouldBeIdempotent(LinearUnitJsonSerializationFormat serializationFormat)
        {
            // arrange
            var obj = new SomeUnitOwner<PowerUnit> {Unit = Fixture.Create<PowerUnit>()};
            var converter = new PowerUnitJsonConverter(serializationFormat);

            // act
            string serializedObj1 = Serialize(obj, converter);
            var deserializedObj1 = Deserialize<SomeUnitOwner<PowerUnit>>(serializedObj1, converter);
            string serializedObj2 = Serialize(deserializedObj1, converter);
            var deserializedObj2 = Deserialize<SomeUnitOwner<PowerUnit>>(serializedObj2, converter);

            // assert
            deserializedObj1!.Unit.Should().Be(obj.Unit);
            deserializedObj2!.Unit.Should().Be(obj.Unit);

            deserializedObj2.Unit.Should().Be(deserializedObj1.Unit);
            serializedObj2.Should().Be(serializedObj1);
        }
    }
}