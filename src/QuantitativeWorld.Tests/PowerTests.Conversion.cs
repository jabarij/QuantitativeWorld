using FluentAssertions;
using QuantitativeWorld.TestAbstractions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace QuantitativeWorld.Tests
{
    partial class PowerTests
    {
        public class Convert : PowerTests
        {
            public Convert(TestFixture testFixture)
                : base(testFixture) { }

            [Theory]
            [MemberData(nameof(GetTestData), typeof(Convert), nameof(GetConvertTestData))]
            public void ShouldConvertToExpectedValue(Power originalPower, PowerUnit targetUnit, Power expectedPower)
            {
                // arrange
                // act
                var actualPower = originalPower.Convert(targetUnit);

                // assert
                actualPower.Should().Be(expectedPower);
                actualPower.Unit.Should().Be(targetUnit);
            }

            [Fact]
            public void MultipleSerialConversion_ShouldHaveSameValueAtTheEnd()
            {
                // arrange
                var units = new List<PowerUnit>
                {
                    PowerUnit.Watt,
                    PowerUnit.Milliwatt,
                    PowerUnit.Kilowatt,
                    PowerUnit.Megawatt,
                    PowerUnit.MechanicalHorsepower,
                    PowerUnit.Watt
                };
                var initialPower = new Power(1234.5678d, units.First());
                Power? finalPower = null;

                // act
                units.ForEach(u => finalPower = (finalPower ?? initialPower).Convert(u));

                // assert
                finalPower.Should().Be(initialPower);
            }

            private static IEnumerable<ITestDataProvider> GetConvertTestData()
            {
                yield return new ConvertTestData(new Power(123.456d, PowerUnit.Watt), PowerUnit.Milliwatt, new Power(123456d, PowerUnit.Milliwatt));
                yield return new ConvertTestData(new Power(123456d, PowerUnit.Milliwatt), PowerUnit.Watt, new Power(123.456d, PowerUnit.Watt));

                yield return new ConvertTestData(new Power(123.456d, PowerUnit.Watt), PowerUnit.Kilowatt, new Power(0.123456d, PowerUnit.Kilowatt));
                yield return new ConvertTestData(new Power(0.123456d, PowerUnit.Kilowatt), PowerUnit.Watt, new Power(123.456d, PowerUnit.Watt));

                yield return new ConvertTestData(new Power(1234.56d, PowerUnit.Watt), PowerUnit.MechanicalHorsepower, new Power(1234.56d / (76.0402249d * 9.80665d), PowerUnit.MechanicalHorsepower));
                yield return new ConvertTestData(new Power(123.456d, PowerUnit.MechanicalHorsepower), PowerUnit.Watt, new Power(123.456d * (76.0402249d * 9.80665d), PowerUnit.Watt));
            }

            class ConvertTestData : ITestDataProvider
            {
                public ConvertTestData(Power originalPower, PowerUnit targetUnit, Power expectedPower)
                {
                    OriginalPower = originalPower;
                    TargetUnit = targetUnit;
                    ExpectedPower = expectedPower;
                }

                public Power OriginalPower { get; }
                public PowerUnit TargetUnit { get; }
                public Power ExpectedPower { get; }

                public object[] SerializeTestData() =>
                    new[] { (object)OriginalPower, TargetUnit, ExpectedPower };
            }
        }
    }
}
