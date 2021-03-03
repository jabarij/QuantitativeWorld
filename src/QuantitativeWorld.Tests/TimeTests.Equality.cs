using FluentAssertions;
using Xunit;

#if DECIMAL
namespace DecimalQuantitativeWorld.Tests
{
    using DecimalQuantitativeWorld.TestAbstractions;
    using Constants = DecimalConstants;
#else
namespace QuantitativeWorld.Tests
{
    using QuantitativeWorld.TestAbstractions;
    using Constants = DoubleConstants;
    using number = System.Double;
#endif

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
                var zeroTime = new Time(Constants.Zero);

                // act
                // assert
                zeroTime.Equals(defaultTime).Should().BeTrue(because: "'new Time(Constants.Zero)' should be equal 'default(Time)'");
                defaultTime.Equals(zeroTime).Should().BeTrue(because: "'default(Time)' should be equal 'new Time(Constants.Zero)'");
            }

            [Fact]
            public void TimeCreateUsingParamlessConstructor_ShouldBeEqualToZeroHours()
            {
                // arrange
                var zeroTime = new Time(Constants.Zero);
                var paramlessConstructedTime = new Time();

                // act
                // assert
                zeroTime.Equals(paramlessConstructedTime).Should().BeTrue(because: "'new Time(Constants.Zero)' should be equal 'new Time()'");
                paramlessConstructedTime.Equals(zeroTime).Should().BeTrue(because: "'new Time()' should be equal 'new Time(Constants.Zero)'");
            }
        }
    }
}
