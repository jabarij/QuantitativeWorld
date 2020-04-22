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
                var zeroWattsPower = new Power(0d);

                // act
                // assert
                zeroWattsPower.Equals(defaultPower).Should().BeTrue(because: "'new Power(0d)' should be equal 'default(Power)'");
                defaultPower.Equals(zeroWattsPower).Should().BeTrue(because: "'default(Power)' should be equal 'new Power(0d)'");
            }

            [Fact]
            public void PowerCreateUtinsParamlessConstructor_ShouldBeEqualToZeroWatts()
            {
                // arrange
                var zeroWattsPower = new Power(0d);
                var paramlessConstructedPower = new Power();

                // act
                // assert
                zeroWattsPower.Equals(paramlessConstructedPower).Should().BeTrue(because: "'new Power(0d)' should be equal 'new Power()'");
                paramlessConstructedPower.Equals(zeroWattsPower).Should().BeTrue(because: "'new Power()' should be equal 'new Power(0d)'");
            }

            [Fact]
            public void ZeroUnitsPower_ShouldBeEqualToZeroWatts()
            {
                // arrange
                var zeroWattsPower = new Power(0d);
                var zeroUnitsPower = new Power(0d, CreateUnitOtherThan(PowerUnit.Watt));

                // act
                // assert
                zeroWattsPower.Equals(zeroUnitsPower).Should().BeTrue(because: "'new Power(0d)' should be equal 'new Power(0d, SomeUnit)'");
                zeroUnitsPower.Equals(zeroWattsPower).Should().BeTrue(because: "'new Power(0d, SomeUnit)' should be equal 'new Power(0d)'");
            }

            [Fact]
            public void PowersConvertedToDifferentUnitsEqualInWatts_ShouldBeEqual()
            {
                // arrange
                var power1 = new Power(Fixture.Create<double>()).Convert(Fixture.Create<PowerUnit>());
                var power2 = new Power(power1.Watts).Convert(CreateUnitOtherThan(power1.Unit));

                // act
                bool equalsResult = power1.Equals(power2);

                // assert
                equalsResult.Should().BeTrue();
            }
        }
    }
}
