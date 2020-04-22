using AutoFixture;
using FluentAssertions;
using QuantitativeWorld.TestAbstractions;
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
                double kilograms = Fixture.Create<double>();

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
                yield return new ConstructorForValueAndUnitTestData(1d, WeightUnit.Kilogram, 1d);
                yield return new ConstructorForValueAndUnitTestData(1000d, WeightUnit.Gram, 1d);
                yield return new ConstructorForValueAndUnitTestData(100d, WeightUnit.Decagram, 1d);
                yield return new ConstructorForValueAndUnitTestData(1 / 0.45359237d, WeightUnit.Pound, 1d);
                yield return new ConstructorForValueAndUnitTestData(1 / (0.45359237d / 16d), WeightUnit.Ounce, 1d);
            }
            public class ConstructorForValueAndUnitTestData
            {
                public ConstructorForValueAndUnitTestData(double value, WeightUnit unit, double expectedKilograms)
                {
                    Value = value;
                    Unit = unit;
                    ExpectedKilograms = expectedKilograms;
                }

                public double Value { get; }
                public WeightUnit Unit { get; }
                public double ExpectedKilograms { get; }
            }

            [Theory]
            [InlineData(0.001, 1000)]
            [InlineData(1, 1000000)]
            [InlineData(1000, 1000000000)]
            public void FromTons_ShouldCreateValidWeight(double tons, double grams)
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
