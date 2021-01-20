using FluentAssertions;
using QuantitativeWorld.Angular;
using QuantitativeWorld.TestAbstractions;
using Xunit;

namespace QuantitativeWorld.Tests.Angular
{
#if DECIMAL
    using number = System.Decimal;
#else
    using number = System.Double;
    using Constants = QuantitativeWorld.DoubleConstants;
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
