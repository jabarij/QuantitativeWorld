using AutoFixture;
using FluentAssertions;
using Xunit;

namespace QuantitativeWorld.Tests
{
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
                kilogram.Equals(defaultWeightUnit).Should().BeTrue(because: "'new Weight(0m)' should be equal 'default(Weight)'");
                defaultWeightUnit.Equals(kilogram).Should().BeTrue(because: "'default(Weight)' should be equal 'new Weight(0m)'");
            }
        }
    }
}
