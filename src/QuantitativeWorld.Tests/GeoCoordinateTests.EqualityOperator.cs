using AutoFixture;
using FluentAssertions;
using Xunit;

#if DECIMAL
namespace DecimalQuantitativeWorld.Tests
{
    using DecimalQuantitativeWorld.TestAbstractions;
    using number = System.Decimal;
#else
namespace QuantitativeWorld.Tests
{
    using QuantitativeWorld.TestAbstractions;
    using Constants = DoubleConstants;
    using number = System.Double;
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
                // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                bool result = sut == null;

                // assert
                // ReSharper disable once ConditionIsAlwaysTrueOrFalse
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
