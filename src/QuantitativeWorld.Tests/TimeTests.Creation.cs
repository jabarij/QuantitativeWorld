using AutoFixture;
using FluentAssertions;
using QuantitativeWorld.TestAbstractions;
using System;
using Xunit;

namespace QuantitativeWorld.Tests.Angular
{
    partial class TimeTests
    {
        public class Creation : TimeTests
        {
            public Creation(TestFixture testFixture)
                : base(testFixture) { }

            [Theory]
            [InlineData(0.5d, 0, 0, 0.5d, false)]
            [InlineData(-0.5d, 0, 0, 0.5d, true)]
            [InlineData(180 * 3600d + 30 * 60d + 30.5d, 180, 30, 30.5d, false)]
            [InlineData(-(180 * 3600d + 30 * 60d + 30.5d), 180, 30, 30.5d, true)]
            public void ConstructorForTotalSeconds_ShouldCreateValidTime(float totalSeconds, int expectedHours, int expectedMinutes, double expectedSeconds, bool expectedIsNegative)
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

            [Fact]
            public void FromTimeSpan_ShouldCreateValidTime()
            {
                // arrange
                var timeSpan = Fixture.Create<TimeSpan>();

                // act
                var time = Time.FromTimeSpan(timeSpan);

                // assert
                time.TotalSeconds.Should().BeApproximately(timeSpan.TotalSeconds, DoublePrecision);
            }
        }
    }
}
