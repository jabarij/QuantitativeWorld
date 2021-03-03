using AutoFixture;
using FluentAssertions;
using Xunit;

#if DECIMAL
namespace DecimalQuantitativeWorld.Tests.Angular
{
    using DecimalQuantitativeWorld.Angular;
    using DecimalQuantitativeWorld.TestAbstractions;
    using Constants = DecimalConstants;
    using number = System.Decimal;
#else
namespace QuantitativeWorld.Tests.Angular
{
    using QuantitativeWorld.Angular;
    using QuantitativeWorld.TestAbstractions;
    using Constants = DoubleConstants;
    using number = System.Double;
#endif

    partial class RadianAngleTests
    {
        public class ToDegreeAngle : RadianAngleTests
        {
            public ToDegreeAngle(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void ShouldProduceValidResult()
            {
                // arrange
                var sut = Fixture.Create<RadianAngle>();

                // act
                var result = sut.ToDegreeAngle();

                // assert
                result.TotalDegrees.Should().BeApproximately(sut.Radians * (number)180m / Constants.PI);
            }
        }
    }
}
