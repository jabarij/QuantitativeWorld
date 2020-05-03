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
        public class MinAreasBySelector : EnumerableExtensionsTests
        {
            public MinAreasBySelector(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void NullSource_ShouldThrow()
            {
                // arrange
                IEnumerable<TestObject<Area>> source = null;
                Func<TestObject<Area>, Area> selector = e => e.Property;

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
                var source = Enumerable.Empty<TestObject<Area>>();
                Func<TestObject<Area>, Area> selector = null;

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
                var source = Enumerable.Empty<TestObject<Area>>();
                Func<TestObject<Area>, Area> selector = e => e.Property;

                // act
                Action min = () => EnumerableExtensions.Min(source, selector);

                // assert
                min.Should().Throw<InvalidOperationException>();
            }

            [Fact]
            public void ShouldReturnValidResult()
            {
                // arrange
                var source = Fixture.CreateMany<Area>(3).Select(e => new TestObject<Area>(e));
                Func<TestObject<Area>, Area> selector = e => e.Property;

                var expectedResult = source.OrderBy(e => e.Property).First().Property;

                // act
                var result = EnumerableExtensions.Min(source, selector);

                // assert
                result.Should().Be(expectedResult);
            }
        }
    }
}
