using AutoFixture;
using FluentAssertions;
using QuantitativeWorld.TestAbstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace QuantitativeWorld.Tests
{
#if DECIMAL
    using number = System.Decimal;
    using Constants = QuantitativeWorld.DecimalConstants;
#else
    using number = System.Double;
    using Constants = QuantitativeWorld.DoubleConstants;
#endif

    partial class EnumerableExtensionsTests
    {
        public class EnergyQuantity : EnumerableExtensionsTests
        {
            public EnergyQuantity(TestFixture testFixture) : base(testFixture) { }

            public class Average : EnergyQuantity
            {
                public Average(TestFixture testFixture) : base(testFixture) { }

                [Fact]
                public void NullSource_ShouldThrow()
                {
                    // arrange
                    IEnumerable<Energy> source = null;

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
                    var source = Enumerable.Empty<Energy>();

                    // act
                    Action average = () => EnumerableExtensions.Average(source);

                    // assert
                    average.Should().Throw<InvalidOperationException>();
                }

                [Fact]
                public void ShouldReturnValidResult()
                {
                    // arrange
                    var source = Fixture.CreateMany<Energy>(3);

                    number expectedResultInJoules = source.Average(e => e.Joules);
                    var expectedResultUnit = source.First().Unit;
                    var expectedResult = new Energy(expectedResultInJoules).Convert(expectedResultUnit);

                    // act
                    var result = EnumerableExtensions.Average(source);

                    // assert
                    result.Joules.Should().BeApproximately(expectedResult.Joules);
                    result.Unit.Should().Be(expectedResult.Unit);
                    result.Value.Should().BeApproximately(expectedResult.Value);
                }
            }

            public class AverageBySelector : EnergyQuantity
            {
                public AverageBySelector(TestFixture testFixture) : base(testFixture) { }

                [Fact]
                public void NullSource_ShouldThrow()
                {
                    // arrange
                    IEnumerable<TestObject<Energy>> source = null;
                    Func<TestObject<Energy>, Energy> selector = e => e.Property;

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
                    var source = Enumerable.Empty<TestObject<Energy>>();
                    Func<TestObject<Energy>, Energy> selector = null;

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
                    var source = Enumerable.Empty<TestObject<Energy>>();
                    Func<TestObject<Energy>, Energy> selector = e => e.Property;

                    // act
                    Action average = () => EnumerableExtensions.Average(source, selector);

                    // assert
                    average.Should().Throw<InvalidOperationException>();
                }

                [Fact]
                public void ShouldReturnValidResult()
                {
                    // arrange
                    var source = Fixture.CreateMany<Energy>(3).Select(e => new TestObject<Energy>(e));
                    Func<TestObject<Energy>, Energy> selector = e => e.Property;

                    number expectedResultInJoules = source.Average(e => e.Property.Joules);
                    var expectedResultUnit = source.First().Property.Unit;
                    var expectedResult = new Energy(expectedResultInJoules).Convert(expectedResultUnit);

                    // act
                    var result = EnumerableExtensions.Average(source, selector);

                    // assert
                    result.Joules.Should().BeApproximately(expectedResult.Joules);
                    result.Unit.Should().Be(expectedResult.Unit);
                    result.Value.Should().BeApproximately(expectedResult.Value);
                }
            }

            public class Max : EnergyQuantity
            {
                public Max(TestFixture testFixture) : base(testFixture) { }

                [Fact]
                public void NullSource_ShouldThrow()
                {
                    // arrange
                    IEnumerable<Energy> source = null;

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
                    var source = Enumerable.Empty<Energy>();

                    // act
                    Action max = () => EnumerableExtensions.Max(source);

                    // assert
                    max.Should().Throw<InvalidOperationException>();
                }

                [Fact]
                public void ShouldReturnValidResult()
                {
                    // arrange
                    var source = Fixture.CreateMany<Energy>(3);

                    var expectedResult = source.OrderByDescending(e => e).First();

                    // act
                    var result = EnumerableExtensions.Max(source);

                    // assert
                    result.Should().Be(expectedResult);
                }
            }

            public class MaxBySelector : EnergyQuantity
            {
                public MaxBySelector(TestFixture testFixture) : base(testFixture) { }

                [Fact]
                public void NullSource_ShouldThrow()
                {
                    // arrange
                    IEnumerable<TestObject<Energy>> source = null;
                    Func<TestObject<Energy>, Energy> selector = e => e.Property;

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
                    var source = Enumerable.Empty<TestObject<Energy>>();
                    Func<TestObject<Energy>, Energy> selector = null;

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
                    var source = Enumerable.Empty<TestObject<Energy>>();
                    Func<TestObject<Energy>, Energy> selector = e => e.Property;

                    // act
                    Action max = () => EnumerableExtensions.Max(source, selector);

                    // assert
                    max.Should().Throw<InvalidOperationException>();
                }

                [Fact]
                public void ShouldReturnValidResult()
                {
                    // arrange
                    var source = Fixture.CreateMany<Energy>(3).Select(e => new TestObject<Energy>(e));
                    Func<TestObject<Energy>, Energy> selector = e => e.Property;

                    var expectedResult = source.OrderByDescending(e => e.Property).First().Property;

                    // act
                    var result = EnumerableExtensions.Max(source, selector);

                    // assert
                    result.Should().Be(expectedResult);
                }
            }

            public class Min : EnergyQuantity
            {
                public Min(TestFixture testFixture) : base(testFixture) { }

                [Fact]
                public void NullSource_ShouldThrow()
                {
                    // arrange
                    IEnumerable<Energy> source = null;

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
                    var source = Enumerable.Empty<Energy>();

                    // act
                    Action min = () => EnumerableExtensions.Min(source);

                    // assert
                    min.Should().Throw<InvalidOperationException>();
                }

                [Fact]
                public void ShouldReturnValidResult()
                {
                    // arrange
                    var source = Fixture.CreateMany<Energy>(3);

                    var expectedResult = source.OrderBy(e => e).First();

                    // act
                    var result = EnumerableExtensions.Min(source);

                    // assert
                    result.Should().Be(expectedResult);
                }
            }

            public class MinBySelector : EnergyQuantity
            {
                public MinBySelector(TestFixture testFixture) : base(testFixture) { }

                [Fact]
                public void NullSource_ShouldThrow()
                {
                    // arrange
                    IEnumerable<TestObject<Energy>> source = null;
                    Func<TestObject<Energy>, Energy> selector = e => e.Property;

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
                    var source = Enumerable.Empty<TestObject<Energy>>();
                    Func<TestObject<Energy>, Energy> selector = null;

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
                    var source = Enumerable.Empty<TestObject<Energy>>();
                    Func<TestObject<Energy>, Energy> selector = e => e.Property;

                    // act
                    Action min = () => EnumerableExtensions.Min(source, selector);

                    // assert
                    min.Should().Throw<InvalidOperationException>();
                }

                [Fact]
                public void ShouldReturnValidResult()
                {
                    // arrange
                    var source = Fixture.CreateMany<Energy>(3).Select(e => new TestObject<Energy>(e));
                    Func<TestObject<Energy>, Energy> selector = e => e.Property;

                    var expectedResult = source.OrderBy(e => e.Property).First().Property;

                    // act
                    var result = EnumerableExtensions.Min(source, selector);

                    // assert
                    result.Should().Be(expectedResult);
                }
            }

            public class Sum : EnergyQuantity
            {
                public Sum(TestFixture testFixture) : base(testFixture) { }

                [Fact]
                public void NullSource_ShouldThrow()
                {
                    // arrange
                    IEnumerable<Energy> areas = null;

                    // act
                    Action sum = () => EnumerableExtensions.Sum(areas);

                    // assert
                    sum.Should().Throw<ArgumentNullException>()
                        .And.ParamName.Should().Be("source");
                }

                [Fact]
                public void EmptySource_ShouldReturnDefaultEnergy()
                {
                    // arrange
                    var areas = Enumerable.Empty<Energy>();

                    // act
                    var result = EnumerableExtensions.Sum(areas);

                    // assert
                    result.Should().Be(default(Energy));
                }

                [Fact]
                public void ShouldReturnValidResult()
                {
                    // arrange
                    var areas = Fixture.CreateMany<Energy>(3);
                    number expectedResultInJoules = areas.Sum(e => e.Joules);
                    var expectedResultUnit = areas.First().Unit;
                    var expectedResult = new Energy(expectedResultInJoules).Convert(expectedResultUnit);

                    // act
                    var result = EnumerableExtensions.Sum(areas);

                    // assert
                    result.Joules.Should().BeApproximately(expectedResult.Joules);
                    result.Unit.Should().Be(expectedResult.Unit);
                    result.Value.Should().BeApproximately(expectedResult.Value);
                }
            }

            public class SumBySelector : EnergyQuantity
            {
                public SumBySelector(TestFixture testFixture) : base(testFixture) { }

                [Fact]
                public void NullSource_ShouldThrow()
                {
                    // arrange
                    IEnumerable<TestObject<Energy>> objects = null;
                    Func<TestObject<Energy>, Energy> selector = e => e.Property;

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
                    var objects = Enumerable.Empty<TestObject<Energy>>();
                    Func<TestObject<Energy>, Energy> selector = null;

                    // act
                    Action sum = () => EnumerableExtensions.Sum(objects, selector);

                    // assert
                    sum.Should().Throw<ArgumentNullException>()
                        .And.ParamName.Should().Be("selector");
                }

                [Fact]
                public void EmptySource_ShouldReturnDefaultEnergy()
                {
                    // arrange
                    var objects = Enumerable.Empty<TestObject<Energy>>();

                    // act
                    var result = EnumerableExtensions.Sum(objects, e => e.Property);

                    // assert
                    result.Should().Be(default(Energy));
                }

                [Fact]
                public void ShouldReturnValidResult()
                {
                    // arrange
                    var objects = Fixture.CreateMany<Energy>(3).Select(e => new TestObject<Energy>(e));
                    number expectedResultInMetres = objects.Sum(e => e.Property.Joules);
                    var expectedResultUnit = objects.First().Property.Unit;
                    var expectedResult = new Energy(expectedResultInMetres).Convert(expectedResultUnit);

                    // act
                    var result = EnumerableExtensions.Sum(objects, e => e.Property);

                    // assert
                    result.Joules.Should().BeApproximately(expectedResult.Joules);
                    result.Unit.Should().Be(expectedResult.Unit);
                    result.Value.Should().BeApproximately(expectedResult.Value);
                }
            }
        }
    }
}
