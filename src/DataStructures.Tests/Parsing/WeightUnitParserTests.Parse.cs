using AutoFixture;
using QuantitativeWorld.Parsing;
using FluentAssertions;
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
            public void KnownUnit_ShouldParseToExpectedUnit(string knownUnit, WeightUnit expectedUnit)
            {
                // arrange
                var parser = new WeightUnitParser();

                // act
                var actualUnit = parser.Parse(knownUnit);

                // assert
                actualUnit.Should().Be(expectedUnit);
            }

            private static IEnumerable<ITestDataProvider> GetParsableUnitsTestData()
            {
                yield return new ParsableUnitTestData(WeightUnit.Decagram.Name, WeightUnit.Decagram);
                yield return new ParsableUnitTestData(WeightUnit.Gram.Name, WeightUnit.Gram);
                yield return new ParsableUnitTestData(WeightUnit.Kilogram.Name, WeightUnit.Kilogram);
                yield return new ParsableUnitTestData(WeightUnit.Milligram.Name, WeightUnit.Milligram);
                yield return new ParsableUnitTestData(WeightUnit.Ounce.Name, WeightUnit.Ounce);
                yield return new ParsableUnitTestData(WeightUnit.Pound.Name, WeightUnit.Pound);
                yield return new ParsableUnitTestData(WeightUnit.Ton.Name, WeightUnit.Ton);

                yield return new ParsableUnitTestData(WeightUnit.Decagram.Abbreviation, WeightUnit.Decagram);
                yield return new ParsableUnitTestData(WeightUnit.Gram.Abbreviation, WeightUnit.Gram);
                yield return new ParsableUnitTestData(WeightUnit.Kilogram.Abbreviation, WeightUnit.Kilogram);
                yield return new ParsableUnitTestData(WeightUnit.Milligram.Abbreviation, WeightUnit.Milligram);
                yield return new ParsableUnitTestData(WeightUnit.Ounce.Abbreviation, WeightUnit.Ounce);
                yield return new ParsableUnitTestData(WeightUnit.Pound.Abbreviation, WeightUnit.Pound);
                yield return new ParsableUnitTestData(WeightUnit.Ton.Abbreviation, WeightUnit.Ton);
            }

            class ParsableUnitTestData : ITestDataProvider
            {
                public ParsableUnitTestData(string parsedValue, WeightUnit expectedUnit)
                {
                    ParsedValue = parsedValue;
                    ExpectedUnit = expectedUnit;
                }

                public string ParsedValue { get; }
                public WeightUnit ExpectedUnit { get; }

                public object[] SerializeTestData() =>
                    new[] { (object)ParsedValue, ExpectedUnit };
            }
        }
    }
}
