using AutoFixture;
using DataStructures.Parsing;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace DataStructures.Tests.Parsing
{
    partial class WeightUnitParserTests
    {
        public class ParseExact : WeightUnitParserTests
        {
            public ParseExact(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void UnknownUnit_ShouldThrow()
            {
                // arrange
                var parser = new WeightUnitParser();

                // act
                Action parseExact = () => parser.ParseExact(Fixture.Create<string>(), "s", null);

                // assert
                parseExact.Should().Throw<InvalidOperationException>();
            }

            [Fact]
            public void UnknownFormat_ShouldThrow()
            {
                // arrange
                var parser = new WeightUnitParser();

                // act
                Action parseExact = () => parser.ParseExact(WeightUnit.Kilogram.Name, Fixture.Create<string>(), null);

                // assert
                parseExact.Should().Throw<FormatException>();
            }

            [Theory]
            [MemberData(nameof(GetTestData), typeof(ParseExact), nameof(GetExactlyParsableUnitsTestData))]
            public void KnownUnit_ShouldParseExactToExpectedUnit(ExactlyParsableUnitTestData testData)
            {
                // arrange
                var parser = new WeightUnitParser();

                // act
                var actualUnit = parser.ParseExact(testData.ParsedValue, testData.ParseFormat, null);

                // assert
                actualUnit.Should().Be(testData.ExpectedUnit);
            }

            private static IEnumerable<ExactlyParsableUnitTestData> GetExactlyParsableUnitsTestData()
            {
                var units = new List<WeightUnit>
                {
                    WeightUnit.Decagram,
                    WeightUnit.Gram,
                    WeightUnit.Kilogram,
                    WeightUnit.Milligram,
                    WeightUnit.Ounce,
                    WeightUnit.Pound,
                    WeightUnit.Ton
                };

                foreach (var unit in units)
                {
                    yield return new ExactlyParsableUnitTestData(unit.Name, "l", unit);
                    yield return new ExactlyParsableUnitTestData(unit.Abbreviation, "s", unit);
                    yield return new ExactlyParsableUnitTestData(unit.Name + "s", "ll", unit);
                }
            }

            public class ExactlyParsableUnitTestData
            {
                public ExactlyParsableUnitTestData(string parsedValue, string parseFormat, WeightUnit expectedUnit)
                {
                    ParsedValue = parsedValue;
                    ParseFormat = parseFormat;
                    ExpectedUnit = expectedUnit;
                }

                public string ParsedValue { get; }
                public string ParseFormat { get; }
                public WeightUnit ExpectedUnit { get; }
            }
        }
    }
}
