using AutoFixture;
using FluentAssertions;
using Xunit;

#if DECIMAL
namespace DecimalQuantitativeWorld.Tests
{
    using DecimalQuantitativeWorld.TestAbstractions;
#else
namespace QuantitativeWorld.Tests
{
    using QuantitativeWorld.TestAbstractions;
    using Constants = DoubleConstants;
    using number = System.Double;
#endif

    partial class TimeTests
    {
        public class ToTimeSpan : TimeTests
        {
            public ToTimeSpan(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void ShouldProduceValidResult()
            {
                // arrange
                var sut = Fixture.Create<Time>();

                // act
                var result = sut.ToTimeSpan();

                // assert
                result.TotalSeconds.Should().BeApproximately(sut.TotalSeconds);
            }
        }
    }
}
