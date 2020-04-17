using AutoFixture;
using FluentAssertions;
using QuantitativeWorld.Angular;
using QuantitativeWorld.TestAbstractions;
using System;
using Xunit;

namespace QuantitativeWorld.Tests.Angular
{
    partial class RadianAngleTests
    {
        public class Operator_Oposite : RadianAngleTests
        {
            public Operator_Oposite(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void NullRadianAngle_ShouldReturnNull()
            {
                // arrange
                RadianAngle? radianRadianAngle = null;

                // act
                var result = -radianRadianAngle;

                // assert
                result.Should().BeNull();
            }
        }

        public class Operator_Add : RadianAngleTests
        {
            public Operator_Add(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void TwoDefaultRadianAngles_ShouldProduceDefaultRadianAngle()
            {
                // arrange
                var radianRadianAngle1 = default(RadianAngle);
                var radianRadianAngle2 = default(RadianAngle);

                // act
                var result = radianRadianAngle1 + radianRadianAngle2;

                // assert
                result.Should().Be(default(RadianAngle));
            }

            [Fact]
            public void DefaultRadianAngleAndZeroWithOtherUnit_ShouldProduceZeroWithOtherUnit()
            {
                // arrange
                var defaultRadianAngle = default(RadianAngle);
                var zeroRadians = new RadianAngle(0m);

                // act
                var result1 = defaultRadianAngle + zeroRadians;
                var result2 = zeroRadians + defaultRadianAngle;

                // assert
                result1.IsZero().Should().BeTrue();
                result2.IsZero().Should().BeTrue();
            }

            [Fact]
            public void ShouldProduceValidResult()
            {
                // arrange
                var radianRadianAngle1 = CreateRadianAngle();
                var radianRadianAngle2 = CreateRadianAngle();

                // act
                var result = radianRadianAngle1 + radianRadianAngle2;

                // assert
                result.Radians.Should().Be(radianRadianAngle1.Radians + radianRadianAngle2.Radians);
            }

            [Fact]
            public void NullRadianAngles_ShouldReturnNull()
            {
                // arrange
                RadianAngle? nullRadianAngle1 = null;
                RadianAngle? nullRadianAngle2 = null;

                // act
                var result = nullRadianAngle1 + nullRadianAngle2;

                // assert
                result.Should().BeNull();
            }

            [Fact]
            public void NullAndRadianAngle_ShouldTreatNullAsDefault()
            {
                // arrange
                RadianAngle? nullRadianAngle = null;
                var radianRadianAngle = CreateRadianAngle();

                // act
                var result1 = radianRadianAngle + nullRadianAngle;
                var result2 = nullRadianAngle + radianRadianAngle;

                // assert
                result1.Should().NotBeNull();
                result1.Value.Radians.Should().Be(radianRadianAngle.Radians);

                result2.Should().NotBeNull();
                result2.Value.Radians.Should().Be(radianRadianAngle.Radians);
            }
        }

        public class Operator_Subtract : RadianAngleTests
        {
            public Operator_Subtract(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void TwoDefaultRadianAngles_ShouldProduceDefaultRadianAngle()
            {
                // arrange
                var radianRadianAngle1 = default(RadianAngle);
                var radianRadianAngle2 = default(RadianAngle);

                // act
                var result = radianRadianAngle1 - radianRadianAngle2;

                // assert
                result.Should().Be(default(RadianAngle));
            }

            [Fact]
            public void DefaultRadianAngleAndZeroWithOtherUnit_ShouldProduceZeroWithOtherUnit()
            {
                // arrange
                var defaultRadianAngle = default(RadianAngle);
                var zeroRadians = new RadianAngle(0m);

                // act
                var result1 = defaultRadianAngle - zeroRadians;
                var result2 = zeroRadians - defaultRadianAngle;

                // assert
                result1.IsZero().Should().BeTrue();
                result2.IsZero().Should().BeTrue();
            }

            [Fact]
            public void ShouldProduceValidResult()
            {
                // arrange
                var radianRadianAngle1 = CreateRadianAngle();
                var radianRadianAngle2 = CreateRadianAngle();

                // act
                var result = radianRadianAngle1 - radianRadianAngle2;

                // assert
                result.Radians.Should().Be(radianRadianAngle1.Radians - radianRadianAngle2.Radians);
            }

            [Fact]
            public void NullRadianAngles_ShouldReturnNull()
            {
                // arrange
                RadianAngle? nullRadianAngle1 = null;
                RadianAngle? nullRadianAngle2 = null;

                // act
                var result = nullRadianAngle1 - nullRadianAngle2;

                // assert
                result.Should().BeNull();
            }

            [Fact]
            public void NullAndRadianAngle_ShouldTreatNullAsDefault()
            {
                // arrange
                RadianAngle? nullRadianAngle = null;
                var radianRadianAngle = CreateRadianAngle();

                // act
                var result1 = radianRadianAngle - nullRadianAngle;
                var result2 = nullRadianAngle - radianRadianAngle;

                // assert
                result1.Should().NotBeNull();
                result1.Value.Radians.Should().Be(radianRadianAngle.Radians);

                result2.Should().NotBeNull();
                result2.Value.Radians.Should().Be(-radianRadianAngle.Radians);
            }
        }

        public class Operator_MultiplyByDecimal : RadianAngleTests
        {
            public Operator_MultiplyByDecimal(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void ShouldProduceValidResultInSameUnit()
            {
                // arrange
                var radianRadianAngle = CreateRadianAngle();
                decimal factor = Fixture.Create<decimal>();

                // act
                var result = radianRadianAngle * factor;

                // assert
                result.Radians.Should().Be(radianRadianAngle.Radians * factor);
            }

            [Fact]
            public void NullRadianAngle_ShouldTreatNullAsDefault()
            {
                // arrange
                RadianAngle? nullRadianAngle = null;
                decimal factor = Fixture.Create<decimal>();
                var expectedResult = default(RadianAngle) * factor;

                // act
                var result = nullRadianAngle * factor;

                // assert
                result.Should().NotBeNull();
                result.Value.Should().Be(expectedResult);
            }
        }

        public class Operator_DivideByDecimal : RadianAngleTests
        {
            public Operator_DivideByDecimal(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void DivideByZero_ShouldThrow()
            {
                // arrange
                var radianRadianAngle = CreateRadianAngle();

                // act
                Func<RadianAngle> divideByZero = () => radianRadianAngle / decimal.Zero;

                // assert
                divideByZero.Should().Throw<DivideByZeroException>();
            }

            [Fact]
            public void ShouldProduceValidResult()
            {
                // arrange
                var radianRadianAngle = CreateRadianAngle();
                decimal denominator = Fixture.CreateNonZeroDecimal();

                // act
                var result = radianRadianAngle / denominator;

                // assert
                result.Radians.Should().Be(radianRadianAngle.Radians / denominator);
            }

            [Fact]
            public void NullRadianAngle_ShouldTreatNullAsDefault()
            {
                // arrange
                RadianAngle? nullRadianAngle = null;
                decimal denominator = Fixture.CreateNonZeroDecimal();
                var expectedResult = default(RadianAngle) / denominator;

                // act
                var result = nullRadianAngle * denominator;

                // assert
                result.Should().NotBeNull();
                result.Value.Should().Be(expectedResult);
            }
        }

        public class Operator_DivideByRadianAngle : RadianAngleTests
        {
            public Operator_DivideByRadianAngle(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void DivideByZero_ShouldThrow()
            {
                // arrange
                var radianRadianAngle = CreateRadianAngle();
                var denominator = new RadianAngle(decimal.Zero);

                // act
                Func<decimal> divideByZero = () => radianRadianAngle / denominator;

                // assert
                divideByZero.Should().Throw<DivideByZeroException>();
            }

            [Fact]
            public void DivideByNull_ShouldThrow()
            {
                // arrange
                var radianRadianAngle = CreateRadianAngle();
                RadianAngle? denominator = null;

                // act
                Func<decimal> divideByZero = () => radianRadianAngle / denominator;

                // assert
                divideByZero.Should().Throw<DivideByZeroException>();
            }

            [Fact]
            public void ShouldProduceValidDecimalResult()
            {
                // arrange
                var nominator = CreateRadianAngle();
                var denominator = new RadianAngle(Fixture.CreateNonZeroDecimal());

                // act
                decimal result = nominator / denominator;

                // assert
                result.Should().Be(nominator.Radians / denominator.Radians);
            }
        }
    }
}
