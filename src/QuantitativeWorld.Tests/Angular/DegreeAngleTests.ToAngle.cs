using FluentAssertions;
using QuantitativeWorld.Angular;
using QuantitativeWorld.TestAbstractions;
using Xunit;

namespace QuantitativeWorld.Tests.Angular
{
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
                result.Value.Should().BeApproximately((decimal)sut.TotalDegrees, DecimalPrecision);
                result.Unit.Should().Be(AngleUnit.Degree);
            }
        }
    }
}
