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
        public class MaxNullablePowers : EnumerableExtensionsTests
        {
            public MaxNullablePowers(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void NullSource_ShouldThrow()
            {
                // arrange
                IEnumerable<Power?> source = null;

                // act
                Action max = () => EnumerableExtensions.Max(source);

                // assert
                max.Should().Throw<ArgumentNullException>()
                    .And.ParamName.Should().Be("source");
            }

            [Fact]
            public void EmptySource_ShouldReturnNull()
            {
                // arrange
                var source = Enumerable.Empty<Power?>();

                // act
                var result = EnumerableExtensions.Max(source);

                // assert
                result.Should().BeNull();
            }

            [Fact]
            public void ShouldReturnValidResult()
            {
                // arrange
                var source = Fixture.CreateMany<Power?>(3);
                var expectedResult = source.OrderByDescending(e => e).First();

                // act
                var result = EnumerableExtensions.Max(source);

                // assert
                result.Should().Be(expectedResult);
            }
        }
    }
}
