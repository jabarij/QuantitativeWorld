using AutoFixture;
using FluentAssertions;
using QuantitativeWorld.Parsing;
using System;
using System.Collections.Generic;
using Xunit;

namespace QuantitativeWorld.Tests.Parsing
{
    partial class WeightUnitParserTests
    {
        public class Parse : WeightUnitParserTests
        {
            public Parse(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void UnknownUnit_ShouldThrow()
            {
                // arrange
                var parser = new WeightUnitParser();

                // act
                Action parse = () => parser.Parse(Fixture.Create<string>());

                // assert
                parse.Should().Throw<FormatException>();
            }

            [Theory]
            [MemberData(nameof(GetTestData), typeof(Parse), nameof(GetParsableUnitsTestData))]
            public void PredefinedUnit_ShouldParseToExpectedUnit(ParseUnitTestData testData)
            {
                // arrange
                var parser = new WeightUnitParser();

                // act
                var actualUnit = parser.Parse(testData.ParseValue);

                // assert
                actualUnit.Should().Be(testData.ExpectedUnit);
            }

            private static IEnumerable<ParseUnitTestData> GetParsableUnitsTestData()
            {
                foreach (var predefinedUnit in WeightUnit.GetPredefinedUnits())
                {
                    yield return new ParseUnitTestData(predefinedUnit, predefinedUnit.Name);
                    yield return new ParseUnitTestData(predefinedUnit, predefinedUnit.Abbreviation);
                }
            }

            public class ParseUnitTestData
            {
                public ParseUnitTestData(WeightUnit expectedUnit, string parseValue)
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
