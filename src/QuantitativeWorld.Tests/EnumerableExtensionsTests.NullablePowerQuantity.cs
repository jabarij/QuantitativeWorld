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
        public class NullablePowerQuantity : EnumerableExtensionsTests
        {
            public NullablePowerQuantity(TestFixture testFixture) : base(testFixture) { }

            public class Max : NullablePowerQuantity
            {
                public Max(TestFixture testFixture) : base(testFixture) { }

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

            public class MaxBySelector : NullablePowerQuantity
            {
                public MaxBySelector(TestFixture testFixture) : base(testFixture) { }

                [Fact]
                public void NullSource_ShouldThrow()
                {
                    // arrange
                    IEnumerable<TestObject<Power?>> source = null;
                    Func<TestObject<Power?>, Power?> selector = e => e.Property;

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
                    var source = Enumerable.Empty<TestObject<Power?>>();
                    Func<TestObject<Power?>, Power?> selector = null;

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
                    var source = Enumerable.Empty<TestObject<Power?>>();
                    Func<TestObject<Power?>, Power?> selector = e => e.Property;

                    // act
                    var result = EnumerableExtensions.Max(source, selector);

                    // assert
                    result.Should().BeNull();
                }

                [Fact]
                public void ShouldReturnValidResult()
                {
                    // arrange
                    var source = Fixture.CreateMany<Power?>(3).Select(e => new TestObject<Power?>(e));
                    Func<TestObject<Power?>, Power?> selector = e => e.Property;

                    var expectedResult = source.OrderByDescending(e => e.Property.Value).First().Property;

                    // act
                    var result = EnumerableExtensions.Max(source, selector);

                    // assert
                    result.Should().Be(expectedResult.Value);
                }
            }

            public class Min : NullablePowerQuantity
            {
                public Min(TestFixture testFixture) : base(testFixture) { }

                [Fact]
                public void NullSource_ShouldThrow()
                {
                    // arrange
                    IEnumerable<Power?> source = null;

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
                    var source = Enumerable.Empty<Power?>();

                    // act
                    var result = EnumerableExtensions.Min(source);

                    // assert
                    result.Should().BeNull();
                }

                [Fact]
                public void ShouldReturnValidResult()
                {
                    // arrange
                    var source = Fixture.CreateMany<Power?>(3);
                    var expectedResult = source.OrderBy(e => e).First();

                    // act
                    var result = EnumerableExtensions.Min(source);

                    // assert
                    result.Should().Be(expectedResult);
                }
            }

            public class MinBySelector : NullablePowerQuantity
            {
                public MinBySelector(TestFixture testFixture) : base(testFixture) { }

                [Fact]
                public void NullSource_ShouldThrow()
                {
                    // arrange
                    IEnumerable<TestObject<Power?>> source = null;
                    Func<TestObject<Power?>, Power?> selector = e => e.Property;

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
                    var source = Enumerable.Empty<TestObject<Power?>>();
                    Func<TestObject<Power?>, Power?> selector = null;

                    // act
                    Action min = () => EnumerableExtensions.Min(source, selector);

                    // assert
                    min.Should().Throw<ArgumentNullException>()
                        .And.ParamName.Should().Be("selector");
                }

                [Fact]
                public void EmptySource_ShouldReturnNull()
                {
                    // arrange
                    var source = Enumerable.Empty<TestObject<Power?>>();
                    Func<TestObject<Power?>, Power?> selector = e => e.Property;

                    // act
                    var result = EnumerableExtensions.Min(source, selector);

                    // assert
                    result.Should().BeNull();
                }

                [Fact]
                public void ShouldReturnValidResult()
                {
                    // arrange
                    var source = Fixture.CreateMany<Power?>(3).Select(e => new TestObject<Power?>(e));
                    Func<TestObject<Power?>, Power?> selector = e => e.Property;

                    var expectedResult = source.OrderBy(e => e.Property.Value).First().Property;

                    // act
                    var result = EnumerableExtensions.Min(source, selector);

                    // assert
                    result.Should().Be(expectedResult.Value);
                }
            }
        }
    }
}
