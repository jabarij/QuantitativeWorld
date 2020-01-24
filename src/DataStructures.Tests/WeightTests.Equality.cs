using FluentAssertions;
using Xunit;

namespace DataStructures.Tests
{
    partial class WeightTests
    {
        public class Equality
        {
            [Fact]
            public void DefaultWeightAndEmptyWeigth_ShouldBeEqual()
            {
                // arrange
                var defaultWeight = new Weight();
                var emptyWeight = Weight.Empty;

                // act
                bool defaultEqualsEmpty = defaultWeight.Equals(emptyWeight);
                bool emptyEqualsDefault = emptyWeight.Equals(defaultWeight);

                // assert
                defaultEqualsEmpty.Should().BeTrue();
                emptyEqualsDefault.Should().BeTrue();
            }
        }
    }
}
