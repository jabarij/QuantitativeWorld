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

    partial class TimeTests
    {
        public class Operator_Oposite : TimeTests
        {
            public Operator_Oposite(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void NullTime_ShouldReturnNull()
            {
                // arrange
                Time? time = null;

                // act
                var result = -time;

                // assert
                result.Should().BeNull();
            }
        }

        public class Operator_Add : TimeTests
        {
            public Operator_Add(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void TwoDefaultTimes_ShouldProduceDefaultTime()
            {
                // arrange
                var time1 = default(Time);
                var time2 = default(Time);

                // act
                var result = time1 + time2;

                // assert
                result.Should().Be(default(Time));
            }

            [Fact]
            public void DefaultTimeAndZeroWithOtherUnit_ShouldProduceZeroWithOtherUnit()
            {
                // arrange
                var defaultTime = default(Time);
                var zeroHours = new Time(Constants.Zero);

                // act
                var result1 = defaultTime + zeroHours;
                var result2 = zeroHours + defaultTime;

                // assert
                result1.IsZero().Should().BeTrue();
                result2.IsZero().Should().BeTrue();
            }

            [Fact]
            public void ShouldProduceValidResult()
            {
                // arrange
                var time1 = CreateTime();
                var time2 = CreateTime();

                // act
                var result = time1 + time2;

                // assert
                result.TotalSeconds.Should().Be(time1.TotalSeconds + time2.TotalSeconds);
            }

            [Fact]
            public void NullTimes_ShouldReturnNull()
            {
                // arrange
                Time? nullTime1 = null;
                Time? nullTime2 = null;

                // act
                var result = nullTime1 + nullTime2;

                // assert
                result.Should().BeNull();
            }

            [Fact]
            public void NullAndTime_ShouldTreatNullAsDefault()
            {
                // arrange
                Time? nullTime = null;
                var time = CreateTime();

                // act
                var result1 = time + nullTime;
                var result2 = nullTime + time;

                // assert
                result1.Should().NotBeNull();
                result1.Value.TotalSeconds.Should().Be(time.TotalSeconds);

                result2.Should().NotBeNull();
                result2.Value.TotalSeconds.Should().Be(time.TotalSeconds);
            }
        }

        public class Operator_Subtract : TimeTests
        {
            public Operator_Subtract(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void TwoDefaultTimes_ShouldProduceDefaultTime()
            {
                // arrange
                var time1 = default(Time);
                var time2 = default(Time);

                // act
                var result = time1 - time2;

                // assert
                result.Should().Be(default(Time));
            }

            [Fact]
            public void DefaultTimeAndZeroWithOtherUnit_ShouldProduceZeroWithOtherUnit()
            {
                // arrange
                var defaultTime = default(Time);
                var zeroHours = new Time(Constants.Zero);

                // act
                var result1 = defaultTime - zeroHours;
                var result2 = zeroHours - defaultTime;

                // assert
                result1.IsZero().Should().BeTrue();
                result2.IsZero().Should().BeTrue();
            }

            [Fact]
            public void ShouldProduceValidResult()
            {
                // arrange
                var time1 = CreateTime();
                var time2 = CreateTime();

                // act
                var result = time1 - time2;

                // assert
                result.TotalSeconds.Should().Be(time1.TotalSeconds - time2.TotalSeconds);
            }

            [Fact]
            public void NullTimes_ShouldReturnNull()
            {
                // arrange
                Time? nullTime1 = null;
                Time? nullTime2 = null;

                // act
                var result = nullTime1 - nullTime2;

                // assert
                result.Should().BeNull();
            }

            [Fact]
            public void NullAndTime_ShouldTreatNullAsDefault()
            {
                // arrange
                Time? nullTime = null;
                var time = CreateTime();

                // act
                var result1 = time - nullTime;
                var result2 = nullTime - time;

                // assert
                result1.Should().NotBeNull();
                result1.Value.TotalSeconds.Should().Be(time.TotalSeconds);

                result2.Should().NotBeNull();
                result2.Value.TotalSeconds.Should().Be(-time.TotalSeconds);
            }
        }

        public class Operator_MultiplyByDouble : TimeTests
        {
            public Operator_MultiplyByDouble(TestFixture testFixture)
                : base(testFixture) { }

            [Fact]
            public void ShouldProduceValidResultInSameUnit()
            {
                // arrange
                var time = CreateTime();
                number factor = Fixture.Create<number>();

                // act
                var result = time * factor;

                // assert
                result.TotalSeconds.Should().Be(time.TotalSeconds * factor);
            }

            [Fact]
            public void NullTime_ShouldTreatNullAsDefault()
            {
                // arrange
                Time? nullTime = null;
                number factor = Fixture.Create<number>();
                var expectedResult = default(Time) * factor;

                // act
                var result = nullTime * factor;

                // assert
                result.Should().NotBeNull();
                result.Value.Should().Be(expectedResult);
            }
        }

        public class Operator_DivideByNumber : TimeTests
        {
            public Operator_DivideByNumber(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void DivideByZero_ShouldThrow()
            {
                // arrange
                var time = CreateTime();

                // act
                Func<Time> divideByZero = () => time / Constants.Zero;

                // assert
                divideByZero.Should().Throw<DivideByZeroException>();
            }

            [Fact]
            public void ShouldProduceValidResult()
            {
                // arrange
                var time = CreateTime();
                number denominator = Fixture.CreateNonZeroNumber();

                // act
                var result = time / denominator;

                // assert
                result.TotalSeconds.Should().Be(time.TotalSeconds / denominator);
            }

            [Fact]
            public void NullTime_ShouldTreatNullAsDefault()
            {
                // arrange
                Time? nullTime = null;
                number denominator = Fixture.CreateNonZeroNumber();
                var expectedResult = default(Time) / denominator;

                // act
                var result = nullTime * denominator;

                // assert
                result.Should().NotBeNull();
                result.Value.Should().Be(expectedResult);
            }
        }

        public class Operator_DivideByTime : TimeTests
        {
            public Operator_DivideByTime(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void DivideByZero_ShouldThrow()
            {
                // arrange
                var time = CreateTime();
                var denominator = new Time(Constants.Zero);

                // act
                Func<number> divideByZero = () => time / denominator;

                // assert
                divideByZero.Should().Throw<DivideByZeroException>();
            }

            [Fact]
            public void DivideByNull_ShouldThrow()
            {
                // arrange
                var time = CreateTime();
                Time? denominator = null;

                // act
                Func<number> divideByZero = () => time / denominator;

                // assert
                divideByZero.Should().Throw<DivideByZeroException>();
            }

            [Fact]
            public void ShouldProduceValidResult()
            {
                // arrange
                var nominator = CreateTime();
                var denominator = new Time(Fixture.CreateNonZeroNumber());

                // act
                number result = nominator / denominator;

                // assert
                result.Should().Be(nominator.TotalSeconds / denominator.TotalSeconds);
            }
        }
    }
}
