using AutoFixture;
using FluentAssertions;
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

    partial class SpecificEnergyTests
    {
        public class Equality : SpecificEnergyTests
        {
            public Equality(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void DefaultSpecificEnergy_ShouldBeEqualToZeroJoulesPerKilogram()
            {
                // arrange
                var defaultSpecificEnergy = default(SpecificEnergy);
                var zeroJoulesPerKilogramSpecificEnergy = new SpecificEnergy(Constants.Zero);

                // act
                // assert
                zeroJoulesPerKilogramSpecificEnergy.Equals(defaultSpecificEnergy).Should().BeTrue(because: "'new SpecificEnergy(Constants.Zero)' should be equal 'default(SpecificEnergy)'");
                defaultSpecificEnergy.Equals(zeroJoulesPerKilogramSpecificEnergy).Should().BeTrue(because: "'default(SpecificEnergy)' should be equal 'new SpecificEnergy(Constants.Zero)'");
            }

            [Fact]
            public void SpecificEnergyCreateUsingParamlessConstructor_ShouldBeEqualToZeroJoulesPerKilogram()
            {
                // arrange
                var zeroJoulesPerKilogramSpecificEnergy = new SpecificEnergy(Constants.Zero);
                var paramlessConstructedSpecificEnergy = new SpecificEnergy();

                // act
                // assert
                zeroJoulesPerKilogramSpecificEnergy.Equals(paramlessConstructedSpecificEnergy).Should().BeTrue(because: "'new SpecificEnergy(Constants.Zero)' should be equal 'new SpecificEnergy()'");
                paramlessConstructedSpecificEnergy.Equals(zeroJoulesPerKilogramSpecificEnergy).Should().BeTrue(because: "'new SpecificEnergy()' should be equal 'new SpecificEnergy(Constants.Zero)'");
            }

            [Fact]
            public void ZeroUnitsSpecificEnergy_ShouldBeEqualToZeroJoulesPerKilogram()
            {
                // arrange
                var zeroJoulesPerKilogramSpecificEnergy = new SpecificEnergy(Constants.Zero);
                var zeroUnitsSpecificEnergy = new SpecificEnergy(Constants.Zero, CreateUnitOtherThan(SpecificEnergyUnit.JoulePerKilogram));

                // act
                // assert
                zeroJoulesPerKilogramSpecificEnergy.Equals(zeroUnitsSpecificEnergy).Should().BeTrue(because: "'new SpecificEnergy(Constants.Zero)' should be equal 'new SpecificEnergy(Constants.Zero, SomeUnit)'");
                zeroUnitsSpecificEnergy.Equals(zeroJoulesPerKilogramSpecificEnergy).Should().BeTrue(because: "'new SpecificEnergy(Constants.Zero, SomeUnit)' should be equal 'new SpecificEnergy(Constants.Zero)'");
            }

            [Fact]
            public void SpecificEnergysConvertedToDifferentUnitsEqualInJoulesPerKilogram_ShouldBeEqual()
            {
                // arrange
                var specificEnergy1 = new SpecificEnergy(Fixture.Create<number>()).Convert(Fixture.Create<SpecificEnergyUnit>());
                var specificEnergy2 = new SpecificEnergy(specificEnergy1.JoulesPerKilogram).Convert(CreateUnitOtherThan(specificEnergy1.Unit));

                // act
                bool equalsResult = specificEnergy1.Equals(specificEnergy2);

                // assert
                equalsResult.Should().BeTrue();
            }
        }
    }
}
