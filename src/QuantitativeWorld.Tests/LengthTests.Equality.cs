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
                var zeroMetresLength = new Length(0d);

                // act
                // assert
                zeroMetresLength.Equals(defaultLength).Should().BeTrue(because: "'new Length(0d)' should be equal 'default(Length)'");
                defaultLength.Equals(zeroMetresLength).Should().BeTrue(because: "'default(Length)' should be equal 'new Length(0d)'");
            }

            [Fact]
            public void LengthCreateUsingParamlessConstructor_ShouldBeEqualToZeroMetres()
            {
                // arrange
                var zeroMetresLength = new Length(0d);
                var paramlessConstructedLength = new Length();

                // act
                // assert
                zeroMetresLength.Equals(paramlessConstructedLength).Should().BeTrue(because: "'new Length(0d)' should be equal 'new Length()'");
                paramlessConstructedLength.Equals(zeroMetresLength).Should().BeTrue(because: "'new Length()' should be equal 'new Length(0d)'");
            }

            [Fact]
            public void ZeroUnitsLength_ShouldBeEqualToZeroMetres()
            {
                // arrange
                var zeroMetresLength = new Length(0d);
                var zeroUnitsLength = new Length(0d, CreateUnitOtherThan(LengthUnit.Metre));

                // act
                // assert
                zeroMetresLength.Equals(zeroUnitsLength).Should().BeTrue(because: "'new Length(0d)' should be equal 'new Length(0d, SomeUnit)'");
                zeroUnitsLength.Equals(zeroMetresLength).Should().BeTrue(because: "'new Length(0d, SomeUnit)' should be equal 'new Length(0d)'");
            }

            [Fact]
            public void LengthsConvertedToDifferentUnitsEqualInMetres_ShouldBeEqual()
            {
                // arrange
                var length1 = new Length(Fixture.Create<double>()).Convert(Fixture.Create<LengthUnit>());
                var length2 = new Length(length1.Metres).Convert(CreateUnitOtherThan(length1.Unit));

                // act
                bool equalsResult = length1.Equals(length2);

                // assert
                equalsResult.Should().BeTrue();
            }
        }
    }
}
