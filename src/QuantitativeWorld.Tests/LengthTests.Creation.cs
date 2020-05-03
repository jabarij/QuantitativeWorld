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
                yield return new ConstructorForValueAndUnitTestData(1000d, LengthUnit.Millimetre, 1d);
                yield return new ConstructorForValueAndUnitTestData(10d, LengthUnit.Decimetre, 1d);
                yield return new ConstructorForValueAndUnitTestData(1 / 0.0254d, LengthUnit.Inch, 1d);
                yield return new ConstructorForValueAndUnitTestData(1 / (0.0254d * 12d), LengthUnit.Foot, 1d);
                yield return new ConstructorForValueAndUnitTestData(1 / (0.0254d * 12d * 3d), LengthUnit.Yard, 1d);
                yield return new ConstructorForValueAndUnitTestData(1 / (0.0254d * 12d * 3d * 5.5d), LengthUnit.Rod, 1d);
                yield return new ConstructorForValueAndUnitTestData(1 / (0.0254d * 12d * 3d * 5.5d * 4d), LengthUnit.Chain, 1d);
                yield return new ConstructorForValueAndUnitTestData(1 / (0.0254d * 12d * 3d * 5.5d * 4d * 10d), LengthUnit.Furlong, 1d);
                yield return new ConstructorForValueAndUnitTestData(1 / (0.0254d * 12d * 3d * 5.5d * 4d * 10d * 8d), LengthUnit.Mile, 1d);
                yield return new ConstructorForValueAndUnitTestData(0.0006213711922373339696174342d, LengthUnit.Mile, 1d);
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
