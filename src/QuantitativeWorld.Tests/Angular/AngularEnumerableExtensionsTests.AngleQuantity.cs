using AutoFixture;
using FluentAssertions;
using QuantitativeWorld.TestAbstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using QuantitativeWorld.Angular;

namespace QuantitativeWorld.Tests.Angular
{
    partial class AngularEnumerableExtensionsTests
    {
        public class AngleQuantity : AngularEnumerableExtensionsTests
        {
            public AngleQuantity(TestFixture testFixture) : base(testFixture) { }

            public class Average : AngleQuantity
            {
                public Average(TestFixture testFixture) : base(testFixture) { }

                [Fact]
                public void NullSource_ShouldThrow()
                {
                    // arrange
                    IEnumerable<Angle> source = null;

                    // act
                    Action average = () => AngularEnumerableExtensions.Average(source);

                    // assert
                    average.Should().Throw<ArgumentNullException>()
                        .And.ParamName.Should().Be("source");
                }

                [Fact]
                public void EmptySource_ShouldThrow()
                {
                    // arrange
                    var source = Enumerable.Empty<Angle>();

                    // act
                    Action average = () => AngularEnumerableExtensions.Average(source);

                    // assert
                    average.Should().Throw<InvalidOperationException>();
                }

                [Fact]
                public void ShouldReturnValidResult()
                {
                    // arrange
                    var source = Fixture.CreateMany<Angle>(3);

                    double expectedResultInTurns = source.Average(e => e.Turns);
                    var expectedResult = new Angle(expectedResultInTurns);

                    // act
                    var result = AngularEnumerableExtensions.Average(source);

                    // assert
                    result.Turns.Should().Be(expectedResult.Turns);
                }
            }

            public class AverageBySelector : AngleQuantity
            {
                public AverageBySelector(TestFixture testFixture) : base(testFixture) { }

                [Fact]
                public void NullSource_ShouldThrow()
                {
                    // arrange
                    IEnumerable<TestObject<Angle>> source = null;
                    Func<TestObject<Angle>, Angle> selector = e => e.Property;

                    // act
                    Action average = () => AngularEnumerableExtensions.Average(source, selector);

                    // assert
                    average.Should().Throw<ArgumentNullException>()
                        .And.ParamName.Should().Be("source");
                }

                [Fact]
                public void NullSelector_ShouldThrow()
                {
                    // arrange
                    var source = Enumerable.Empty<TestObject<Angle>>();
                    Func<TestObject<Angle>, Angle> selector = null;

                    // act
                    Action average = () => AngularEnumerableExtensions.Average(source, selector);

                    // assert
                    average.Should().Throw<ArgumentNullException>()
                        .And.ParamName.Should().Be("selector");
                }

                [Fact]
                public void EmptySource_ShouldThrow()
                {
                    // arrange
                    var source = Enumerable.Empty<TestObject<Angle>>();
                    Func<TestObject<Angle>, Angle> selector = e => e.Property;

                    // act
                    Action average = () => AngularEnumerableExtensions.Average(source, selector);

                    // assert
                    average.Should().Throw<InvalidOperationException>();
                }

                [Fact]
                public void ShouldReturnValidResult()
                {
                    // arrange
                    var source = Fixture.CreateMany<Angle>(3).Select(e => new TestObject<Angle>(e));
                    Func<TestObject<Angle>, Angle> selector = e => e.Property;

                    double expectedResultInTurns = source.Average(e => e.Property.Turns);
                    var expectedResult = new Angle(expectedResultInTurns);

                    // act
                    var result = AngularEnumerableExtensions.Average(source, selector);

                    // assert
                    result.Turns.Should().Be(expectedResult.Turns);
                }
            }

            public class Max : AngleQuantity
            {
                public Max(TestFixture testFixture) : base(testFixture) { }

                [Fact]
                public void NullSource_ShouldThrow()
                {
                    // arrange
                    IEnumerable<Angle> source = null;

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
                    var source = Enumerable.Empty<Angle>();

                    // act
                    Action max = () => EnumerableExtensions.Max(source);

                    // assert
                    max.Should().Throw<InvalidOperationException>();
                }

                [Fact]
                public void ShouldReturnValidResult()
                {
                    // arrange
                    var source = Fixture.CreateMany<Angle>(3);

                    var expectedResult = source.OrderByDescending(e => e).First();

                    // act
                    var result = EnumerableExtensions.Max(source);

                    // assert
                    result.Should().Be(expectedResult);
                }
            }

            public class MaxBySelector : AngleQuantity
            {
                public MaxBySelector(TestFixture testFixture) : base(testFixture) { }

                [Fact]
                public void NullSource_ShouldThrow()
                {
                    // arrange
                    IEnumerable<TestObject<Angle>> source = null;
                    Func<TestObject<Angle>, Angle> selector = e => e.Property;

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
                    var source = Enumerable.Empty<TestObject<Angle>>();
                    Func<TestObject<Angle>, Angle> selector = null;

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
                    var source = Enumerable.Empty<TestObject<Angle>>();
                    Func<TestObject<Angle>, Angle> selector = e => e.Property;

                    // act
                    Action max = () => EnumerableExtensions.Max(source, selector);

                    // assert
                    max.Should().Throw<InvalidOperationException>();
                }

                [Fact]
                public void ShouldReturnValidResult()
                {
                    // arrange
                    var source = Fixture.CreateMany<Angle>(3).Select(e => new TestObject<Angle>(e));
                    Func<TestObject<Angle>, Angle> selector = e => e.Property;

                    var expectedResult = source.OrderByDescending(e => e.Property).First().Property;

                    // act
                    var result = EnumerableExtensions.Max(source, selector);

                    // assert
                    result.Should().Be(expectedResult);
                }
            }

            public class Min : AngleQuantity
            {
                public Min(TestFixture testFixture) : base(testFixture) { }

                [Fact]
                public void NullSource_ShouldThrow()
                {
                    // arrange
                    IEnumerable<Angle> source = null;

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
                    var source = Enumerable.Empty<Angle>();

                    // act
                    Action min = () => EnumerableExtensions.Min(source);

                    // assert
                    min.Should().Throw<InvalidOperationException>();
                }

                [Fact]
                public void ShouldReturnValidResult()
                {
                    // arrange
                    var source = Fixture.CreateMany<Angle>(3);

                    var expectedResult = source.OrderBy(e => e).First();

                    // act
                    var result = EnumerableExtensions.Min(source);

                    // assert
                    result.Should().Be(expectedResult);
                }
            }

            public class MinBySelector : AngleQuantity
            {
                public MinBySelector(TestFixture testFixture) : base(testFixture) { }

                [Fact]
                public void NullSource_ShouldThrow()
                {
                    // arrange
                    IEnumerable<TestObject<Angle>> source = null;
                    Func<TestObject<Angle>, Angle> selector = e => e.Property;

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
                    var source = Enumerable.Empty<TestObject<Angle>>();
                    Func<TestObject<Angle>, Angle> selector = null;

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
                    var source = Enumerable.Empty<TestObject<Angle>>();
                    Func<TestObject<Angle>, Angle> selector = e => e.Property;

                    // act
                    Action min = () => EnumerableExtensions.Min(source, selector);

                    // assert
                    min.Should().Throw<InvalidOperationException>();
                }

                [Fact]
                public void ShouldReturnValidResult()
                {
                    // arrange
                    var source = Fixture.CreateMany<Angle>(3).Select(e => new TestObject<Angle>(e));
                    Func<TestObject<Angle>, Angle> selector = e => e.Property;

                    var expectedResult = source.OrderBy(e => e.Property).First().Property;

                    // act
                    var result = EnumerableExtensions.Min(source, selector);

                    // assert
                    result.Should().Be(expectedResult);
                }
            }

            public class Sum : AngleQuantity
            {
                public Sum(TestFixture testFixture) : base(testFixture) { }

                [Fact]
                public void NullSource_ShouldThrow()
                {
                    // arrange
                    IEnumerable<Angle> angles = null;

                    // act
                    Action sum = () => AngularEnumerableExtensions.Sum(angles);

                    // assert
                    sum.Should().Throw<ArgumentNullException>()
                        .And.ParamName.Should().Be("source");
                }

                [Fact]
                public void EmptySource_ShouldReturnDefaultAngle()
                {
                    // arrange
                    var angles = Enumerable.Empty<Angle>();

                    // act
                    var result = AngularEnumerableExtensions.Sum(angles);

                    // assert
                    result.Should().Be(default(Angle));
                }

                [Fact]
                public void ShouldReturnValidResult()
                {
                    // arrange
                    var angles = Fixture.CreateMany<Angle>(3);
                    double expectedResultInTurns = angles.Sum(e => e.Turns);
                    var expectedResult = new Angle(expectedResultInTurns);

                    // act
                    var result = AngularEnumerableExtensions.Sum(angles);

                    // assert
                    result.Turns.Should().Be(expectedResult.Turns);
                }
            }

            public class SumBySelector : AngleQuantity
            {
                public SumBySelector(TestFixture testFixture) : base(testFixture) { }

                [Fact]
                public void NullSource_ShouldThrow()
                {
                    // arrange
                    IEnumerable<TestObject<Angle>> objects = null;
                    Func<TestObject<Angle>, Angle> selector = e => e.Property;

                    // act
                    Action sum = () => AngularEnumerableExtensions.Sum(objects, selector);

                    // assert
                    sum.Should().Throw<ArgumentNullException>()
                        .And.ParamName.Should().Be("source");
                }

                [Fact]
                public void NullSelector_ShouldThrow()
                {
                    // arrange
                    var objects = Enumerable.Empty<TestObject<Angle>>();
                    Func<TestObject<Angle>, Angle> selector = null;

                    // act
                    Action sum = () => AngularEnumerableExtensions.Sum(objects, selector);

                    // assert
                    sum.Should().Throw<ArgumentNullException>()
                        .And.ParamName.Should().Be("selector");
                }

                [Fact]
                public void EmptySource_ShouldReturnDefaultAngle()
                {
                    // arrange
                    var objects = Enumerable.Empty<TestObject<Angle>>();

                    // act
                    var result = AngularEnumerableExtensions.Sum(objects, e => e.Property);

                    // assert
                    result.Should().Be(default(Angle));
                }

                [Fact]
                public void ShouldReturnValidResult()
                {
                    // arrange
                    var objects = Fixture.CreateMany<Angle>(3).Select(e => new TestObject<Angle>(e));
                    double expectedResultInMetres = objects.Sum(e => e.Property.Turns);
                    var expectedResult = new Angle(expectedResultInMetres);

                    // act
                    var result = AngularEnumerableExtensions.Sum(objects, e => e.Property);

                    // assert
                    result.Turns.Should().Be(expectedResult.Turns);
                }
            }
        }
    }
}
