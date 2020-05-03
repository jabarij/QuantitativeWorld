using AutoFixture;
using FluentAssertions;
using QuantitativeWorld.TestAbstractions;
using System.Collections.Generic;
using Xunit;

namespace QuantitativeWorld.Tests
{
    partial class VolumeTests
    {
        public class Creation : VolumeTests
        {
            public Creation(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void ConstructorForMetres_ShouldCreateValidVolume()
            {
                // arrange
                double cubicMetres = Fixture.Create<double>();

                // act
                var volume = new Volume(cubicMetres);

                // assert
                volume.CubicMetres.Should().Be(cubicMetres);
                volume.Value.Should().Be(cubicMetres);
                volume.Unit.Should().Be(VolumeUnit.CubicMetre);
            }

            [Theory]
            [MemberData(nameof(GetTestData), typeof(Creation), nameof(GetConstructorForValueAndUnitTestData))]
            public void ConstructorForValueAndUnit_ShouldCreateValidVolume(ConstructorForValueAndUnitTestData testData)
            {
                // arrange
                // act
                var volume = new Volume(testData.Value, testData.Unit);

                // assert
                volume.CubicMetres.Should().BeApproximately(testData.ExpectedMetres, DoublePrecision);
                volume.Value.Should().Be(testData.Value);
                volume.Unit.Should().Be(testData.Unit);
            }
            private static IEnumerable<ConstructorForValueAndUnitTestData> GetConstructorForValueAndUnitTestData()
            {
                yield return new ConstructorForValueAndUnitTestData(1d, VolumeUnit.CubicMetre, 1d);
                yield return new ConstructorForValueAndUnitTestData(1000d, VolumeUnit.CubicDecimetre, 1d);
                yield return new ConstructorForValueAndUnitTestData(1000d, VolumeUnit.CubicInch, 0.016387064d);
            }
            public class ConstructorForValueAndUnitTestData
            {
                public ConstructorForValueAndUnitTestData(double value, VolumeUnit unit, double expectedCubicMetres)
                {
                    Value = value;
                    Unit = unit;
                    ExpectedMetres = expectedCubicMetres;
                }

                public double Value { get; }
                public VolumeUnit Unit { get; }
                public double ExpectedMetres { get; }
            }

            [Theory]
            [InlineData(0.001d, 1d)]
            [InlineData(1d, 1000d)]
            [InlineData(1000d, 1000000d)]
            public void FromCubicKilometres_ShouldCreateValidVolume(double litres, double cubicCentimetres)
            {
                // arrange
                var expectedVolume = new Volume(cubicCentimetres, VolumeUnit.CubicCentimetre);

                // act
                var actualVolume = new Volume(litres, VolumeUnit.Litre);

                // assert
                actualVolume.Should().Be(expectedVolume);
            }
        }
    }
}
