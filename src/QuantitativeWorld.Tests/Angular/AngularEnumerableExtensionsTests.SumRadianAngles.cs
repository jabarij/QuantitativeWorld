using AutoFixture;
using FluentAssertions;
using QuantitativeWorld.Angular;
using QuantitativeWorld.TestAbstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace QuantitativeWorld.Tests.Angular
{
    partial class AngularEnumerableExtensionsTests
    {
        public class SumRadianAngles : AngularEnumerableExtensionsTests
        {
            public SumRadianAngles(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void NullSource_ShouldThrow()
            {
                // arrange
                IEnumerable<RadianAngle> radianAngles = null;

                // act
                Action sum = () => AngularEnumerableExtensions.Sum(radianAngles);

                // assert
                sum.Should().Throw<ArgumentNullException>()
                    .And.ParamName.Should().Be("source");
            }

            [Fact]
            public void EmptySource_ShouldReturnDefaultRadianAngle()
            {
                // arrange
                var radianAngles = Enumerable.Empty<RadianAngle>();

                // act
                var result = AngularEnumerableExtensions.Sum(radianAngles);

                // assert
                result.Should().Be(default(RadianAngle));
            }

            [Fact]
            public void ShouldReturnValidResult()
            {
                // arrange
                var radianAngles = Fixture.CreateMany<RadianAngle>(3);
                double expectedResultInRadians = radianAngles.Sum(e => e.Radians);
                var expectedResult = new RadianAngle(expectedResultInRadians);

                // act
                var result = AngularEnumerableExtensions.Sum(radianAngles);

                // assert
                result.Radians.Should().Be(expectedResult.Radians);
            }
        }
    }
}
