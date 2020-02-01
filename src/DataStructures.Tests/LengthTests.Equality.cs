using FluentAssertions;
using Xunit;

namespace QuantitativeWorld.Tests
{
    partial class LengthTests
    {
        public class Equality
        {
            [Fact]
            public void DefaultLengthAndEmptyWeigth_ShouldBeEqual()
            {
                // arrange
                var defaultLength = new Length();
                var emptyLength = Length.Empty;

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
