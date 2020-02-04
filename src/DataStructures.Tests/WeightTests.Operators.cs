using AutoFixture;
using FluentAssertions;
using System;
using Xunit;

namespace QuantitativeWorld.Tests
{
    partial class WeightTests
    {
        public class Operator_Add : WeightTests
        {
            public Operator_Add(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void WeightsOfSameUnit_ShouldProduceValidResultInSameUnit()
            {
                // arrange
                var weight1 = CreateWeightInUnitOtherThan(WeightUnit.Kilogram);
                var weight2 = CreateWeightInUnit(weight1.Unit);

                // act
                var result = weight1 + weight2;

                // assert
                result.Kilograms.Should().Be(weight1.Kilograms + weight2.Kilograms);
                result.Value.Should().Be(weight1.Value + weight2.Value);
                result.Unit.Should().Be(weight1.Unit);
            }

            [Fact]
            public void WeightOfDifferentUnits_ShouldProduceValidResultInUnitOfLeftOperand()
            {
                // arrange
                var weight1 = CreateWeightInUnitOtherThan(WeightUnit.Kilogram);
                var weight2 = CreateWeightInUnitOtherThan(WeightUnit.Kilogram, weight1.Unit);

                // act
                var result = weight1 + weight2;

                // assert
                result.Kilograms.Should().Be(weight1.Kilograms + weight2.Kilograms);
                result.Unit.Should().Be(weight1.Unit);
            }
        }

        public class Operator_Subtract : WeightTests
        {
            public Operator_Subtract(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void WeightsOfSameUnit_ShouldProduceValidResultInSameUnit()
            {
                // arrange
                var weight1 = CreateWeightInUnitOtherThan(WeightUnit.Kilogram);
                var weight2 = CreateWeightInUnit(weight1.Unit);

                // act
                var result = weight1 - weight2;

                // assert
                result.Kilograms.Should().Be(weight1.Kilograms - weight2.Kilograms);
                result.Value.Should().Be(weight1.Value - weight2.Value);
                result.Unit.Should().Be(weight1.Unit);
            }

            [Fact]
            public void WeightOfDifferentUnits_ShouldProduceValidResultInUnitOfLeftOperand()
            {
                // arrange
                var weight1 = CreateWeightInUnitOtherThan(WeightUnit.Kilogram);
                var weight2 = CreateWeightInUnitOtherThan(WeightUnit.Kilogram, weight1.Unit);

                // act
                var result = weight1 - weight2;

                // assert
                result.Kilograms.Should().Be(weight1.Kilograms - weight2.Kilograms);
                result.Unit.Should().Be(weight1.Unit);
            }
        }

        public class Operator_Multiply : WeightTests
        {
            public Operator_Multiply(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void MultiplyByDecimal_ShouldProduceValidResultInSameUnit()
            {
                // arrange
                var weight = CreateWeightInUnitOtherThan(WeightUnit.Kilogram);
                decimal factor = Fixture.Create<decimal>();

                // act
                var result = weight * factor;

                // assert
                result.Kilograms.Should().Be(weight.Kilograms * factor);
                result.Value.Should().Be(weight.Value * factor);
                result.Unit.Should().Be(weight.Unit);
            }
        }

        public class Operator_Divide : WeightTests
        {
            public Operator_Divide(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void DivideByZeroDecimal_ShouldThrow()
            {
                // arrange
                var weight = CreateWeightInUnitOtherThan(WeightUnit.Kilogram);

                // act
                Func<Weight> divideByZero = () => weight / decimal.Zero;

                // assert
                divideByZero.Should().Throw<DivideByZeroException>();
            }

            [Fact]
            public void DivideByDecimal_ShouldProduceValidResultInSameUnit()
            {
                // arrange
                var weight = CreateWeightInUnitOtherThan(WeightUnit.Kilogram);
                decimal denominator = Fixture.CreateNonZeroDecimal();

                // act
                var result = weight / denominator;

                // assert
                result.Kilograms.Should().BeApproximately(weight.Kilograms / denominator, DecimalPrecision);
                result.Unit.Should().Be(weight.Unit);
            }

            [Fact]
            public void DivideByZeroWeight_ShouldThrow()
            {
                // arrange
                var weight = CreateWeightInUnitOtherThan(WeightUnit.Kilogram);

                // act
                Func<decimal> divideByZero = () => weight / new Weight(decimal.Zero);

                // assert
                divideByZero.Should().Throw<DivideByZeroException>();
            }

            [Fact]
            public void DivideByWeight_ShouldProduceValidDecimalResult()
            {
                // arrange
                var nominator = CreateWeightInUnitOtherThan(WeightUnit.Kilogram);
                var denominator = new Weight(
                    value: Fixture.CreateNonZeroDecimal(),
                    unit: CreateUnitOtherThan(WeightUnit.Kilogram, nominator.Unit));

                // act
                decimal result = nominator / denominator;

                // assert
                result.Should().BeApproximately(nominator.Kilograms / denominator.Kilograms, DecimalPrecision);
            }
        }
    }
}
