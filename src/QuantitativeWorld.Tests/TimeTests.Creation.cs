using AutoFixture;
using FluentAssertions;
using System;
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

    partial class TimeTests
    {
        public class Creation : TimeTests
        {
            public Creation(TestFixture testFixture)
                : base(testFixture) { }

            [Theory]
            [MemberData(nameof(GetTestData), typeof(Creation), nameof(GetConstructorTestData))]
            public void ConstructorForTotalSeconds_ShouldCreateValidTime(number totalSeconds, int expectedHours, int expectedMinutes, number expectedSeconds, bool expectedIsNegative)
            {
                // arrange
                // act
                var time = new Time(totalSeconds);

                // assert
                time.TotalSeconds.Should().Be(totalSeconds);
                time.Hours.Should().Be(expectedHours);
                time.Minutes.Should().Be(expectedMinutes);
                time.Seconds.Should().Be(expectedSeconds);
                time.IsNegative.Should().Be(expectedIsNegative);
            }
            private static IEnumerable<ConstructorTestData> GetConstructorTestData()
            {
                yield return new ConstructorTestData(0.5d, 0, 0, 0.5d, false);
                yield return new ConstructorTestData(-0.5d, 0, 0, 0.5d, true);
                yield return new ConstructorTestData(180 * 3600d + 30 * 60d + 30.5d, 180, 30, 30.5d, false);
                yield return new ConstructorTestData(-(180 * 3600d + 30 * 60d + 30.5d), 180, 30, 30.5d, true);
            }
            public class ConstructorTestData : ITestDataProvider
            {
                public ConstructorTestData(decimal totalSeconds, int expectedHours, int expectedMinutes, decimal expectedSeconds, bool expectedIsNegative)
                {
                    TotalSeconds = (number)totalSeconds;
                    ExpectedHours = expectedHours;
                    ExpectedMinutes = expectedMinutes;
                    ExpectedSeconds = (number)expectedSeconds;
                    ExpectedIsNegative = expectedIsNegative;
                }
                public ConstructorTestData(double totalSeconds, int expectedHours, int expectedMinutes, double expectedSeconds, bool expectedIsNegative)
                {
                    TotalSeconds = (number)totalSeconds;
                    ExpectedHours = expectedHours;
                    ExpectedMinutes = expectedMinutes;
                    ExpectedSeconds = (number)expectedSeconds;
                    ExpectedIsNegative = expectedIsNegative;
                }

                public number TotalSeconds { get; set; }
                public int ExpectedHours { get; set; }
                public int ExpectedMinutes { get; set; }
                public number ExpectedSeconds { get; set; }
                public bool ExpectedIsNegative { get; set; }

                public object[] GetTestParameters() =>
                    new[] { (object)TotalSeconds, ExpectedHours, ExpectedMinutes, ExpectedSeconds, ExpectedIsNegative };
            }

            [Fact]
            public void FromTimeSpan_ShouldCreateValidTime()
            {
                // arrange
                var timeSpan = Fixture.Create<TimeSpan>();

                // act
                var time = Time.FromTimeSpan(timeSpan);

                // assert
                time.TotalSeconds.Should().BeApproximately(timeSpan.TotalSeconds);
            }
        }
    }
}
