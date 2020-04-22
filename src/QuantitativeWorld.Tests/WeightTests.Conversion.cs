using FluentAssertions;
using QuantitativeWorld.TestAbstractions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace QuantitativeWorld.Tests
{
    partial class WeightTests
    {
        public class Convert : WeightTests
        {
            public Convert(TestFixture testFixture)
                : base(testFixture) { }

            [Theory]
            [MemberData(nameof(GetTestData), typeof(Convert), nameof(GetConvertTestData))]
            public void ShouldConvertToExpectedValue(Weight originalWeight, WeightUnit targetUnit, Weight expectedWeight)
            {
                // arrange
                // act
                var actualWeight = originalWeight.Convert(targetUnit);

                // assert
                actualWeight.Should().Be(expectedWeight);
                actualWeight.Unit.Should().Be(targetUnit);
            }

            [Fact]
            public void MultipleSerialConversion_ShouldHaveSameValueAtTheEnd()
            {
                // arrange
                var units = new List<WeightUnit>
                {
                    WeightUnit.Kilogram,
                    WeightUnit.Gram,
                    WeightUnit.Ton,
                    WeightUnit.Pound,
                    WeightUnit.Ounce,
                    WeightUnit.Kilogram
                };
                var initialWeight = new Weight(1234.5678d, units.First());
                Weight? finalWeight = null;

                // act
                units.ForEach(u => finalWeight = (finalWeight ?? initialWeight).Convert(u));

                // assert
                finalWeight.Should().Be(initialWeight);
            }

            private static IEnumerable<ITestDataProvider> GetConvertTestData()
            {
                yield return new ConvertTestData(new Weight(123.456d, WeightUnit.Kilogram), WeightUnit.Decagram, new Weight(12345.6d, WeightUnit.Decagram));
                yield return new ConvertTestData(new Weight(12345.6d, WeightUnit.Decagram), WeightUnit.Kilogram, new Weight(123.456d, WeightUnit.Kilogram));

                yield return new ConvertTestData(new Weight(123.456d, WeightUnit.Kilogram), WeightUnit.Ton, new Weight(0.123456d, WeightUnit.Ton));
                yield return new ConvertTestData(new Weight(0.123456d, WeightUnit.Ton), WeightUnit.Kilogram, new Weight(123.456d, WeightUnit.Kilogram));

                yield return new ConvertTestData(new Weight(123.456d, WeightUnit.Kilogram), WeightUnit.Pound, new Weight(123.456d / 0.45359237d, WeightUnit.Pound));
                yield return new ConvertTestData(new Weight(272.17389d, WeightUnit.Pound), WeightUnit.Kilogram, new Weight(272.17389d * 0.45359237d, WeightUnit.Kilogram));
            }

            class ConvertTestData : ITestDataProvider
            {
                public ConvertTestData(Weight originalWeight, WeightUnit targetUnit, Weight expectedWeight)
                {
                    OriginalWeight = originalWeight;
                    TargetUnit = targetUnit;
                    ExpectedWeight = expectedWeight;
                }

                public Weight OriginalWeight { get; }
                public WeightUnit TargetUnit { get; }
                public Weight ExpectedWeight { get; }

                public object[] SerializeTestData() =>
                    new[] { (object)OriginalWeight, TargetUnit, ExpectedWeight };
            }
        }
    }
}
