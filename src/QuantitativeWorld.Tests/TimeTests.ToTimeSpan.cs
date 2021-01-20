using AutoFixture;
using FluentAssertions;
using QuantitativeWorld.TestAbstractions;
using Xunit;

namespace QuantitativeWorld.Tests.Angular
{
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
