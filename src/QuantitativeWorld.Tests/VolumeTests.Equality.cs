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
                var zeroMetresVolume = new Volume(Constants.Zero);

                // act
                // assert
                zeroMetresVolume.Equals(defaultVolume).Should().BeTrue(because: "'new Volume(Constants.Zero)' should be equal 'default(Volume)'");
                defaultVolume.Equals(zeroMetresVolume).Should().BeTrue(because: "'default(Volume)' should be equal 'new Volume(Constants.Zero)'");
            }

            [Fact]
            public void VolumeCreateUsingParamlessConstructor_ShouldBeEqualToZeroMetres()
            {
                // arrange
                var zeroMetresVolume = new Volume(Constants.Zero);
                var paramlessConstructedVolume = new Volume();

                // act
                // assert
                zeroMetresVolume.Equals(paramlessConstructedVolume).Should().BeTrue(because: "'new Volume(Constants.Zero)' should be equal 'new Volume()'");
                paramlessConstructedVolume.Equals(zeroMetresVolume).Should().BeTrue(because: "'new Volume()' should be equal 'new Volume(Constants.Zero)'");
            }

            [Fact]
            public void ZeroUnitsVolume_ShouldBeEqualToZeroMetres()
            {
                // arrange
                var zeroMetresVolume = new Volume(Constants.Zero);
                var zeroUnitsVolume = new Volume(Constants.Zero, CreateUnitOtherThan(VolumeUnit.CubicMetre));

                // act
                // assert
                zeroMetresVolume.Equals(zeroUnitsVolume).Should().BeTrue(because: "'new Volume(Constants.Zero)' should be equal 'new Volume(Constants.Zero, SomeUnit)'");
                zeroUnitsVolume.Equals(zeroMetresVolume).Should().BeTrue(because: "'new Volume(Constants.Zero, SomeUnit)' should be equal 'new Volume(Constants.Zero)'");
            }

            [Fact]
            public void VolumesConvertedToDifferentUnitsEqualInMetres_ShouldBeEqual()
            {
                // arrange
                var volume1 = new Volume(Fixture.Create<number>()).Convert(Fixture.Create<VolumeUnit>());
                var volume2 = new Volume(volume1.CubicMetres).Convert(CreateUnitOtherThan(volume1.Unit));

                // act
                bool equalsResult = volume1.Equals(volume2);

                // assert
                equalsResult.Should().BeTrue();
            }
        }
    }
}
