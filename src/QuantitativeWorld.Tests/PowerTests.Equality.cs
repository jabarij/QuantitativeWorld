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
                var zeroWattsPower = new Power(Constants.Zero);

                // act
                // assert
                zeroWattsPower.Equals(defaultPower).Should().BeTrue(because: "'new Power(Constants.Zero)' should be equal 'default(Power)'");
                defaultPower.Equals(zeroWattsPower).Should().BeTrue(because: "'default(Power)' should be equal 'new Power(Constants.Zero)'");
            }

            [Fact]
            public void PowerCreateUsingParamlessConstructor_ShouldBeEqualToZeroWatts()
            {
                // arrange
                var zeroWattsPower = new Power(Constants.Zero);
                var paramlessConstructedPower = new Power();

                // act
                // assert
                zeroWattsPower.Equals(paramlessConstructedPower).Should().BeTrue(because: "'new Power(Constants.Zero)' should be equal 'new Power()'");
                paramlessConstructedPower.Equals(zeroWattsPower).Should().BeTrue(because: "'new Power()' should be equal 'new Power(Constants.Zero)'");
            }

            [Fact]
            public void ZeroUnitsPower_ShouldBeEqualToZeroWatts()
            {
                // arrange
                var zeroWattsPower = new Power(Constants.Zero);
                var zeroUnitsPower = new Power(Constants.Zero, CreateUnitOtherThan(PowerUnit.Watt));

                // act
                // assert
                zeroWattsPower.Equals(zeroUnitsPower).Should().BeTrue(because: "'new Power(Constants.Zero)' should be equal 'new Power(Constants.Zero, SomeUnit)'");
                zeroUnitsPower.Equals(zeroWattsPower).Should().BeTrue(because: "'new Power(Constants.Zero, SomeUnit)' should be equal 'new Power(Constants.Zero)'");
            }

            [Fact]
            public void PowersConvertedToDifferentUnitsEqualInWatts_ShouldBeEqual()
            {
                // arrange
                var power1 = new Power(Fixture.Create<number>()).Convert(Fixture.Create<PowerUnit>());
                var power2 = new Power(power1.Watts).Convert(CreateUnitOtherThan(power1.Unit));

                // act
                bool equalsResult = power1.Equals(power2);

                // assert
                equalsResult.Should().BeTrue();
            }
        }
    }
}
