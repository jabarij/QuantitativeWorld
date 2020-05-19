using FluentAssertions;
using QuantitativeWorld.TestAbstractions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace QuantitativeWorld.Tests
{
    partial class EnergyTests
    {
        public class Convert : EnergyTests
        {
            public Convert(TestFixture testFixture)
                : base(testFixture) { }

            [Theory]
            [MemberData(nameof(GetTestData), typeof(Convert), nameof(GetConvertTestData))]
            public void ShouldConvertToExpectedValue(Energy originalEnergy, EnergyUnit targetUnit, Energy expectedEnergy)
            {
                // arrange
                // act
                var actualEnergy = originalEnergy.Convert(targetUnit);

                // assert
                actualEnergy.Joules.Should().BeApproximately(expectedEnergy.Joules, DoublePrecision);
                actualEnergy.Value.Should().BeApproximately(expectedEnergy.Value, DoublePrecision);
                actualEnergy.Unit.Should().Be(targetUnit);
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
                var initialEnergy = new Energy(1234.5678d, units.First());
                Energy? finalEnergy = null;

                // act
                units.ForEach(u => finalEnergy = (finalEnergy ?? initialEnergy).Convert(u));

                // assert
                finalEnergy.Should().Be(initialEnergy);
            }

            private static IEnumerable<ITestDataProvider> GetConvertTestData()
            {
                //yield return new ConvertTestData(new Energy(123.456d, EnergyUnit.Joule), EnergyUnit.Kilocalorie, new Energy(0.0295066922d, EnergyUnit.Kilocalorie));
                //yield return new ConvertTestData(new Energy(0.0295066922d, EnergyUnit.Kilocalorie), EnergyUnit.Joule, new Energy(123.456d, EnergyUnit.Joule));

                //yield return new ConvertTestData(new Energy(123.456d, EnergyUnit.Joule), EnergyUnit.Kilojoule, new Energy(0.123456d, EnergyUnit.Kilojoule));
                //yield return new ConvertTestData(new Energy(0.123456d, EnergyUnit.Kilojoule), EnergyUnit.Joule, new Energy(123.456d, EnergyUnit.Joule));

                yield return new ConvertTestData(new Energy(123.456d, EnergyUnit.Joule), EnergyUnit.Erg, new Energy(1234560000d, EnergyUnit.Erg));
                yield return new ConvertTestData(new Energy(1234560000d, EnergyUnit.Erg), EnergyUnit.Joule, new Energy(123.456d, EnergyUnit.Joule));
            }

            class ConvertTestData : ITestDataProvider
            {
                public ConvertTestData(Energy originalEnergy, EnergyUnit targetUnit, Energy expectedEnergy)
                {
                    OriginalEnergy = originalEnergy;
                    TargetUnit = targetUnit;
                    ExpectedEnergy = expectedEnergy;
                }

                public Energy OriginalEnergy { get; }
                public EnergyUnit TargetUnit { get; }
                public Energy ExpectedEnergy { get; }

                public object[] SerializeTestData() =>
                    new[] { (object)OriginalEnergy, TargetUnit, ExpectedEnergy };
            }
        }
    }
}
