using AutoFixture;
using FluentAssertions;
using QuantitativeWorld.TestAbstractions;
using Xunit;

namespace QuantitativeWorld.Tests
{
#if DECIMAL
    using number = System.Decimal;
    using Constants = QuantitativeWorld.DecimalConstants;
#else
    using number = System.Double;
    using Constants = QuantitativeWorld.DoubleConstants;
#endif

    partial class SpeedTests
    {
        public class Equality : SpeedTests
        {
            public Equality(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void DefaultSpeed_ShouldBeEqualToZeroMetresPerSecond()
            {
                // arrange
                var defaultSpeed = default(Speed);
                var zeroMetresPerSecondSpeed = new Speed(Constants.Zero);

                // act
                // assert
                zeroMetresPerSecondSpeed.Equals(defaultSpeed).Should().BeTrue(because: "'new Speed(Constants.Zero)' should be equal 'default(Speed)'");
                defaultSpeed.Equals(zeroMetresPerSecondSpeed).Should().BeTrue(because: "'default(Speed)' should be equal 'new Speed(Constants.Zero)'");
            }

            [Fact]
            public void SpeedCreateUsingParamlessConstructor_ShouldBeEqualToZeroMetresPerSecond()
            {
                // arrange
                var zeroMetresPerSecondSpeed = new Speed(Constants.Zero);
                var paramlessConstructedSpeed = new Speed();

                // act
                // assert
                zeroMetresPerSecondSpeed.Equals(paramlessConstructedSpeed).Should().BeTrue(because: "'new Speed(Constants.Zero)' should be equal 'new Speed()'");
                paramlessConstructedSpeed.Equals(zeroMetresPerSecondSpeed).Should().BeTrue(because: "'new Speed()' should be equal 'new Speed(Constants.Zero)'");
            }

            [Fact]
            public void ZeroUnitsSpeed_ShouldBeEqualToZeroMetresPerSecond()
            {
                // arrange
                var zeroMetresPerSecondSpeed = new Speed(Constants.Zero);
                var zeroUnitsSpeed = new Speed(Constants.Zero, CreateUnitOtherThan(SpeedUnit.MetrePerSecond));

                // act
                // assert
                zeroMetresPerSecondSpeed.Equals(zeroUnitsSpeed).Should().BeTrue(because: "'new Speed(Constants.Zero)' should be equal 'new Speed(Constants.Zero, SomeUnit)'");
                zeroUnitsSpeed.Equals(zeroMetresPerSecondSpeed).Should().BeTrue(because: "'new Speed(Constants.Zero, SomeUnit)' should be equal 'new Speed(Constants.Zero)'");
            }

            [Fact]
            public void SpeedsConvertedToDifferentUnitsEqualInMetresPerSecond_ShouldBeEqual()
            {
                // arrange
                var length1 = new Speed(Fixture.Create<number>()).Convert(Fixture.Create<SpeedUnit>());
                var length2 = new Speed(length1.MetresPerSecond).Convert(CreateUnitOtherThan(length1.Unit));

                // act
                bool equalsResult = length1.Equals(length2);

                // assert
                equalsResult.Should().BeTrue();
            }
        }
    }
}
