using AutoFixture;
using FluentAssertions;
using System.Linq;
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
            public void DefaultWeight_ShouldBeEqualToZeroInAnyUnit()
            {
                // arrange
                var defaultWeight = new Weight();
                var zero = new Weight(
                    value: 0m,
                    unit: Fixture.Create<WeightUnit>());

                // act
                bool defaultEqualsZero = defaultWeight.Equals(zero);
                bool zeroEqualsDefault = zero.Equals(defaultWeight);

                // assert
                defaultEqualsZero.Should().BeTrue();
                zeroEqualsDefault.Should().BeTrue();
            }

            [Fact]
            public void WeightsOfDifferentUnitsEqualInKilograms_ShouldBeEqual()
            {
                // arrange
                var weight1 = Fixture.Create<Weight>();
                var differentUnit = Fixture.CreateFromSet(WeightUnit.GetKnownUnits().Except(new[] { weight1.Unit }));
                var weight2 = new Weight(
                    value: weight1.Kilograms / differentUnit.ValueInKilograms,
                    unit: differentUnit);

                // act
                // assert
                weight1.Equals(weight2).Should().BeTrue();
                weight2.Equals(weight1).Should().BeTrue();
            }
        }
    }
}
