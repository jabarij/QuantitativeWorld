using FluentAssertions;
using QuantitativeWorld.TestAbstractions;
using Xunit;

namespace QuantitativeWorld.Tests.Angular
{
    partial class TimeTests
    {
        public class Equality : TimeTests
        {
            public Equality(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void DefaultTime_ShouldBeEqualToZeroHours()
            {
                // arrange
                var defaultTime = default(Time);
                var zeroTime = new Time(0d);

                // act
                // assert
                zeroTime.Equals(defaultTime).Should().BeTrue(because: "'new Time(0d)' should be equal 'default(Time)'");
                defaultTime.Equals(zeroTime).Should().BeTrue(because: "'default(Time)' should be equal 'new Time(0d)'");
            }

            [Fact]
            public void TimeCreateUtinsParamlessConstructor_ShouldBeEqualToZeroHours()
            {
                // arrange
                var zeroTime = new Time(0d);
                var paramlessConstructedTime = new Time();

                // act
                // assert
                zeroTime.Equals(paramlessConstructedTime).Should().BeTrue(because: "'new Time(0d)' should be equal 'new Time()'");
                paramlessConstructedTime.Equals(zeroTime).Should().BeTrue(because: "'new Time()' should be equal 'new Time(0d)'");
            }
        }
    }
}
