using AutoFixture;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
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

    partial class LengthUnitParserTests
    {
        public class ParseExact : LengthUnitParserTests
        {
            public ParseExact(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void UnknownUnit_ShouldThrow()
            {
                // arrange
                var parser = new LengthUnitParser();

                // act
                Action parseExact = () => parser.ParseExact(Fixture.Create<string>(), "s", null);

                // assert
                parseExact.Should().Throw<FormatException>();
            }

            [Fact]
            public void UnknownFormat_ShouldThrow()
            {
                // arrange
                var parser = new LengthUnitParser();

                // act
                Action parseExact = () => parser.ParseExact(LengthUnit.Metre.Name, Fixture.Create<string>(), null);

                // assert
                parseExact.Should().Throw<FormatException>();
            }

            [Theory]
            [MemberData(nameof(GetTestData), typeof(ParseExact), nameof(GetExactlyParsableUnitsTestData))]
            public void PredefinedUnit_ShouldParseExactToExpectedUnit(ExactlyParsableUnitTestData testData)
            {
                // arrange
                var parser = new LengthUnitParser();

                // act
                var actualUnit = parser.ParseExact(testData.ParseValue, testData.ParseFormat, null);

                // assert
                actualUnit.Should().Be(testData.ExpectedUnit);
            }

            private static IEnumerable<ExactlyParsableUnitTestData> GetExactlyParsableUnitsTestData()
            {
                foreach (var unit in LengthUnit.GetPredefinedUnits().Except(new[] { LengthUnit.Foot, LengthUnit.Inch }))
                {
                    yield return new ExactlyParsableUnitTestData(unit.Name, "l", unit);
                    yield return new ExactlyParsableUnitTestData(unit.Abbreviation, "s", unit);
                    yield return new ExactlyParsableUnitTestData(unit.Name + "s", "ll", unit);
                }

                var foot = LengthUnit.Foot;
                yield return new ExactlyParsableUnitTestData(foot.Name, "l", foot);
                yield return new ExactlyParsableUnitTestData(foot.Abbreviation, "s", foot);
                yield return new ExactlyParsableUnitTestData("feet", "ll", foot);

                var inch = LengthUnit.Inch;
                yield return new ExactlyParsableUnitTestData(inch.Name, "l", inch);
                yield return new ExactlyParsableUnitTestData(inch.Abbreviation, "s", inch);
                yield return new ExactlyParsableUnitTestData("inches", "ll", inch);
            }

            public class ExactlyParsableUnitTestData
            {
                public ExactlyParsableUnitTestData(string parseValue, string parseFormat, LengthUnit expectedUnit)
                {
                    ParseValue = parseValue;
                    ParseFormat = parseFormat;
                    ExpectedUnit = expectedUnit;
                }

                public string ParseValue { get; }
                public string ParseFormat { get; }
                public LengthUnit ExpectedUnit { get; }
            }
        }
    }
}
