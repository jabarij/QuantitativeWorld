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
        public class MaxNullableRadianAngles : AngularEnumerableExtensionsTests
        {
            public MaxNullableRadianAngles(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void NullSource_ShouldThrow()
            {
                // arrange
                IEnumerable<RadianAngle?> source = null;

                // act
                Action max = () => AngularEnumerableExtensions.Max(source);

                // assert
                max.Should().Throw<ArgumentNullException>()
                    .And.ParamName.Should().Be("source");
            }

            [Fact]
            public void EmptySource_ShouldReturnNull()
            {
                // arrange
                var source = Enumerable.Empty<RadianAngle?>();

                // act
                var result = AngularEnumerableExtensions.Max(source);

                // assert
                result.Should().BeNull();
            }

            [Fact]
            public void ShouldReturnValidResult()
            {
                // arrange
                var source = Fixture.CreateMany<RadianAngle?>(3);
                var expectedResult = source.OrderByDescending(e => e).First();

                // act
                var result = AngularEnumerableExtensions.Max(source);

                // assert
                result.Should().Be(expectedResult);
            }
        }
    }
}
