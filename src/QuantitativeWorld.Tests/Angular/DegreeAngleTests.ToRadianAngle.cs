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

    partial class DegreeAngleTests
    {
        public class ToRadianAngle : DegreeAngleTests
        {
            public ToRadianAngle(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void ShouldProduceValidResult()
            {
                // arrange
                var sut = Fixture.Create<DegreeAngle>();

                // act
                var result = sut.ToRadianAngle();

                // assert
                result.Radians.Should().BeApproximately(sut.TotalDegrees * Constants.PI / (number)180m);
            }
        }
    }
}
