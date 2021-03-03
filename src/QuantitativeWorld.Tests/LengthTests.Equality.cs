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
                var zeroMetresLength = new Length(Constants.Zero);

                // act
                // assert
                zeroMetresLength.Equals(defaultLength).Should().BeTrue(because: "'new Length(Constants.Zero)' should be equal 'default(Length)'");
                defaultLength.Equals(zeroMetresLength).Should().BeTrue(because: "'default(Length)' should be equal 'new Length(Constants.Zero)'");
            }

            [Fact]
            public void LengthCreateUsingParamlessConstructor_ShouldBeEqualToZeroMetres()
            {
                // arrange
                var zeroMetresLength = new Length(Constants.Zero);
                var paramlessConstructedLength = new Length();

                // act
                // assert
                zeroMetresLength.Equals(paramlessConstructedLength).Should().BeTrue(because: "'new Length(Constants.Zero)' should be equal 'new Length()'");
                paramlessConstructedLength.Equals(zeroMetresLength).Should().BeTrue(because: "'new Length()' should be equal 'new Length(Constants.Zero)'");
            }

            [Fact]
            public void ZeroUnitsLength_ShouldBeEqualToZeroMetres()
            {
                // arrange
                var zeroMetresLength = new Length(Constants.Zero);
                var zeroUnitsLength = new Length(Constants.Zero, CreateUnitOtherThan(LengthUnit.Metre));

                // act
                // assert
                zeroMetresLength.Equals(zeroUnitsLength).Should().BeTrue(because: "'new Length(Constants.Zero)' should be equal 'new Length(Constants.Zero, SomeUnit)'");
                zeroUnitsLength.Equals(zeroMetresLength).Should().BeTrue(because: "'new Length(Constants.Zero, SomeUnit)' should be equal 'new Length(Constants.Zero)'");
            }

            [Fact]
            public void LengthsConvertedToDifferentUnitsEqualInMetres_ShouldBeEqual()
            {
                // arrange
                var length1 = new Length(Fixture.Create<number>()).Convert(Fixture.Create<LengthUnit>());
                var length2 = new Length(length1.Metres).Convert(CreateUnitOtherThan(length1.Unit));

                // act
                bool equalsResult = length1.Equals(length2);

                // assert
                equalsResult.Should().BeTrue();
            }
        }
    }
}
