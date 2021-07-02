using AutoFixture;
using FluentAssertions;
using System;
using Xunit;

#if DECIMAL
namespace DecimalQuantitativeWorld.Tests
{
    using DecimalQuantitativeWorld.TestAbstractions;
    using Constants = DecimalConstants;
    using number = Decimal;
#else
namespace QuantitativeWorld.Tests
{
    using QuantitativeWorld.TestAbstractions;
    using Constants = DoubleConstants;
    using number = Double;
#endif

    partial class SpecificEnergyTests
    {
        public class Operator_Oposite : SpecificEnergyTests
        {
            public Operator_Oposite(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void ShouldProduceValidResultInSameUnit()
            {
                // arrange
                var specificEnergy = CreateSpecificEnergyInUnitOtherThan(SpecificEnergyUnit.JoulePerKilogram);

                // act
                var result = -specificEnergy;

                // assert
                result.JoulesPerKilogram.Should().Be(-specificEnergy.JoulesPerKilogram);
                result.Value.Should().Be(-specificEnergy.Value);
                result.Unit.Should().Be(specificEnergy.Unit);
            }

            [Fact]
            public void NullSpecificEnergy_ShouldReturnNull()
            {
                // arrange
                SpecificEnergy? specificEnergy = null;

                // act
                var result = -specificEnergy;

                // assert
                result.Should().BeNull();
            }
        }

        public class Operator_Add : SpecificEnergyTests
        {
            public Operator_Add(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void TwoDefaultSpecificEnergys_ShouldProduceDefaultSpecificEnergy()
            {
                // arrange
                var specificEnergy1 = default(SpecificEnergy);
                var specificEnergy2 = default(SpecificEnergy);

                // act
                var result = specificEnergy1 + specificEnergy2;

                // assert
                result.Should().Be(default(SpecificEnergy));
            }

            [Fact]
            public void DefaultSpecificEnergyAndZeroWithOtherUnit_ShouldProduceZeroWithOtherUnit()
            {
                // arrange
                var defaultSpecificEnergy = default(SpecificEnergy);
                var zeroKilojoulesPerKilogram = new SpecificEnergy(Constants.Zero, SpecificEnergyUnit.KilojoulePerKilogram);

                // act
                var result1 = defaultSpecificEnergy + zeroKilojoulesPerKilogram;
                var result2 = zeroKilojoulesPerKilogram + defaultSpecificEnergy;

                // assert
                result1.IsZero().Should().BeTrue();
                result1.Unit.Should().Be(zeroKilojoulesPerKilogram.Unit);
                result2.IsZero().Should().BeTrue();
                result2.Unit.Should().Be(zeroKilojoulesPerKilogram.Unit);
            }

            [Fact]
            public void ShouldProduceValidResultInUnitOfLeftOperand()
            {
                // arrange
                var specificEnergy1 = CreateSpecificEnergyInUnitOtherThan(SpecificEnergyUnit.JoulePerKilogram);
                var specificEnergy2 = CreateSpecificEnergyInUnitOtherThan(SpecificEnergyUnit.JoulePerKilogram, specificEnergy1.Unit);

                // act
                var result = specificEnergy1 + specificEnergy2;

                // assert
                result.JoulesPerKilogram.Should().Be(specificEnergy1.JoulesPerKilogram + specificEnergy2.JoulesPerKilogram);
                result.Unit.Should().Be(specificEnergy1.Unit);
            }

            [Fact]
            public void NullSpecificEnergys_ShouldReturnNull()
            {
                // arrange
                SpecificEnergy? nullSpecificEnergy1 = null;
                SpecificEnergy? nullSpecificEnergy2 = null;

                // act
                var result = nullSpecificEnergy1 + nullSpecificEnergy2;

                // assert
                result.Should().BeNull();
            }

            [Fact]
            public void NullAndSpecificEnergy_ShouldTreatNullAsDefault()
            {
                // arrange
                SpecificEnergy? nullSpecificEnergy = null;
                var specificEnergy = CreateSpecificEnergyInUnitOtherThan(SpecificEnergyUnit.JoulePerKilogram);

                // act
                var result1 = specificEnergy + nullSpecificEnergy;
                var result2 = nullSpecificEnergy + specificEnergy;

                // assert
                result1.Should().NotBeNull();
                result1.Value.JoulesPerKilogram.Should().Be(specificEnergy.JoulesPerKilogram);
                result1.Value.Unit.Should().Be(specificEnergy.Unit);

                result2.Should().NotBeNull();
                result2.Value.JoulesPerKilogram.Should().Be(specificEnergy.JoulesPerKilogram);
                result2.Value.Unit.Should().Be(specificEnergy.Unit);
            }

            [Fact]
            public void SpecificEnergysOfEquivalentUnits_ShouldConserveValue()
            {
                // arrange
                number leftValue = Fixture.Create<number>();
                number rightValue = Fixture.Create<number>();
                var left = new SpecificEnergy(leftValue, MetricPrefix.Yocto * SpecificEnergyUnit.JoulePerKilogram);
                var right = new SpecificEnergy(rightValue, new SpecificEnergyUnit("some unit", "su", MetricPrefix.Yocto.Factor));

                // act
                var result = left + right;

                // assert
                result.Unit.Should().Be(left.Unit);
                result.Value.Should().Be(leftValue + rightValue);
            }

            [Fact]
            public void SpecificEnergysOfNotEquivalentUnits_ShouldConserveJoulesPerKilogramAndLeftUnit()
            {
                // arrange
                var left = new SpecificEnergy(Fixture.Create<number>(), MetricPrefix.Yotta * SpecificEnergyUnit.JoulePerKilogram);
                var right = new SpecificEnergy(Fixture.Create<number>(), MetricPrefix.Yocto * SpecificEnergyUnit.JoulePerKilogram);

                // act
                var result = left + right;

                // assert
                result.Unit.Should().Be(left.Unit);
                result.JoulesPerKilogram.Should().Be(left.JoulesPerKilogram + right.JoulesPerKilogram);
            }
        }

        public class Operator_Subtract : SpecificEnergyTests
        {
            public Operator_Subtract(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void TwoDefaultSpecificEnergys_ShouldProduceDefaultSpecificEnergy()
            {
                // arrange
                var specificEnergy1 = default(SpecificEnergy);
                var specificEnergy2 = default(SpecificEnergy);

                // act
                var result = specificEnergy1 - specificEnergy2;

                // assert
                result.Should().Be(default(SpecificEnergy));
            }

            [Fact]
            public void DefaultSpecificEnergyAndZeroWithOtherUnit_ShouldProduceZeroWithOtherUnit()
            {
                // arrange
                var defaultSpecificEnergy = default(SpecificEnergy);
                var zeroKilojoulesPerKilogram = new SpecificEnergy(Constants.Zero, SpecificEnergyUnit.KilojoulePerKilogram);

                // act
                var result1 = defaultSpecificEnergy - zeroKilojoulesPerKilogram;
                var result2 = zeroKilojoulesPerKilogram - defaultSpecificEnergy;

                // assert
                result1.IsZero().Should().BeTrue();
                result1.Unit.Should().Be(zeroKilojoulesPerKilogram.Unit);
                result2.IsZero().Should().BeTrue();
                result2.Unit.Should().Be(zeroKilojoulesPerKilogram.Unit);
            }

            [Fact]
            public void ShouldProduceValidResultInUnitOfLeftOperand()
            {
                // arrange
                var specificEnergy1 = CreateSpecificEnergyInUnitOtherThan(SpecificEnergyUnit.JoulePerKilogram);
                var specificEnergy2 = CreateSpecificEnergyInUnitOtherThan(SpecificEnergyUnit.JoulePerKilogram, specificEnergy1.Unit);

                // act
                var result = specificEnergy1 - specificEnergy2;

                // assert
                result.JoulesPerKilogram.Should().Be(specificEnergy1.JoulesPerKilogram - specificEnergy2.JoulesPerKilogram);
                result.Unit.Should().Be(specificEnergy1.Unit);
            }

            [Fact]
            public void NullSpecificEnergys_ShouldReturnNull()
            {
                // arrange
                SpecificEnergy? nullSpecificEnergy1 = null;
                SpecificEnergy? nullSpecificEnergy2 = null;

                // act
                var result = nullSpecificEnergy1 - nullSpecificEnergy2;

                // assert
                result.Should().BeNull();
            }

            [Fact]
            public void NullAndSpecificEnergy_ShouldTreatNullAsDefault()
            {
                // arrange
                SpecificEnergy? nullSpecificEnergy = null;
                var specificEnergy = CreateSpecificEnergyInUnitOtherThan(SpecificEnergyUnit.JoulePerKilogram);

                // act
                var result1 = specificEnergy - nullSpecificEnergy;
                var result2 = nullSpecificEnergy - specificEnergy;

                // assert
                result1.Should().NotBeNull();
                result1.Value.JoulesPerKilogram.Should().Be(specificEnergy.JoulesPerKilogram);
                result1.Value.Unit.Should().Be(specificEnergy.Unit);

                result2.Should().NotBeNull();
                result2.Value.JoulesPerKilogram.Should().Be(-specificEnergy.JoulesPerKilogram);
                result2.Value.Unit.Should().Be(specificEnergy.Unit);
            }
        }

        public class Operator_MultiplyByDouble : SpecificEnergyTests
        {
            public Operator_MultiplyByDouble(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void ShouldProduceValidResultInSameUnit()
            {
                // arrange
                var specificEnergy = CreateSpecificEnergyInUnitOtherThan(SpecificEnergyUnit.JoulePerKilogram);
                number factor = Fixture.Create<number>();

                // act
                var result = specificEnergy * factor;

                // assert
                result.JoulesPerKilogram.Should().BeApproximately(specificEnergy.JoulesPerKilogram * factor);
                result.Value.Should().BeApproximately(specificEnergy.Value * factor);
                result.Unit.Should().Be(specificEnergy.Unit);
            }

            [Fact]
            public void NullSpecificEnergy_ShouldTreatNullAsDefault()
            {
                // arrange
                SpecificEnergy? nullSpecificEnergy = null;
                number factor = Fixture.Create<number>();
                var expectedResult = default(SpecificEnergy) * factor;

                // act
                var result = nullSpecificEnergy * factor;

                // assert
                result.Should().NotBeNull();
                result.Value.Should().Be(expectedResult);
            }

#if !DECIMAL
            [Fact]
            public void MultiplyByNaN_ShouldTreatNullAsDefault()
            {
                // arrange
                var specificEnergy = CreateSpecificEnergyInUnitOtherThan(SpecificEnergyUnit.JoulePerKilogram);

                // act
                Func<SpecificEnergy> multiplyByNaN = () => specificEnergy * double.NaN;

                // assert
                multiplyByNaN.Should().Throw<ArgumentException>();
            }
#endif
        }

        public class Operator_DivideByNumber : SpecificEnergyTests
        {
            public Operator_DivideByNumber(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void DivideByZero_ShouldThrow()
            {
                // arrange
                var specificEnergy = CreateSpecificEnergyInUnitOtherThan(SpecificEnergyUnit.JoulePerKilogram);

                // act
                Func<SpecificEnergy> divideByZero = () => specificEnergy / Constants.Zero;

                // assert
                divideByZero.Should().Throw<DivideByZeroException>();
            }

#if !DECIMAL
            [Fact]
            public void DivideByNaN_ShouldThrow()
            {
                // arrange
                var specificEnergy = CreateSpecificEnergyInUnitOtherThan(SpecificEnergyUnit.JoulePerKilogram);

                // act
                Func<SpecificEnergy> divideByNaN = () => specificEnergy / double.NaN;

                // assert
                divideByNaN.Should().Throw<ArgumentException>();
            }
#endif

            [Fact]
            public void ShouldProduceValidResultInSameUnit()
            {
                // arrange
                var specificEnergy = CreateSpecificEnergyInUnitOtherThan(SpecificEnergyUnit.JoulePerKilogram);
                number denominator = (number)Fixture.CreateNonZeroNumber();

                // act
                var result = specificEnergy / denominator;

                // assert
                result.JoulesPerKilogram.Should().BeApproximately(specificEnergy.JoulesPerKilogram / (number)denominator);
                result.Unit.Should().Be(specificEnergy.Unit);
            }

            [Fact]
            public void NullSpecificEnergy_ShouldTreatNullAsDefault()
            {
                // arrange
                SpecificEnergy? nullSpecificEnergy = null;
                number denominator = (number)Fixture.CreateNonZeroNumber();
                var expectedResult = default(SpecificEnergy) / denominator;

                // act
                var result = nullSpecificEnergy * denominator;

                // assert
                result.Should().NotBeNull();
                result.Value.Should().Be(expectedResult);
            }
        }

        public class Operator_DivideBySpecificEnergy : SpecificEnergyTests
        {
            public Operator_DivideBySpecificEnergy(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void DivideByZero_ShouldThrow()
            {
                // arrange
                var specificEnergy = CreateSpecificEnergyInUnitOtherThan(SpecificEnergyUnit.JoulePerKilogram);
                var denominator = new SpecificEnergy(Constants.Zero);

                // act
                Func<number> divideByZero = () => specificEnergy / denominator;

                // assert
                divideByZero.Should().Throw<DivideByZeroException>();
            }

            [Fact]
            public void DivideByNull_ShouldThrow()
            {
                // arrange
                var specificEnergy = CreateSpecificEnergyInUnitOtherThan(SpecificEnergyUnit.JoulePerKilogram);
                SpecificEnergy? denominator = null;

                // act
                Func<number> divideByZero = () => specificEnergy / denominator;

                // assert
                divideByZero.Should().Throw<DivideByZeroException>();
            }

            [Fact]
            public void ShouldProduceValidDoubleResult()
            {
                // arrange
                var nominator = CreateSpecificEnergyInUnitOtherThan(SpecificEnergyUnit.JoulePerKilogram);
                var denominator = new SpecificEnergy(
                    value: Fixture.CreateNonZeroNumber(),
                    unit: CreateUnitOtherThan(SpecificEnergyUnit.JoulePerKilogram, nominator.Unit));

                // act
                number result = nominator / denominator;

                // assert
                result.Should().Be(nominator.JoulesPerKilogram / denominator.JoulesPerKilogram);
            }
        }
    }
}
