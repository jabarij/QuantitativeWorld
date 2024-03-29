﻿using AutoFixture;
using FluentAssertions;
using System.Collections.Generic;
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

    public class LengthJsonConverterTests : TestsBase
    {
        public LengthJsonConverterTests(TestFixture testFixture) : base(testFixture)
        {
        }

        [Theory]
        [MemberData(nameof(GetTestData), typeof(LengthJsonConverterTests), nameof(GetSerializeTestData))]
        public void Serialize_ShouldReturnValidJson(LengthSerializeTestData testData)
        {
            // arrange
            var converter = new LengthJsonConverter(testData.Format);
            var unitConverter = new LengthUnitJsonConverter(testData.UnitFormat);

            // act
            string actualJson = Serialize(testData.Quantity, converter, unitConverter);

            // assert
            actualJson.Should().MatchRegex(testData.ExpectedJsonPattern);
        }

        private static IEnumerable<LengthSerializeTestData> GetSerializeTestData()
        {
            var testValue = new Length((number)5m, LengthUnit.Kilometre);
            yield return new LengthSerializeTestData(
                value: testValue,
                format: LengthJsonSerializationFormat.AsMetres,
                unitFormat: LinearUnitJsonSerializationFormat.PredefinedAsString,
                expectedJsonPattern: "{\"Metres\":5000(\\.0+)?}");
            yield return new LengthSerializeTestData(
                value: testValue,
                format: LengthJsonSerializationFormat.AsMetresWithUnit,
                unitFormat: LinearUnitJsonSerializationFormat.PredefinedAsString,
                expectedJsonPattern: "{\"Metres\":5000(\\.0+)?,\"Unit\":\"km\"}");
            yield return new LengthSerializeTestData(
                value: testValue,
                format: LengthJsonSerializationFormat.AsValueWithUnit,
                unitFormat: LinearUnitJsonSerializationFormat.PredefinedAsString,
                expectedJsonPattern: "{\"Value\":5(\\.0+)?,\"Unit\":\"km\"}");
            yield return new LengthSerializeTestData(
                value: testValue,
                format: LengthJsonSerializationFormat.AsMetresWithUnit,
                unitFormat: LinearUnitJsonSerializationFormat.AlwaysFull,
                expectedJsonPattern:
                "{\"Metres\":5000(\\.0+)?,\"Unit\":{\"Name\":\"kilometre\",\"Abbreviation\":\"km\",\"ValueInMetres\":1000(\\.0+)?}}");
            yield return new LengthSerializeTestData(
                value: testValue,
                format: LengthJsonSerializationFormat.AsValueWithUnit,
                unitFormat: LinearUnitJsonSerializationFormat.AlwaysFull,
                expectedJsonPattern:
                "{\"Value\":5(\\.0+)?,\"Unit\":{\"Name\":\"kilometre\",\"Abbreviation\":\"km\",\"ValueInMetres\":1000(\\.0+)?}}");
        }

        public class LengthSerializeTestData : QuantitySerializeTestData<Length>
        {
            public LengthSerializeTestData(
                Length value,
                LengthJsonSerializationFormat format,
                LinearUnitJsonSerializationFormat unitFormat,
                string expectedJsonPattern)
                : base(value, unitFormat, expectedJsonPattern)
            {
                Format = format;
            }

            public LengthJsonSerializationFormat Format { get; set; }
        }

        [Fact]
        public void DeserializeAsMetres_ShouldReturnValidResult()
        {
            // arrange
            string json = @"{
  ""Metres"": 123.456
}";
            var converter = new LengthJsonConverter();
            var expectedResult =
                new Length(metres: (number)123.456m);

            // act
            var result = Deserialize<Length>(json, converter);

            // assert
            result.Should().Be(expectedResult);
        }

        [Fact]
        public void DeserializeAsMetresWithPredefinedUnit_ShouldReturnValidResult()
        {
            // arrange
            string json = @"{
  ""Metres"": 123.456,
  ""Unit"": ""km""
}";
            var converter = new LengthJsonConverter();
            var unitConverter = new LengthUnitJsonConverter();
            var expectedResult =
                new Length(metres: (number)123.456m)
                    .Convert(LengthUnit.Kilometre);

            // act
            var result = Deserialize<Length>(json, converter, unitConverter);

            // assert
            result.Should().Be(expectedResult);
        }

        [Fact]
        public void DeserializeAsMetresWithFullySerializedUnit_ShouldReturnValidResult()
        {
            // arrange
            string json = @"{
  ""Metres"": 123.456,
  ""Unit"": {
    ""Name"": ""kilometre"",
    ""Abbreviation"": ""km"",
    ""ValueInMetres"": ""1000""
  }
}";
            var converter = new LengthJsonConverter();
            var expectedResult =
                new Length(metres: (number)123.456m)
                    .Convert(new LengthUnit(
                        name: "kilometre",
                        abbreviation: "km",
                        valueInMetres: (number)1000));

            // act
            var result = Deserialize<Length>(json, converter);

            // assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(LengthJsonSerializationFormat.AsMetres, LinearUnitJsonSerializationFormat.AlwaysFull)]
        [InlineData(LengthJsonSerializationFormat.AsMetres, LinearUnitJsonSerializationFormat.PredefinedAsString)]
        [InlineData(LengthJsonSerializationFormat.AsMetresWithUnit, LinearUnitJsonSerializationFormat.AlwaysFull)]
        [InlineData(LengthJsonSerializationFormat.AsMetresWithUnit,
            LinearUnitJsonSerializationFormat.PredefinedAsString)]
        public void SerializeAndDeserializeWithMetres_ShouldBeIdempotent(LengthJsonSerializationFormat format,
            LinearUnitJsonSerializationFormat unitFormat)
        {
            // arrange
            var length = Fixture.Create<Length>();
            var converter = new LengthJsonConverter(format);
            var unitConverter = new LengthUnitJsonConverter(unitFormat);

            // act
            string serializedLength1 = Serialize(length, converter, unitConverter);
            var deserializedLength1 = Deserialize<Length>(serializedLength1, converter, unitConverter);
            string serializedLength2 = Serialize(deserializedLength1, converter, unitConverter);
            var deserializedLength2 = Deserialize<Length>(serializedLength2, converter, unitConverter);

            // assert
            deserializedLength1.Should().Be(length);
            deserializedLength2.Should().Be(length);

            deserializedLength2.Should().Be(deserializedLength1);
            serializedLength2.Should().Be(serializedLength1);
        }

        [Fact]
        public void SerializeAndDeserializeWithoutMetres_ShouldBeApproximatelyIdempotent()
        {
            // arrange
            var length = Fixture.Create<Length>();
            var converter = new LengthJsonConverter(LengthJsonSerializationFormat.AsValueWithUnit);
            var unitConverter = new LengthUnitJsonConverter();

            // act
            string serializedLength1 = Serialize(length, converter, unitConverter);
            var deserializedLength1 = Deserialize<Length>(serializedLength1, converter, unitConverter);
            string serializedLength2 = Serialize(deserializedLength1, converter, unitConverter);
            var deserializedLength2 = Deserialize<Length>(serializedLength2, converter, unitConverter);

            // assert
            deserializedLength1.Metres.Should().BeApproximately(length.Metres);
            deserializedLength2.Metres.Should().BeApproximately(length.Metres);

            deserializedLength2.Should().Be(deserializedLength1);
            serializedLength2.Should().Be(serializedLength1);
        }
    }
}