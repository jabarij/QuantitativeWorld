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

            [Theory]
            [InlineData(1234.5678d)]
            public void MultipleSerialConversion_ShouldHaveSameValueAtTheEnd(number value)
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
                var initialWeight = new Weight(value, units.First());
                Weight? finalWeight = null;

                // act
                units.ForEach(u => finalWeight = (finalWeight ?? initialWeight).Convert(u));

                // assert
                finalWeight.Should().Be(initialWeight);
            }

            private static IEnumerable<ITestDataProvider> GetConvertTestData()
            {
                yield return new ConvertTestData(123.456m, WeightUnit.Kilogram, WeightUnit.Decagram, 12345.6m);
                yield return new ConvertTestData(12345.6m, WeightUnit.Decagram, WeightUnit.Kilogram, 123.456m);

                yield return new ConvertTestData(123.456m, WeightUnit.Kilogram, WeightUnit.Ton, 0.123456m);
                yield return new ConvertTestData(0.123456m, WeightUnit.Ton, WeightUnit.Kilogram, 123.456m);

                yield return new ConvertTestData(123.456m, WeightUnit.Kilogram, WeightUnit.Pound, 123.456m / 0.45359237m);
                yield return new ConvertTestData(272.17389m, WeightUnit.Pound, WeightUnit.Kilogram, 272.17389m * 0.45359237m);
            }

            class ConvertTestData : ITestDataProvider
            {
                public ConvertTestData(Weight originalWeight, WeightUnit targetUnit, Weight expectedWeight)
                {
                    OriginalWeight = originalWeight;
                    TargetUnit = targetUnit;
                    ExpectedWeight = expectedWeight;
                }
                public ConvertTestData(decimal value, WeightUnit unit, WeightUnit targetUnit, decimal expectedValue)
                    : this(new Weight((number)value, unit), targetUnit, new Weight((number)expectedValue, targetUnit)) { }
                public ConvertTestData(double value, WeightUnit unit, WeightUnit targetUnit, double expectedValue)
                    : this(new Weight((number)value, unit), targetUnit, new Weight((number)expectedValue, targetUnit)) { }

                public Weight OriginalWeight { get; }
                public WeightUnit TargetUnit { get; }
                public Weight ExpectedWeight { get; }

                public object[] GetTestParameters() =>
                    new[] { (object)OriginalWeight, TargetUnit, ExpectedWeight };
            }
        }
    }
}
