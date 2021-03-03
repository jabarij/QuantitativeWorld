using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

#if DECIMAL
namespace DecimalQuantitativeWorld.Tests
{
    using DecimalQuantitativeWorld.TestAbstractions;
    using number = System.Decimal;
#else
namespace QuantitativeWorld.Tests
{
    using QuantitativeWorld.TestAbstractions;
    using Constants = DoubleConstants;
    using number = System.Double;
#endif

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
                var initialPower = new Power((number)1234.5678m, units.First());
                Power? finalPower = null;

                // act
                units.ForEach(u => finalPower = (finalPower ?? initialPower).Convert(u));

                // assert
                finalPower.Should().Be(initialPower);
            }

            private static IEnumerable<ITestDataProvider> GetConvertTestData()
            {
                yield return new ConvertTestData(123.456m, PowerUnit.Watt, 123456m, PowerUnit.Milliwatt);
                yield return new ConvertTestData(123456m, PowerUnit.Milliwatt, 123.456m, PowerUnit.Watt);

                yield return new ConvertTestData(123.456m, PowerUnit.Watt, 0.123456m, PowerUnit.Kilowatt);
                yield return new ConvertTestData(0.123456m, PowerUnit.Kilowatt, 123.456m, PowerUnit.Watt);

                yield return new ConvertTestData(1234.56m, PowerUnit.Watt, 1234.56m / (76.0402249m * 9.80665m), PowerUnit.MechanicalHorsepower);
                yield return new ConvertTestData(123.456m, PowerUnit.MechanicalHorsepower, 123.456m * (76.0402249m * 9.80665m), PowerUnit.Watt);
            }
            class ConvertTestData : ConversionTestData<Power>, ITestDataProvider
            {
                public ConvertTestData(decimal originalValue, PowerUnit originalUnit, decimal expectedValue, PowerUnit expectedUnit)
                    : base(new Power((number)originalValue, originalUnit), new Power((number)expectedValue, expectedUnit)) { }

                public object[] GetTestParameters() =>
                    new[] { (object)OriginalValue, ExpectedValue.Unit, ExpectedValue };
            }
        }
    }
}
