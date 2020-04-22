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
        public class ToDegreeAngle : RadianAngleTests
        {
            public ToDegreeAngle(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void ShouldProduceValidResult()
            {
                // arrange
                var sut = Fixture.Create<RadianAngle>();

                // act
                var result = sut.ToDegreeAngle();

                // assert
                result.TotalDegrees.Should().BeApproximately(sut.Radians * 180d / Math.PI, DoublePrecision);
            }
        }
    }
}
