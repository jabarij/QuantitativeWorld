using AutoFixture;
using FluentAssertions;
using QuantitativeWorld.TestAbstractions;
using Xunit;

namespace QuantitativeWorld.Tests
{
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
                var zeroMetresArea = new Area(0d);

                // act
                // assert
                zeroMetresArea.Equals(defaultArea).Should().BeTrue(because: "'new Area(0d)' should be equal 'default(Area)'");
                defaultArea.Equals(zeroMetresArea).Should().BeTrue(because: "'default(Area)' should be equal 'new Area(0d)'");
            }

            [Fact]
            public void AreaCreateUtinsParamlessConstructor_ShouldBeEqualToZeroMetres()
            {
                // arrange
                var zeroMetresArea = new Area(0d);
                var paramlessConstructedArea = new Area();

                // act
                // assert
                zeroMetresArea.Equals(paramlessConstructedArea).Should().BeTrue(because: "'new Area(0d)' should be equal 'new Area()'");
                paramlessConstructedArea.Equals(zeroMetresArea).Should().BeTrue(because: "'new Area()' should be equal 'new Area(0d)'");
            }

            [Fact]
            public void ZeroUnitsArea_ShouldBeEqualToZeroMetres()
            {
                // arrange
                var zeroMetresArea = new Area(0d);
                var zeroUnitsArea = new Area(0d, CreateUnitOtherThan(AreaUnit.SquareMetre));

                // act
                // assert
                zeroMetresArea.Equals(zeroUnitsArea).Should().BeTrue(because: "'new Area(0d)' should be equal 'new Area(0d, SomeUnit)'");
                zeroUnitsArea.Equals(zeroMetresArea).Should().BeTrue(because: "'new Area(0d, SomeUnit)' should be equal 'new Area(0d)'");
            }

            [Fact]
            public void AreasConvertedToDifferentUnitsEqualInMetres_ShouldBeEqual()
            {
                // arrange
                var area1 = new Area(Fixture.Create<double>()).Convert(Fixture.Create<AreaUnit>());
                var area2 = new Area(area1.SquareMetres).Convert(CreateUnitOtherThan(area1.Unit));

                // act
                bool equalsResult = area1.Equals(area2);

                // assert
                equalsResult.Should().BeTrue();
            }
        }
    }
}
