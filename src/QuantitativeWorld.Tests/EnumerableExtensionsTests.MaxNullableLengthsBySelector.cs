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
        public class MaxNullableLengthsBySelector : EnumerableExtensionsTests
        {
            public MaxNullableLengthsBySelector(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void NullSource_ShouldThrow()
            {
                // arrange
                IEnumerable<TestObject<Length?>> source = null;
                Func<TestObject<Length?>, Length?> selector = e => e.Property;

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
                var source = Enumerable.Empty<TestObject<Length?>>();
                Func<TestObject<Length?>, Length?> selector = null;

                // act
                Action max = () => EnumerableExtensions.Max(source, selector);

                // assert
                max.Should().Throw<ArgumentNullException>()
                    .And.ParamName.Should().Be("selector");
            }

            [Fact]
            public void EmptySource_ShouldReturnNull()
            {
                // arrange
                var source = Enumerable.Empty<TestObject<Length?>>();
                Func<TestObject<Length?>, Length?> selector = e => e.Property;

                // act
                var result = EnumerableExtensions.Max(source, selector);

                // assert
                result.Should().BeNull();
            }

            [Fact]
            public void ShouldReturnValidResult()
            {
                // arrange
                var source = Fixture.CreateMany<Length?>(3).Select(e => new TestObject<Length?>(e));
                Func<TestObject<Length?>, Length?> selector = e => e.Property;

                var expectedResult = source.OrderByDescending(e => e.Property.Value).First().Property;

                // act
                var result = EnumerableExtensions.Max(source, selector);

                // assert
                result.Should().Be(expectedResult.Value);
            }
        }
    }
}
