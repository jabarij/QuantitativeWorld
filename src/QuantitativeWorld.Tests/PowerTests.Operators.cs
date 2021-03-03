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

    partial class PowerTests
    {
        public class Operator_Oposite : PowerTests
        {
            public Operator_Oposite(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void ShouldProduceValidResultInSameUnit()
            {
                // arrange
                var power = CreatePowerInUnitOtherThan(PowerUnit.Watt);

                // act
                var result = -power;

                // assert
                result.Watts.Should().Be(-power.Watts);
                result.Value.Should().Be(-power.Value);
                result.Unit.Should().Be(power.Unit);
            }

            [Fact]
            public void NullPower_ShouldReturnNull()
            {
                // arrange
                Power? power = null;

                // act
                var result = -power;

                // assert
                result.Should().BeNull();
            }
        }

        public class Operator_Add : PowerTests
        {
            public Operator_Add(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void TwoDefaultPowers_ShouldProduceDefaultPower()
            {
                // arrange
                var power1 = default(Power);
                var power2 = default(Power);

                // act
                var result = power1 + power2;

                // assert
                result.Should().Be(default(Power));
            }

            [Fact]
            public void DefaultPowerAndZeroWithOtherUnit_ShouldProduceZeroWithOtherUnit()
            {
                // arrange
                var defaultPower = default(Power);
                var zeroKilowatts = new Power(Constants.Zero, PowerUnit.Kilowatt);

                // act
                var result1 = defaultPower + zeroKilowatts;
                var result2 = zeroKilowatts + defaultPower;

                // assert
                result1.IsZero().Should().BeTrue();
                result1.Unit.Should().Be(zeroKilowatts.Unit);
                result2.IsZero().Should().BeTrue();
                result2.Unit.Should().Be(zeroKilowatts.Unit);
            }

            [Fact]
            public void ShouldProduceValidResultInUnitOfLeftOperand()
            {
                // arrange
                var power1 = CreatePowerInUnitOtherThan(PowerUnit.Watt);
                var power2 = CreatePowerInUnitOtherThan(PowerUnit.Watt, power1.Unit);

                // act
                var result = power1 + power2;

                // assert
                result.Watts.Should().Be(power1.Watts + power2.Watts);
                result.Unit.Should().Be(power1.Unit);
            }

            [Fact]
            public void NullPowers_ShouldReturnNull()
            {
                // arrange
                Power? nullPower1 = null;
                Power? nullPower2 = null;

                // act
                var result = nullPower1 + nullPower2;

                // assert
                result.Should().BeNull();
            }

            [Fact]
            public void NullAndPower_ShouldTreatNullAsDefault()
            {
                // arrange
                Power? nullPower = null;
                var power = CreatePowerInUnitOtherThan(PowerUnit.Watt);

                // act
                var result1 = power + nullPower;
                var result2 = nullPower + power;

                // assert
                result1.Should().NotBeNull();
                result1.Value.Watts.Should().Be(power.Watts);
                result1.Value.Unit.Should().Be(power.Unit);

                result2.Should().NotBeNull();
                result2.Value.Watts.Should().Be(power.Watts);
                result2.Value.Unit.Should().Be(power.Unit);
            }
        }

        public class Operator_Subtract : PowerTests
        {
            public Operator_Subtract(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void TwoDefaultPowers_ShouldProduceDefaultPower()
            {
                // arrange
                var power1 = default(Power);
                var power2 = default(Power);

                // act
                var result = power1 - power2;

                // assert
                result.Should().Be(default(Power));
            }

            [Fact]
            public void DefaultPowerAndZeroWithOtherUnit_ShouldProduceZeroWithOtherUnit()
            {
                // arrange
                var defaultPower = default(Power);
                var zeroKilowatts = new Power(Constants.Zero, PowerUnit.Kilowatt);

                // act
                var result1 = defaultPower - zeroKilowatts;
                var result2 = zeroKilowatts - defaultPower;

                // assert
                result1.IsZero().Should().BeTrue();
                result1.Unit.Should().Be(zeroKilowatts.Unit);
                result2.IsZero().Should().BeTrue();
                result2.Unit.Should().Be(zeroKilowatts.Unit);
            }

            [Fact]
            public void ShouldProduceValidResultInUnitOfLeftOperand()
            {
                // arrange
                var power1 = CreatePowerInUnitOtherThan(PowerUnit.Watt);
                var power2 = CreatePowerInUnitOtherThan(PowerUnit.Watt, power1.Unit);

                // act
                var result = power1 - power2;

                // assert
                result.Watts.Should().Be(power1.Watts - power2.Watts);
                result.Unit.Should().Be(power1.Unit);
            }

            [Fact]
            public void NullPowers_ShouldReturnNull()
            {
                // arrange
                Power? nullPower1 = null;
                Power? nullPower2 = null;

                // act
                var result = nullPower1 - nullPower2;

                // assert
                result.Should().BeNull();
            }

            [Fact]
            public void NullAndPower_ShouldTreatNullAsDefault()
            {
                // arrange
                Power? nullPower = null;
                var power = CreatePowerInUnitOtherThan(PowerUnit.Watt);

                // act
                var result1 = power - nullPower;
                var result2 = nullPower - power;

                // assert
                result1.Should().NotBeNull();
                result1.Value.Watts.Should().Be(power.Watts);
                result1.Value.Unit.Should().Be(power.Unit);

                result2.Should().NotBeNull();
                result2.Value.Watts.Should().Be(-power.Watts);
                result2.Value.Unit.Should().Be(power.Unit);
            }
        }

        public class Operator_MultiplyByDouble : PowerTests
        {
            public Operator_MultiplyByDouble(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void ShouldProduceValidResultInSameUnit()
            {
                // arrange
                var power = CreatePowerInUnitOtherThan(PowerUnit.Watt);
                number factor = Fixture.Create<number>();

                // act
                var result = power * factor;

                // assert
                result.Watts.Should().BeApproximately(power.Watts * factor);
                result.Value.Should().BeApproximately(power.Value * factor);
                result.Unit.Should().Be(power.Unit);
            }

            [Fact]
            public void NullPower_ShouldTreatNullAsDefault()
            {
                // arrange
                Power? nullPower = null;
                number factor = Fixture.Create<number>();
                var expectedResult = default(Power) * factor;

                // act
                var result = nullPower * factor;

                // assert
                result.Should().NotBeNull();
                result.Value.Should().Be(expectedResult);
            }

#if !DECIMAL
            [Fact]
            public void MultiplyByNaN_ShouldTreatNullAsDefault()
            {
                // arrange
                var power = CreatePowerInUnitOtherThan(PowerUnit.Watt);

                // act
                Func<Power> multiplyByNaN = () => power * double.NaN;

                // assert
                multiplyByNaN.Should().Throw<ArgumentException>();
            }
#endif
        }

        public class Operator_MultiplyByTime : PowerTests
        {
            public Operator_MultiplyByTime(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void ShouldProduceValidResultInJoules()
            {
                // arrange
                var argument = CreatePowerInUnitOtherThan(PowerUnit.Watt);
                var factor = new Time(
                    totalSeconds: Fixture.CreateNonZeroNumber());

                // act
                var result = argument * factor;

                // assert
                result.Joules.Should().Be(argument.Watts * factor.TotalSeconds);
                result.Unit.Should().Be(EnergyUnit.Joule);
            }

            [Fact]
            public void NullArgument_ShouldTreatNullAsDefault()
            {
                // arrange
                Power? argument = null;
                var factor = new Time(
                    totalSeconds: Fixture.CreateNonZeroNumber());
                var expectedResult = default(Power) * factor;

                // act
                var result = argument * factor;

                // assert
                result.Should().Be(expectedResult);
            }

            [Fact]
            public void NullFactor_ShouldTreatNullAsDefault()
            {
                // arrange
                var argument = CreatePowerInUnitOtherThan(PowerUnit.Watt);
                Time? factor = null;
                var expectedResult = argument * default(Time);

                // act
                var result = argument * factor;

                // assert
                result.Should().Be(expectedResult);
            }

            [Fact]
            public void NullArgumentAndFactor_ShouldReturnNull()
            {
                // arrange
                Power? argument = null;
                Time? factor = null;

                // act
                var result = argument * factor;

                // assert
                result.Should().BeNull();
            }
        }

        public class Operator_DivideByDouble : PowerTests
        {
            public Operator_DivideByDouble(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void DivideByZero_ShouldThrow()
            {
                // arrange
                var power = CreatePowerInUnitOtherThan(PowerUnit.Watt);

                // act
                Func<Power> divideByZero = () => power / Constants.Zero;

                // assert
                divideByZero.Should().Throw<DivideByZeroException>();
            }

#if !DECIMAL
            [Fact]
            public void DivideByNaN_ShouldThrow()
            {
                // arrange
                var power = CreatePowerInUnitOtherThan(PowerUnit.Watt);

                // act
                Func<Power> divideByNaN = () => power / double.NaN;

                // assert
                divideByNaN.Should().Throw<ArgumentException>();
            }
#endif

            [Fact]
            public void ShouldProduceValidResultInSameUnit()
            {
                // arrange
                var power = CreatePowerInUnitOtherThan(PowerUnit.Watt);
                number denominator = (number)Fixture.CreateNonZeroNumber();

                // act
                var result = power / denominator;

                // assert
                result.Watts.Should().BeApproximately(power.Watts / (number)denominator);
                result.Unit.Should().Be(power.Unit);
            }

            [Fact]
            public void NullPower_ShouldTreatNullAsDefault()
            {
                // arrange
                Power? nullPower = null;
                number denominator = (number)Fixture.CreateNonZeroNumber();
                var expectedResult = default(Power) / denominator;

                // act
                var result = nullPower * denominator;

                // assert
                result.Should().NotBeNull();
                result.Value.Should().Be(expectedResult);
            }
        }

        public class Operator_DivideByPower : PowerTests
        {
            public Operator_DivideByPower(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void DivideByZero_ShouldThrow()
            {
                // arrange
                var power = CreatePowerInUnitOtherThan(PowerUnit.Watt);
                var denominator = new Power(Constants.Zero);

                // act
                Func<number> divideByZero = () => power / denominator;

                // assert
                divideByZero.Should().Throw<DivideByZeroException>();
            }

            [Fact]
            public void DivideByNull_ShouldThrow()
            {
                // arrange
                var power = CreatePowerInUnitOtherThan(PowerUnit.Watt);
                Power? denominator = null;

                // act
                Func<number> divideByZero = () => power / denominator;

                // assert
                divideByZero.Should().Throw<DivideByZeroException>();
            }

            [Fact]
            public void ShouldProduceValidResult()
            {
                // arrange
                var nominator = CreatePowerInUnitOtherThan(PowerUnit.Watt);
                var denominator = new Power(
                    value: Fixture.CreateNonZeroNumber(),
                    unit: CreateUnitOtherThan(PowerUnit.Watt, nominator.Unit));

                // act
                number result = nominator / denominator;

                // assert
                result.Should().BeApproximately(nominator.Watts / denominator.Watts);
            }
        }
    }
}
