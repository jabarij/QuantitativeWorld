using AutoFixture;
using FluentAssertions;
using System;
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
        public class Operator_Oposite : AreaTests
        {
            public Operator_Oposite(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void ShouldProduceValidResultInSameUnit()
            {
                // arrange
                var area = CreateAreaInUnitOtherThan(AreaUnit.SquareMetre);

                // act
                var result = -area;

                // assert
                result.SquareMetres.Should().Be(-area.SquareMetres);
                result.Value.Should().Be(-area.Value);
                result.Unit.Should().Be(area.Unit);
            }

            [Fact]
            public void NullArea_ShouldReturnNull()
            {
                // arrange
                Area? area = null;

                // act
                var result = -area;

                // assert
                result.Should().BeNull();
            }
        }

        public class Operator_Add : AreaTests
        {
            public Operator_Add(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void TwoDefaultAreas_ShouldProduceDefaultArea()
            {
                // arrange
                var area1 = default(Area);
                var area2 = default(Area);

                // act
                var result = area1 + area2;

                // assert
                result.Should().Be(default(Area));
            }

            [Fact]
            public void DefaultAreaAndZeroWithOtherUnit_ShouldProduceZeroWithOtherUnit()
            {
                // arrange
                var defaultArea = default(Area);
                var zeroSquareKilometres = new Area(Constants.Zero, AreaUnit.SquareKilometre);

                // act
                var result1 = defaultArea + zeroSquareKilometres;
                var result2 = zeroSquareKilometres + defaultArea;

                // assert
                result1.IsZero().Should().BeTrue();
                result1.Unit.Should().Be(zeroSquareKilometres.Unit);
                result2.IsZero().Should().BeTrue();
                result2.Unit.Should().Be(zeroSquareKilometres.Unit);
            }

            [Fact]
            public void ShouldProduceValidResultInUnitOfLeftOperand()
            {
                // arrange
                var area1 = CreateAreaInUnitOtherThan(AreaUnit.SquareMetre);
                var area2 = CreateAreaInUnitOtherThan(AreaUnit.SquareMetre, area1.Unit);

                // act
                var result = area1 + area2;

                // assert
                result.SquareMetres.Should().Be(area1.SquareMetres + area2.SquareMetres);
                result.Unit.Should().Be(area1.Unit);
            }

            [Fact]
            public void NullAreas_ShouldReturnNull()
            {
                // arrange
                Area? nullArea1 = null;
                Area? nullArea2 = null;

                // act
                var result = nullArea1 + nullArea2;

                // assert
                result.Should().BeNull();
            }

            [Fact]
            public void NullAndArea_ShouldTreatNullAsDefault()
            {
                // arrange
                Area? nullArea = null;
                var area = CreateAreaInUnitOtherThan(AreaUnit.SquareMetre);

                // act
                var result1 = area + nullArea;
                var result2 = nullArea + area;

                // assert
                result1.Should().NotBeNull();
                result1.Value.SquareMetres.Should().Be(area.SquareMetres);
                result1.Value.Unit.Should().Be(area.Unit);

                result2.Should().NotBeNull();
                result2.Value.SquareMetres.Should().Be(area.SquareMetres);
                result2.Value.Unit.Should().Be(area.Unit);
            }
        }

        public class Operator_Subtract : AreaTests
        {
            public Operator_Subtract(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void TwoDefaultAreas_ShouldProduceDefaultArea()
            {
                // arrange
                var area1 = default(Area);
                var area2 = default(Area);

                // act
                var result = area1 - area2;

                // assert
                result.Should().Be(default(Area));
            }

            [Fact]
            public void DefaultAreaAndZeroWithOtherUnit_ShouldProduceZeroWithOtherUnit()
            {
                // arrange
                var defaultArea = default(Area);
                var zeroSquareKilometres = new Area(Constants.Zero, AreaUnit.SquareKilometre);

                // act
                var result1 = defaultArea - zeroSquareKilometres;
                var result2 = zeroSquareKilometres - defaultArea;

                // assert
                result1.IsZero().Should().BeTrue();
                result1.Unit.Should().Be(zeroSquareKilometres.Unit);
                result2.IsZero().Should().BeTrue();
                result2.Unit.Should().Be(zeroSquareKilometres.Unit);
            }

            [Fact]
            public void ShouldProduceValidResultInUnitOfLeftOperand()
            {
                // arrange
                var area1 = CreateAreaInUnitOtherThan(AreaUnit.SquareMetre);
                var area2 = CreateAreaInUnitOtherThan(AreaUnit.SquareMetre, area1.Unit);

                // act
                var result = area1 - area2;

                // assert
                result.SquareMetres.Should().Be(area1.SquareMetres - area2.SquareMetres);
                result.Unit.Should().Be(area1.Unit);
            }

            [Fact]
            public void NullAreas_ShouldReturnNull()
            {
                // arrange
                Area? nullArea1 = null;
                Area? nullArea2 = null;

                // act
                var result = nullArea1 - nullArea2;

                // assert
                result.Should().BeNull();
            }

            [Fact]
            public void NullAndArea_ShouldTreatNullAsDefault()
            {
                // arrange
                Area? nullArea = null;
                var area = CreateAreaInUnitOtherThan(AreaUnit.SquareMetre);

                // act
                var result1 = area - nullArea;
                var result2 = nullArea - area;

                // assert
                result1.Should().NotBeNull();
                result1.Value.SquareMetres.Should().Be(area.SquareMetres);
                result1.Value.Unit.Should().Be(area.Unit);

                result2.Should().NotBeNull();
                result2.Value.SquareMetres.Should().Be(-area.SquareMetres);
                result2.Value.Unit.Should().Be(area.Unit);
            }
        }

        public class Operator_MultiplyByDouble : AreaTests
        {
            public Operator_MultiplyByDouble(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void ShouldProduceValidResultInSameUnit()
            {
                // arrange
                var area = CreateAreaInUnitOtherThan(AreaUnit.SquareMetre);
                number factor = Fixture.Create<number>();

                // act
                var result = area * factor;

                // assert
                result.SquareMetres.Should().BeApproximately(area.SquareMetres * factor);
                result.Value.Should().BeApproximately(area.Value * factor);
                result.Unit.Should().Be(area.Unit);
            }

            [Fact]
            public void NullArea_ShouldTreatNullAsDefault()
            {
                // arrange
                Area? nullArea = null;
                number factor = Fixture.Create<number>();
                var expectedResult = default(Area) * factor;

                // act
                var result = nullArea * factor;

                // assert
                result.Should().NotBeNull();
                result.Value.Should().Be(expectedResult);
            }

#if !DECIMAL
            [Fact]
            public void MultiplyByNaN_ShouldThrow()
            {
                // arrange
                var area = CreateAreaInUnitOtherThan(AreaUnit.SquareMetre);

                // act
                Func<Area> multiplyByNaN = () => area * number.NaN;

                // assert
                multiplyByNaN.Should().Throw<ArgumentException>();
            }
#endif
        }

        public class Operator_MultiplyByLength : AreaTests
        {
            public Operator_MultiplyByLength(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void ShouldProduceValidResultInSquareMetres()
            {
                // arrange
                var argument = CreateAreaInUnitOtherThan(AreaUnit.SquareMetre);
                var factor = new Length(
                    value: Fixture.CreateNonZeroNumber(),
                    unit: Fixture.Create<LengthUnit>());

                // act
                var result1 = argument * factor;
                var result2 = factor * argument;

                // assert
                result1.CubicMetres.Should().Be(argument.SquareMetres * factor.Metres);
                result1.Unit.Should().Be(VolumeUnit.CubicMetre);
                result2.CubicMetres.Should().Be(argument.SquareMetres * factor.Metres);
                result2.Unit.Should().Be(VolumeUnit.CubicMetre);
            }

            [Fact]
            public void NullArgument_ShouldTreatNullAsDefault()
            {
                // arrange
                Area? argument = null;
                var factor = new Length(
                    value: Fixture.CreateNonZeroNumber(),
                    unit: Fixture.Create<LengthUnit>());
                var expectedResult = default(Area) * factor;

                // act
                var result1 = argument * factor;
                var result2 = factor * argument;

                // assert
                result1.Should().Be(expectedResult);
                result2.Should().Be(expectedResult);
            }

            [Fact]
            public void NullFactor_ShouldTreatNullAsDefault()
            {
                // arrange
                var argument = CreateAreaInUnitOtherThan(AreaUnit.SquareMetre);
                Length? factor = null;
                var expectedResult = argument * default(Length);

                // act
                var result1 = argument * factor;
                var result2 = factor * argument;

                // assert
                result1.Should().Be(expectedResult);
                result2.Should().Be(expectedResult);
            }

            [Fact]
            public void NullArgumentAndFactor_ShouldReturnNull()
            {
                // arrange
                Area? argument = null;
                Length? factor = null;

                // act
                var result1 = argument * factor;
                var result2 = factor * argument;

                // assert
                result1.Should().BeNull();
                result2.Should().BeNull();
            }
        }

        public class Operator_DivideByDouble : AreaTests
        {
            public Operator_DivideByDouble(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void DivideByZero_ShouldThrow()
            {
                // arrange
                var area = CreateAreaInUnitOtherThan(AreaUnit.SquareMetre);

                // act
                Func<Area> divideByZero = () => area / Constants.Zero;

                // assert
                divideByZero.Should().Throw<DivideByZeroException>();
            }

#if !DECIMAL
            [Fact]
            public void DivideByNaN_ShouldThrow()
            {
                // arrange
                var area = CreateAreaInUnitOtherThan(AreaUnit.SquareMetre);

                // act
                Func<Area> divideByNaN = () => area / double.NaN;

                // assert
                divideByNaN.Should().Throw<ArgumentException>();
            } 
#endif

            [Fact]
            public void ShouldProduceValidResultInSameUnit()
            {
                // arrange
                var area = CreateAreaInUnitOtherThan(AreaUnit.SquareMetre);
                number denominator = (number)Fixture.CreateNonZeroNumber();
                number expectedSquareMetres = area.SquareMetres / (number)denominator;

                // act
                var result = area / denominator;

                // assert
                result.SquareMetres.Should().BeApproximately(expectedSquareMetres);
                result.Unit.Should().Be(area.Unit);
            }

            [Fact]
            public void NullArea_ShouldTreatNullAsDefault()
            {
                // arrange
                Area? nullArea = null;
                number denominator = Fixture.CreateNonZeroNumber();
                var expectedResult = default(Area) / denominator;

                // act
                var result = nullArea / denominator;

                // assert
                result.Should().Be(expectedResult);
            }
        }

        public class Operator_DivideByLength : AreaTests
        {
            public Operator_DivideByLength(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void ShouldProduceValidResultInMetres()
            {
                // arrange
                var nominator = CreateAreaInUnitOtherThan(AreaUnit.SquareMetre);
                var denominator = new Length(
                    value: Fixture.CreateNonZeroNumber(),
                    unit: Fixture.Create<LengthUnit>());

                // act
                var result = nominator / denominator;

                // assert
                result.Metres.Should().Be(nominator.SquareMetres / denominator.Metres);
                result.Unit.Should().Be(LengthUnit.Metre);
            }

            [Fact]
            public void NullNominator_ShouldTreatNullAsDefault()
            {
                // arrange
                Area? nominator = null;
                var denominator = new Length(
                    value: Fixture.CreateNonZeroNumber(),
                    unit: Fixture.Create<LengthUnit>());
                var expectedResult = default(Area) / denominator;

                // act
                var result = nominator / denominator;

                // assert
                result.Should().NotBeNull();
                result.Value.Should().Be(expectedResult);
            }

            [Fact]
            public void NullDenominator_ShouldThrow()
            {
                // arrange
                var nominator = CreateAreaInUnitOtherThan(AreaUnit.SquareMetre);
                Length? denominator = null;

                // act
                Func<Length?> result = () => nominator / denominator;

                // assert
                result.Should().Throw<DivideByZeroException>();
            }

            [Fact]
            public void NullNominatorAndDenominator_ShouldThrow()
            {
                // arrange
                Area? nominator = null;
                Length? denominator = null;

                // act
                Func<Length?> result = () => nominator / denominator;

                // assert
                result.Should().Throw<DivideByZeroException>();
            }
        }

        public class Operator_DivideByArea : AreaTests
        {
            public Operator_DivideByArea(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void DivideByZero_ShouldThrow()
            {
                // arrange
                var area = CreateAreaInUnitOtherThan(AreaUnit.SquareMetre);
                var denominator = new Area(Constants.Zero);

                // act
                Func<number> divideByZero = () => area / denominator;

                // assert
                divideByZero.Should().Throw<DivideByZeroException>();
            }

            [Fact]
            public void DivideByNull_ShouldThrow()
            {
                // arrange
                var nominator = CreateAreaInUnitOtherThan(AreaUnit.SquareMetre);
                Area? denominator = null;

                // act
                Func<number> divideByNull = () => nominator / denominator;

                // assert
                divideByNull.Should().Throw<DivideByZeroException>();
            }

            [Fact]
            public void DivideNullByArea_ShouldTreatNullAsDefault()
            {
                // arrange
                Area? nominator = null;
                var denominator = new Area(
                    value: Fixture.CreateNonZeroNumber(),
                    unit: CreateUnitOtherThan(AreaUnit.SquareMetre));

                // act
                number result = nominator / denominator;

                // assert
                result.Should().Be(default(Area) / denominator);
            }

            [Fact]
            public void ShouldProduceValidDoubleResult()
            {
                // arrange
                var nominator = CreateAreaInUnitOtherThan(AreaUnit.SquareMetre);
                var denominator = new Area(
                    value: Fixture.CreateNonZeroNumber(),
                    unit: CreateUnitOtherThan(AreaUnit.SquareMetre, nominator.Unit));

                // act
                number result = nominator / denominator;

                // assert
                result.Should().Be(nominator.SquareMetres / denominator.SquareMetres);
            }
        }
    }
}
