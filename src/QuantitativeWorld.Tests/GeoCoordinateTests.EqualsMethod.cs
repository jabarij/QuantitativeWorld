using AutoFixture;
using FluentAssertions;
using QuantitativeWorld.TestAbstractions;
using Xunit;

namespace QuantitativeWorld.Tests
{
    partial class GeoCoordinateTests
    {
        public class EqualsMethod : GeoCoordinateTests
        {
            public EqualsMethod(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void OtherIsNull_ShouldReturnFalse()
            {
                // arrange
                var sut = Fixture.Create<GeoCoordinate>();

                // act
                bool result = sut.Equals(null);

                // assert
                result.Should().BeFalse();
            }

            [Theory]
            [InlineData(10d, 11d, 10d, 11d, true)]
            [InlineData(10d, 11d, 20d, 11d, false)]
            [InlineData(10d, 11d, 10d, 21d, false)]
            public void ShouldReturnProperValue(double lat1, double lon1, double lat2, double lon2, bool expectedResult)
            {
                // arrange
                var sut = new GeoCoordinate(lat1, lon1);

                // act
                bool result = sut.Equals(new GeoCoordinate(lat2, lon2));

                // assert
                result.Should().Be(expectedResult);
            }
        }
    }
}
