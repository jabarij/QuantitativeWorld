using FluentAssertions;
using Xunit;

namespace DataStructures.Tests
{
    partial class WeightTests
    {
        public class Creation
        {
            [Theory]
            [InlineData(0.001, 1)]
            [InlineData(1, 1000)]
            [InlineData(1000, 1000000)]
            public void FromKilograms_ShouldCreateValidWeight(decimal kilograms, decimal grams)
            {
                // arrange
                var expectedWeight = new Weight(grams, WeightUnit.Gram);

                // act
                var actualWeight = Weight.FromKilograms(kilograms);

                // assert
                actualWeight.Should().Be(expectedWeight);
            }

            [Theory]
            [InlineData(0.001, 1000)]
            [InlineData(1, 1000000)]
            [InlineData(1000, 1000000000)]
            public void FromTons_ShouldCreateValidWeight(decimal tons, decimal grams)
            {
                // arrange
                var expectedWeight = new Weight(grams, WeightUnit.Gram);

                // act
                var actualWeight = Weight.FromTons(tons);

                // assert
                actualWeight.Should().Be(expectedWeight);
            }
        }
    }
}
