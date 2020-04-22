using AutoFixture;
using FluentAssertions;
using QuantitativeWorld.Angular;
using QuantitativeWorld.TestAbstractions;
using Xunit;

namespace QuantitativeWorld.Tests.Angular
{
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
                var zeroTurnsAngle = new Angle(0d);

                // act
                // assert
                zeroTurnsAngle.Equals(defaultAngle).Should().BeTrue(because: "'new Angle(0d)' should be equal 'default(Angle)'");
                defaultAngle.Equals(zeroTurnsAngle).Should().BeTrue(because: "'default(Angle)' should be equal 'new Angle(0d)'");
            }

            [Fact]
            public void AngleCreateUtinsParamlessConstructor_ShouldBeEqualToZeroTurns()
            {
                // arrange
                var zeroTurnsAngle = new Angle(0d);
                var paramlessConstructedAngle = new Angle();

                // act
                // assert
                zeroTurnsAngle.Equals(paramlessConstructedAngle).Should().BeTrue(because: "'new Angle(0d)' should be equal 'new Angle()'");
                paramlessConstructedAngle.Equals(zeroTurnsAngle).Should().BeTrue(because: "'new Angle()' should be equal 'new Angle(0d)'");
            }

            [Fact]
            public void ZeroUnitsAngle_ShouldBeEqualToZeroTurns()
            {
                // arrange
                var zeroTurnsAngle = new Angle(0d);
                var zeroUnitsAngle = new Angle(0d, CreateUnitOtherThan(AngleUnit.Turn));

                // act
                // assert
                zeroTurnsAngle.Equals(zeroUnitsAngle).Should().BeTrue(because: "'new Angle(0d)' should be equal 'new Angle(0d, SomeUnit)'");
                zeroUnitsAngle.Equals(zeroTurnsAngle).Should().BeTrue(because: "'new Angle(0d, SomeUnit)' should be equal 'new Angle(0d)'");
            }

            [Fact]
            public void AnglesConvertedToDifferentUnitsEqualInTurns_ShouldBeEqual()
            {
                // arrange
                var angle1 = new Angle(Fixture.Create<double>()).Convert(Fixture.Create<AngleUnit>());
                var angle2 = new Angle(angle1.Turns).Convert(CreateUnitOtherThan(angle1.Unit));

                // act
                bool equalsResult = angle1.Equals(angle2);

                // assert
                equalsResult.Should().BeTrue();
            }
        }
    }
}
