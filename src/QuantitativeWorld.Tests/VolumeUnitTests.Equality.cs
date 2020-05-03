using FluentAssertions;
using QuantitativeWorld.TestAbstractions;
using Xunit;

namespace QuantitativeWorld.Tests
{
    partial class VolumeUnitTests
    {
        public class Equality : VolumeUnitTests
        {
            public Equality(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void DefaultVolumeUnit_ShouldBeEqualToMetre()
            {
                // arrange
                var defaultVolumeUnit = default(VolumeUnit);
                var metre = VolumeUnit.CubicMetre;

                // act
                // assert
                metre.Equals(defaultVolumeUnit).Should().BeTrue(because: "'VolumeUnit.CubicMetre' should be equal 'default(VolumeUnit)'");
                defaultVolumeUnit.Equals(metre).Should().BeTrue(because: "'default(VolumeUnit)' should be equal 'VolumeUnit.CubicMetre'");
            }

            [Fact]
            public void ParamlessConstructedVolumeUnit_ShouldBeEqualToMetre()
            {
                // arrange
                var paramlessConstructedVolumeUnit = new VolumeUnit();
                var metre = VolumeUnit.CubicMetre;

                // act
                // assert
                metre.Equals(paramlessConstructedVolumeUnit).Should().BeTrue(because: "'VolumeUnit.CubicMetre' should be equal 'new VolumeUnit()'");
                paramlessConstructedVolumeUnit.Equals(metre).Should().BeTrue(because: "'new VolumeUnit()' should be equal 'VolumeUnit.CubicMetre'");
            }
        }
    }
}
