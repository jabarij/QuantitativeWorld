using AutoFixture;
using FluentAssertions;
using Newtonsoft.Json;
using QuantitativeWorld.TestAbstractions;
using Xunit;

namespace QuantitativeWorld.Text.Json.Tests
{
    public class SpeedUnitJsonConverterTests : TestsBase
    {
        public SpeedUnitJsonConverterTests(TestFixture testFixture) : base(testFixture) { }

        [Theory]
        [InlineData(LinearUnitJsonSerializationFormat.AlwaysFull, "{\"Unit\":{\"Name\":\"kilometre per hour\",\"Abbreviation\":\"km/h\",\"ValueInMetresPerSecond\":0.27777777777777779}}")]
        [InlineData(LinearUnitJsonSerializationFormat.PredefinedAsString, "{\"Unit\":\"km/h\"}")]
        public void SerializePredefinedUnit_ShouldReturnValidJson(LinearUnitJsonSerializationFormat serializationFormat, string expectedJson)
        {
            // arrange
            var unit = SpeedUnit.KilometrePerHour;
            var converter = new SpeedUnitJsonConverter(serializationFormat);

            // act
            string actualJson = JsonConvert.SerializeObject(new SomeUnitOwner<SpeedUnit> { Unit = unit }, converter);

            // assert
            actualJson.Should().Be(expectedJson);
        }

        [Theory]
        [MemberData(nameof(GetTestData), typeof(SpeedUnit), nameof(SpeedUnit.GetPredefinedUnits))]
        public void DeserializePredefinedUnitString_ShouldReturnValidResult(SpeedUnit expectedUnit)
        {
            // arrange
            string json = $@"{{
  'unit': '{expectedUnit.Abbreviation}'
}}";
            var converter = new SpeedUnitJsonConverter();

            // act
            var result = JsonConvert.DeserializeObject<SomeUnitOwner<SpeedUnit>>(json, converter);

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
  'valueInMetresPerSecond': 123.456
}";
            var converter = new SpeedUnitJsonConverter();

            // act
            var result = JsonConvert.DeserializeObject<SpeedUnit>(json, converter);

            // assert
            result.Name.Should().Be("some unit");
            result.Abbreviation.Should().Be("su");
            result.ValueInMetresPerSecond.Should().Be(123.456d);
        }

        [Theory]
        [InlineData(LinearUnitJsonSerializationFormat.AlwaysFull)]
        [InlineData(LinearUnitJsonSerializationFormat.PredefinedAsString)]
        public void SerializeAndDeserialize_ShouldBeIdempotent(LinearUnitJsonSerializationFormat serializationFormat)
        {
            // arrange
            var obj = new SomeUnitOwner<SpeedUnit> { Unit = Fixture.Create<SpeedUnit>() };
            var converter = new SpeedUnitJsonConverter(serializationFormat);

            // act
            string serializedObj1 = JsonConvert.SerializeObject(obj, converter);
            var deserializedObj1 = JsonConvert.DeserializeObject<SomeUnitOwner<SpeedUnit>>(serializedObj1, converter);
            string serializedObj2 = JsonConvert.SerializeObject(deserializedObj1, converter);
            var deserializedObj2 = JsonConvert.DeserializeObject<SomeUnitOwner<SpeedUnit>>(serializedObj2, converter);

            // assert
            deserializedObj1.Unit.Should().Be(obj.Unit);
            deserializedObj2.Unit.Should().Be(obj.Unit);

            deserializedObj2.Unit.Should().Be(deserializedObj1.Unit);
            serializedObj2.Should().Be(serializedObj1);
        }
    }
}
