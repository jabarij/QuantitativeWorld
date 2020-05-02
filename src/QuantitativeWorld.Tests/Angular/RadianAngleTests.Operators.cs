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
                RadianAngle? radianAngle = null;

                // act
                var result = -radianAngle;

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
                var radianAngle1 = default(RadianAngle);
                var radianAngle2 = default(RadianAngle);

                // act
                var result = radianAngle1 + radianAngle2;

                // assert
                result.Should().Be(default(RadianAngle));
            }

            [Fact]
            public void DefaultRadianAngleAndZeroWithOtherUnit_ShouldProduceZeroWithOtherUnit()
            {
                // arrange
                var defaultRadianAngle = default(RadianAngle);
                var zeroRadians = new RadianAngle(0d);

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
                var radianAngle1 = CreateRadianAngle();
                var radianAngle2 = CreateRadianAngle();

                // act
                var result = radianAngle1 + radianAngle2;

                // assert
                result.Radians.Should().Be(radianAngle1.Radians + radianAngle2.Radians);
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
                var radianAngle = CreateRadianAngle();

                // act
                var result1 = radianAngle + nullRadianAngle;
                var result2 = nullRadianAngle + radianAngle;

                // assert
                result1.Should().NotBeNull();
                result1.Value.Radians.Should().Be(radianAngle.Radians);

                result2.Should().NotBeNull();
                result2.Value.Radians.Should().Be(radianAngle.Radians);
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
                var radianAngle1 = default(RadianAngle);
                var radianAngle2 = default(RadianAngle);

                // act
                var result = radianAngle1 - radianAngle2;

                // assert
                result.Should().Be(default(RadianAngle));
            }

            [Fact]
            public void DefaultRadianAngleAndZeroWithOtherUnit_ShouldProduceZeroWithOtherUnit()
            {
                // arrange
                var defaultRadianAngle = default(RadianAngle);
                var zeroRadians = new RadianAngle(0d);

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
                var radianAngle1 = CreateRadianAngle();
                var radianAngle2 = CreateRadianAngle();

                // act
                var result = radianAngle1 - radianAngle2;

                // assert
                result.Radians.Should().Be(radianAngle1.Radians - radianAngle2.Radians);
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
                var radianAngle = CreateRadianAngle();

                // act
                var result1 = radianAngle - nullRadianAngle;
                var result2 = nullRadianAngle - radianAngle;

                // assert
                result1.Should().NotBeNull();
                result1.Value.Radians.Should().Be(radianAngle.Radians);

                result2.Should().NotBeNull();
                result2.Value.Radians.Should().Be(-radianAngle.Radians);
            }
        }

        public class Operator_MultiplyByDouble : RadianAngleTests
        {
            public Operator_MultiplyByDouble(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void ShouldProduceValidResultInSameUnit()
            {
                // arrange
                var radianAngle = CreateRadianAngle();
                double factor = Fixture.Create<double>();

                // act
                var result = radianAngle * factor;

                // assert
                result.Radians.Should().Be(radianAngle.Radians * factor);
            }

            [Fact]
            public void NullRadianAngle_ShouldTreatNullAsDefault()
            {
                // arrange
                RadianAngle? nullRadianAngle = null;
                double factor = Fixture.Create<double>();
                var expectedResult = default(RadianAngle) * factor;

                // act
                var result = nullRadianAngle * factor;

                // assert
                result.Should().NotBeNull();
                result.Value.Should().Be(expectedResult);
            }
        }

        public class Operator_DivideByDouble : RadianAngleTests
        {
            public Operator_DivideByDouble(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void DivideByZero_ShouldThrow()
            {
                // arrange
                var radianAngle = CreateRadianAngle();

                // act
                Func<RadianAngle> divideByZero = () => radianAngle / 0d;

                // assert
                divideByZero.Should().Throw<DivideByZeroException>();
            }

            [Fact]
            public void ShouldProduceValidResult()
            {
                // arrange
                var radianAngle = CreateRadianAngle();
                double denominator = Fixture.CreateNonZeroDouble();

                // act
                var result = radianAngle / denominator;

                // assert
                result.Radians.Should().Be(radianAngle.Radians / denominator);
            }

            [Fact]
            public void NullRadianAngle_ShouldTreatNullAsDefault()
            {
                // arrange
                RadianAngle? nullRadianAngle = null;
                double denominator = Fixture.CreateNonZeroDouble();
                var expectedResult = default(RadianAngle) / denominator;

                // act
                var result = nullRadianAngle * denominator;

                // assert
                result.Should().NotBeNull();
                result.Value.Should().Be(expectedResult);
            }
        }

        public class Operator_ModuloByDouble : RadianAngleTests
        {
            public Operator_ModuloByDouble(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void ModuloByZero_ShouldThrow()
            {
                // arrange
                var radianAngle = CreateRadianAngle();

                // act
                Func<RadianAngle> moduloByZero = () => radianAngle % 0d;

                // assert
                moduloByZero.Should().Throw<DivideByZeroException>();
            }

            [Fact]
            public void ShouldProduceValidResult()
            {
                // arrange
                var radianAngle = CreateRadianAngle();
                double denominator = Fixture.CreateNonZeroDouble();

                // act
                var result = radianAngle % denominator;

                // assert
                result.Radians.Should().Be(radianAngle.Radians % denominator);
            }

            [Fact]
            public void NullRadianAngle_ShouldTreatNullAsDefault()
            {
                // arrange
                RadianAngle? nullRadianAngle = null;
                double denominator = Fixture.CreateNonZeroDouble();
                var expectedResult = default(RadianAngle) % denominator;

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
                var radianAngle = CreateRadianAngle();
                var denominator = new RadianAngle(0d);

                // act
                Func<double> divideByZero = () => radianAngle / denominator;

                // assert
                divideByZero.Should().Throw<DivideByZeroException>();
            }

            [Fact]
            public void DivideByNull_ShouldThrow()
            {
                // arrange
                var radianAngle = CreateRadianAngle();
                RadianAngle? denominator = null;

                // act
                Func<double> divideByZero = () => radianAngle / denominator;

                // assert
                divideByZero.Should().Throw<DivideByZeroException>();
            }

            [Fact]
            public void ShouldProduceValidResult()
            {
                // arrange
                var nominator = CreateRadianAngle();
                var denominator = new RadianAngle(Fixture.CreateNonZeroDouble());

                // act
                double result = nominator / denominator;

                // assert
                result.Should().Be(nominator.Radians / denominator.Radians);
            }
        }
    }
}