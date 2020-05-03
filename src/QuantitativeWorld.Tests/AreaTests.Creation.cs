using AutoFixture;
using FluentAssertions;
using QuantitativeWorld.TestAbstractions;
using System.Collections.Generic;
using Xunit;

namespace QuantitativeWorld.Tests
{
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
                double squareMetres = Fixture.Create<double>();

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
                var area = new Area(testData.Value, testData.Unit);

                // assert
                area.SquareMetres.Should().BeApproximately(testData.ExpectedMetres, DoublePrecision);
                area.Value.Should().Be(testData.Value);
                area.Unit.Should().Be(testData.Unit);
            }
            private static IEnumerable<ConstructorForValueAndUnitTestData> GetConstructorForValueAndUnitTestData()
            {
                yield return new ConstructorForValueAndUnitTestData(1d, AreaUnit.SquareMetre, 1d);
                yield return new ConstructorForValueAndUnitTestData(1000000d, AreaUnit.SquareMillimetre, 1d);
                yield return new ConstructorForValueAndUnitTestData(1 / (0.00064516d), AreaUnit.SquareInch, 1d);
                yield return new ConstructorForValueAndUnitTestData(1 / (0.00064516d * 144d), AreaUnit.SquareFoot, 1d);
                yield return new ConstructorForValueAndUnitTestData(1 / (0.00064516d * 144d * 9d), AreaUnit.SquareYard, 1d);
                yield return new ConstructorForValueAndUnitTestData(1 / (0.00064516d * 4014489600d), AreaUnit.SquareMile, 1d);
            }
            public class ConstructorForValueAndUnitTestData
            {
                public ConstructorForValueAndUnitTestData(double value, AreaUnit unit, double expectedSquareMetres)
                {
                    Value = value;
                    Unit = unit;
                    ExpectedMetres = expectedSquareMetres;
                }

                public double Value { get; }
                public AreaUnit Unit { get; }
                public double ExpectedMetres { get; }
            }

            [Theory]
            [InlineData(0.000001, 1000000)]
            [InlineData(1, 1000000000000)]
            [InlineData(1000, 1000000000000000)]
            public void FromSquareKilometres_ShouldCreateValidArea(double squareKilometres, double squareMillimetres)
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
