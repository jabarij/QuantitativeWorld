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
                var initialPower = new Power(1234.5678m, units.First());
                Power? finalPower = null;

                // act
                units.ForEach(u => finalPower = (finalPower ?? initialPower).Convert(u));

                // assert
                finalPower.Should().Be(initialPower);
            }

            private static IEnumerable<ITestDataProvider> GetConvertTestData()
            {
                yield return new ConvertTestData(new Power(123.456m, PowerUnit.Watt), PowerUnit.Milliwatt, new Power(123456m, PowerUnit.Milliwatt));
                yield return new ConvertTestData(new Power(123456m, PowerUnit.Milliwatt), PowerUnit.Watt, new Power(123.456m, PowerUnit.Watt));

                yield return new ConvertTestData(new Power(123.456m, PowerUnit.Watt), PowerUnit.Kilowatt, new Power(0.123456m, PowerUnit.Kilowatt));
                yield return new ConvertTestData(new Power(0.123456m, PowerUnit.Kilowatt), PowerUnit.Watt, new Power(123.456m, PowerUnit.Watt));

                yield return new ConvertTestData(new Power(1234.56m, PowerUnit.Watt), PowerUnit.MechanicalHorsepower, new Power(1234.56m / (76.0402249m * 9.80665m), PowerUnit.MechanicalHorsepower));
                yield return new ConvertTestData(new Power(123.456m, PowerUnit.MechanicalHorsepower), PowerUnit.Watt, new Power(123.456m * (76.0402249m * 9.80665m), PowerUnit.Watt));
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
