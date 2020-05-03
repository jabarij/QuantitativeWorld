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
        public class MaxAreasBySelector : EnumerableExtensionsTests
        {
            public MaxAreasBySelector(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void NullSource_ShouldThrow()
            {
                // arrange
                IEnumerable<TestObject<Area>> source = null;
                Func<TestObject<Area>, Area> selector = e => e.Property;

                // act
                Action max = () => EnumerableExtensions.Max(source, selector);

                // assert
                max.Should().Throw<ArgumentNullException>()
                    .And.ParamName.Should().Be("source");
            }

            [Fact]
            public void NullSelector_ShouldThrow()
            {
                // arrange
                var source = Enumerable.Empty<TestObject<Area>>();
                Func<TestObject<Area>, Area> selector = null;

                // act
                Action max = () => EnumerableExtensions.Max(source, selector);

                // assert
                max.Should().Throw<ArgumentNullException>()
                    .And.ParamName.Should().Be("selector");
            }

            [Fact]
            public void EmptySource_ShouldThrow()
            {
                // arrange
                var source = Enumerable.Empty<TestObject<Area>>();
                Func<TestObject<Area>, Area> selector = e => e.Property;

                // act
                Action max = () => EnumerableExtensions.Max(source, selector);

                // assert
                max.Should().Throw<InvalidOperationException>();
            }

            [Fact]
            public void ShouldReturnValidResult()
            {
                // arrange
                var source = Fixture.CreateMany<Area>(3).Select(e => new TestObject<Area>(e));
                Func<TestObject<Area>, Area> selector = e => e.Property;

                var expectedResult = source.OrderByDescending(e => e.Property).First().Property;

                // act
                var result = EnumerableExtensions.Max(source, selector);

                // assert
                result.Should().Be(expectedResult);
            }
        }
    }
}
