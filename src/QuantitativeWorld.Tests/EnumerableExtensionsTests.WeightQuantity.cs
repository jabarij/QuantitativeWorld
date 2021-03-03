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
        public class WeightQuantity : EnumerableExtensionsTests
        {
            public WeightQuantity(TestFixture testFixture) : base(testFixture) { }

            public class Average : WeightQuantity
            {
                public Average(TestFixture testFixture) : base(testFixture) { }

                [Fact]
                public void NullSource_ShouldThrow()
                {
                    // arrange
                    IEnumerable<Weight> source = null;

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
                    var source = Enumerable.Empty<Weight>();

                    // act
                    Action average = () => EnumerableExtensions.Average(source);

                    // assert
                    average.Should().Throw<InvalidOperationException>();
                }

                [Fact]
                public void ShouldReturnValidResult()
                {
                    // arrange
                    var source = Fixture.CreateMany<Weight>(3);

                    number expectedResultInKilograms = source.Average(e => e.Kilograms);
                    var expectedResultUnit = source.First().Unit;
                    var expectedResult = new Weight(expectedResultInKilograms).Convert(expectedResultUnit);

                    // act
                    var result = EnumerableExtensions.Average(source);

                    // assert
                    result.Kilograms.Should().BeApproximately(expectedResult.Kilograms);
                    result.Unit.Should().Be(expectedResult.Unit);
                    result.Value.Should().BeApproximately(expectedResult.Value);
                }
            }

            public class AverageBySelector : WeightQuantity
            {
                public AverageBySelector(TestFixture testFixture) : base(testFixture) { }

                [Fact]
                public void NullSource_ShouldThrow()
                {
                    // arrange
                    IEnumerable<TestObject<Weight>> source = null;
                    Func<TestObject<Weight>, Weight> selector = e => e.Property;

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
                    var source = Enumerable.Empty<TestObject<Weight>>();
                    Func<TestObject<Weight>, Weight> selector = null;

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
                    var source = Enumerable.Empty<TestObject<Weight>>();
                    Func<TestObject<Weight>, Weight> selector = e => e.Property;

                    // act
                    Action average = () => EnumerableExtensions.Average(source, selector);

                    // assert
                    average.Should().Throw<InvalidOperationException>();
                }

                [Fact]
                public void ShouldReturnValidResult()
                {
                    // arrange
                    var source = Fixture.CreateMany<Weight>(3).Select(e => new TestObject<Weight>(e));
                    Func<TestObject<Weight>, Weight> selector = e => e.Property;

                    number expectedResultInKilograms = source.Average(e => e.Property.Kilograms);
                    var expectedResultUnit = source.First().Property.Unit;
                    var expectedResult = new Weight(expectedResultInKilograms).Convert(expectedResultUnit);

                    // act
                    var result = EnumerableExtensions.Average(source, selector);

                    // assert
                    result.Kilograms.Should().BeApproximately(expectedResult.Kilograms);
                    result.Unit.Should().Be(expectedResult.Unit);
                    result.Value.Should().BeApproximately(expectedResult.Value);
                }
            }

            public class Max : WeightQuantity
            {
                public Max(TestFixture testFixture) : base(testFixture) { }

                [Fact]
                public void NullSource_ShouldThrow()
                {
                    // arrange
                    IEnumerable<Weight> source = null;

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
                    var source = Enumerable.Empty<Weight>();

                    // act
                    Action max = () => EnumerableExtensions.Max(source);

                    // assert
                    max.Should().Throw<InvalidOperationException>();
                }

                [Fact]
                public void ShouldReturnValidResult()
                {
                    // arrange
                    var source = Fixture.CreateMany<Weight>(3);

                    var expectedResult = source.OrderByDescending(e => e).First();

                    // act
                    var result = EnumerableExtensions.Max(source);

                    // assert
                    result.Should().Be(expectedResult);
                }
            }

            public class MaxBySelector : WeightQuantity
            {
                public MaxBySelector(TestFixture testFixture) : base(testFixture) { }

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

            public class Min : WeightQuantity
            {
                public Min(TestFixture testFixture) : base(testFixture) { }

                [Fact]
                public void NullSource_ShouldThrow()
                {
                    // arrange
                    IEnumerable<Weight> source = null;

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
                    var source = Enumerable.Empty<Weight>();

                    // act
                    Action min = () => EnumerableExtensions.Min(source);

                    // assert
                    min.Should().Throw<InvalidOperationException>();
                }

                [Fact]
                public void ShouldReturnValidResult()
                {
                    // arrange
                    var source = Fixture.CreateMany<Weight>(3);

                    var expectedResult = source.OrderBy(e => e).First();

                    // act
                    var result = EnumerableExtensions.Min(source);

                    // assert
                    result.Should().Be(expectedResult);
                }
            }

            public class MinBySelector : WeightQuantity
            {
                public MinBySelector(TestFixture testFixture) : base(testFixture) { }

                [Fact]
                public void NullSource_ShouldThrow()
                {
                    // arrange
                    IEnumerable<TestObject<Weight>> source = null;
                    Func<TestObject<Weight>, Weight> selector = e => e.Property;

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
                    var source = Enumerable.Empty<TestObject<Weight>>();
                    Func<TestObject<Weight>, Weight> selector = null;

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
                    var source = Enumerable.Empty<TestObject<Weight>>();
                    Func<TestObject<Weight>, Weight> selector = e => e.Property;

                    // act
                    Action min = () => EnumerableExtensions.Min(source, selector);

                    // assert
                    min.Should().Throw<InvalidOperationException>();
                }

                [Fact]
                public void ShouldReturnValidResult()
                {
                    // arrange
                    var source = Fixture.CreateMany<Weight>(3).Select(e => new TestObject<Weight>(e));
                    Func<TestObject<Weight>, Weight> selector = e => e.Property;

                    var expectedResult = source.OrderBy(e => e.Property).First().Property;

                    // act
                    var result = EnumerableExtensions.Min(source, selector);

                    // assert
                    result.Should().Be(expectedResult);
                }
            }

            public class Sum : WeightQuantity
            {
                public Sum(TestFixture testFixture) : base(testFixture) { }

                [Fact]
                public void NullSource_ShouldThrow()
                {
                    // arrange
                    IEnumerable<Weight> areas = null;

                    // act
                    Action sum = () => EnumerableExtensions.Sum(areas);

                    // assert
                    sum.Should().Throw<ArgumentNullException>()
                        .And.ParamName.Should().Be("source");
                }

                [Fact]
                public void EmptySource_ShouldReturnDefaultWeight()
                {
                    // arrange
                    var areas = Enumerable.Empty<Weight>();

                    // act
                    var result = EnumerableExtensions.Sum(areas);

                    // assert
                    result.Should().Be(default(Weight));
                }

                [Fact]
                public void ShouldReturnValidResult()
                {
                    // arrange
                    var areas = Fixture.CreateMany<Weight>(3);
                    number expectedResultInKilograms = areas.Sum(e => e.Kilograms);
                    var expectedResultUnit = areas.First().Unit;
                    var expectedResult = new Weight(expectedResultInKilograms).Convert(expectedResultUnit);

                    // act
                    var result = EnumerableExtensions.Sum(areas);

                    // assert
                    result.Kilograms.Should().BeApproximately(expectedResult.Kilograms);
                    result.Unit.Should().Be(expectedResult.Unit);
                    result.Value.Should().BeApproximately(expectedResult.Value);
                }
            }

            public class SumBySelector : WeightQuantity
            {
                public SumBySelector(TestFixture testFixture) : base(testFixture) { }

                [Fact]
                public void NullSource_ShouldThrow()
                {
                    // arrange
                    IEnumerable<TestObject<Weight>> objects = null;
                    Func<TestObject<Weight>, Weight> selector = e => e.Property;

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
                    var objects = Enumerable.Empty<TestObject<Weight>>();
                    Func<TestObject<Weight>, Weight> selector = null;

                    // act
                    Action sum = () => EnumerableExtensions.Sum(objects, selector);

                    // assert
                    sum.Should().Throw<ArgumentNullException>()
                        .And.ParamName.Should().Be("selector");
                }

                [Fact]
                public void EmptySource_ShouldReturnDefaultWeight()
                {
                    // arrange
                    var objects = Enumerable.Empty<TestObject<Weight>>();

                    // act
                    var result = EnumerableExtensions.Sum(objects, e => e.Property);

                    // assert
                    result.Should().Be(default(Weight));
                }

                [Fact]
                public void ShouldReturnValidResult()
                {
                    // arrange
                    var objects = Fixture.CreateMany<Weight>(3).Select(e => new TestObject<Weight>(e));
                    number expectedResultInMetres = objects.Sum(e => e.Property.Kilograms);
                    var expectedResultUnit = objects.First().Property.Unit;
                    var expectedResult = new Weight(expectedResultInMetres).Convert(expectedResultUnit);

                    // act
                    var result = EnumerableExtensions.Sum(objects, e => e.Property);

                    // assert
                    result.Kilograms.Should().BeApproximately(expectedResult.Kilograms);
                    result.Unit.Should().Be(expectedResult.Unit);
                    result.Value.Should().BeApproximately(expectedResult.Value);
                }
            }
        }
    }
}
