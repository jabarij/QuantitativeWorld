using AutoFixture;
using FluentAssertions;
using System;
using Xunit;

namespace QuantitativeWorld.Tests
{
    partial class LengthTests
    {
        public class Operator_Add : LengthTests
        {
            public Operator_Add(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void LengthsOfSameUnit_ShouldProduceValidResultInSameUnit()
            {
                // arrange
                var weight1 = CreateLengthInUnitOtherThan(LengthUnit.Metre);
                var weight2 = CreateLengthInUnit(weight1.Unit);

                // act
                var result = weight1 + weight2;

                // assert
                result.Metres.Should().Be(weight1.Metres + weight2.Metres);
                result.Value.Should().Be(weight1.Value + weight2.Value);
                result.Unit.Should().Be(weight1.Unit);
            }

            [Fact]
            public void LengthOfDifferentUnits_ShouldProduceValidResultInUnitOfLeftOperand()
            {
                // arrange
                var weight1 = CreateLengthInUnitOtherThan(LengthUnit.Metre);
                var weight2 = CreateLengthInUnitOtherThan(LengthUnit.Metre, weight1.Unit);

                // act
                var result = weight1 + weight2;

                // assert
                result.Metres.Should().Be(weight1.Metres + weight2.Metres);
                result.Unit.Should().Be(weight1.Unit);
            }
        }

        public class Operator_Subtract : LengthTests
        {
            public Operator_Subtract(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void LengthsOfSameUnit_ShouldProduceValidResultInSameUnit()
            {
                // arrange
                var weight1 = CreateLengthInUnitOtherThan(LengthUnit.Metre);
                var weight2 = CreateLengthInUnit(weight1.Unit);

                // act
                var result = weight1 - weight2;

                // assert
                result.Metres.Should().Be(weight1.Metres - weight2.Metres);
                result.Value.Should().Be(weight1.Value - weight2.Value);
                result.Unit.Should().Be(weight1.Unit);
            }

            [Fact]
            public void LengthOfDifferentUnits_ShouldProduceValidResultInUnitOfLeftOperand()
            {
                // arrange
                var weight1 = CreateLengthInUnitOtherThan(LengthUnit.Metre);
                var weight2 = CreateLengthInUnitOtherThan(LengthUnit.Metre, weight1.Unit);

                // act
                var result = weight1 - weight2;

                // assert
                result.Metres.Should().Be(weight1.Metres - weight2.Metres);
                result.Unit.Should().Be(weight1.Unit);
            }
        }

        public class Operator_Multiply : LengthTests
        {
            public Operator_Multiply(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void MultiplyByDecimal_ShouldProduceValidResultInSameUnit()
            {
                // arrange
                var weight = CreateLengthInUnitOtherThan(LengthUnit.Metre);
                decimal factor = Fixture.Create<decimal>();

                // act
                var result = weight * factor;

                // assert
                result.Metres.Should().Be(weight.Metres * factor);
                result.Value.Should().Be(weight.Value * factor);
                result.Unit.Should().Be(weight.Unit);
            }
        }

        public class Operator_Divide : LengthTests
        {
            public Operator_Divide(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void DivideByZeroDecimal_ShouldThrow()
            {
                // arrange
                var length = CreateLengthInUnitOtherThan(LengthUnit.Metre);

                // act
                Func<Length> divideByZero = () => length / decimal.Zero;

                // assert
                divideByZero.Should().Throw<DivideByZeroException>();
            }

            [Fact]
            public void DivideByDecimal_ShouldProduceValidResultInSameUnit()
            {
                // arrange
                var weight = CreateLengthInUnitOtherThan(LengthUnit.Metre);
                decimal denominator = Fixture.CreateNonZeroDecimal();

                // act
                var result = weight / denominator;

                // assert
                result.Metres.Should().BeApproximately(weight.Metres / denominator, DecimalPrecision);
                result.Unit.Should().Be(weight.Unit);
            }

            [Fact]
            public void DivideByZeroLength_ShouldThrow()
            {
                // arrange
                var length = CreateLengthInUnitOtherThan(LengthUnit.Metre);

                // act
                Func<decimal> divideByZero = () => length / new Length(decimal.Zero);

                // assert
                divideByZero.Should().Throw<DivideByZeroException>();
            }

            [Fact]
            public void DivideByLength_ShouldProduceValidDecimalResult()
            {
                // arrange
                var nominator = CreateLengthInUnitOtherThan(LengthUnit.Metre);
                var denominator = new Length(
                    value: Fixture.CreateNonZeroDecimal(),
                    unit: CreateUnitOtherThan(LengthUnit.Metre, nominator.Unit));

                // act
                decimal result = nominator / denominator;

                // assert
                result.Should().BeApproximately(nominator.Metres / denominator.Metres, DecimalPrecision);
            }
        }
    }
}
