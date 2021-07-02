using AutoFixture;
using FluentAssertions;
using System;
using Xunit;

#if DECIMAL
namespace DecimalQuantitativeWorld.Tests
{
    using DecimalQuantitativeWorld.TestAbstractions;
    using Constants = DecimalConstants;
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
        public class Operator_Oposite : WeightTests
        {
            public Operator_Oposite(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void ShouldProduceValidResultInSameUnit()
            {
                // arrange
                var weight = CreateWeightInUnitOtherThan(WeightUnit.Kilogram);

                // act
                var result = -weight;

                // assert
                result.Kilograms.Should().Be(-weight.Kilograms);
                result.Value.Should().Be(-weight.Value);
                result.Unit.Should().Be(weight.Unit);
            }

            [Fact]
            public void NullWeight_ShouldReturnNull()
            {
                // arrange
                Weight? weight = null;

                // act
                var result = -weight;

                // assert
                result.Should().BeNull();
            }
        }

        public class Operator_Add : WeightTests
        {
            public Operator_Add(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void TwoDefaultWeights_ShouldProduceDefaultWeight()
            {
                // arrange
                var weight1 = default(Weight);
                var weight2 = default(Weight);

                // act
                var result = weight1 + weight2;

                // assert
                result.Should().Be(default(Weight));
            }

            [Fact]
            public void DefaultWeightAndZeroWithOtherUnit_ShouldProduceZeroWithOtherUnit()
            {
                // arrange
                var defaultWeight = default(Weight);
                var zeroTons = new Weight(Constants.Zero, WeightUnit.Ton);

                // act
                var result1 = defaultWeight + zeroTons;
                var result2 = zeroTons + defaultWeight;

                // assert
                result1.IsZero().Should().BeTrue();
                result1.Unit.Should().Be(zeroTons.Unit);
                result2.IsZero().Should().BeTrue();
                result2.Unit.Should().Be(zeroTons.Unit);
            }

            [Fact]
            public void ShouldProduceValidResultInUnitOfLeftOperand()
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

            [Fact]
            public void NullWeights_ShouldReturnNull()
            {
                // arrange
                Weight? nullWeight1 = null;
                Weight? nullWeight2 = null;

                // act
                var result = nullWeight1 + nullWeight2;

                // assert
                result.Should().BeNull();
            }

            [Fact]
            public void NullAndWeight_ShouldTreatNullAsDefault()
            {
                // arrange
                Weight? nullWeight = null;
                var weight = CreateWeightInUnitOtherThan(WeightUnit.Kilogram);

                // act
                var result1 = weight + nullWeight;
                var result2 = nullWeight + weight;

                // assert
                result1.Should().NotBeNull();
                result1.Value.Kilograms.Should().Be(weight.Kilograms);
                result1.Value.Unit.Should().Be(weight.Unit);

                result2.Should().NotBeNull();
                result2.Value.Kilograms.Should().Be(weight.Kilograms);
                result2.Value.Unit.Should().Be(weight.Unit);
            }
        }

        public class Operator_Subtract : WeightTests
        {
            public Operator_Subtract(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void TwoDefaultWeights_ShouldProduceDefaultWeight()
            {
                // arrange
                var weight1 = default(Weight);
                var weight2 = default(Weight);

                // act
                var result = weight1 - weight2;

                // assert
                result.Should().Be(default(Weight));
            }

            [Fact]
            public void DefaultWeightAndZeroWithOtherUnit_ShouldProduceZeroWithOtherUnit()
            {
                // arrange
                var defaultWeight = default(Weight);
                var zeroTons = new Weight(Constants.Zero, WeightUnit.Ton);

                // act
                var result1 = defaultWeight - zeroTons;
                var result2 = zeroTons - defaultWeight;

                // assert
                result1.IsZero().Should().BeTrue();
                result1.Unit.Should().Be(WeightUnit.Ton);
                result2.IsZero().Should().BeTrue();
                result2.Unit.Should().Be(WeightUnit.Ton);
            }

            [Fact]
            public void ShouldProduceValidResultInUnitOfLeftOperand()
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

            [Fact]
            public void NullWeights_ShouldReturnNull()
            {
                // arrange
                Weight? nullWeight1 = null;
                Weight? nullWeight2 = null;

                // act
                var result = nullWeight1 - nullWeight2;

                // assert
                result.Should().BeNull();
            }

            [Fact]
            public void NullAndWeight_ShouldTreatNullAsDefault()
            {
                // arrange
                Weight? nullWeight = null;
                var weight = CreateWeightInUnitOtherThan(WeightUnit.Kilogram);

                // act
                var result1 = weight - nullWeight;
                var result2 = nullWeight - weight;

                // assert
                result1.Should().NotBeNull();
                result1.Value.Kilograms.Should().Be(weight.Kilograms);
                result1.Value.Unit.Should().Be(weight.Unit);

                result2.Should().NotBeNull();
                result2.Value.Kilograms.Should().Be(-weight.Kilograms);
                result2.Value.Unit.Should().Be(weight.Unit);
            }
        }

        public class Operator_MultiplyByDouble : WeightTests
        {
            public Operator_MultiplyByDouble(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void ShouldProduceValidResultInSameUnit()
            {
                // arrange
                var weight = new Weight(2, WeightUnit.Ton);
                number factor = 3;

                // act
                var result = weight * factor;

                // assert
                result.Kilograms.Should().Be(weight.Kilograms * factor);
                result.Value.Should().Be(weight.Value * factor);
                result.Unit.Should().Be(weight.Unit);
            }

            [Fact]
            public void NullWeight_ShouldTreatNullAsDefault()
            {
                // arrange
                Weight? nullWeight = null;
                number factor = Fixture.Create<number>();
                var expectedResult = default(Weight) * factor;

                // act
                var result = nullWeight * factor;

                // assert
                result.Should().NotBeNull();
                result.Value.Should().Be(expectedResult);
            }

#if !DECIMAL
            [Fact]
            public void MultiplyByNaN_ShouldTreatNullAsDefault()
            {
                // arrange
                var weight = CreateWeightInUnitOtherThan(WeightUnit.Kilogram);

                // act
                Func<Weight> multiplyByNaN = () => weight * number.NaN;

                // assert
                multiplyByNaN.Should().Throw<ArgumentException>();
            }
#endif
        }

        public class Operator_DivideByNumber : WeightTests
        {
            public Operator_DivideByNumber(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void DivideByZero_ShouldThrow()
            {
                // arrange
                var weight = CreateWeightInUnitOtherThan(WeightUnit.Kilogram);

                // act
                Func<Weight> divideByZero = () => weight / Constants.Zero;

                // assert
                divideByZero.Should().Throw<DivideByZeroException>();
            }

#if !DECIMAL
            [Fact]
            public void DivideByNaN_ShouldThrow()
            {
                // arrange
                var weight = CreateWeightInUnitOtherThan(WeightUnit.Kilogram);

                // act
                Func<Weight> divideByNaN = () => weight / number.NaN;

                // assert
                divideByNaN.Should().Throw<ArgumentException>();
            }
#endif

            [Fact]
            public void ShouldProduceValidResultInSameUnit()
            {
                // arrange
                var weight = CreateWeightInUnitOtherThan(WeightUnit.Kilogram);
                number denominator = Fixture.CreateNonZeroNumber();

                // act
                var result = weight / denominator;

                // assert
                result.Kilograms.Should().BeApproximately(weight.Kilograms / denominator);
                result.Unit.Should().Be(weight.Unit);
            }

            [Fact]
            public void NullWeight_ShouldTreatNullAsDefault()
            {
                // arrange
                Weight? nullWeight = null;
                number denominator = Fixture.CreateNonZeroNumber();
                var expectedResult = default(Weight) / denominator;

                // act
                var result = nullWeight / denominator;

                // assert
                result.Should().NotBeNull();
                result.Value.Should().Be(expectedResult);
            }
        }

        public class Operator_DivideByWeight : WeightTests
        {
            public Operator_DivideByWeight(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void DivideByZero_ShouldThrow()
            {
                // arrange
                var weight = CreateWeightInUnitOtherThan(WeightUnit.Kilogram);
                var denominator = new Weight(Constants.Zero);

                // act
                Func<number> divideByZero = () => weight / denominator;

                // assert
                divideByZero.Should().Throw<DivideByZeroException>();
            }

            [Fact]
            public void DivideByNull_ShouldThrow()
            {
                // arrange
                var weight = CreateWeightInUnitOtherThan(WeightUnit.Kilogram);
                Weight? denominator = null;

                // act
                Func<number> divideByZero = () => weight / denominator;

                // assert
                divideByZero.Should().Throw<DivideByZeroException>();
            }

            [Fact]
            public void ShouldProduceValidResult()
            {
                // arrange
                var nominator = CreateWeightInUnitOtherThan(WeightUnit.Kilogram);
                var denominator = new Weight(
                    value: Fixture.CreateNonZeroNumber(),
                    unit: CreateUnitOtherThan(WeightUnit.Kilogram, nominator.Unit));

                // act
                number result = nominator / denominator;

                // assert
                result.Should().BeApproximately(nominator.Kilograms / denominator.Kilograms, because: $"{nominator} divide by {denominator} shoud return {nominator.Kilograms / denominator.Kilograms} kg");
            }
        }
    }
}
