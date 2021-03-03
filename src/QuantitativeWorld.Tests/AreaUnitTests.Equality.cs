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
#endif

    partial class AreaUnitTests
    {
        public class Equality : AreaUnitTests
        {
            public Equality(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void DefaultAreaUnit_ShouldBeEqualToMetre()
            {
                // arrange
                var defaultAreaUnit = default(AreaUnit);
                var metre = AreaUnit.SquareMetre;

                // act
                // assert
                metre.Equals(defaultAreaUnit).Should().BeTrue(because: "'AreaUnit.SquareMetre' should be equal 'default(AreaUnit)'");
                defaultAreaUnit.Equals(metre).Should().BeTrue(because: "'default(AreaUnit)' should be equal 'AreaUnit.SquareMetre'");
            }

            [Fact]
            public void ParamlessConstructedAreaUnit_ShouldBeEqualToMetre()
            {
                // arrange
                var paramlessConstructedAreaUnit = new AreaUnit();
                var metre = AreaUnit.SquareMetre;

                // act
                // assert
                metre.Equals(paramlessConstructedAreaUnit).Should().BeTrue(because: "'AreaUnit.SquareMetre' should be equal 'new AreaUnit()'");
                paramlessConstructedAreaUnit.Equals(metre).Should().BeTrue(because: "'new AreaUnit()' should be equal 'AreaUnit.SquareMetre'");
            }
        }
    }
}
