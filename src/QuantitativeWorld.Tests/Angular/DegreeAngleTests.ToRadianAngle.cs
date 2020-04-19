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
        public class ToRadianAngle : DegreeAngleTests
        {
            public ToRadianAngle(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void ShouldProduceValidResult()
            {
                // arrange
                var sut = Fixture.Create<DegreeAngle>();

                // act
                var result = sut.ToRadianAngle();

                // assert
                result.Radians.Should().BeApproximately(sut.TotalDegrees * System.Math.PI / 180d, DoublePrecision);
            }
        }
    }
}
