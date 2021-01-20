using AutoFixture;
using FluentAssertions;
using QuantitativeWorld.Angular;
using QuantitativeWorld.TestAbstractions;
using Xunit;

namespace QuantitativeWorld.Tests.Angular
{
#if DECIMAL
    using Constants = QuantitativeWorld.DecimalConstants;
    using number = System.Decimal;
#else
    using number = System.Double;
    using Constants = QuantitativeWorld.DoubleConstants;
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
