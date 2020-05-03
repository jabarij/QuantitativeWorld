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
                var zeroKilometres = new Length(0d, LengthUnit.Kilometre);

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
            public void ShouldProduceValidResultInUnitOfLeftOperand()
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
                var zeroKilometres = new Length(0d, LengthUnit.Kilometre);

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
            public void ShouldProduceValidResultInUnitOfLeftOperand()
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

        public class Operator_MultiplyByDouble : LengthTests
        {
            public Operator_MultiplyByDouble(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void ShouldProduceValidResultInSameUnit()
            {
                // arrange
                var length = CreateLengthInUnitOtherThan(LengthUnit.Metre);
                double factor = Fixture.Create<double>();

                // act
                var result = length * factor;

                // assert
                result.Metres.Should().BeApproximately(length.Metres * factor, DoublePrecision);
                result.Value.Should().BeApproximately(length.Value * factor, DoublePrecision);
                result.Unit.Should().Be(length.Unit);
            }

            [Fact]
            public void NullLength_ShouldTreatNullAsDefault()
            {
                // arrange
                Length? nullLength = null;
                double factor = Fixture.Create<double>();
                var expectedResult = default(Length) * factor;

                // act
                var result = nullLength * factor;

                // assert
                result.Should().NotBeNull();
                result.Value.Should().Be(expectedResult);
            }

            [Fact]
            public void MultiplyByNaN_ShouldTreatNullAsDefault()
            {
                // arrange
                var length = CreateLengthInUnitOtherThan(LengthUnit.Metre);

                // act
                Func<Length> multiplyByNaN = () => length * double.NaN;

                // assert
                multiplyByNaN.Should().Throw<ArgumentException>();
            }
        }

        public class Operator_MultiplyByLength : LengthTests
        {
            public Operator_MultiplyByLength(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void ShouldProduceValidResultInSquareMetres()
            {
                // arrange
                var argument = CreateLengthInUnitOtherThan(LengthUnit.Metre);
                var factor = CreateLengthInUnitOtherThan(LengthUnit.Metre);

                // act
                var result = argument * factor;

                // assert
                result.SquareMetres.Should().Be(argument.Metres * factor.Metres);
                result.Unit.Should().Be(AreaUnit.SquareMetre);
            }

            [Fact]
            public void NullArgument_ShouldTreatNullAsDefault()
            {
                // arrange
                Length? argument = null;
                var factor = CreateLengthInUnitOtherThan(LengthUnit.Metre);
                var expectedResult = default(Length) * factor;

                // act
                var result = argument * factor;

                // assert
                result.Should().Be(expectedResult);
            }

            [Fact]
            public void NullFactor_ShouldTreatNullAsDefault()
            {
                // arrange
                var argument = CreateLengthInUnitOtherThan(LengthUnit.Metre);
                Length? factor = null;
                var expectedResult = argument * default(Length);

                // act
                var result = argument * factor;

                // assert
                result.Should().Be(expectedResult);
            }

            [Fact]
            public void NullArgumentAndFactor_ShouldReturnNull()
            {
                // arrange
                Length? argument = null;
                Length? factor = null;

                // act
                var result = argument * factor;

                // assert
                result.Should().BeNull();
            }
        }

        public class Operator_DivideByDouble : LengthTests
        {
            public Operator_DivideByDouble(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void DivideByZero_ShouldThrow()
            {
                // arrange
                var length = CreateLengthInUnitOtherThan(LengthUnit.Metre);

                // act
                Func<Length> divideByZero = () => length / 0d;

                // assert
                divideByZero.Should().Throw<DivideByZeroException>();
            }

            [Fact]
            public void DivideByNaN_ShouldThrow()
            {
                // arrange
                var length = CreateLengthInUnitOtherThan(LengthUnit.Metre);

                // act
                Func<Length> divideByNaN = () => length / double.NaN;

                // assert
                divideByNaN.Should().Throw<ArgumentException>();
            }

            [Fact]
            public void ShouldProduceValidResultInSameUnit()
            {
                // arrange
                var length = CreateLengthInUnitOtherThan(LengthUnit.Metre);
                double denominator = (double)Fixture.CreateNonZeroDouble();

                // act
                var result = length / denominator;

                // assert
                result.Metres.Should().BeApproximately(length.Metres / (double)denominator, DoublePrecision);
                result.Unit.Should().Be(length.Unit);
            }

            [Fact]
            public void NullLength_ShouldTreatNullAsDefault()
            {
                // arrange
                Length? nullLength = null;
                double denominator = (double)Fixture.CreateNonZeroDouble();
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
                var denominator = new Length(0d);

                // act
                Func<double> divideByZero = () => length / denominator;

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
                Func<double> divideByZero = () => length / denominator;

                // assert
                divideByZero.Should().Throw<DivideByZeroException>();
            }

            [Fact]
            public void ShouldProduceValidDoubleResult()
            {
                // arrange
                var nominator = CreateLengthInUnitOtherThan(LengthUnit.Metre);
                var denominator = new Length(
                    value: Fixture.CreateNonZeroDouble(),
                    unit: CreateUnitOtherThan(LengthUnit.Metre, nominator.Unit));

                // act
                double result = nominator / denominator;

                // assert
                result.Should().Be(nominator.Metres / denominator.Metres);
            }
        }
    }
}
