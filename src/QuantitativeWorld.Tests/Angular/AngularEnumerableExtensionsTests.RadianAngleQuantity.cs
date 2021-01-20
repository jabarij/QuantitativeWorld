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
        public class RadianAngleQuantity : AngularEnumerableExtensionsTests
        {
            public RadianAngleQuantity(TestFixture testFixture) : base(testFixture) { }

            public class Average : RadianAngleQuantity
            {
                public Average(TestFixture testFixture) : base(testFixture) { }

                [Fact]
                public void NullSource_ShouldThrow()
                {
                    // arrange
                    IEnumerable<RadianAngle> source = null;

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
                    var source = Enumerable.Empty<RadianAngle>();

                    // act
                    Action average = () => AngularEnumerableExtensions.Average(source);

                    // assert
                    average.Should().Throw<InvalidOperationException>();
                }

                [Fact]
                public void ShouldReturnValidResult()
                {
                    // arrange
                    var source = Fixture.CreateMany<RadianAngle>(3);

                    number expectedResultInRadians = source.Average(e => e.Radians);
                    var expectedResult = new RadianAngle(expectedResultInRadians);

                    // act
                    var result = AngularEnumerableExtensions.Average(source);

                    // assert
                    result.Radians.Should().Be(expectedResult.Radians);
                }
            }

            public class AverageBySelector : RadianAngleQuantity
            {
                public AverageBySelector(TestFixture testFixture) : base(testFixture) { }

                [Fact]
                public void NullSource_ShouldThrow()
                {
                    // arrange
                    IEnumerable<TestObject<RadianAngle>> source = null;
                    Func<TestObject<RadianAngle>, RadianAngle> selector = e => e.Property;

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
                    var source = Enumerable.Empty<TestObject<RadianAngle>>();
                    Func<TestObject<RadianAngle>, RadianAngle> selector = null;

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
                    var source = Enumerable.Empty<TestObject<RadianAngle>>();
                    Func<TestObject<RadianAngle>, RadianAngle> selector = e => e.Property;

                    // act
                    Action average = () => AngularEnumerableExtensions.Average(source, selector);

                    // assert
                    average.Should().Throw<InvalidOperationException>();
                }

                [Fact]
                public void ShouldReturnValidResult()
                {
                    // arrange
                    var source = Fixture.CreateMany<RadianAngle>(3).Select(e => new TestObject<RadianAngle>(e));
                    Func<TestObject<RadianAngle>, RadianAngle> selector = e => e.Property;

                    number expectedResultInRadians = source.Average(e => e.Property.Radians);
                    var expectedResult = new RadianAngle(expectedResultInRadians);

                    // act
                    var result = AngularEnumerableExtensions.Average(source, selector);

                    // assert
                    result.Radians.Should().Be(expectedResult.Radians);
                }
            }

            public class Max : RadianAngleQuantity
            {
                public Max(TestFixture testFixture) : base(testFixture) { }

                [Fact]
                public void NullSource_ShouldThrow()
                {
                    // arrange
                    IEnumerable<RadianAngle> source = null;

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
                    var source = Enumerable.Empty<RadianAngle>();

                    // act
                    Action max = () => EnumerableExtensions.Max(source);

                    // assert
                    max.Should().Throw<InvalidOperationException>();
                }

                [Fact]
                public void ShouldReturnValidResult()
                {
                    // arrange
                    var source = Fixture.CreateMany<RadianAngle>(3);

                    var expectedResult = source.OrderByDescending(e => e).First();

                    // act
                    var result = EnumerableExtensions.Max(source);

                    // assert
                    result.Should().Be(expectedResult);
                }
            }

            public class MaxBySelector : RadianAngleQuantity
            {
                public MaxBySelector(TestFixture testFixture) : base(testFixture) { }

                [Fact]
                public void NullSource_ShouldThrow()
                {
                    // arrange
                    IEnumerable<TestObject<RadianAngle>> source = null;
                    Func<TestObject<RadianAngle>, RadianAngle> selector = e => e.Property;

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
                    var source = Enumerable.Empty<TestObject<RadianAngle>>();
                    Func<TestObject<RadianAngle>, RadianAngle> selector = null;

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
                    var source = Enumerable.Empty<TestObject<RadianAngle>>();
                    Func<TestObject<RadianAngle>, RadianAngle> selector = e => e.Property;

                    // act
                    Action max = () => EnumerableExtensions.Max(source, selector);

                    // assert
                    max.Should().Throw<InvalidOperationException>();
                }

                [Fact]
                public void ShouldReturnValidResult()
                {
                    // arrange
                    var source = Fixture.CreateMany<RadianAngle>(3).Select(e => new TestObject<RadianAngle>(e));
                    Func<TestObject<RadianAngle>, RadianAngle> selector = e => e.Property;

                    var expectedResult = source.OrderByDescending(e => e.Property).First().Property;

                    // act
                    var result = EnumerableExtensions.Max(source, selector);

                    // assert
                    result.Should().Be(expectedResult);
                }
            }

            public class Min : RadianAngleQuantity
            {
                public Min(TestFixture testFixture) : base(testFixture) { }

                [Fact]
                public void NullSource_ShouldThrow()
                {
                    // arrange
                    IEnumerable<RadianAngle> source = null;

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
                    var source = Enumerable.Empty<RadianAngle>();

                    // act
                    Action min = () => EnumerableExtensions.Min(source);

                    // assert
                    min.Should().Throw<InvalidOperationException>();
                }

                [Fact]
                public void ShouldReturnValidResult()
                {
                    // arrange
                    var source = Fixture.CreateMany<RadianAngle>(3);

                    var expectedResult = source.OrderBy(e => e).First();

                    // act
                    var result = EnumerableExtensions.Min(source);

                    // assert
                    result.Should().Be(expectedResult);
                }
            }

            public class MinBySelector : RadianAngleQuantity
            {
                public MinBySelector(TestFixture testFixture) : base(testFixture) { }

                [Fact]
                public void NullSource_ShouldThrow()
                {
                    // arrange
                    IEnumerable<TestObject<RadianAngle>> source = null;
                    Func<TestObject<RadianAngle>, RadianAngle> selector = e => e.Property;

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
                    var source = Enumerable.Empty<TestObject<RadianAngle>>();
                    Func<TestObject<RadianAngle>, RadianAngle> selector = null;

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
                    var source = Enumerable.Empty<TestObject<RadianAngle>>();
                    Func<TestObject<RadianAngle>, RadianAngle> selector = e => e.Property;

                    // act
                    Action min = () => EnumerableExtensions.Min(source, selector);

                    // assert
                    min.Should().Throw<InvalidOperationException>();
                }

                [Fact]
                public void ShouldReturnValidResult()
                {
                    // arrange
                    var source = Fixture.CreateMany<RadianAngle>(3).Select(e => new TestObject<RadianAngle>(e));
                    Func<TestObject<RadianAngle>, RadianAngle> selector = e => e.Property;

                    var expectedResult = source.OrderBy(e => e.Property).First().Property;

                    // act
                    var result = EnumerableExtensions.Min(source, selector);

                    // assert
                    result.Should().Be(expectedResult);
                }
            }

            public class Sum : RadianAngleQuantity
            {
                public Sum(TestFixture testFixture) : base(testFixture) { }

                [Fact]
                public void NullSource_ShouldThrow()
                {
                    // arrange
                    IEnumerable<RadianAngle> areas = null;

                    // act
                    Action sum = () => AngularEnumerableExtensions.Sum(areas);

                    // assert
                    sum.Should().Throw<ArgumentNullException>()
                        .And.ParamName.Should().Be("source");
                }

                [Fact]
                public void EmptySource_ShouldReturnDefaultRadianAngle()
                {
                    // arrange
                    var areas = Enumerable.Empty<RadianAngle>();

                    // act
                    var result = AngularEnumerableExtensions.Sum(areas);

                    // assert
                    result.Should().Be(default(RadianAngle));
                }

                [Fact]
                public void ShouldReturnValidResult()
                {
                    // arrange
                    var areas = Fixture.CreateMany<RadianAngle>(3);
                    number expectedResultInRadians = areas.Sum(e => e.Radians);
                    var expectedResult = new RadianAngle(expectedResultInRadians);

                    // act
                    var result = AngularEnumerableExtensions.Sum(areas);

                    // assert
                    result.Radians.Should().Be(expectedResult.Radians);
                }
            }

            public class SumBySelector : RadianAngleQuantity
            {
                public SumBySelector(TestFixture testFixture) : base(testFixture) { }

                [Fact]
                public void NullSource_ShouldThrow()
                {
                    // arrange
                    IEnumerable<TestObject<RadianAngle>> objects = null;
                    Func<TestObject<RadianAngle>, RadianAngle> selector = e => e.Property;

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
                    var objects = Enumerable.Empty<TestObject<RadianAngle>>();
                    Func<TestObject<RadianAngle>, RadianAngle> selector = null;

                    // act
                    Action sum = () => AngularEnumerableExtensions.Sum(objects, selector);

                    // assert
                    sum.Should().Throw<ArgumentNullException>()
                        .And.ParamName.Should().Be("selector");
                }

                [Fact]
                public void EmptySource_ShouldReturnDefaultRadianAngle()
                {
                    // arrange
                    var objects = Enumerable.Empty<TestObject<RadianAngle>>();

                    // act
                    var result = AngularEnumerableExtensions.Sum(objects, e => e.Property);

                    // assert
                    result.Should().Be(default(RadianAngle));
                }

                [Fact]
                public void ShouldReturnValidResult()
                {
                    // arrange
                    var objects = Fixture.CreateMany<RadianAngle>(3).Select(e => new TestObject<RadianAngle>(e));
                    number expectedResultInMetres = objects.Sum(e => e.Property.Radians);
                    var expectedResult = new RadianAngle(expectedResultInMetres);

                    // act
                    var result = AngularEnumerableExtensions.Sum(objects, e => e.Property);

                    // assert
                    result.Radians.Should().Be(expectedResult.Radians);
                }
            }
        }
    }
}
