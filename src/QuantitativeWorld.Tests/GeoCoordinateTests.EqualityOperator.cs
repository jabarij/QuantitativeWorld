using AutoFixture;
using FluentAssertions;
using QuantitativeWorld.TestAbstractions;
using Xunit;

namespace QuantitativeWorld.Tests
{
#if DECIMAL
    using number = System.Decimal;
    using Constants = QuantitativeWorld.DecimalConstants;
#else
    using number = System.Double;
    using Constants = QuantitativeWorld.DoubleConstants;
#endif

    partial class GeoCoordinateTests
    {
        public class EqualityOperator : GeoCoordinateTests
        {
            public EqualityOperator(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void OtherIsNull_ShouldReturnFalse()
            {
                // arrange
                var sut = Fixture.Create<GeoCoordinate>();

                // act
                bool result = sut == null;

                // assert
                result.Should().BeFalse();
            }

            [Theory]
            [InlineData(10, 11, 10, 11, true)]
            [InlineData(10, 11, 20, 11, false)]
            [InlineData(10, 11, 10, 21, false)]
            public void ShouldReturnProperValue(number lat1, number lon1, number lat2, number lon2, bool expectedResult)
            {
                // arrange
                var sut = new GeoCoordinate(lat1, lon1);

                // act
                bool result = sut == new GeoCoordinate(lat2, lon2);

                // assert
                result.Should().Be(expectedResult);
            }
        }
    }
}
