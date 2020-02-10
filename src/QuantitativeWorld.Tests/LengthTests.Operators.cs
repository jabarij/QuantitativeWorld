using AutoFixture;
using FluentAssertions;
using QuantitativeWorld.TestAbstractions;
using System;
using Xunit;

namespace QuantitativeWorld.Tests
{
    partial class LengthTests
    {
        public class Operator_Oposite : LengthTests
        {
            public Operator_Oposite(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void ShouldProduceValidResultInSameUnit()
            {
                // arrange
                var length = CreateLengthInUnitOtherThan(LengthUnit.Metre);

                // act
                var result = -length;

                // assert
                result.Metres.Should().Be(-length.Metres);
                result.Value.Should().Be(-length.Value);
                result.Unit.Should().Be(length.Unit);
            }

            [Fact]
            public void NullLength_ShouldReturnNull()
            {
                // arrange
                Length? length = null;

                // act
                var result = -length;

                // assert
                result.Should().BeNull();
            }
        }

        public class Operator_Add : LengthTests
        {
            public Operator_Add(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void TwoDefaultLengths_ShouldProduceDefaultLength()
            {
                // arrange
                var length1 = default(Length);
                var length2 = default(Length);

                // act
                var result = length1 + length2;

                // assert
                result.Should().Be(default(Length));
            }

            [Fact]
            public void DefaultLengthAndZeroWithOtherUnit_ShouldProduceZeroWithOtherUnit()
            {
                // arrange
                var defaultLength = default(Length);
                var zeroKilometres = new Length(0m, LengthUnit.Kilometre);

                // act
                var result1 = defaultLength + zeroKilometres;
                var result2 = zeroKilometres + defaultLength;

                // assert
                result1.IsZero().Should().BeTrue();
                result1.Unit.Should().Be(zeroKilometres.Unit);
                result2.IsZero().Should().BeTrue();
                result2.Unit.Should().Be(zeroKilometres.Unit);
            }

            [Fact]
            public void LengthsOfSameUnit_ShouldProduceValidResultInSameUnit()
            {
                // arrange
                var length1 = CreateLengthInUnitOtherThan(LengthUnit.Metre);
                var length2 = CreateLengthInUnit(length1.Unit);

                // act
                var result = length1 + length2;

                // assert
                result.Metres.Should().Be(length1.Metres + length2.Metres);
                result.Value.Should().Be(length1.Value + length2.Value);
                result.Unit.Should().Be(length1.Unit);
            }

            [Fact]
            public void LengthOfDifferentUnits_ShouldProduceValidResultInUnitOfLeftOperand()
            {
                // arrange
                var length1 = CreateLengthInUnitOtherThan(LengthUnit.Metre);
                var length2 = CreateLengthInUnitOtherThan(LengthUnit.Metre, length1.Unit);

                // act
                var result = length1 + length2;

                // assert
                result.Metres.Should().Be(length1.Metres + length2.Metres);
                result.Unit.Should().Be(length1.Unit);
            }

            [Fact]
            public void NullLengths_ShouldReturnNull()
            {
                // arrange
                Length? nullLength1 = null;
                Length? nullLength2 = null;

                // act
                var result = nullLength1 + nullLength2;

                // assert
                result.Should().BeNull();
            }

            [Fact]
            public void NullAndLength_ShouldTreatNullAsDefault()
            {
                // arrange
                Length? nullLength = null;
                var length = CreateLengthInUnitOtherThan(LengthUnit.Metre);

                // act
                var result1 = length + nullLength;
                var result2 = nullLength + length;

                // assert
                result1.Should().NotBeNull();
                result1.Value.Metres.Should().Be(length.Metres);
                result1.Value.Unit.Should().Be(length.Unit);

                result2.Should().NotBeNull();
                result2.Value.Metres.Should().Be(length.Metres);
                result2.Value.Unit.Should().Be(length.Unit);
            }
        }

        public class Operator_Subtract : LengthTests
        {
            public Operator_Subtract(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void TwoDefaultLengths_ShouldProduceDefaultLength()
            {
                // arrange
                var length1 = default(Length);
                var length2 = default(Length);

                // act
                var result = length1 - length2;

                // assert
                result.Should().Be(default(Length));
            }

            [Fact]
            public void DefaultLengthAndZeroWithOtherUnit_ShouldProduceZeroWithOtherUnit()
            {
                // arrange
                var defaultLength = default(Length);
                var zeroKilometres = new Length(0m, LengthUnit.Kilometre);

                // act
                var result1 = defaultLength - zeroKilometres;
                var result2 = zeroKilometres - defaultLength;

                // assert
                result1.IsZero().Should().BeTrue();
                result1.Unit.Should().Be(zeroKilometres.Unit);
                result2.IsZero().Should().BeTrue();
                result2.Unit.Should().Be(zeroKilometres.Unit);
            }

            [Fact]
            public void LengthsOfSameUnit_ShouldProduceValidResultInSameUnit()
            {
                // arrange
                var length1 = CreateLengthInUnitOtherThan(LengthUnit.Metre);
                var length2 = CreateLengthInUnit(length1.Unit);

                // act
                var result = length1 - length2;

                // assert
                result.Metres.Should().Be(length1.Metres - length2.Metres);
                result.Value.Should().Be(length1.Value - length2.Value);
                result.Unit.Should().Be(length1.Unit);
            }

            [Fact]
            public void LengthOfDifferentUnits_ShouldProduceValidResultInUnitOfLeftOperand()
            {
                // arrange
                var length1 = CreateLengthInUnitOtherThan(LengthUnit.Metre);
                var length2 = CreateLengthInUnitOtherThan(LengthUnit.Metre, length1.Unit);

                // act
                var result = length1 - length2;

                // assert
                result.Metres.Should().Be(length1.Metres - length2.Metres);
                result.Unit.Should().Be(length1.Unit);
            }

            [Fact]
            public void NullLengths_ShouldReturnNull()
            {
                // arrange
                Length? nullLength1 = null;
                Length? nullLength2 = null;

                // act
                var result = nullLength1 - nullLength2;

                // assert
                result.Should().BeNull();
            }

            [Fact]
            public void NullAndLength_ShouldTreatNullAsDefault()
            {
                // arrange
                Length? nullLength = null;
                var length = CreateLengthInUnitOtherThan(LengthUnit.Metre);

                // act
                var result1 = length - nullLength;
                var result2 = nullLength - length;

                // assert
                result1.Should().NotBeNull();
                result1.Value.Metres.Should().Be(length.Metres);
                result1.Value.Unit.Should().Be(length.Unit);

                result2.Should().NotBeNull();
                result2.Value.Metres.Should().Be(-length.Metres);
                result2.Value.Unit.Should().Be(length.Unit);
            }
        }

        public class Operator_MultiplyByDecimal : LengthTests
        {
            public Operator_MultiplyByDecimal(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void ShouldProduceValidResultInSameUnit()
            {
                // arrange
                var length = CreateLengthInUnitOtherThan(LengthUnit.Metre);
                decimal factor = Fixture.Create<decimal>();

                // act
                var result = length * factor;

                // assert
                result.Metres.Should().Be(length.Metres * factor);
                result.Value.Should().Be(length.Value * factor);
                result.Unit.Should().Be(length.Unit);
            }

            [Fact]
            public void NullLength_ShouldTreatNullAsDefault()
            {
                // arrange
                Length? nullLength = null;
                decimal factor = Fixture.Create<decimal>();
                var expectedResult = default(Length) * factor;

                // act
                var result = nullLength * factor;

                // assert
                result.Should().NotBeNull();
                result.Value.Should().Be(expectedResult);
            }
        }

        public class Operator_DivideByDecimal : LengthTests
        {
            public Operator_DivideByDecimal(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void DivideByZero_ShouldThrow()
            {
                // arrange
                var length = CreateLengthInUnitOtherThan(LengthUnit.Metre);

                // act
                Func<Length> divideByZero = () => length / decimal.Zero;

                // assert
                divideByZero.Should().Throw<DivideByZeroException>();
            }

            [Fact]
            public void ShouldProduceValidResultInSameUnit()
            {
                // arrange
                var length = CreateLengthInUnitOtherThan(LengthUnit.Metre);
                decimal denominator = Fixture.CreateNonZeroDecimal();

                // act
                var result = length / denominator;

                // assert
                result.Metres.Should().BeApproximately(length.Metres / denominator, DecimalPrecision);
                result.Unit.Should().Be(length.Unit);
            }

            [Fact]
            public void NullLength_ShouldTreatNullAsDefault()
            {
                // arrange
                Length? nullLength = null;
                decimal denominator = Fixture.CreateNonZeroDecimal();
                var expectedResult = default(Length) / denominator;

                // act
                var result = nullLength * denominator;

                // assert
                result.Should().NotBeNull();
                result.Value.Should().Be(expectedResult);
            }
        }

        public class Operator_DivideByLength : LengthTests
        {
            public Operator_DivideByLength(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void DivideByZero_ShouldThrow()
            {
                // arrange
                var length = CreateLengthInUnitOtherThan(LengthUnit.Metre);
                var denominator = new Length(decimal.Zero);

                // act
                Func<decimal> divideByZero = () => length / denominator;

                // assert
                divideByZero.Should().Throw<DivideByZeroException>();
            }

            [Fact]
            public void DivideByNull_ShouldThrow()
            {
                // arrange
                var length = CreateLengthInUnitOtherThan(LengthUnit.Metre);
                Length? denominator = null;

                // act
                Func<decimal> divideByZero = () => length / denominator;

                // assert
                divideByZero.Should().Throw<DivideByZeroException>();
            }

            [Fact]
            public void ShouldProduceValidDecimalResult()
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
