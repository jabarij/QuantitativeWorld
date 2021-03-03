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

    partial class EnergyTests
    {
        public class Equality : EnergyTests
        {
            public Equality(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void DefaultEnergy_ShouldBeEqualToZeroJoules()
            {
                // arrange
                var defaultEnergy = default(Energy);
                var zeroJoulesEnergy = new Energy(Constants.Zero);

                // act
                // assert
                zeroJoulesEnergy.Equals(defaultEnergy).Should().BeTrue(because: "'new Energy(Constants.Zero)' should be equal 'default(Energy)'");
                defaultEnergy.Equals(zeroJoulesEnergy).Should().BeTrue(because: "'default(Energy)' should be equal 'new Energy(Constants.Zero)'");
            }

            [Fact]
            public void EnergyCreateUsingParamlessConstructor_ShouldBeEqualToZeroJoules()
            {
                // arrange
                var zeroJoulesEnergy = new Energy(Constants.Zero);
                var paramlessConstructedEnergy = new Energy();

                // act
                // assert
                zeroJoulesEnergy.Equals(paramlessConstructedEnergy).Should().BeTrue(because: "'new Energy(Constants.Zero)' should be equal 'new Energy()'");
                paramlessConstructedEnergy.Equals(zeroJoulesEnergy).Should().BeTrue(because: "'new Energy()' should be equal 'new Energy(Constants.Zero)'");
            }

            [Fact]
            public void ZeroUnitsEnergy_ShouldBeEqualToZeroJoules()
            {
                // arrange
                var zeroJoulesEnergy = new Energy(Constants.Zero);
                var zeroUnitsEnergy = new Energy(Constants.Zero, CreateUnitOtherThan(EnergyUnit.Joule));

                // act
                // assert
                zeroJoulesEnergy.Equals(zeroUnitsEnergy).Should().BeTrue(because: "'new Energy(Constants.Zero)' should be equal 'new Energy(Constants.Zero, SomeUnit)'");
                zeroUnitsEnergy.Equals(zeroJoulesEnergy).Should().BeTrue(because: "'new Energy(Constants.Zero, SomeUnit)' should be equal 'new Energy(Constants.Zero)'");
            }

            [Fact]
            public void EnergysConvertedToDifferentUnitsEqualInJoules_ShouldBeEqual()
            {
                // arrange
                var energy1 = new Energy(Fixture.Create<number>()).Convert(Fixture.Create<EnergyUnit>());
                var energy2 = new Energy(energy1.Joules).Convert(CreateUnitOtherThan(energy1.Unit));

                // act
                bool equalsResult = energy1.Equals(energy2);

                // assert
                equalsResult.Should().BeTrue();
            }
        }
    }
}
