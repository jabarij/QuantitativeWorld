using AutoFixture;
using FluentAssertions;
using QuantitativeWorld.TestAbstractions;
using Xunit;

namespace QuantitativeWorld.Tests
{
    partial class VolumeTests
    {
        public class Equality : VolumeTests
        {
            public Equality(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void DefaultVolume_ShouldBeEqualToZeroMetres()
            {
                // arrange
                var defaultVolume = default(Volume);
                var zeroMetresVolume = new Volume(0d);

                // act
                // assert
                zeroMetresVolume.Equals(defaultVolume).Should().BeTrue(because: "'new Volume(0d)' should be equal 'default(Volume)'");
                defaultVolume.Equals(zeroMetresVolume).Should().BeTrue(because: "'default(Volume)' should be equal 'new Volume(0d)'");
            }

            [Fact]
            public void VolumeCreateUsingParamlessConstructor_ShouldBeEqualToZeroMetres()
            {
                // arrange
                var zeroMetresVolume = new Volume(0d);
                var paramlessConstructedVolume = new Volume();

                // act
                // assert
                zeroMetresVolume.Equals(paramlessConstructedVolume).Should().BeTrue(because: "'new Volume(0d)' should be equal 'new Volume()'");
                paramlessConstructedVolume.Equals(zeroMetresVolume).Should().BeTrue(because: "'new Volume()' should be equal 'new Volume(0d)'");
            }

            [Fact]
            public void ZeroUnitsVolume_ShouldBeEqualToZeroMetres()
            {
                // arrange
                var zeroMetresVolume = new Volume(0d);
                var zeroUnitsVolume = new Volume(0d, CreateUnitOtherThan(VolumeUnit.CubicMetre));

                // act
                // assert
                zeroMetresVolume.Equals(zeroUnitsVolume).Should().BeTrue(because: "'new Volume(0d)' should be equal 'new Volume(0d, SomeUnit)'");
                zeroUnitsVolume.Equals(zeroMetresVolume).Should().BeTrue(because: "'new Volume(0d, SomeUnit)' should be equal 'new Volume(0d)'");
            }

            [Fact]
            public void VolumesConvertedToDifferentUnitsEqualInMetres_ShouldBeEqual()
            {
                // arrange
                var volume1 = new Volume(Fixture.Create<double>()).Convert(Fixture.Create<VolumeUnit>());
                var volume2 = new Volume(volume1.CubicMetres).Convert(CreateUnitOtherThan(volume1.Unit));

                // act
                bool equalsResult = volume1.Equals(volume2);

                // assert
                equalsResult.Should().BeTrue();
            }
        }
    }
}
