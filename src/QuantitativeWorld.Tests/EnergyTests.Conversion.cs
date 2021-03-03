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

    partial class EnergyTests
    {
        public class Convert : EnergyTests
        {
            public Convert(TestFixture testFixture)
                : base(testFixture) { }

            [Theory]
            [MemberData(nameof(GetTestData), typeof(Convert), nameof(GetConvertTestData))]
            public void ShouldConvertToExpectedValue(Energy original, Energy expected)
            {
                // arrange
                // act
                var actual = original.Convert(expected.Unit);

                // assert
                actual.Joules.Should().BeApproximately(expected.Joules);
                actual.Value.Should().BeApproximately(expected.Value);
                actual.Unit.Should().Be(expected.Unit);
            }

            [Fact]
            public void MultipleSerialConversion_ShouldHaveSameValueAtTheEnd()
            {
                // arrange
                var units = new List<EnergyUnit>
                {
                    EnergyUnit.Joule,
                    EnergyUnit.Kilojoule,
                    EnergyUnit.KilowattHour,
                    EnergyUnit.Kilocalorie,
                    EnergyUnit.Joule
                };
                var initialEnergy = new Energy((number)1234.5678m, units.First());
                Energy? finalEnergy = null;

                // act
                units.ForEach(u => finalEnergy = (finalEnergy ?? initialEnergy).Convert(u));

                // assert
                finalEnergy.Should().Be(initialEnergy);
            }

            private static IEnumerable<ITestDataProvider> GetConvertTestData()
            {
                yield return new ConvertTestData(123.456m, EnergyUnit.Joule, 0.0295066922m, EnergyUnit.Kilocalorie);
                yield return new ConvertTestData(0.0295066922m, EnergyUnit.Kilocalorie, 123.456m, EnergyUnit.Joule);

                yield return new ConvertTestData(123.456m, EnergyUnit.Joule, 0.123456m, EnergyUnit.Kilojoule);
                yield return new ConvertTestData(0.123456m, EnergyUnit.Kilojoule, 123.456m, EnergyUnit.Joule);

                yield return new ConvertTestData(123.456m, EnergyUnit.Joule, 1234560000m, EnergyUnit.Erg);
                yield return new ConvertTestData(1234560000m, EnergyUnit.Erg, 123.456m, EnergyUnit.Joule);
            }

            class ConvertTestData : ConversionTestData<Energy>, ITestDataProvider
            {
                public ConvertTestData(decimal originalValue, EnergyUnit originalUnit, decimal expectedValue, EnergyUnit expectedUnit)
                    : base(new Energy((number)originalValue, originalUnit), new Energy((number)expectedValue, expectedUnit)) { }

                public object[] GetTestParameters() =>
                    new[] { (object)OriginalValue, ExpectedValue };
            }
        }
    }
}
