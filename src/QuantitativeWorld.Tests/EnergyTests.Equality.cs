using AutoFixture;
using FluentAssertions;
using QuantitativeWorld.TestAbstractions;
using Xunit;

namespace QuantitativeWorld.Tests
{
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
                var zeroJoulesEnergy = new Energy(0d);

                // act
                // assert
                zeroJoulesEnergy.Equals(defaultEnergy).Should().BeTrue(because: "'new Energy(0d)' should be equal 'default(Energy)'");
                defaultEnergy.Equals(zeroJoulesEnergy).Should().BeTrue(because: "'default(Energy)' should be equal 'new Energy(0d)'");
            }

            [Fact]
            public void EnergyCreateUsingParamlessConstructor_ShouldBeEqualToZeroJoules()
            {
                // arrange
                var zeroJoulesEnergy = new Energy(0d);
                var paramlessConstructedEnergy = new Energy();

                // act
                // assert
                zeroJoulesEnergy.Equals(paramlessConstructedEnergy).Should().BeTrue(because: "'new Energy(0d)' should be equal 'new Energy()'");
                paramlessConstructedEnergy.Equals(zeroJoulesEnergy).Should().BeTrue(because: "'new Energy()' should be equal 'new Energy(0d)'");
            }

            [Fact]
            public void ZeroUnitsEnergy_ShouldBeEqualToZeroJoules()
            {
                // arrange
                var zeroJoulesEnergy = new Energy(0d);
                var zeroUnitsEnergy = new Energy(0d, CreateUnitOtherThan(EnergyUnit.Joule));

                // act
                // assert
                zeroJoulesEnergy.Equals(zeroUnitsEnergy).Should().BeTrue(because: "'new Energy(0d)' should be equal 'new Energy(0d, SomeUnit)'");
                zeroUnitsEnergy.Equals(zeroJoulesEnergy).Should().BeTrue(because: "'new Energy(0d, SomeUnit)' should be equal 'new Energy(0d)'");
            }

            [Fact]
            public void EnergysConvertedToDifferentUnitsEqualInJoules_ShouldBeEqual()
            {
                // arrange
                var energy1 = new Energy(Fixture.Create<double>()).Convert(Fixture.Create<EnergyUnit>());
                var energy2 = new Energy(energy1.Joules).Convert(CreateUnitOtherThan(energy1.Unit));

                // act
                bool equalsResult = energy1.Equals(energy2);

                // assert
                equalsResult.Should().BeTrue();
            }
        }
    }
}
