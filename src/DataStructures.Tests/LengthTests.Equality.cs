using FluentAssertions;
using Xunit;

namespace QuantitativeWorld.Tests
{
    partial class LengthTests
    {
        public class Equality
        {
            [Fact]
            public void DefaultLength_ShouldBeEqualToZeroMetres()
            {
                // arrange
                var defaultLength = new Length();
                var emptyLength = new Length(0m);

                // act
                bool defaultEqualsEmpty = defaultLength.Equals(emptyLength);
                bool emptyEqualsDefault = emptyLength.Equals(defaultLength);

                // assert
                defaultEqualsEmpty.Should().BeTrue();
                emptyEqualsDefault.Should().BeTrue();
            }
        }
    }
}
