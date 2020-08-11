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
        public class TimeQuantity : EnumerableExtensionsTests
        {
            public TimeQuantity(TestFixture testFixture) : base(testFixture) { }

            public class Average : TimeQuantity
            {
                public Average(TestFixture testFixture) : base(testFixture) { }

                [Fact]
                public void NullSource_ShouldThrow()
                {
                    // arrange
                    IEnumerable<Time> source = null;

                    // act
                    Action average = () => EnumerableExtensions.Average(source);

                    // assert
                    average.Should().Throw<ArgumentNullException>()
                        .And.ParamName.Should().Be("source");
                }

                [Fact]
                public void EmptySource_ShouldThrow()
                {
                    // arrange
                    var source = Enumerable.Empty<Time>();

                    // act
                    Action average = () => EnumerableExtensions.Average(source);

                    // assert
                    average.Should().Throw<InvalidOperationException>();
                }

                [Fact]
                public void ShouldReturnValidResult()
                {
                    // arrange
                    var source = Fixture.CreateMany<Time>(3);

                    double expectedResultInTotalSeconds = source.Average(e => e.TotalSeconds);
                    var expectedResult = new Time(expectedResultInTotalSeconds);

                    // act
                    var result = EnumerableExtensions.Average(source);

                    // assert
                    result.TotalSeconds.Should().Be(expectedResult.TotalSeconds);
                }
            }

            public class AverageBySelector : TimeQuantity
            {
                public AverageBySelector(TestFixture testFixture) : base(testFixture) { }

                [Fact]
                public void NullSource_ShouldThrow()
                {
                    // arrange
                    IEnumerable<TestObject<Time>> source = null;
                    Func<TestObject<Time>, Time> selector = e => e.Property;

                    // act
                    Action average = () => EnumerableExtensions.Average(source, selector);

                    // assert
                    average.Should().Throw<ArgumentNullException>()
                        .And.ParamName.Should().Be("source");
                }

                [Fact]
                public void NullSelector_ShouldThrow()
                {
                    // arrange
                    var source = Enumerable.Empty<TestObject<Time>>();
                    Func<TestObject<Time>, Time> selector = null;

                    // act
                    Action average = () => EnumerableExtensions.Average(source, selector);

                    // assert
                    average.Should().Throw<ArgumentNullException>()
                        .And.ParamName.Should().Be("selector");
                }

                [Fact]
                public void EmptySource_ShouldThrow()
                {
                    // arrange
                    var source = Enumerable.Empty<TestObject<Time>>();
                    Func<TestObject<Time>, Time> selector = e => e.Property;

                    // act
                    Action average = () => EnumerableExtensions.Average(source, selector);

                    // assert
                    average.Should().Throw<InvalidOperationException>();
                }

                [Fact]
                public void ShouldReturnValidResult()
                {
                    // arrange
                    var source = Fixture.CreateMany<Time>(3).Select(e => new TestObject<Time>(e));
                    Func<TestObject<Time>, Time> selector = e => e.Property;

                    double expectedResultInTotalSeconds = source.Average(e => e.Property.TotalSeconds);
                    var expectedResult = new Time(expectedResultInTotalSeconds);

                    // act
                    var result = EnumerableExtensions.Average(source, selector);

                    // assert
                    result.TotalSeconds.Should().Be(expectedResult.TotalSeconds);
                }
            }

            public class Max : TimeQuantity
            {
                public Max(TestFixture testFixture) : base(testFixture) { }

                [Fact]
                public void NullSource_ShouldThrow()
                {
                    // arrange
                    IEnumerable<Time> source = null;

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
                    var source = Enumerable.Empty<Time>();

                    // act
                    Action max = () => EnumerableExtensions.Max(source);

                    // assert
                    max.Should().Throw<InvalidOperationException>();
                }

                [Fact]
                public void ShouldReturnValidResult()
                {
                    // arrange
                    var source = Fixture.CreateMany<Time>(3);

                    var expectedResult = source.OrderByDescending(e => e).First();

                    // act
                    var result = EnumerableExtensions.Max(source);

                    // assert
                    result.Should().Be(expectedResult);
                }
            }

            public class MaxBySelector : TimeQuantity
            {
                public MaxBySelector(TestFixture testFixture) : base(testFixture) { }

                [Fact]
                public void NullSource_ShouldThrow()
                {
                    // arrange
                    IEnumerable<TestObject<Time>> source = null;
                    Func<TestObject<Time>, Time> selector = e => e.Property;

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
                    var source = Enumerable.Empty<TestObject<Time>>();
                    Func<TestObject<Time>, Time> selector = null;

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
                    var source = Enumerable.Empty<TestObject<Time>>();
                    Func<TestObject<Time>, Time> selector = e => e.Property;

                    // act
                    Action max = () => EnumerableExtensions.Max(source, selector);

                    // assert
                    max.Should().Throw<InvalidOperationException>();
                }

                [Fact]
                public void ShouldReturnValidResult()
                {
                    // arrange
                    var source = Fixture.CreateMany<Time>(3).Select(e => new TestObject<Time>(e));
                    Func<TestObject<Time>, Time> selector = e => e.Property;

                    var expectedResult = source.OrderByDescending(e => e.Property).First().Property;

                    // act
                    var result = EnumerableExtensions.Max(source, selector);

                    // assert
                    result.Should().Be(expectedResult);
                }
            }

            public class Min : TimeQuantity
            {
                public Min(TestFixture testFixture) : base(testFixture) { }

                [Fact]
                public void NullSource_ShouldThrow()
                {
                    // arrange
                    IEnumerable<Time> source = null;

                    // act
                    Action min = () => EnumerableExtensions.Min(source);

                    // assert
                    min.Should().Throw<ArgumentNullException>()
                        .And.ParamName.Should().Be("source");
                }

                [Fact]
                public void EmptySource_ShouldThrow()
                {
                    // arrange
                    var source = Enumerable.Empty<Time>();

                    // act
                    Action min = () => EnumerableExtensions.Min(source);

                    // assert
                    min.Should().Throw<InvalidOperationException>();
                }

                [Fact]
                public void ShouldReturnValidResult()
                {
                    // arrange
                    var source = Fixture.CreateMany<Time>(3);

                    var expectedResult = source.OrderBy(e => e).First();

                    // act
                    var result = EnumerableExtensions.Min(source);

                    // assert
                    result.Should().Be(expectedResult);
                }
            }

            public class MinBySelector : TimeQuantity
            {
                public MinBySelector(TestFixture testFixture) : base(testFixture) { }

                [Fact]
                public void NullSource_ShouldThrow()
                {
                    // arrange
                    IEnumerable<TestObject<Time>> source = null;
                    Func<TestObject<Time>, Time> selector = e => e.Property;

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
                    var source = Enumerable.Empty<TestObject<Time>>();
                    Func<TestObject<Time>, Time> selector = null;

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
                    var source = Enumerable.Empty<TestObject<Time>>();
                    Func<TestObject<Time>, Time> selector = e => e.Property;

                    // act
                    Action min = () => EnumerableExtensions.Min(source, selector);

                    // assert
                    min.Should().Throw<InvalidOperationException>();
                }

                [Fact]
                public void ShouldReturnValidResult()
                {
                    // arrange
                    var source = Fixture.CreateMany<Time>(3).Select(e => new TestObject<Time>(e));
                    Func<TestObject<Time>, Time> selector = e => e.Property;

                    var expectedResult = source.OrderBy(e => e.Property).First().Property;

                    // act
                    var result = EnumerableExtensions.Min(source, selector);

                    // assert
                    result.Should().Be(expectedResult);
                }
            }

            public class Sum : TimeQuantity
            {
                public Sum(TestFixture testFixture) : base(testFixture) { }

                [Fact]
                public void NullSource_ShouldThrow()
                {
                    // arrange
                    IEnumerable<Time> areas = null;

                    // act
                    Action sum = () => EnumerableExtensions.Sum(areas);

                    // assert
                    sum.Should().Throw<ArgumentNullException>()
                        .And.ParamName.Should().Be("source");
                }

                [Fact]
                public void EmptySource_ShouldReturnDefaultTime()
                {
                    // arrange
                    var areas = Enumerable.Empty<Time>();

                    // act
                    var result = EnumerableExtensions.Sum(areas);

                    // assert
                    result.Should().Be(default(Time));
                }

                [Fact]
                public void ShouldReturnValidResult()
                {
                    // arrange
                    var areas = Fixture.CreateMany<Time>(3);
                    double expectedResultInTotalSeconds = areas.Sum(e => e.TotalSeconds);
                    var expectedResult = new Time(expectedResultInTotalSeconds);

                    // act
                    var result = EnumerableExtensions.Sum(areas);

                    // assert
                    result.TotalSeconds.Should().Be(expectedResult.TotalSeconds);
                }
            }

            public class SumBySelector : TimeQuantity
            {
                public SumBySelector(TestFixture testFixture) : base(testFixture) { }

                [Fact]
                public void NullSource_ShouldThrow()
                {
                    // arrange
                    IEnumerable<TestObject<Time>> objects = null;
                    Func<TestObject<Time>, Time> selector = e => e.Property;

                    // act
                    Action sum = () => EnumerableExtensions.Sum(objects, selector);

                    // assert
                    sum.Should().Throw<ArgumentNullException>()
                        .And.ParamName.Should().Be("source");
                }

                [Fact]
                public void NullSelector_ShouldThrow()
                {
                    // arrange
                    var objects = Enumerable.Empty<TestObject<Time>>();
                    Func<TestObject<Time>, Time> selector = null;

                    // act
                    Action sum = () => EnumerableExtensions.Sum(objects, selector);

                    // assert
                    sum.Should().Throw<ArgumentNullException>()
                        .And.ParamName.Should().Be("selector");
                }

                [Fact]
                public void EmptySource_ShouldReturnDefaultTime()
                {
                    // arrange
                    var objects = Enumerable.Empty<TestObject<Time>>();

                    // act
                    var result = EnumerableExtensions.Sum(objects, e => e.Property);

                    // assert
                    result.Should().Be(default(Time));
                }

                [Fact]
                public void ShouldReturnValidResult()
                {
                    // arrange
                    var objects = Fixture.CreateMany<Time>(3).Select(e => new TestObject<Time>(e));
                    double expectedResultInMetres = objects.Sum(e => e.Property.TotalSeconds);
                    var expectedResult = new Time(expectedResultInMetres);

                    // act
                    var result = EnumerableExtensions.Sum(objects, e => e.Property);

                    // assert
                    result.TotalSeconds.Should().Be(expectedResult.TotalSeconds);
                }
            }
        }
    }
}
