using AutoFixture;
using FluentAssertions;
using QuantitativeWorld.TestAbstractions;
using System.Collections.Generic;
using Xunit;

namespace QuantitativeWorld.Tests
{
    partial class LengthTests
    {
        public class Creation : LengthTests
        {
            public Creation(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void ConstructorForMetres_ShouldCreateValidLength()
            {
                // arrange
                decimal kilograms = Fixture.Create<decimal>();

                // act
                var length = new Length(kilograms);

                // assert
                length.Metres.Should().Be(kilograms);
                length.Value.Should().Be(kilograms);
                length.Unit.Should().Be(LengthUnit.Metre);
            }

            [Theory]
            [MemberData(nameof(GetTestData), typeof(Creation), nameof(GetConstructorForValueAndUnitTestData))]
            public void ConstructorForValueAndUnit_ShouldCreateValidLength(ConstructorForValueAndUnitTestData testData)
            {
                // arrange
                // act
                var length = new Length(testData.Value, testData.Unit);

                // assert
                length.Metres.Should().BeApproximately(testData.ExpectedMetres, DecimalPrecision);
                length.Value.Should().Be(testData.Value);
                length.Unit.Should().Be(testData.Unit);
            }
            private static IEnumerable<ConstructorForValueAndUnitTestData> GetConstructorForValueAndUnitTestData()
            {
                yield return new ConstructorForValueAndUnitTestData(1m, LengthUnit.Metre, 1m);
                yield return new ConstructorForValueAndUnitTestData(1000m, LengthUnit.Millimetre, 1m);
                yield return new ConstructorForValueAndUnitTestData(10m, LengthUnit.Decimetre, 1m);
                yield return new ConstructorForValueAndUnitTestData(1 / 0.0254m, LengthUnit.Inch, 1m);
                yield return new ConstructorForValueAndUnitTestData(1 / (0.0254m * 12m), LengthUnit.Foot, 1m);
                yield return new ConstructorForValueAndUnitTestData(1 / (0.0254m * 12m * 3m), LengthUnit.Yard, 1m);
                yield return new ConstructorForValueAndUnitTestData(1 / (0.0254m * 12m * 3m * 5.5m), LengthUnit.Rod, 1m);
                yield return new ConstructorForValueAndUnitTestData(1 / (0.0254m * 12m * 3m * 5.5m * 4m), LengthUnit.Chain, 1m);
                yield return new ConstructorForValueAndUnitTestData(1 / (0.0254m * 12m * 3m * 5.5m * 4m * 10m), LengthUnit.Furlong, 1m);
                yield return new ConstructorForValueAndUnitTestData(1 / (0.0254m * 12m * 3m * 5.5m * 4m * 10m * 8m), LengthUnit.Mile, 1m);
                yield return new ConstructorForValueAndUnitTestData(0.0006213711922373339696174342m, LengthUnit.Mile, 1m);
            }
            public class ConstructorForValueAndUnitTestData
            {
                public ConstructorForValueAndUnitTestData(decimal value, LengthUnit unit, decimal expectedMetres)
                {
                    Value = value;
                    Unit = unit;
                    ExpectedMetres = expectedMetres;
                }

                public decimal Value { get; }
                public LengthUnit Unit { get; }
                public decimal ExpectedMetres { get; }
            }

            [Theory]
            [InlineData(0.001, 1000)]
            [InlineData(1, 1000000)]
            [InlineData(1000, 1000000000)]
            public void FromKilometres_ShouldCreateValidLength(decimal kilometres, decimal millimetres)
            {
                // arrange
                var expectedLength = new Length(millimetres, LengthUnit.Millimetre);

                // act
                var actualLength = new Length(kilometres, LengthUnit.Kilometre);

                // assert
                actualLength.Should().Be(expectedLength);
            }
        }
    }
}
