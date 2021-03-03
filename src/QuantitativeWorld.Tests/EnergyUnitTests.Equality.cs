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

    partial class EnergyUnitTests
    {
        public class Equality : EnergyUnitTests
        {
            public Equality(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void DefaultEnergyUnit_ShouldBeEqualToJoule()
            {
                // arrange
                var defaultEnergyUnit = default(EnergyUnit);
                var watt = EnergyUnit.Joule;

                // act
                // assert
                watt.Equals(defaultEnergyUnit).Should().BeTrue(because: "'EnergyUnit.Joule' should be equal 'default(EnergyUnit)'");
                defaultEnergyUnit.Equals(watt).Should().BeTrue(because: "'default(EnergyUnit)' should be equal 'EnergyUnit.Joule'");
            }

            [Fact]
            public void ParamlessConstructedEnergyUnit_ShouldBeEqualToJoule()
            {
                // arrange
                var paramlessConstructedEnergyUnit = new EnergyUnit();
                var watt = EnergyUnit.Joule;

                // act
                // assert
                watt.Equals(paramlessConstructedEnergyUnit).Should().BeTrue(because: "'EnergyUnit.Joule' should be equal 'new EnergyUnit()'");
                paramlessConstructedEnergyUnit.Equals(watt).Should().BeTrue(because: "'new EnergyUnit()' should be equal 'EnergyUnit.Joule'");
            }
        }
    }
}
