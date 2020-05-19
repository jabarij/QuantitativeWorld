using AutoFixture;
using FluentAssertions;
using Newtonsoft.Json;
using QuantitativeWorld.TestAbstractions;
using Xunit;

namespace QuantitativeWorld.Text.Json.Tests
{
    public class EnergyUnitJsonConverterTests : TestsBase
    {
        public EnergyUnitJsonConverterTests(TestFixture testFixture) : base(testFixture) { }

        [Theory]
        [InlineData(LinearUnitJsonSerializationFormat.AlwaysFull, "{\"Unit\":{\"Name\":\"kilojoule\",\"Abbreviation\":\"kJ\",\"ValueInJoules\":1000.0}}")]
        [InlineData(LinearUnitJsonSerializationFormat.PredefinedAsString, "{\"Unit\":\"kJ\"}")]
        public void SerializePredefinedUnit_ShouldReturnValidJson(LinearUnitJsonSerializationFormat serializationFormat, string expectedJson)
        {
            // arrange
            var unit = EnergyUnit.Kilojoule;
            var converter = new EnergyUnitJsonConverter(serializationFormat);

            // act
            string actualJson = JsonConvert.SerializeObject(new SomeUnitOwner<EnergyUnit> { Unit = unit }, converter);

            // assert
            actualJson.Should().Be(expectedJson);
        }

        [Theory]
        [MemberData(nameof(GetTestData), typeof(EnergyUnit), nameof(EnergyUnit.GetPredefinedUnits))]
        public void DeserializePredefinedUnitString_ShouldReturnValidResult(EnergyUnit expectedUnit)
        {
            // arrange
            string json = $@"{{
  'unit': '{expectedUnit.Abbreviation}'
}}";
            var converter = new EnergyUnitJsonConverter();

            // act
            var result = JsonConvert.DeserializeObject<SomeUnitOwner<EnergyUnit>>(json, converter);

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
  'valueInJoules': 123.456
}";
            var converter = new EnergyUnitJsonConverter();

            // act
            var result = JsonConvert.DeserializeObject<EnergyUnit>(json, converter);

            // assert
            result.Name.Should().Be("some unit");
            result.Abbreviation.Should().Be("su");
            result.ValueInJoules.Should().Be(123.456d);
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
            string serializedObj1 = JsonConvert.SerializeObject(obj, converter);
            var deserializedObj1 = JsonConvert.DeserializeObject<SomeUnitOwner<EnergyUnit>>(serializedObj1, converter);
            string serializedObj2 = JsonConvert.SerializeObject(deserializedObj1, converter);
            var deserializedObj2 = JsonConvert.DeserializeObject<SomeUnitOwner<EnergyUnit>>(serializedObj2, converter);

            // assert
            deserializedObj1.Unit.Should().Be(obj.Unit);
            deserializedObj2.Unit.Should().Be(obj.Unit);

            deserializedObj2.Unit.Should().Be(deserializedObj1.Unit);
            serializedObj2.Should().Be(serializedObj1);
        }
    }
}
