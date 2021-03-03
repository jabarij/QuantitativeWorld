using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

#if DECIMAL
namespace DecimalQuantitativeWorld.Tests
{
    using DecimalQuantitativeWorld.TestAbstractions;
    using number = System.Decimal;
#else
namespace QuantitativeWorld.Tests
{
    using QuantitativeWorld.TestAbstractions;
    using number = System.Double;
#endif

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
                actualArea.SquareMetres.Should().BeApproximately(expectedArea.SquareMetres);
                actualArea.Value.Should().BeApproximately(expectedArea.Value);
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
                var initialArea = new Area((number)1234.5678m, units.First());
                Area? finalArea = null;

                // act
                units.ForEach(u => finalArea = (finalArea ?? initialArea).Convert(u));

                // assert
                finalArea.Should().Be(initialArea);
            }

            private static IEnumerable<ITestDataProvider> GetConvertTestData()
            {
                yield return new ConvertTestData(123456m, AreaUnit.SquareMetre, 0.123456m, AreaUnit.SquareKilometre);
                yield return new ConvertTestData(0.123456m, AreaUnit.SquareKilometre, 123456m, AreaUnit.SquareMetre);

                yield return new ConvertTestData(123.456m, AreaUnit.Are, 1.23456m, AreaUnit.Hectare);
                yield return new ConvertTestData(1.23456m, AreaUnit.Hectare, 123.456m, AreaUnit.Are);
            }

            class ConvertTestData : ConversionTestData<Area>, ITestDataProvider
            {
                public ConvertTestData(decimal originalValue, AreaUnit originalUnit, decimal expectedValue, AreaUnit expectedUnit)
                    : base(new Area((number)originalValue, originalUnit), new Area((number)expectedValue, expectedUnit)) { }

                public object[] GetTestParameters() =>
                    new[] { (object)OriginalValue, ExpectedValue.Unit, ExpectedValue };
            }
        }
    }
}
