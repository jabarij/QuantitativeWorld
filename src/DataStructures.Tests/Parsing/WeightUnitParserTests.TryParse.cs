using AutoFixture;
using FluentAssertions;
using QuantitativeWorld.Parsing;
using System.Collections.Generic;
using Xunit;

namespace QuantitativeWorld.Tests.Parsing
{
    partial class WeightUnitParserTests
    {
        public class TryParse : WeightUnitParserTests
        {
            public TryParse(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void UnknownUnit_ShouldSetUnitToDefaultAndReturnFalse()
            {
                // arrange
                var parser = new WeightUnitParser();

                // act
                bool result = parser.TryParse(Fixture.Create<string>(), out var actualUnit);

                // assert
                actualUnit.Should().Be(default(WeightUnit));
                result.Should().BeFalse();
            }

            [Theory]
            [MemberData(nameof(GetTestData), typeof(TryParse), nameof(GetParsableUnitsTestData))]
            public void PredefinedUnit_ShouldParseToExpectedUnitAndReturnTrue(TryParsWeightUnitTestData testData)
            {
                // arrange
                var parser = new WeightUnitParser();

                // act
                bool result = parser.TryParse(testData.ParseValue, out var actualUnit);

                // assert
                actualUnit.Should().Be(testData.ExpectedUnit);
                result.Should().BeTrue();
            }

            private static IEnumerable<TryParsWeightUnitTestData> GetParsableUnitsTestData()
            {
                foreach (var predefinedUnit in WeightUnit.GetPredefinedUnits())
                {
                    yield return new TryParsWeightUnitTestData(predefinedUnit, predefinedUnit.Name);
                    yield return new TryParsWeightUnitTestData(predefinedUnit, predefinedUnit.Abbreviation);
                }
            }

            public class TryParsWeightUnitTestData
            {
                public TryParsWeightUnitTestData(WeightUnit expectedUnit, string parseValue)
                {
                    ExpectedUnit = expectedUnit;
                    ParseValue = parseValue;
                }

                public WeightUnit ExpectedUnit { get; }
                public string ParseValue { get; }
            }
        }
    }
}
