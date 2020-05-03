using AutoFixture;
using FluentAssertions;
using Newtonsoft.Json;
using QuantitativeWorld.TestAbstractions;
using Xunit;

namespace QuantitativeWorld.Text.Json.Tests
{
    public class VolumeUnitJsonConverterTests : TestsBase
    {
        public VolumeUnitJsonConverterTests(TestFixture testFixture) : base(testFixture) { }

        [Theory]
        [InlineData(LinearUnitJsonSerializationFormat.AlwaysFull, "{\"Unit\":{\"Name\":\"cubic millimetre\",\"Abbreviation\":\"mm³\",\"ValueInCubicMetres\":1E-09}}")]
        [InlineData(LinearUnitJsonSerializationFormat.PredefinedAsString, "{\"Unit\":\"mm³\"}")]
        public void SerializePredefinedUnit_ShouldReturnValidJson(LinearUnitJsonSerializationFormat serializationFormat, string expectedJson)
        {
            // arrange
            var unit = VolumeUnit.CubicMillimetre;
            var converter = new VolumeUnitJsonConverter(serializationFormat);

            // act
            string actualJson = JsonConvert.SerializeObject(new SomeUnitOwner<VolumeUnit> { Unit = unit }, converter);

            // assert
            actualJson.Should().Be(expectedJson);
        }

        [Theory]
        [MemberData(nameof(GetTestData), typeof(VolumeUnit), nameof(VolumeUnit.GetPredefinedUnits))]
        public void DeserializePredefinedUnitString_ShouldReturnValidResult(VolumeUnit expectedUnit)
        {
            // arrange
            string json = $@"{{
  'unit': '{expectedUnit.Abbreviation}'
}}";
            var converter = new VolumeUnitJsonConverter();

            // act
            var result = JsonConvert.DeserializeObject<SomeUnitOwner<VolumeUnit>>(json, converter);

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
  'valueInCubicMetres': 123.456
}";
            var converter = new VolumeUnitJsonConverter();

            // act
            var result = JsonConvert.DeserializeObject<VolumeUnit>(json, converter);

            // assert
            result.Name.Should().Be("some unit");
            result.Abbreviation.Should().Be("su");
            result.ValueInCubicMetres.Should().Be(123.456d);
        }

        [Theory]
        [InlineData(LinearUnitJsonSerializationFormat.AlwaysFull)]
        [InlineData(LinearUnitJsonSerializationFormat.PredefinedAsString)]
        public void SerializeAndDeserialize_ShouldBeIdempotent(LinearUnitJsonSerializationFormat serializationFormat)
        {
            // arrange
            var obj = new SomeUnitOwner<VolumeUnit> { Unit = Fixture.Create<VolumeUnit>() };
            var converter = new VolumeUnitJsonConverter(serializationFormat);

            // act
            string serializedObj1 = JsonConvert.SerializeObject(obj, converter);
            var deserializedObj1 = JsonConvert.DeserializeObject<SomeUnitOwner<VolumeUnit>>(serializedObj1, converter);
            string serializedObj2 = JsonConvert.SerializeObject(deserializedObj1, converter);
            var deserializedObj2 = JsonConvert.DeserializeObject<SomeUnitOwner<VolumeUnit>>(serializedObj2, converter);

            // assert
            deserializedObj1.Unit.Should().Be(obj.Unit);
            deserializedObj2.Unit.Should().Be(obj.Unit);

            deserializedObj2.Unit.Should().Be(deserializedObj1.Unit);
            serializedObj2.Should().Be(serializedObj1);
        }
    }
}
