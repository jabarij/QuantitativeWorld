using AutoFixture;
using FluentAssertions;
using Xunit;

namespace QuantitativeWorld.Tests
{
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
                var zeroKilogramsWeight = new Weight(0m);

                // act
                // assert
                zeroKilogramsWeight.Equals(defaultWeight).Should().BeTrue(because: "'new Weight(0m)' should be equal 'default(Weight)'");
                defaultWeight.Equals(zeroKilogramsWeight).Should().BeTrue(because: "'default(Weight)' should be equal 'new Weight(0m)'");
            }

            [Fact]
            public void WeightCreateUtinsParamlessConstructor_ShouldBeEqualToZeroKilograms()
            {
                // arrange
                var zeroKilogramsWeight = new Weight(0m);
                var paramlessConstructedWeight = new Weight();

                // act
                // assert
                zeroKilogramsWeight.Equals(paramlessConstructedWeight).Should().BeTrue(because: "'new Weight(0m)' should be equal 'new Weight()'");
                paramlessConstructedWeight.Equals(zeroKilogramsWeight).Should().BeTrue(because: "'new Weight()' should be equal 'new Weight(0m)'");
            }

            [Fact]
            public void ZeroUnitsWeight_ShouldBeEqualToZeroKilograms()
            {
                // arrange
                var zeroKilogramsWeight = new Weight(0m);
                var zeroUnitsWeight = new Weight(0m, CreateUnitOtherThan(WeightUnit.Kilogram));

                // act
                // assert
                zeroKilogramsWeight.Equals(zeroUnitsWeight).Should().BeTrue(because: "'new Weight(0m)' should be equal 'new Weight(0m, SomeUnit)'");
                zeroUnitsWeight.Equals(zeroKilogramsWeight).Should().BeTrue(because: "'new Weight(0m, SomeUnit)' should be equal 'new Weight(0m)'");
            }

            [Fact]
            public void WeightsOfDifferentUnitsEqualInKilograms_ShouldBeEqual()
            {
                // arrange
                var weight1 = new Weight(
                    value: Fixture.Create<decimal>(),
                    unit: WeightUnit.Ton);
                var weight2 = new Weight(
                    value: weight1.Kilograms * 1000m,
                    unit: WeightUnit.Gram);

                // act
                bool equalsResult = weight1.Equals(weight2);

                // assert
                equalsResult.Should().BeTrue(because: $"{weight1.Value} {weight1.Unit} ({weight1.Kilograms} kg) == {weight2.Value} {weight2.Unit} ({weight2.Kilograms} kg)");
            }
        }
    }
}
