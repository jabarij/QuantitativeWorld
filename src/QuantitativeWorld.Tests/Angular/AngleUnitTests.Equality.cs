using FluentAssertions;
using Xunit;

#if DECIMAL
namespace DecimalQuantitativeWorld.Tests.Angular
{
    using DecimalQuantitativeWorld.Angular;
    using DecimalQuantitativeWorld.TestAbstractions;
#else
namespace QuantitativeWorld.Tests.Angular
{
    using QuantitativeWorld.Angular;
    using QuantitativeWorld.TestAbstractions;
#endif

    partial class AngleUnitTests
    {
        public class Equality : AngleUnitTests
        {
            public Equality(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void DefaultAngleUnit_ShouldBeEqualToTurn()
            {
                // arrange
                var defaultAngleUnit = default(AngleUnit);
                var turn = AngleUnit.Turn;

                // act
                // assert
                turn.Equals(defaultAngleUnit).Should().BeTrue(because: "'AngleUnit.Turn' should be equal 'default(AngleUnit)'");
                defaultAngleUnit.Equals(turn).Should().BeTrue(because: "'default(AngleUnit)' should be equal 'AngleUnit.Turn'");
            }

            [Fact]
            public void ParamlessConstructedAngleUnit_ShouldBeEqualToTurn()
            {
                // arrange
                var paramlessConstructedAngleUnit = new AngleUnit();
                var turn = AngleUnit.Turn;

                // act
                // assert
                turn.Equals(paramlessConstructedAngleUnit).Should().BeTrue(because: "'AngleUnit.Turn' should be equal 'new AngleUnit()'");
                paramlessConstructedAngleUnit.Equals(turn).Should().BeTrue(because: "'new AngleUnit()' should be equal 'AngleUnit.Turn'");
            }
        }
    }
}
