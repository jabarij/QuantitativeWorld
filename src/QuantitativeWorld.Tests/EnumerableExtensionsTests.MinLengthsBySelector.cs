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
        public class MinLengthsBySelector : EnumerableExtensionsTests
        {
            public MinLengthsBySelector(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void NullSource_ShouldThrow()
            {
                // arrange
                IEnumerable<TestObject<Length>> source = null;
                Func<TestObject<Length>, Length> selector = e => e.Property;

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
                var source = Enumerable.Empty<TestObject<Length>>();
                Func<TestObject<Length>, Length> selector = null;

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
                var source = Enumerable.Empty<TestObject<Length>>();
                Func<TestObject<Length>, Length> selector = e => e.Property;

                // act
                Action min = () => EnumerableExtensions.Min(source, selector);

                // assert
                min.Should().Throw<InvalidOperationException>();
            }

            [Fact]
            public void ShouldReturnValidResult()
            {
                // arrange
                var source = Fixture.CreateMany<Length>(3).Select(e => new TestObject<Length>(e));
                Func<TestObject<Length>, Length> selector = e => e.Property;

                var expectedResult = source.OrderBy(e => e.Property).First().Property;

                // act
                var result = EnumerableExtensions.Min(source, selector);

                // assert
                result.Should().Be(expectedResult);
            }
        }
    }
}
