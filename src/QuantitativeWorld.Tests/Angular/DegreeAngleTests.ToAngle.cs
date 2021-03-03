using FluentAssertions;
using Xunit;

#if DECIMAL
namespace DecimalQuantitativeWorld.Tests.Angular
{
    using DecimalQuantitativeWorld.Angular;
    using DecimalQuantitativeWorld.TestAbstractions;
    using number = System.Decimal;
#else
namespace QuantitativeWorld.Tests.Angular
{
    using QuantitativeWorld.Angular;
    using QuantitativeWorld.TestAbstractions;
    using number = System.Double;
#endif

    partial class DegreeAngleTests
    {
        public class ToAngle : DegreeAngleTests
        {
            public ToAngle(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void ShouldProduceValidResultInDegrees()
            {
                // arrange
                var sut = CreateDegreeAngle();

                // act
                var result = sut.ToAngle();

                // assert
                result.Value.Should().BeApproximately((number)sut.TotalDegrees);
                result.Unit.Should().Be(AngleUnit.Degree);
            }
        }
    }
}
