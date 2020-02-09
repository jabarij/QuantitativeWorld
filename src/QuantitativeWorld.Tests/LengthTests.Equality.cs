using AutoFixture;
using FluentAssertions;
using QuantitativeWorld.TestAbstractions;
using Xunit;

namespace QuantitativeWorld.Tests
{
    partial class LengthTests
    {
        public class Equality : LengthTests
        {
            public Equality(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void DefaultLength_ShouldBeEqualToZeroMetres()
            {
                // arrange
                var defaultLength = default(Length);
                var zeroMetresLength = new Length(0m);

                // act
                // assert
                zeroMetresLength.Equals(defaultLength).Should().BeTrue(because: "'new Length(0m)' should be equal 'default(Length)'");
                defaultLength.Equals(zeroMetresLength).Should().BeTrue(because: "'default(Length)' should be equal 'new Length(0m)'");
            }

            [Fact]
            public void LengthCreateUtinsParamlessConstructor_ShouldBeEqualToZeroMetres()
            {
                // arrange
                var zeroMetresLength = new Length(0m);
                var paramlessConstructedLength = new Length();

                // act
                // assert
                zeroMetresLength.Equals(paramlessConstructedLength).Should().BeTrue(because: "'new Length(0m)' should be equal 'new Length()'");
                paramlessConstructedLength.Equals(zeroMetresLength).Should().BeTrue(because: "'new Length()' should be equal 'new Length(0m)'");
            }

            [Fact]
            public void ZeroUnitsLength_ShouldBeEqualToZeroMetres()
            {
                // arrange
                var zeroMetresLength = new Length(0m);
                var zeroUnitsLength = new Length(0m, CreateUnitOtherThan(LengthUnit.Metre));

                // act
                // assert
                zeroMetresLength.Equals(zeroUnitsLength).Should().BeTrue(because: "'new Length(0m)' should be equal 'new Length(0m, SomeUnit)'");
                zeroUnitsLength.Equals(zeroMetresLength).Should().BeTrue(because: "'new Length(0m, SomeUnit)' should be equal 'new Length(0m)'");
            }

            [Fact]
            public void LengthsOfDifferentUnitsEqualInMetres_ShouldBeEqual()
            {
                // arrange
                var length1 = new Length(
                    value: Fixture.Create<decimal>(),
                    unit: LengthUnit.Kilometre);
                var length2 = new Length(
                    value: length1.Metres * 1000m,
                    unit: LengthUnit.Millimetre);

                // act
                bool equalsResult = length1.Equals(length2);

                // assert
                equalsResult.Should().BeTrue(because: $"{length1.Value} {length1.Unit} ({length1.Metres} ,) == {length2.Value} {length2.Unit} ({length2.Metres} ,)");
            }
        }
    }
}
