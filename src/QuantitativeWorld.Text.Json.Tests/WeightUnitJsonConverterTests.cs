using AutoFixture;
using FluentAssertions;
using Newtonsoft.Json;
using QuantitativeWorld.TestAbstractions;
using Xunit;

namespace QuantitativeWorld.Text.Json.Tests
{
    public class WeightUnitJsonConverterTests : TestsBase
    {
        public WeightUnitJsonConverterTests(TestFixture testFixture) : base(testFixture) { }

        [Theory]
        [InlineData(LinearUnitJsonSerializationFormat.AlwaysFull, "{\"Unit\":{\"Name\":\"gram\",\"Abbreviation\":\"g\",\"ValueInKilograms\":0.001}}")]
        [InlineData(LinearUnitJsonSerializationFormat.PredefinedAsString, "{\"Unit\":\"g\"}")]
        public void SerializePredefinedUnit_ShouldReturnValidJson(LinearUnitJsonSerializationFormat serializationFormat, string expectedJson)
        {
            // arrange
            var unit = WeightUnit.Gram;
            var converter = new WeightUnitJsonConverter(serializationFormat);

            // act
            string actualJson = JsonConvert.SerializeObject(new SomeUnitOwner<WeightUnit> { Unit = unit }, converter);

            // assert
            actualJson.Should().Be(expectedJson);
        }

        [Theory]
        [MemberData(nameof(GetTestData), typeof(WeightUnit), nameof(WeightUnit.GetPredefinedUnits))]
        public void DeserializePredefinedUnitString_ShouldReturnValidResult(WeightUnit expectedUnit)
        {
            // arrange
            string json = $@"{{
  'unit': '{expectedUnit.Abbreviation}'
}}";
            var converter = new WeightUnitJsonConverter();

            // act
            var result = JsonConvert.DeserializeObject<SomeUnitOwner<WeightUnit>>(json, converter);

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
  'valueInKilograms': 123.456
}";
            var converter = new WeightUnitJsonConverter();

            // act
            var result = JsonConvert.DeserializeObject<WeightUnit>(json, converter);

            // assert
            result.Name.Should().Be("some unit");
            result.Abbreviation.Should().Be("su");
            result.ValueInKilograms.Should().Be(123.456m);
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
            string serializedObj1 = JsonConvert.SerializeObject(obj, converter);
            var deserializedObj1 = JsonConvert.DeserializeObject<SomeUnitOwner<WeightUnit>>(serializedObj1, converter);
            string serializedObj2 = JsonConvert.SerializeObject(deserializedObj1, converter);
            var deserializedObj2 = JsonConvert.DeserializeObject<SomeUnitOwner<WeightUnit>>(serializedObj2, converter);

            // assert
            deserializedObj1.Unit.Should().Be(obj.Unit);
            deserializedObj2.Unit.Should().Be(obj.Unit);

            deserializedObj2.Unit.Should().Be(deserializedObj1.Unit);
            serializedObj2.Should().Be(serializedObj1);
        }
    }
}
