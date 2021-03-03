using AutoFixture;
using FluentAssertions;
using System.Collections.Generic;
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
        public class Creation : AreaTests
        {
            public Creation(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void ConstructorForMetres_ShouldCreateValidArea()
            {
                // arrange
                number squareMetres = Fixture.Create<number>();

                // act
                var area = new Area(squareMetres);

                // assert
                area.SquareMetres.Should().Be(squareMetres);
                area.Value.Should().Be(squareMetres);
                area.Unit.Should().Be(AreaUnit.SquareMetre);
            }

            [Theory]
            [MemberData(nameof(GetTestData), typeof(Creation), nameof(GetConstructorForValueAndUnitTestData))]
            public void ConstructorForValueAndUnit_ShouldCreateValidArea(ConstructorForValueAndUnitTestData testData)
            {
                // arrange
                // act
                var area = new Area(testData.OriginalValue.Value, testData.OriginalValue.Unit);

                // assert
                area.SquareMetres.Should().BeApproximately(testData.ExpectedValue.SquareMetres);
                area.Value.Should().Be(testData.OriginalValue.Value);
                area.Unit.Should().Be(testData.OriginalValue.Unit);
            }
            private static IEnumerable<ConstructorForValueAndUnitTestData> GetConstructorForValueAndUnitTestData()
            {
                yield return new ConstructorForValueAndUnitTestData(1m, AreaUnit.SquareMetre, 1m);
                yield return new ConstructorForValueAndUnitTestData(1000000m, AreaUnit.SquareMillimetre, 1m);
                yield return new ConstructorForValueAndUnitTestData(1 / (0.00064516m), AreaUnit.SquareInch, 1m);
                yield return new ConstructorForValueAndUnitTestData(1 / (0.00064516m * 144m), AreaUnit.SquareFoot, 1m);
                yield return new ConstructorForValueAndUnitTestData(1 / (0.00064516m * 144m * 9m), AreaUnit.SquareYard, 1m);
                yield return new ConstructorForValueAndUnitTestData(1 / (0.00064516m * 4014489600m), AreaUnit.SquareMile, 1m);
            }
            public class ConstructorForValueAndUnitTestData : ConversionTestData<Area>
            {
                public ConstructorForValueAndUnitTestData(decimal value, AreaUnit unit, decimal expectedSquareMetres)
                    : base(new Area((number)value, unit), new Area((number)expectedSquareMetres)) { }
            }

            [Theory]
            [InlineData(0.000001, 1000000)]
            [InlineData(1, 1000000000000)]
            [InlineData(1000, 1000000000000000)]
            public void FromSquareKilometres_ShouldCreateValidArea(number squareKilometres, number squareMillimetres)
            {
                // arrange
                var expectedArea = new Area(squareMillimetres, AreaUnit.SquareMillimetre);

                // act
                var actualArea = new Area(squareKilometres, AreaUnit.SquareKilometre);

                // assert
                actualArea.Should().Be(expectedArea);
            }
        }
    }
}
