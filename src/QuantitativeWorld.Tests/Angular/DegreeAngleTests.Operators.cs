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
                var zeroDegrees = new DegreeAngle(0f);

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
                var degreeAngle1 = Fixture.Create<DegreeAngle>();
                var degreeAngle2 = Fixture.Create<DegreeAngle>();

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
                var degreeAngle = Fixture.Create<DegreeAngle>();

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
                var zeroDegrees = new DegreeAngle(0f);

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
                var degreeAngle1 = Fixture.Create<DegreeAngle>();
                var degreeAngle2 = Fixture.Create<DegreeAngle>();

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
                var degreeAngle = Fixture.Create<DegreeAngle>();

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

        public class Operator_MultiplyByFloat : DegreeAngleTests
        {
            public Operator_MultiplyByFloat(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void ShouldProduceValidResultInSameUnit()
            {
                // arrange
                var degreeAngle = Fixture.Create<DegreeAngle>();
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

        public class Operator_DivideByFloat : DegreeAngleTests
        {
            public Operator_DivideByFloat(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void DivideByZero_ShouldThrow()
            {
                // arrange
                var degreeAngle = Fixture.Create<DegreeAngle>();

                // act
                Func<DegreeAngle> divideByZero = () => degreeAngle / 0f;

                // assert
                divideByZero.Should().Throw<DivideByZeroException>();
            }

            [Fact]
            public void ShouldProduceValidResult()
            {
                // arrange
                var degreeAngle = Fixture.Create<DegreeAngle>();
                double denominator = Fixture.CreateNonZeroFloat();

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
                double denominator = Fixture.CreateNonZeroFloat();
                var expectedResult = default(DegreeAngle) / denominator;

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
                var degreeAngle = Fixture.Create<DegreeAngle>();
                var denominator = new DegreeAngle(0f);

                // act
                Func<double> divideByZero = () => degreeAngle / denominator;

                // assert
                divideByZero.Should().Throw<DivideByZeroException>();
            }

            [Fact]
            public void DivideByNull_ShouldThrow()
            {
                // arrange
                var degreeAngle = Fixture.Create<DegreeAngle>();
                DegreeAngle? denominator = null;

                // act
                Func<double> divideByNull = () => degreeAngle / denominator;

                // assert
                divideByNull.Should().Throw<DivideByZeroException>();
            }

            [Fact]
            public void ShouldProduceValidFloatResult()
            {
                // arrange
                var nominator = Fixture.Create<DegreeAngle>();
                var denominator = new DegreeAngle(Fixture.CreateNonZeroFloat());

                // act
                double result = nominator / denominator;

                // assert
                result.Should().Be(nominator.TotalSeconds / denominator.TotalSeconds);
            }
        }
    }
}
