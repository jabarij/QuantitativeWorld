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

    partial class LengthUnitTests
    {
        public class Equality : LengthUnitTests
        {
            public Equality(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void DefaultLengthUnit_ShouldBeEqualToMetre()
            {
                // arrange
                var defaultLengthUnit = default(LengthUnit);
                var metre = LengthUnit.Metre;

                // act
                // assert
                metre.Equals(defaultLengthUnit).Should().BeTrue(because: "'LengthUnit.Metre' should be equal 'default(LengthUnit)'");
                defaultLengthUnit.Equals(metre).Should().BeTrue(because: "'default(LengthUnit)' should be equal 'LengthUnit.Metre'");
            }

            [Fact]
            public void ParamlessConstructedLengthUnit_ShouldBeEqualToMetre()
            {
                // arrange
                var paramlessConstructedLengthUnit = new LengthUnit();
                var metre = LengthUnit.Metre;

                // act
                // assert
                metre.Equals(paramlessConstructedLengthUnit).Should().BeTrue(because: "'LengthUnit.Metre' should be equal 'new LengthUnit()'");
                paramlessConstructedLengthUnit.Equals(metre).Should().BeTrue(because: "'new LengthUnit()' should be equal 'LengthUnit.Metre'");
            }
        }
    }
}
