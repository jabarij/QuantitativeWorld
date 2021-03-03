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
    using Constants = DoubleConstants;
    using number = System.Double;
#endif

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
                number kilograms = Fixture.Create<number>();

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
                var weight = new Weight(testData.OriginalValue.Value, testData.OriginalValue.Unit);

                // assert
                weight.Kilograms.Should().BeApproximately(testData.ExpectedValue.Kilograms);
                weight.Value.Should().BeApproximately(testData.OriginalValue.Value);
                weight.Unit.Should().Be(testData.OriginalValue.Unit);
            }
            private static IEnumerable<ConstructorForValueAndUnitTestData> GetConstructorForValueAndUnitTestData()
            {
                yield return new ConstructorForValueAndUnitTestData(1m, WeightUnit.Kilogram, 1m);
                yield return new ConstructorForValueAndUnitTestData(1000m, WeightUnit.Gram, 1m);
                yield return new ConstructorForValueAndUnitTestData(100m, WeightUnit.Decagram, 1m);
                yield return new ConstructorForValueAndUnitTestData(1 / 0.45359237m, WeightUnit.Pound, 1m);
                yield return new ConstructorForValueAndUnitTestData(1 / (0.45359237m / 16m), WeightUnit.Ounce, 1m);
            }
            public class ConstructorForValueAndUnitTestData : ConversionTestData<Weight>
            {
                public ConstructorForValueAndUnitTestData(double value, WeightUnit unit, double expectedKilograms)
                    : base(new Weight((number)value, unit), new Weight((number)expectedKilograms)) { }
                public ConstructorForValueAndUnitTestData(decimal value, WeightUnit unit, decimal expectedKilograms)
                    : base(new Weight((number)value, unit), new Weight((number)expectedKilograms)) { }
            }

            [Theory]
            [InlineData(0.001, 1000)]
            [InlineData(1, 1000000)]
            [InlineData(1000, 1000000000)]
            public void FromTons_ShouldCreateValidWeight(number tons, number grams)
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
