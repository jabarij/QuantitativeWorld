using AutoFixture;
using FluentAssertions;
using QuantitativeWorld.Angular;
using QuantitativeWorld.TestAbstractions;
using System;
using Xunit;

namespace QuantitativeWorld.Tests.Angular
{
    partial class DegreeAngleTests
    {
        public class Operator_Oposite : DegreeAngleTests
        {
            public Operator_Oposite(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void NullDegreeAngle_ShouldReturnNull()
            {
                // arrange
                DegreeAngle? degreeAngle = null;

                // act
                var result = -degreeAngle;

                // assert
                result.Should().BeNull();
            }
        }

        public class Operator_Add : DegreeAngleTests
        {
            public Operator_Add(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void TwoDefaultDegreeAngles_ShouldProduceDefaultDegreeAngle()
            {
                // arrange
                var degreeAngle1 = default(DegreeAngle);
                var degreeAngle2 = default(DegreeAngle);

                // act
                var result = degreeAngle1 + degreeAngle2;

                // assert
                result.Should().Be(default(DegreeAngle));
            }

            [Fact]
            public void DefaultDegreeAngleAndZeroWithOtherUnit_ShouldProduceZeroWithOtherUnit()
            {
                // arrange
                var defaultDegreeAngle = default(DegreeAngle);
                var zeroDegrees = new DegreeAngle(0d);

                // act
                var result1 = defaultDegreeAngle + zeroDegrees;
                var result2 = zeroDegrees + defaultDegreeAngle;

                // assert
                result1.IsZero().Should().BeTrue();
                result2.IsZero().Should().BeTrue();
            }

            [Fact]
            public void ShouldProduceValidResult()
            {
                // arrange
                var degreeAngle1 = CreateDegreeAngle();
                var degreeAngle2 = CreateDegreeAngle();

                // act
                var result = degreeAngle1 + degreeAngle2;

                // assert
                result.TotalSeconds.Should().Be(degreeAngle1.TotalSeconds + degreeAngle2.TotalSeconds);
            }

            [Fact]
            public void NullDegreeAngles_ShouldReturnNull()
            {
                // arrange
                DegreeAngle? nullDegreeAngle1 = null;
                DegreeAngle? nullDegreeAngle2 = null;

                // act
                var result = nullDegreeAngle1 + nullDegreeAngle2;

                // assert
                result.Should().BeNull();
            }

            [Fact]
            public void NullAndDegreeAngle_ShouldTreatNullAsDefault()
            {
                // arrange
                DegreeAngle? nullDegreeAngle = null;
                var degreeAngle = CreateDegreeAngle();

                // act
                var result1 = degreeAngle + nullDegreeAngle;
                var result2 = nullDegreeAngle + degreeAngle;

                // assert
                result1.Should().NotBeNull();
                result1.Value.TotalSeconds.Should().Be(degreeAngle.TotalSeconds);

                result2.Should().NotBeNull();
                result2.Value.TotalSeconds.Should().Be(degreeAngle.TotalSeconds);
            }
        }

        public class Operator_Subtract : DegreeAngleTests
        {
            public Operator_Subtract(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void TwoDefaultDegreeAngles_ShouldProduceDefaultDegreeAngle()
            {
                // arrange
                var degreeAngle1 = default(DegreeAngle);
                var degreeAngle2 = default(DegreeAngle);

                // act
                var result = degreeAngle1 - degreeAngle2;

                // assert
                result.Should().Be(default(DegreeAngle));
            }

            [Fact]
            public void DefaultDegreeAngleAndZeroWithOtherUnit_ShouldProduceZeroWithOtherUnit()
            {
                // arrange
                var defaultDegreeAngle = default(DegreeAngle);
                var zeroDegrees = new DegreeAngle(0d);

                // act
                var result1 = defaultDegreeAngle - zeroDegrees;
                var result2 = zeroDegrees - defaultDegreeAngle;

                // assert
                result1.IsZero().Should().BeTrue();
                result2.IsZero().Should().BeTrue();
            }

            [Fact]
            public void ShouldProduceValidResult()
            {
                // arrange
                var degreeAngle1 = CreateDegreeAngle();
                var degreeAngle2 = CreateDegreeAngle();

                // act
                var result = degreeAngle1 - degreeAngle2;

                // assert
                result.TotalSeconds.Should().Be(degreeAngle1.TotalSeconds - degreeAngle2.TotalSeconds);
            }

            [Fact]
            public void NullDegreeAngles_ShouldReturnNull()
            {
                // arrange
                DegreeAngle? nullDegreeAngle1 = null;
                DegreeAngle? nullDegreeAngle2 = null;

                // act
                var result = nullDegreeAngle1 - nullDegreeAngle2;

                // assert
                result.Should().BeNull();
            }

            [Fact]
            public void NullAndDegreeAngle_ShouldTreatNullAsDefault()
            {
                // arrange
                DegreeAngle? nullDegreeAngle = null;
                var degreeAngle = CreateDegreeAngle();

                // act
                var result1 = degreeAngle - nullDegreeAngle;
                var result2 = nullDegreeAngle - degreeAngle;

                // assert
                result1.Should().NotBeNull();
                result1.Value.TotalSeconds.Should().Be(degreeAngle.TotalSeconds);

                result2.Should().NotBeNull();
                result2.Value.TotalSeconds.Should().Be(-degreeAngle.TotalSeconds);
            }
        }

        public class Operator_MultiplyByDouble : DegreeAngleTests
        {
            public Operator_MultiplyByDouble(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void ShouldProduceValidResultInSameUnit()
            {
                // arrange
                var degreeAngle = CreateDegreeAngle();
                double factor = Fixture.Create<double>();

                // act
                var result = degreeAngle * factor;

                // assert
                result.TotalSeconds.Should().Be(degreeAngle.TotalSeconds * factor);
            }

            [Fact]
            public void NullDegreeAngle_ShouldTreatNullAsDefault()
            {
                // arrange
                DegreeAngle? nullDegreeAngle = null;
                double factor = Fixture.Create<double>();
                var expectedResult = default(DegreeAngle) * factor;

                // act
                var result = nullDegreeAngle * factor;

                // assert
                result.Should().NotBeNull();
                result.Value.Should().Be(expectedResult);
            }
        }

        public class Operator_DivideByDouble : DegreeAngleTests
        {
            public Operator_DivideByDouble(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void DivideByZero_ShouldThrow()
            {
                // arrange
                var degreeAngle = CreateDegreeAngle();

                // act
                Func<DegreeAngle> divideByZero = () => degreeAngle / 0d;

                // assert
                divideByZero.Should().Throw<DivideByZeroException>();
            }

            [Fact]
            public void ShouldProduceValidResult()
            {
                // arrange
                var degreeAngle = CreateDegreeAngle();
                double denominator = Fixture.CreateNonZeroDouble();

                // act
                var result = degreeAngle / denominator;

                // assert
                result.TotalSeconds.Should().Be(degreeAngle.TotalSeconds / denominator);
            }

            [Fact]
            public void NullDegreeAngle_ShouldTreatNullAsDefault()
            {
                // arrange
                DegreeAngle? nullDegreeAngle = null;
                double denominator = Fixture.CreateNonZeroDouble();
                var expectedResult = default(DegreeAngle) / denominator;

                // act
                var result = nullDegreeAngle * denominator;

                // assert
                result.Should().NotBeNull();
                result.Value.Should().Be(expectedResult);
            }
        }

        public class Operator_ModuloByDouble : DegreeAngleTests
        {
            public Operator_ModuloByDouble(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void ModuloByZero_ShouldThrow()
            {
                // arrange
                var degreeAngle = CreateDegreeAngle();

                // act
                Func<DegreeAngle> moduloByZero = () => degreeAngle % 0d;

                // assert
                moduloByZero.Should().Throw<DivideByZeroException>();
            }

            [Fact]
            public void ShouldProduceValidResult()
            {
                // arrange
                var degreeAngle = CreateDegreeAngle();
                double denominator = Fixture.CreateNonZeroDouble();

                // act
                var result = degreeAngle % denominator;

                // assert
                result.TotalSeconds.Should().Be(degreeAngle.TotalSeconds % denominator);
            }

            [Fact]
            public void NullDegreeAngle_ShouldTreatNullAsDefault()
            {
                // arrange
                DegreeAngle? nullDegreeAngle = null;
                double denominator = Fixture.CreateNonZeroDouble();
                var expectedResult = default(DegreeAngle) % denominator;

                // act
                var result = nullDegreeAngle * denominator;

                // assert
                result.Should().NotBeNull();
                result.Value.Should().Be(expectedResult);
            }
        }

        public class Operator_DivideByDegreeAngle : DegreeAngleTests
        {
            public Operator_DivideByDegreeAngle(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void DivideByZero_ShouldThrow()
            {
                // arrange
                var degreeAngle = CreateDegreeAngle();
                var denominator = new DegreeAngle(0d);

                // act
                Func<double> divideByZero = () => degreeAngle / denominator;

                // assert
                divideByZero.Should().Throw<DivideByZeroException>();
            }

            [Fact]
            public void DivideByNull_ShouldThrow()
            {
                // arrange
                var degreeAngle = CreateDegreeAngle();
                DegreeAngle? denominator = null;

                // act
                Func<double> divideByZero = () => degreeAngle / denominator;

                // assert
                divideByZero.Should().Throw<DivideByZeroException>();
            }

            [Fact]
            public void ShouldProduceValidResult()
            {
                // arrange
                var nominator = CreateDegreeAngle();
                var denominator = new DegreeAngle(Fixture.CreateNonZeroDouble());

                // act
                double result = nominator / denominator;

                // assert
                result.Should().Be(nominator.TotalSeconds / denominator.TotalSeconds);
            }
        }
    }
}
