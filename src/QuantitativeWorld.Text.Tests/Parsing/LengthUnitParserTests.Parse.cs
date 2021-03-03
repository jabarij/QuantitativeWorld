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
    partial class LengthUnitParserTests
    {
        public class Parse : LengthUnitParserTests
        {
            public Parse(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void UnknownUnit_ShouldThrow()
            {
                // arrange
                var parser = new LengthUnitParser();

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
                var parser = new LengthUnitParser();

                // act
                var actualUnit = parser.Parse(testData.ParseValue);

                // assert
                actualUnit.Should().Be(testData.ExpectedUnit);
            }

            private static IEnumerable<ParseUnitTestData> GetParsableUnitsTestData()
            {
                foreach (var predefinedUnit in LengthUnit.GetPredefinedUnits())
                {
                    yield return new ParseUnitTestData(predefinedUnit, predefinedUnit.Name);
                    yield return new ParseUnitTestData(predefinedUnit, predefinedUnit.Abbreviation);
                }
            }

            public class ParseUnitTestData
            {
                public ParseUnitTestData(LengthUnit expectedUnit, string parseValue)
                {
                    ExpectedUnit = expectedUnit;
                    ParseValue = parseValue;
                }

                public LengthUnit ExpectedUnit { get; }
                public string ParseValue { get; }
            }
        }
    }
}
