﻿using AutoFixture;
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
        public class NullableRadianAngleQuantity : AngularEnumerableExtensionsTests
        {
            public NullableRadianAngleQuantity(TestFixture testFixture) : base(testFixture) { }

            public class Max : NullableRadianAngleQuantity
            {
                public Max(TestFixture testFixture) : base(testFixture) { }

                [Fact]
                public void NullSource_ShouldThrow()
                {
                    // arrange
                    IEnumerable<RadianAngle?> source = null;

                    // act
                    Action max = () => AngularEnumerableExtensions.Max(source);

                    // assert
                    max.Should().Throw<ArgumentNullException>()
                        .And.ParamName.Should().Be("source");
                }

                [Fact]
                public void EmptySource_ShouldReturnNull()
                {
                    // arrange
                    var source = Enumerable.Empty<RadianAngle?>();

                    // act
                    var result = AngularEnumerableExtensions.Max(source);

                    // assert
                    result.Should().BeNull();
                }

                [Fact]
                public void ShouldReturnValidResult()
                {
                    // arrange
                    var source = Fixture.CreateMany<RadianAngle?>(3);
                    var expectedResult = source.OrderByDescending(e => e).First();

                    // act
                    var result = AngularEnumerableExtensions.Max(source);

                    // assert
                    result.Should().Be(expectedResult);
                }
            }

            public class MaxBySelector : NullableRadianAngleQuantity
            {
                public MaxBySelector(TestFixture testFixture) : base(testFixture) { }

                [Fact]
                public void NullSource_ShouldThrow()
                {
                    // arrange
                    IEnumerable<TestObject<RadianAngle?>> source = null;
                    Func<TestObject<RadianAngle?>, RadianAngle?> selector = e => e.Property;

                    // act
                    Action max = () => AngularEnumerableExtensions.Max(source, selector);

                    // assert
                    max.Should().Throw<ArgumentNullException>()
                        .And.ParamName.Should().Be("source");
                }

                [Fact]
                public void NullSelector_ShouldThrow()
                {
                    // arrange
                    var source = Enumerable.Empty<TestObject<RadianAngle?>>();
                    Func<TestObject<RadianAngle?>, RadianAngle?> selector = null;

                    // act
                    Action max = () => AngularEnumerableExtensions.Max(source, selector);

                    // assert
                    max.Should().Throw<ArgumentNullException>()
                        .And.ParamName.Should().Be("selector");
                }

                [Fact]
                public void EmptySource_ShouldReturnNull()
                {
                    // arrange
                    var source = Enumerable.Empty<TestObject<RadianAngle?>>();
                    Func<TestObject<RadianAngle?>, RadianAngle?> selector = e => e.Property;

                    // act
                    var result = AngularEnumerableExtensions.Max(source, selector);

                    // assert
                    result.Should().BeNull();
                }

                [Fact]
                public void ShouldReturnValidResult()
                {
                    // arrange
                    var source = Fixture.CreateMany<RadianAngle?>(3).Select(e => new TestObject<RadianAngle?>(e));
                    Func<TestObject<RadianAngle?>, RadianAngle?> selector = e => e.Property;

                    var expectedResult = source.OrderByDescending(e => e.Property.Value).First().Property;

                    // act
                    var result = AngularEnumerableExtensions.Max(source, selector);

                    // assert
                    result.Should().Be(expectedResult.Value);
                }
            }

            public class Min : NullableRadianAngleQuantity
            {
                public Min(TestFixture testFixture) : base(testFixture) { }

                [Fact]
                public void NullSource_ShouldThrow()
                {
                    // arrange
                    IEnumerable<RadianAngle?> source = null;

                    // act
                    Action min = () => AngularEnumerableExtensions.Min(source);

                    // assert
                    min.Should().Throw<ArgumentNullException>()
                        .And.ParamName.Should().Be("source");
                }

                [Fact]
                public void EmptySource_ShouldReturnNull()
                {
                    // arrange
                    var source = Enumerable.Empty<RadianAngle?>();

                    // act
                    var result = AngularEnumerableExtensions.Min(source);

                    // assert
                    result.Should().BeNull();
                }

                [Fact]
                public void ShouldReturnValidResult()
                {
                    // arrange
                    var source = Fixture.CreateMany<RadianAngle?>(3);
                    var expectedResult = source.OrderBy(e => e).First();

                    // act
                    var result = AngularEnumerableExtensions.Min(source);

                    // assert
                    result.Should().Be(expectedResult);
                }
            }

            public class MinBySelector : NullableRadianAngleQuantity
            {
                public MinBySelector(TestFixture testFixture) : base(testFixture) { }

                [Fact]
                public void NullSource_ShouldThrow()
                {
                    // arrange
                    IEnumerable<TestObject<RadianAngle?>> source = null;
                    Func<TestObject<RadianAngle?>, RadianAngle?> selector = e => e.Property;

                    // act
                    Action min = () => AngularEnumerableExtensions.Min(source, selector);

                    // assert
                    min.Should().Throw<ArgumentNullException>()
                        .And.ParamName.Should().Be("source");
                }

                [Fact]
                public void NullSelector_ShouldThrow()
                {
                    // arrange
                    var source = Enumerable.Empty<TestObject<RadianAngle?>>();
                    Func<TestObject<RadianAngle?>, RadianAngle?> selector = null;

                    // act
                    Action min = () => AngularEnumerableExtensions.Min(source, selector);

                    // assert
                    min.Should().Throw<ArgumentNullException>()
                        .And.ParamName.Should().Be("selector");
                }

                [Fact]
                public void EmptySource_ShouldReturnNull()
                {
                    // arrange
                    var source = Enumerable.Empty<TestObject<RadianAngle?>>();
                    Func<TestObject<RadianAngle?>, RadianAngle?> selector = e => e.Property;

                    // act
                    var result = AngularEnumerableExtensions.Min(source, selector);

                    // assert
                    result.Should().BeNull();
                }

                [Fact]
                public void ShouldReturnValidResult()
                {
                    // arrange
                    var source = Fixture.CreateMany<RadianAngle?>(3).Select(e => new TestObject<RadianAngle?>(e));
                    Func<TestObject<RadianAngle?>, RadianAngle?> selector = e => e.Property;

                    var expectedResult = source.OrderBy(e => e.Property.Value).First().Property;

                    // act
                    var result = AngularEnumerableExtensions.Min(source, selector);

                    // assert
                    result.Should().Be(expectedResult.Value);
                }
            }
        }
    }
}
