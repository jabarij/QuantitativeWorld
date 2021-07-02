using AutoFixture;
using FluentAssertions;
using System;
using Xunit;

#if DECIMAL
namespace DecimalQuantitativeWorld.Tests.Angular
{
    using DecimalQuantitativeWorld.Angular;
    using DecimalQuantitativeWorld.TestAbstractions;
    using Constants = DecimalConstants;
    using number = Decimal;
#else
namespace QuantitativeWorld.Tests.Angular
{
    using QuantitativeWorld.Angular;
    using QuantitativeWorld.TestAbstractions;
    using Constants = DoubleConstants;
    using number = Double;
#endif

    partial class AngleTests
    {
        public class Operator_Oposite : AngleTests
        {
            public Operator_Oposite(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void ShouldProduceValidResultInSameUnit()
            {
                // arrange
                var angle = CreateAngleInUnitOtherThan(AngleUnit.Turn);

                // act
                var result = -angle;

                // assert
                result.Turns.Should().Be(-angle.Turns);
                result.Value.Should().Be(-angle.Value);
                result.Unit.Should().Be(angle.Unit);
            }

            [Fact]
            public void NullAngle_ShouldReturnNull()
            {
                // arrange
                Angle? angle = null;

                // act
                var result = -angle;

                // assert
                result.Should().BeNull();
            }
        }

        public class Operator_Add : AngleTests
        {
            public Operator_Add(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void TwoDefaultAngles_ShouldProduceDefaultAngle()
            {
                // arrange
                var angle1 = default(Angle);
                var angle2 = default(Angle);

                // act
                var result = angle1 + angle2;

                // assert
                result.Should().Be(default(Angle));
            }

            [Fact]
            public void DefaultAngleAndZeroWithOtherUnit_ShouldProduceZeroWithOtherUnit()
            {
                // arrange
                var defaultAngle = default(Angle);
                var zeroDegrees = new Angle(Constants.Zero, AngleUnit.Degree);

                // act
                var result1 = defaultAngle + zeroDegrees;
                var result2 = zeroDegrees + defaultAngle;

                // assert
                result1.IsZero().Should().BeTrue();
                result1.Unit.Should().Be(zeroDegrees.Unit);
                result2.IsZero().Should().BeTrue();
                result2.Unit.Should().Be(zeroDegrees.Unit);
            }

            [Fact]
            public void ShouldProduceValidResultInUnitOfLeftOperand()
            {
                // arrange
                var angle1 = CreateAngleInUnitOtherThan(AngleUnit.Turn);
                var angle2 = CreateAngleInUnitOtherThan(AngleUnit.Turn, angle1.Unit);

                // act
                var result = angle1 + angle2;

                // assert
                result.Turns.Should().Be(angle1.Turns + angle2.Turns);
                result.Unit.Should().Be(angle1.Unit);
            }

            [Fact]
            public void NullAngles_ShouldReturnNull()
            {
                // arrange
                Angle? nullAngle1 = null;
                Angle? nullAngle2 = null;

                // act
                var result = nullAngle1 + nullAngle2;

                // assert
                result.Should().BeNull();
            }

            [Fact]
            public void NullAndAngle_ShouldTreatNullAsDefault()
            {
                // arrange
                Angle? nullAngle = null;
                var angle = CreateAngleInUnitOtherThan(AngleUnit.Turn);

                // act
                var result1 = angle + nullAngle;
                var result2 = nullAngle + angle;

                // assert
                result1.Should().NotBeNull();
                result1.Value.Turns.Should().Be(angle.Turns);
                result1.Value.Unit.Should().Be(angle.Unit);

                result2.Should().NotBeNull();
                result2.Value.Turns.Should().Be(angle.Turns);
                result2.Value.Unit.Should().Be(angle.Unit);
            }
        }

        public class Operator_Subtract : AngleTests
        {
            public Operator_Subtract(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void TwoDefaultAngles_ShouldProduceDefaultAngle()
            {
                // arrange
                var angle1 = default(Angle);
                var angle2 = default(Angle);

                // act
                var result = angle1 - angle2;

                // assert
                result.Should().Be(default(Angle));
            }

            [Fact]
            public void DefaultAngleAndZeroWithOtherUnit_ShouldProduceZeroWithOtherUnit()
            {
                // arrange
                var defaultAngle = default(Angle);
                var zeroDegrees = new Angle(Constants.Zero, AngleUnit.Degree);

                // act
                var result1 = defaultAngle - zeroDegrees;
                var result2 = zeroDegrees - defaultAngle;

                // assert
                result1.IsZero().Should().BeTrue();
                result1.Unit.Should().Be(zeroDegrees.Unit);
                result2.IsZero().Should().BeTrue();
                result2.Unit.Should().Be(zeroDegrees.Unit);
            }

            [Fact]
            public void ShouldProduceValidResultInUnitOfLeftOperand()
            {
                // arrange
                var angle1 = CreateAngleInUnitOtherThan(AngleUnit.Turn);
                var angle2 = CreateAngleInUnitOtherThan(AngleUnit.Turn, angle1.Unit);

                // act
                var result = angle1 - angle2;

                // assert
                result.Turns.Should().Be(angle1.Turns - angle2.Turns);
                result.Unit.Should().Be(angle1.Unit);
            }

            [Fact]
            public void NullAngles_ShouldReturnNull()
            {
                // arrange
                Angle? nullAngle1 = null;
                Angle? nullAngle2 = null;

                // act
                var result = nullAngle1 - nullAngle2;

                // assert
                result.Should().BeNull();
            }

            [Fact]
            public void NullAndAngle_ShouldTreatNullAsDefault()
            {
                // arrange
                Angle? nullAngle = null;
                var angle = CreateAngleInUnitOtherThan(AngleUnit.Turn);

                // act
                var result1 = angle - nullAngle;
                var result2 = nullAngle - angle;

                // assert
                result1.Should().NotBeNull();
                result1.Value.Turns.Should().Be(angle.Turns);
                result1.Value.Unit.Should().Be(angle.Unit);

                result2.Should().NotBeNull();
                result2.Value.Turns.Should().Be(-angle.Turns);
                result2.Value.Unit.Should().Be(angle.Unit);
            }
        }

        public class Operator_MultiplyByDouble : AngleTests
        {
            public Operator_MultiplyByDouble(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void ShouldProduceValidResultInSameUnit()
            {
                // arrange
                var angle = CreateAngleInUnitOtherThan(AngleUnit.Turn);
                number factor = Fixture.Create<number>();

                // act
                var result = angle * factor;

                // assert
                result.Turns.Should().BeApproximately(angle.Turns * factor);
                result.Value.Should().BeApproximately(angle.Value * factor);
                result.Unit.Should().Be(angle.Unit);
            }

            [Fact]
            public void NullAngle_ShouldTreatNullAsDefault()
            {
                // arrange
                Angle? nullAngle = null;
                number factor = Fixture.Create<number>();
                var expectedResult = default(Angle) * factor;

                // act
                var result = nullAngle * factor;

                // assert
                result.Should().NotBeNull();
                result.Value.Should().Be(expectedResult);
            }
        }

        public class Operator_DivideByNumber : AngleTests
        {
            public Operator_DivideByNumber(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void DivideByZero_ShouldThrow()
            {
                // arrange
                var angle = CreateAngleInUnitOtherThan(AngleUnit.Turn);

                // act
                Func<Angle> divideByZero = () => angle / Constants.Zero;

                // assert
                divideByZero.Should().Throw<DivideByZeroException>();
            }

            [Fact]
            public void ShouldProduceValidResultInSameUnit()
            {
                // arrange
                var angle = CreateAngleInUnitOtherThan(AngleUnit.Turn);
                number denominator = Fixture.CreateNonZeroNumber();

                // act
                var result = angle / denominator;

                // assert
                result.Turns.Should().BeApproximately(angle.Turns / denominator);
                result.Unit.Should().Be(angle.Unit);
            }

            [Fact]
            public void NullAngle_ShouldTreatNullAsDefault()
            {
                // arrange
                Angle? nullAngle = null;
                number denominator = Fixture.CreateNonZeroNumber();
                var expectedResult = default(Angle) / denominator;

                // act
                var result = nullAngle * denominator;

                // assert
                result.Should().NotBeNull();
                result.Value.Should().Be(expectedResult);
            }
        }

        public class Operator_ModuloByDouble : AngleTests
        {
            public Operator_ModuloByDouble(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void ModuloByZero_ShouldThrow()
            {
                // arrange
                var angle = CreateAngleInUnitOtherThan(AngleUnit.Turn);

                // act
                Func<Angle> moduloByZero = () => angle % Constants.Zero;

                // assert
                moduloByZero.Should().Throw<DivideByZeroException>();
            }

            [Fact]
            public void ShouldProduceValidResultInSameUnit()
            {
                // arrange
                var angle = CreateAngleInUnitOtherThan(AngleUnit.Turn);
                number denominator = Fixture.CreateNonZeroNumber();

                // act
                var result = angle % denominator;

                // assert
                result.Value.Should().BeApproximately(angle.Value % denominator);
                result.Unit.Should().Be(angle.Unit);
            }

            [Fact]
            public void NullAngle_ShouldTreatNullAsDefault()
            {
                // arrange
                Angle? nullAngle = null;
                number denominator = Fixture.CreateNonZeroNumber();
                var expectedResult = default(Angle) % denominator;

                // act
                var result = nullAngle * denominator;

                // assert
                result.Should().NotBeNull();
                result.Value.Should().Be(expectedResult);
            }
        }

        public class Operator_DivideByAngle : AngleTests
        {
            public Operator_DivideByAngle(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void DivideByZero_ShouldThrow()
            {
                // arrange
                var angle = CreateAngleInUnitOtherThan(AngleUnit.Turn);
                var denominator = new Angle(Constants.Zero);

                // act
                Func<number> divideByZero = () => angle / denominator;

                // assert
                divideByZero.Should().Throw<DivideByZeroException>();
            }

            [Fact]
            public void DivideByNull_ShouldThrow()
            {
                // arrange
                var angle = CreateAngleInUnitOtherThan(AngleUnit.Turn);
                Angle? denominator = null;

                // act
                Func<number> divideByZero = () => angle / denominator;

                // assert
                divideByZero.Should().Throw<DivideByZeroException>();
            }

            [Fact]
            public void ShouldProduceValidResult()
            {
                // arrange
                var nominator = CreateAngleInUnitOtherThan(AngleUnit.Turn);
                var denominator = new Angle(
                    value: Fixture.CreateNonZeroNumber(),
                    unit: CreateUnitOtherThan(AngleUnit.Turn, nominator.Unit));

                // act
                number result = nominator / denominator;

                // assert
                result.Should().BeApproximately(nominator.Turns / denominator.Turns);
            }
        }
    }
}
