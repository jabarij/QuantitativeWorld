using FluentAssertions;
using Xunit;

#if DECIMAL
namespace DecimalQuantitativeWorld.Tests
{
    using DecimalQuantitativeWorld.TestAbstractions;
#else
namespace QuantitativeWorld.Tests
{
    using QuantitativeWorld.TestAbstractions;
    using Constants = DoubleConstants;
    using number = System.Double;
#endif

    partial class WeightUnitTests
    {
        public class Equality : WeightUnitTests
        {
            public Equality(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void DefaultWeightUnit_ShouldBeEqualToKilogram()
            {
                // arrange
                var defaultWeightUnit = default(WeightUnit);
                var kilogram = WeightUnit.Kilogram;

                // act
                // assert
                kilogram.Equals(defaultWeightUnit).Should().BeTrue(because: "'WeightUnit.Kilogram' should be equal 'default(WeightUnit)'");
                defaultWeightUnit.Equals(kilogram).Should().BeTrue(because: "'default(WeightUnit)' should be equal 'WeightUnit.Kilogram'");
            }

            [Fact]
            public void ParamlessConstructedWeightUnit_ShouldBeEqualToKilogram()
            {
                // arrange
                var paramlessConstructedWeightUnit = new WeightUnit();
                var kilogram = WeightUnit.Kilogram;

                // act
                // assert
                kilogram.Equals(paramlessConstructedWeightUnit).Should().BeTrue(because: "'WeightUnit.Kilogram' should be equal 'new WeightUnit()'");
                paramlessConstructedWeightUnit.Equals(kilogram).Should().BeTrue(because: "'new WeightUnit()' should be equal 'WeightUnit.Kilogram'");
            }
        }
    }
}
