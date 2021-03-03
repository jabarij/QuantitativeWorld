using AutoFixture;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

#if DECIMAL
namespace DecimalQuantitativeWorld.Tests.Parsing
{
    using DecimalQuantitativeWorld.TestAbstractions;
    using DecimalQuantitativeWorld.Text.Parsing;
#else
namespace QuantitativeWorld.Tests.Parsing
{
    using QuantitativeWorld.TestAbstractions;
    using QuantitativeWorld.Text.Parsing;
#endif

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
                parseExact.Should().Throw<FormatException>();
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
            public void PredefinedUnit_ShouldParseExactToExpectedUnit(ExactlyParsableUnitTestData testData)
            {
                // arrange
                var parser = new WeightUnitParser();

                // act
                var actualUnit = parser.ParseExact(testData.ParseValue, testData.ParseFormat, null);

                // assert
                actualUnit.Should().Be(testData.ExpectedUnit);
            }

            private static IEnumerable<ExactlyParsableUnitTestData> GetExactlyParsableUnitsTestData()
            {
                foreach (var unit in WeightUnit.GetPredefinedUnits())
                {
                    yield return new ExactlyParsableUnitTestData(unit.Name, "l", unit);
                    yield return new ExactlyParsableUnitTestData(unit.Abbreviation, "s", unit);
                    yield return new ExactlyParsableUnitTestData(unit.Name + "s", "ll", unit);
                }
            }

            public class ExactlyParsableUnitTestData
            {
                public ExactlyParsableUnitTestData(string parseValue, string parseFormat, WeightUnit expectedUnit)
                {
                    ParseValue = parseValue;
                    ParseFormat = parseFormat;
                    ExpectedUnit = expectedUnit;
                }

                public string ParseValue { get; }
                public string ParseFormat { get; }
                public WeightUnit ExpectedUnit { get; }
            }
        }
    }
}
