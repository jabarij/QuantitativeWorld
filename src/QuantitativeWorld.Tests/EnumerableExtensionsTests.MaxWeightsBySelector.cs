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
        public class MaxWeightsBySelector : EnumerableExtensionsTests
        {
            public MaxWeightsBySelector(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void NullSource_ShouldThrow()
            {
                // arrange
                IEnumerable<TestObject<Weight>> source = null;
                Func<TestObject<Weight>, Weight> selector = e => e.Property;

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
                var source = Enumerable.Empty<TestObject<Weight>>();
                Func<TestObject<Weight>, Weight> selector = null;

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
                var source = Enumerable.Empty<TestObject<Weight>>();
                Func<TestObject<Weight>, Weight> selector = e => e.Property;

                // act
                Action max = () => EnumerableExtensions.Max(source, selector);

                // assert
                max.Should().Throw<InvalidOperationException>();
            }

            [Fact]
            public void ShouldReturnValidResult()
            {
                // arrange
                var source = Fixture.CreateMany<Weight>(3).Select(e => new TestObject<Weight>(e));
                Func<TestObject<Weight>, Weight> selector = e => e.Property;

                var expectedResult = source.OrderByDescending(e => e.Property).First().Property;

                // act
                var result = EnumerableExtensions.Max(source, selector);

                // assert
                result.Should().Be(expectedResult);
            }
        }
    }
}
