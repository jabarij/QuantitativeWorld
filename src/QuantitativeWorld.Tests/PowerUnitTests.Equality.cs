using FluentAssertions;
using Xunit;

#if DECIMAL
namespace DecimalQuantitativeWorld.Tests
{
    using DecimalQuantitativeWorld.TestAbstractions;
#else
namespace QuantitativeWorld.Tests
{
    using QuantitativeWorld.TestAbstractions;
    using Constants = DoubleConstants;
    using number = System.Double;
#endif

    partial class PowerUnitTests
    {
        public class Equality : PowerUnitTests
        {
            public Equality(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void DefaultPowerUnit_ShouldBeEqualToWatt()
            {
                // arrange
                var defaultPowerUnit = default(PowerUnit);
                var watt = PowerUnit.Watt;

                // act
                // assert
                watt.Equals(defaultPowerUnit).Should().BeTrue(because: "'PowerUnit.Watt' should be equal 'default(PowerUnit)'");
                defaultPowerUnit.Equals(watt).Should().BeTrue(because: "'default(PowerUnit)' should be equal 'PowerUnit.Watt'");
            }

            [Fact]
            public void ParamlessConstructedPowerUnit_ShouldBeEqualToWatt()
            {
                // arrange
                var paramlessConstructedPowerUnit = new PowerUnit();
                var watt = PowerUnit.Watt;

                // act
                // assert
                watt.Equals(paramlessConstructedPowerUnit).Should().BeTrue(because: "'PowerUnit.Watt' should be equal 'new PowerUnit()'");
                paramlessConstructedPowerUnit.Equals(watt).Should().BeTrue(because: "'new PowerUnit()' should be equal 'PowerUnit.Watt'");
            }
        }
    }
}
