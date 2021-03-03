using AutoFixture;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

#if DECIMAL
namespace DecimalQuantitativeWorld.Tests
{
    using DecimalQuantitativeWorld.TestAbstractions;
    using number = System.Decimal;
#else
namespace QuantitativeWorld.Tests
{
    using QuantitativeWorld.TestAbstractions;
    using Constants = DoubleConstants;
    using number = System.Double;
#endif

    partial class EnumerableExtensionsTests
    {
        public class AreaQuantity : EnumerableExtensionsTests
        {
            public AreaQuantity(TestFixture testFixture) : base(testFixture) { }

            public class Average : AreaQuantity
            {
                public Average(TestFixture testFixture) : base(testFixture) { }

                [Fact]
                public void NullSource_ShouldThrow()
                {
                    // arrange
                    IEnumerable<Area> source = null;

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
                    var source = Enumerable.Empty<Area>();

                    // act
                    Action average = () => EnumerableExtensions.Average(source);

                    // assert
                    average.Should().Throw<InvalidOperationException>();
                }

                [Fact]
                public void ShouldReturnValidResult()
                {
                    // arrange
                    var source = Fixture.CreateMany<Area>(3);

                    number expectedResultInSquareMetres = source.Average(e => e.SquareMetres);
                    var expectedResultUnit = source.First().Unit;
                    var expectedResult = new Area(expectedResultInSquareMetres).Convert(expectedResultUnit);

                    // act
                    var result = EnumerableExtensions.Average(source);

                    // assert
                    result.SquareMetres.Should().BeApproximately(expectedResult.SquareMetres);
                    result.Unit.Should().Be(expectedResult.Unit);
                    result.Value.Should().BeApproximately(expectedResult.Value);
                }
            }

            public class AverageBySelector : AreaQuantity
            {
                public AverageBySelector(TestFixture testFixture) : base(testFixture) { }

                [Fact]
                public void NullSource_ShouldThrow()
                {
                    // arrange
                    IEnumerable<TestObject<Area>> source = null;
                    Func<TestObject<Area>, Area> selector = e => e.Property;

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
                    var source = Enumerable.Empty<TestObject<Area>>();
                    Func<TestObject<Area>, Area> selector = null;

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
                    var source = Enumerable.Empty<TestObject<Area>>();
                    Func<TestObject<Area>, Area> selector = e => e.Property;

                    // act
                    Action average = () => EnumerableExtensions.Average(source, selector);

                    // assert
                    average.Should().Throw<InvalidOperationException>();
                }

                [Fact]
                public void ShouldReturnValidResult()
                {
                    // arrange
                    var source = Fixture.CreateMany<Area>(3).Select(e => new TestObject<Area>(e));
                    Func<TestObject<Area>, Area> selector = e => e.Property;

                    number expectedResultInSquareMetres = source.Average(e => e.Property.SquareMetres);
                    var expectedResultUnit = source.First().Property.Unit;
                    var expectedResult = new Area(expectedResultInSquareMetres).Convert(expectedResultUnit);

                    // act
                    var result = EnumerableExtensions.Average(source, selector);

                    // assert
                    result.SquareMetres.Should().BeApproximately(expectedResult.SquareMetres);
                    result.Unit.Should().Be(expectedResult.Unit);
                    result.Value.Should().BeApproximately(expectedResult.Value);
                }
            }

            public class Max : AreaQuantity
            {
                public Max(TestFixture testFixture) : base(testFixture) { }

                [Fact]
                public void NullSource_ShouldThrow()
                {
                    // arrange
                    IEnumerable<Area> source = null;

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
                    var source = Enumerable.Empty<Area>();

                    // act
                    Action max = () => EnumerableExtensions.Max(source);

                    // assert
                    max.Should().Throw<InvalidOperationException>();
                }

                [Fact]
                public void ShouldReturnValidResult()
                {
                    // arrange
                    var source = Fixture.CreateMany<Area>(3);

                    var expectedResult = source.OrderByDescending(e => e).First();

                    // act
                    var result = EnumerableExtensions.Max(source);

                    // assert
                    result.Should().Be(expectedResult);
                }
            }

            public class MaxBySelector : AreaQuantity
            {
                public MaxBySelector(TestFixture testFixture) : base(testFixture) { }

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

            public class Min : AreaQuantity
            {
                public Min(TestFixture testFixture) : base(testFixture) { }

                [Fact]
                public void NullSource_ShouldThrow()
                {
                    // arrange
                    IEnumerable<Area> source = null;

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
                    var source = Enumerable.Empty<Area>();

                    // act
                    Action min = () => EnumerableExtensions.Min(source);

                    // assert
                    min.Should().Throw<InvalidOperationException>();
                }

                [Fact]
                public void ShouldReturnValidResult()
                {
                    // arrange
                    var source = Fixture.CreateMany<Area>(3);

                    var expectedResult = source.OrderBy(e => e).First();

                    // act
                    var result = EnumerableExtensions.Min(source);

                    // assert
                    result.Should().Be(expectedResult);
                }
            }

            public class MinBySelector : AreaQuantity
            {
                public MinBySelector(TestFixture testFixture) : base(testFixture) { }

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

            public class Sum : AreaQuantity
            {
                public Sum(TestFixture testFixture) : base(testFixture) { }

                [Fact]
                public void NullSource_ShouldThrow()
                {
                    // arrange
                    IEnumerable<Area> areas = null;

                    // act
                    Action sum = () => EnumerableExtensions.Sum(areas);

                    // assert
                    sum.Should().Throw<ArgumentNullException>()
                        .And.ParamName.Should().Be("source");
                }

                [Fact]
                public void EmptySource_ShouldReturnDefaultArea()
                {
                    // arrange
                    var areas = Enumerable.Empty<Area>();

                    // act
                    var result = EnumerableExtensions.Sum(areas);

                    // assert
                    result.Should().Be(default(Area));
                }

                [Fact]
                public void ShouldReturnValidResult()
                {
                    // arrange
                    var areas = Fixture.CreateMany<Area>(3);
                    number expectedResultInSquareMetres = areas.Sum(e => e.SquareMetres);
                    var expectedResultUnit = areas.First().Unit;
                    var expectedResult = new Area(expectedResultInSquareMetres).Convert(expectedResultUnit);

                    // act
                    var result = EnumerableExtensions.Sum(areas);

                    // assert
                    result.SquareMetres.Should().BeApproximately(expectedResult.SquareMetres);
                    result.Unit.Should().Be(expectedResult.Unit);
                    result.Value.Should().BeApproximately(expectedResult.Value);
                }
            }

            public class SumBySelector : AreaQuantity
            {
                public SumBySelector(TestFixture testFixture) : base(testFixture) { }

                [Fact]
                public void NullSource_ShouldThrow()
                {
                    // arrange
                    IEnumerable<TestObject<Area>> objects = null;
                    Func<TestObject<Area>, Area> selector = e => e.Property;

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
                    var objects = Enumerable.Empty<TestObject<Area>>();
                    Func<TestObject<Area>, Area> selector = null;

                    // act
                    Action sum = () => EnumerableExtensions.Sum(objects, selector);

                    // assert
                    sum.Should().Throw<ArgumentNullException>()
                        .And.ParamName.Should().Be("selector");
                }

                [Fact]
                public void EmptySource_ShouldReturnDefaultArea()
                {
                    // arrange
                    var objects = Enumerable.Empty<TestObject<Area>>();

                    // act
                    var result = EnumerableExtensions.Sum(objects, e => e.Property);

                    // assert
                    result.Should().Be(default(Area));
                }

                [Fact]
                public void ShouldReturnValidResult()
                {
                    // arrange
                    var objects = Fixture.CreateMany<Area>(3).Select(e => new TestObject<Area>(e));
                    number expectedResultInMetres = objects.Sum(e => e.Property.SquareMetres);
                    var expectedResultUnit = objects.First().Property.Unit;
                    var expectedResult = new Area(expectedResultInMetres).Convert(expectedResultUnit);

                    // act
                    var result = EnumerableExtensions.Sum(objects, e => e.Property);

                    // assert
                    result.SquareMetres.Should().BeApproximately(expectedResult.SquareMetres);
                    result.Unit.Should().Be(expectedResult.Unit);
                    result.Value.Should().BeApproximately(expectedResult.Value);
                }
            }

            public class Extremes : AreaQuantity
            {
                public Extremes(TestFixture testFixture) : base(testFixture) { }

                [Fact]
                public void NullSource_ShouldThrow()
                {
                    // arrange
                    IEnumerable<Area> source = null;

                    // act
                    Action extremes = () => EnumerableExtensions.Extremes(source);

                    // assert
                    extremes.Should().Throw<ArgumentNullException>()
                        .And.ParamName.Should().Be("source");
                }

                [Fact]
                public void EmptySource_ShouldThrow()
                {
                    // arrange
                    var source = Enumerable.Empty<Area>();

                    // act
                    Action extremes = () => EnumerableExtensions.Extremes(source);

                    // assert
                    extremes.Should().Throw<InvalidOperationException>();
                }

                [Fact]
                public void ShouldReturnValidResult()
                {
                    // arrange
                    var source = Fixture.CreateMany<Area>(3).ToList();

                    var min = source.OrderBy(e => e).First();
                    var max = source.OrderBy(e => e).Last();
                    var expectedResult = (min, max);

                    // act
                    var result = EnumerableExtensions.Extremes(source);

                    // assert
                    result.Should().Be(expectedResult, because: "The source was: {0}", string.Join(", ", source.Select(e => e.ToString())));
                }
            }

            public class ExtremesBySelector : AreaQuantity
            {
                public ExtremesBySelector(TestFixture testFixture) : base(testFixture) { }

                [Fact]
                public void NullSource_ShouldThrow()
                {
                    // arrange
                    IEnumerable<TestObject<Area>> source = null;
                    Func<TestObject<Area>, Area> selector = e => e.Property;

                    // act
                    Action extremes = () => EnumerableExtensions.Extremes(source, selector);

                    // assert
                    extremes.Should().Throw<ArgumentNullException>()
                        .And.ParamName.Should().Be("source");
                }

                [Fact]
                public void NullSelector_ShouldThrow()
                {
                    // arrange
                    var source = Enumerable.Empty<TestObject<Area>>();
                    Func<TestObject<Area>, Area> selector = null;

                    // act
                    Action extremes = () => EnumerableExtensions.Extremes(source, selector);

                    // assert
                    extremes.Should().Throw<ArgumentNullException>()
                        .And.ParamName.Should().Be("selector");
                }

                [Fact]
                public void EmptySource_ShouldThrow()
                {
                    // arrange
                    var source = Enumerable.Empty<TestObject<Area>>();
                    Func<TestObject<Area>, Area> selector = e => e.Property;

                    // act
                    Action extremes = () => EnumerableExtensions.Extremes(source, selector);

                    // assert
                    extremes.Should().Throw<InvalidOperationException>();
                }

                [Fact]
                public void ShouldReturnValidResult()
                {
                    // arrange
                    var source = Fixture.CreateMany<Area>(3).Select(e => new TestObject<Area>(e)).ToList();
                    Func<TestObject<Area>, Area> selector = e => e.Property;

                    var min = source.OrderBy(selector).First().Property;
                    var max = source.OrderBy(selector).Last().Property;
                    var expectedResult = (min, max);

                    // act
                    var result = EnumerableExtensions.Extremes(source, selector);

                    // assert
                    result.Should().Be(expectedResult, because: "The source was: {0}", string.Join(", ", source.Select(e => selector(e).ToString())));
                }
            }
        }
    }
}
