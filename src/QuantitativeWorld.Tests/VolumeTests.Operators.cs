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

    partial class VolumeTests
    {
        public class Operator_Oposite : VolumeTests
        {
            public Operator_Oposite(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void ShouldProduceValidResultInSameUnit()
            {
                // arrange
                var volume = CreateVolumeInUnitOtherThan(VolumeUnit.CubicMetre);

                // act
                var result = -volume;

                // assert
                result.CubicMetres.Should().Be(-volume.CubicMetres);
                result.Value.Should().Be(-volume.Value);
                result.Unit.Should().Be(volume.Unit);
            }

            [Fact]
            public void NullVolume_ShouldReturnNull()
            {
                // arrange
                Volume? volume = null;

                // act
                var result = -volume;

                // assert
                result.Should().BeNull();
            }
        }

        public class Operator_Add : VolumeTests
        {
            public Operator_Add(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void TwoDefaultVolumes_ShouldProduceDefaultVolume()
            {
                // arrange
                var volume1 = default(Volume);
                var volume2 = default(Volume);

                // act
                var result = volume1 + volume2;

                // assert
                result.Should().Be(default(Volume));
            }

            [Fact]
            public void DefaultVolumeAndZeroWithOtherUnit_ShouldProduceZeroWithOtherUnit()
            {
                // arrange
                var defaultVolume = default(Volume);
                var zeroCubicKilometres = new Volume(Constants.Zero, VolumeUnit.CubicKilometre);

                // act
                var result1 = defaultVolume + zeroCubicKilometres;
                var result2 = zeroCubicKilometres + defaultVolume;

                // assert
                result1.IsZero().Should().BeTrue();
                result1.Unit.Should().Be(zeroCubicKilometres.Unit);
                result2.IsZero().Should().BeTrue();
                result2.Unit.Should().Be(zeroCubicKilometres.Unit);
            }

            [Fact]
            public void ShouldProduceValidResultInUnitOfLeftOperand()
            {
                // arrange
                var volume1 = CreateVolumeInUnitOtherThan(VolumeUnit.CubicMetre);
                var volume2 = CreateVolumeInUnitOtherThan(VolumeUnit.CubicMetre, volume1.Unit);

                // act
                var result = volume1 + volume2;

                // assert
                result.CubicMetres.Should().Be(volume1.CubicMetres + volume2.CubicMetres);
                result.Unit.Should().Be(volume1.Unit);
            }

            [Fact]
            public void NullVolumes_ShouldReturnNull()
            {
                // arrange
                Volume? nullVolume1 = null;
                Volume? nullVolume2 = null;

                // act
                var result = nullVolume1 + nullVolume2;

                // assert
                result.Should().BeNull();
            }

            [Fact]
            public void NullAndVolume_ShouldTreatNullAsDefault()
            {
                // arrange
                Volume? nullVolume = null;
                var volume = CreateVolumeInUnitOtherThan(VolumeUnit.CubicMetre);

                // act
                var result1 = volume + nullVolume;
                var result2 = nullVolume + volume;

                // assert
                result1.Should().NotBeNull();
                result1.Value.CubicMetres.Should().Be(volume.CubicMetres);
                result1.Value.Unit.Should().Be(volume.Unit);

                result2.Should().NotBeNull();
                result2.Value.CubicMetres.Should().Be(volume.CubicMetres);
                result2.Value.Unit.Should().Be(volume.Unit);
            }
        }

        public class Operator_Subtract : VolumeTests
        {
            public Operator_Subtract(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void TwoDefaultVolumes_ShouldProduceDefaultVolume()
            {
                // arrange
                var volume1 = default(Volume);
                var volume2 = default(Volume);

                // act
                var result = volume1 - volume2;

                // assert
                result.Should().Be(default(Volume));
            }

            [Fact]
            public void DefaultVolumeAndZeroWithOtherUnit_ShouldProduceZeroWithOtherUnit()
            {
                // arrange
                var defaultVolume = default(Volume);
                var zeroCubicKilometres = new Volume(Constants.Zero, VolumeUnit.CubicKilometre);

                // act
                var result1 = defaultVolume - zeroCubicKilometres;
                var result2 = zeroCubicKilometres - defaultVolume;

                // assert
                result1.IsZero().Should().BeTrue();
                result1.Unit.Should().Be(zeroCubicKilometres.Unit);
                result2.IsZero().Should().BeTrue();
                result2.Unit.Should().Be(zeroCubicKilometres.Unit);
            }

            [Fact]
            public void ShouldProduceValidResultInUnitOfLeftOperand()
            {
                // arrange
                var volume1 = CreateVolumeInUnitOtherThan(VolumeUnit.CubicMetre);
                var volume2 = CreateVolumeInUnitOtherThan(VolumeUnit.CubicMetre, volume1.Unit);

                // act
                var result = volume1 - volume2;

                // assert
                result.CubicMetres.Should().Be(volume1.CubicMetres - volume2.CubicMetres);
                result.Unit.Should().Be(volume1.Unit);
            }

            [Fact]
            public void NullVolumes_ShouldReturnNull()
            {
                // arrange
                Volume? nullVolume1 = null;
                Volume? nullVolume2 = null;

                // act
                var result = nullVolume1 - nullVolume2;

                // assert
                result.Should().BeNull();
            }

            [Fact]
            public void NullAndVolume_ShouldTreatNullAsDefault()
            {
                // arrange
                Volume? nullVolume = null;
                var volume = CreateVolumeInUnitOtherThan(VolumeUnit.CubicMetre);

                // act
                var result1 = volume - nullVolume;
                var result2 = nullVolume - volume;

                // assert
                result1.Should().NotBeNull();
                result1.Value.CubicMetres.Should().Be(volume.CubicMetres);
                result1.Value.Unit.Should().Be(volume.Unit);

                result2.Should().NotBeNull();
                result2.Value.CubicMetres.Should().Be(-volume.CubicMetres);
                result2.Value.Unit.Should().Be(volume.Unit);
            }
        }

        public class Operator_MultiplyByDouble : VolumeTests
        {
            public Operator_MultiplyByDouble(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void ShouldProduceValidResultInSameUnit()
            {
                // arrange
                var volume = CreateVolumeInUnitOtherThan(VolumeUnit.CubicMetre);
                number factor = Fixture.Create<number>();

                // act
                var result = volume * factor;

                // assert
                result.CubicMetres.Should().BeApproximately(volume.CubicMetres * factor);
                result.Value.Should().BeApproximately(volume.Value * factor);
                result.Unit.Should().Be(volume.Unit);
            }

            [Fact]
            public void NullVolume_ShouldTreatNullAsDefault()
            {
                // arrange
                Volume? nullVolume = null;
                number factor = Fixture.Create<number>();
                var expectedResult = default(Volume) * factor;

                // act
                var result = nullVolume * factor;

                // assert
                result.Should().NotBeNull();
                result.Value.Should().Be(expectedResult);
            }

#if !DECIMAL
            [Fact]
            public void MultiplyByNaN_ShouldThrow()
            {
                // arrange
                var volume = CreateVolumeInUnitOtherThan(VolumeUnit.CubicMetre);

                // act
                Func<Volume> multiplyByNaN = () => volume * double.NaN;

                // assert
                multiplyByNaN.Should().Throw<ArgumentException>();
            }
#endif
        }

        public class Operator_DivideByNumber : VolumeTests
        {
            public Operator_DivideByNumber(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void DivideByZero_ShouldThrow()
            {
                // arrange
                var volume = CreateVolumeInUnitOtherThan(VolumeUnit.CubicMetre);

                // act
                Func<Volume> divideByZero = () => volume / Constants.Zero;

                // assert
                divideByZero.Should().Throw<DivideByZeroException>();
            }

#if !DECIMAL
            [Fact]
            public void DivideByNaN_ShouldThrow()
            {
                // arrange
                var volume = CreateVolumeInUnitOtherThan(VolumeUnit.CubicMetre);

                // act
                Func<Volume> divideByNaN = () => volume / number.NaN;

                // assert
                divideByNaN.Should().Throw<ArgumentException>();
            }
#endif

            [Fact]
            public void ShouldProduceValidResultInSameUnit()
            {
                // arrange
                var volume = CreateVolumeInUnitOtherThan(VolumeUnit.CubicMetre);
                number denominator = (number)Fixture.CreateNonZeroNumber();

                // act
                var result = volume / denominator;

                // assert
                result.CubicMetres.Should().BeApproximately(volume.CubicMetres / (number)denominator);
                result.Unit.Should().Be(volume.Unit);
            }

            [Fact]
            public void NullVolume_ShouldTreatNullAsDefault()
            {
                // arrange
                Volume? nullVolume = null;
                number denominator = Fixture.CreateNonZeroNumber();
                var expectedResult = default(Volume) / denominator;

                // act
                var result = nullVolume / denominator;

                // assert
                result.Should().Be(expectedResult);
            }
        }

        public class Operator_DivideByLength : VolumeTests
        {
            public Operator_DivideByLength(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void ShouldProduceValidResultInSquareMetres()
            {
                // arrange
                var nominator = CreateVolumeInUnitOtherThan(VolumeUnit.CubicMetre);
                var denominator = new Length(
                    value: Fixture.CreateNonZeroNumber(),
                    unit: Fixture.Create<LengthUnit>());

                // act
                var result = nominator / denominator;

                // assert
                result.SquareMetres.Should().Be(nominator.CubicMetres / denominator.Metres);
                result.Unit.Should().Be(AreaUnit.SquareMetre);
            }

            [Fact]
            public void NullNominator_ShouldTreatNullAsDefault()
            {
                // arrange
                Volume? nominator = null;
                var denominator = new Length(
                    value: Fixture.CreateNonZeroNumber(),
                    unit: Fixture.Create<LengthUnit>());
                var expectedResult = default(Volume) / denominator;

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
                var nominator = CreateVolumeInUnitOtherThan(VolumeUnit.CubicMetre);
                Length? denominator = null;

                // act
                Func<Area?> result = () => nominator / denominator;

                // assert
                result.Should().Throw<DivideByZeroException>();
            }

            [Fact]
            public void NullNominatorAndDenominator_ShouldThrow()
            {
                // arrange
                Volume? nominator = null;
                Length? denominator = null;

                // act
                Func<Area?> result = () => nominator / denominator;

                // assert
                result.Should().Throw<DivideByZeroException>();
            }
        }

        public class Operator_DivideByArea : VolumeTests
        {
            public Operator_DivideByArea(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void ShouldProduceValidResultInMetres()
            {
                // arrange
                var nominator = CreateVolumeInUnitOtherThan(VolumeUnit.CubicMetre);
                var denominator = new Area(
                    value: Fixture.CreateNonZeroNumber(),
                    unit: Fixture.Create<AreaUnit>());

                // act
                var result = nominator / denominator;

                // assert
                result.Metres.Should().Be(nominator.CubicMetres / denominator.SquareMetres);
                result.Unit.Should().Be(LengthUnit.Metre);
            }

            [Fact]
            public void NullNominator_ShouldTreatNullAsDefault()
            {
                // arrange
                Volume? nominator = null;
                var denominator = new Area(
                    value: Fixture.CreateNonZeroNumber(),
                    unit: Fixture.Create<AreaUnit>());
                var expectedResult = default(Volume) / denominator;

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
                var nominator = CreateVolumeInUnitOtherThan(VolumeUnit.CubicMetre);
                Area? denominator = null;

                // act
                Func<Length?> result = () => nominator / denominator;

                // assert
                result.Should().Throw<DivideByZeroException>();
            }

            [Fact]
            public void NullNominatorAndDenominator_ShouldThrow()
            {
                // arrange
                Volume? nominator = null;
                Area? denominator = null;

                // act
                Func<Length?> result = () => nominator / denominator;

                // assert
                result.Should().Throw<DivideByZeroException>();
            }
        }

        public class Operator_DivideByVolume : VolumeTests
        {
            public Operator_DivideByVolume(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void DivideByZero_ShouldThrow()
            {
                // arrange
                var volume = CreateVolumeInUnitOtherThan(VolumeUnit.CubicMetre);
                var denominator = new Volume(Constants.Zero);

                // act
                Func<number> divideByZero = () => volume / denominator;

                // assert
                divideByZero.Should().Throw<DivideByZeroException>();
            }

            [Fact]
            public void DivideByNull_ShouldThrow()
            {
                // arrange
                var nominator = CreateVolumeInUnitOtherThan(VolumeUnit.CubicMetre);
                Volume? denominator = null;

                // act
                Func<number> divideByNull = () => nominator / denominator;

                // assert
                divideByNull.Should().Throw<DivideByZeroException>();
            }

            [Fact]
            public void DivideNullByVolume_ShouldTreatNullAsDefault()
            {
                // arrange
                Volume? nominator = null;
                var denominator = new Volume(
                    value: Fixture.CreateNonZeroNumber(),
                    unit: CreateUnitOtherThan(VolumeUnit.CubicMetre));

                // act
                number result = nominator / denominator;

                // assert
                result.Should().Be(default(Volume) / denominator);
            }

            [Fact]
            public void ShouldProduceValidDoubleResult()
            {
                // arrange
                var nominator = CreateVolumeInUnitOtherThan(VolumeUnit.CubicMetre);
                var denominator = new Volume(
                    value: Fixture.CreateNonZeroNumber(),
                    unit: CreateUnitOtherThan(VolumeUnit.CubicMetre, nominator.Unit));

                // act
                number result = nominator / denominator;

                // assert
                result.Should().Be(nominator.CubicMetres / denominator.CubicMetres);
            }
        }
    }
}
