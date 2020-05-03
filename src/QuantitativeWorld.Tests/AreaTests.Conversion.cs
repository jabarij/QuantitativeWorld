using FluentAssertions;
using QuantitativeWorld.TestAbstractions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace QuantitativeWorld.Tests
{
    partial class AreaTests
    {
        public class Convert : AreaTests
        {
            public Convert(TestFixture testFixture)
                : base(testFixture) { }

            [Theory]
            [MemberData(nameof(GetTestData), typeof(Convert), nameof(GetConvertTestData))]
            public void ShouldConvertToExpectedValue(Area originalArea, AreaUnit targetUnit, Area expectedArea)
            {
                // arrange
                // act
                var actualArea = originalArea.Convert(targetUnit);

                // assert
                actualArea.SquareMetres.Should().BeApproximately(expectedArea.SquareMetres, DoublePrecision);
                actualArea.Value.Should().BeApproximately(expectedArea.Value, DoublePrecision);
                actualArea.Unit.Should().Be(targetUnit);
            }

            [Fact]
            public void MultipleSerialConversion_ShouldHaveSameValueAtTheEnd()
            {
                // arrange
                var units = new List<AreaUnit>
                {
                    AreaUnit.SquareMetre,
                    AreaUnit.SquareMillimetre,
                    AreaUnit.SquareKilometre,
                    AreaUnit.SquareFoot,
                    AreaUnit.SquareInch,
                    AreaUnit.SquareMetre
                };
                var initialArea = new Area(1234.5678d, units.First());
                Area? finalArea = null;

                // act
                units.ForEach(u => finalArea = (finalArea ?? initialArea).Convert(u));

                // assert
                finalArea.Should().Be(initialArea);
            }

            private static IEnumerable<ITestDataProvider> GetConvertTestData()
            {
                yield return new ConvertTestData(new Area(123456d, AreaUnit.SquareMetre), AreaUnit.SquareKilometre, new Area(0.123456d, AreaUnit.SquareKilometre));
                yield return new ConvertTestData(new Area(0.123456d, AreaUnit.SquareKilometre), AreaUnit.SquareMetre, new Area(123456d, AreaUnit.SquareMetre));

                yield return new ConvertTestData(new Area(123.456d, AreaUnit.Are), AreaUnit.Hectare, new Area(1.23456d, AreaUnit.Hectare));
                yield return new ConvertTestData(new Area(1.23456d, AreaUnit.Hectare), AreaUnit.Are, new Area(123.456d, AreaUnit.Are));
            }

            class ConvertTestData : ITestDataProvider
            {
                public ConvertTestData(Area originalArea, AreaUnit targetUnit, Area expectedArea)
                {
                    OriginalArea = originalArea;
                    TargetUnit = targetUnit;
                    ExpectedArea = expectedArea;
                }

                public Area OriginalArea { get; }
                public AreaUnit TargetUnit { get; }
                public Area ExpectedArea { get; }

                public object[] SerializeTestData() =>
                    new[] { (object)OriginalArea, TargetUnit, ExpectedArea };
            }
        }
    }
}
