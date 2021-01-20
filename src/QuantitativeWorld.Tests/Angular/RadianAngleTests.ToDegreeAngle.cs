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
