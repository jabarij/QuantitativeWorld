using AutoFixture;
using FluentAssertions;
using QuantitativeWorld.Angular;
using QuantitativeWorld.TestAbstractions;
using Xunit;

namespace QuantitativeWorld.Tests.Angular
{
#if DECIMAL
    using number = System.Decimal;
    using Constants = QuantitativeWorld.DecimalConstants;
#else
    using number = System.Double;
    using Constants = QuantitativeWorld.DoubleConstants;
#endif

    partial class AngleTests
    {
        public class Equality : AngleTests
        {
            public Equality(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void DefaultAngle_ShouldBeEqualToZeroTurns()
            {
                // arrange
                var defaultAngle = default(Angle);
                var zeroTurnsAngle = new Angle(Constants.Zero);

                // act
                // assert
                zeroTurnsAngle.Equals(defaultAngle).Should().BeTrue(because: "'new Angle(Constants.Zero)' should be equal 'default(Angle)'");
                defaultAngle.Equals(zeroTurnsAngle).Should().BeTrue(because: "'default(Angle)' should be equal 'new Angle(Constants.Zero)'");
            }

            [Fact]
            public void AngleCreateUsingParamlessConstructor_ShouldBeEqualToZeroTurns()
            {
                // arrange
                var zeroTurnsAngle = new Angle(Constants.Zero);
                var paramlessConstructedAngle = new Angle();

                // act
                // assert
                zeroTurnsAngle.Equals(paramlessConstructedAngle).Should().BeTrue(because: "'new Angle(Constants.Zero)' should be equal 'new Angle()'");
                paramlessConstructedAngle.Equals(zeroTurnsAngle).Should().BeTrue(because: "'new Angle()' should be equal 'new Angle(Constants.Zero)'");
            }

            [Fact]
            public void ZeroUnitsAngle_ShouldBeEqualToZeroTurns()
            {
                // arrange
                var zeroTurnsAngle = new Angle(Constants.Zero);
                var zeroUnitsAngle = new Angle(Constants.Zero, CreateUnitOtherThan(AngleUnit.Turn));

                // act
                // assert
                zeroTurnsAngle.Equals(zeroUnitsAngle).Should().BeTrue(because: "'new Angle(Constants.Zero)' should be equal 'new Angle(Constants.Zero, SomeUnit)'");
                zeroUnitsAngle.Equals(zeroTurnsAngle).Should().BeTrue(because: "'new Angle(Constants.Zero, SomeUnit)' should be equal 'new Angle(Constants.Zero)'");
            }

            [Fact]
            public void AnglesConvertedToDifferentUnitsEqualInTurns_ShouldBeEqual()
            {
                // arrange
                var angle1 = new Angle(Fixture.Create<number>()).Convert(Fixture.Create<AngleUnit>());
                var angle2 = new Angle(angle1.Turns).Convert(CreateUnitOtherThan(angle1.Unit));

                // act
                bool equalsResult = angle1.Equals(angle2);

                // assert
                equalsResult.Should().BeTrue();
            }
        }
    }
}
