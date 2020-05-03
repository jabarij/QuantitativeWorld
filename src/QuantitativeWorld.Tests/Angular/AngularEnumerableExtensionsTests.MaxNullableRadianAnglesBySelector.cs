using AutoFixture;
using FluentAssertions;
using QuantitativeWorld.Angular;
using QuantitativeWorld.TestAbstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace QuantitativeWorld.Tests.Angular
{
    partial class AngularEnumerableExtensionsTests
    {
        public class MaxNullableRadianAnglesBySelector : AngularEnumerableExtensionsTests
        {
            public MaxNullableRadianAnglesBySelector(TestFixture testFixture) : base(testFixture) { }

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
    }
}
