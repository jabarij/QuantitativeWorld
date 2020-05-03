using AutoFixture;
using FluentAssertions;
using QuantitativeWorld.TestAbstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace QuantitativeWorld.Tests
{
    partial class EnumerableExtensionsTests
    {
        public class MaxLengths : EnumerableExtensionsTests
        {
            public MaxLengths(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void NullSource_ShouldThrow()
            {
                // arrange
                IEnumerable<Length> source = null;

                // act
                Action max = () => EnumerableExtensions.Max(source);

                // assert
                max.Should().Throw<ArgumentNullException>()
                    .And.ParamName.Should().Be("source");
            }

            [Fact]
            public void EmptySource_ShouldThrow()
            {
                // arrange
                var source = Enumerable.Empty<Length>();

                // act
                Action max = () => EnumerableExtensions.Max(source);

                // assert
                max.Should().Throw<InvalidOperationException>();
            }

            [Fact]
            public void ShouldReturnValidResult()
            {
                // arrange
                var source = Fixture.CreateMany<Length>(3);

                var expectedResult = source.OrderByDescending(e => e).First();

                // act
                var result = EnumerableExtensions.Max(source);

                // assert
                result.Should().Be(expectedResult);
            }
        }
    }
}
