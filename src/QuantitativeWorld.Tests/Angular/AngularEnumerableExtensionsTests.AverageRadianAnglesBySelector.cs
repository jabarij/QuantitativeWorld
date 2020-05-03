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
        public class AverageRadianAnglesBySelector : AngularEnumerableExtensionsTests
        {
            public AverageRadianAnglesBySelector(TestFixture testFixture) : base(testFixture) { }

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

                double expectedResultInRadians = source.Average(e => e.Property.Radians);
                var expectedResult = new RadianAngle(expectedResultInRadians);

                // act
                var result = AngularEnumerableExtensions.Average(source, selector);

                // assert
                result.Radians.Should().Be(expectedResult.Radians);
            }
        }
    }
}
