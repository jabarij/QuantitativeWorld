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
                var initialWeight = new Weight(1234.5678m, units.First());
                Weight? finalWeight = null;

                // act
                units.ForEach(u => finalWeight = (finalWeight ?? initialWeight).Convert(u));

                // assert
                finalWeight.Should().Be(initialWeight);
            }

            private static IEnumerable<ITestDataProvider> GetConvertTestData()
            {
                yield return new ConvertTestData(new Weight(123.456m, WeightUnit.Kilogram), WeightUnit.Decagram, new Weight(12345.6m, WeightUnit.Decagram));
                yield return new ConvertTestData(new Weight(12345.6m, WeightUnit.Decagram), WeightUnit.Kilogram, new Weight(123.456m, WeightUnit.Kilogram));

                yield return new ConvertTestData(new Weight(123.456m, WeightUnit.Kilogram), WeightUnit.Ton, new Weight(0.123456m, WeightUnit.Ton));
                yield return new ConvertTestData(new Weight(0.123456m, WeightUnit.Ton), WeightUnit.Kilogram, new Weight(123.456m, WeightUnit.Kilogram));

                yield return new ConvertTestData(new Weight(123.456m, WeightUnit.Kilogram), WeightUnit.Pound, new Weight(123.456m / 0.45359237m, WeightUnit.Pound));
                yield return new ConvertTestData(new Weight(272.17389m, WeightUnit.Pound), WeightUnit.Kilogram, new Weight(272.17389m * 0.45359237m, WeightUnit.Kilogram));
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
