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
                actualSpeed.MetresPerSecond.Should().BeApproximately(expectedSpeed.MetresPerSecond);
                actualSpeed.Value.Should().BeApproximately(expectedSpeed.Value);
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
                var initialSpeed = new Speed((number)1234.5678m, units.First());
                Speed? finalSpeed = null;

                // act
                units.ForEach(u => finalSpeed = (finalSpeed ?? initialSpeed).Convert(u));

                // assert
                finalSpeed.Should().Be(initialSpeed);
            }

            private static IEnumerable<ITestDataProvider> GetConvertTestData()
            {
                yield return new ConvertTestData(123.456m, SpeedUnit.MetrePerSecond, 444.4416m, SpeedUnit.KilometrePerHour);
                yield return new ConvertTestData(444.4416m, SpeedUnit.KilometrePerHour, 123.456m, SpeedUnit.MetrePerSecond);

                yield return new ConvertTestData(123.456m, SpeedUnit.MetrePerSecond, 276.1632068718683m, SpeedUnit.MilePerHour);
                yield return new ConvertTestData(276.1632068718683m, SpeedUnit.MilePerHour, 123.456m, SpeedUnit.MetrePerSecond);
            }

            class ConvertTestData : ConversionTestData<Speed>, ITestDataProvider
            {
                public ConvertTestData(decimal originalValue, SpeedUnit originalUnit, decimal expectedValue, SpeedUnit expectedUnit)
                    : base(new Speed((number)originalValue, originalUnit), new Speed((number)expectedValue, expectedUnit)) { }

                public object[] GetTestParameters() =>
                    new[] { (object)OriginalValue, ExpectedValue.Unit, ExpectedValue };
            }
        }
    }
}
