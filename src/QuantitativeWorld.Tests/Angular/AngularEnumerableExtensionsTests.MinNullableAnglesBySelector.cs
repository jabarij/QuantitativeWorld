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
        public class MinNullableAnglesBySelector : AngularEnumerableExtensionsTests
        {
            public MinNullableAnglesBySelector(TestFixture testFixture) : base(testFixture) { }

            [Fact]
            public void NullSource_ShouldThrow()
            {
                // arrange
                IEnumerable<TestObject<Angle?>> source = null;
                Func<TestObject<Angle?>, Angle?> selector = e => e.Property;

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
                var source = Enumerable.Empty<TestObject<Angle?>>();
                Func<TestObject<Angle?>, Angle?> selector = null;

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
                var source = Enumerable.Empty<TestObject<Angle?>>();
                Func<TestObject<Angle?>, Angle?> selector = e => e.Property;

                // act
                var result = AngularEnumerableExtensions.Min(source, selector);

                // assert
                result.Should().BeNull();
            }

            [Fact]
            public void ShouldReturnValidResult()
            {
                // arrange
                var source = Fixture.CreateMany<Angle?>(3).Select(e => new TestObject<Angle?>(e));
                Func<TestObject<Angle?>, Angle?> selector = e => e.Property;

                var expectedResult = source.OrderBy(e => e.Property.Value).First().Property;

                // act
                var result = AngularEnumerableExtensions.Min(source, selector);

                // assert
                result.Should().Be(expectedResult.Value);
            }
        }
    }
}
