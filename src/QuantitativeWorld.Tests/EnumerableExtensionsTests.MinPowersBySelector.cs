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
        public class MinPowersBySelector : EnumerableExtensionsTests
        {
            public MinPowersBySelector(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void NullSource_ShouldThrow()
            {
                // arrange
                IEnumerable<TestObject<Power>> source = null;
                Func<TestObject<Power>, Power> selector = e => e.Property;

                // act
                Action min = () => EnumerableExtensions.Min(source, selector);

                // assert
                min.Should().Throw<ArgumentNullException>()
                    .And.ParamName.Should().Be("source");
            }

            [Fact]
            public void NullSelector_ShouldThrow()
            {
                // arrange
                var source = Enumerable.Empty<TestObject<Power>>();
                Func<TestObject<Power>, Power> selector = null;

                // act
                Action min = () => EnumerableExtensions.Min(source, selector);

                // assert
                min.Should().Throw<ArgumentNullException>()
                    .And.ParamName.Should().Be("selector");
            }

            [Fact]
            public void EmptySource_ShouldThrow()
            {
                // arrange
                var source = Enumerable.Empty<TestObject<Power>>();
                Func<TestObject<Power>, Power> selector = e => e.Property;

                // act
                Action min = () => EnumerableExtensions.Min(source, selector);

                // assert
                min.Should().Throw<InvalidOperationException>();
            }

            [Fact]
            public void ShouldReturnValidResult()
            {
                // arrange
                var source = Fixture.CreateMany<Power>(3).Select(e => new TestObject<Power>(e));
                Func<TestObject<Power>, Power> selector = e => e.Property;

                var expectedResult = source.OrderBy(e => e.Property).First().Property;

                // act
                var result = EnumerableExtensions.Min(source, selector);

                // assert
                result.Should().Be(expectedResult);
            }
        }
    }
}
