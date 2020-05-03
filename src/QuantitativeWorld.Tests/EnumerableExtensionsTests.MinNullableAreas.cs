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
        public class MinNullableAreas : EnumerableExtensionsTests
        {
            public MinNullableAreas(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void NullSource_ShouldThrow()
            {
                // arrange
                IEnumerable<Area?> source = null;

                // act
                Action min = () => EnumerableExtensions.Min(source);

                // assert
                min.Should().Throw<ArgumentNullException>()
                    .And.ParamName.Should().Be("source");
            }

            [Fact]
            public void EmptySource_ShouldReturnNull()
            {
                // arrange
                var source = Enumerable.Empty<Area?>();

                // act
                var result = EnumerableExtensions.Min(source);

                // assert
                result.Should().BeNull();
            }

            [Fact]
            public void ShouldReturnValidResult()
            {
                // arrange
                var source = Fixture.CreateMany<Area?>(3);
                var expectedResult = source.OrderBy(e => e).First();

                // act
                var result = EnumerableExtensions.Min(source);

                // assert
                result.Should().Be(expectedResult);
            }
        }
    }
}
