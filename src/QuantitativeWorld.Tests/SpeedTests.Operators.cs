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

    partial class SpeedTests
    {
        public class Operator_Oposite : SpeedTests
        {
            public Operator_Oposite(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void ShouldProduceValidResultInSameUnit()
            {
                // arrange
                var length = CreateSpeedInUnitOtherThan(SpeedUnit.MetrePerSecond);

                // act
                var result = -length;

                // assert
                result.MetresPerSecond.Should().Be(-length.MetresPerSecond);
                result.Value.Should().Be(-length.Value);
                result.Unit.Should().Be(length.Unit);
            }

            [Fact]
            public void NullSpeed_ShouldReturnNull()
            {
                // arrange
                Speed? length = null;

                // act
                var result = -length;

                // assert
                result.Should().BeNull();
            }
        }

        public class Operator_Add : SpeedTests
        {
            public Operator_Add(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void TwoDefaultSpeeds_ShouldProduceDefaultSpeed()
            {
                // arrange
                var length1 = default(Speed);
                var length2 = default(Speed);

                // act
                var result = length1 + length2;

                // assert
                result.Should().Be(default(Speed));
            }

            [Fact]
            public void DefaultSpeedAndZeroWithOtherUnit_ShouldProduceZeroWithOtherUnit()
            {
                // arrange
                var defaultSpeed = default(Speed);
                var zeroKilometresPerHour = new Speed(Constants.Zero, SpeedUnit.KilometrePerHour);

                // act
                var result1 = defaultSpeed + zeroKilometresPerHour;
                var result2 = zeroKilometresPerHour + defaultSpeed;

                // assert
                result1.IsZero().Should().BeTrue();
                result1.Unit.Should().Be(zeroKilometresPerHour.Unit);
                result2.IsZero().Should().BeTrue();
                result2.Unit.Should().Be(zeroKilometresPerHour.Unit);
            }

            [Fact]
            public void ShouldProduceValidResultInUnitOfLeftOperand()
            {
                // arrange
                var length1 = CreateSpeedInUnitOtherThan(SpeedUnit.MetrePerSecond);
                var length2 = CreateSpeedInUnitOtherThan(SpeedUnit.MetrePerSecond, length1.Unit);

                // act
                var result = length1 + length2;

                // assert
                result.MetresPerSecond.Should().Be(length1.MetresPerSecond + length2.MetresPerSecond);
                result.Unit.Should().Be(length1.Unit);
            }

            [Fact]
            public void NullSpeeds_ShouldReturnNull()
            {
                // arrange
                Speed? nullSpeed1 = null;
                Speed? nullSpeed2 = null;

                // act
                var result = nullSpeed1 + nullSpeed2;

                // assert
                result.Should().BeNull();
            }

            [Fact]
            public void NullAndSpeed_ShouldTreatNullAsDefault()
            {
                // arrange
                Speed? nullSpeed = null;
                var length = CreateSpeedInUnitOtherThan(SpeedUnit.MetrePerSecond);

                // act
                var result1 = length + nullSpeed;
                var result2 = nullSpeed + length;

                // assert
                result1.Should().NotBeNull();
                result1.Value.MetresPerSecond.Should().Be(length.MetresPerSecond);
                result1.Value.Unit.Should().Be(length.Unit);

                result2.Should().NotBeNull();
                result2.Value.MetresPerSecond.Should().Be(length.MetresPerSecond);
                result2.Value.Unit.Should().Be(length.Unit);
            }
        }

        public class Operator_Subtract : SpeedTests
        {
            public Operator_Subtract(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void TwoDefaultSpeeds_ShouldProduceDefaultSpeed()
            {
                // arrange
                var length1 = default(Speed);
                var length2 = default(Speed);

                // act
                var result = length1 - length2;

                // assert
                result.Should().Be(default(Speed));
            }

            [Fact]
            public void DefaultSpeedAndZeroWithOtherUnit_ShouldProduceZeroWithOtherUnit()
            {
                // arrange
                var defaultSpeed = default(Speed);
                var zeroKilometresPerHour = new Speed(Constants.Zero, SpeedUnit.KilometrePerHour);

                // act
                var result1 = defaultSpeed - zeroKilometresPerHour;
                var result2 = zeroKilometresPerHour - defaultSpeed;

                // assert
                result1.IsZero().Should().BeTrue();
                result1.Unit.Should().Be(zeroKilometresPerHour.Unit);
                result2.IsZero().Should().BeTrue();
                result2.Unit.Should().Be(zeroKilometresPerHour.Unit);
            }

            [Fact]
            public void ShouldProduceValidResultInUnitOfLeftOperand()
            {
                // arrange
                var length1 = CreateSpeedInUnitOtherThan(SpeedUnit.MetrePerSecond);
                var length2 = CreateSpeedInUnitOtherThan(SpeedUnit.MetrePerSecond, length1.Unit);

                // act
                var result = length1 - length2;

                // assert
                result.MetresPerSecond.Should().Be(length1.MetresPerSecond - length2.MetresPerSecond);
                result.Unit.Should().Be(length1.Unit);
            }

            [Fact]
            public void NullSpeeds_ShouldReturnNull()
            {
                // arrange
                Speed? nullSpeed1 = null;
                Speed? nullSpeed2 = null;

                // act
                var result = nullSpeed1 - nullSpeed2;

                // assert
                result.Should().BeNull();
            }

            [Fact]
            public void NullAndSpeed_ShouldTreatNullAsDefault()
            {
                // arrange
                Speed? nullSpeed = null;
                var length = CreateSpeedInUnitOtherThan(SpeedUnit.MetrePerSecond);

                // act
                var result1 = length - nullSpeed;
                var result2 = nullSpeed - length;

                // assert
                result1.Should().NotBeNull();
                result1.Value.MetresPerSecond.Should().Be(length.MetresPerSecond);
                result1.Value.Unit.Should().Be(length.Unit);

                result2.Should().NotBeNull();
                result2.Value.MetresPerSecond.Should().Be(-length.MetresPerSecond);
                result2.Value.Unit.Should().Be(length.Unit);
            }
        }

        public class Operator_MultiplyByDouble : SpeedTests
        {
            public Operator_MultiplyByDouble(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void ShouldProduceValidResultInSameUnit()
            {
                // arrange
                var length = CreateSpeedInUnitOtherThan(SpeedUnit.MetrePerSecond);
                number factor = Fixture.Create<number>();

                // act
                var result = length * factor;

                // assert
                result.MetresPerSecond.Should().BeApproximately(length.MetresPerSecond * factor);
                result.Value.Should().BeApproximately(length.Value * factor);
                result.Unit.Should().Be(length.Unit);
            }

            [Fact]
            public void NullSpeed_ShouldTreatNullAsDefault()
            {
                // arrange
                Speed? nullSpeed = null;
                number factor = Fixture.Create<number>();
                var expectedResult = default(Speed) * factor;

                // act
                var result = nullSpeed * factor;

                // assert
                result.Should().NotBeNull();
                result.Value.Should().Be(expectedResult);
            }

#if !DECIMAL
            [Fact]
            public void MultiplyByNaN_ShouldTreatNullAsDefault()
            {
                // arrange
                var length = CreateSpeedInUnitOtherThan(SpeedUnit.MetrePerSecond);

                // act
                Func<Speed> multiplyByNaN = () => length * double.NaN;

                // assert
                multiplyByNaN.Should().Throw<ArgumentException>();
            }
#endif
        }

        public class Operator_MultiplyByTime : SpeedTests
        {
            public Operator_MultiplyByTime(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void ShouldProduceValidResultInSameUnit()
            {
                // arrange
                var length = CreateSpeedInUnitOtherThan(SpeedUnit.MetrePerSecond);
                var time = new Time(totalSeconds: Fixture.CreateNonZeroNumber());

                // act
                var result = length * time;

                // assert
                result.Metres.Should().Be(length.MetresPerSecond * time.TotalSeconds);
                result.Value.Should().Be(length.MetresPerSecond * time.TotalSeconds);
                result.Unit.Should().Be(LengthUnit.Metre);
            }

            [Fact]
            public void NullSpeed_ShouldTreatNullAsDefault()
            {
                // arrange
                Speed? nullSpeed = null;
                Time? time = new Time(totalSeconds: Fixture.CreateNonZeroNumber());
                var expectedResult = default(Speed) * time;

                // act
                var result = nullSpeed * time;

                // assert
                result.Should().NotBeNull();
                result.Should().Be(expectedResult);
            }
        }

        public class Operator_DivideByDouble : SpeedTests
        {
            public Operator_DivideByDouble(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void DivideByZero_ShouldThrow()
            {
                // arrange
                var length = CreateSpeedInUnitOtherThan(SpeedUnit.MetrePerSecond);

                // act
                Func<Speed> divideByZero = () => length / Constants.Zero;

                // assert
                divideByZero.Should().Throw<DivideByZeroException>();
            }

#if !DECIMAL
            [Fact]
            public void DivideByNaN_ShouldThrow()
            {
                // arrange
                var length = CreateSpeedInUnitOtherThan(SpeedUnit.MetrePerSecond);

                // act
                Func<Speed> divideByNaN = () => length / double.NaN;

                // assert
                divideByNaN.Should().Throw<ArgumentException>();
            }
#endif

            [Fact]
            public void ShouldProduceValidResultInSameUnit()
            {
                // arrange
                var length = CreateSpeedInUnitOtherThan(SpeedUnit.MetrePerSecond);
                number denominator = (number)Fixture.CreateNonZeroNumber();

                // act
                var result = length / denominator;

                // assert
                result.MetresPerSecond.Should().BeApproximately(length.MetresPerSecond / (number)denominator);
                result.Unit.Should().Be(length.Unit);
            }

            [Fact]
            public void NullSpeed_ShouldTreatNullAsDefault()
            {
                // arrange
                Speed? nullSpeed = null;
                number denominator = (number)Fixture.CreateNonZeroNumber();
                var expectedResult = default(Speed) / denominator;

                // act
                var result = nullSpeed * denominator;

                // assert
                result.Should().NotBeNull();
                result.Value.Should().Be(expectedResult);
            }
        }

        public class Operator_DivideBySpeed : SpeedTests
        {
            public Operator_DivideBySpeed(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void DivideByZero_ShouldThrow()
            {
                // arrange
                var nominator = CreateSpeedInUnitOtherThan(SpeedUnit.MetrePerSecond);
                var denominator = new Speed(Constants.Zero);

                // act
                Func<number> divideByZero = () => nominator / denominator;

                // assert
                divideByZero.Should().Throw<DivideByZeroException>();
            }

            [Fact]
            public void DivideByNull_ShouldThrow()
            {
                // arrange
                var nominator = CreateSpeedInUnitOtherThan(SpeedUnit.MetrePerSecond);
                Speed? denominator = null;

                // act
                Func<number> divideByZero = () => nominator / denominator;

                // assert
                divideByZero.Should().Throw<DivideByZeroException>();
            }

            [Fact]
            public void ShouldProduceValidDoubleResult()
            {
                // arrange
                var nominator = CreateSpeedInUnitOtherThan(SpeedUnit.MetrePerSecond);
                var denominator = new Speed(
                    value: Fixture.CreateNonZeroNumber(),
                    unit: CreateUnitOtherThan(SpeedUnit.MetrePerSecond, nominator.Unit));

                // act
                number result = nominator / denominator;

                // assert
                result.Should().Be(nominator.MetresPerSecond / denominator.MetresPerSecond);
            }
        }

        public class Operator_DivideLengthBySpeed : SpeedTests
        {
            public Operator_DivideLengthBySpeed(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void DivideByZero_ShouldThrow()
            {
                // arrange
                var nominator = CreateLengthInUnitOtherThan(LengthUnit.Metre);
                var denominator = new Speed(Constants.Zero);

                // act
                Func<Time> divideByZero = () => nominator / denominator;

                // assert
                divideByZero.Should().Throw<DivideByZeroException>();
            }

            [Fact]
            public void DivideByNull_ShouldThrow()
            {
                // arrange
                var nominator = CreateLengthInUnitOtherThan(LengthUnit.Metre);
                Speed? denominator = null;

                // act
                Func<Time?> divideByZero = () => nominator / denominator;

                // assert
                divideByZero.Should().Throw<DivideByZeroException>();
            }

            [Fact]
            public void ShouldProduceValidDoubleResult()
            {
                // arrange
                var nominator = CreateLengthInUnitOtherThan(LengthUnit.Metre);
                var denominator = new Speed(
                    value: Fixture.CreateNonZeroNumber(),
                    unit: CreateUnitOtherThan(SpeedUnit.MetrePerSecond));

                // act
                var result = nominator / denominator;

                // assert
                result.TotalSeconds.Should().Be(nominator.Metres / denominator.MetresPerSecond);
            }
        }
    }
}
