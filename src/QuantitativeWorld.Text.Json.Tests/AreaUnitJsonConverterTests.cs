using AutoFixture;
using FluentAssertions;
using Newtonsoft.Json;
using QuantitativeWorld.TestAbstractions;
using Xunit;

namespace QuantitativeWorld.Text.Json.Tests
{
#if DECIMAL
    using number = System.Decimal;
#else
    using number = System.Double;
    using Constants = QuantitativeWorld.DoubleConstants;
#endif

    public class AreaUnitJsonConverterTests : TestsBase
    {
        public AreaUnitJsonConverterTests(TestFixture testFixture) : base(testFixture) { }

        [Theory]
        [InlineData(LinearUnitJsonSerializationFormat.AlwaysFull, "{\"Unit\":{\"Name\":\"square millimetre\",\"Abbreviation\":\"mm²\",\"ValueInSquareMetres\":(0\\.000001|1E-06)}}")]
        [InlineData(LinearUnitJsonSerializationFormat.PredefinedAsString, "{\"Unit\":\"mm²\"}")]
        public void SerializePredefinedUnit_ShouldReturnValidJson(LinearUnitJsonSerializationFormat serializationFormat, string expectedJsonPattern)
        {
            // arrange
            var unit = AreaUnit.SquareMillimetre;
            var converter = new AreaUnitJsonConverter(serializationFormat);

            // act
            string actualJson = JsonConvert.SerializeObject(new SomeUnitOwner<AreaUnit> { Unit = unit }, converter);

            // assert
            actualJson.Should().MatchRegex(expectedJsonPattern);
        }

        [Theory]
        [MemberData(nameof(GetTestData), typeof(AreaUnit), nameof(AreaUnit.GetPredefinedUnits))]
        public void DeserializePredefinedUnitString_ShouldReturnValidResult(AreaUnit expectedUnit)
        {
            // arrange
            string json = $@"{{
  'unit': '{expectedUnit.Abbreviation}'
}}";
            var converter = new AreaUnitJsonConverter();

            // act
            var result = JsonConvert.DeserializeObject<SomeUnitOwner<AreaUnit>>(json, converter);

            // assert
            result.Unit.Should().Be(expectedUnit);
        }

        [Fact]
        public void DeserializeCustomUnit_ShouldReturnValidResult()
        {
            // arrange
            string json = @"{
  'name': 'some unit',
  'abbreviation': 'su',
  'valueInSquareMetres': 123.456
}";
            var converter = new AreaUnitJsonConverter();

            // act
            var result = JsonConvert.DeserializeObject<AreaUnit>(json, converter);

            // assert
            result.Name.Should().Be("some unit");
            result.Abbreviation.Should().Be("su");
            result.ValueInSquareMetres.Should().Be((number)123.456m);
        }

        [Fact]
        public void DeserializeCustomUnitAsPredefined_ShouldReturnValidResult()
        {
            // arrange
            var someUnit = new AreaUnit(
                name: "some unit",
                abbreviation: "su",
                valueInSquareMetres: (number)123.456m);
            string json = @"{
  'unit': 'su'
}";
            var converter = new AreaUnitJsonConverter(
                serializationFormat: LinearUnitJsonSerializationFormat.PredefinedAsString,
                tryReadCustomPredefinedUnit: (string value, out AreaUnit predefinedUnit) =>
                {
                    if (value == someUnit.Abbreviation)
                    {
                        predefinedUnit = someUnit;
                        return true;
                    }

                    predefinedUnit = default(AreaUnit);
                    return false;
                });

            // act
            var result = JsonConvert.DeserializeObject<SomeUnitOwner<AreaUnit>>(json, converter);

            // assert
            result.Unit.Should().Be(someUnit);
        }

        [Theory]
        [InlineData(LinearUnitJsonSerializationFormat.AlwaysFull)]
        [InlineData(LinearUnitJsonSerializationFormat.PredefinedAsString)]
        public void SerializeAndDeserialize_ShouldBeIdempotent(LinearUnitJsonSerializationFormat serializationFormat)
        {
            // arrange
            var obj = new SomeUnitOwner<AreaUnit> { Unit = Fixture.Create<AreaUnit>() };
            var converter = new AreaUnitJsonConverter(serializationFormat);

            // act
            string serializedObj1 = JsonConvert.SerializeObject(obj, converter);
            var deserializedObj1 = JsonConvert.DeserializeObject<SomeUnitOwner<AreaUnit>>(serializedObj1, converter);
            string serializedObj2 = JsonConvert.SerializeObject(deserializedObj1, converter);
            var deserializedObj2 = JsonConvert.DeserializeObject<SomeUnitOwner<AreaUnit>>(serializedObj2, converter);

            // assert
            deserializedObj1.Unit.Should().Be(obj.Unit);
            deserializedObj2.Unit.Should().Be(obj.Unit);

            deserializedObj2.Unit.Should().Be(deserializedObj1.Unit);
            serializedObj2.Should().Be(serializedObj1);
        }
    }
}
