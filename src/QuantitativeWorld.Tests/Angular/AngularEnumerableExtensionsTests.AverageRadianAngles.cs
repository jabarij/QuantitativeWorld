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
        public class AverageRadianAngles : AngularEnumerableExtensionsTests
        {
            public AverageRadianAngles(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void NullSource_ShouldThrow()
            {
                // arrange
                IEnumerable<RadianAngle> source = null;

                // act
                Action average = () => AngularEnumerableExtensions.Average(source);

                // assert
                average.Should().Throw<ArgumentNullException>()
                    .And.ParamName.Should().Be("source");
            }

            [Fact]
            public void EmptySource_ShouldThrow()
            {
                // arrange
                var source = Enumerable.Empty<RadianAngle>();

                // act
                Action average = () => AngularEnumerableExtensions.Average(source);

                // assert
                average.Should().Throw<InvalidOperationException>();
            }

            [Fact]
            public void ShouldReturnValidResult()
            {
                // arrange
                var source = Fixture.CreateMany<RadianAngle>(3);

                double expectedResultInRadians = source.Average(e => e.Radians);
                var expectedResult = new RadianAngle(expectedResultInRadians);

                // act
                var result = AngularEnumerableExtensions.Average(source);

                // assert
                result.Radians.Should().Be(expectedResult.Radians);
            }
        }
    }
}
