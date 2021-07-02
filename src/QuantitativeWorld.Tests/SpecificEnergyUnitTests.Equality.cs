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
#endif

    partial class SpecificEnergyUnitTests
    {
        public class Equality : SpecificEnergyUnitTests
        {
            public Equality(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void DefaultSpecificEnergyUnit_ShouldBeEqualToJoulePerKilogram()
            {
                // arrange
                var defaultSpecificEnergyUnit = default(SpecificEnergyUnit);
                var metre = SpecificEnergyUnit.JoulePerKilogram;

                // act
                // assert
                metre.Equals(defaultSpecificEnergyUnit).Should().BeTrue(because: "'SpecificEnergyUnit.JoulePerKilogram' should be equal 'default(SpecificEnergyUnit)'");
                defaultSpecificEnergyUnit.Equals(metre).Should().BeTrue(because: "'default(SpecificEnergyUnit)' should be equal 'SpecificEnergyUnit.JoulePerKilogram'");
            }

            [Fact]
            public void ParamlessConstructedSpecificEnergyUnit_ShouldBeEqualToJoulePerKilogram()
            {
                // arrange
                var paramlessConstructedSpecificEnergyUnit = new SpecificEnergyUnit();
                var metre = SpecificEnergyUnit.JoulePerKilogram;

                // act
                // assert
                metre.Equals(paramlessConstructedSpecificEnergyUnit).Should().BeTrue(because: "'SpecificEnergyUnit.JoulePerKilogram' should be equal 'new SpecificEnergyUnit()'");
                paramlessConstructedSpecificEnergyUnit.Equals(metre).Should().BeTrue(because: "'new SpecificEnergyUnit()' should be equal 'SpecificEnergyUnit.JoulePerKilogram'");
            }
        }
    }
}
