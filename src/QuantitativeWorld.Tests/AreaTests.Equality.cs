using AutoFixture;
using FluentAssertions;
using Xunit;

#if DECIMAL
namespace DecimalQuantitativeWorld.Tests
{
    using DecimalQuantitativeWorld.TestAbstractions;
    using Constants = DecimalConstants;
    using number = System.Decimal;
#else
namespace QuantitativeWorld.Tests
{
    using QuantitativeWorld.TestAbstractions;
    using Constants = DoubleConstants;
    using number = System.Double;
#endif

    partial class AreaTests
    {
        public class Equality : AreaTests
        {
            public Equality(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void DefaultArea_ShouldBeEqualToZeroMetres()
            {
                // arrange
                var defaultArea = default(Area);
                var zeroMetresArea = new Area(Constants.Zero);

                // act
                // assert
                zeroMetresArea.Equals(defaultArea).Should().BeTrue(because: "'new Area(Constants.Zero)' should be equal 'default(Area)'");
                defaultArea.Equals(zeroMetresArea).Should().BeTrue(because: "'default(Area)' should be equal 'new Area(Constants.Zero)'");
            }

            [Fact]
            public void AreaCreateUsingParamlessConstructor_ShouldBeEqualToZeroMetres()
            {
                // arrange
                var zeroMetresArea = new Area(Constants.Zero);
                var paramlessConstructedArea = new Area();

                // act
                // assert
                zeroMetresArea.Equals(paramlessConstructedArea).Should().BeTrue(because: "'new Area(Constants.Zero)' should be equal 'new Area()'");
                paramlessConstructedArea.Equals(zeroMetresArea).Should().BeTrue(because: "'new Area()' should be equal 'new Area(Constants.Zero)'");
            }

            [Fact]
            public void ZeroUnitsArea_ShouldBeEqualToZeroMetres()
            {
                // arrange
                var zeroMetresArea = new Area(Constants.Zero);
                var zeroUnitsArea = new Area(Constants.Zero, CreateUnitOtherThan(AreaUnit.SquareMetre));

                // act
                // assert
                zeroMetresArea.Equals(zeroUnitsArea).Should().BeTrue(because: "'new Area(Constants.Zero)' should be equal 'new Area(Constants.Zero, SomeUnit)'");
                zeroUnitsArea.Equals(zeroMetresArea).Should().BeTrue(because: "'new Area(Constants.Zero, SomeUnit)' should be equal 'new Area(Constants.Zero)'");
            }

            [Fact]
            public void AreasConvertedToDifferentUnitsEqualInMetres_ShouldBeEqual()
            {
                // arrange
                var area1 = new Area(Fixture.Create<number>()).Convert(Fixture.Create<AreaUnit>());
                var area2 = new Area(area1.SquareMetres).Convert(CreateUnitOtherThan(area1.Unit));

                // act
                bool equalsResult = area1.Equals(area2);

                // assert
                equalsResult.Should().BeTrue();
            }
        }
    }
}
