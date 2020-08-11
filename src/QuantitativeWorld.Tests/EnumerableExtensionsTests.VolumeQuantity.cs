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
        public class VolumeQuantity : EnumerableExtensionsTests
        {
            public VolumeQuantity(TestFixture testFixture) : base(testFixture) { }

            public class Average : VolumeQuantity
            {
                public Average(TestFixture testFixture) : base(testFixture) { }

                [Fact]
                public void NullSource_ShouldThrow()
                {
                    // arrange
                    IEnumerable<Volume> source = null;

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
                    var source = Enumerable.Empty<Volume>();

                    // act
                    Action average = () => EnumerableExtensions.Average(source);

                    // assert
                    average.Should().Throw<InvalidOperationException>();
                }

                [Fact]
                public void ShouldReturnValidResult()
                {
                    // arrange
                    var source = Fixture.CreateMany<Volume>(3);

                    double expectedResultInCubicMetres = source.Average(e => e.CubicMetres);
                    var expectedResultUnit = source.First().Unit;
                    var expectedResult = new Volume(expectedResultInCubicMetres).Convert(expectedResultUnit);

                    // act
                    var result = EnumerableExtensions.Average(source);

                    // assert
                    result.CubicMetres.Should().BeApproximately(expectedResult.CubicMetres, DoublePrecision);
                    result.Unit.Should().Be(expectedResult.Unit);
                    result.Value.Should().BeApproximately(expectedResult.Value, DoublePrecision);
                }
            }

            public class AverageBySelector : VolumeQuantity
            {
                public AverageBySelector(TestFixture testFixture) : base(testFixture) { }

                [Fact]
                public void NullSource_ShouldThrow()
                {
                    // arrange
                    IEnumerable<TestObject<Volume>> source = null;
                    Func<TestObject<Volume>, Volume> selector = e => e.Property;

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
                    var source = Enumerable.Empty<TestObject<Volume>>();
                    Func<TestObject<Volume>, Volume> selector = null;

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
                    var source = Enumerable.Empty<TestObject<Volume>>();
                    Func<TestObject<Volume>, Volume> selector = e => e.Property;

                    // act
                    Action average = () => EnumerableExtensions.Average(source, selector);

                    // assert
                    average.Should().Throw<InvalidOperationException>();
                }

                [Fact]
                public void ShouldReturnValidResult()
                {
                    // arrange
                    var source = Fixture.CreateMany<Volume>(3).Select(e => new TestObject<Volume>(e));
                    Func<TestObject<Volume>, Volume> selector = e => e.Property;

                    double expectedResultInCubicMetres = source.Average(e => e.Property.CubicMetres);
                    var expectedResultUnit = source.First().Property.Unit;
                    var expectedResult = new Volume(expectedResultInCubicMetres).Convert(expectedResultUnit);

                    // act
                    var result = EnumerableExtensions.Average(source, selector);

                    // assert
                    result.CubicMetres.Should().BeApproximately(expectedResult.CubicMetres, DoublePrecision);
                    result.Unit.Should().Be(expectedResult.Unit);
                    result.Value.Should().BeApproximately(expectedResult.Value, DoublePrecision);
                }
            }

            public class Max : VolumeQuantity
            {
                public Max(TestFixture testFixture) : base(testFixture) { }

                [Fact]
                public void NullSource_ShouldThrow()
                {
                    // arrange
                    IEnumerable<Volume> source = null;

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
                    var source = Enumerable.Empty<Volume>();

                    // act
                    Action max = () => EnumerableExtensions.Max(source);

                    // assert
                    max.Should().Throw<InvalidOperationException>();
                }

                [Fact]
                public void ShouldReturnValidResult()
                {
                    // arrange
                    var source = Fixture.CreateMany<Volume>(3);

                    var expectedResult = source.OrderByDescending(e => e).First();

                    // act
                    var result = EnumerableExtensions.Max(source);

                    // assert
                    result.Should().Be(expectedResult);
                }
            }

            public class MaxBySelector : VolumeQuantity
            {
                public MaxBySelector(TestFixture testFixture) : base(testFixture) { }

                [Fact]
                public void NullSource_ShouldThrow()
                {
                    // arrange
                    IEnumerable<TestObject<Volume>> source = null;
                    Func<TestObject<Volume>, Volume> selector = e => e.Property;

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
                    var source = Enumerable.Empty<TestObject<Volume>>();
                    Func<TestObject<Volume>, Volume> selector = null;

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
                    var source = Enumerable.Empty<TestObject<Volume>>();
                    Func<TestObject<Volume>, Volume> selector = e => e.Property;

                    // act
                    Action max = () => EnumerableExtensions.Max(source, selector);

                    // assert
                    max.Should().Throw<InvalidOperationException>();
                }

                [Fact]
                public void ShouldReturnValidResult()
                {
                    // arrange
                    var source = Fixture.CreateMany<Volume>(3).Select(e => new TestObject<Volume>(e));
                    Func<TestObject<Volume>, Volume> selector = e => e.Property;

                    var expectedResult = source.OrderByDescending(e => e.Property).First().Property;

                    // act
                    var result = EnumerableExtensions.Max(source, selector);

                    // assert
                    result.Should().Be(expectedResult);
                }
            }

            public class Min : VolumeQuantity
            {
                public Min(TestFixture testFixture) : base(testFixture) { }

                [Fact]
                public void NullSource_ShouldThrow()
                {
                    // arrange
                    IEnumerable<Volume> source = null;

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
                    var source = Enumerable.Empty<Volume>();

                    // act
                    Action min = () => EnumerableExtensions.Min(source);

                    // assert
                    min.Should().Throw<InvalidOperationException>();
                }

                [Fact]
                public void ShouldReturnValidResult()
                {
                    // arrange
                    var source = Fixture.CreateMany<Volume>(3);

                    var expectedResult = source.OrderBy(e => e).First();

                    // act
                    var result = EnumerableExtensions.Min(source);

                    // assert
                    result.Should().Be(expectedResult);
                }
            }

            public class MinBySelector : VolumeQuantity
            {
                public MinBySelector(TestFixture testFixture) : base(testFixture) { }

                [Fact]
                public void NullSource_ShouldThrow()
                {
                    // arrange
                    IEnumerable<TestObject<Volume>> source = null;
                    Func<TestObject<Volume>, Volume> selector = e => e.Property;

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
                    var source = Enumerable.Empty<TestObject<Volume>>();
                    Func<TestObject<Volume>, Volume> selector = null;

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
                    var source = Enumerable.Empty<TestObject<Volume>>();
                    Func<TestObject<Volume>, Volume> selector = e => e.Property;

                    // act
                    Action min = () => EnumerableExtensions.Min(source, selector);

                    // assert
                    min.Should().Throw<InvalidOperationException>();
                }

                [Fact]
                public void ShouldReturnValidResult()
                {
                    // arrange
                    var source = Fixture.CreateMany<Volume>(3).Select(e => new TestObject<Volume>(e));
                    Func<TestObject<Volume>, Volume> selector = e => e.Property;

                    var expectedResult = source.OrderBy(e => e.Property).First().Property;

                    // act
                    var result = EnumerableExtensions.Min(source, selector);

                    // assert
                    result.Should().Be(expectedResult);
                }
            }

            public class Sum : VolumeQuantity
            {
                public Sum(TestFixture testFixture) : base(testFixture) { }

                [Fact]
                public void NullSource_ShouldThrow()
                {
                    // arrange
                    IEnumerable<Volume> areas = null;

                    // act
                    Action sum = () => EnumerableExtensions.Sum(areas);

                    // assert
                    sum.Should().Throw<ArgumentNullException>()
                        .And.ParamName.Should().Be("source");
                }

                [Fact]
                public void EmptySource_ShouldReturnDefaultVolume()
                {
                    // arrange
                    var areas = Enumerable.Empty<Volume>();

                    // act
                    var result = EnumerableExtensions.Sum(areas);

                    // assert
                    result.Should().Be(default(Volume));
                }

                [Fact]
                public void ShouldReturnValidResult()
                {
                    // arrange
                    var areas = Fixture.CreateMany<Volume>(3);
                    double expectedResultInCubicMetres = areas.Sum(e => e.CubicMetres);
                    var expectedResultUnit = areas.First().Unit;
                    var expectedResult = new Volume(expectedResultInCubicMetres).Convert(expectedResultUnit);

                    // act
                    var result = EnumerableExtensions.Sum(areas);

                    // assert
                    result.CubicMetres.Should().BeApproximately(expectedResult.CubicMetres, DoublePrecision);
                    result.Unit.Should().Be(expectedResult.Unit);
                    result.Value.Should().BeApproximately(expectedResult.Value, DoublePrecision);
                }
            }

            public class SumBySelector : VolumeQuantity
            {
                public SumBySelector(TestFixture testFixture) : base(testFixture) { }

                [Fact]
                public void NullSource_ShouldThrow()
                {
                    // arrange
                    IEnumerable<TestObject<Volume>> objects = null;
                    Func<TestObject<Volume>, Volume> selector = e => e.Property;

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
                    var objects = Enumerable.Empty<TestObject<Volume>>();
                    Func<TestObject<Volume>, Volume> selector = null;

                    // act
                    Action sum = () => EnumerableExtensions.Sum(objects, selector);

                    // assert
                    sum.Should().Throw<ArgumentNullException>()
                        .And.ParamName.Should().Be("selector");
                }

                [Fact]
                public void EmptySource_ShouldReturnDefaultVolume()
                {
                    // arrange
                    var objects = Enumerable.Empty<TestObject<Volume>>();

                    // act
                    var result = EnumerableExtensions.Sum(objects, e => e.Property);

                    // assert
                    result.Should().Be(default(Volume));
                }

                [Fact]
                public void ShouldReturnValidResult()
                {
                    // arrange
                    var objects = Fixture.CreateMany<Volume>(3).Select(e => new TestObject<Volume>(e));
                    double expectedResultInMetres = objects.Sum(e => e.Property.CubicMetres);
                    var expectedResultUnit = objects.First().Property.Unit;
                    var expectedResult = new Volume(expectedResultInMetres).Convert(expectedResultUnit);

                    // act
                    var result = EnumerableExtensions.Sum(objects, e => e.Property);

                    // assert
                    result.CubicMetres.Should().BeApproximately(expectedResult.CubicMetres, DoublePrecision);
                    result.Unit.Should().Be(expectedResult.Unit);
                    result.Value.Should().BeApproximately(expectedResult.Value, DoublePrecision);
                }
            }
        }
    }
}
