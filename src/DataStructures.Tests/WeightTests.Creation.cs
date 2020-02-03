using AutoFixture;
using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace QuantitativeWorld.Tests
{
    partial class WeightTests
    {
        public class Creation : WeightTests
        {
            public Creation(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void ConstructorForKilograms_ShouldCreateValidWeight()
            {
                // arrange
                decimal kilograms = Fixture.Create<decimal>();

                // act
                var weight = new Weight(kilograms);

                // assert
                weight.Kilograms.Should().Be(kilograms);
                weight.Value.Should().Be(kilograms);
                weight.Unit.Should().Be(WeightUnit.Kilogram);
            }

            [Theory]
            [MemberData(nameof(GetTestData), typeof(Creation), nameof(GetConstructorForValueAndUnitTestData))]
            public void ConstructorForValueAndUnit_ShouldCreateValidWeight(ConstructorForValueAndUnitTestData testData)
            {
                // arrange
                // act
                var weight = new Weight(testData.Value, testData.Unit);

                // assert
                weight.Kilograms.Should().Be(testData.ExpectedKilograms);
                weight.Value.Should().Be(testData.Value);
                weight.Unit.Should().Be(testData.Unit);
            }
            private static IEnumerable<ConstructorForValueAndUnitTestData> GetConstructorForValueAndUnitTestData()
            {
                yield return new ConstructorForValueAndUnitTestData(1m, WeightUnit.Kilogram, 1m);
                yield return new ConstructorForValueAndUnitTestData(1000m, WeightUnit.Gram, 1m);
                yield return new ConstructorForValueAndUnitTestData(100m, WeightUnit.Decagram, 1m);
                yield return new ConstructorForValueAndUnitTestData(1 / 0.45359237m, WeightUnit.Pound, 1m);
                yield return new ConstructorForValueAndUnitTestData(1 / (0.45359237m / 16m), WeightUnit.Ounce, 1m);
            }
            public class ConstructorForValueAndUnitTestData
            {
                public ConstructorForValueAndUnitTestData(decimal value, WeightUnit unit, decimal expectedKilograms)
                {
                    Value = value;
                    Unit = unit;
                    ExpectedKilograms = expectedKilograms;
                }

                public decimal Value { get; }
                public WeightUnit Unit { get; }
                public decimal ExpectedKilograms { get; }
            }

            [Theory]
            [InlineData(0.001, 1000)]
            [InlineData(1, 1000000)]
            [InlineData(1000, 1000000000)]
            public void FromTons_ShouldCreateValidWeight(decimal tons, decimal grams)
            {
                // arrange
                var expectedWeight = new Weight(grams, WeightUnit.Gram);

                // act
                var actualWeight = new Weight(tons, WeightUnit.Ton);

                // assert
                actualWeight.Should().Be(expectedWeight);
            }
        }
    }
}
