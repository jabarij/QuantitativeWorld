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
        public class MaxAnglesBySelector : AngularEnumerableExtensionsTests
        {
            public MaxAnglesBySelector(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void NullSource_ShouldThrow()
            {
                // arrange
                IEnumerable<TestObject<Angle>> source = null;
                Func<TestObject<Angle>, Angle> selector = e => e.Property;

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
                var source = Enumerable.Empty<TestObject<Angle>>();
                Func<TestObject<Angle>, Angle> selector = null;

                // act
                Action max = () => AngularEnumerableExtensions.Max(source, selector);

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
                Action max = () => AngularEnumerableExtensions.Max(source, selector);

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
                var result = AngularEnumerableExtensions.Max(source, selector);

                // assert
                result.Should().Be(expectedResult);
            }
        }
    }
}
