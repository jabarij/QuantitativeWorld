using FluentAssertions;
using QuantitativeWorld.TestAbstractions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace QuantitativeWorld.Tests
{
    partial class VolumeTests
    {
        public class Convert : VolumeTests
        {
            public Convert(TestFixture testFixture)
                : base(testFixture) { }

            [Theory]
            [MemberData(nameof(GetTestData), typeof(Convert), nameof(GetConvertTestData))]
            public void ShouldConvertToExpectedValue(Volume originalVolume, VolumeUnit targetUnit, Volume expectedVolume)
            {
                // arrange
                // act
                var actualVolume = originalVolume.Convert(targetUnit);

                // assert
                actualVolume.CubicMetres.Should().BeApproximately(expectedVolume.CubicMetres, DoublePrecision);
                actualVolume.Value.Should().BeApproximately(expectedVolume.Value, DoublePrecision);
                actualVolume.Unit.Should().Be(targetUnit);
            }

            [Fact]
            public void MultipleSerialConversion_ShouldHaveSameValueAtTheEnd()
            {
                // arrange
                var units = new List<VolumeUnit>
                {
                    VolumeUnit.CubicMetre,
                    VolumeUnit.CubicMillimetre,
                    VolumeUnit.CubicKilometre,
                    VolumeUnit.CubicFoot,
                    VolumeUnit.CubicInch,
                    VolumeUnit.CubicMetre
                };
                var initialVolume = new Volume(1234.5678d, units.First());
                Volume? finalVolume = null;

                // act
                units.ForEach(u => finalVolume = (finalVolume ?? initialVolume).Convert(u));

                // assert
                finalVolume.Should().Be(initialVolume);
            }

            private static IEnumerable<ITestDataProvider> GetConvertTestData()
            {
                yield return new ConvertTestData(new Volume(123456000d, VolumeUnit.CubicMetre), VolumeUnit.CubicKilometre, new Volume(0.123456d, VolumeUnit.CubicKilometre));
                yield return new ConvertTestData(new Volume(0.123456d, VolumeUnit.CubicKilometre), VolumeUnit.CubicMetre, new Volume(123456000d, VolumeUnit.CubicMetre));

                yield return new ConvertTestData(new Volume(1234.56d, VolumeUnit.CubicCentimetre), VolumeUnit.Litre, new Volume(1.23456d, VolumeUnit.Litre));
                yield return new ConvertTestData(new Volume(1.23456d, VolumeUnit.Litre), VolumeUnit.CubicCentimetre, new Volume(1234.56d, VolumeUnit.CubicCentimetre));
            }

            class ConvertTestData : ITestDataProvider
            {
                public ConvertTestData(Volume originalVolume, VolumeUnit targetUnit, Volume expectedVolume)
                {
                    OriginalVolume = originalVolume;
                    TargetUnit = targetUnit;
                    ExpectedVolume = expectedVolume;
                }

                public Volume OriginalVolume { get; }
                public VolumeUnit TargetUnit { get; }
                public Volume ExpectedVolume { get; }

                public object[] SerializeTestData() =>
                    new[] { (object)OriginalVolume, TargetUnit, ExpectedVolume };
            }
        }
    }
}
