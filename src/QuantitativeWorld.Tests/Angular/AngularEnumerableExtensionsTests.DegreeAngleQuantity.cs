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
#if DECIMAL
    using number = System.Decimal;
    using Constants = QuantitativeWorld.DecimalConstants;
#else
    using number = System.Double;
    using Constants = QuantitativeWorld.DoubleConstants;
#endif

    partial class AngularEnumerableExtensionsTests
    {
        public class DegreeAngleQuantity : AngularEnumerableExtensionsTests
        {
            public DegreeAngleQuantity(TestFixture testFixture) : base(testFixture) { }

            public class Average : DegreeAngleQuantity
            {
                public Average(TestFixture testFixture) : base(testFixture) { }

                [Fact]
                public void NullSource_ShouldThrow()
                {
                    // arrange
                    IEnumerable<DegreeAngle> source = null;

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
                    var source = Enumerable.Empty<DegreeAngle>();

                    // act
                    Action average = () => AngularEnumerableExtensions.Average(source);

                    // assert
                    average.Should().Throw<InvalidOperationException>();
                }

                [Fact]
                public void ShouldReturnValidResult()
                {
                    // arrange
                    var source = Fixture.CreateMany<DegreeAngle>(3);

                    number expectedResultInTotalSeconds = source.Average(e => e.TotalSeconds);
                    var expectedResult = new DegreeAngle(expectedResultInTotalSeconds);

                    // act
                    var result = AngularEnumerableExtensions.Average(source);

                    // assert
                    result.TotalSeconds.Should().Be(expectedResult.TotalSeconds);
                }
            }

            public class AverageBySelector : DegreeAngleQuantity
            {
                public AverageBySelector(TestFixture testFixture) : base(testFixture) { }

                [Fact]
                public void NullSource_ShouldThrow()
                {
                    // arrange
                    IEnumerable<TestObject<DegreeAngle>> source = null;
                    Func<TestObject<DegreeAngle>, DegreeAngle> selector = e => e.Property;

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
                    var source = Enumerable.Empty<TestObject<DegreeAngle>>();
                    Func<TestObject<DegreeAngle>, DegreeAngle> selector = null;

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
                    var source = Enumerable.Empty<TestObject<DegreeAngle>>();
                    Func<TestObject<DegreeAngle>, DegreeAngle> selector = e => e.Property;

                    // act
                    Action average = () => AngularEnumerableExtensions.Average(source, selector);

                    // assert
                    average.Should().Throw<InvalidOperationException>();
                }

                [Fact]
                public void ShouldReturnValidResult()
                {
                    // arrange
                    var source = Fixture.CreateMany<DegreeAngle>(3).Select(e => new TestObject<DegreeAngle>(e));
                    Func<TestObject<DegreeAngle>, DegreeAngle> selector = e => e.Property;

                    number expectedResultInTotalSeconds = source.Average(e => e.Property.TotalSeconds);
                    var expectedResult = new DegreeAngle(expectedResultInTotalSeconds);

                    // act
                    var result = AngularEnumerableExtensions.Average(source, selector);

                    // assert
                    result.TotalSeconds.Should().Be(expectedResult.TotalSeconds);
                }
            }

            public class Max : DegreeAngleQuantity
            {
                public Max(TestFixture testFixture) : base(testFixture) { }

                [Fact]
                public void NullSource_ShouldThrow()
                {
                    // arrange
                    IEnumerable<DegreeAngle> source = null;

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
                    var source = Enumerable.Empty<DegreeAngle>();

                    // act
                    Action max = () => EnumerableExtensions.Max(source);

                    // assert
                    max.Should().Throw<InvalidOperationException>();
                }

                [Fact]
                public void ShouldReturnValidResult()
                {
                    // arrange
                    var source = Fixture.CreateMany<DegreeAngle>(3);

                    var expectedResult = source.OrderByDescending(e => e).First();

                    // act
                    var result = EnumerableExtensions.Max(source);

                    // assert
                    result.Should().Be(expectedResult);
                }
            }

            public class MaxBySelector : DegreeAngleQuantity
            {
                public MaxBySelector(TestFixture testFixture) : base(testFixture) { }

                [Fact]
                public void NullSource_ShouldThrow()
                {
                    // arrange
                    IEnumerable<TestObject<DegreeAngle>> source = null;
                    Func<TestObject<DegreeAngle>, DegreeAngle> selector = e => e.Property;

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
                    var source = Enumerable.Empty<TestObject<DegreeAngle>>();
                    Func<TestObject<DegreeAngle>, DegreeAngle> selector = null;

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
                    var source = Enumerable.Empty<TestObject<DegreeAngle>>();
                    Func<TestObject<DegreeAngle>, DegreeAngle> selector = e => e.Property;

                    // act
                    Action max = () => EnumerableExtensions.Max(source, selector);

                    // assert
                    max.Should().Throw<InvalidOperationException>();
                }

                [Fact]
                public void ShouldReturnValidResult()
                {
                    // arrange
                    var source = Fixture.CreateMany<DegreeAngle>(3).Select(e => new TestObject<DegreeAngle>(e));
                    Func<TestObject<DegreeAngle>, DegreeAngle> selector = e => e.Property;

                    var expectedResult = source.OrderByDescending(e => e.Property).First().Property;

                    // act
                    var result = EnumerableExtensions.Max(source, selector);

                    // assert
                    result.Should().Be(expectedResult);
                }
            }

            public class Min : DegreeAngleQuantity
            {
                public Min(TestFixture testFixture) : base(testFixture) { }

                [Fact]
                public void NullSource_ShouldThrow()
                {
                    // arrange
                    IEnumerable<DegreeAngle> source = null;

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
                    var source = Enumerable.Empty<DegreeAngle>();

                    // act
                    Action min = () => EnumerableExtensions.Min(source);

                    // assert
                    min.Should().Throw<InvalidOperationException>();
                }

                [Fact]
                public void ShouldReturnValidResult()
                {
                    // arrange
                    var source = Fixture.CreateMany<DegreeAngle>(3);

                    var expectedResult = source.OrderBy(e => e).First();

                    // act
                    var result = EnumerableExtensions.Min(source);

                    // assert
                    result.Should().Be(expectedResult);
                }
            }

            public class MinBySelector : DegreeAngleQuantity
            {
                public MinBySelector(TestFixture testFixture) : base(testFixture) { }

                [Fact]
                public void NullSource_ShouldThrow()
                {
                    // arrange
                    IEnumerable<TestObject<DegreeAngle>> source = null;
                    Func<TestObject<DegreeAngle>, DegreeAngle> selector = e => e.Property;

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
                    var source = Enumerable.Empty<TestObject<DegreeAngle>>();
                    Func<TestObject<DegreeAngle>, DegreeAngle> selector = null;

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
                    var source = Enumerable.Empty<TestObject<DegreeAngle>>();
                    Func<TestObject<DegreeAngle>, DegreeAngle> selector = e => e.Property;

                    // act
                    Action min = () => EnumerableExtensions.Min(source, selector);

                    // assert
                    min.Should().Throw<InvalidOperationException>();
                }

                [Fact]
                public void ShouldReturnValidResult()
                {
                    // arrange
                    var source = Fixture.CreateMany<DegreeAngle>(3).Select(e => new TestObject<DegreeAngle>(e));
                    Func<TestObject<DegreeAngle>, DegreeAngle> selector = e => e.Property;

                    var expectedResult = source.OrderBy(e => e.Property).First().Property;

                    // act
                    var result = EnumerableExtensions.Min(source, selector);

                    // assert
                    result.Should().Be(expectedResult);
                }
            }

            public class Sum : DegreeAngleQuantity
            {
                public Sum(TestFixture testFixture) : base(testFixture) { }

                [Fact]
                public void NullSource_ShouldThrow()
                {
                    // arrange
                    IEnumerable<DegreeAngle> areas = null;

                    // act
                    Action sum = () => AngularEnumerableExtensions.Sum(areas);

                    // assert
                    sum.Should().Throw<ArgumentNullException>()
                        .And.ParamName.Should().Be("source");
                }

                [Fact]
                public void EmptySource_ShouldReturnDefaultDegreeAngle()
                {
                    // arrange
                    var areas = Enumerable.Empty<DegreeAngle>();

                    // act
                    var result = AngularEnumerableExtensions.Sum(areas);

                    // assert
                    result.Should().Be(default(DegreeAngle));
                }

                [Fact]
                public void ShouldReturnValidResult()
                {
                    // arrange
                    var areas = Fixture.CreateMany<DegreeAngle>(3);
                    number expectedResultInTotalSeconds = areas.Sum(e => e.TotalSeconds);
                    var expectedResult = new DegreeAngle(expectedResultInTotalSeconds);

                    // act
                    var result = AngularEnumerableExtensions.Sum(areas);

                    // assert
                    result.TotalSeconds.Should().Be(expectedResult.TotalSeconds);
                }
            }

            public class SumBySelector : DegreeAngleQuantity
            {
                public SumBySelector(TestFixture testFixture) : base(testFixture) { }

                [Fact]
                public void NullSource_ShouldThrow()
                {
                    // arrange
                    IEnumerable<TestObject<DegreeAngle>> objects = null;
                    Func<TestObject<DegreeAngle>, DegreeAngle> selector = e => e.Property;

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
                    var objects = Enumerable.Empty<TestObject<DegreeAngle>>();
                    Func<TestObject<DegreeAngle>, DegreeAngle> selector = null;

                    // act
                    Action sum = () => AngularEnumerableExtensions.Sum(objects, selector);

                    // assert
                    sum.Should().Throw<ArgumentNullException>()
                        .And.ParamName.Should().Be("selector");
                }

                [Fact]
                public void EmptySource_ShouldReturnDefaultDegreeAngle()
                {
                    // arrange
                    var objects = Enumerable.Empty<TestObject<DegreeAngle>>();

                    // act
                    var result = AngularEnumerableExtensions.Sum(objects, e => e.Property);

                    // assert
                    result.Should().Be(default(DegreeAngle));
                }

                [Fact]
                public void ShouldReturnValidResult()
                {
                    // arrange
                    var objects = Fixture.CreateMany<DegreeAngle>(3).Select(e => new TestObject<DegreeAngle>(e));
                    number expectedResultInMetres = objects.Sum(e => e.Property.TotalSeconds);
                    var expectedResult = new DegreeAngle(expectedResultInMetres);

                    // act
                    var result = AngularEnumerableExtensions.Sum(objects, e => e.Property);

                    // assert
                    result.TotalSeconds.Should().Be(expectedResult.TotalSeconds);
                }
            }
        }
    }
}
