using AutoFixture;
using FluentAssertions;
using QuantitativeWorld.TestAbstractions;
using Xunit;

namespace QuantitativeWorld.Tests
{
    partial class PowerTests
    {
        public class Equality : PowerTests
        {
            public Equality(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void DefaultPower_ShouldBeEqualToZeroWatts()
            {
                // arrange
                var defaultPower = default(Power);
                var zeroWattsPower = new Power(0m);

                // act
                // assert
                zeroWattsPower.Equals(defaultPower).Should().BeTrue(because: "'new Power(0m)' should be equal 'default(Power)'");
                defaultPower.Equals(zeroWattsPower).Should().BeTrue(because: "'default(Power)' should be equal 'new Power(0m)'");
            }

            [Fact]
            public void PowerCreateUtinsParamlessConstructor_ShouldBeEqualToZeroWatts()
            {
                // arrange
                var zeroWattsPower = new Power(0m);
                var paramlessConstructedPower = new Power();

                // act
                // assert
                zeroWattsPower.Equals(paramlessConstructedPower).Should().BeTrue(because: "'new Power(0m)' should be equal 'new Power()'");
                paramlessConstructedPower.Equals(zeroWattsPower).Should().BeTrue(because: "'new Power()' should be equal 'new Power(0m)'");
            }

            [Fact]
            public void ZeroUnitsPower_ShouldBeEqualToZeroWatts()
            {
                // arrange
                var zeroWattsPower = new Power(0m);
                var zeroUnitsPower = new Power(0m, CreateUnitOtherThan(PowerUnit.Watt));

                // act
                // assert
                zeroWattsPower.Equals(zeroUnitsPower).Should().BeTrue(because: "'new Power(0m)' should be equal 'new Power(0m, SomeUnit)'");
                zeroUnitsPower.Equals(zeroWattsPower).Should().BeTrue(because: "'new Power(0m, SomeUnit)' should be equal 'new Power(0m)'");
            }

            [Fact]
            public void PowersOfDifferentUnitsEqualInWatts_ShouldBeEqual()
            {
                // arrange
                var power1 = new Power(
                    value: Fixture.Create<decimal>(),
                    unit: PowerUnit.Kilowatt);
                var power2 = new Power(
                    value: power1.Watts * 0.000001m,
                    unit: PowerUnit.Megawatt);

                // act
                bool equalsResult = power1.Equals(power2);

                // assert
                equalsResult.Should().BeTrue(because: $"{power1.Value} {power1.Unit} ({power1.Watts} ,) == {power2.Value} {power2.Unit} ({power2.Watts} ,)");
            }
        }
    }
}
