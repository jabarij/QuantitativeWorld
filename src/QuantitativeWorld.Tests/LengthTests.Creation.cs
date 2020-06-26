using AutoFixture;
using FluentAssertions;
using QuantitativeWorld.TestAbstractions;
using System.Collections.Generic;
using Xunit;

namespace QuantitativeWorld.Tests
{
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
                double metres = Fixture.Create<double>();

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
                var length = new Length(testData.Value, testData.Unit);

                // assert
                length.Metres.Should().BeApproximately(testData.ExpectedMetres, DoublePrecision);
                length.Value.Should().Be(testData.Value);
                length.Unit.Should().Be(testData.Unit);
            }
            private static IEnumerable<ConstructorForValueAndUnitTestData> GetConstructorForValueAndUnitTestData()
            {
                yield return new ConstructorForValueAndUnitTestData(1d, LengthUnit.Metre, 1d);
                yield return new ConstructorForValueAndUnitTestData(1d, LengthUnit.Millimetre, 0.001d);
                yield return new ConstructorForValueAndUnitTestData(1d, LengthUnit.Decimetre, 0.1d);
                yield return new ConstructorForValueAndUnitTestData(1d, LengthUnit.Inch, 0.0254d);
                yield return new ConstructorForValueAndUnitTestData(1d, LengthUnit.Foot, 0.3048d);
                yield return new ConstructorForValueAndUnitTestData(1d, LengthUnit.Yard, 0.9144d);
                yield return new ConstructorForValueAndUnitTestData(1d, LengthUnit.Rod, 5.0292d);
                yield return new ConstructorForValueAndUnitTestData(1d, LengthUnit.Chain, 20.1168d);
                yield return new ConstructorForValueAndUnitTestData(1d, LengthUnit.Furlong, 201.168d);
                yield return new ConstructorForValueAndUnitTestData(1d, LengthUnit.Mile, 1609.344d);
            }
            public class ConstructorForValueAndUnitTestData
            {
                public ConstructorForValueAndUnitTestData(double value, LengthUnit unit, double expectedMetres)
                {
                    Value = value;
                    Unit = unit;
                    ExpectedMetres = expectedMetres;
                }

                public double Value { get; }
                public LengthUnit Unit { get; }
                public double ExpectedMetres { get; }
            }

            [Theory]
            [InlineData(0.001, 1000)]
            [InlineData(1, 1000000)]
            [InlineData(1000, 1000000000)]
            public void FromKilometres_ShouldCreateValidLength(double kilometres, double millimetres)
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
