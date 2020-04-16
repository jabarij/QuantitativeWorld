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
                var zeroTurnsAngle = new Angle(0m);

                // act
                // assert
                zeroTurnsAngle.Equals(defaultAngle).Should().BeTrue(because: "'new Angle(0m)' should be equal 'default(Angle)'");
                defaultAngle.Equals(zeroTurnsAngle).Should().BeTrue(because: "'default(Angle)' should be equal 'new Angle(0m)'");
            }

            [Fact]
            public void AngleCreateUtinsParamlessConstructor_ShouldBeEqualToZeroTurns()
            {
                // arrange
                var zeroTurnsAngle = new Angle(0m);
                var paramlessConstructedAngle = new Angle();

                // act
                // assert
                zeroTurnsAngle.Equals(paramlessConstructedAngle).Should().BeTrue(because: "'new Angle(0m)' should be equal 'new Angle()'");
                paramlessConstructedAngle.Equals(zeroTurnsAngle).Should().BeTrue(because: "'new Angle()' should be equal 'new Angle(0m)'");
            }

            [Fact]
            public void ZeroUnitsAngle_ShouldBeEqualToZeroTurns()
            {
                // arrange
                var zeroTurnsAngle = new Angle(0m);
                var zeroUnitsAngle = new Angle(0m, CreateUnitOtherThan(AngleUnit.Turn));

                // act
                // assert
                zeroTurnsAngle.Equals(zeroUnitsAngle).Should().BeTrue(because: "'new Angle(0m)' should be equal 'new Angle(0m, SomeUnit)'");
                zeroUnitsAngle.Equals(zeroTurnsAngle).Should().BeTrue(because: "'new Angle(0m, SomeUnit)' should be equal 'new Angle(0m)'");
            }

            [Fact]
            public void AnglesOfDifferentUnitsEqualInTurns_ShouldBeEqual()
            {
                // arrange
                var angle1 = new Angle(
                    value: Fixture.Create<decimal>(),
                    unit: AngleUnit.Degree);
                var angle2 = new Angle(
                    value: angle1.Turns * 400m,
                    unit: AngleUnit.Gradian);

                // act
                bool equalsResult = angle1.Equals(angle2);

                // assert
                equalsResult.Should().BeTrue(because: $"{angle1.Value} {angle1.Unit} ({angle1.Turns} ,) == {angle2.Value} {angle2.Unit} ({angle2.Turns} ,)");
            }
        }
    }
}
