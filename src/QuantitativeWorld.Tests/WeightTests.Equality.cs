using AutoFixture;
using FluentAssertions;
using Xunit;

#if DECIMAL
namespace DecimalQuantitativeWorld.Tests
{
    using DecimalQuantitativeWorld.TestAbstractions;
    using Constants = DecimalConstants;
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
        public class Equality : WeightTests
        {
            public Equality(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void DefaultWeight_ShouldBeEqualToZeroKilograms()
            {
                // arrange
                var defaultWeight = default(Weight);
                var zeroKilogramsWeight = new Weight(Constants.Zero);

                // act
                // assert
                zeroKilogramsWeight.Equals(defaultWeight).Should().BeTrue(because: "'new Weight(Constants.Zero)' should be equal 'default(Weight)'");
                defaultWeight.Equals(zeroKilogramsWeight).Should().BeTrue(because: "'default(Weight)' should be equal 'new Weight(Constants.Zero)'");
            }

            [Fact]
            public void WeightCreateUsingParamlessConstructor_ShouldBeEqualToZeroKilograms()
            {
                // arrange
                var zeroKilogramsWeight = new Weight(Constants.Zero);
                var paramlessConstructedWeight = new Weight();

                // act
                // assert
                zeroKilogramsWeight.Equals(paramlessConstructedWeight).Should().BeTrue(because: "'new Weight(Constants.Zero)' should be equal 'new Weight()'");
                paramlessConstructedWeight.Equals(zeroKilogramsWeight).Should().BeTrue(because: "'new Weight()' should be equal 'new Weight(Constants.Zero)'");
            }

            [Fact]
            public void ZeroUnitsWeight_ShouldBeEqualToZeroKilograms()
            {
                // arrange
                var zeroKilogramsWeight = new Weight(Constants.Zero);
                var zeroUnitsWeight = new Weight(Constants.Zero, CreateUnitOtherThan(WeightUnit.Kilogram));

                // act
                // assert
                zeroKilogramsWeight.Equals(zeroUnitsWeight).Should().BeTrue(because: "'new Weight(Constants.Zero)' should be equal 'new Weight(Constants.Zero, SomeUnit)'");
                zeroUnitsWeight.Equals(zeroKilogramsWeight).Should().BeTrue(because: "'new Weight(Constants.Zero, SomeUnit)' should be equal 'new Weight(Constants.Zero)'");
            }

            [Fact]
            public void WeightsConvertedToDifferentUnitsEqualInKilograms_ShouldBeEqual()
            {
                // arrange
                var weight1 = new Weight(Fixture.Create<number>()).Convert(Fixture.Create<WeightUnit>());
                var weight2 = new Weight(weight1.Kilograms).Convert(CreateUnitOtherThan(weight1.Unit));

                // act
                bool equalsResult = weight1.Equals(weight2);

                // assert
                equalsResult.Should().BeTrue();
            }
        }
    }
}
