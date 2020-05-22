using FluentAssertions;
using QuantitativeWorld.TestAbstractions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace QuantitativeWorld.Tests
{
    partial class SpeedTests
    {
        public class Convert : SpeedTests
        {
            public Convert(TestFixture testFixture)
                : base(testFixture) { }

            [Theory]
            [MemberData(nameof(GetTestData), typeof(Convert), nameof(GetConvertTestData))]
            public void ShouldConvertToExpectedValue(Speed originalSpeed, SpeedUnit targetUnit, Speed expectedSpeed)
            {
                // arrange
                // act
                var actualSpeed = originalSpeed.Convert(targetUnit);

                // assert
                actualSpeed.MetresPerSecond.Should().BeApproximately(expectedSpeed.MetresPerSecond, DoublePrecision);
                actualSpeed.Value.Should().BeApproximately(expectedSpeed.Value, DoublePrecision);
                actualSpeed.Unit.Should().Be(targetUnit);
            }

            [Fact]
            public void MultipleSerialConversion_ShouldHaveSameValueAtTheEnd()
            {
                // arrange
                var units = new List<SpeedUnit>
                {
                    SpeedUnit.MetrePerSecond,
                    SpeedUnit.KilometrePerHour,
                    SpeedUnit.FootPerSecond,
                    SpeedUnit.MetrePerSecond
                };
                var initialSpeed = new Speed(1234.5678d, units.First());
                Speed? finalSpeed = null;

                // act
                units.ForEach(u => finalSpeed = (finalSpeed ?? initialSpeed).Convert(u));

                // assert
                finalSpeed.Should().Be(initialSpeed);
            }

            private static IEnumerable<ITestDataProvider> GetConvertTestData()
            {
                yield return new ConvertTestData(new Speed(123.456d, SpeedUnit.MetrePerSecond), SpeedUnit.KilometrePerHour, new Speed(444.4416d, SpeedUnit.KilometrePerHour));
                //yield return new ConvertTestData(new Speed(444.4416d, SpeedUnit.KilometrePerHour), SpeedUnit.MetrePerSecond, new Speed(123.456d, SpeedUnit.MetrePerSecond));

                //yield return new ConvertTestData(new Speed(123.456d, SpeedUnit.MetrePerSecond), SpeedUnit.MilePerHour, new Speed(276.1632068718683d, SpeedUnit.MilePerHour));
                //yield return new ConvertTestData(new Speed(276.1632068718683d, SpeedUnit.MilePerHour), SpeedUnit.MetrePerSecond, new Speed(123.456d, SpeedUnit.MetrePerSecond));
            }

            class ConvertTestData : ITestDataProvider
            {
                public ConvertTestData(Speed originalSpeed, SpeedUnit targetUnit, Speed expectedSpeed)
                {
                    OriginalSpeed = originalSpeed;
                    TargetUnit = targetUnit;
                    ExpectedSpeed = expectedSpeed;
                }

                public Speed OriginalSpeed { get; }
                public SpeedUnit TargetUnit { get; }
                public Speed ExpectedSpeed { get; }

                public object[] SerializeTestData() =>
                    new[] { (object)OriginalSpeed, TargetUnit, ExpectedSpeed };
            }
        }
    }
}
