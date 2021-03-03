using AutoFixture;
using FluentAssertions;
using System.Collections.Generic;
using Xunit;

#if DECIMAL
namespace DecimalQuantitativeWorld.Tests
{
    using DecimalQuantitativeWorld.TestAbstractions;
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
        public class Creation : LengthTests
        {
            public Creation(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void ConstructorForMetres_ShouldCreateValidLength()
            {
                // arrange
                number metres = Fixture.Create<number>();

                // act
                var length = new Length(metres);

                // assert
                length.Metres.Should().Be(metres);
                length.Value.Should().Be(metres);
                length.Unit.Should().Be(LengthUnit.Metre);
            }

            [Theory]
            [MemberData(nameof(GetTestData), typeof(Creation), nameof(GetConstructorForValueAndUnitTestData))]
            public void ConstructorForValueAndUnit_ShouldCreateValidLength(ConstructorForValueAndUnitTestData testData)
            {
                // arrange
                // act
                var length = new Length(testData.OriginalValue.Value, testData.OriginalValue.Unit);

                // assert
                length.Metres.Should().BeApproximately(testData.ExpectedValue.Metres);
                length.Value.Should().BeApproximately(testData.OriginalValue.Value);
                length.Unit.Should().Be(testData.OriginalValue.Unit);
            }
            private static IEnumerable<ConstructorForValueAndUnitTestData> GetConstructorForValueAndUnitTestData()
            {
                yield return new ConstructorForValueAndUnitTestData(1m, LengthUnit.Metre, 1m);
                yield return new ConstructorForValueAndUnitTestData(1m, LengthUnit.Millimetre, 0.001m);
                yield return new ConstructorForValueAndUnitTestData(1m, LengthUnit.Decimetre, 0.1m);
                yield return new ConstructorForValueAndUnitTestData(1m, LengthUnit.Inch, 0.0254m);
                yield return new ConstructorForValueAndUnitTestData(1m, LengthUnit.Foot, 0.3048m);
                yield return new ConstructorForValueAndUnitTestData(1m, LengthUnit.Yard, 0.9144m);
                yield return new ConstructorForValueAndUnitTestData(1m, LengthUnit.Rod, 5.0292m);
                yield return new ConstructorForValueAndUnitTestData(1m, LengthUnit.Chain, 20.1168m);
                yield return new ConstructorForValueAndUnitTestData(1m, LengthUnit.Furlong, 201.168m);
                yield return new ConstructorForValueAndUnitTestData(1m, LengthUnit.Mile, 1609.344m);
            }
            public class ConstructorForValueAndUnitTestData : ConversionTestData<Length>
            {
                public ConstructorForValueAndUnitTestData(decimal value, LengthUnit unit, decimal expectedMetres)
                    : base(new Length((number)value, unit), new Length((number)expectedMetres)) { }
                public ConstructorForValueAndUnitTestData(double value, LengthUnit unit, double expectedMetres)
                    : base(new Length((number)value, unit), new Length((number)expectedMetres)) { }
            }

            [Theory]
            [InlineData(0.001, 1000)]
            [InlineData(1, 1000000)]
            [InlineData(1000, 1000000000)]
            public void FromKilometres_ShouldCreateValidLength(number kilometres, number millimetres)
            {
                // arrange
                var expectedLength = new Length(millimetres, LengthUnit.Millimetre);

                // act
                var actualLength = new Length(kilometres, LengthUnit.Kilometre);

                // assert
                actualLength.Should().Be(expectedLength);
            }
        }
    }
}
