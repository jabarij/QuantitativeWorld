using AutoFixture;
using FluentAssertions;
using QuantitativeWorld.TestAbstractions;
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
                var zeroKilogramsWeight = new Weight(0d);

                // act
                // assert
                zeroKilogramsWeight.Equals(defaultWeight).Should().BeTrue(because: "'new Weight(0d)' should be equal 'default(Weight)'");
                defaultWeight.Equals(zeroKilogramsWeight).Should().BeTrue(because: "'default(Weight)' should be equal 'new Weight(0d)'");
            }

            [Fact]
            public void WeightCreateUsingParamlessConstructor_ShouldBeEqualToZeroKilograms()
            {
                // arrange
                var zeroKilogramsWeight = new Weight(0d);
                var paramlessConstructedWeight = new Weight();

                // act
                // assert
                zeroKilogramsWeight.Equals(paramlessConstructedWeight).Should().BeTrue(because: "'new Weight(0d)' should be equal 'new Weight()'");
                paramlessConstructedWeight.Equals(zeroKilogramsWeight).Should().BeTrue(because: "'new Weight()' should be equal 'new Weight(0d)'");
            }

            [Fact]
            public void ZeroUnitsWeight_ShouldBeEqualToZeroKilograms()
            {
                // arrange
                var zeroKilogramsWeight = new Weight(0d);
                var zeroUnitsWeight = new Weight(0d, CreateUnitOtherThan(WeightUnit.Kilogram));

                // act
                // assert
                zeroKilogramsWeight.Equals(zeroUnitsWeight).Should().BeTrue(because: "'new Weight(0d)' should be equal 'new Weight(0d, SomeUnit)'");
                zeroUnitsWeight.Equals(zeroKilogramsWeight).Should().BeTrue(because: "'new Weight(0d, SomeUnit)' should be equal 'new Weight(0d)'");
            }

            [Fact]
            public void WeightsConvertedToDifferentUnitsEqualInKilograms_ShouldBeEqual()
            {
                // arrange
                var weight1 = new Weight(Fixture.Create<double>()).Convert(Fixture.Create<WeightUnit>());
                var weight2 = new Weight(weight1.Kilograms).Convert(CreateUnitOtherThan(weight1.Unit));

                // act
                bool equalsResult = weight1.Equals(weight2);

                // assert
                equalsResult.Should().BeTrue();
            }
        }
    }
}
