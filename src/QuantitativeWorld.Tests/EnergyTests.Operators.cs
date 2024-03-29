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


    partial class EnergyTests
    {
        public class Operator_Oposite : EnergyTests
        {
            public Operator_Oposite(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void ShouldProduceValidResultInSameUnit()
            {
                // arrange
                var energy = CreateEnergyInUnitOtherThan(EnergyUnit.Joule);

                // act
                var result = -energy;

                // assert
                result.Joules.Should().Be(-energy.Joules);
                result.Value.Should().Be(-energy.Value);
                result.Unit.Should().Be(energy.Unit);
            }

            [Fact]
            public void NullEnergy_ShouldReturnNull()
            {
                // arrange
                Energy? energy = null;

                // act
                var result = -energy;

                // assert
                result.Should().BeNull();
            }
        }

        public class Operator_Add : EnergyTests
        {
            public Operator_Add(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void TwoDefaultEnergys_ShouldProduceDefaultEnergy()
            {
                // arrange
                var energy1 = default(Energy);
                var energy2 = default(Energy);

                // act
                var result = energy1 + energy2;

                // assert
                result.Should().Be(default(Energy));
            }

            [Fact]
            public void DefaultEnergyAndZeroWithOtherUnit_ShouldProduceZeroWithOtherUnit()
            {
                // arrange
                var defaultEnergy = default(Energy);
                var zeroKilojoules = new Energy(Constants.Zero, EnergyUnit.Kilojoule);

                // act
                var result1 = defaultEnergy + zeroKilojoules;
                var result2 = zeroKilojoules + defaultEnergy;

                // assert
                result1.IsZero().Should().BeTrue();
                result1.Unit.Should().Be(zeroKilojoules.Unit);
                result2.IsZero().Should().BeTrue();
                result2.Unit.Should().Be(zeroKilojoules.Unit);
            }

            [Fact]
            public void ShouldProduceValidResultInUnitOfLeftOperand()
            {
                // arrange
                var energy1 = CreateEnergyInUnitOtherThan(EnergyUnit.Joule);
                var energy2 = CreateEnergyInUnitOtherThan(EnergyUnit.Joule, energy1.Unit);

                // act
                var result = energy1 + energy2;

                // assert
                result.Joules.Should().Be(energy1.Joules + energy2.Joules);
                result.Unit.Should().Be(energy1.Unit);
            }

            [Fact]
            public void NullEnergys_ShouldReturnNull()
            {
                // arrange
                Energy? nullEnergy1 = null;
                Energy? nullEnergy2 = null;

                // act
                var result = nullEnergy1 + nullEnergy2;

                // assert
                result.Should().BeNull();
            }

            [Fact]
            public void NullAndEnergy_ShouldTreatNullAsDefault()
            {
                // arrange
                Energy? nullEnergy = null;
                var energy = CreateEnergyInUnitOtherThan(EnergyUnit.Joule);

                // act
                var result1 = energy + nullEnergy;
                var result2 = nullEnergy + energy;

                // assert
                result1.Should().NotBeNull();
                result1.Value.Joules.Should().Be(energy.Joules);
                result1.Value.Unit.Should().Be(energy.Unit);

                result2.Should().NotBeNull();
                result2.Value.Joules.Should().Be(energy.Joules);
                result2.Value.Unit.Should().Be(energy.Unit);
            }
        }

        public class Operator_Subtract : EnergyTests
        {
            public Operator_Subtract(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void TwoDefaultEnergys_ShouldProduceDefaultEnergy()
            {
                // arrange
                var energy1 = default(Energy);
                var energy2 = default(Energy);

                // act
                var result = energy1 - energy2;

                // assert
                result.Should().Be(default(Energy));
            }

            [Fact]
            public void DefaultEnergyAndZeroWithOtherUnit_ShouldProduceZeroWithOtherUnit()
            {
                // arrange
                var defaultEnergy = default(Energy);
                var zeroKilojoules = new Energy(Constants.Zero, EnergyUnit.Kilojoule);

                // act
                var result1 = defaultEnergy - zeroKilojoules;
                var result2 = zeroKilojoules - defaultEnergy;

                // assert
                result1.IsZero().Should().BeTrue();
                result1.Unit.Should().Be(zeroKilojoules.Unit);
                result2.IsZero().Should().BeTrue();
                result2.Unit.Should().Be(zeroKilojoules.Unit);
            }

            [Fact]
            public void ShouldProduceValidResultInUnitOfLeftOperand()
            {
                // arrange
                var energy1 = CreateEnergyInUnitOtherThan(EnergyUnit.Joule);
                var energy2 = CreateEnergyInUnitOtherThan(EnergyUnit.Joule, energy1.Unit);

                // act
                var result = energy1 - energy2;

                // assert
                result.Joules.Should().Be(energy1.Joules - energy2.Joules);
                result.Unit.Should().Be(energy1.Unit);
            }

            [Fact]
            public void NullEnergys_ShouldReturnNull()
            {
                // arrange
                Energy? nullEnergy1 = null;
                Energy? nullEnergy2 = null;

                // act
                var result = nullEnergy1 - nullEnergy2;

                // assert
                result.Should().BeNull();
            }

            [Fact]
            public void NullAndEnergy_ShouldTreatNullAsDefault()
            {
                // arrange
                Energy? nullEnergy = null;
                var energy = CreateEnergyInUnitOtherThan(EnergyUnit.Joule);

                // act
                var result1 = energy - nullEnergy;
                var result2 = nullEnergy - energy;

                // assert
                result1.Should().NotBeNull();
                result1.Value.Joules.Should().Be(energy.Joules);
                result1.Value.Unit.Should().Be(energy.Unit);

                result2.Should().NotBeNull();
                result2.Value.Joules.Should().Be(-energy.Joules);
                result2.Value.Unit.Should().Be(energy.Unit);
            }
        }

        public class Operator_MultiplyByDouble : EnergyTests
        {
            public Operator_MultiplyByDouble(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void ShouldProduceValidResultInSameUnit()
            {
                // arrange
                var energy = CreateEnergyInUnitOtherThan(EnergyUnit.Joule, EnergyUnit.Electronvolt);
                number factor = Fixture.Create<number>();

                // act
                var result = energy * factor;

                // assert
                result.Joules.Should().BeApproximately(energy.Joules * factor);
                result.Value.Should().BeApproximately(energy.Value * factor);
                result.Unit.Should().Be(energy.Unit);
            }

            [Fact]
            public void NullEnergy_ShouldTreatNullAsDefault()
            {
                // arrange
                Energy? nullEnergy = null;
                number factor = Fixture.Create<number>();
                var expectedResult = default(Energy) * factor;

                // act
                var result = nullEnergy * factor;

                // assert
                result.Should().NotBeNull();
                result.Value.Should().Be(expectedResult);
            }

#if !DECIMAL
            [Fact]
            public void MultiplyByNaN_ShouldTreatNullAsDefault()
            {
                // arrange
                var energy = CreateEnergyInUnitOtherThan(EnergyUnit.Joule);

                // act
                Func<Energy> multiplyByNaN = () => energy * double.NaN;

                // assert
                multiplyByNaN.Should().Throw<ArgumentException>();
            }
#endif
        }

        public class Operator_DivideByNumber : EnergyTests
        {
            public Operator_DivideByNumber(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void DivideByZero_ShouldThrow()
            {
                // arrange
                var energy = CreateEnergyInUnitOtherThan(EnergyUnit.Joule);

                // act
                Func<Energy> divideByZero = () => energy / Constants.Zero;

                // assert
                divideByZero.Should().Throw<DivideByZeroException>();
            }

#if !DECIMAL
            [Fact]
            public void DivideByNaN_ShouldThrow()
            {
                // arrange
                var energy = CreateEnergyInUnitOtherThan(EnergyUnit.Joule);

                // act
                Func<Energy> divideByNaN = () => energy / double.NaN;

                // assert
                divideByNaN.Should().Throw<ArgumentException>();
            }
#endif

            [Fact]
            public void ShouldProduceValidResultInSameUnit()
            {
                // arrange
                var energy = CreateEnergyInUnitOtherThan(EnergyUnit.Joule);
                number denominator = (number)Fixture.CreateNonZeroNumber();

                // act
                var result = energy / denominator;

                // assert
                result.Joules.Should().BeApproximately(energy.Joules / (number)denominator);
                result.Unit.Should().Be(energy.Unit);
            }

            [Fact]
            public void NullEnergy_ShouldTreatNullAsDefault()
            {
                // arrange
                Energy? nullEnergy = null;
                number denominator = (number)Fixture.CreateNonZeroNumber();
                var expectedResult = default(Energy) / denominator;

                // act
                var result = nullEnergy * denominator;

                // assert
                result.Should().NotBeNull();
                result.Value.Should().Be(expectedResult);
            }
        }

        public class Operator_DivideByEnergy : EnergyTests
        {
            public Operator_DivideByEnergy(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void DivideByZero_ShouldThrow()
            {
                // arrange
                var energy = CreateEnergyInUnitOtherThan(EnergyUnit.Joule);
                var denominator = new Energy(Constants.Zero);

                // act
                Func<number> divideByZero = () => energy / denominator;

                // assert
                divideByZero.Should().Throw<DivideByZeroException>();
            }

            [Fact]
            public void DivideByNull_ShouldThrow()
            {
                // arrange
                var energy = CreateEnergyInUnitOtherThan(EnergyUnit.Joule);
                Energy? denominator = null;

                // act
                Func<number> divideByZero = () => energy / denominator;

                // assert
                divideByZero.Should().Throw<DivideByZeroException>();
            }

            [Fact]
            public void ShouldProduceValidResult()
            {
                // arrange
                var nominator = CreateEnergyInUnitOtherThan(EnergyUnit.Joule);
                var denominator = new Energy(
                    value: Fixture.CreateNonZeroNumber(),
                    unit: CreateUnitOtherThan(EnergyUnit.Joule, nominator.Unit));

                // act
                number result = nominator / denominator;

                // assert
                result.Should().BeApproximately(nominator.Joules / denominator.Joules);
            }
        }

        public class Operator_DivideByTime : EnergyTests
        {
            public Operator_DivideByTime(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void ShouldProduceValidResultInWatts()
            {
                // arrange
                var nominator = CreateEnergyInUnitOtherThan(EnergyUnit.Joule);
                var denominator = new Time(
                    totalSeconds: Fixture.CreateNonZeroNumber());

                // act
                var result = nominator / denominator;

                // assert
                result.Watts.Should().Be(nominator.Joules / denominator.TotalSeconds);
                result.Unit.Should().Be(PowerUnit.Watt);
            }

            [Fact]
            public void NullNominator_ShouldTreatNullAsDefault()
            {
                // arrange
                Energy? nominator = null;
                var denominator = new Time(
                    totalSeconds: Fixture.CreateNonZeroNumber());
                var expectedResult = default(Energy) / denominator;

                // act
                var result = nominator / denominator;

                // assert
                result.Should().NotBeNull();
                result.Value.Should().Be(expectedResult);
            }

            [Fact]
            public void NullDenominator_ShouldThrow()
            {
                // arrange
                var nominator = CreateEnergyInUnitOtherThan(EnergyUnit.Joule);
                Time? denominator = null;

                // act
                Func<Power?> result = () => nominator / denominator;

                // assert
                result.Should().Throw<DivideByZeroException>();
            }

            [Fact]
            public void NullNominatorAndDenominator_ShouldThrow()
            {
                // arrange
                Energy? nominator = null;
                Time? denominator = null;

                // act
                Func<Power?> result = () => nominator / denominator;

                // assert
                result.Should().Throw<DivideByZeroException>();
            }
        }

        public class Operator_DivideByPower : EnergyTests
        {
            public Operator_DivideByPower(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void ShouldProduceValidResultInWatts()
            {
                // arrange
                var nominator = CreateEnergyInUnitOtherThan(EnergyUnit.Joule);
                var denominator = new Power(
                    watts: Fixture.CreateNonZeroNumber());

                // act
                var result = nominator / denominator;

                // assert
                result.TotalSeconds.Should().Be(nominator.Joules / denominator.Watts);
            }

            [Fact]
            public void NullNominator_ShouldTreatNullAsDefault()
            {
                // arrange
                Energy? nominator = null;
                var denominator = new Power(
                    watts: Fixture.CreateNonZeroNumber());
                var expectedResult = default(Energy) / denominator;

                // act
                var result = nominator / denominator;

                // assert
                result.Should().NotBeNull();
                result.Value.Should().Be(expectedResult);
            }

            [Fact]
            public void NullDenominator_ShouldThrow()
            {
                // arrange
                var nominator = CreateEnergyInUnitOtherThan(EnergyUnit.Joule);
                Power? denominator = null;

                // act
                Func<Time?> result = () => nominator / denominator;

                // assert
                result.Should().Throw<DivideByZeroException>();
            }

            [Fact]
            public void NullNominatorAndDenominator_ShouldThrow()
            {
                // arrange
                Energy? nominator = null;
                Power? denominator = null;

                // act
                Func<Time?> result = () => nominator / denominator;

                // assert
                result.Should().Throw<DivideByZeroException>();
            }
        }
    }
}
