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
        public class SumRadianAnglesBySelector : AngularEnumerableExtensionsTests
        {
            public SumRadianAnglesBySelector(TestFixture testFixture) : base(testFixture) { }

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
                double expectedResultInRadians = objects.Sum(e => e.Property.Radians);
                var expectedResult = new RadianAngle(expectedResultInRadians);

                // act
                var result = AngularEnumerableExtensions.Sum(objects, e => e.Property);

                // assert
                result.Radians.Should().Be(expectedResult.Radians);
            }
        }
    }
}
