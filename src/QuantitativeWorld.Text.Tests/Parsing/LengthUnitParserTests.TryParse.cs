using AutoFixture;
using FluentAssertions;
using QuantitativeWorld.TestAbstractions;
using QuantitativeWorld.Text.Parsing;
using System.Collections.Generic;
using Xunit;

namespace QuantitativeWorld.Tests.Parsing
{
    partial class LengthUnitParserTests
    {
        public class TryParse : LengthUnitParserTests
        {
            public TryParse(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void UnknownUnit_ShouldSetUnitToDefaultAndReturnFalse()
            {
                // arrange
                var parser = new LengthUnitParser();

                // act
                bool result = parser.TryParse(Fixture.Create<string>(), out var actualUnit);

                // assert
                actualUnit.Should().Be(default(LengthUnit));
                result.Should().BeFalse();
            }

            [Theory]
            [MemberData(nameof(GetTestData), typeof(TryParse), nameof(GetParsableUnitsTestData))]
            public void PredefinedUnit_ShouldParseToExpectedUnitAndReturnTrue(TryParsLengthUnitTestData testData)
            {
                // arrange
                var parser = new LengthUnitParser();

                // act
                bool result = parser.TryParse(testData.ParseValue, out var actualUnit);

                // assert
                actualUnit.Should().Be(testData.ExpectedUnit);
                result.Should().BeTrue();
            }

            private static IEnumerable<TryParsLengthUnitTestData> GetParsableUnitsTestData()
            {
                foreach (var predefinedUnit in LengthUnit.GetPredefinedUnits())
                {
                    yield return new TryParsLengthUnitTestData(predefinedUnit, predefinedUnit.Name);
                    yield return new TryParsLengthUnitTestData(predefinedUnit, predefinedUnit.Abbreviation);
                }
            }

            public class TryParsLengthUnitTestData
            {
                public TryParsLengthUnitTestData(LengthUnit expectedUnit, string parseValue)
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
