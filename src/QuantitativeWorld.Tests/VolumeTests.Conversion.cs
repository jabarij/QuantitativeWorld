using FluentAssertions;
using QuantitativeWorld.TestAbstractions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace QuantitativeWorld.Tests
{
#if DECIMAL
    using number = System.Decimal;
#else
    using number = System.Double;
    using Constants = QuantitativeWorld.DoubleConstants;
#endif

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
                actualVolume.CubicMetres.Should().BeApproximately(expectedVolume.CubicMetres);
                actualVolume.Value.Should().BeApproximately(expectedVolume.Value);
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
                var initialVolume = new Volume((number)1234.5678m, units.First());
                Volume? finalVolume = null;

                // act
                units.ForEach(u => finalVolume = (finalVolume ?? initialVolume).Convert(u));

                // assert
                finalVolume.Should().Be(initialVolume);
            }

            private static IEnumerable<ITestDataProvider> GetConvertTestData()
            {
                yield return new ConvertTestData(123456000d, VolumeUnit.CubicMetre, 0.123456d, VolumeUnit.CubicKilometre);
                yield return new ConvertTestData(0.123456d, VolumeUnit.CubicKilometre, 123456000d, VolumeUnit.CubicMetre);

                yield return new ConvertTestData(1234.56d, VolumeUnit.CubicCentimetre, 1.23456d, VolumeUnit.Litre);
                yield return new ConvertTestData(1.23456d, VolumeUnit.Litre, 1234.56d, VolumeUnit.CubicCentimetre);
            }

            class ConvertTestData : ConversionTestData<Volume>, ITestDataProvider
            {
                public ConvertTestData(double originalValue, VolumeUnit originalUnit, double expectedValue, VolumeUnit expectedUnit)
                    : base(new Volume((number)originalValue, originalUnit), new Volume((number)expectedValue, expectedUnit)) { }
                public ConvertTestData(decimal originalValue, VolumeUnit originalUnit, decimal expectedValue, VolumeUnit expectedUnit)
                    : base(new Volume((number)originalValue, originalUnit), new Volume((number)expectedValue, expectedUnit)) { }

                public object[] GetTestParameters() =>
                    new[] { (object)OriginalValue, ExpectedValue.Unit, ExpectedValue };
            }
        }
    }
}
